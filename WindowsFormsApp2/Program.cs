using System;
using System.IO;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists("MaterialSkin.dll"))
            {
                FileStream fsArchivo = new FileStream("MaterialSkin.dll", FileMode.Create);
                fsArchivo.Write(Properties.Resources.MaterialSkin, 0, Properties.Resources.MaterialSkin.Length);
                fsArchivo.Close();
            }
            Lanzador.lanzar();
        }
    }
}
