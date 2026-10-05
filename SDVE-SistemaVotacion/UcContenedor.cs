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

            // Tipografías
            btnExportar.Font = HelperFuentes.BricolageBold(9f);
            btnGraficas.Font = HelperFuentes.BricolageBold(9f);
            btnResultados.Font = HelperFuentes.BricolageBold(9f);

            // El indicador siempre va detrás de los botones
            pnlIndicador.SendToBack();

            // Abrimos el primer módulo
            abrirModulo(new UcResultados());
            AjustarIndicador(btnResultados);
        }

        private void abrirModulo(UserControl uc)
        {
            pnlWork.SuspendLayout();
            pnlWork.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlWork.Controls.Add(uc);
            pnlWork.ResumeLayout();
        }

        private void AjustarIndicador(Control botonActivo)
        {
            const int margen = 8;          // espacio a cada lado del botón
            const int topIndicador = 4;    // separación desde arriba

            pnlIndicador.Left = botonActivo.Left - margen;
            pnlIndicador.Width = botonActivo.Width + margen * 2;
            pnlIndicador.Top = topIndicador;

            // Llega hasta el borde inferior de la barra; el extra (Radius + 2)
            // oculta las esquinas redondeadas de abajo porque el padre las recorta
            pnlIndicador.Height = pnlTopContenedor.Height - topIndicador + pnlIndicador.Radius + 2;
        }

        private void btnResultados_Click(object sender, EventArgs e)
        {
            abrirModulo(new UcResultados());
            AjustarIndicador(btnResultados);
        }

        private void btnGraficas_Click(object sender, EventArgs e)
        {
            abrirModulo(new UcGraficas());
            AjustarIndicador(btnGraficas);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            abrirModulo(new UcExportar());
            AjustarIndicador(btnExportar);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form1 ventanaPrincipal = this.ParentForm as Form1;
            if (ventanaPrincipal != null)
            {
                ventanaPrincipal.Ir(new UcBienvenida());
            }
        }
    }
}