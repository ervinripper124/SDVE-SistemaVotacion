namespace SDVE_SistemaVotacion
{
    partial class UcGraficas
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelGrafica = new Guna.UI2.WinForms.Guna2Panel();
            chartResultados = new System.Windows.Forms.DataVisualization.Charting.Chart();
            pnlExportar = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlGraficas = new Guna.UI2.WinForms.Guna2Panel();
            lGraficas = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlResultados = new Guna.UI2.WinForms.Guna2Panel();
            lResultados = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelGrafica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartResultados).BeginInit();
            pnlExportar.SuspendLayout();
            pnlGraficas.SuspendLayout();
            pnlResultados.SuspendLayout();
            SuspendLayout();
            // 
            // panelGrafica
            // 
            panelGrafica.BackColor = Color.FromArgb(40, 42, 54);
            panelGrafica.Controls.Add(chartResultados);
            panelGrafica.Controls.Add(pnlExportar);
            panelGrafica.Controls.Add(pnlGraficas);
            panelGrafica.Controls.Add(pnlResultados);
            panelGrafica.CustomizableEdges = customizableEdges7;
            panelGrafica.Dock = DockStyle.Fill;
            panelGrafica.Location = new Point(0, 0);
            panelGrafica.Margin = new Padding(4);
            panelGrafica.Name = "panelGrafica";
            panelGrafica.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelGrafica.Size = new Size(1059, 500);
            panelGrafica.TabIndex = 4;
            // 
            // chartResultados
            // 
            chartArea1.Name = "ChartArea1";
            chartResultados.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartResultados.Legends.Add(legend1);
            chartResultados.Location = new Point(319, 115);
            chartResultados.Name = "chartResultados";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartResultados.Series.Add(series1);
            chartResultados.Size = new Size(421, 324);
            chartResultados.TabIndex = 4;
            chartResultados.Text = "chart1";
            // 
            // pnlExportar
            // 
            pnlExportar.BackColor = Color.FromArgb(49, 50, 68);
            pnlExportar.Controls.Add(guna2HtmlLabel1);
            pnlExportar.CustomizableEdges = customizableEdges1;
            pnlExportar.Location = new Point(708, 0);
            pnlExportar.Name = "pnlExportar";
            pnlExportar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlExportar.Size = new Size(351, 62);
            pnlExportar.TabIndex = 2;
            pnlExportar.Click += pnlExportar_Click;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.ForeColor = Color.FromArgb(205, 214, 244);
            guna2HtmlLabel1.Location = new Point(121, 14);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(69, 27);
            guna2HtmlLabel1.TabIndex = 3;
            guna2HtmlLabel1.Text = "Exportar";
            // 
            // pnlGraficas
            // 
            pnlGraficas.BackColor = Color.FromArgb(49, 50, 68);
            pnlGraficas.Controls.Add(lGraficas);
            pnlGraficas.CustomizableEdges = customizableEdges3;
            pnlGraficas.Location = new Point(354, 0);
            pnlGraficas.Name = "pnlGraficas";
            pnlGraficas.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlGraficas.Size = new Size(351, 62);
            pnlGraficas.TabIndex = 1;
            // 
            // lGraficas
            // 
            lGraficas.BackColor = Color.Transparent;
            lGraficas.ForeColor = Color.FromArgb(205, 214, 244);
            lGraficas.Location = new Point(152, 14);
            lGraficas.Name = "lGraficas";
            lGraficas.Size = new Size(65, 27);
            lGraficas.TabIndex = 3;
            lGraficas.Text = "Graficas";
            // 
            // pnlResultados
            // 
            pnlResultados.BackColor = Color.FromArgb(49, 50, 68);
            pnlResultados.Controls.Add(lResultados);
            pnlResultados.CustomizableEdges = customizableEdges5;
            pnlResultados.Location = new Point(0, 0);
            pnlResultados.Name = "pnlResultados";
            pnlResultados.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlResultados.Size = new Size(351, 62);
            pnlResultados.TabIndex = 0;
            pnlResultados.Click += pnlResultados_Click;
            // 
            // lResultados
            // 
            lResultados.BackColor = Color.Transparent;
            lResultados.ForeColor = Color.FromArgb(205, 214, 244);
            lResultados.Location = new Point(121, 14);
            lResultados.Name = "lResultados";
            lResultados.Size = new Size(90, 27);
            lResultados.TabIndex = 3;
            lResultados.Text = "Resultados";
            // 
            // UcGraficas
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelGrafica);
            Name = "UcGraficas";
            Size = new Size(1059, 500);
            panelGrafica.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartResultados).EndInit();
            pnlExportar.ResumeLayout(false);
            pnlExportar.PerformLayout();
            pnlGraficas.ResumeLayout(false);
            pnlGraficas.PerformLayout();
            pnlResultados.ResumeLayout(false);
            pnlResultados.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelGrafica;
        private Guna.UI2.WinForms.Guna2Panel pnlExportar;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel pnlGraficas;
        private Guna.UI2.WinForms.Guna2HtmlLabel lGraficas;
        private Guna.UI2.WinForms.Guna2Panel pnlResultados;
        private Guna.UI2.WinForms.Guna2HtmlLabel lResultados;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResultados;
    }
}
