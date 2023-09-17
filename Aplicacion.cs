using System;
using System.Windows.Forms;
using FuncionesdeInsercion_Lib;

namespace AdmCompEscritorio
{
    static class Aplicacion
    {
        internal static VentanaPrincipal vp;
        internal static int minx = 0;
        internal static int miny = 0;

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                if (args.Length == 4)
                {
                    if (args[0] == "InsertarDLL")
                    {
                        IntPtr hWnd = IntPtr.Zero;
                        if (IntPtr.Size == 8)
                        {
                            if (!long.TryParse(args[1], out long hWndn)) return;
                            hWnd = new IntPtr(hWndn);
                        }
                        else
                        {
                            if (!int.TryParse(args[1], out int hWndn)) return;
                            hWnd = new IntPtr(hWndn);
                        }
                        if (!WinAPI.IsWindow(hWnd)) return;
                        if (!int.TryParse(args[2], out int numeroFn)) return;
                        if (!bool.TryParse(args[3], out bool activar)) return;
                        Console.Write(CorrerenOtroProceso.InsertarDLL(hWnd, numeroFn, activar));
                    }
                }
                return;
            }
            vp = new VentanaPrincipal();
            Application.Run(vp);
        }

        internal static void RedibujarTodo()
        {
            WinAPI.RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, WinAPI.RedrawWindowFlags.AllChildren | WinAPI.RedrawWindowFlags.Invalidate | WinAPI.RedrawWindowFlags.UpdateNow);
            HackeoEscritorio.ActualizarWorkerW();
            vp.ActualizarV();
        }

        internal static void ActualizarP()
        {
            vp.BtnEnviarAEscritorio.Text = (Screen.AllScreens.Length > 1) ? vp.eaeTextoOriginal + "..." : vp.eaeTextoOriginal;
            foreach (Screen sc in Screen.AllScreens)
            {
                if (sc.Bounds.X < minx)
                {
                    minx = sc.Bounds.X;
                }
                if (sc.Bounds.Y < miny)
                {
                    miny = sc.Bounds.Y;
                }
            }
        }
    }
}