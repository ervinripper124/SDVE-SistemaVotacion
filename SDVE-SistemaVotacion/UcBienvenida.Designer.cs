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
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lbSubtitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnComenzarVotacion = new Guna.UI2.WinForms.Guna2Button();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.ForeColor = Color.FromArgb(10, 25, 49);
            lblTitulo.Location = new Point(291, 31);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(261, 22);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema Digital de Votación Estudiantil";
            lblTitulo.TextAlignment = ContentAlignment.TopCenter;
            // 
            // lbSubtitulo
            // 
            lbSubtitulo.BackColor = Color.Transparent;
            lbSubtitulo.ForeColor = Color.FromArgb(107, 114, 128);
            lbSubtitulo.Location = new Point(241, 106);
            lbSubtitulo.Name = "lbSubtitulo";
            lbSubtitulo.Size = new Size(374, 22);
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
            btnComenzarVotacion.Location = new Point(291, 305);
            btnComenzarVotacion.Name = "btnComenzarVotacion";
            btnComenzarVotacion.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnComenzarVotacion.Size = new Size(280, 50);
            btnComenzarVotacion.TabIndex = 2;
            btnComenzarVotacion.Text = "Comenzar Votacion";
            btnComenzarVotacion.Click += btnComenzarVotacion_Click;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges3;
            guna2PictureBox1.Image = (Image)resources.GetObject("guna2PictureBox1.Image");
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(291, 87);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2PictureBox1.Size = new Size(283, 212);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2PictureBox1.TabIndex = 3;
            guna2PictureBox1.TabStop = false;
            // 
            // UcBienvenida
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            Controls.Add(btnComenzarVotacion);
            Controls.Add(lbSubtitulo);
            Controls.Add(lblTitulo);
            Controls.Add(guna2PictureBox1);
            Name = "UcBienvenida";
            Size = new Size(847, 400);
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
    }
}
