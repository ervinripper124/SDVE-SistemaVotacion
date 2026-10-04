using System;
using System.Collections.Generic;
using System.Text;

namespace SDVE_SistemaVotacion.Models
{
    public class Voto
    {
        public string Folio { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public TipoConvocatoria Convocatoria { get; set; }
        public string Candidato { get; set; } = string.Empty;
        public bool EsWriteIn { get; set; } = false; // Registra si fue voto manual ("Otro")

        // Atributos de segmentación sin relación con la matrícula del alumno (voto secreto)
        public string CentroUniversitario { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string Grupo { get; set; } = string.Empty;
    }
}
