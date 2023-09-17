using System;
using System.Windows.Forms;
using AdmCompEscritorio.Properties;

namespace AdmCompEscritorio
{
    public partial class Opciones : Form
    {
        Settings settings = Settings.Default;

        public Opciones()
        {
            InitializeComponent();
            BorrarBorde.Checked = settings.BorrarBorde;
            AgregarBorde.Checked = settings.AgregarBorde;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == BorrarBorde)
            {
                settings.BorrarBorde = (sender as CheckBox).Checked;
            }
            else
            {
                settings.AgregarBorde = (sender as CheckBox).Checked;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            settings.Save();
            Close();
        }

        private void Opciones_Shown(object sender, EventArgs e)
        {
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
    }
}
