using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>Lo que el votante eligió en una convocatoria (sale de EstadoDeSeleccion).</summary>
    public record SeleccionVoto(TipoConvocatoria Convocatoria, string Candidato, bool EsWriteIn);

    /// <summary>
    /// Guarda todos los votos: en memoria (para calcular rápido) y en votos.json
    /// (%LocalAppData%\SDVE) para que no se pierdan si se cierra el programa.
    /// Es estático: cualquier pantalla puede usarlo sin pasarse objetos de una a otra.
    /// </summary>
    public static class AlmacenVotos
    {
        private static readonly object Candado = new object();

        private static readonly JsonSerializerOptions Opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,   // acentos legibles en el archivo
            Converters = { new JsonStringEnumConverter() }           // "SociedadDeAlumnos" en lugar de 2
        };

        private static string _ruta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SDVE", "votos.json");

        private static List<Voto>? _votos;

        /// <summary>Ruta del archivo donde se guardan los votos (útil para la documentación).</summary>
        public static string RutaArchivo => _ruta;

        /// <summary>Copia de todos los votos guardados.</summary>
        public static IReadOnlyList<Voto> ObtenerTodos()
        {
            lock (Candado)
            {
                Cargar();
                return _votos!.ToList();
            }
        }

        /// <summary>
        /// Registra una papeleta completa (un Voto por convocatoria elegida) y devuelve el folio.
        /// Lanza InvalidOperationException si la papeleta no es válida e IOException si no puede escribir.
        /// </summary>
        public static string Registrar(Alumno votante, IReadOnlyList<SeleccionVoto> selecciones)
        {
            if (selecciones.Count == 0)
                throw new InvalidOperationException("La papeleta no tiene ninguna selección.");
            if (selecciones.Any(s => string.IsNullOrWhiteSpace(s.Candidato)))
                throw new InvalidOperationException("Hay una convocatoria sin candidato.");
            if (selecciones.Select(s => s.Convocatoria).Distinct().Count() != selecciones.Count)
                throw new InvalidOperationException("La papeleta repite una convocatoria.");

            lock (Candado)
            {
                var repetidas = selecciones.Where(s => votante.HaVotadoEn(s.Convocatoria)).ToList();
                if (repetidas.Count > 0)
                    throw new InvalidOperationException("Ya votaste en: " +
                        string.Join(", ", repetidas.Select(s => s.Convocatoria.ObtenerNombreMostrar())) + ".");

                Cargar();

                string folio = GenerarFolio();
                DateTime ahora = DateTime.Now;
                var nuevos = selecciones.Select(s => new Voto
                {
                    Folio = folio,
                    FechaHora = ahora,
                    Convocatoria = s.Convocatoria,
                    Candidato = Texto.Limpiar(s.Candidato),
                    EsWriteIn = s.EsWriteIn,
                    Grupo = votante.Grupo,
                    Carrera = votante.Carrera,
                    CentroUniversitario = votante.CentroUniversitario
                }).ToList();

                var combinado = new List<Voto>(_votos!);
                combinado.AddRange(nuevos);

                var anteriores = _votos!;
                Guardar(combinado);      // 1) los votos a disco...
                _votos = combinado;
                try
                {
                    AlmacenPadron.MarcarVotos(votante, selecciones.Select(s => s.Convocatoria));   // 2) ...y que ya votó
                }
                catch
                {
                    Guardar(anteriores);   // si no se pudo marcar, se deshace el voto
                    _votos = anteriores;
                    throw;
                }
                return folio;
            }
        }

        private static void Cargar()
        {
            if (_votos != null) return;

            if (!File.Exists(_ruta))
            {
                _votos = new List<Voto>();
                return;
            }

            try
            {
                _votos = JsonSerializer.Deserialize<List<Voto>>(File.ReadAllText(_ruta), Opciones) ?? new List<Voto>();
            }
            catch (JsonException ex)
            {
                throw new InvalidDataException("El archivo de votos (votos.json) está dañado.", ex);
            }
        }

        /// <summary>Escribe a un archivo temporal y lo reemplaza: un corte a media escritura no daña los votos.</summary>
        private static void Guardar(List<Voto> votos)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);
            string temporal = _ruta + ".tmp";
            File.WriteAllText(temporal, JsonSerializer.Serialize(votos, Opciones));
            File.Move(temporal, _ruta, overwrite: true);
        }

        private static string GenerarFolio(int longitud = 8)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var resultado = new char[longitud];
            for (int i = 0; i < longitud; i++)
                resultado[i] = caracteres[RandomNumberGenerator.GetInt32(caracteres.Length)];
            return new string(resultado);
        }
    }
}
