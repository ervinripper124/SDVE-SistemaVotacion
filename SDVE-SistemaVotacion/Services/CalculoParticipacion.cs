using System;
using System.Collections.Generic;
using System.Linq;
using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>Cómo se agrupa la participación.</summary>
    public enum Agrupacion
    {
        General,
        Grupo,
        Carrera,
        CentroUniversitario
    }

    public static class AgrupacionExtensions
    {
        public static string NombreVisible(this Agrupacion a) => a switch
        {
            Agrupacion.General => "General",
            Agrupacion.Grupo => "Grupo",
            Agrupacion.Carrera => "Carrera",
            Agrupacion.CentroUniversitario => "Centro Universitario",
            _ => a.ToString()
        };
    }

    /// <summary>Participación y abstencionismo de una convocatoria dentro de un grupo (o en general).</summary>
    public class FilaParticipacion
    {
        public TipoConvocatoria Convocatoria { get; set; }
        public Agrupacion Agrupacion { get; set; }

        /// <summary>"3A", "CUCEI"... Para Agrupacion.General es "Todos".</summary>
        public string Valor { get; set; } = string.Empty;

        /// <summary>Alumnos del padrón (con derecho a voto) en ese grupo.</summary>
        public int Electores { get; set; }

        /// <summary>Alumnos que votaron en esa convocatoria.</summary>
        public int Votaron { get; set; }

        /// <summary>Alumnos que NO votaron (votos que faltaron).</summary>
        public int Abstenciones { get; set; }

        public double PorcentajeParticipacion { get; set; }
        public double PorcentajeAbstencion { get; set; }
    }

    /// <summary>
    /// Cruza los votos con el padrón para calcular participación y abstencionismo,
    /// en general o por Grupo, Carrera o Centro Universitario.
    /// </summary>
    public static class CalculoParticipacion
    {
        public const string ValorGeneral = "Todos";

        public static List<FilaParticipacion> Calcular(
            IEnumerable<Voto> votos, IEnumerable<Alumno> padron, TipoConvocatoria convocatoria, Agrupacion por)
        {
            var alumnos = padron.ToList();
            var votosC = votos.Where(v => v.Convocatoria == convocatoria).ToList();

            // Valores posibles del grupo: los del padrón y los que aparezcan en votos.
            var valores = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (por == Agrupacion.General)
            {
                valores.Add(ValorGeneral);
            }
            else
            {
                foreach (var a in alumnos) valores.Add(Valor(por, a.Grupo, a.Carrera, a.CentroUniversitario));
                foreach (var v in votosC) valores.Add(Valor(por, v.Grupo, v.Carrera, v.CentroUniversitario));
                valores.Remove(string.Empty);
            }

            var filas = new List<FilaParticipacion>();
            foreach (var valor in valores.OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase))
            {
                int electores = alumnos.Count(a => Pertenece(por, valor, a.Grupo, a.Carrera, a.CentroUniversitario));
                int votaron = votosC.Count(v => Pertenece(por, valor, v.Grupo, v.Carrera, v.CentroUniversitario));

                double participacion = electores > 0 ? Math.Min(100.0, (votaron * 100.0) / electores) : 0;

                filas.Add(new FilaParticipacion
                {
                    Convocatoria = convocatoria,
                    Agrupacion = por,
                    Valor = valor,
                    Electores = electores,
                    Votaron = votaron,
                    Abstenciones = Math.Max(0, electores - votaron),
                    PorcentajeParticipacion = participacion,
                    PorcentajeAbstencion = electores > 0 ? 100.0 - participacion : 0
                });
            }
            return filas;
        }

        /// <summary>Participación general de una convocatoria (una sola fila).</summary>
        public static FilaParticipacion General(IEnumerable<Voto> votos, IEnumerable<Alumno> padron, TipoConvocatoria c) =>
            Calcular(votos, padron, c, Agrupacion.General)[0];

        private static string Valor(Agrupacion por, string grupo, string carrera, string centro) => por switch
        {
            Agrupacion.Grupo => grupo,
            Agrupacion.Carrera => carrera,
            Agrupacion.CentroUniversitario => centro,
            _ => ValorGeneral
        };

        private static bool Pertenece(Agrupacion por, string valor, string grupo, string carrera, string centro) =>
            por == Agrupacion.General ||
            string.Equals(Valor(por, grupo, carrera, centro), valor, StringComparison.OrdinalIgnoreCase);
    }
}