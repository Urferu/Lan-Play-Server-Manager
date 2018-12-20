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

            if (!AppDomain.CurrentDomain.FriendlyName.Equals("Lan-Play-Server-Manager.exe"))
            {
                File.Copy(AppDomain.CurrentDomain.FriendlyName, "Lan-Play-Server-Manager.exe", true);
                System.Diagnostics.Process splc = new System.Diagnostics.Process();
                splc.StartInfo = new System.Diagnostics.ProcessStartInfo("Lan-Play-Server-Manager.exe");
                splc.Start();
            }
            else
            {
                if (File.Exists("Lan-Play-Server-Manager-Upd.exe"))
                {
                    File.Delete("Lan-Play-Server-Manager-Upd.exe");
                }
                Lanzador.lanzar();
            }
        }
    }
}
