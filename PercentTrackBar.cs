using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AdmCompEscritorio
{
    public partial class PercentTrackBar : UserControl
    {
        public new bool Enabled
        {
            get
            {
                return groupBox1.Enabled;
            }
            set
            {
                groupBox1.Enabled = value;
            }
        }
        public int Minimum
        {
            get
            {
                return trackBar1.Minimum;
            }
            set
            {
                trackBar1.Minimum = value;
            }
        }
        public int Maximum
        {
            get
            {
                return trackBar1.Maximum;
            } set
            {
                trackBar1.Maximum = value;
            }
        }
        public int Value
        {
            get
            {
                return trackBar1.Value;
            }
            set
            {
                trackBar1.Value = value;
            }
        }
        public int Percent
        {
            get
            {
                return GetPercent();
            }
        }

        string format;
        public PercentTrackBar()
        {
            InitializeComponent();
            format = label1.Text;
            UpdateText();
        }

        [Browsable(true)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                groupBox1.Text = value;
            }
        }

        public event EventHandler ValueChanged;
        public event EventHandler PercentChanged;
        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        private void OnPercentChanged()
        {
            PercentChanged?.Invoke(this, EventArgs.Empty);
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            UpdateText();
            OnValueChanged();
        }
        private void UpdateText()
        {
            label1.Text = string.Format(format, GetPercent());
        }
        private int GetPercent()
        {
            return (int)(((float)trackBar1.Value - (float)trackBar1.Minimum) / ((float)trackBar1.Maximum - (float)trackBar1.Minimum) * 100f);
        }
        private void label1_TextChanged(object sender, EventArgs e)
        {
            OnPercentChanged();
        }
    }
}
