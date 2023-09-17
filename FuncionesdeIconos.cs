using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace AdmCompEscritorio
{
    class FuncionesdeIconos
    {
        
        private static readonly Bitmap icon1 = IconoBase();

        internal static Bitmap IconodePrograma(IntPtr hwnd)
        {
            IntPtr iconHandle = IntPtr.Zero;
            WinAPI.SendMessageTimeoutW(hwnd, WinAPI.SystemMessages.WM_GETICON, WinAPI.MessageFlags.ICON_SMALL, 0, WinAPI.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 300, out iconHandle);

            if (iconHandle == IntPtr.Zero)
                WinAPI.SendMessageTimeoutW(hwnd, WinAPI.SystemMessages.WM_GETICON, WinAPI.MessageFlags.ICON_SMALL2, 0, WinAPI.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 300, out iconHandle);
            if (iconHandle == IntPtr.Zero)
                iconHandle = WinAPI.GetClassLongPtrW(hwnd, WinAPI.GetClassLongFlags.GCL_HICONSM);
            if (iconHandle == IntPtr.Zero)
                return icon1;
            Bitmap icn = Icon.FromHandle(iconHandle).ToBitmap();

            return icn;
        }

        internal static Bitmap IconoPequeño(string path)
        {
            WinAPI.SHFILEINFO info = new WinAPI.SHFILEINFO();
            WinAPI.SHGetFileInfoW(path, 0, ref info, (uint)Marshal.SizeOf(info), WinAPI.SHGetFileInfoFlags.SHGFI_ICON | WinAPI.SHGetFileInfoFlags.SHGFI_SMALLICON);
            return Icon.FromHandle(info.Handle).ToBitmap();
        }

        internal static Bitmap IconoBase()
        {
            WinAPI.SHFILEINFO shfi = new WinAPI.SHFILEINFO();
            WinAPI.SHGetFileInfoFlags flags = WinAPI.SHGetFileInfoFlags.SHGFI_ICON | WinAPI.SHGetFileInfoFlags.SHGFI_USEFILEATTRIBUTES | WinAPI.SHGetFileInfoFlags.SHGFI_SMALLICON;

            WinAPI.SHGetFileInfoW(".exe",
                WinAPI.FileAttributeFlags.FILE_ATTRIBUTE_NORMAL,
                ref shfi,
                (uint)Marshal.SizeOf(shfi),
                flags);
            
            Bitmap icon = Icon.FromHandle(shfi.Handle).ToBitmap();
            WinAPI.DestroyIcon(shfi.Handle);
            return icon;
        }
    }
}
