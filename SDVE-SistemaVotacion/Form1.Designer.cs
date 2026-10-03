namespace SDVE_SistemaVotacion
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnRegresar = new Guna.UI2.WinForms.Guna2Button();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).BeginInit();
            SuspendLayout();
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = pnlTop;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(49, 50, 68);
            pnlTop.Controls.Add(guna2CirclePictureBox1);
            pnlTop.Controls.Add(lblTitulo);
            pnlTop.Controls.Add(btnRegresar);
            pnlTop.CustomizableEdges = customizableEdges4;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.ShadowDecoration.CustomizableEdges = customizableEdges5;
            pnlTop.Size = new Size(847, 50);
            pnlTop.TabIndex = 0;
            pnlTop.Paint += pnlTop_Paint;
            // 
            // guna2CirclePictureBox1
            // 
            guna2CirclePictureBox1.Image = (Image)resources.GetObject("guna2CirclePictureBox1.Image");
            guna2CirclePictureBox1.ImageRotate = 0F;
            guna2CirclePictureBox1.Location = new Point(610, 9);
            guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            guna2CirclePictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox1.Size = new Size(34, 30);
            guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2CirclePictureBox1.TabIndex = 1;
            guna2CirclePictureBox1.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.ForeColor = Color.FromArgb(205, 214, 244);
            lblTitulo.Location = new Point(285, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(261, 22);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema Digital de Votación Estudiantil";
            lblTitulo.Click += guna2HtmlLabel1_Click;
            // 
            // btnRegresar
            // 
            btnRegresar.BorderRadius = 8;
            btnRegresar.CustomizableEdges = customizableEdges2;
            btnRegresar.DisabledState.BorderColor = Color.DarkGray;
            btnRegresar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRegresar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRegresar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRegresar.FillColor = Color.FromArgb(230, 81, 0);
            btnRegresar.Font = new Font("Segoe UI", 9F);
            btnRegresar.Location = new Point(12, 6);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnRegresar.Size = new Size(59, 33);
            btnRegresar.TabIndex = 0;
            btnRegresar.Text = " ←";
            btnRegresar.Click += btnRegresar_Click;
            // 
            // pnlMain
            // 
            pnlMain.CustomizableEdges = customizableEdges6;
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 50);
            pnlMain.Name = "pnlMain";
            pnlMain.ShadowDecoration.CustomizableEdges = customizableEdges7;
            pnlMain.Size = new Size(847, 400);
            pnlMain.TabIndex = 1;
            pnlMain.Paint += pnlMain_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 42, 54);
            ClientSize = new Size(847, 450);
            Controls.Add(pnlMain);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnRegresar;
    }
}
