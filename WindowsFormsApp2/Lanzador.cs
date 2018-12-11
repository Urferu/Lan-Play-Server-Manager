using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Lanzador
    {
        public static void lanzar()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }
    }
}
