using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class Form1 : Form
    {
        // Colección de fuentes a nivel de clase
        public PrivateFontCollection pfc = new PrivateFontCollection();

        private Stack<UserControl> historial = new Stack<UserControl>();

        public Form1()
        {
            InitializeComponent();

            // 1. Carga de fuentes segura desde la ruta de ejecución
            string rutaFonts = Path.Combine(Application.StartupPath, "fonts");
            string fontBricolage = Path.Combine(rutaFonts, "BricolageGrotesque_24pt-Bold.ttf");
            string fontInstrument = Path.Combine(rutaFonts, "InstrumentSans-Regular.ttf");

            if (File.Exists(fontBricolage)) pfc.AddFontFile(fontBricolage);
            if (File.Exists(fontInstrument)) pfc.AddFontFile(fontInstrument);

            // Aplicar Instrument Sans por defecto
            if (pfc.Families.Length > 1)
            {
                this.Font = new Font(pfc.Families[1], 10, FontStyle.Regular);
            }

            // 2. Colores institucionales UAA
            pnlTop.BackColor = Color.FromArgb(10, 25, 49);     // Azul UAA (#0A1931)
            pnlMain.BackColor = Color.FromArgb(244, 246, 249);  // Gris Fondo (#F4F6F9)
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CentrarTituloHeader();

            // Cargar la pantalla inicial
            UcBienvenida pantallaBienvenida = new UcBienvenida();
            Ir(pantallaBienvenida);

            lblTitulo.Font = HelperFuentes.BricolageBold(12f);
            lblTitulo.CentrarHorizontal(this);
        }

        public void Ir(UserControl u, bool guardarEnHistorial = true)
        {
            pnlMain.SuspendLayout();

            // 1. Guardar la pantalla actual en el historial si la regla lo permite
            if (guardarEnHistorial && pnlMain.Controls.Count > 0)
            {
                historial.Push(pnlMain.Controls[0] as UserControl);
            }

            // 2. Quitar la pantalla actual del panel (SIN c.Dispose() para mantener los datos en memoria)
            pnlMain.Controls.Clear();

            // 3. Cargar la nueva pantalla
            u.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(u);

            // 4. Visibilidad del botón Regresar
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
                lblTitulo.Location = new Point(
                    (pnlTop.Width - lblTitulo.Width) / 2,
                    (pnlTop.Height - lblTitulo.Height) / 2
                );
            }
        }

        private void pnlTop_Resize(object sender, EventArgs e)
        {
            CentrarTituloHeader();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e) { }
        private void pnlTop_Paint(object sender, PaintEventArgs e) { }
        private void pnlMain_Paint(object sender, PaintEventArgs e) { }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Verificamos que haya alguna pantalla cargada dentro del panel principal
            if (pnlMain.Controls.Count > 0)
            {
                Control pantallaActual = pnlMain.Controls[0];

                // 1. Si está en cualquiera de los paneles de votación individual -> Regresa a la Papeleta
                if (pantallaActual is UcVotarSociedadDeAlumnos ||
                    pantallaActual is UcVotarConsejoUniversitario ||
                    pantallaActual is UcVotarConsejoDeRepresentantes)
                {
                    // Necesitas pasar el estado actual si tu papeleta lo requiere
                    Ir(new UcPapeleta());
                }
                // 2. Si está en la Papeleta o en Confirmación -> Regresa a la Bienvenida
                else if (pantallaActual is UcPapeleta || pantallaActual is UcConfirmacion)
                {
                    Ir(new UcBienvenida());
                }
            }
        }
    }
}