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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelGrafica = new Guna.UI2.WinForms.Guna2Panel();
            chartResultados = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panelGrafica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartResultados).BeginInit();
            SuspendLayout();
            // 
            // panelGrafica
            // 
            panelGrafica.BackColor = Color.FromArgb(40, 42, 54);
            panelGrafica.Controls.Add(chartResultados);
            panelGrafica.CustomizableEdges = customizableEdges1;
            panelGrafica.Dock = DockStyle.Fill;
            panelGrafica.Location = new Point(0, 0);
            panelGrafica.Name = "panelGrafica";
            panelGrafica.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelGrafica.Size = new Size(847, 400);
            panelGrafica.TabIndex = 4;
            // 
            // chartResultados
            // 
            chartArea1.Name = "ChartArea1";
            chartResultados.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartResultados.Legends.Add(legend1);
            chartResultados.Location = new Point(264, 60);
            chartResultados.Margin = new Padding(2, 2, 2, 2);
            chartResultados.Name = "chartResultados";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartResultados.Series.Add(series1);
            chartResultados.Size = new Size(337, 259);
            chartResultados.TabIndex = 4;
            chartResultados.Text = "chart1";
            // 
            // UcGraficas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelGrafica);
            Margin = new Padding(2, 2, 2, 2);
            Name = "UcGraficas";
            Size = new Size(847, 400);
            panelGrafica.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartResultados).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelGrafica;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResultados;
    }
}
