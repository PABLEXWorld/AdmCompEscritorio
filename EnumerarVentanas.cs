using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AdmCompEscritorio
{
    static class EnumerarVentanas
    {
        static bool ProcEnumVentanas(IntPtr hWnd, int param)
        {
            if (WinAPI.IsWindowVisible(hWnd))
            {
                StringBuilder title = new StringBuilder(new string(' ', 256));
                StringBuilder wClass = new StringBuilder(new string(' ', 256));
                int ret;
                int ret2;
                string winName;
                string winClass;
                ret = WinAPI.GetWindowTextW(hWnd, title, title.Length);
                ret2 = WinAPI.GetClassNameW(hWnd, wClass, wClass.Length);
                winName = title.ToString().Substring(0, ret);
                winClass = wClass.ToString().Substring(0, ret2);
                /*if (winClass != "ApplicationFrameWindow" & winClass != "Windows.UI.Core.CoreWindow")
                {*/
                    string tname = "[" + winClass + "]";
                    if (winName != null && winName.Length > 0)
                    {
                        tname = winName + " [" + winClass + "]";
                    }
                    if (!Aplicacion.vp.ColVentanas.ContainsKey(hWnd.ToInt32()))
                    {
                        Aplicacion.vp.ColVentanas.Add(hWnd.ToInt32(), new TreeNode(tname));
                        try
                        {
                            Bitmap icn = FuncionesdeIconos.IconodePrograma(hWnd);
                            Aplicacion.vp.BitmapList.Add(icn);
                            Aplicacion.vp.Vista.ImageList.Images.Add(icn);
                            Aplicacion.vp.ColVentanas[hWnd.ToInt32()].ImageIndex = Aplicacion.vp.Vista.ImageList.Images.Count - 1;
                        }
                        catch
                        {
                            Aplicacion.vp.ColVentanas[hWnd.ToInt32()].ImageIndex = -1;
                        }
                        Aplicacion.vp.ColVentanas[hWnd.ToInt32()].SelectedImageIndex = Aplicacion.vp.ColVentanas[hWnd.ToInt32()].ImageIndex;
                        if (param == -1)
                        {
                            Aplicacion.vp.Vista.Nodes.Add(Aplicacion.vp.ColVentanas[hWnd.ToInt32()]);
                        }
                        else
                        {
                            Aplicacion.vp.ColVentanas[param].Nodes.Add(Aplicacion.vp.ColVentanas[hWnd.ToInt32()]);
                        }
                    }
                    WinAPI.EnumChildWindows(hWnd, ProcEnumVentanas, hWnd.ToInt32());
                //}
            }
            return true;
        }

        internal static void EnumVentanas()
        {
            WinAPI.EnumWindows(new WinAPI.EnumWindowsDelegate(ProcEnumVentanas), -1);
        }
    }
}
