namespace SDVE_SistemaVotacion
{
    partial class UcVotoRegistrado
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblFolioVotar = new Guna.UI2.WinForms.Guna2HtmlLabel();
            timerCuentaRegresiva = new System.Windows.Forms.Timer(components);
            guna2ProgressBar1 = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblSegundosRestantes = new Guna.UI2.WinForms.Guna2HtmlLabel();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(84, 121);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(111, 22);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Voto Registrado";
            // 
            // lblFolioVotar
            // 
            lblFolioVotar.BackColor = Color.Transparent;
            lblFolioVotar.Location = new Point(97, 197);
            lblFolioVotar.Name = "lblFolioVotar";
            lblFolioVotar.Size = new Size(3, 2);
            lblFolioVotar.TabIndex = 2;
            lblFolioVotar.Text = null;
            // 
            // timerCuentaRegresiva
            // 
            timerCuentaRegresiva.Tick += timerCuentaRegresiva_Tick;
            // 
            // guna2ProgressBar1
            // 
            guna2ProgressBar1.CustomizableEdges = customizableEdges3;
            guna2ProgressBar1.Location = new Point(84, 309);
            guna2ProgressBar1.Name = "guna2ProgressBar1";
            guna2ProgressBar1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2ProgressBar1.Size = new Size(679, 10);
            guna2ProgressBar1.TabIndex = 3;
            guna2ProgressBar1.Text = "guna2ProgressBar1";
            guna2ProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblSegundosRestantes
            // 
            lblSegundosRestantes.BackColor = Color.Transparent;
            lblSegundosRestantes.Location = new Point(84, 268);
            lblSegundosRestantes.Name = "lblSegundosRestantes";
            lblSegundosRestantes.Size = new Size(3, 2);
            lblSegundosRestantes.TabIndex = 4;
            // 
            // UcVotoRegistrado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblSegundosRestantes);
            Controls.Add(guna2ProgressBar1);
            Controls.Add(lblFolioVotar);
            Controls.Add(guna2HtmlLabel1);
            Name = "UcVotoRegistrado";
            Size = new Size(847, 400);
            Load += UcVotoRegistrado_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFolioVotar;
        private System.Windows.Forms.Timer timerCuentaRegresiva;
        private Guna.UI2.WinForms.Guna2ProgressBar guna2ProgressBar1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSegundosRestantes;
    }
}
