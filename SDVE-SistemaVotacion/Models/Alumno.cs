using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;

namespace SDVE_SistemaVotacion.Models
{
    public class Alumno
    {
        public string Matricula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CentroUniversitario { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string Grupo { get; set; } = string.Empty;

        // Guarda qué convocatorias ha votado sin vincular la opción elegida
        public HashSet<TipoConvocatoria> ConvocatoriasVotadas { get; set; } = new HashSet<TipoConvocatoria>();

        public bool HaVotadoEn(TipoConvocatoria convocatoria)
        {
            return ConvocatoriasVotadas.Contains(convocatoria);
        }

        public void RegistrarVoto(TipoConvocatoria convocatoria)
        {
            ConvocatoriasVotadas.Add(convocatoria);
        }
    }
}
