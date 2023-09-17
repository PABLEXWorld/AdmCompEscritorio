using System;
using System.Drawing;
using System.Linq;

namespace AdmCompEscritorio
{
    class HackeoEscritorio
    {
        internal static IntPtr workerw = IntPtr.Zero;
        static IntPtr defview = IntPtr.Zero;

        static bool ProcEAE(IntPtr hWnd, int param)
        {
            IntPtr p = WinAPI.FindWindowExW(hWnd, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero);
            if (p != IntPtr.Zero)
                workerw = WinAPI.FindWindowExW(IntPtr.Zero, hWnd, "WorkerW", IntPtr.Zero);
            return true;
        }

        static bool ProcDV(IntPtr hWnd, int param)
        {
            IntPtr p = WinAPI.FindWindowExW(hWnd, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero);
            if (p != IntPtr.Zero)
                defview = p;
            return true;
        }

        internal static void ActualizarEscritorio(bool juntarprogman)
        {
            WinAPI.SendMessageTimeoutW(WinAPI.FindWindow("Progman", null), WinAPI.SystemMessages.WM_USER_CONTROLPROGMAN, juntarprogman ? WinAPI.MessageFlags.JUNTARPROGMAN : WinAPI.MessageFlags.SEPARARPROGMAN, 0, WinAPI.SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out IntPtr result);
        }

        internal static void ActualizarWorkerW()
        {
            WinAPI.EnumWindows(new WinAPI.EnumWindowsDelegate(ProcEAE), 0);
            WinAPI.ShowWindow(workerw, WinAPI.ShowWindowCommands.Hide);
            WinAPI.ShowWindow(workerw, WinAPI.ShowWindowCommands.Show);
        }

        internal static bool EstaSeparadoProgman()
        {
            workerw = IntPtr.Zero;
            WinAPI.EnumWindows(new WinAPI.EnumWindowsDelegate(ProcEAE), 0);
            if (workerw == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal static void EnviaraEscritorio(Rectangle screen)
        {
            ActualizarEscritorio(false);
            if (Aplicacion.vp.Vista.SelectedNode != null)
            {
                IntPtr numero = (IntPtr)Aplicacion.vp.ColVentanas.Keys.Where(x => Aplicacion.vp.ColVentanas[x] == Aplicacion.vp.Vista.SelectedNode).Single();
                WinAPI.EnumWindows(new WinAPI.EnumWindowsDelegate(ProcEAE), 0);
                if (Properties.Settings.Default.BorrarBorde)
                {
                    WinAPI.WindowStyleFlags winStyleorig = (WinAPI.WindowStyleFlags)WinAPI.GetWindowLongPtrW(numero, WinAPI.WindowLongFlags.GWL_STYLE);
                    WinAPI.WindowStyleFlags winStyle = winStyleorig;
                    if ((winStyle & WinAPI.WindowStyleFlags.WS_DLGFRAME) != 0)
                    {
                        winStyle = winStyle & ~WinAPI.WindowStyleFlags.WS_DLGFRAME;
                    }
                    if ((winStyle & WinAPI.WindowStyleFlags.WS_BORDER) != 0)
                    {
                        winStyle = winStyle & ~WinAPI.WindowStyleFlags.WS_BORDER;
                    }
                    if ((winStyle & WinAPI.WindowStyleFlags.WS_THICKFRAME) != 0)
                    {
                        winStyle = winStyle & ~WinAPI.WindowStyleFlags.WS_THICKFRAME;
                    }
                    if (winStyle != winStyleorig)
                    {
                        WinAPI.SetWindowLongGeneric(numero, WinAPI.WindowLongFlags.GWL_STYLE, winStyle);
                    }
                    WinAPI.ExtendedWindowStyleFlags winExStyleorig = (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(numero, WinAPI.WindowLongFlags.GWL_EXSTYLE);
                    WinAPI.ExtendedWindowStyleFlags winExStyle = winExStyleorig;
                    if ((winExStyle & WinAPI.ExtendedWindowStyleFlags.WS_EX_DLGMODALFRAME) != 0)
                    {
                        winExStyle = winExStyle & ~WinAPI.ExtendedWindowStyleFlags.WS_EX_DLGMODALFRAME;
                    }
                    if ((winExStyle & WinAPI.ExtendedWindowStyleFlags.WS_EX_WINDOWEDGE) != 0)
                    {
                        winExStyle = winExStyle & ~WinAPI.ExtendedWindowStyleFlags.WS_EX_WINDOWEDGE;
                    }
                    if (winExStyle != winExStyleorig)
                    {
                        WinAPI.SetWindowLongGeneric(numero, WinAPI.WindowLongFlags.GWL_EXSTYLE, winExStyle);
                    }
                }
                WinAPI.SetParent(numero, workerw);
                WinAPI.MoveWindow(numero, screen.X - Aplicacion.minx, screen.Y - Aplicacion.miny, screen.Width, screen.Height, true);
                Aplicacion.RedibujarTodo();
            }
        }
    }
}
