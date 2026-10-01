using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace SDVE_SistemaVotacion
{
    public partial class UcVotoRegistrado : UserControl
    {
        private int segundosRestantes = 5;
        public UcVotoRegistrado()
        {
            InitializeComponent();
        }

        private void UcVotoRegistrado_Load(object sender, EventArgs e)
        {
            lblFolioVotar.Text = "Gracias por votar! Tu folio es: " + GenerarFolio(8);

            guna2ProgressBar1.Maximum = segundosRestantes;
            guna2ProgressBar1.Value = segundosRestantes;

            timerCuentaRegresiva.Interval = 1000; 
            timerCuentaRegresiva.Start();

        }
        public static string GenerarFolio(int longitud = 8)
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] resultado = new char[longitud];

            for (int i = 0; i < longitud; i++)
            {
                int indice = RandomNumberGenerator.GetInt32(caracteres.Length);
                resultado[i] = caracteres[indice];
            }

            return new string(resultado);
        }


        private void timerCuentaRegresiva_Tick(object sender, EventArgs e)
        {
            segundosRestantes--;
            lblSegundosRestantes.Text = "Volviendo al inicio en " + segundosRestantes.ToString()+" segundos";
            if (segundosRestantes >= 0)
            {
             
                guna2ProgressBar1.Value = segundosRestantes;
            }
            else
            {
                
                timerCuentaRegresiva.Stop();

                Form1 ventanaPrincipal = this.ParentForm as Form1;
                if (ventanaPrincipal != null)
                {
                    UcBienvenida VolverBienvenida = new UcBienvenida();     
                    ventanaPrincipal.Ir(VolverBienvenida);
                }
            }
        }

     
      
    }
}
