using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class UcVotarConsejoDeRepresentantes : UserControl
    {
        private UcPapeleta.EstadoDeSeleccion EstadoRecibido;

        bool Candidato1 = false;
        bool Candidato2 = false;
        bool Candidato3 = false;
        bool Candidato4 = false;

        private int Seleccion = 0;
        private string OtroCandidato = "";

        public UcPapeleta.EstadoDeSeleccion EstadoActual => EstadoRecibido;


        public UcVotarConsejoDeRepresentantes(UcPapeleta.EstadoDeSeleccion Estado)
        {
            InitializeComponent();
            EstadoRecibido = Estado;
        }

        private void UcVotarConsejoDeRepresentantes_Load(object sender, EventArgs e)
        {
            lblConsejoDeRepresentantes.Font = HelperFuentes.BricolageBold(15f);
            lblCandidato1ConsejoR.Font = HelperFuentes.InstrumentRegular(12f);
            lblCandidato2ConsejoR.Font = HelperFuentes.InstrumentRegular(12f);
            lblCandidato3ConsejoR.Font = HelperFuentes.InstrumentRegular(12f);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (Seleccion == 0)
            {
                MessageBox.Show("Debe seleccionar un candidato antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Seleccion == 4)
            {
                if (string.IsNullOrWhiteSpace(txtOtro.Text))
                {
                    MessageBox.Show("Debe ingresar el nombre del candidato antes de continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    EstadoRecibido.candidatoConsejoR = txtOtro.Text;
                }

            }
            else
            {
                if (Seleccion == 1)
                {
                    EstadoRecibido.candidatoConsejoR = lblCandidato1ConsejoR.Text;
                }
                else if (Seleccion == 2)
                {
                    EstadoRecibido.candidatoConsejoR = lblCandidato2ConsejoR.Text;
                }
                else if (Seleccion == 3)
                {
                    EstadoRecibido.candidatoConsejoR = lblCandidato3ConsejoR.Text;
                }
            }
            EstadoRecibido.selConsejoR = false;
            Form1 ventanaPrincipal = this.ParentForm as Form1;

            if (ventanaPrincipal != null)
            {
                UcConfirmacion siguiente = new UcConfirmacion(EstadoRecibido);
                ventanaPrincipal.Ir(siguiente);

            }
        }


        private void pnlCandidato1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlCandidato1_Click(object sender, EventArgs e)
        {
            SeleccionCandidato(1);
            Seleccion = 1;
        }

        private void pnlCandidato2_Click(object sender, EventArgs e)
        {
            SeleccionCandidato(2);
            Seleccion = 2;
        }
        private void pnlCandidato3_Click(object sender, EventArgs e)
        {
            SeleccionCandidato(3);
            Seleccion = 3;
        }
        private void pnlOtro_Click_1(object sender, EventArgs e)
        {
            SeleccionCandidato(4);
            Seleccion = 4;
        }
        private void txtOtro_Click(object sender, EventArgs e)
        {
            SeleccionCandidato(4);
            Seleccion = 4;
        }


        private void SeleccionCandidato(int numpanel)
        {
            if (numpanel == 1)
            {
                // Candidato 1 - SELECCIONADO
                Candidato1 = true;
                pnlCandidato1.FillColor = Color.FromArgb(10, 25, 49);        // Azul UAA
                circulo1.FillColor = Color.FromArgb(230, 81, 0);            // Naranja Flama
                lblCandidato1ConsejoR.ForeColor = Color.White;

                // Candidato 2 - Deseleccionado
                Candidato2 = false;
                pnlCandidato2.FillColor = Color.White;
                circulo2.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato2ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Candidato 3 - Deseleccionado
                Candidato3 = false;
                pnlCandidato3.FillColor = Color.White;
                circulo3.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato3ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Otro / Candidato 4 - Deseleccionado
                Candidato4 = false;
                pnlOtro.FillColor = Color.White;
                circulo4.FillColor = Color.FromArgb(224, 224, 224);
                txtOtro.Text = OtroCandidato;
            }
            else if (numpanel == 2)
            {
                // Candidato 1 - Deseleccionado
                Candidato1 = false;
                pnlCandidato1.FillColor = Color.White;
                circulo1.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato1ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Candidato 2 - SELECCIONADO
                Candidato2 = true;
                pnlCandidato2.FillColor = Color.FromArgb(10, 25, 49);        // Azul UAA
                circulo2.FillColor = Color.FromArgb(230, 81, 0);            // Naranja Flama
                lblCandidato2ConsejoR.ForeColor = Color.White;

                // Candidato 3 - Deseleccionado
                Candidato3 = false;
                pnlCandidato3.FillColor = Color.White;
                circulo3.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato3ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Otro / Candidato 4 - Deseleccionado
                Candidato4 = false;
                pnlOtro.FillColor = Color.White;
                circulo4.FillColor = Color.FromArgb(224, 224, 224);
               

                txtOtro.Text = OtroCandidato;
            }
            else if (numpanel == 3)
            {
                // Candidato 1 - Deseleccionado
                Candidato1 = false;
                pnlCandidato1.FillColor = Color.White;
                circulo1.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato1ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Candidato 2 - Deseleccionado
                Candidato2 = false;
                pnlCandidato2.FillColor = Color.White;
                circulo2.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato2ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Candidato 3 - SELECCIONADO
                Candidato3 = true;
                pnlCandidato3.FillColor = Color.FromArgb(10, 25, 49);        // Azul UAA
                circulo3.FillColor = Color.FromArgb(230, 81, 0);            // Naranja Flama
                lblCandidato3ConsejoR.ForeColor = Color.White;

                // Otro / Candidato 4 - Deseleccionado
                Candidato4 = false;
                pnlOtro.FillColor = Color.White;
                circulo4.FillColor = Color.FromArgb(224, 224, 224);
               

                txtOtro.Text = OtroCandidato;
            }
            else if (numpanel == 4)
            {
                // Candidato 1 - Deseleccionado
                Candidato1 = false;
                pnlCandidato1.FillColor = Color.White;
                circulo1.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato1ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Candidato 2 - Deseleccionado
                Candidato2 = false;
                pnlCandidato2.FillColor = Color.White;
                circulo2.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato2ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Candidato 3 - Deseleccionado
                Candidato3 = false;
                pnlCandidato3.FillColor = Color.White;
                circulo3.FillColor = Color.FromArgb(224, 224, 224);
                lblCandidato3ConsejoR.ForeColor = Color.FromArgb(10, 25, 49);

                // Otro / Candidato 4 - SELECCIONADO
                Candidato4 = true;
                pnlOtro.FillColor = Color.FromArgb(10, 25, 49);             // Azul UAA
                circulo4.FillColor = Color.FromArgb(230, 81, 0);         // Naranja Flama
                
            }

        }

        private void lblConsejoDeRepresentantes_Click(object sender, EventArgs e)
        {

        }
    }
}
