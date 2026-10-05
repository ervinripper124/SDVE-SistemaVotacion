namespace SDVE_SistemaVotacion
{
    partial class UcExportar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelExportar = new Guna.UI2.WinForms.Guna2Panel();
            lFechaExportar = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lAlumnosVotaron = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lFilasDeResultados = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnExportar = new Guna.UI2.WinForms.Guna2Button();
            lVotosRegistrados = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2DragControl3 = new Guna.UI2.WinForms.Guna2DragControl(components);
            lTituloExportar = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelExportar.SuspendLayout();
            SuspendLayout();
            // 
            // panelExportar
            // 
            panelExportar.BackColor = Color.FromArgb(40, 42, 54);
            panelExportar.Controls.Add(lFechaExportar);
            panelExportar.Controls.Add(lAlumnosVotaron);
            panelExportar.Controls.Add(lFilasDeResultados);
            panelExportar.Controls.Add(btnExportar);
            panelExportar.Controls.Add(lVotosRegistrados);
            panelExportar.CustomizableEdges = customizableEdges3;
            panelExportar.Dock = DockStyle.Fill;
            panelExportar.Location = new Point(0, 0);
            panelExportar.Name = "panelExportar";
            panelExportar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelExportar.Size = new Size(847, 400);
            panelExportar.TabIndex = 4;
            panelExportar.Paint += panelExportar_Paint;
            // 
            // lFechaExportar
            // 
            lFechaExportar.BackColor = Color.Transparent;
            lFechaExportar.ForeColor = Color.White;
            lFechaExportar.Location = new Point(62, 117);
            lFechaExportar.Margin = new Padding(2);
            lFechaExportar.Name = "lFechaExportar";
            lFechaExportar.Size = new Size(144, 22);
            lFechaExportar.TabIndex = 8;
            lFechaExportar.Text = "Fecha de Generación:";
            // 
            // lAlumnosVotaron
            // 
            lAlumnosVotaron.BackColor = Color.Transparent;
            lAlumnosVotaron.ForeColor = Color.White;
            lAlumnosVotaron.Location = new Point(62, 90);
            lAlumnosVotaron.Margin = new Padding(2);
            lAlumnosVotaron.Name = "lAlumnosVotaron";
            lAlumnosVotaron.Size = new Size(148, 22);
            lAlumnosVotaron.TabIndex = 7;
            lAlumnosVotaron.Text = "Alumnos que votaron:";
            // 
            // lFilasDeResultados
            // 
            lFilasDeResultados.BackColor = Color.Transparent;
            lFilasDeResultados.ForeColor = Color.White;
            lFilasDeResultados.Location = new Point(62, 64);
            lFilasDeResultados.Margin = new Padding(2);
            lFilasDeResultados.Name = "lFilasDeResultados";
            lFilasDeResultados.Size = new Size(132, 22);
            lFilasDeResultados.TabIndex = 6;
            lFilasDeResultados.Text = "Filas de Resultados:";
            // 
            // btnExportar
            // 
            btnExportar.CustomizableEdges = customizableEdges1;
            btnExportar.DisabledState.BorderColor = Color.DarkGray;
            btnExportar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExportar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExportar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExportar.Font = new Font("Segoe UI", 9F);
            btnExportar.ForeColor = Color.White;
            btnExportar.Location = new Point(103, 206);
            btnExportar.Margin = new Padding(2);
            btnExportar.Name = "btnExportar";
            btnExportar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnExportar.Size = new Size(216, 54);
            btnExportar.TabIndex = 5;
            btnExportar.Text = "Exportar Archivo";
            // 
            // lVotosRegistrados
            // 
            lVotosRegistrados.BackColor = Color.Transparent;
            lVotosRegistrados.ForeColor = Color.White;
            lVotosRegistrados.Location = new Point(62, 38);
            lVotosRegistrados.Margin = new Padding(2);
            lVotosRegistrados.Name = "lVotosRegistrados";
            lVotosRegistrados.Size = new Size(126, 22);
            lVotosRegistrados.TabIndex = 4;
            lVotosRegistrados.Text = "Votos Registrados:";
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2DragControl2
            // 
            guna2DragControl2.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl2.UseTransparentDrag = true;
            // 
            // guna2DragControl3
            // 
            guna2DragControl3.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl3.UseTransparentDrag = true;
            // 
            // lTituloExportar
            // 
            lTituloExportar.BackColor = Color.Transparent;
            lTituloExportar.ForeColor = Color.White;
            lTituloExportar.Location = new Point(329, 18);
            lTituloExportar.Margin = new Padding(2);
            lTituloExportar.Name = "lTituloExportar";
            lTituloExportar.Size = new Size(171, 22);
            lTituloExportar.TabIndex = 0;
            lTituloExportar.Text = "EXPORTAR DATOS A CSV";
            // 
            // UcExportar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelExportar);
            Margin = new Padding(2);
            Name = "UcExportar";
            Size = new Size(847, 400);
            panelExportar.ResumeLayout(false);
            panelExportar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelExportar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lVotosRegistrados;
        private Guna.UI2.WinForms.Guna2Button btnExportar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lFechaExportar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lAlumnosVotaron;
        private Guna.UI2.WinForms.Guna2HtmlLabel lFilasDeResultados;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl3;
        private Guna.UI2.WinForms.Guna2HtmlLabel lTituloExportar;
    }
}
