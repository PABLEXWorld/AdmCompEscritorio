namespace AdmCompEscritorio
{
    partial class VentanaEAE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaEAE));
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bRTodaslasPantallas = new System.Windows.Forms.RadioButton();
            this.bRPantallaEspecifica = new System.Windows.Forms.RadioButton();
            this.bRPantallaPrimaria = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // bRTodaslasPantallas
            // 
            resources.ApplyResources(this.bRTodaslasPantallas, "bRTodaslasPantallas");
            this.bRTodaslasPantallas.Name = "bRTodaslasPantallas";
            this.bRTodaslasPantallas.UseVisualStyleBackColor = true;
            this.bRTodaslasPantallas.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.bRTodaslasPantallas.SizeChanged += new System.EventHandler(this.bRTodaslasPantallas_SizeChanged);
            // 
            // bRPantallaEspecifica
            // 
            resources.ApplyResources(this.bRPantallaEspecifica, "bRPantallaEspecifica");
            this.bRPantallaEspecifica.Name = "bRPantallaEspecifica";
            this.bRPantallaEspecifica.UseVisualStyleBackColor = true;
            this.bRPantallaEspecifica.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // bRPantallaPrimaria
            // 
            resources.ApplyResources(this.bRPantallaPrimaria, "bRPantallaPrimaria");
            this.bRPantallaPrimaria.Checked = true;
            this.bRPantallaPrimaria.Name = "bRPantallaPrimaria";
            this.bRPantallaPrimaria.TabStop = true;
            this.bRPantallaPrimaria.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.bRPantallaPrimaria.FontChanged += new System.EventHandler(this.bRPantallaPrimaria_FontChanged);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.bRPantallaPrimaria);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.bRTodaslasPantallas);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.bRPantallaEspecifica);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDown1);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // VentanaEAE
            // 
            this.AcceptButton = this.button2;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VentanaEAE";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VentanaEAE_FormClosed);
            this.Shown += new System.EventHandler(this.VentanaEAE_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton bRTodaslasPantallas;
        private System.Windows.Forms.RadioButton bRPantallaEspecifica;
        private System.Windows.Forms.RadioButton bRPantallaPrimaria;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}