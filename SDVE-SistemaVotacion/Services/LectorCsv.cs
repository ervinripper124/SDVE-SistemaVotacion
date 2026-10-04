using System.Text;

namespace SDVE_SistemaVotacion.Services
{
    /// <summary>Una fila de datos leída de un CSV (Numero = número de línea en el archivo).</summary>
    internal record FilaCsv(int Numero, string[] Campos);

    /// <summary>
    /// Lector de CSV sencillo: detecta ',' o ';', respeta comillas, acepta encabezados
    /// con o sin acentos y en cualquier orden, y lee UTF-8 (con o sin BOM) o ANSI/Latin-1.
    /// </summary>
    internal static class LectorCsv
    {
        public static List<FilaCsv> Leer(string ruta, string[] columnas, List<string> advertencias)
        {
            string archivo = Path.GetFileName(ruta);
            var lineas = LeerLineas(ruta);
            if (lineas.Length == 0 || string.IsNullOrWhiteSpace(lineas[0]))
                throw new InvalidDataException($"El archivo {archivo} está vacío.");

            char separador = lineas[0].Count(c => c == ';') > lineas[0].Count(c => c == ',') ? ';' : ',';

            var encabezados = ParsearLinea(lineas[0], separador).Select(Texto.Clave).ToList();
            var posiciones = new int[columnas.Length];
            for (int i = 0; i < columnas.Length; i++)
            {
                posiciones[i] = encabezados.IndexOf(Texto.Clave(columnas[i]));
                if (posiciones[i] < 0)
                    throw new InvalidDataException(
                        $"Al archivo {archivo} le falta una columna. Debe tener: {string.Join(", ", columnas)}.");
            }

            var filas = new List<FilaCsv>();
            for (int n = 1; n < lineas.Length; n++)
            {
                if (string.IsNullOrWhiteSpace(lineas[n])) continue;

                var campos = ParsearLinea(lineas[n], separador);
                if (campos.Count < encabezados.Count)
                {
                    advertencias.Add($"{archivo}: fila {n + 1} incompleta, se omitió.");
                    continue;
                }
                filas.Add(new FilaCsv(n + 1, posiciones.Select(p => campos[p].Trim()).ToArray()));
            }
            return filas;
        }

        /// <summary>UTF-8 (con o sin BOM) si es válido; si no, ANSI/Latin-1 (como guarda Excel). Así los acentos no se dañan.</summary>
        private static string[] LeerLineas(string ruta)
        {
            byte[] bytes = File.ReadAllBytes(ruta);
            string texto;
            try
            {
                texto = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true).GetString(bytes);
            }
            catch (DecoderFallbackException)
            {
                texto = Encoding.Latin1.GetString(bytes);
            }
            return texto.TrimStart('\uFEFF').Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
        }

        private static List<string> ParsearLinea(string linea, char separador)
        {
            var campos = new List<string>();
            var actual = new StringBuilder();
            bool entreComillas = false;

            for (int i = 0; i < linea.Length; i++)
            {
                char c = linea[i];
                if (entreComillas)
                {
                    if (c == '"' && i + 1 < linea.Length && linea[i + 1] == '"') { actual.Append('"'); i++; }
                    else if (c == '"') entreComillas = false;
                    else actual.Append(c);
                }
                else if (c == '"') entreComillas = true;
                else if (c == separador) { campos.Add(actual.ToString()); actual.Clear(); }
                else actual.Append(c);
            }
            campos.Add(actual.ToString());
            return campos;
        }
    }
}