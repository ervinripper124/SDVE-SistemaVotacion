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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcBienvenida));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbSubtitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnComenzarVotacion = new Guna.UI2.WinForms.Guna2Button();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            btnResultados = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.ForeColor = Color.FromArgb(10, 25, 49);
            lblTitulo.Location = new Point(364, 39);
            lblTitulo.Margin = new Padding(4);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(307, 27);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema Digital de Votación Estudiantil";
            lblTitulo.TextAlignment = ContentAlignment.TopCenter;
            // 
            // lbSubtitulo
            // 
            lbSubtitulo.BackColor = Color.Transparent;
            lbSubtitulo.ForeColor = Color.FromArgb(107, 114, 128);
            lbSubtitulo.Location = new Point(301, 132);
            lbSubtitulo.Margin = new Padding(4);
            lbSubtitulo.Name = "lbSubtitulo";
            lbSubtitulo.Size = new Size(437, 27);
            lbSubtitulo.TabIndex = 1;
            lbSubtitulo.Text = "Selecciona comenzar para iniciar el proceso de elección";
            // 
            // btnComenzarVotacion
            // 
            btnComenzarVotacion.BorderRadius = 12;
            btnComenzarVotacion.CustomizableEdges = customizableEdges1;
            btnComenzarVotacion.DisabledState.BorderColor = Color.DarkGray;
            btnComenzarVotacion.DisabledState.CustomBorderColor = Color.DarkGray;
            btnComenzarVotacion.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnComenzarVotacion.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnComenzarVotacion.FillColor = Color.FromArgb(230, 81, 0);
            btnComenzarVotacion.Font = new Font("Segoe UI", 9F);
            btnComenzarVotacion.ForeColor = Color.White;
            btnComenzarVotacion.Location = new Point(364, 381);
            btnComenzarVotacion.Margin = new Padding(4);
            btnComenzarVotacion.Name = "btnComenzarVotacion";
            btnComenzarVotacion.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnComenzarVotacion.Size = new Size(350, 62);
            btnComenzarVotacion.TabIndex = 2;
            btnComenzarVotacion.Text = "Comenzar Votacion";
            btnComenzarVotacion.Click += btnComenzarVotacion_Click;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges3;
            guna2PictureBox1.Image = (Image)resources.GetObject("guna2PictureBox1.Image");
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(364, 109);
            guna2PictureBox1.Margin = new Padding(4);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2PictureBox1.Size = new Size(354, 265);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox1.TabIndex = 3;
            guna2PictureBox1.TabStop = false;
            // 
            // btnResultados
            // 
            btnResultados.BorderRadius = 12;
            btnResultados.CustomizableEdges = customizableEdges5;
            btnResultados.DisabledState.BorderColor = Color.DarkGray;
            btnResultados.DisabledState.CustomBorderColor = Color.DarkGray;
            btnResultados.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnResultados.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnResultados.FillColor = Color.FromArgb(230, 81, 0);
            btnResultados.Font = new Font("Segoe UI", 9F);
            btnResultados.ForeColor = Color.White;
            btnResultados.Location = new Point(845, 425);
            btnResultados.Margin = new Padding(4);
            btnResultados.Name = "btnResultados";
            btnResultados.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnResultados.Size = new Size(200, 62);
            btnResultados.TabIndex = 4;
            btnResultados.Text = "Mostrar Resultados";
            btnResultados.Click += btnResultados_Click_1;
            // 
            // UcBienvenida
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            Controls.Add(btnResultados);
            Controls.Add(btnComenzarVotacion);
            Controls.Add(lbSubtitulo);
            Controls.Add(lblTitulo);
            Controls.Add(guna2PictureBox1);
            Margin = new Padding(4);
            Name = "UcBienvenida";
            Size = new Size(1059, 500);
            Load += UcBienvenida_Load;
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbSubtitulo;
        private Guna.UI2.WinForms.Guna2Button btnComenzarVotacion;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnResultados;
    }
}
