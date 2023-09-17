using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using wpf = System.Windows;

namespace AdmCompEscritorio
{
    public partial class VentanaEAE : Form
    {
        private RadioButton selectedrb;
        private string windowName;
        internal List<VentanaIdentificarMonitor> listaVentanas = new List<VentanaIdentificarMonitor>();

        internal VentanaEAE(string wName, string fallbackName)
        {
            if (wName != String.Empty)
            {
                windowName = wName;
            } else
            {
                windowName = fallbackName;
            }
            InitializeComponent();
            selectedrb = bRPantallaPrimaria;
            label1.Text = string.Format(label1.Text, windowName);
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = Screen.AllScreens.Length;
            label2.Text = string.Format(label2.Text, 1, Screen.AllScreens.Length);
        }
        
        void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb.Checked)
            {
                selectedrb = rb;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listaVentanas.Count == 0)
            {
                for (int i = 0; i < Screen.AllScreens.Length; i++)
                {
                    var sc = Screen.AllScreens[i];
                    var rectRes = new wpf.Rect(new wpf.Point(sc.Bounds.X, sc.Bounds.Y), new wpf.Size(sc.Bounds.Width, sc.Bounds.Height));
                    var rectwa = new wpf.Rect(new wpf.Point(sc.WorkingArea.X, sc.WorkingArea.Y), new wpf.Size(sc.WorkingArea.Width, sc.WorkingArea.Height));
                    var testWin = new VentanaIdentificarMonitor(i + 1, rectRes, rectwa, this)
                    {
                        ShowActivated = false
                    };
                    listaVentanas.Add(testWin);
                    testWin.Show();
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (selectedrb == bRPantallaPrimaria)
            {
                HackeoEscritorio.EnviaraEscritorio(Screen.PrimaryScreen.Bounds);
            }
            else if (selectedrb == bRPantallaEspecifica)
            {
                HackeoEscritorio.EnviaraEscritorio(Screen.AllScreens[(int)numericUpDown1.Value - 1].Bounds);
            }
            else
            {
                HackeoEscritorio.EnviaraEscritorio(SystemInformation.VirtualScreen);
            }
            Close();
        }

        private void VentanaEAE_Shown(object sender, EventArgs e)
        {
            bRTodaslasPantallas.Font = new Font(bRPantallaPrimaria.Font.FontFamily, bRPantallaPrimaria.Font.Size, FontStyle.Bold);
            foreach (Control ctrl in flowLayoutPanel2.Controls)
            {
                ctrl.Size = ctrl.PreferredSize;
            }
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                ctrl.Size = ctrl.PreferredSize;
            }
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                ctrl.Size = ctrl.PreferredSize;
            }
            foreach (Control ctrl in Controls)
            {
                ctrl.Size = ctrl.PreferredSize;
            }
            Size = PreferredSize;
        }

        private void VentanaEAE_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var ven in listaVentanas)
            {
                ven.Close();
            }
        }

        private void bRPantallaPrimaria_FontChanged(object sender, EventArgs e)
        {
            bRTodaslasPantallas.Font = new Font(bRPantallaPrimaria.Font.FontFamily, bRPantallaPrimaria.Font.Size, FontStyle.Bold);
        }

        private void bRTodaslasPantallas_SizeChanged(object sender, EventArgs e)
        {
            bRTodaslasPantallas.Size = bRTodaslasPantallas.PreferredSize;
        }
    }
}
