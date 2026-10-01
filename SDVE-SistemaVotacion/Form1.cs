using System;
using System.Drawing;
using System.Drawing.Text; //para poder usar PrivateFontCollection
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class Form1 : Form
    {
        // Declaramos la colección a nivel de clase para que Windows no la borre
        PrivateFontCollection pfc = new PrivateFontCollection();

        public Form1()
        {
            InitializeComponent();

            // Cargamos las fuentes segun los nombres de los archivos
            pfc.AddFontFile("fonts/BricolageGrotesque_24pt-Bold.ttf");
            pfc.AddFontFile("fonts/InstrumentSans-Regular.ttf");

            // Aplicamos Instrument Sans (posición 1) a TODA la ventana por defecto
            this.Font = new Font(pfc.Families[1], 10, FontStyle.Regular);

            lblTitulo.Location = new Point((pnlTop.Width - lblTitulo.Width) / 2, (pnlTop.Height - lblTitulo.Height) / 2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UcBienvenida pantallaBienvenida = new UcBienvenida();

            Ir(pantallaBienvenida);
        }
        public void Ir(UserControl u)
        {
            pnlMain.Controls.Clear();
            u.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(u);
        }
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}