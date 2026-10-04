using System.Globalization;
using System.Text;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>Utilidades para comparar textos escritos a mano ("Juan Pérez" = "juan perez").</summary>
    internal static class Texto
    {
        /// <summary>Clave de comparación: sin acentos, en minúsculas y solo letras y dígitos.</summary>
        public static string Clave(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return string.Empty;

            var sb = new StringBuilder();
            foreach (char c in texto.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
                if (char.IsLetterOrDigit(c)) sb.Append(char.ToLowerInvariant(c));
            }
            return sb.ToString();
        }

        /// <summary>Quita espacios de sobra: "  Juan   Pérez " -> "Juan Pérez".</summary>
        public static string Limpiar(string? texto) =>
            string.Join(" ", (texto ?? string.Empty).Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
    }
}
