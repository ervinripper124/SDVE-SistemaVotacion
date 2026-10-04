using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>
    /// Candidatos oficiales de cada convocatoria (los mismos nombres que muestran las pantallas de
    /// votación). Sirve para que los resultados incluyan a los candidatos con 0 votos y para saber
    /// quién es "registrado" y quién escribieron los votantes en "Otro".
    /// Si cambian un nombre en una pantalla de votación, cámbienlo también aquí.
    /// </summary>
    public static class ListaOficial
    {
        public static readonly IReadOnlyDictionary<TipoConvocatoria, string[]> Candidatos =
            new Dictionary<TipoConvocatoria, string[]>
            {
                { TipoConvocatoria.SociedadDeAlumnos,       new[] { "Sebastian", "Juan", "Arevalo" } },
                { TipoConvocatoria.ConsejoUniversitario,    new[] { "Gael Zair", "Marcos Aranda", "Ervin Rocha" } },
                { TipoConvocatoria.ConsejoDeRepresentantes, new[] { "Gael Zair", "Marcos Aranda", "Ervin Rocha" } }
            };
    }
}
