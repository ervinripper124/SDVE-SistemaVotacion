using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public partial class UcExportar : UserControl
    {
        private DatosExportacion? datos;

        public UcExportar()
        {
            InitializeComponent();
            btnExportar.Click += btnExportar_Click;
            this.Load += (s, e) => CargarResumen();
        }

        private bool CargarResumen()
        {
            try
            {
                datos = Exportador.Preparar(AlmacenVotos.ObtenerTodos(), AlmacenPadron.Alumnos);
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidDataException || ex is UnauthorizedAccessException)
            {
                datos = null;
                MessageBox.Show("No se pudieron leer los datos para exportar:\n\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            double porcentajeAlumnos = datos.AlumnosEnPadron > 0
            ? datos.AlumnosQueVotaron * 100.0 / datos.AlumnosEnPadron : 0;

            lVotosRegistrados.Text = "Votos registrados: " + datos.VotosRegistrados;
            lFilasDeResultados.Text = "Filas de resultados: " + datos.Resultados.Count;
            lAlumnosVotaron.Text = "Alumnos que votaron: " + datos.AlumnosQueVotaron + " de " +
                                   datos.AlumnosEnPadron + " (" + porcentajeAlumnos.ToString("0.0") + "%)";
            lFechaExportar.Text = "Fecha de generación: " + datos.Fecha.ToString("dd/MM/yyyy HH:mm");
            return true;
        }

        private void btnExportar_Click(object? sender, EventArgs e)
        {
            if (!CargarResumen() || datos == null) return;

            if (datos.VotosRegistrados == 0)
            {
                MessageBox.Show("Aún no hay votos registrados, así que no hay nada que exportar.",
                                "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Title = "Exportar resultados";
                dialogo.Filter = "Archivo CSV (*.csv)|*.csv";
                dialogo.DefaultExt = "csv";
                dialogo.AddExtension = true;
                dialogo.OverwritePrompt = true;
                dialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialogo.FileName = "resultados_sdve_" + datos.Fecha.ToString("yyyy-MM-dd_HHmm") + ".csv";

                if (dialogo.ShowDialog(this) != DialogResult.OK) return;

                string rutaResultados = dialogo.FileName;
                string rutaParticipacion = Exportador.RutaParticipacion(rutaResultados);

                if (File.Exists(rutaParticipacion))
                {
                    var respuesta = MessageBox.Show(
                        "Ya existe el archivo:\n" + rutaParticipacion + "\n\n¿Deseas reemplazarlo?",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta != DialogResult.Yes) return;
                }

                try
                {
                    Exportador.ExportarCsv(rutaResultados, datos);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                {
                    MessageBox.Show(
                        "No se pudo guardar el archivo. Si está abierto en Excel, ciérralo e inténtalo de nuevo.\n\n" + ex.Message,
                        "Error al exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(
                    "Se crearon dos archivos:\n\n" +
                    "• Resultados (" + datos.Resultados.Count + " filas):\n" + rutaResultados + "\n\n" +
                    "• Participación (" + datos.Participacion.Count + " filas):\n" + rutaParticipacion,
                    "Exportación lista", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlResultados_Click(object sender, EventArgs e)
        {
            Form1 parent = this.FindForm() as Form1;
            if (parent != null)
            {
                UcResultados r = new UcResultados();
                parent.Ir(r);
            }
        }

        private void pnlGraficas_Click(object sender, EventArgs e)
        {
            Form1 parent = this.FindForm() as Form1;
            if (parent != null)
            {
                UcGraficas g = new UcGraficas();
                parent.Ir(g);
            }
        }

        private void panelExportar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}