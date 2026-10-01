namespace SDVE_SistemaVotacion
{
    partial class UcBienvenida
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnComenzarVotacion = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(291, 31);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(261, 22);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "Sistema Digital de Votación Estudiantil";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(234, 59);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(374, 22);
            guna2HtmlLabel2.TabIndex = 1;
            guna2HtmlLabel2.Text = "Selecciona comenzar para iniciar el proceso de elección";
            // 
            // btnComenzarVotacion
            // 
            btnComenzarVotacion.CustomizableEdges = customizableEdges1;
            btnComenzarVotacion.DisabledState.BorderColor = Color.DarkGray;
            btnComenzarVotacion.DisabledState.CustomBorderColor = Color.DarkGray;
            btnComenzarVotacion.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnComenzarVotacion.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnComenzarVotacion.Font = new Font("Segoe UI", 9F);
            btnComenzarVotacion.ForeColor = Color.White;
            btnComenzarVotacion.Location = new Point(317, 199);
            btnComenzarVotacion.Name = "btnComenzarVotacion";
            btnComenzarVotacion.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnComenzarVotacion.Size = new Size(225, 56);
            btnComenzarVotacion.TabIndex = 2;
            btnComenzarVotacion.Text = "Comenzar Votacion";
            btnComenzarVotacion.Click += btnComenzarVotacion_Click;
            // 
            // UcBienvenida
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnComenzarVotacion);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(guna2HtmlLabel1);
            Name = "UcBienvenida";
            Size = new Size(847, 400);
            Load += UcBienvenida_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Button btnComenzarVotacion;
    }
}
