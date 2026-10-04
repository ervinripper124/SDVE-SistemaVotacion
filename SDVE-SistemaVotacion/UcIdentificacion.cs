using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public class UcIdentificacion : UserControl
    {
        private const int Ancho = 460;

        private readonly Panel contenido = new Panel();
        private readonly Guna2HtmlLabel lblTitulo = new Guna2HtmlLabel();
        private readonly Guna2HtmlLabel lblSub = new Guna2HtmlLabel();
        private readonly Guna2TextBox txtMatricula = new Guna2TextBox();
        private readonly Guna2HtmlLabel lblError = new Guna2HtmlLabel();
        private readonly Guna2Button btnContinuar = new Guna2Button();
        private readonly Guna2Button btnVolver = new Guna2Button();

        public UcIdentificacion()
        {
            BackColor = Color.White;
            contenido.Size = new Size(Ancho, 330);
            contenido.BackColor = Color.Transparent;

            lblTitulo.AutoSize = false;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Text = "Identifícate para votar";
            lblTitulo.TextAlignment = ContentAlignment.MiddleCenter;
            lblTitulo.Size = new Size(Ancho, 46);
            lblTitulo.Location = new Point(0, 0);

            lblSub.AutoSize = false;
            lblSub.BackColor = Color.Transparent;
            lblSub.ForeColor = Color.DimGray;
            lblSub.Text = "Escribe tu matrícula para comenzar.";
            lblSub.TextAlignment = ContentAlignment.MiddleCenter;
            lblSub.Size = new Size(Ancho, 30);
            lblSub.Location = new Point(0, 52);

            txtMatricula.Size = new Size(Ancho, 54);
            txtMatricula.Location = new Point(0, 100);
            txtMatricula.PlaceholderText = "Matrícula";
            txtMatricula.MaxLength = 30;
            txtMatricula.BorderRadius = 8;
            txtMatricula.TextAlign = HorizontalAlignment.Center;
            txtMatricula.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMatricula.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtMatricula.TextChanged += (s, e) => lblError.Text = string.Empty;

            lblError.AutoSize = false;
            lblError.BackColor = Color.Transparent;
            lblError.ForeColor = Color.FromArgb(200, 50, 50);
            lblError.Text = string.Empty;
            lblError.TextAlignment = ContentAlignment.MiddleCenter;
            lblError.Size = new Size(Ancho, 30);
            lblError.Location = new Point(0, 162);

            btnContinuar.Size = new Size(Ancho, 58);
            btnContinuar.Location = new Point(0, 200);
            btnContinuar.Text = "Continuar";
            btnContinuar.ForeColor = Color.White;
            btnContinuar.FillColor = Color.FromArgb(94, 148, 255);
            btnContinuar.BorderRadius = 8;
            btnContinuar.Cursor = Cursors.Hand;
            btnContinuar.Click += (s, e) => Continuar();

            btnVolver.Size = new Size(Ancho, 46);
            btnVolver.Location = new Point(0, 270);
            btnVolver.Text = "Volver";
            btnVolver.ForeColor = Color.FromArgb(60, 60, 60);
            btnVolver.FillColor = Color.FromArgb(245, 245, 245);
            btnVolver.BorderRadius = 8;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.Click += (s, e) => Volver();

            contenido.Controls.Add(lblTitulo);
            contenido.Controls.Add(lblSub);
            contenido.Controls.Add(txtMatricula);
            contenido.Controls.Add(lblError);
            contenido.Controls.Add(btnContinuar);
            contenido.Controls.Add(btnVolver);
            Controls.Add(contenido);

            Resize += (s, e) => Centrar();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblTitulo.Font = new Font(Font.FontFamily, 22f, FontStyle.Bold);
            lblSub.Font = new Font(Font.FontFamily, 11f);
            lblError.Font = new Font(Font.FontFamily, 10f);
            txtMatricula.Font = new Font(Font.FontFamily, 15f);
            btnContinuar.Font = new Font(Font.FontFamily, 12f, FontStyle.Bold);
            btnVolver.Font = new Font(Font.FontFamily, 11f);
            Centrar();
            txtMatricula.Focus();
        }

        /// <summary>Enter dentro del campo de matrícula equivale a pulsar "Continuar".</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtMatricula.ContainsFocus)
            {
                Continuar();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Centrar()
        {
            contenido.Location = new Point(
                Math.Max(0, (Width - contenido.Width) / 2),
                Math.Max(0, (Height - contenido.Height) / 2));
        }

        private void Volver()
        {
            Form1? ventana = FindForm() as Form1;
            if (ventana != null) ventana.MostrarBienvenida();
        }

        private void Continuar()
        {
            Form1? ventana = FindForm() as Form1;
            if (ventana == null) return;

            string matricula = txtMatricula.Text.Trim();
            if (matricula.Length == 0)
            {
                lblError.Text = "Escribe tu matrícula.";
                return;
            }

            Models.Alumno? alumno;
            try
            {
                alumno = AlmacenPadron.Identificar(matricula);
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidDataException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("No se pudo leer el padrón de alumnos: " + ex.Message, "Error en el padrón",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (alumno == null)
            {
                lblError.Text = "Esa matrícula no está en el padrón.";
                return;
            }

            if (AlmacenPadron.Disponibles(alumno).Count == 0)
            {
                lblError.Text = "Ya votaste en todas las convocatorias.";
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eres " + alumno.Nombre + "?", "Confirmar identidad",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            ventana.AlumnoActual = alumno;
            ventana.Ir(new UcPapeleta());
        }
    }
}
