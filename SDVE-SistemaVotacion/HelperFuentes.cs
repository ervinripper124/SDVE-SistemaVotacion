using System;
using System.Drawing;
using System.Drawing.Text;

namespace SDVE_SistemaVotacion
{
    public static class HelperFuentes
    {
        private static PrivateFontCollection coleccion = new PrivateFontCollection();

        public static Font BricolageBold(float tamano)
        {
            if (coleccion.Families.Length == 0) Cargar();
            return new Font(coleccion.Families[0], tamano, FontStyle.Bold);
        }

        public static Font InstrumentRegular(float tamano)
        {
            if (coleccion.Families.Length == 0) Cargar();
            return new Font(coleccion.Families[1], tamano, FontStyle.Regular);
        }

        private static void Cargar()
        {
            // Rutas relativas a tus archivos en la carpeta fonts
            string rutaBricolage = System.IO.Path.Combine(Application.StartupPath, "fonts", "BricolageGrotesque_24pt-Bold.ttf");
            string rutaInstrument = System.IO.Path.Combine(Application.StartupPath, "fonts", "InstrumentSans-Regular.ttf");

            if (System.IO.File.Exists(rutaBricolage)) coleccion.AddFontFile(rutaBricolage);
            if (System.IO.File.Exists(rutaInstrument)) coleccion.AddFontFile(rutaInstrument);
        }
    }
}  