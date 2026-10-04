using System;
using System.Drawing;
using System.Windows.Forms;
using SDVE_SistemaVotacion.Services;
using SDVE_SistemaVotacion.Models;

namespace SDVE_SistemaVotacion
{
    public partial class UcConfirmacion : UserControl
    {
        private UcPapeleta.EstadoDeSeleccion EstadoRecibido;
        private int progreso = 0;

        public UcPapeleta.EstadoDeSeleccion EstadoActual => EstadoRecibido;

        public UcConfirmacion(UcPapeleta.EstadoDeSeleccion Estado)
        {
            InitializeComponent();

            EstadoRecibido = Estado;
            lblConfirmacionCandidatoSociedadA.Text = Estado.candidatoSociedad;
            lblConfirmacionCandidatoConsejoU.Text = Estado.candidatoConsejoU;
            lblConfirmacionCandidatoConsejoR.Text = Estado.candidatoConsejoR;
        }

        private void UcConfirmacion_Load(object sender, EventArgs e)
        {
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy");

            // 1. Asegurar que las imágenes pertenezcan al UserControl
            this.Controls.Add(pbSociedadVacio);
            this.Controls.Add(pbCUVacio);
            this.Controls.Add(pbCRVacio);

            // 2. Calibrar posición (X, Y) y tamaño exacto respecto a cada panel
            pbSociedadVacio.Location = pnlSociedad.Location;
            pbSociedadVacio.Size = pnlSociedad.Size;

            pbCUVacio.Location = pnlCU.Location;
            pbCUVacio.Size = pnlCU.Size;

            pbCRVacio.Location = pnlCR.Location;
            pbCRVacio.Size = pnlCR.Size;

            // 3. Mostrar u ocultar según el estado de la votación
            mostrarPaneles(EstadoRecibido);

            // Tipografías y Colores Institucionales UAA
            lblRevisar.Font = HelperFuentes.BricolageBold(15f);
            lblRevisar.ForeColor = Color.FromArgb(10, 25, 49); // Azul UAA

            // Aplicar Instrument Sans a los textos de las tarjetas
            lbl1.Font = HelperFuentes.InstrumentRegular(11f);
            lblConfirmacionCandidatoSociedadA.Font = HelperFuentes.BricolageBold(8f);
            lbl1.ForeColor = Color.FromArgb(10, 25, 49);

            lbl2.Font = HelperFuentes.InstrumentRegular(11f);
            lblConfirmacionCandidatoConsejoU.Font = HelperFuentes.BricolageBold(8f);
            lbl2.ForeColor = Color.FromArgb(10, 25, 49);

            lbl3.Font = HelperFuentes.InstrumentRegular(11f);
            lblConfirmacionCandidatoConsejoR.Font = HelperFuentes.BricolageBold(8f);
            lbl3.ForeColor = Color.FromArgb(10, 25, 49);

            lblFecha.Font = HelperFuentes.InstrumentRegular(10f);

            // --- CONFIGURACIÓN DE LA ETIQUETA SOBRE LA BARRA DE PROGRESO ---
            lblEnviar.Font = HelperFuentes.BricolageBold(12f);
            lblEnviar.Parent = pbEnviar; // Vincular directamente a la barra para fondo 100% transparente
            lblEnviar.BackColor = Color.Transparent;

            // VINCULAR EVENTOS DEL MOUSE PARA QUE RESPONDA AL CLIC SOBRE EL TEXTO
            lblEnviar.MouseDown += pbEnviar_MouseDown;
            lblEnviar.MouseUp += pbEnviar_MouseUp;
            lblEnviar.MouseLeave += pbEnviar_MouseLeave;

            CancelarEnvio();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            ProcesarRegistroVoto();
        }

        private void ProcesarRegistroVoto()
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal == null) return;

            Alumno? alumno = ventanaPrincipal.AlumnoActual;
            if (alumno == null)
            {
                MessageBox.Show("Se perdió la identificación del alumno. Vuelve a comenzar.", "Sesión no válida",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ventanaPrincipal.MostrarBienvenida();
                return;
            }

            string folio;
            try
            {
                folio = AlmacenVotos.Registrar(alumno, EstadoRecibido.ComoSelecciones());
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is IOException
                                       || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("No se pudo registrar el voto: " + ex.Message, "Error al guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ventanaPrincipal.AlumnoActual = null;
            ventanaPrincipal.Ir(new UcVotoRegistrado(folio), guardarEnHistorial: false);
        }

        private void mostrarPaneles(UcPapeleta.EstadoDeSeleccion Estado)
        {
            bool tieneSociedad = !string.IsNullOrWhiteSpace(Estado.candidatoSociedad);
            bool tieneCU = !string.IsNullOrWhiteSpace(Estado.candidatoConsejoU);
            bool tieneCR = !string.IsNullOrWhiteSpace(Estado.candidatoConsejoR);

            pnlSociedad.Visible = tieneSociedad;
            pbSociedadVacio.Visible = !tieneSociedad;
            if (!tieneSociedad) pbSociedadVacio.BringToFront();

            pnlCU.Visible = tieneCU;
            pbCUVacio.Visible = !tieneCU;
            if (!tieneCU) pbCUVacio.BringToFront();

            pnlCR.Visible = tieneCR;
            pbCRVacio.Visible = !tieneCR;
            if (!tieneCR) pbCRVacio.BringToFront();
        }

        #region Navegación para Modificar Selección
        private void btnCambiarSA_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            ventanaPrincipal?.Ir(new UcVotarSociedadDeAlumnos(EstadoRecibido));
        }

        private void btnCambiarCU_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            ventanaPrincipal?.Ir(new UcVotarConsejoUniversitario(EstadoRecibido));
        }

        private void btnCambiarCR_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            ventanaPrincipal?.Ir(new UcVotarConsejoDeRepresentantes(EstadoRecibido));
        }
        #endregion

        #region Lógica Hold to Confirm (pbEnviar + lblEnviar)
        private void pbEnviar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                progreso = 0;
                pbEnviar.Value = 0;
                lblEnviar.ForeColor = Color.White;
                timerEnvio.Start();
            }
        }

        private void pbEnviar_MouseUp(object sender, MouseEventArgs e)
        {
            CancelarEnvio();
        }

        private void pbEnviar_MouseLeave(object sender, EventArgs e)
        {
            CancelarEnvio();
        }

        private void CancelarEnvio()
        {
            timerEnvio.Stop();
            progreso = 0;
            pbEnviar.Value = 0;

            lblEnviar.Text = "Mantén presionado para enviar";
            lblEnviar.ForeColor = Color.White;

            CentrarTextoEnBarra();
        }

        private void timerEnvio_Tick(object sender, EventArgs e)
        {
            progreso += 2;

            if (progreso <= 100)
            {
                pbEnviar.Value = progreso;
            }
            else
            {
                timerEnvio.Stop();
                pbEnviar.Value = 100;
                lblEnviar.Text = "¡Voto Confirmado!";
                CentrarTextoEnBarra();

                // Llamamos al método centralizado para registrar el voto de forma segura
                ProcesarRegistroVoto();
            }
        }

        private void CentrarTextoEnBarra()
        {
            if (pbEnviar != null && lblEnviar != null)
            {
                lblEnviar.Location = new Point(
                    (pbEnviar.Width - lblEnviar.Width) / 2,
                    (pbEnviar.Height - lblEnviar.Height) / 2
                );
            }
        }
        #endregion

        private void guna2HtmlLabel4_Click(object sender, EventArgs e) { }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { }
    }
}