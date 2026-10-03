using System.Windows.Forms;

namespace SDVE_SistemaVotacion
{
    public static class HelperUI
    {
        // Método de extensión para centrar un solo control horizontalmente
        public static void CentrarHorizontal(this Control control, Control contenedor)
        {
            if (control != null && contenedor != null)
            {
                control.Left = (contenedor.Width - control.Width) / 2;
            }
        }

        // Método sobrecargado para centrar varios controles de un solo golpe
        public static void CentrarVariosHorizontal(Control contenedor, params Control[] controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl != null)
                {
                    ctrl.Left = (contenedor.Width - ctrl.Width) / 2;
                }
            }
        }
    }
}