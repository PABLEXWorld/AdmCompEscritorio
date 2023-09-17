namespace AdmCompEscritorio
{
    partial class Opciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opciones));
            this.button1 = new System.Windows.Forms.Button();
            this.BorrarBorde = new System.Windows.Forms.CheckBox();
            this.AgregarBorde = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BorrarBorde
            // 
            this.BorrarBorde.Checked = true;
            this.BorrarBorde.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.BorrarBorde, "BorrarBorde");
            this.BorrarBorde.Name = "BorrarBorde";
            this.BorrarBorde.UseVisualStyleBackColor = true;
            this.BorrarBorde.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // AgregarBorde
            // 
            this.AgregarBorde.Checked = true;
            this.AgregarBorde.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.AgregarBorde, "AgregarBorde");
            this.AgregarBorde.Name = "AgregarBorde";
            this.AgregarBorde.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.AgregarBorde);
            this.flowLayoutPanel1.Controls.Add(this.BorrarBorde);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // Opciones
            // 
            this.AcceptButton = this.button1;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Opciones";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.Opciones_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox BorrarBorde;
        private System.Windows.Forms.CheckBox AgregarBorde;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}