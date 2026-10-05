using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace SDVE_SistemaVotacion
{
    public partial class UcBienvenida : UserControl
    {
        public event EventHandler SolicitudMostrarResultados;
        private bool _inicializado = false;

        public UcBienvenida()
        {
            InitializeComponent();

            // 1. Asignar las fuentes una sola vez en el constructor para evitar
            // que se recalcule el tamaño de las fuentes y las etiquetas múltiples veces al navegar.
            lblTitulo.Font = HelperFuentes.BricolageBold(22f);
            lbSubtitulo.Font = HelperFuentes.InstrumentRegular(12f);
            btnComenzarVotacion.Font = HelperFuentes.BricolageBold(14f);
            btnResultados.Font = HelperFuentes.BricolageBold(9f);

            btnResultados.Enabled = true;

            // 2. Controlar el Resize de forma segura (solo cuando ya se mostró por primera vez)
            this.Resize += (s, e) =>
            {
                if (_inicializado)
                {
                    AcomodarElementos();
                }
            };
        }

        private void UcBienvenida_Load(object sender, EventArgs e)
        {
            AcomodarElementos();
            _inicializado = true;
        }

        private void AcomodarElementos()
        {
            if (this.Width <= 0 || this.Height <= 0) return;

            // Asegurar anclajes neutrales para el centrado manual
            lblTitulo.Anchor = AnchorStyles.None;
            lbSubtitulo.Anchor = AnchorStyles.None;
            btnComenzarVotacion.Anchor = AnchorStyles.None;
            btnResultados.Anchor = AnchorStyles.None;

            // Centrar horizontalmente usando el helper
            HelperUI.CentrarVariosHorizontal(this, lblTitulo, lbSubtitulo, btnComenzarVotacion);
        }

        private void btnComenzarVotacion_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                ventanaPrincipal.Ir(new UcIdentificacion());
            }
        }

        private void btnResultados_Click(object sender, EventArgs e)
        {
            SolicitudMostrarResultados?.Invoke(this, EventArgs.Empty);
        }

        private void btnResultados_Click_1(object sender, EventArgs e)
        {
            SolicitudMostrarResultados?.Invoke(this, EventArgs.Empty);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}