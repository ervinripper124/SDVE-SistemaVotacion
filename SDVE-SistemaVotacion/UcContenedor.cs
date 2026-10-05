using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public partial class UcContenedor : UserControl
    {
        public UcContenedor()
        {
            InitializeComponent();

            abrirModulo(new UcResultados());
            
            btnExportar.Font = HelperFuentes.BricolageBold(9f);
            btnGraficas.Font = HelperFuentes.BricolageBold(9f);
            btnResultados.Font = HelperFuentes.BricolageBold(9f);

        }

        private void abrirModulo(UserControl uc)
        {
            pnlWork.SuspendLayout();
            pnlWork.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlWork.Controls.Add(uc);
            pnlWork.ResumeLayout();
        }

        private void btnResultados_Click(object sender, EventArgs e)
        {
            abrirModulo(new UcResultados());
        }

        private void btnGraficas_Click(object sender, EventArgs e)
        {
            abrirModulo(new UcGraficas());
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            abrirModulo(new UcExportar());
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                UcBienvenida regresar = new UcBienvenida();
                ventanaPrincipal.Ir(regresar);
            }
        }

        private void pnlTopContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
