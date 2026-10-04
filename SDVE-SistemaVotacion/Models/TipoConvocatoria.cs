using System;
using System.Collections.Generic;
using System.Text;

namespace SDVE_SistemaVotacion.Models
{
    public enum TipoConvocatoria
    {
        ConsejoUniversitario,
        ConsejoDeRepresentantes,
        SociedadDeAlumnos
    }

    public static class ConvocatoriaExtensions
    {
        public static string ObtenerNombreMostrar(this TipoConvocatoria tipo)
        {
            return tipo switch
            {
                TipoConvocatoria.ConsejoUniversitario => "Consejo Universitario",
                TipoConvocatoria.ConsejoDeRepresentantes => "Consejo de Representantes",
                TipoConvocatoria.SociedadDeAlumnos => "Sociedad de Alumnos",
                _ => tipo.ToString()
            };
        }
    }
}