using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class UcVotoRegistrado : UserControl
    {
        // 50 ticks de 100 ms = 5 segundos de conteo fluido
        private int totalTicks = 50;
        private int tickActual = 50;

        public UcVotoRegistrado()
        {
            InitializeComponent();
        }

        private readonly string folio;

        public UcVotoRegistrado(string folio)
        {
            InitializeComponent();
            this.folio = folio;
        }

        private void UcVotoRegistrado_Load(object sender, EventArgs e)
        {
            // 1. Título de Confirmación
            guna2HtmlLabel1.Text = "¡Voto Registrado con Éxito!";
            guna2HtmlLabel1.Font = HelperFuentes.BricolageBold(18f);
            guna2HtmlLabel1.ForeColor = Color.FromArgb(10, 25, 49); // Azul UAA

            // 2. Folio real (el mismo que se guardó con los votos)
            lblFolioVotar.Text = $"¡Gracias por votar!\nTu folio es: {folio}";
            lblFolioVotar.Font = HelperFuentes.BricolageBold(12f);
            lblFolioVotar.ForeColor = Color.FromArgb(230, 81, 0); // Naranja Flama UAA

            // 3. Cuenta regresiva
            lblSegundosRestantes.Font = HelperFuentes.InstrumentRegular(11f);
            lblSegundosRestantes.ForeColor = Color.FromArgb(107, 114, 128); // Gris tenue

            // 4. Estilo de la Barra de Progreso UAA
            guna2ProgressBar1.ProgressColor = Color.FromArgb(230, 81, 0); // Naranja Flama
            guna2ProgressBar1.ProgressColor2 = Color.DarkOrange;
            guna2ProgressBar1.Maximum = totalTicks;
            guna2ProgressBar1.Value = totalTicks;

            // 5. Iniciar Timer (100 ms por tick)
            timerCuentaRegresiva.Interval = 100;
            tickActual = totalTicks;
            timerCuentaRegresiva.Start();
        }

        private void timerCuentaRegresiva_Tick(object sender, EventArgs e)
        {
            tickActual--;

            if (tickActual >= 0)
            {
                guna2ProgressBar1.Value = tickActual;

                int segundos = (int)Math.Ceiling(tickActual / 10.0);
                lblSegundosRestantes.Text = $"Volviendo al inicio en {segundos} segundo{(segundos == 1 ? "" : "s")}...";
            }
            else
            {
                timerCuentaRegresiva.Stop();

                Form1 ventanaPrincipal = this.ParentForm as Form1;
                if (ventanaPrincipal != null)
                {
                    ventanaPrincipal.MostrarBienvenida();
                }
            }
        }
    }
}