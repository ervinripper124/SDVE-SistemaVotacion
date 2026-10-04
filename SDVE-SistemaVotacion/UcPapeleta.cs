using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SDVE_SistemaVotacion.Models;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public partial class UcPapeleta : UserControl
    {
        
        public struct EstadoDeSeleccion
        {
            public bool selSociedad;
            public bool selConsejoU;
            public bool selConsejoR;

            public string candidatoSociedad;
            public string candidatoConsejoU;
            public string candidatoConsejoR;

            public bool otroSociedad;
            public bool otroConsejoU;
            public bool otroConsejoR;

            public List<SeleccionVoto> ComoSelecciones()
            {
                var lista = new List<SeleccionVoto>();
                if (!string.IsNullOrWhiteSpace(candidatoSociedad))
                    lista.Add(new SeleccionVoto(TipoConvocatoria.SociedadDeAlumnos, candidatoSociedad, otroSociedad));
                if (!string.IsNullOrWhiteSpace(candidatoConsejoU))
                    lista.Add(new SeleccionVoto(TipoConvocatoria.ConsejoUniversitario, candidatoConsejoU, otroConsejoU));
                if (!string.IsNullOrWhiteSpace(candidatoConsejoR))
                    lista.Add(new SeleccionVoto(TipoConvocatoria.ConsejoDeRepresentantes, candidatoConsejoR, otroConsejoR));
                return lista;
            }
        }

        bool selSociedad = false;
        bool selConsejoU = false;
        bool selConsejoR = false;

        public int TotalSelecciones = 0;
        public UcPapeleta()
        {
            InitializeComponent();
        }
        public UcPapeleta(EstadoDeSeleccion estadoRecibido) : this()
        {
           

            // Sincronizamos las variables booleanas internas con el estado recibido
            this.selSociedad = !string.IsNullOrEmpty(estadoRecibido.candidatoSociedad);
            this.selConsejoU = !string.IsNullOrEmpty(estadoRecibido.candidatoConsejoU);
            this.selConsejoR = !string.IsNullOrEmpty(estadoRecibido.candidatoConsejoR);
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UcPapeleta_Load(object sender, EventArgs e)
        {
            lblTitulo.Font = HelperFuentes.BricolageBold(12f);
            Alumno? alumno = (this.ParentForm as Form1)?.AlumnoActual;
            if (alumno == null) return;

            if (alumno.HaVotadoEn(TipoConvocatoria.SociedadDeAlumnos))
                Bloquear(pnlSelSociedadDeAlumnos, guna2HtmlLabel1);
            if (alumno.HaVotadoEn(TipoConvocatoria.ConsejoUniversitario))
                Bloquear(pnlConsejoUniversitario, guna2HtmlLabel2);
            if (alumno.HaVotadoEn(TipoConvocatoria.ConsejoDeRepresentantes))
                Bloquear(pnlConsejoDeRepresentantes, guna2HtmlLabel3);
        }

        private static void Bloquear(Guna.UI2.WinForms.Guna2ShadowPanel panel, Guna.UI2.WinForms.Guna2HtmlLabel titulo)
        {
            panel.FillColor = Color.FromArgb(225, 225, 225);
            panel.Enabled = false;
            titulo.ForeColor = Color.Gray;
            titulo.Text += "  (ya votaste)";
        }

        private void actualizarSeleccion()
        {
            int contador = 0;

            if (selSociedad == true) { contador++; }
            if (selConsejoU == true) { contador++; }
            if (selConsejoR == true) { contador++; }

            TotalSelecciones = contador;

            if (contador == 0)
            {
                lblIzquierda.Text = "Aun no ha seleccionado ninguna opción";
            }
            else if (contador == 1)
            {
                lblIzquierda.Text = "1 de 3 seleccionado";
            }
            else if (contador == 2)
            {
                lblIzquierda.Text = "2 de 3 seleccionado";
            }
            else if (contador == 3)
            {
                lblIzquierda.Text = "3 de 3 seleccionado";

            }
        }

        private void pnlSelSociedadDeAlumnos_Click(object sender, EventArgs e)
        {
            selSociedad = !selSociedad;

            if (selSociedad == true)
            {
                pnlSelSociedadDeAlumnos.FillColor = Color.FromArgb(10, 25, 49);
                guna2HtmlLabel1.ForeColor = Color.White;
                circulo1.FillColor = Color.FromArgb(230, 81, 0);
            }
            else
            {
                pnlSelSociedadDeAlumnos.FillColor = Color.FromArgb(245, 245, 245);
                guna2HtmlLabel1.ForeColor = Color.FromArgb(10, 25, 49);
                circulo1.FillColor = Color.FromArgb(224, 224, 224);

            }
            actualizarSeleccion();
        }

        private void pnlConsejoUniversitario_Click(object sender, EventArgs e)
        {
            selConsejoU = !selConsejoU;
            if (selConsejoU == true)
            {
                pnlConsejoUniversitario.FillColor = Color.FromArgb(10, 25, 49);
                guna2HtmlLabel2.ForeColor = Color.White;
                circulo2.FillColor = Color.FromArgb(230, 81, 0);
            }
            else
            {
                pnlConsejoUniversitario.FillColor = Color.FromArgb(245, 245, 245);
                guna2HtmlLabel2.ForeColor = Color.FromArgb(10, 25, 49);
                circulo2.FillColor = Color.FromArgb(224, 224, 224);
            }
            actualizarSeleccion();
        }



        private void pnlConsejoDeRepresentantes_Click(object sender, EventArgs e)
        {
            selConsejoR = !selConsejoR;
            if (selConsejoR == true)
            {
                pnlConsejoDeRepresentantes.FillColor = Color.FromArgb(10, 25, 49);
                guna2HtmlLabel3.ForeColor = Color.White;
                circulo3.FillColor = Color.FromArgb(230, 81, 0);
            }
            else
            {
                pnlConsejoDeRepresentantes.FillColor = Color.FromArgb(245, 245, 245);
                guna2HtmlLabel3.ForeColor = Color.FromArgb(10, 25, 49); 
                circulo3.FillColor = Color.FromArgb(224, 224, 224);
            }
            actualizarSeleccion();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (TotalSelecciones == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una opción para continuar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Form1 ventanaPrincipal = this.ParentForm as Form1;

                if (ventanaPrincipal != null)
                {
                    EstadoDeSeleccion Estado = new EstadoDeSeleccion();
                    {
                        Estado.selSociedad = selSociedad;
                        Estado.selConsejoU = selConsejoU;
                        Estado.selConsejoR = selConsejoR;

                        if (Estado.selSociedad)
                        {
                            UcVotarSociedadDeAlumnos votarSociedad = new UcVotarSociedadDeAlumnos(Estado);
                            ventanaPrincipal.Ir(votarSociedad);
                        }
                        else if (Estado.selConsejoU)
                        {
                            UcVotarConsejoUniversitario votarConsejoU = new UcVotarConsejoUniversitario(Estado);
                            ventanaPrincipal.Ir(votarConsejoU);
                        }
                        else if (Estado.selConsejoR)
                        {
                            UcVotarConsejoDeRepresentantes votarConsejoR = new UcVotarConsejoDeRepresentantes(Estado);
                            ventanaPrincipal.Ir(votarConsejoR);

                        }

                    }
                }


            }
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

