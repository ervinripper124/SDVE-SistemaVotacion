using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class UcVotarConsejoUniversitario : UserControl
    {
        private UcPapeleta.EstadoDeSeleccion EstadoRecibido;

        bool Candidato1 = false;
        bool Candidato2 = false;
        bool Candidato3 = false;
        bool Candidato4 = false;

        private int Seleccion = 0;
        private string OtroCandidato = "";


        public UcVotarConsejoUniversitario(UcPapeleta.EstadoDeSeleccion Estado)
        {
            InitializeComponent();
            EstadoRecibido = Estado;
        }

        private void UcVotarConsejoUniversitario_Load(object sender, EventArgs e)
        {

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
                    EstadoRecibido.candidatoConsejoU = txtOtro.Text;
                }
            } 
            else
            {
                if (Seleccion == 1) EstadoRecibido.candidatoConsejoU = lblCandidato1ConsejoU.Text;
                else if (Seleccion == 2) EstadoRecibido.candidatoConsejoU = lblCandidato2ConsejoU.Text;
                else if (Seleccion == 3) EstadoRecibido.candidatoConsejoU = lblCandidato3ConsejoU.Text;
            }
            EstadoRecibido.selConsejoU = false;
            Form1 ventanaPrincipal = this.ParentForm as Form1;

            if (ventanaPrincipal != null)
            {

                if (EstadoRecibido.selConsejoR)
                {
                    UcVotarConsejoDeRepresentantes siguiente = new UcVotarConsejoDeRepresentantes(EstadoRecibido);
                    ventanaPrincipal.Ir(siguiente);
                }
                else
                {
                    UcConfirmacion siguiente = new UcConfirmacion(EstadoRecibido);
                    ventanaPrincipal.Ir(siguiente);
                }
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
                Candidato1 = true;
                pnlCandidato1.FillColor = Color.FromArgb(94, 148, 255);
                Candidato2 = false;
                pnlCandidato2.FillColor = Color.FromArgb(245, 245, 245);
                Candidato3 = false;
                pnlCandidato3.FillColor = Color.FromArgb(245, 245, 245);
                Candidato4 = false;
                pnlOtro.FillColor = Color.FromArgb(245, 245, 245);
                txtOtro.Text = OtroCandidato;
            }
            else if (numpanel == 2)
            {
                Candidato2 = true;
                pnlCandidato2.FillColor = Color.FromArgb(94, 148, 255);
                Candidato1 = false;
                pnlCandidato1.FillColor = Color.FromArgb(245, 245, 245);
                Candidato3 = false;
                pnlCandidato3.FillColor = Color.FromArgb(245, 245, 245);
                Candidato4 = false;
                pnlOtro.FillColor = Color.FromArgb(245, 245, 245);
                txtOtro.Text = OtroCandidato;
            }
            else if (numpanel == 3)
            {
                Candidato3 = true;
                pnlCandidato3.FillColor = Color.FromArgb(94, 148, 255);
                Candidato1 = false;
                pnlCandidato1.FillColor = Color.FromArgb(245, 245, 245);
                Candidato2 = false;
                pnlCandidato2.FillColor = Color.FromArgb(245, 245, 245);
                Candidato4 = false;
                pnlOtro.FillColor = Color.FromArgb(245, 245, 245);
                txtOtro.Text = OtroCandidato;
            }
            else if (numpanel == 4)
            {
                Candidato4 = true;
                pnlOtro.FillColor = Color.FromArgb(94, 148, 255);
                Candidato1 = false;
                pnlCandidato1.FillColor = Color.FromArgb(245, 245, 245);
                Candidato2 = false;
                pnlCandidato2.FillColor = Color.FromArgb(245, 245, 245);
                Candidato3 = false;
                pnlCandidato3.FillColor = Color.FromArgb(245, 245, 245);
            }

        }

    }
}
