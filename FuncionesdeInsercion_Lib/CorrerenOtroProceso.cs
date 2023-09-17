using System;
using System.IO;
using System.Text;
using AdmCompEscritorio;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace FuncionesdeInsercion_Lib
{
    static class CorrerenOtroProceso
    {
        internal static bool GetWindowCompositionAttribute(IntPtr hWnd) //NO FUNCIONA
        {
            return RevisarInsertarDLL(InsertarDLL(hWnd, 2, false));
        }

        internal static bool GetThemeAppProperties(IntPtr hWnd)
        {
            string test = InsertarDLL(hWnd, 3, false);
            return int.Parse(test) == 0;
        }

        internal static bool GetWindowDisplayAffinity(IntPtr hWnd)
        {
            return RevisarInsertarDLL(InsertarDLL(hWnd, 4, false));
        }

        internal static void SetWindowDisplayAffinity(IntPtr hWnd, bool activar)
        {
            InsertarDLL(hWnd, 1, activar);
        }

        internal static void DwmSetWindowAttribute(IntPtr hWnd, bool activar)
        {
            InsertarDLL(hWnd, 5, activar);
        }

        internal static void SetThemeAppProperties(IntPtr hWnd, bool activar)
        {
            InsertarDLL(hWnd, 0, activar);
        }

        private static bool RevisarInsertarDLL(string ret)
        {
            if (ret != "-1")
            {
                var retpartes = ret.Split(' ');
                if (retpartes.Length > 1)
                {
                    return int.Parse(retpartes[1]) == 1;
                }
            }
            return false;
        }

        internal static string InsertarDLL(IntPtr hWnd, int numeroFn, bool activar)
        {
            string errnombre = string.Empty;
            WinAPI.GetWindowThreadProcessId(hWnd, out int pid);
            IntPtr procHandle = WinAPI.OpenProcess(WinAPI.PROCESS_CREATE_THREAD | WinAPI.PROCESS_QUERY_INFORMATION | WinAPI.PROCESS_VM_OPERATION | WinAPI.PROCESS_VM_WRITE | WinAPI.PROCESS_VM_READ, false, (int)pid);
            if (procHandle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            string dll = "InjectionFunctions.dll";
            var thisPlatform = Environment.Is64BitProcess ? 64 : 32;
            var targetPlatform = MetodosAdicionales.Is64BitProcess(pid) ? 64 : 32;
            if (targetPlatform == 64)
            {
                dll = "InjectionFunctions64.dll";
            }
            if (thisPlatform == targetPlatform)
            {
                string dllName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), dll);
                if (File.Exists(dllName))

                {
                    string info = $"{numeroFn.ToString()} {(activar ? "1" : "0")} {hWnd.ToString()}";
                    byte[] bytesf = Encoding.Unicode.GetBytes(info);
                    MetodosAdicionales.WriteSharedMemory("ComandosACEDLL_{BEA5EAD7-AA86-494F-AC6C-B6740BED6F91}", bytesf);
                    IntPtr retLib = WinAPI.GetProcAddress(WinAPI.GetModuleHandle("Kernel32.dll"), "LoadLibraryW");
                    byte[] dllBytes = Encoding.Unicode.GetBytes(dllName);
                    uint objBuffer = (uint)dllBytes.Length;
                    IntPtr objPtr = WinAPI.VirtualAllocEx(procHandle, IntPtr.Zero, objBuffer, 4096, 4);
                    IntPtr bytesWritten = IntPtr.Zero;
                    bool ret = WinAPI.WriteProcessMemory(procHandle, objPtr, dllBytes, objBuffer, out bytesWritten);
                    IntPtr remoteThread = WinAPI.CreateRemoteThread(procHandle, IntPtr.Zero, 0, retLib, objPtr, 0, IntPtr.Zero);
                    WinAPI.WaitForSingleObject(remoteThread, WinAPI.INFINITE);
                    if (numeroFn == 3 | numeroFn == 5)
                    {
                        string n = MetodosAdicionales.ReadSharedMemory("RetornoACEDLL_{BEA5EAD7-AA86-494F-AC6C-B6740BED6F91}");
                        UIntPtr newFunc = new UIntPtr(Convert.ToUInt64(n, 16));
                        remoteThread = WinAPI.CreateRemoteThread(procHandle, IntPtr.Zero, 0, newFunc, IntPtr.Zero, 0, IntPtr.Zero);
                        WinAPI.WaitForSingleObject(remoteThread, WinAPI.INFINITE);
                        WinAPI.GetExitCodeThread(remoteThread, out uint retnuevo);
                        WinAPI.CloseHandle(procHandle);
                        return retnuevo.ToString();
                    }
                    else
                    {
                        WinAPI.CloseHandle(procHandle);
                        return MetodosAdicionales.ReadSharedMemory("RetornoACEDLL_{BEA5EAD7-AA86-494F-AC6C-B6740BED6F91}");
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                string proc = $"RunAs{targetPlatform}.exe";
                if (File.Exists(proc))
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = proc,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            CreateNoWindow = true,
                            Arguments = $"\"{typeof(Aplicacion).Assembly.Location}\" InsertarDLL {hWnd} {numeroFn} {activar}"
                        }
                    };
                    process.Start();
                    process.WaitForExit();
                    return process.StandardOutput.ReadToEnd();
                }
                else
                {
                    errnombre = $"RunAs{targetPlatform}.exe";
                    throw new FileNotFoundException();
                }
            }
        }
    }
}
