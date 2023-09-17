using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

partial class WinAPI
{
    #region Constants
    internal const int WM_VSCROLL = 0x0115;
    internal const int WM_SETTINGCHANGE = 0x001A;
    internal const int WM_SYSCOMMAND = 0x112;
    internal const int SC_CONTEXTHELP = 0xf180;
    internal const int DWMNCRP_USEWINDOWSTYLE = 0;
    internal const int DWMNCRP_DISABLED = 1;
    internal const int DWMNCRP_ENABLED = 2;
    internal const uint NCRenderingEnabled = 1;
    internal const uint NCRenderingPolicy = 2;
    internal const uint DWMWA_ALLOW_NCPAINT = 4;
    internal const int WCA_ACCENT_POLICY = 19;
    internal const int PROCESS_CREATE_THREAD = 0x0002;
    internal const int PROCESS_QUERY_INFORMATION = 0x0400;
    internal const int PROCESS_VM_OPERATION = 0x0008;
    internal const int PROCESS_VM_WRITE = 0x0020;
    internal const int PROCESS_VM_READ = 0x0010;
    internal const uint MEM_COMMIT = 0x00001000;
    internal const uint MEM_RESERVE = 0x00002000;
    internal const uint PAGE_READWRITE = 4;
    internal const uint INFINITE = 0xFFFFFFFF;
    internal const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
    internal const uint SECTION_QUERY = 0x0001;
    internal const uint SECTION_MAP_WRITE = 0x0002;
    internal const uint SECTION_MAP_READ = 0x0004;
    internal const uint SECTION_MAP_EXECUTE = 0x0008;
    internal const uint SECTION_EXTEND_SIZE = 0x0010;
    internal const uint SECTION_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SECTION_QUERY |
        SECTION_MAP_WRITE |
        SECTION_MAP_READ |
        SECTION_MAP_EXECUTE |
        SECTION_EXTEND_SIZE);
    internal const uint FILE_MAP_ALL_ACCESS = SECTION_ALL_ACCESS;
    internal const int SM_CXSMICON = 49;
    internal const int SM_CYSMICON = 50;
    #endregion
    
    #region Delegates
    internal delegate bool EnumWindowsDelegate(IntPtr hWnd, int param);
    #endregion

    #region DllImports
    [DllImport("user32.dll")]
    internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    [DllImport("dwmapi.dll", PreserveSig = true)]
    internal static extern int DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref int attrValue, int attrSize);

    [DllImport("dwmapi.dll")]
    internal static extern int DwmGetWindowAttribute(IntPtr hwnd, uint dwAttribute, out bool pvAttribute, int cbAttribute);

    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    [DllImport("user32.dll")]
    internal static extern bool SetWindowDisplayAffinity(IntPtr hwnd, uint affinity);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool IsWindow(IntPtr hWnd);

    [DllImport("dwmapi.dll", PreserveSig = true)]
    internal static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr SetWindowLong(IntPtr hWnd, WindowLongFlags nIndex, WindowStyleFlags dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex, WindowStyleFlags dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr SetWindowLong(IntPtr hWnd, WindowLongFlags nIndex, ExtendedWindowStyleFlags dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex, ExtendedWindowStyleFlags dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern IntPtr FindWindowExW(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetWindowLongW")]
    internal static extern IntPtr GetWindowLongPtrW32(IntPtr hWnd, WindowLongFlags nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetWindowLongPtrW")]
    internal static extern IntPtr GetWindowLongPtrW64(IntPtr hWnd, WindowLongFlags nIndex);

    [DllImport("user32.dll")]
    internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

    [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
    internal static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern bool DestroyWindowW(IntPtr hwnd);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr SHGetFileInfoW(string path, FileAttributeFlags fattrs, ref SHFILEINFO sfi, uint size, SHGetFileInfoFlags flags);

    [DllImport("user32.dll")]
    internal static extern bool DestroyIcon(IntPtr hIcon);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetClassLongW")]
    internal static extern uint GetClassLongPtrW32(IntPtr hWnd, GetClassLongFlags nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetClassLongPtrW")]
    internal static extern IntPtr GetClassLongPtrW64(IntPtr hWnd, GetClassLongFlags nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
    internal static extern IntPtr SendMessage(IntPtr hWnd, SystemMessages Msg, MessageFlags wParam, int lParam);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern IntPtr SendMessageTimeoutW(IntPtr windowHandle, SystemMessages Msg, MessageFlags wParam, int lParam, SendMessageTimeoutFlags flag, uint timeout, out IntPtr result);

    [DllImport("user32.dll")]
    internal static extern bool EnumWindows(EnumWindowsDelegate lpfn, int lParam);

    [DllImport("user32.dll")]
    internal static extern int EnumChildWindows(IntPtr hWndParent, EnumWindowsDelegate lpEnumFunc, int lParam);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int cch);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    internal static extern int GetClassNameW(IntPtr hWnd, StringBuilder lpString, int cch);

    [DllImport("user32.dll")]
    internal static extern int GetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
    internal static extern bool IsWow64Process(IntPtr process, out bool wow64Process);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, FileMapProtection flProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, FileMapAccess dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

    [DllImport("kernel32.dll", SetLastError = false)]
    internal static extern void CopyMemory(IntPtr dest, IntPtr src, int count);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern IntPtr OpenFileMapping(uint dwDesiredAccess, bool bInheritHandle, string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SuppressUnmanagedCodeSecurity]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal extern static bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);

    [DllImport("user32.dll")]
    internal static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    internal static extern int GetSystemMetricsForDpi(int nIndex, uint dpi);

    [DllImport("dwmapi.dll", PreserveSig = true)]
    internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref int attrValue, int attrSize);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out uint crKey, out byte bAlpha, out uint dwFlags);

    [DllImport("user32.dll")]
    internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
    #endregion

    #region Function Pointers
    internal static IntPtr SetWindowLongGeneric(IntPtr hWnd, WindowLongFlags nIndex, WindowStyleFlags dwNewLong)
    {
        if (IntPtr.Size > 4)
            return SetWindowLongPtr(hWnd, nIndex, dwNewLong);
        else
            return SetWindowLong(hWnd, nIndex, dwNewLong);
    }

    internal static IntPtr SetWindowLongGeneric(IntPtr hWnd, WindowLongFlags nIndex, ExtendedWindowStyleFlags dwNewLong)
    {
        if (IntPtr.Size > 4)
            return SetWindowLongPtr(hWnd, nIndex, dwNewLong);
        else
            return SetWindowLong(hWnd, nIndex, dwNewLong);
    }

    internal static IntPtr GetWindowLongPtrW(IntPtr hWnd, WindowLongFlags nIndex)
    {
        if (IntPtr.Size == 8)
            return GetWindowLongPtrW64(hWnd, nIndex);
        else
            return GetWindowLongPtrW32(hWnd, nIndex);
    }

    internal static IntPtr GetClassLongPtrW(IntPtr hWnd, GetClassLongFlags nIndex)
    {
        if (IntPtr.Size > 4)
            return GetClassLongPtrW64(hWnd, nIndex);
        else
            return new IntPtr(GetClassLongPtrW32(hWnd, nIndex));
    }
    #endregion

    #region Structures
    [StructLayout(LayoutKind.Sequential)]
    internal struct MARGINS
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public int Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHFILEINFO
    {
        public IntPtr Handle { get; set; }
        private IntPtr index;
        private uint attr;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        private string display;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        private string type;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEM_INFO
    {
        public ushort processorArchitecture;
        ushort reserved;
        public uint pageSize;
        public IntPtr minimumApplicationAddress;
        public IntPtr maximumApplicationAddress;
        public IntPtr activeProcessorMask;
        public uint numberOfProcessors;
        public uint processorType;
        public uint allocationGranularity;
        public ushort processorLevel;
        public ushort processorRevision;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }
    #endregion

    #region Flags
    [Flags]
    internal enum RedrawWindowFlags : uint
    {
        Invalidate = 0x1,
        InternalPaint = 0x2,
        Erase = 0x4,
        Validate = 0x8,
        NoInternalPaint = 0x10,
        NoErase = 0x20,
        NoChildren = 0x40,
        AllChildren = 0x80,
        UpdateNow = 0x100,
        EraseNow = 0x200,
        Frame = 0x400,
        NoFrame = 0x800
    }

    [Flags]
    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
        ACCENT_INVALID_STATE = 5
    }

    [Flags]
    internal enum ShowWindowCommands
    {
        Hide = 0,
        Normal = 1,
        ShowMinimized = 2,
        Maximize = 3,
        ShowMaximized = 3,
        ShowNoActivate = 4,
        Show = 5,
        Minimize = 6,
        ShowMinNoActive = 7,
        ShowNA = 8,
        Restore = 9,
        ShowDefault = 10,
        ForceMinimize = 11
    }

    [Flags]
    internal enum WindowStyleFlags : long
    {
        WS_BORDER = 0x00800000,
        WS_CAPTION = 0x00C00000,
        WS_CHILD = 0x40000000,
        WS_CHILDWINDOW = 0x40000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_DISABLED = 0x08000000,
        WS_DLGFRAME = 0x00400000,
        WS_GROUP = 0x00020000,
        WS_HSCROLL = 0x00100000,
        WS_ICONIC = 0x20000000,
        WS_MAXIMIZE = 0x01000000,
        WS_MAXIMIZEBOX = 0x00010000,
        WS_MINIMIZE = 0x20000000,
        WS_MINIMIZEBOX = 0x00020000,
        WS_OVERLAPPED = 0x00000000,
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUP = 0x80000000,
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
        WS_SIZEBOX = 0x00040000,
        WS_SYSMENU = 0x00080000,
        WS_TABSTOP = 0x00010000,
        WS_THICKFRAME = 0x00040000,
        WS_TILED = 0x00000000,
        WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_VISIBLE = 0x10000000,
        WS_VSCROLL = 0x00200000
    }

    internal enum DWMWINDOWATTRIBUTE : uint
    {
        DWMWA_NCRENDERING_ENABLED = 1,      // [get] Is non-client rendering enabled/disabled
        DWMWA_NCRENDERING_POLICY,           // [set] DWMNCRENDERINGPOLICY - Non-client rendering policy
        DWMWA_TRANSITIONS_FORCEDISABLED,    // [set] Potentially enable/forcibly disable transitions
        DWMWA_ALLOW_NCPAINT,                // [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.
        DWMWA_CAPTION_BUTTON_BOUNDS,        // [get] Bounds of the caption button area in window-relative space.
        DWMWA_NONCLIENT_RTL_LAYOUT,         // [set] Is non-client content RTL mirrored
        DWMWA_FORCE_ICONIC_REPRESENTATION,  // [set] Force this window to display iconic thumbnails.
        DWMWA_FLIP3D_POLICY,                // [set] Designates how Flip3D will treat the window.
        DWMWA_EXTENDED_FRAME_BOUNDS,        // [get] Gets the extended frame bounds rectangle in screen space
        DWMWA_HAS_ICONIC_BITMAP,            // [set] Indicates an available bitmap when there is no better thumbnail representation.
        DWMWA_DISALLOW_PEEK,                // [set] Don't invoke Peek on the window.
        DWMWA_EXCLUDED_FROM_PEEK,           // [set] LivePreview exclusion information
        DWMWA_CLOAK,                        // [set] Cloak or uncloak the window
        DWMWA_CLOAKED,                      // [get] Gets the cloaked state of the window
        DWMWA_FREEZE_REPRESENTATION,        // [set] BOOL, Force this window to freeze the thumbnail without live update
        DWMWA_LAST
    };

    [Flags]
    internal enum ExtendedWindowStyleFlags : long
    {
        WS_EX_ACCEPTFILES = 0x00000010,
        WS_EX_APPWINDOW = 0x00040000,
        WS_EX_CLIENTEDGE = 0x00000200,
        WS_EX_COMPOSITED = 0x02000000,
        WS_EX_CONTEXTHELP = 0x00000400,
        WS_EX_CONTROLPARENT = 0x00010000,
        WS_EX_DLGMODALFRAME = 0x00000001,
        WS_EX_LAYERED = 0x00080000,
        WS_EX_LAYOUTRTL = 0x00400000,
        WS_EX_LEFT = 0x00000000,
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        WS_EX_LTRREADING = 0x00000000,
        WS_EX_MDICHILD = 0x00000040,
        WS_EX_NOACTIVATE = 0x08000000,
        WS_EX_NOINHERITLAYOUT = 0x00100000,
        WS_EX_NOPARENTNOTIFY = 0x00000004,
        WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
        WS_EX_RIGHT = 0x00001000,
        WS_EX_RIGHTSCROLLBAR = 0x00000000,
        WS_EX_RTLREADING = 0x00002000,
        WS_EX_STATICEDGE = 0x00020000,
        WS_EX_TOOLWINDOW = 0x00000080,
        WS_EX_TOPMOST = 0x00000008,
        WS_EX_TRANSPARENT = 0x00000020,
        WS_EX_WINDOWEDGE = 0x00000100
    }

    [Flags]
    internal enum WindowLongFlags : int
    {
        GWL_EXSTYLE = -20,
        GWLP_HINSTANCE = -6,
        GWLP_HWNDPARENT = -8,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_USERDATA = -21,
        GWL_WNDPROC = -4,
        DWLP_USER = 0x8,
        DWLP_MSGRESULT = 0x0,
        DWLP_DLGPROC = 0x4
    }

    [Flags]
    internal enum GetClassLongFlags : int
    {
        GCW_ATOM = -32,
        GCL_CBCLSEXTRA = -20,
        GCL_CBWNDEXTRA = -18,
        GCL_HBRBACKGROUND = -10,
        GCL_HCURSOR = -12,
        GCL_HICON = -14,
        GCL_HICONSM = -34,
        GCL_HMODULE = -16,
        GCL_MENUNAME = -8,
        GCL_STYLE = -26,
        GCL_WNDPROC = -24
    }

    [Flags]
    internal enum MessageFlags : int
    {
        JUNTARPROGMAN = 1,
        SEPARARPROGMAN = 0,
        ICON_BIG = 1,
        ICON_SMALL = 0,
        ICON_SMALL2 = 2
    }

    [Flags]
    internal enum SystemMessages : int
    {
        WM_USER = 0x400,
        WM_USER_CONTROLPROGMAN = WM_USER + 300,
        WM_GETICON = 0x7F
    }

    [Flags]
    internal enum SendMessageTimeoutFlags : uint
    {
        SMTO_ABORTIFHUNG = 0x0002,
        SMTO_BLOCK = 0x0001,
        SMTO_NORMAL = 0x0000,
        SMTO_NOTIMEOUTIFNOTHUNG = 0x0008,
        SMTO_ERRORONEXIT = 0x0020
    }

    [Flags]
    internal enum SHGetFileInfoFlags : uint
    {
        SHGFI_ADDOVERLAYS = 0x000000020,
        SHGFI_ATTR_SPECIFIED = 0x000020000,
        SHGFI_ATTRIBUTES = 0x000000800,
        SHGFI_DISPLAYNAME = 0x000000200,
        SHGFI_EXETYPE = 0x000002000,
        SHGFI_ICON = 0x000000100,
        SHGFI_ICONLOCATION = 0x000001000,
        SHGFI_LARGEICON = 0x000000000,
        SHGFI_LINKOVERLAY = 0x000008000,
        SHGFI_OPENICON = 0x000000002,
        SHGFI_OVERLAYINDEX = 0x000000040,
        SHGFI_PIDL = 0x000000008,
        SHGFI_SELECTED = 0x000010000,
        SHGFI_SHELLICONSIZE = 0x000000004,
        SHGFI_SMALLICON = 0x000000001,
        SHGFI_SYSICONINDEX = 0x000004000,
        SHGFI_TYPENAME = 0x000000400,
        SHGFI_USEFILEATTRIBUTES = 0x000000010
    }

    [Flags]
    internal enum FileAttributeFlags : uint
    {
        FILE_ATTRIBUTE_ARCHIVE = 0x20,
        FILE_ATTRIBUTE_HIDDEN = 0x2,
        FILE_ATTRIBUTE_NORMAL = 0x80,
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x2000,
        FILE_ATTRIBUTE_OFFLINE = 0x1000,
        FILE_ATTRIBUTE_READONLY = 0x1,
        FILE_ATTRIBUTE_SYSTEM = 0x4,
        FILE_ATTRIBUTE_TEMPORARY = 0x100
    }

    [Flags]
    internal enum FileMapProtection : uint
    {
        PageReadonly = 0x02,
        PageReadWrite = 0x04,
        PageWriteCopy = 0x08,
        PageExecuteRead = 0x20,
        PageExecuteReadWrite = 0x40,
        SectionCommit = 0x8000000,
        SectionImage = 0x1000000,
        SectionNoCache = 0x10000000,
        SectionReserve = 0x4000000,
    }

    [Flags]
    internal enum FileMapAccess : uint
    {
        FileMapCopy = 0x0001,
        FileMapWrite = 0x0002,
        FileMapRead = 0x0004,
        FileMapAllAccess = 0x001f,
        FileMapExecute = 0x0020,
    }
    #endregion

}
