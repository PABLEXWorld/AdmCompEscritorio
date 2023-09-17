using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FuncionesdeInsercion_Lib
{
    static class MetodosAdicionales
    {
        internal static IntPtr WriteSharedMemory(string sharedMemName, byte[] dataBytes)
        {
            var buf = IntPtr.Zero;
            var view = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(dataBytes.Length);
                Marshal.Copy(dataBytes, 0, buf, dataBytes.Length);
                var mapFile = WinAPI.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, WinAPI.FileMapProtection.PageReadWrite, 0, dataBytes.Length, sharedMemName);

                view = WinAPI.MapViewOfFile(mapFile, WinAPI.FileMapAccess.FileMapAllAccess, 0, 0, dataBytes.Length);
                WinAPI.CopyMemory(view, buf, dataBytes.Length);
                return mapFile;
            }
            finally
            {
                if (buf != IntPtr.Zero) Marshal.FreeHGlobal(buf);
                if (view != IntPtr.Zero) WinAPI.UnmapViewOfFile(view);
            }
        }

        const int BUF_SIZE = 20;
        internal static string ReadSharedMemory(string sharedMemName)
        {
            var view = IntPtr.Zero;
            var mapFile = IntPtr.Zero;
            byte[] Ret = new byte[BUF_SIZE * 2];
            try
            {
                mapFile = WinAPI.OpenFileMapping(WinAPI.FILE_MAP_ALL_ACCESS, false, sharedMemName);

                if (mapFile == IntPtr.Zero)
                {
                    return string.Empty;
                }

                view = WinAPI.MapViewOfFile(mapFile, WinAPI.FileMapAccess.FileMapAllAccess, 0, 0, BUF_SIZE * 2);
                Marshal.Copy(view, Ret, 0, BUF_SIZE * 2);
            }
            finally
            {
                if (view != IntPtr.Zero) WinAPI.UnmapViewOfFile(view);
                if (mapFile != IntPtr.Zero) WinAPI.CloseHandle(mapFile);
            }
            string rets = Encoding.Unicode.GetString(Ret);
            int retn = rets.IndexOf('\0');
            rets = (retn == -1) ? string.Empty : rets.Remove(retn);
            return rets;
        }

        internal static bool Is64BitProcess(int pid)
        {
            var si = new WinAPI.SYSTEM_INFO();
            WinAPI.GetNativeSystemInfo(ref si);

            if (si.processorArchitecture == 0)
            {
                return false;
            }

            var process = WinAPI.OpenProcess(WinAPI.PROCESS_QUERY_INFORMATION, false, pid);
            if (process == null)
            {
                throw new Exception($"Cannot open process {pid}");
            }

            if (!WinAPI.IsWow64Process(process, out bool result))
            {
                throw new InvalidOperationException();
            }

            return !result;
        }
    }
}
