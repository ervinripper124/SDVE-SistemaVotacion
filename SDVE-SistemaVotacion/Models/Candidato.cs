using System;
using System.Collections.Generic;
using System.Text;

namespace SDVE_SistemaVotacion.Models
{
    public class Candidato
    {
        public string IdCandidato { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public TipoConvocatoria Convocatoria { get; set; }
    }
}
