using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class UcBienvenida : UserControl
    {
        public UcBienvenida()
        {
            InitializeComponent();
        }

        private void btnComenzarVotacion_Click(object sender, EventArgs e)
        {

            Form1 ventanaPrincipal = this.ParentForm as Form1;

            if (ventanaPrincipal != null)
            {
                UcPapeleta pantallaPapeleta = new UcPapeleta();
                ventanaPrincipal.Ir(pantallaPapeleta);

            }
        }

        private void UcBienvenida_Load(object sender, EventArgs e)
        {
           lblTitulo.Font = HelperFuentes.BricolageBold(20f);
            lblTitulo.CentrarHorizontal(this);

        }
    }
}