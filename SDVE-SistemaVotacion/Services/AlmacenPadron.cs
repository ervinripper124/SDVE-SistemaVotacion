using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>
    /// El padrón: la lista de alumnos con derecho a voto (padron.csv) y quién ya votó en qué convocatoria
    /// (estado_padron.json). El CSV nunca se modifica; solo se guarda "ya votó", nunca por quién.
    /// Todo vive en %LocalAppData%\SDVE. Si falta padron.csv, se copia el que viaja junto al programa
    /// (carpeta Datos del ejecutable).
    /// </summary>
    public static class AlmacenPadron
    {
        private static readonly string[] Columnas = { "Id", "Nombre", "Grupo", "Carrera", "CentroUniversitario" };
        private static readonly object Candado = new object();

        private static readonly JsonSerializerOptions Opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumConverter() }
        };

        private static readonly string _carpeta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SDVE");

        private static readonly string _padronIncluido = Path.Combine(AppContext.BaseDirectory, "Datos", "padron.csv");

        private static List<Alumno>? _alumnos;
        private static Dictionary<string, Alumno> _indice = new Dictionary<string, Alumno>();

        private static string RutaCsv => Path.Combine(_carpeta, "padron.csv");
        private static string RutaEstado => Path.Combine(_carpeta, "estado_padron.json");

        /// <summary>Carpeta donde viven los archivos de datos (para la documentación y el soporte).</summary>
        public static string Carpeta => _carpeta;

        /// <summary>Avisos de la última carga (filas incompletas, matrículas repetidas...).</summary>
        public static List<string> Advertencias { get; } = new List<string>();

        /// <summary>Todos los alumnos del padrón (carga el archivo la primera vez).</summary>
        public static IReadOnlyList<Alumno> Alumnos
        {
            get { lock (Candado) { Cargar(); return _alumnos!; } }
        }

        public static int TotalElectores => Alumnos.Count;

        /// <summary>Busca un alumno por ID (ignora espacios y mayúsculas). Null si no existe.</summary>
        public static Alumno? Identificar(string? id)
        {
            lock (Candado)
            {
                Cargar();
                _indice.TryGetValue(Clave(id), out var alumno);
                return alumno;
            }
        }

        /// <summary>Convocatorias en las que el alumno todavía puede votar, en el orden de la papeleta.</summary>
        public static IReadOnlyList<TipoConvocatoria> Disponibles(Alumno alumno) =>
            CalculoResultados.Orden.Where(c => !alumno.HaVotadoEn(c)).ToList();

        /// <summary>Marca que el alumno ya votó en esas convocatorias y lo guarda en disco.</summary>
        public static void MarcarVotos(Alumno alumno, IEnumerable<TipoConvocatoria> convocatorias)
        {
            lock (Candado)
            {
                Cargar();
                var anteriores = alumno.ConvocatoriasVotadas.ToList();
                foreach (var c in convocatorias) alumno.RegistrarVoto(c);
                try
                {
                    GuardarEstado();
                }
                catch
                {
                    alumno.ConvocatoriasVotadas.Clear();                  // si no se pudo guardar, no se queda a medias
                    foreach (var c in anteriores) alumno.RegistrarVoto(c);
                    throw;
                }
            }
        }

        // ---------- carga ----------

        private static void Cargar()
        {
            if (_alumnos != null) return;

            Advertencias.Clear();
            Directory.CreateDirectory(_carpeta);

            if (!File.Exists(RutaCsv) && File.Exists(_padronIncluido))
                File.Copy(_padronIncluido, RutaCsv);

            if (!File.Exists(RutaCsv))
                throw new FileNotFoundException(
                    "No se encontró el padrón de alumnos. Coloque el archivo padron.csv en: " + _carpeta);

            var alumnos = LeerAlumnos();
            var indice = new Dictionary<string, Alumno>();
            foreach (var a in alumnos) indice[Clave(a.Id)] = a;

            AplicarEstado(indice);

            _alumnos = alumnos;
            _indice = indice;
        }

        private static List<Alumno> LeerAlumnos()
        {
            var alumnos = new List<Alumno>();
            var vistas = new HashSet<string>();

            foreach (var fila in LectorCsv.Leer(RutaCsv, Columnas, Advertencias))
            {
                string id = fila.Campos[0];
                if (id.Length == 0)
                {
                    Advertencias.Add($"padron.csv: fila {fila.Numero} sin ID, se omitió.");
                    continue;
                }
                if (!vistas.Add(Clave(id)))
                {
                    Advertencias.Add($"padron.csv: ID repetido '{id}' en la fila {fila.Numero}, se omitió.");
                    continue;
                }

                alumnos.Add(new Alumno
                {
                    Id = id,
                    Nombre = fila.Campos[1],
                    Grupo = fila.Campos[2],
                    Carrera = fila.Campos[3],
                    CentroUniversitario = fila.Campos[4]
                });
            }

            if (alumnos.Count == 0)
                throw new InvalidDataException("El padrón no contiene ningún alumno válido.");

            return alumnos;
        }

        private static void AplicarEstado(Dictionary<string, Alumno> indice)
        {
            if (!File.Exists(RutaEstado)) return;

            Dictionary<string, List<TipoConvocatoria>>? estado;
            try
            {
                estado = JsonSerializer.Deserialize<Dictionary<string, List<TipoConvocatoria>>>(
                    File.ReadAllText(RutaEstado), Opciones);
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("El archivo estado_padron.json está dañado.", ex);
            }
            if (estado == null) return;

            foreach (var par in estado)
            {
                if (!indice.TryGetValue(Clave(par.Key), out var alumno))
                {
                    Advertencias.Add($"estado_padron.json: la matrícula {par.Key} ya no está en el padrón.");
                    continue;
                }
                foreach (var c in par.Value) alumno.RegistrarVoto(c);
            }
        }

        /// <summary>Escribe a un archivo temporal y lo reemplaza: un corte a media escritura no daña nada.</summary>
        private static void GuardarEstado()
        {
            var estado = _alumnos!
                .Where(a => a.ConvocatoriasVotadas.Count > 0)
                .ToDictionary(a => Clave(a.Id), a => a.ConvocatoriasVotadas.ToList());

            string temporal = RutaEstado + ".tmp";
            File.WriteAllText(temporal, JsonSerializer.Serialize(estado, Opciones));
            File.Move(temporal, RutaEstado, overwrite: true);
        }

        private static string Clave(string? id) => (id ?? string.Empty).Trim().ToUpperInvariant();
    }
}
