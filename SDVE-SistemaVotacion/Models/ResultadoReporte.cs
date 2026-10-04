using System;
using System.Collections.Generic;
using System.Text;

namespace SDVE_SistemaVotacion.Models
{
    public class ResultadoConvocatoria
    {
        public TipoConvocatoria Convocatoria { get; set; }
        public int TotalVotosEmitidos { get; set; }
        public int AbstencionismoAbsoluto { get; set; }
        public double PorcentajeParticipacion { get; set; }
        public double PorcentajeAbstencionismo { get; set; }

        // Conteo y distribución por candidato
        public Dictionary<string, int> VotosPorCandidato { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, double> PorcentajesPorCandidato { get; set; } = new Dictionary<string, double>();
    }

    public class ResultadoReporte
    {
        public int TotalElectoresPadron { get; set; }
        public List<ResultadoConvocatoria> ResultadosPorConvocatoria { get; set; } = new List<ResultadoConvocatoria>();
    }
}
