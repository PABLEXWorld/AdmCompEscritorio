using System;
using System.Windows.Forms;

namespace AdmCompEscritorio
{
    partial class VentanaPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaPrincipal));
            this.BtnEnviarAEscritorio = new System.Windows.Forms.Button();
            this.Tree1 = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.BtnSeparar = new System.Windows.Forms.Button();
            this.BtnOpciones = new System.Windows.Forms.Button();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.BtnAyuda = new System.Windows.Forms.Button();
            this.grupoPropComposicion = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkAcrilico = new System.Windows.Forms.CheckBox();
            this.checkBasico = new System.Windows.Forms.CheckBox();
            this.checkClasico = new System.Windows.Forms.CheckBox();
            this.checkSinCapturas = new System.Windows.Forms.CheckBox();
            this.checkCT = new System.Windows.Forms.CheckBox();
            this.checkCloak = new System.Windows.Forms.CheckBox();
            this.remb = new System.Windows.Forms.Button();
            this.addb = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkOp = new System.Windows.Forms.CheckBox();
            this.ColorKeyBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.TestBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.percentTrackBar1 = new AdmCompEscritorio.PercentTrackBar();
            this.grupoPropComposicion.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnEnviarAEscritorio
            // 
            resources.ApplyResources(this.BtnEnviarAEscritorio, "BtnEnviarAEscritorio");
            this.BtnEnviarAEscritorio.Name = "BtnEnviarAEscritorio";
            this.BtnEnviarAEscritorio.UseCompatibleTextRendering = true;
            this.BtnEnviarAEscritorio.Click += new System.EventHandler(this.BtnEnviarAEscritorio_Click);
            // 
            // Tree1
            // 
            this.Tree1.AllowDrop = true;
            this.Tree1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Tree1, "Tree1");
            this.Tree1.HideSelection = false;
            this.Tree1.ImageList = this.imageList;
            this.Tree1.Name = "Tree1";
            this.Tree1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView_ItemDrag);
            this.Tree1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree1_AfterSelect);
            this.Tree1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_DragDrop);
            this.Tree1.DragOver += new System.Windows.Forms.DragEventHandler(this.Tree1_DragOver);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageList, "imageList");
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // BtnSeparar
            // 
            resources.ApplyResources(this.BtnSeparar, "BtnSeparar");
            this.BtnSeparar.Name = "BtnSeparar";
            this.BtnSeparar.UseCompatibleTextRendering = true;
            this.BtnSeparar.Click += new System.EventHandler(this.BtnSeparar_Click);
            // 
            // BtnOpciones
            // 
            resources.ApplyResources(this.BtnOpciones, "BtnOpciones");
            this.BtnOpciones.Name = "BtnOpciones";
            this.BtnOpciones.UseCompatibleTextRendering = true;
            this.BtnOpciones.Click += new System.EventHandler(this.BtnOpciones_Click);
            // 
            // BtnActualizar
            // 
            resources.ApplyResources(this.BtnActualizar, "BtnActualizar");
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.UseCompatibleTextRendering = true;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // BtnAyuda
            // 
            resources.ApplyResources(this.BtnAyuda, "BtnAyuda");
            this.BtnAyuda.Name = "BtnAyuda";
            this.BtnAyuda.UseCompatibleTextRendering = true;
            this.BtnAyuda.Click += new System.EventHandler(this.BtnAyuda_Click);
            // 
            // grupoPropComposicion
            // 
            resources.ApplyResources(this.grupoPropComposicion, "grupoPropComposicion");
            this.grupoPropComposicion.Controls.Add(this.flowLayoutPanel1);
            this.grupoPropComposicion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grupoPropComposicion.Name = "grupoPropComposicion";
            this.grupoPropComposicion.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkAcrilico);
            this.flowLayoutPanel1.Controls.Add(this.checkBasico);
            this.flowLayoutPanel1.Controls.Add(this.checkClasico);
            this.flowLayoutPanel1.Controls.Add(this.checkSinCapturas);
            this.flowLayoutPanel1.Controls.Add(this.checkCT);
            this.flowLayoutPanel1.Controls.Add(this.checkCloak);
            this.flowLayoutPanel1.Controls.Add(this.remb);
            this.flowLayoutPanel1.Controls.Add(this.addb);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.ColorKeyBtn);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // checkAcrilico
            // 
            this.checkAcrilico.AutoCheck = false;
            resources.ApplyResources(this.checkAcrilico, "checkAcrilico");
            this.checkAcrilico.Name = "checkAcrilico";
            this.checkAcrilico.UseVisualStyleBackColor = true;
            this.checkAcrilico.Click += new System.EventHandler(this.CheckAcrilico_Click);
            // 
            // checkBasico
            // 
            this.checkBasico.AutoCheck = false;
            resources.ApplyResources(this.checkBasico, "checkBasico");
            this.checkBasico.Name = "checkBasico";
            this.checkBasico.ThreeState = true;
            this.checkBasico.UseVisualStyleBackColor = true;
            this.checkBasico.Click += new System.EventHandler(this.CheckBasico_Click);
            // 
            // checkClasico
            // 
            this.checkClasico.AutoCheck = false;
            resources.ApplyResources(this.checkClasico, "checkClasico");
            this.checkClasico.Name = "checkClasico";
            this.checkClasico.UseVisualStyleBackColor = true;
            this.checkClasico.Click += new System.EventHandler(this.CheckClasico_Click);
            // 
            // checkSinCapturas
            // 
            this.checkSinCapturas.AutoCheck = false;
            resources.ApplyResources(this.checkSinCapturas, "checkSinCapturas");
            this.checkSinCapturas.Name = "checkSinCapturas";
            this.checkSinCapturas.UseVisualStyleBackColor = true;
            this.checkSinCapturas.Click += new System.EventHandler(this.CheckSinCapturas_Click);
            // 
            // checkCT
            // 
            this.checkCT.AutoCheck = false;
            resources.ApplyResources(this.checkCT, "checkCT");
            this.checkCT.Name = "checkCT";
            this.checkCT.UseVisualStyleBackColor = true;
            this.checkCT.CheckedChanged += new System.EventHandler(this.CheckCT_CheckedChanged);
            this.checkCT.Click += new System.EventHandler(this.CheckCT_Click);
            // 
            // checkCloak
            // 
            this.checkCloak.AutoCheck = false;
            resources.ApplyResources(this.checkCloak, "checkCloak");
            this.checkCloak.Name = "checkCloak";
            this.checkCloak.UseVisualStyleBackColor = true;
            this.checkCloak.Click += new System.EventHandler(this.CheckCloak_Click);
            // 
            // remb
            // 
            resources.ApplyResources(this.remb, "remb");
            this.remb.Name = "remb";
            this.remb.UseVisualStyleBackColor = true;
            this.remb.Click += new System.EventHandler(this.Remb_Click);
            // 
            // addb
            // 
            resources.ApplyResources(this.addb, "addb");
            this.addb.Name = "addb";
            this.addb.UseVisualStyleBackColor = true;
            this.addb.Click += new System.EventHandler(this.Addb_Click);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.checkOp, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.percentTrackBar1, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // checkOp
            // 
            resources.ApplyResources(this.checkOp, "checkOp");
            this.checkOp.AutoCheck = false;
            this.checkOp.Name = "checkOp";
            this.checkOp.UseVisualStyleBackColor = true;
            this.checkOp.CheckedChanged += new System.EventHandler(this.CheckOp_CheckedChanged);
            this.checkOp.Click += new System.EventHandler(this.CheckOp_Click);
            // 
            // ColorKeyBtn
            // 
            resources.ApplyResources(this.ColorKeyBtn, "ColorKeyBtn");
            this.ColorKeyBtn.Name = "ColorKeyBtn";
            this.ColorKeyBtn.UseVisualStyleBackColor = true;
            this.ColorKeyBtn.Click += new System.EventHandler(this.ColorKeyBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.BtnActualizar);
            this.flowLayoutPanel3.Controls.Add(this.BtnEnviarAEscritorio);
            this.flowLayoutPanel3.Controls.Add(this.BtnSeparar);
            this.flowLayoutPanel3.Controls.Add(this.BtnOpciones);
            this.flowLayoutPanel3.Controls.Add(this.BtnAyuda);
            this.flowLayoutPanel3.Controls.Add(this.TestBtn);
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // TestBtn
            // 
            resources.ApplyResources(this.TestBtn, "TestBtn");
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.UseCompatibleTextRendering = true;
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.Tree1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grupoPropComposicion, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // percentTrackBar1
            // 
            resources.ApplyResources(this.percentTrackBar1, "percentTrackBar1");
            this.percentTrackBar1.Maximum = 255;
            this.percentTrackBar1.Minimum = 0;
            this.percentTrackBar1.Name = "percentTrackBar1";
            this.percentTrackBar1.Value = 255;
            this.percentTrackBar1.ValueChanged += new System.EventHandler(this.PercentTrackBar1_ValueChanged);
            // 
            // VentanaPrincipal
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "VentanaPrincipal";
            this.Shown += new System.EventHandler(this.VentanaPrincipal_Shown);
            this.grupoPropComposicion.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button BtnEnviarAEscritorio;
        private System.Windows.Forms.TreeView Tree1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button BtnSeparar;
        private Button BtnOpciones;
        private Button BtnActualizar;
        private Button BtnAyuda;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaPrincipal));
        private GroupBox grupoPropComposicion;
        private CheckBox checkSinCapturas;
        private CheckBox checkClasico;
        private CheckBox checkBasico;
        private CheckBox checkAcrilico;
        private Timer timer1;
        private Button remb;
        private CheckBox checkCT;
        private Button addb;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox checkCloak;
        private PercentTrackBar percentTrackBar1;
        private CheckBox checkOp;
        private TableLayoutPanel tableLayoutPanel3;
        private Button TestBtn;
        private Button ColorKeyBtn;
        private ColorDialog colorDialog1;
    }
}