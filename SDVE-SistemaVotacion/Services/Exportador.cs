using System.Globalization;
using System.Text;
using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion.Services
{
    public class DatosExportacion
    {
        public DateTime Fecha { get; set; }
        public int VotosRegistrados { get; set; }
        public List<FilaResultado> Resultados { get; set; } = new List<FilaResultado>();
        public List<FilaParticipacion> Participacion { get; set; } = new List<FilaParticipacion>();
        public int AlumnosEnPadron { get; set; }
        public int AlumnosQueVotaron { get; set; }
    }

    public static class Exportador
    {
        public static DatosExportacion Preparar(IEnumerable<Voto> votos, IEnumerable<Alumno> padron)
        {
            var listaVotos = votos.ToList();
            var listaPadron = padron.ToList();

            var resumen = CalculoResultados.Calcular(listaVotos, ListaOficial.Candidatos);

            var participacion = new List<FilaParticipacion>();
            foreach (var convocatoria in CalculoResultados.Orden)
                foreach (var agrupacion in Enum.GetValues<Agrupacion>())
                    participacion.AddRange(CalculoParticipacion.Calcular(listaVotos, listaPadron, convocatoria, agrupacion));

            return new DatosExportacion
            {
                AlumnosEnPadron = listaPadron.Count,
                AlumnosQueVotaron = listaPadron.Count(a => a.ConvocatoriasVotadas.Count > 0),
                Fecha = resumen.Fecha,
                VotosRegistrados = listaVotos.Count,
                Resultados = resumen.Filas,
                Participacion = participacion
            };
        }

        public static string RutaParticipacion(string rutaResultados) =>
            Path.Combine(Path.GetDirectoryName(rutaResultados) ?? string.Empty,
                         Path.GetFileNameWithoutExtension(rutaResultados) + "_participacion" + Path.GetExtension(rutaResultados));

        public static void ExportarCsv(string rutaResultados, DatosExportacion datos)
        {
            var resultadosOrdenados = datos.Resultados.OrderBy(f => f.Posicion).ToList();

            var resultados = new StringBuilder();
            resultados.AppendLine("sep=;"); // Fuerza a Excel a separar las columnas por punto y coma
            resultados.AppendLine("Posicion;Convocatoria;Candidato;Tipo;Votos;Porcentaje");
            foreach (var f in resultadosOrdenados)
            {
                resultados.AppendLine(string.Join(";",
                    f.Posicion.ToString(CultureInfo.InvariantCulture),
                    Campo(f.Categoria),
                    Campo(f.Candidato),
                    Campo(f.EsWriteIn ? "No registrado" : "Registrado"),
                    f.Votos.ToString(CultureInfo.InvariantCulture),
                    Decimal(f.Porcentaje)));
            }

            var participacionOrdenada = datos.Participacion
                .OrderByDescending(f => f.PorcentajeParticipacion)
                .ThenByDescending(f => f.Abstenciones)
                .ToList();

            var participacion = new StringBuilder();
            participacion.AppendLine("sep=;"); // Fuerza a Excel a separar las columnas por punto y coma
            participacion.AppendLine("Convocatoria;TipoAgrupacion;Agrupacion;Electores;Votaron;Abstenciones;PorcentajeParticipacion;PorcentajeAbstencion");
            foreach (var f in participacionOrdenada)
            {
                participacion.AppendLine(string.Join(";",
                    Campo(f.Convocatoria.ObtenerNombreMostrar()),
                    Campo(f.Agrupacion.NombreVisible()),
                    Campo(f.Valor),
                    f.Electores.ToString(CultureInfo.InvariantCulture),
                    f.Votaron.ToString(CultureInfo.InvariantCulture),
                    f.Abstenciones.ToString(CultureInfo.InvariantCulture),
                    Decimal(f.PorcentajeParticipacion),
                    Decimal(f.PorcentajeAbstencion)));
            }

            var utf8ConBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            File.WriteAllText(rutaResultados, resultados.ToString(), utf8ConBom);
            File.WriteAllText(RutaParticipacion(rutaResultados), participacion.ToString(), utf8ConBom);
        }

        private static string Decimal(double valor) => valor.ToString("0.00", CultureInfo.InvariantCulture);

        private static string Campo(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;
            if ("=+-@\t\r".IndexOf(texto[0]) >= 0) texto = "'" + texto;
            if (texto.IndexOfAny(new[] { ';', '"', '\n', '\r' }) >= 0)
                texto = "\"" + texto.Replace("\"", "\"\"") + "\"";
            return texto;
        }
    }
}