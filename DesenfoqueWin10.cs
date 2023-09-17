using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace AdmCompEscritorio
{
    static class DesenfoqueWin10
    {
        static Color Oscuro = ColorTranslator.FromHtml("#99000000");
        static Color Claro = ColorTranslator.FromHtml("#99FFFFFF");
        static Color Nada = ColorTranslator.FromHtml("#00000000");

        internal static void ActualizarDesenfoque(IntPtr hWnd, bool activar, bool transparenciatotal)
        {
            WinAPI.AccentState estado = activar ? WinAPI.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND : WinAPI.AccentState.ACCENT_DISABLED;
                Color colorAcento = transparenciatotal ? Nada : (((int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", 1) == 1) ? Claro : Oscuro);
            var acento = new WinAPI.AccentPolicy
            {
                AccentState = estado,
                GradientColor = (uint)colorAcento.ToArgb()
            };
            var t_acento = Marshal.SizeOf(acento);
            var ptrAcento = Marshal.AllocHGlobal(t_acento);
            Marshal.StructureToPtr(acento, ptrAcento, false);
            var datos = new WinAPI.WindowCompositionAttributeData
            {
                Attribute = WinAPI.WCA_ACCENT_POLICY,
                SizeOfData = t_acento,
                Data = ptrAcento
            };
            WinAPI.SetWindowCompositionAttribute(hWnd, ref datos);
            Marshal.FreeHGlobal(ptrAcento);
        }
    }
}
