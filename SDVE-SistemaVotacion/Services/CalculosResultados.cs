using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>Una fila de la tabla de resultados: un candidato dentro de una convocatoria.</summary>
    public class FilaResultado
    {
        public int Posicion { get; set; }
        public TipoConvocatoria Convocatoria { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Candidato { get; set; } = string.Empty;

        /// <summary>true si NO está en la lista oficial (lo escribió el votante en "Otro").</summary>
        public bool EsWriteIn { get; set; }

        public int Votos { get; set; }

        /// <summary>Porcentaje sobre los votos de su convocatoria (0 a 100).</summary>
        public double Porcentaje { get; set; }
    }

    /// <summary>Todo lo que UcResultados, UcGraficas y UcExportar necesitan mostrar.</summary>
    public class ResumenResultados
    {
        public List<FilaResultado> Filas { get; set; } = new List<FilaResultado>();
        public int TotalVotos { get; set; }

        /// <summary>Votos para candidatos que no están en la lista oficial.</summary>
        public int VotosNoRegistrados { get; set; }

        /// <summary>Fila con más votos de toda la tabla (null si aún no hay votos).</summary>
        public FilaResultado? Lider { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }

    /// <summary>Convierte la lista de votos en resultados. No depende de ninguna pantalla.</summary>
    public static class CalculoResultados
    {
        /// <summary>Orden en que aparecen las convocatorias (el mismo de la papeleta).</summary>
        public static readonly TipoConvocatoria[] Orden =
        {
            TipoConvocatoria.SociedadDeAlumnos,
            TipoConvocatoria.ConsejoUniversitario,
            TipoConvocatoria.ConsejoDeRepresentantes
        };

        public static ResumenResultados Calcular(
            IEnumerable<Voto> votos,
            IReadOnlyDictionary<TipoConvocatoria, string[]>? oficiales = null)
        {
            var todos = votos.ToList();
            var resumen = new ResumenResultados { TotalVotos = todos.Count };

            foreach (var convocatoria in Orden)
                resumen.Filas.AddRange(FilasDe(convocatoria, todos, oficiales));

            resumen.VotosNoRegistrados = resumen.Filas.Where(f => f.EsWriteIn).Sum(f => f.Votos);
            if (resumen.TotalVotos > 0)
                resumen.Lider = resumen.Filas.OrderByDescending(f => f.Votos).First();   // empate: el primero

            return resumen;
        }

        private static IEnumerable<FilaResultado> FilasDe(
            TipoConvocatoria convocatoria, List<Voto> todos,
            IReadOnlyDictionary<TipoConvocatoria, string[]>? oficiales)
        {
            var votosC = todos.Where(v => v.Convocatoria == convocatoria).ToList();

            // clave del candidato -> nombre oficial
            var lista = new Dictionary<string, string>();
            if (oficiales != null && oficiales.TryGetValue(convocatoria, out var nombres))
                foreach (var n in nombres)
                {
                    string clave = Texto.Clave(n);
                    if (clave.Length > 0 && !lista.ContainsKey(clave)) lista[clave] = Texto.Limpiar(n);
                }

            // Agrupar los votos por candidato ignorando mayúsculas, acentos y espacios.
            var grupos = votosC.GroupBy(v => Texto.Clave(v.Candidato)).ToDictionary(g => g.Key, g => g.ToList());
            foreach (var clave in lista.Keys)
                if (!grupos.ContainsKey(clave)) grupos[clave] = new List<Voto>();

            var filas = new List<FilaResultado>();
            foreach (var par in grupos)
            {
                bool oficial = lista.ContainsKey(par.Key) || par.Value.Any(v => !v.EsWriteIn);
                string nombre = lista.TryGetValue(par.Key, out var nombreOficial)
                    ? nombreOficial
                    : par.Value.GroupBy(v => Texto.Limpiar(v.Candidato))       // la forma más escrita
                               .OrderByDescending(g => g.Count()).ThenBy(g => g.Key, StringComparer.Ordinal)
                               .First().Key;

                filas.Add(new FilaResultado
                {
                    Convocatoria = convocatoria,
                    Categoria = convocatoria.ObtenerNombreMostrar(),
                    Candidato = nombre,
                    EsWriteIn = !oficial,
                    Votos = par.Value.Count,
                    Porcentaje = votosC.Count > 0 ? par.Value.Count * 100.0 / votosC.Count : 0
                });
            }

            var ordenadas = filas.OrderByDescending(f => f.Votos)
                                 .ThenBy(f => f.Candidato, StringComparer.CurrentCultureIgnoreCase).ToList();

            // Posición con empates: 1, 1, 3 ...
            foreach (var f in ordenadas)
                f.Posicion = 1 + ordenadas.Count(o => o.Votos > f.Votos);

            return ordenadas;
        }
    }
}