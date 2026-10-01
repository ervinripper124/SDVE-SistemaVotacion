using System;
using System.Drawing;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class UcConfirmacion : UserControl
    {
        private UcPapeleta.EstadoDeSeleccion EstadoRecibido;

        public UcConfirmacion(UcPapeleta.EstadoDeSeleccion Estado)
        {
            InitializeComponent();

            EstadoRecibido = Estado;
            lblConfirmacionCandidatoSociedadA.Text = Estado.candidatoSociedad;
            lblConfirmacionCandidatoConsejoU.Text = Estado.candidatoConsejoU;
            lblConfirmacionCandidatoConsejoR.Text = Estado.candidatoConsejoR;
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {
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
        }

        private void btnCambiarSA_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                UcVotarSociedadDeAlumnos cambiarVoto = new UcVotarSociedadDeAlumnos(EstadoRecibido);
                ventanaPrincipal.Ir(cambiarVoto);
            }
        }

        private void btnCambiarCU_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                UcVotarConsejoUniversitario cambiarVoto = new UcVotarConsejoUniversitario(EstadoRecibido);
                ventanaPrincipal.Ir(cambiarVoto);
            }
        }

        private void btnCambiarCR_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                UcVotarConsejoDeRepresentantes cambiarVoto = new UcVotarConsejoDeRepresentantes(EstadoRecibido);
                ventanaPrincipal.Ir(cambiarVoto);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                UcVotoRegistrado votoRegistrado = new UcVotoRegistrado();
                ventanaPrincipal.Ir(votoRegistrado);
            }
        }

        private void mostrarPaneles(UcPapeleta.EstadoDeSeleccion Estado)
        {
            // Evaluamos si cada categoría tiene candidato seleccionado
            bool tieneSociedad = !string.IsNullOrWhiteSpace(Estado.candidatoSociedad);
            bool tieneCU = !string.IsNullOrWhiteSpace(Estado.candidatoConsejoU);
            bool tieneCR = !string.IsNullOrWhiteSpace(Estado.candidatoConsejoR);

            // 1. Sociedad de Alumnos
            pnlSociedad.Visible = tieneSociedad;
            pbSociedadVacio.Visible = !tieneSociedad;
            if (!tieneSociedad) pbSociedadVacio.BringToFront();

            // 2. Consejo Universitario
            pnlCU.Visible = tieneCU;
            pbCUVacio.Visible = !tieneCU;
            if (!tieneCU) pbCUVacio.BringToFront();

            // 3. Consejo de Representantes
            pnlCR.Visible = tieneCR;
            pbCRVacio.Visible = !tieneCR;
            if (!tieneCR) pbCRVacio.BringToFront();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}