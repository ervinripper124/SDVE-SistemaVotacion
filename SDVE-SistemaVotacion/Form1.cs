using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class Form1 : Form
    {
        public PrivateFontCollection pfc = new PrivateFontCollection();
        private Stack<UserControl> historial = new Stack<UserControl>();

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Models.Alumno? AlumnoActual { get; set; }

        private UcBienvenida? pantallaBienvenida;

        public Form1()
        {
            InitializeComponent();

            string rutaFonts = Path.Combine(Application.StartupPath, "fonts");
            string fontBricolage = Path.Combine(rutaFonts, "BricolageGrotesque_24pt-Bold.ttf");
            string fontInstrument = Path.Combine(rutaFonts, "InstrumentSans-Regular.ttf");

            if (File.Exists(fontBricolage)) pfc.AddFontFile(fontBricolage);
            if (File.Exists(fontInstrument)) pfc.AddFontFile(fontInstrument);

            if (pfc.Families.Length > 1)
            {
                this.Font = new Font(pfc.Families[1], 10, FontStyle.Regular);
            }

            pnlTop.BackColor = Color.FromArgb(10, 25, 49);
            pnlMain.BackColor = Color.FromArgb(244, 246, 249);
        }

        private void UcBienvenida_SolicitudMostrarResultados(object sender, EventArgs e)
        {
            Ir(new UcContenedor());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CentrarTituloHeader();

            try
            {
                int total = Services.AlmacenPadron.TotalElectores;
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidDataException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("No se pudo cargar el padrón de alumnos:\n\n" + ex.Message,
                                "Error en el padrón", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MostrarBienvenida();
        }

        public void MostrarBienvenida()
        {
            // 1. Limpiar datos del votante anterior y la pila
            AlumnoActual = null;
            historial.Clear();

            // 2. Crear una instancia NUEVA y mandarla a tu método Ir
            // Esto evita que los controles se muevan de lugar
            Ir(new UcBienvenida(), guardarEnHistorial: false);

            // 3. Asegurar el diseño del encabezado
            if (lblTitulo != null)
            {
                lblTitulo.Font = HelperFuentes.BricolageBold(12f);
                CentrarTituloHeader();
            }
        }

        public void Ir(UserControl u, bool guardarEnHistorial = true)
        {
            pnlMain.SuspendLayout();

            if (guardarEnHistorial && pnlMain.Controls.Count > 0)
            {
                historial.Push(pnlMain.Controls[0] as UserControl);
            }

            pnlMain.Controls.Clear();

            u.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(u);

            pnlTop.Visible = !(u is UcResultados || u is UcGraficas || u is UcExportar || u is UcContenedor);

            if (u is UcBienvenida ucBienvenida)
            {
                ucBienvenida.SolicitudMostrarResultados -= UcBienvenida_SolicitudMostrarResultados;
                ucBienvenida.SolicitudMostrarResultados += UcBienvenida_SolicitudMostrarResultados;
            }

            if (btnRegresar != null)
            {
                btnRegresar.Visible = !(u is UcBienvenida || u is UcVotoRegistrado) && (historial.Count > 0);
            }

            pnlMain.ResumeLayout();
        }

        private void CentrarTituloHeader()
        {
            if (lblTitulo != null && pnlTop != null)
            {
                // Uso del HelperUI mediante método de extensión
                lblTitulo.CentrarHorizontal(pnlTop);
                lblTitulo.Top = (pnlTop.Height - lblTitulo.Height) / 2;
            }
        }

        private void pnlTop_Resize(object sender, EventArgs e)
        {
            CentrarTituloHeader();
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e) { }
        private void pnlMain_Paint(object sender, PaintEventArgs e) { }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            if (pnlMain.Controls.Count > 0)
            {
                Control pantallaActual = pnlMain.Controls[0];

                if (pantallaActual is UcVotarSociedadDeAlumnos ||
                    pantallaActual is UcVotarConsejoUniversitario ||
                    pantallaActual is UcVotarConsejoDeRepresentantes)
                {
                    Ir(new UcPapeleta());
                }
                else if (pantallaActual is UcPapeleta || pantallaActual is UcConfirmacion || pantallaActual is UcIdentificacion)
                {
                    MostrarBienvenida();
                }
            }
        }
    }
}