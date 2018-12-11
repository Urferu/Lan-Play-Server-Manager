using System;

namespace WindowsFormsApp2
{
    public class WinApi
    {
        // Hace que una ventana sea hija (o esté contenida) en otra
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        // Devuelve el Handle (hWnd) de una ventana de la que sabemos el título
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        // Cambia el tamaño y la posición de una ventana
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static int MoveWindow(IntPtr hWnd, int x, int y,
                int nWidth, int nHeight, int bRepaint);

        //Sets window attributes
        [System.Runtime.InteropServices.DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //Gets window attributes
        [System.Runtime.InteropServices.DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        //assorted constants needed
        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000; //child window
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar

        public static void RemoveBorder(System.Diagnostics.Process proc)
        {
            IntPtr pFoundWindow = proc.MainWindowHandle;
            int style = GetWindowLong(pFoundWindow, GWL_STYLE);
            SetWindowLong(pFoundWindow, GWL_STYLE, (style & ~WS_CAPTION));
        }
    }
}
