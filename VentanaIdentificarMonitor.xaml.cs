using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Media.Effects;
using System.Windows.Media;

[assembly: DisableDpiAwareness]
namespace AdmCompEscritorio
{
    public partial class VentanaIdentificarMonitor : Window
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, WinAPI.WindowLongFlags nIndex);
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, WinAPI.WindowLongFlags nIndex);

        [DllImport("Shcore.dll")]
        private static extern IntPtr GetDpiForMonitor(IntPtr hmonitor, DpiType dpiType, out uint dpiX, out uint dpiY);

        [DllImport("User32.dll")]
        private static extern IntPtr MonitorFromPoint(System.Drawing.Point pt, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public enum DpiType
        {
            Effective = 0,
            Angular = 1,
            Raw = 2,
        }

        private static IntPtr GetWindowLongPtr(IntPtr hWnd, WinAPI.WindowLongFlags nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLongPtr32(hWnd, nIndex);
        }

        public static void GetDpi(System.Windows.Forms.Screen screen, DpiType dpiType, out uint dpiX, out uint dpiY)
        {
            var pnt = new System.Drawing.Point(screen.Bounds.Left + 1, screen.Bounds.Top + 1);
            var mon = MonitorFromPoint(pnt, 2/*MONITOR_DEFAULTTONEAREST*/);
            GetDpiForMonitor(mon, dpiType, out dpiX, out dpiY);
        }

        DispatcherTimer dispatcherTimer;
        Rect rectwa;
        VentanaEAE venEAE;
        public VentanaIdentificarMonitor(int id, Rect rectRes, Rect rectwa, VentanaEAE eae)
        {
            venEAE = eae;
            InitializeComponent();
            TextBlock text = FindName("Number") as TextBlock;
            uint dpiX, dpiY;
            GetDpi(System.Windows.Forms.Screen.AllScreens[id - 1], DpiType.Effective, out dpiX, out dpiY);

            double scaleFactor = 1;
            scaleFactor = (dpiX + dpiY) / 96 / 2;
            Grid grid = FindName("Grid") as Grid;
            ((DropShadowEffect)grid.Effect).ShadowDepth = (rectRes.Width + rectRes.Height) / scaleFactor / 1000 * 3/* * scaleFactor*/;
            ((DropShadowEffect)grid.Effect).BlurRadius = (rectRes.Width + rectRes.Height) / scaleFactor / 1000/* * scaleFactor*/;
            ((DropShadowEffect)text.Effect).BlurRadius = (rectRes.Width + rectRes.Height) / scaleFactor / 1000 * 8/* * scaleFactor*/;
            text.Text = id.ToString();
            text.FontSize = (rectRes.Width + rectRes.Height) / scaleFactor / 10/* * scaleFactor*/;
            text.FontSize = (rectRes.Width + rectRes.Height) / scaleFactor / 10/* * scaleFactor*/;
            this.rectwa = rectwa;
            Width = rectwa.Width / dpiX * 96;
            Height = rectwa.Height / dpiY * 96;
            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.EnsureHandle();
            helper.Owner = venEAE.Handle;
            SetWindowPos(helper.Handle, IntPtr.Zero, (int)rectwa.Left, (int)rectwa.Top, 0, 0, 0x0001); // SWP_NOSIZE
            WinAPI.ExtendedWindowStyleFlags extendedStyle = (WinAPI.ExtendedWindowStyleFlags)GetWindowLongPtr(helper.Handle, WinAPI.WindowLongFlags.GWL_EXSTYLE);
            WinAPI.SetWindowLong(helper.Handle, WinAPI.WindowLongFlags.GWL_EXSTYLE, extendedStyle | WinAPI.ExtendedWindowStyleFlags.WS_EX_TRANSPARENT);
        }

        internal void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            Close();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            venEAE.listaVentanas.Remove(this);
        }
    }
}
