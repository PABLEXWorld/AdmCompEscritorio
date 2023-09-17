using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using FuncionesdeInsercion_Lib;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdmCompEscritorio
{
    internal partial class VentanaPrincipal : Form
    {
        private Size minSize;
        private bool Dragging;
        internal string eaeTextoOriginal;
        private string SepararTextoOriginal;
        internal TreeView Vista { get => Tree1; }
        internal Dictionary<int, TreeNode> ColVentanas { get => ColVentanas1; }
        public Dictionary<int, TreeNode> ColVentanas1 { get; set; } = new Dictionary<int, TreeNode>();
        internal List<Bitmap> BitmapList = new List<Bitmap>();

        internal VentanaPrincipal()
        {
            InitializeComponent();
            Tree1.ImageList.ImageSize = SystemInformation.SmallIconSize;
            eaeTextoOriginal = BtnEnviarAEscritorio.Text;
            SepararTextoOriginal = BtnSeparar.Text;
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Tree1.SelectedNode = e.Item as TreeNode;
            if (ObtenerHWND(out IntPtr hWnd))
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private bool DropValid(DragEventArgs e)
        {
            Point targetPoint = Tree1.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = Tree1.GetNodeAt(targetPoint);
            if (!(e.Data.GetData(typeof(TreeNode)) is TreeNode draggedNode))
            {
                return false;
            }
            if (targetNode == null)
            {
                return true;
            }
            else
            {
                TreeNode parentNode = targetNode;
                if (!draggedNode.Equals(targetNode) && targetNode != null)
                {
                    bool canDrop = true;
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }
                    if (canDrop)
                    {
                        try
                        {
                            WinAPI.GetWindowThreadProcessId((IntPtr)ColVentanas1.Keys.Where(x => ColVentanas1[x] == targetNode).Single(), out int pid);
                            IntPtr procHandle = WinAPI.OpenProcess(WinAPI.PROCESS_CREATE_THREAD | WinAPI.PROCESS_QUERY_INFORMATION | WinAPI.PROCESS_VM_OPERATION | WinAPI.PROCESS_VM_WRITE | WinAPI.PROCESS_VM_READ, false, (int)pid);
                            if (procHandle == IntPtr.Zero)
                                throw new Win32Exception(Marshal.GetLastWin32Error());
                            WinAPI.CloseHandle(procHandle);
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            var targetPoint = Tree1.PointToClient(new Point(e.X, e.Y));
            var numero1 = (IntPtr)ColVentanas1.Keys.Where(x => ColVentanas1[x] == e.Data.GetData(typeof(TreeNode)) as TreeNode).Single();
            var numero2 = IntPtr.Zero;
            var winStyleorig = (WinAPI.WindowStyleFlags)WinAPI.GetWindowLongPtrW(numero1, WinAPI.WindowLongFlags.GWL_STYLE);
            var winStyle = winStyleorig;
            if (Tree1.GetNodeAt(targetPoint) != null)
            {
                numero2 = (IntPtr)ColVentanas1.Keys.Where(x => ColVentanas1[x] == Tree1.GetNodeAt(targetPoint)).Single();
                if ((winStyle & WinAPI.WindowStyleFlags.WS_CHILD) == 0)
                {
                    winStyle |= WinAPI.WindowStyleFlags.WS_CHILD;
                }
            }
            else
            {
                if ((winStyle & WinAPI.WindowStyleFlags.WS_CHILD) != 0)
                {
                    winStyle &= ~WinAPI.WindowStyleFlags.WS_CHILD;
                }
                if (Properties.Settings.Default.AgregarBorde & HackeoEscritorio.EstaSeparadoProgman() & (IntPtr)ColVentanas1.Keys.Where(x => ColVentanas1[x] == (e.Data.GetData(typeof(TreeNode)) as TreeNode).Parent).Single() == HackeoEscritorio.workerw)
                {
                    if ((winStyle & WinAPI.WindowStyleFlags.WS_DLGFRAME) == 0)
                    {
                        winStyle |= WinAPI.WindowStyleFlags.WS_DLGFRAME;
                    }
                    if ((winStyle & WinAPI.WindowStyleFlags.WS_BORDER) == 0)
                    {
                        winStyle |= WinAPI.WindowStyleFlags.WS_BORDER;
                    }
                }
            }
            if (winStyle != winStyleorig)
            {
                WinAPI.SetWindowLongGeneric(numero1, WinAPI.WindowLongFlags.GWL_STYLE, winStyle);
            }
            WinAPI.SetParent(numero1, numero2);
            Aplicacion.RedibujarTodo();
            Tree1.SelectedNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
        }

        private void DragScroll(DragEventArgs e)
        {
            const int scrollRegion = 20;

            var pt = Tree1.PointToClient(Cursor.Position);

            if ((pt.Y + scrollRegion) > Tree1.Height)
            {
                WinAPI.SendMessage(Tree1.Handle, WinAPI.WM_VSCROLL, 1, 0);
            }
            else if (pt.Y < (Tree1.Top + scrollRegion))
            {
                WinAPI.SendMessage(Tree1.Handle, WinAPI.WM_VSCROLL, 0, 0);
            }
        }

        private void Tree1_DragOver(object sender, DragEventArgs e)
        {
            DragScroll(e);
            if (DropValid(e))
            {
                e.Effect = DragDropEffects.Move;
                TreeNode node = Tree1.GetNodeAt(Tree1.PointToClient(new Point(e.X, e.Y)));
                Dragging = true;
                Tree1.SelectedNode = node;
                Dragging = false;
            }
            else
            {
                e.Effect = DragDropEffects.None;
                Tree1.SelectedNode = null;
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WinAPI.WM_SETTINGCHANGE:
                    Aplicacion.ActualizarP();
                    break;
            }
            base.WndProc(ref m);
        }

        internal void ActualizarV()
        {
            checkAcrilico.Checked = false;
            checkBasico.Checked = false;
            checkClasico.Checked = false;
            checkSinCapturas.Checked = false;
            checkCT.Checked = false;
            checkCloak.Checked = false;
            checkOp.Checked = false;
            BitmapList.Clear();
            Tree1.ImageList.Images.Clear();
            ColVentanas1.Clear();
            Tree1.Nodes.Clear();
            foreach (Control control in grupoPropComposicion.Controls)
            {
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
            }
            EnumerarVentanas.EnumVentanas();
            BtnSeparar.Text = HackeoEscritorio.EstaSeparadoProgman() ? Properties.frases.BtnJuntar_text : SepararTextoOriginal;
        }

        private bool ObtenerHWND(out IntPtr resultado)
        {
            try
            {
                resultado = (IntPtr)ColVentanas1.Keys.Where(x => ColVentanas1[x] == Tree1.SelectedNode).Single();
                try
                {
                    WinAPI.GetWindowThreadProcessId(resultado, out int pid);
                    IntPtr procHandle = WinAPI.OpenProcess(WinAPI.PROCESS_CREATE_THREAD | WinAPI.PROCESS_QUERY_INFORMATION | WinAPI.PROCESS_VM_OPERATION | WinAPI.PROCESS_VM_WRITE | WinAPI.PROCESS_VM_READ, false, (int)pid);
                    if (procHandle == IntPtr.Zero)
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    WinAPI.CloseHandle(procHandle);
                    return true;
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show(Aplicacion.vp, string.Format(Properties.frases.PrivAdmin_text, ex.Message, Text), string.Format(Properties.frases.MsgErrorClasico_text, Tree1.SelectedNode.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show(this, Properties.frases.MsgErrorNoSeleccion_text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                resultado = IntPtr.Zero;
                return false;
            }
        }

        private void VentanaPrincipal_Shown(object sender, EventArgs e)
        {
            minSize = Size;
            MinimumSize = minSize;
            ActualizarV();
            /*if (ShouldAppsUseDarkMode())
            {
                MessageBox.Show("GoDark se llamaria");
            }*/
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarV();
        }

        private void BtnEnviarAEscritorio_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                if (Screen.AllScreens.Length > 1)
                {
                    var title = new StringBuilder(new string(' ', 256));
                    var ret = WinAPI.GetWindowTextW(hWnd, title, title.Length);
                    var winName = title.ToString().Substring(0, ret);
                    var win = new VentanaEAE(winName, Tree1.SelectedNode.Text);
                    win.ShowDialog();
                }
                else
                {
                    HackeoEscritorio.EnviaraEscritorio(SystemInformation.VirtualScreen);
                }
            }
        }

        private void BtnSeparar_Click(object sender, EventArgs e)
        {
            HackeoEscritorio.ActualizarEscritorio(HackeoEscritorio.EstaSeparadoProgman());
            ActualizarV();
        }

        private void BtnOpciones_Click(object sender, EventArgs e)
        {
            Opciones op = new Opciones();
            op.ShowDialog();
        }

        private void BtnAyuda_Click(object sender, EventArgs e)
        {
            BtnAyuda.Capture = false;
            WinAPI.SendMessage(Handle, WinAPI.WM_SYSCOMMAND, WinAPI.SC_CONTEXTHELP, 0);
        }

        private void CheckAcrilico_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                DesenfoqueWin10.ActualizarDesenfoque(hWnd, !checkAcrilico.Checked, true);
                checkAcrilico.Checked = !checkAcrilico.Checked;
            }
        }

        private void CheckBasico_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                int valor;
                switch (checkBasico.CheckState)
                {
                    case CheckState.Unchecked:
                        valor = WinAPI.DWMNCRP_DISABLED;
                        WinAPI.DwmSetWindowAttribute(hWnd, WinAPI.NCRenderingPolicy, ref valor, sizeof(int));
                        checkBasico.CheckState = CheckState.Checked;
                        break;
                    case CheckState.Checked:
                        valor = WinAPI.DWMNCRP_USEWINDOWSTYLE;
                        WinAPI.DwmSetWindowAttribute(hWnd, WinAPI.NCRenderingPolicy, ref valor, sizeof(int));
                        checkBasico.CheckState = CheckState.Indeterminate;
                        break;
                    case CheckState.Indeterminate:
                        valor = WinAPI.DWMNCRP_ENABLED;
                        WinAPI.DwmSetWindowAttribute(hWnd, WinAPI.NCRenderingPolicy, ref valor, sizeof(int));
                        checkBasico.CheckState = CheckState.Unchecked;
                        break;
                }
            }
        }

        private void CheckClasico_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                /*try
                {*/
                WinAPI.GetWindowThreadProcessId(hWnd, out int pid);
                if (pid != Process.GetCurrentProcess().Id)
                {
                    CorrerenOtroProceso.SetThemeAppProperties(hWnd, !checkClasico.Checked);
                    WinAPI.DwmGetWindowAttribute(hWnd, WinAPI.NCRenderingEnabled, out bool valor, sizeof(int));
                    checkBasico.Checked = !valor;
                }
                else
                {
                    Application.VisualStyleState = !checkClasico.Checked ? System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled : System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
                    timer1.Start();
                }
                checkClasico.Checked = !checkClasico.Checked;
                /*}
                catch (Exception ex)
                {
                    MessageBox.Show(Aplicacion.vp, string.Format(Properties.frases.PrivAdmin_text, ex.Message, Text), string.Format(Properties.frases.MsgErrorClasico_text, Tree1.SelectedNode.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            WinAPI.DwmGetWindowAttribute(Handle, WinAPI.NCRenderingEnabled, out bool valor, sizeof(int));
            checkBasico.Checked = !valor;
            timer1.Stop();
        }

        private void CheckSinCapturas_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                try
                {
                    WinAPI.GetWindowThreadProcessId(hWnd, out int pid);
                    if (pid != Process.GetCurrentProcess().Id)
                    {
                        CorrerenOtroProceso.SetWindowDisplayAffinity(hWnd, !checkSinCapturas.Checked);
                    }
                    else
                    {
                        WinAPI.SetWindowDisplayAffinity(hWnd, (uint)(!checkSinCapturas.Checked ? 1 : 0));
                    }
                    checkSinCapturas.Checked = !checkSinCapturas.Checked;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Aplicacion.vp, string.Format(Properties.frases.PrivAdmin_text, ex.Message, Text), string.Format(Properties.frases.MsgErrorClasico_text, Tree1.SelectedNode.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Tree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IntPtr hWnd = (IntPtr)ColVentanas1.Keys.Where(x => ColVentanas1[x] == e.Node).Single();
            if (WinAPI.IsWindow(hWnd))
            {
                if (!Dragging)
                {
                    try
                    {
                        checkAcrilico.Checked = CorrerenOtroProceso.GetWindowCompositionAttribute(hWnd);
                        WinAPI.DwmGetWindowAttribute(hWnd, WinAPI.NCRenderingEnabled, out bool ret, sizeof(int));
                        checkBasico.Checked = !ret;
                        checkClasico.Checked = CorrerenOtroProceso.GetThemeAppProperties(hWnd);
                        checkSinCapturas.Checked = CorrerenOtroProceso.GetWindowDisplayAffinity(hWnd);
                        WinAPI.ExtendedWindowStyleFlags winExStyle = (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE);
                        checkCT.Checked = (winExStyle & WinAPI.ExtendedWindowStyleFlags.WS_EX_LAYERED) != 0 & (winExStyle & WinAPI.ExtendedWindowStyleFlags.WS_EX_TRANSPARENT) != 0;
                        checkCloak.Checked = false;
                        if (WinAPI.GetLayeredWindowAttributes(hWnd, out uint crKey, out byte bAlpha, out uint dwFlags) != false)
                        {
                            if (dwFlags != 1)
                            {
                                checkOp.Checked = true;
                                percentTrackBar1.Value = bAlpha;
                            }
                            else
                            {
                                checkOp.Checked = false;
                            }
                        }
                        else
                        {
                            checkOp.Checked = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        checkAcrilico.Checked = false;
                        checkBasico.Checked = false;
                        checkClasico.Checked = false;
                        checkSinCapturas.Checked = false;
                        checkCT.Checked = false;
                        checkCloak.Checked = false;
                        checkOp.Checked = false;
                        MessageBox.Show(Aplicacion.vp, string.Format(Properties.frases.PrivAdmin_text, ex.Message, Text), string.Format(Properties.frases.MsgErrorClasico_text, e.Node.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                ActualizarV();
            }
        }

        private void Remb_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                WinAPI.MARGINS margins = new WinAPI.MARGINS()
                {
                    leftWidth = 0,
                    rightWidth = 0,
                    topHeight = 0,
                    bottomHeight = 0
                };
                WinAPI.DwmExtendFrameIntoClientArea(hWnd, ref margins);
            }
        }

        private void CheckCT_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                if (hWnd != Handle)
                {
                    checkCT.Checked = !checkCT.Checked;
                    if (checkCT.Checked)
                    {
                        WinAPI.SetWindowLongPtr(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE, (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE) | WinAPI.ExtendedWindowStyleFlags.WS_EX_LAYERED | WinAPI.ExtendedWindowStyleFlags.WS_EX_TRANSPARENT);
                    }
                    else
                    {
                        WinAPI.SetWindowLongPtr(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE, (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE) & ~WinAPI.ExtendedWindowStyleFlags.WS_EX_LAYERED & ~WinAPI.ExtendedWindowStyleFlags.WS_EX_TRANSPARENT);
                    }
                }
            }
        }

        private void Addb_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                WinAPI.MARGINS margins = new WinAPI.MARGINS() { leftWidth = -1 };
                WinAPI.DwmExtendFrameIntoClientArea(hWnd, ref margins);
            }
        }

        private void Resetb_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                int valor = WinAPI.DWMNCRP_USEWINDOWSTYLE;
                WinAPI.DwmSetWindowAttribute(hWnd, WinAPI.NCRenderingPolicy, ref valor, sizeof(int));
                WinAPI.DwmGetWindowAttribute(hWnd, WinAPI.NCRenderingEnabled, out bool ret, sizeof(int));
                checkBasico.Checked = !ret;
            }
        }

        private void VentanaPrincipal_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            Tree1.ImageList.ImageSize = new Size(WinAPI.GetSystemMetricsForDpi(WinAPI.SM_CXSMICON, (uint)e.DeviceDpiNew), WinAPI.GetSystemMetricsForDpi(WinAPI.SM_CYSMICON, (uint)e.DeviceDpiNew));
            foreach (Bitmap bmp in BitmapList)
            {
                Tree1.ImageList.Images.Add(bmp);
            }
            MinimumSize = new Size(minSize.Width * (e.DeviceDpiNew / e.DeviceDpiOld), minSize.Height * (e.DeviceDpiNew / e.DeviceDpiOld));
            minSize = MinimumSize;
        }

        private void CheckCloak_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                try
                {
                    WinAPI.GetWindowThreadProcessId(hWnd, out int pid);
                    if (pid != Process.GetCurrentProcess().Id)
                    {
                        CorrerenOtroProceso.DwmSetWindowAttribute(hWnd, !checkCloak.Checked);
                        checkCloak.Checked = !checkCloak.Checked;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Aplicacion.vp, string.Format(Properties.frases.PrivAdmin_text, ex.Message, Text), string.Format(Properties.frases.MsgErrorClasico_text, Tree1.SelectedNode.Text), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PercentTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd) & checkOp.Checked)
            {
                WinAPI.SetLayeredWindowAttributes(hWnd, 0, (byte)percentTrackBar1.Value, 0x02);
            }
        }

        private void CheckOp_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                if (checkOp.Checked)
                {
                    WinAPI.SetLayeredWindowAttributes(hWnd, 0, 0xFF, 0x00);
                    WinAPI.SetWindowLongGeneric(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE, (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE) & ~WinAPI.ExtendedWindowStyleFlags.WS_EX_LAYERED);
                    checkOp.Checked = false;
                }
                else
                {
                    WinAPI.SetWindowLongGeneric(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE, (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE) | WinAPI.ExtendedWindowStyleFlags.WS_EX_LAYERED);
                    WinAPI.SetLayeredWindowAttributes(hWnd, 0, 0xFF, 0x02);
                    checkOp.Checked = true;
                }
            }
        }

        private void CheckOp_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkOp.Checked)
            {
                percentTrackBar1.Enabled = false;
                percentTrackBar1.Value = 0xFF;
            }
            else
            {
                percentTrackBar1.Enabled = true;
            }
        }

        private void CheckCT_CheckedChanged(object sender, EventArgs e)
        {
            checkOp.Checked = checkCT.Checked;
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                StringBuilder title = new StringBuilder(new string(' ', 256));
                int ret;
                string winName;
                ret = WinAPI.GetWindowTextW(hWnd, title, title.Length);
                winName = title.ToString().Substring(0, ret);
                WinAPI.GetClientRect(hWnd, out WinAPI.RECT lpRect);
                CompositionFX.CrearVentanadeCompositor(
                    winName + " (Ventana de composición de escritorio)",
                    IconodePrograma2(hWnd, false),
                    IconodePrograma2(hWnd, true),
                    new CompositionFX.SIZE(lpRect.Width, lpRect.Height),
                    1, new IntPtr[] { hWnd },
                    true,
                    false,
                    false,
                    false,
                    true);
                ActualizarV();
            }
        }

        private IntPtr IconodePrograma2(IntPtr hwnd, bool peq)
        {
            IntPtr iconHandle = IntPtr.Zero;
            WinAPI.SendMessageTimeoutW(hwnd, WinAPI.SystemMessages.WM_GETICON, peq ? WinAPI.MessageFlags.ICON_SMALL : WinAPI.MessageFlags.ICON_BIG, 0, WinAPI.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 300, out iconHandle);

            if (iconHandle == IntPtr.Zero & peq)
                WinAPI.SendMessageTimeoutW(hwnd, WinAPI.SystemMessages.WM_GETICON, WinAPI.MessageFlags.ICON_SMALL2, 0, WinAPI.SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 300, out iconHandle);
            if (iconHandle == IntPtr.Zero)
                iconHandle = WinAPI.GetClassLongPtrW(hwnd, peq ? WinAPI.GetClassLongFlags.GCL_HICONSM : WinAPI.GetClassLongFlags.GCL_HICON);
            return iconHandle;
        }

        private void ColorKeyBtn_Click(object sender, EventArgs e)
        {
            if (ObtenerHWND(out IntPtr hWnd))
            {
                DialogResult dlg = colorDialog1.ShowDialog(this);
                if (dlg == DialogResult.OK)
                {
                    Color col = colorDialog1.Color;
                    WinAPI.SetWindowLongGeneric(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE, (WinAPI.ExtendedWindowStyleFlags)WinAPI.GetWindowLongPtrW(hWnd, WinAPI.WindowLongFlags.GWL_EXSTYLE) | WinAPI.ExtendedWindowStyleFlags.WS_EX_LAYERED);
                    WinAPI.SetLayeredWindowAttributes(hWnd, (uint)col.ToArgb(), 0xFF, 0x01);
                }
            }
        }
    }
}
