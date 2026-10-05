using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public partial class UcExportar : UserControl
    {
        // Paleta UAA: azul marino, blanco, poco rojo
        private static readonly Color Azul = Color.FromArgb(10, 42, 102);
        private static readonly Color Rojo = Color.FromArgb(200, 16, 46);
        private static readonly Color Fondo = Color.FromArgb(244, 246, 251);
        private static readonly Color Borde = Color.FromArgb(222, 227, 238);
        private static readonly Color TextoSuave = Color.FromArgb(107, 115, 133);

        private DatosExportacion? datos;

        public UcExportar()
        {
            InitializeComponent();
            AplicarEstiloUAA();
            btnExportar.Click += btnExportar_Click;
            this.Load += (s, e) => CargarResumen();
        }

        private void AplicarEstiloUAA()
        {
            int ancho = panelExportar.Width;

            // 1. Fondo
            panelExportar.FillColor = Fondo;
            panelExportar.BackColor = Fondo;

            // 2. Encabezado azul con línea roja
            var encabezado = new Guna2Panel
            {
                FillColor = Azul,
                BackColor = Azul,
                Location = new Point(0, 0),
                Size = new Size(ancho, 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var titulo = new Label
            {
                Text = "Exportar resultados",
                AutoSize = true,
                Font = HelperFuentes.BricolageBold(15f),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 15)
            };
            var lineaRoja = new Panel
            {
                BackColor = Rojo,
                Dock = DockStyle.Bottom,
                Height = 3
            };
            encabezado.Controls.Add(titulo);
            encabezado.Controls.Add(lineaRoja);
            panelExportar.Controls.Add(encabezado);

            // 3. Tarjeta central
            int anchoTarjeta = 560, altoTarjeta = 256;
            var tarjeta = new Guna2Panel
            {
                FillColor = Color.White,
                BackColor = Fondo,
                BorderColor = Borde,
                BorderThickness = 1,
                BorderRadius = 12,
                Size = new Size(anchoTarjeta, altoTarjeta),
                Location = new Point((ancho - anchoTarjeta) / 2, 84),
                Anchor = AnchorStyles.None
            };
            panelExportar.Controls.Add(tarjeta);

            // 4. Datos del resumen
            var filas = new[] { lVotosRegistrados, lFilasDeResultados, lAlumnosVotaron, lFechaExportar };
            for (int i = 0; i < filas.Length; i++)
            {
                tarjeta.Controls.Add(filas[i]);
                filas[i].Font = HelperFuentes.InstrumentRegular(11f);
                filas[i].ForeColor = Azul;
                filas[i].BackColor = Color.Transparent;
                filas[i].Location = new Point(30, 24 + i * 34);
            }

            // 5. Nota
            var nota = new Label
            {
                Text = "Se crean dos archivos CSV: resultados y participación.",
                AutoSize = true,
                Font = HelperFuentes.InstrumentRegular(10f),
                ForeColor = TextoSuave,
                BackColor = Color.Transparent,
                Location = new Point(30, 162)
            };
            tarjeta.Controls.Add(nota);

            // 6. Botón
            tarjeta.Controls.Add(btnExportar);
            btnExportar.Text = "Exportar archivo";
            btnExportar.Font = HelperFuentes.BricolageBold(11f);
            btnExportar.ForeColor = Color.White;
            btnExportar.FillColor = Azul;
            btnExportar.HoverState.FillColor = Rojo;
            btnExportar.BorderRadius = 10;
            btnExportar.Cursor = Cursors.Hand;
            btnExportar.Size = new Size(240, 48);
            btnExportar.Location = new Point((anchoTarjeta - btnExportar.Width) / 2, 192);
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
            Form1? parent = this.FindForm() as Form1;
            if (parent != null)
            {
                UcResultados r = new UcResultados();
                parent.Ir(r);
            }
        }

        private void pnlGraficas_Click(object sender, EventArgs e)
        {
            Form1? parent = this.FindForm() as Form1;
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