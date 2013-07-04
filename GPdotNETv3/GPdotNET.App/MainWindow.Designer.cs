namespace GPdotNET.App
{
    partial class MainWindow
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.ribbonPanel = new System.Windows.Forms.Panel();
            this.gpModelPanel = new GPdotNET.Tool.Common.GUI.CustomPanel();
            this.rbtnCloseModel = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnSaveModel = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnOpenModel = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnNewModel = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.gpExportPanel = new GPdotNET.Tool.Common.GUI.CustomPanel();
            this.rbtnExportTest = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnExportModel = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.gpModellingPanel = new GPdotNET.Tool.Common.GUI.CustomPanel();
            this.rbtnOptimize = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnStop = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnRun = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.gpCommonPanel = new GPdotNET.Tool.Common.GUI.CustomPanel();
            this.rbtnExit = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnInfo = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLabel15 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel14 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel12 = new System.Windows.Forms.LinkLabel();
            this.linkLabel11 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel13 = new System.Windows.Forms.LinkLabel();
            this.linkLabel9 = new System.Windows.Forms.LinkLabel();
            this.linkLabel10 = new System.Windows.Forms.LinkLabel();
            this.linkLabel8 = new System.Windows.Forms.LinkLabel();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.pbLink3 = new System.Windows.Forms.PictureBox();
            this.pbLink2 = new System.Windows.Forms.PictureBox();
            this.pbLink1 = new System.Windows.Forms.PictureBox();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.pbOpen = new System.Windows.Forms.PictureBox();
            this.pbNew = new System.Windows.Forms.PictureBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbSep2 = new System.Windows.Forms.PictureBox();
            this.pbSep1 = new System.Windows.Forms.PictureBox();
            this.pbLogoHor = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.ribbonPanel.SuspendLayout();
            this.gpModelPanel.SuspendLayout();
            this.gpExportPanel.SuspendLayout();
            this.gpModellingPanel.SuspendLayout();
            this.gpCommonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSep2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSep1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogoHor)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.txtStatusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 628);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 15, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1082, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 20);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // txtStatusMessage
            // 
            this.txtStatusMessage.Name = "txtStatusMessage";
            this.txtStatusMessage.Size = new System.Drawing.Size(50, 20);
            this.txtStatusMessage.Text = "Ready";
            // 
            // ribbonPanel
            // 
            this.ribbonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonPanel.Controls.Add(this.gpModelPanel);
            this.ribbonPanel.Controls.Add(this.gpExportPanel);
            this.ribbonPanel.Controls.Add(this.gpModellingPanel);
            this.ribbonPanel.Controls.Add(this.gpCommonPanel);
            this.ribbonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonPanel.Location = new System.Drawing.Point(0, 0);
            this.ribbonPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbonPanel.Name = "ribbonPanel";
            this.ribbonPanel.Size = new System.Drawing.Size(1082, 100);
            this.ribbonPanel.TabIndex = 9;
            // 
            // gpModelPanel
            // 
            this.gpModelPanel.Caption = "GP Model";
            this.gpModelPanel.Controls.Add(this.rbtnCloseModel);
            this.gpModelPanel.Controls.Add(this.rbtnSaveModel);
            this.gpModelPanel.Controls.Add(this.rbtnOpenModel);
            this.gpModelPanel.Controls.Add(this.rbtnNewModel);
            this.gpModelPanel.Location = new System.Drawing.Point(2, 2);
            this.gpModelPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpModelPanel.Name = "gpModelPanel";
            this.gpModelPanel.Size = new System.Drawing.Size(326, 100);
            this.gpModelPanel.TabIndex = 6;
            // 
            // rbtnCloseModel
            // 
            this.rbtnCloseModel.BackColor = System.Drawing.Color.Transparent;
            this.rbtnCloseModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnCloseModel.filename = null;
            this.rbtnCloseModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnCloseModel.FlatAppearance.BorderSize = 0;
            this.rbtnCloseModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnCloseModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnCloseModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnCloseModel.folder = null;
            this.rbtnCloseModel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnCloseModel.img = null;
            this.rbtnCloseModel.img_back = null;
            this.rbtnCloseModel.img_click = null;
            this.rbtnCloseModel.img_on = null;
            this.rbtnCloseModel.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnCloseModel.InfoComment = "";
            this.rbtnCloseModel.InfoImage = "";
            this.rbtnCloseModel.InfoTitle = "";
            this.rbtnCloseModel.IsToolTipEnabled = false;
            this.rbtnCloseModel.Location = new System.Drawing.Point(245, 9);
            this.rbtnCloseModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnCloseModel.Name = "rbtnCloseModel";
            this.rbtnCloseModel.Size = new System.Drawing.Size(63, 64);
            this.rbtnCloseModel.TabIndex = 3;
            this.rbtnCloseModel.Text = "Close";
            this.rbtnCloseModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnCloseModel.UseVisualStyleBackColor = true;
            // 
            // rbtnSaveModel
            // 
            this.rbtnSaveModel.BackColor = System.Drawing.Color.Transparent;
            this.rbtnSaveModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnSaveModel.filename = null;
            this.rbtnSaveModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnSaveModel.FlatAppearance.BorderSize = 0;
            this.rbtnSaveModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnSaveModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnSaveModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnSaveModel.folder = null;
            this.rbtnSaveModel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnSaveModel.img = null;
            this.rbtnSaveModel.img_back = null;
            this.rbtnSaveModel.img_click = null;
            this.rbtnSaveModel.img_on = null;
            this.rbtnSaveModel.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnSaveModel.InfoComment = "";
            this.rbtnSaveModel.InfoImage = "";
            this.rbtnSaveModel.InfoTitle = "";
            this.rbtnSaveModel.IsToolTipEnabled = false;
            this.rbtnSaveModel.Location = new System.Drawing.Point(165, 9);
            this.rbtnSaveModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnSaveModel.Name = "rbtnSaveModel";
            this.rbtnSaveModel.Size = new System.Drawing.Size(63, 64);
            this.rbtnSaveModel.TabIndex = 2;
            this.rbtnSaveModel.Text = "Save";
            this.rbtnSaveModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnSaveModel.UseVisualStyleBackColor = true;
            // 
            // rbtnOpenModel
            // 
            this.rbtnOpenModel.BackColor = System.Drawing.Color.Transparent;
            this.rbtnOpenModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnOpenModel.filename = null;
            this.rbtnOpenModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnOpenModel.FlatAppearance.BorderSize = 0;
            this.rbtnOpenModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnOpenModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnOpenModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnOpenModel.folder = null;
            this.rbtnOpenModel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnOpenModel.img = null;
            this.rbtnOpenModel.img_back = null;
            this.rbtnOpenModel.img_click = null;
            this.rbtnOpenModel.img_on = null;
            this.rbtnOpenModel.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnOpenModel.InfoComment = "";
            this.rbtnOpenModel.InfoImage = "";
            this.rbtnOpenModel.InfoTitle = "";
            this.rbtnOpenModel.IsToolTipEnabled = false;
            this.rbtnOpenModel.Location = new System.Drawing.Point(88, 9);
            this.rbtnOpenModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnOpenModel.Name = "rbtnOpenModel";
            this.rbtnOpenModel.Size = new System.Drawing.Size(63, 64);
            this.rbtnOpenModel.TabIndex = 1;
            this.rbtnOpenModel.Text = "Open";
            this.rbtnOpenModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnOpenModel.UseVisualStyleBackColor = true;
            // 
            // rbtnNewModel
            // 
            this.rbtnNewModel.BackColor = System.Drawing.Color.Transparent;
            this.rbtnNewModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnNewModel.filename = null;
            this.rbtnNewModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnNewModel.FlatAppearance.BorderSize = 0;
            this.rbtnNewModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnNewModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnNewModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnNewModel.folder = null;
            this.rbtnNewModel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnNewModel.img = null;
            this.rbtnNewModel.img_back = null;
            this.rbtnNewModel.img_click = null;
            this.rbtnNewModel.img_on = null;
            this.rbtnNewModel.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnNewModel.InfoComment = "";
            this.rbtnNewModel.InfoImage = "";
            this.rbtnNewModel.InfoTitle = "";
            this.rbtnNewModel.IsToolTipEnabled = true;
            this.rbtnNewModel.Location = new System.Drawing.Point(3, 9);
            this.rbtnNewModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnNewModel.Name = "rbtnNewModel";
            this.rbtnNewModel.Size = new System.Drawing.Size(63, 64);
            this.rbtnNewModel.TabIndex = 0;
            this.rbtnNewModel.Text = "New";
            this.rbtnNewModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnNewModel.UseVisualStyleBackColor = false;
            // 
            // gpExportPanel
            // 
            this.gpExportPanel.Caption = "Export GP result";
            this.gpExportPanel.Controls.Add(this.rbtnExportTest);
            this.gpExportPanel.Controls.Add(this.rbtnExportModel);
            this.gpExportPanel.Location = new System.Drawing.Point(579, 2);
            this.gpExportPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpExportPanel.Name = "gpExportPanel";
            this.gpExportPanel.Size = new System.Drawing.Size(164, 102);
            this.gpExportPanel.TabIndex = 8;
            // 
            // rbtnExportTest
            // 
            this.rbtnExportTest.BackColor = System.Drawing.Color.Transparent;
            this.rbtnExportTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnExportTest.filename = null;
            this.rbtnExportTest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExportTest.FlatAppearance.BorderSize = 0;
            this.rbtnExportTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExportTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExportTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnExportTest.folder = null;
            this.rbtnExportTest.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnExportTest.img = null;
            this.rbtnExportTest.img_back = null;
            this.rbtnExportTest.img_click = null;
            this.rbtnExportTest.img_on = null;
            this.rbtnExportTest.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnExportTest.InfoComment = "";
            this.rbtnExportTest.InfoImage = "";
            this.rbtnExportTest.InfoTitle = "";
            this.rbtnExportTest.IsToolTipEnabled = false;
            this.rbtnExportTest.Location = new System.Drawing.Point(88, 9);
            this.rbtnExportTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnExportTest.Name = "rbtnExportTest";
            this.rbtnExportTest.Size = new System.Drawing.Size(63, 64);
            this.rbtnExportTest.TabIndex = 2;
            this.rbtnExportTest.Text = "Test";
            this.rbtnExportTest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnExportTest.UseVisualStyleBackColor = true;
            // 
            // rbtnExportModel
            // 
            this.rbtnExportModel.BackColor = System.Drawing.Color.Transparent;
            this.rbtnExportModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnExportModel.filename = null;
            this.rbtnExportModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExportModel.FlatAppearance.BorderSize = 0;
            this.rbtnExportModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExportModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExportModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnExportModel.folder = null;
            this.rbtnExportModel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnExportModel.img = null;
            this.rbtnExportModel.img_back = null;
            this.rbtnExportModel.img_click = null;
            this.rbtnExportModel.img_on = null;
            this.rbtnExportModel.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnExportModel.InfoComment = "";
            this.rbtnExportModel.InfoImage = "";
            this.rbtnExportModel.InfoTitle = "";
            this.rbtnExportModel.IsToolTipEnabled = false;
            this.rbtnExportModel.Location = new System.Drawing.Point(11, 9);
            this.rbtnExportModel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnExportModel.Name = "rbtnExportModel";
            this.rbtnExportModel.Size = new System.Drawing.Size(63, 64);
            this.rbtnExportModel.TabIndex = 1;
            this.rbtnExportModel.Text = "Model";
            this.rbtnExportModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnExportModel.UseVisualStyleBackColor = true;
            // 
            // gpModellingPanel
            // 
            this.gpModellingPanel.Caption = "GP Modelling";
            this.gpModellingPanel.Controls.Add(this.rbtnOptimize);
            this.gpModellingPanel.Controls.Add(this.rbtnStop);
            this.gpModellingPanel.Controls.Add(this.rbtnRun);
            this.gpModellingPanel.Location = new System.Drawing.Point(331, 2);
            this.gpModellingPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpModellingPanel.Name = "gpModellingPanel";
            this.gpModellingPanel.Size = new System.Drawing.Size(245, 100);
            this.gpModellingPanel.TabIndex = 7;
            // 
            // rbtnOptimize
            // 
            this.rbtnOptimize.BackColor = System.Drawing.Color.Transparent;
            this.rbtnOptimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnOptimize.filename = null;
            this.rbtnOptimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnOptimize.FlatAppearance.BorderSize = 0;
            this.rbtnOptimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnOptimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnOptimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnOptimize.folder = null;
            this.rbtnOptimize.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnOptimize.img = null;
            this.rbtnOptimize.img_back = null;
            this.rbtnOptimize.img_click = null;
            this.rbtnOptimize.img_on = null;
            this.rbtnOptimize.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnOptimize.InfoComment = "";
            this.rbtnOptimize.InfoImage = "";
            this.rbtnOptimize.InfoTitle = "";
            this.rbtnOptimize.IsToolTipEnabled = false;
            this.rbtnOptimize.Location = new System.Drawing.Point(165, 9);
            this.rbtnOptimize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnOptimize.Name = "rbtnOptimize";
            this.rbtnOptimize.Size = new System.Drawing.Size(63, 64);
            this.rbtnOptimize.TabIndex = 4;
            this.rbtnOptimize.Text = "Optimize";
            this.rbtnOptimize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnOptimize.UseVisualStyleBackColor = true;
            this.rbtnOptimize.Click += new System.EventHandler(this.optimizeGP);
            // 
            // rbtnStop
            // 
            this.rbtnStop.BackColor = System.Drawing.Color.Transparent;
            this.rbtnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnStop.Enabled = false;
            this.rbtnStop.filename = null;
            this.rbtnStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnStop.FlatAppearance.BorderSize = 0;
            this.rbtnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnStop.folder = null;
            this.rbtnStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnStop.img = null;
            this.rbtnStop.img_back = null;
            this.rbtnStop.img_click = null;
            this.rbtnStop.img_on = null;
            this.rbtnStop.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnStop.InfoComment = "";
            this.rbtnStop.InfoImage = "";
            this.rbtnStop.InfoTitle = "";
            this.rbtnStop.IsToolTipEnabled = false;
            this.rbtnStop.Location = new System.Drawing.Point(88, 9);
            this.rbtnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnStop.Name = "rbtnStop";
            this.rbtnStop.Size = new System.Drawing.Size(63, 64);
            this.rbtnStop.TabIndex = 3;
            this.rbtnStop.Text = "Stop";
            this.rbtnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnStop.UseVisualStyleBackColor = true;
            this.rbtnStop.Click += new System.EventHandler(this.stopGP);
            // 
            // rbtnRun
            // 
            this.rbtnRun.BackColor = System.Drawing.Color.Transparent;
            this.rbtnRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnRun.filename = null;
            this.rbtnRun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnRun.FlatAppearance.BorderSize = 0;
            this.rbtnRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnRun.folder = null;
            this.rbtnRun.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnRun.img = null;
            this.rbtnRun.img_back = null;
            this.rbtnRun.img_click = null;
            this.rbtnRun.img_on = null;
            this.rbtnRun.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnRun.InfoComment = "";
            this.rbtnRun.InfoImage = "";
            this.rbtnRun.InfoTitle = "";
            this.rbtnRun.IsToolTipEnabled = false;
            this.rbtnRun.Location = new System.Drawing.Point(3, 9);
            this.rbtnRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnRun.Name = "rbtnRun";
            this.rbtnRun.Size = new System.Drawing.Size(63, 64);
            this.rbtnRun.TabIndex = 2;
            this.rbtnRun.Text = "Run";
            this.rbtnRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnRun.UseVisualStyleBackColor = true;
            this.rbtnRun.Click += new System.EventHandler(this.run_GP);
            // 
            // gpCommonPanel
            // 
            this.gpCommonPanel.Caption = "Common";
            this.gpCommonPanel.Controls.Add(this.rbtnExit);
            this.gpCommonPanel.Controls.Add(this.rbtnInfo);
            this.gpCommonPanel.Location = new System.Drawing.Point(749, 2);
            this.gpCommonPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gpCommonPanel.Name = "gpCommonPanel";
            this.gpCommonPanel.Size = new System.Drawing.Size(164, 100);
            this.gpCommonPanel.TabIndex = 10;
            // 
            // rbtnExit
            // 
            this.rbtnExit.BackColor = System.Drawing.Color.Transparent;
            this.rbtnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnExit.filename = null;
            this.rbtnExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExit.FlatAppearance.BorderSize = 0;
            this.rbtnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnExit.folder = null;
            this.rbtnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnExit.img = null;
            this.rbtnExit.img_back = null;
            this.rbtnExit.img_click = null;
            this.rbtnExit.img_on = null;
            this.rbtnExit.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnExit.InfoComment = "";
            this.rbtnExit.InfoImage = "";
            this.rbtnExit.InfoTitle = "";
            this.rbtnExit.IsToolTipEnabled = false;
            this.rbtnExit.Location = new System.Drawing.Point(90, 11);
            this.rbtnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnExit.Name = "rbtnExit";
            this.rbtnExit.Size = new System.Drawing.Size(63, 64);
            this.rbtnExit.TabIndex = 4;
            this.rbtnExit.Text = "Exit";
            this.rbtnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnExit.UseVisualStyleBackColor = true;
            this.rbtnExit.Click += new System.EventHandler(this.rbtnExit_Click);
            // 
            // rbtnInfo
            // 
            this.rbtnInfo.BackColor = System.Drawing.Color.Transparent;
            this.rbtnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnInfo.filename = null;
            this.rbtnInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnInfo.FlatAppearance.BorderSize = 0;
            this.rbtnInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnInfo.folder = null;
            this.rbtnInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnInfo.img = null;
            this.rbtnInfo.img_back = null;
            this.rbtnInfo.img_click = null;
            this.rbtnInfo.img_on = null;
            this.rbtnInfo.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnInfo.InfoComment = "";
            this.rbtnInfo.InfoImage = "";
            this.rbtnInfo.InfoTitle = "";
            this.rbtnInfo.IsToolTipEnabled = false;
            this.rbtnInfo.Location = new System.Drawing.Point(3, 11);
            this.rbtnInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnInfo.Name = "rbtnInfo";
            this.rbtnInfo.Size = new System.Drawing.Size(63, 64);
            this.rbtnInfo.TabIndex = 3;
            this.rbtnInfo.Text = "Info";
            this.rbtnInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnInfo.UseVisualStyleBackColor = true;
            this.rbtnInfo.Click += new System.EventHandler(this.rbtnInfo_Click);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logoPictureBox.Location = new System.Drawing.Point(0, 104);
            this.logoPictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(101, 510);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.linkLabel15);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.linkLabel14);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.linkLabel12);
            this.panel2.Controls.Add(this.linkLabel11);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.linkLabel13);
            this.panel2.Controls.Add(this.linkLabel9);
            this.panel2.Controls.Add(this.linkLabel10);
            this.panel2.Controls.Add(this.linkLabel8);
            this.panel2.Controls.Add(this.linkLabel7);
            this.panel2.Controls.Add(this.pbLink3);
            this.panel2.Controls.Add(this.pbLink2);
            this.panel2.Controls.Add(this.pbLink1);
            this.panel2.Controls.Add(this.linkLabel4);
            this.panel2.Controls.Add(this.linkLabel5);
            this.panel2.Controls.Add(this.linkLabel6);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.pbOpen);
            this.panel2.Controls.Add(this.pbNew);
            this.panel2.Controls.Add(this.linkLabel3);
            this.panel2.Controls.Add(this.linkLabel2);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pbSep2);
            this.panel2.Controls.Add(this.pbSep1);
            this.panel2.Controls.Add(this.pbLogoHor);
            this.panel2.Location = new System.Drawing.Point(105, 104);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(977, 510);
            this.panel2.TabIndex = 14;
            // 
            // linkLabel15
            // 
            this.linkLabel15.AutoSize = true;
            this.linkLabel15.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel15.Location = new System.Drawing.Point(222, 406);
            this.linkLabel15.Name = "linkLabel15";
            this.linkLabel15.Size = new System.Drawing.Size(65, 19);
            this.linkLabel15.TabIndex = 33;
            this.linkLabel15.TabStop = true;
            this.linkLabel15.Text = "Sample 1";
            this.linkLabel15.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel15_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label7.Location = new System.Drawing.Point(225, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Transportation problems";
            // 
            // linkLabel14
            // 
            this.linkLabel14.AutoSize = true;
            this.linkLabel14.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel14.Location = new System.Drawing.Point(20, 406);
            this.linkLabel14.Name = "linkLabel14";
            this.linkLabel14.Size = new System.Drawing.Size(61, 19);
            this.linkLabel14.TabIndex = 31;
            this.linkLabel14.TabStop = true;
            this.linkLabel14.Text = "Sample1";
            this.linkLabel14.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel14_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label2.Location = new System.Drawing.Point(17, 368);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Assignment problems";
            // 
            // linkLabel12
            // 
            this.linkLabel12.AutoSize = true;
            this.linkLabel12.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel12.Location = new System.Drawing.Point(447, 288);
            this.linkLabel12.Name = "linkLabel12";
            this.linkLabel12.Size = new System.Drawing.Size(96, 19);
            this.linkLabel12.TabIndex = 29;
            this.linkLabel12.TabStop = true;
            this.linkLabel12.Text = "20 Cities Path";
            this.linkLabel12.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel12_LinkClicked);
            // 
            // linkLabel11
            // 
            this.linkLabel11.AutoSize = true;
            this.linkLabel11.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel11.Location = new System.Drawing.Point(620, 313);
            this.linkLabel11.Name = "linkLabel11";
            this.linkLabel11.Size = new System.Drawing.Size(127, 19);
            this.linkLabel11.TabIndex = 28;
            this.linkLabel11.TabStop = true;
            this.linkLabel11.Text = "MSFT Stock Quote";
            this.linkLabel11.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.microsoft_StockModeling);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(616, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Time Series Modeling";
            // 
            // linkLabel13
            // 
            this.linkLabel13.AutoSize = true;
            this.linkLabel13.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel13.Location = new System.Drawing.Point(447, 313);
            this.linkLabel13.Name = "linkLabel13";
            this.linkLabel13.Size = new System.Drawing.Size(100, 19);
            this.linkLabel13.TabIndex = 26;
            this.linkLabel13.TabStop = true;
            this.linkLabel13.Text = "131 Cities Path";
            this.linkLabel13.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel13_LinkClicked);
            // 
            // linkLabel9
            // 
            this.linkLabel9.AutoSize = true;
            this.linkLabel9.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel9.Location = new System.Drawing.Point(224, 313);
            this.linkLabel9.Name = "linkLabel9";
            this.linkLabel9.Size = new System.Drawing.Size(171, 19);
            this.linkLabel9.TabIndex = 24;
            this.linkLabel9.TabStop = true;
            this.linkLabel9.Text = "CNC Param. Optimization";
            this.linkLabel9.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cnc_params_Optimization);
            // 
            // linkLabel10
            // 
            this.linkLabel10.AutoSize = true;
            this.linkLabel10.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel10.Location = new System.Drawing.Point(224, 288);
            this.linkLabel10.Name = "linkLabel10";
            this.linkLabel10.Size = new System.Drawing.Size(114, 19);
            this.linkLabel10.TabIndex = 23;
            this.linkLabel10.TabStop = true;
            this.linkLabel10.Text = "Global Optimum";
            this.linkLabel10.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.global_Optimization);
            // 
            // linkLabel8
            // 
            this.linkLabel8.AutoSize = true;
            this.linkLabel8.Location = new System.Drawing.Point(79, 98);
            this.linkLabel8.Name = "linkLabel8";
            this.linkLabel8.Size = new System.Drawing.Size(58, 20);
            this.linkLabel8.TabIndex = 21;
            this.linkLabel8.TabStop = true;
            this.linkLabel8.Text = "Open...";
            this.linkLabel8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openLink_Clicked);
            // 
            // linkLabel7
            // 
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.Location = new System.Drawing.Point(79, 42);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(51, 20);
            this.linkLabel7.TabIndex = 20;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "New...";
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.newLink_Clicked);
            // 
            // pbLink3
            // 
            this.pbLink3.Location = new System.Drawing.Point(605, 113);
            this.pbLink3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLink3.Name = "pbLink3";
            this.pbLink3.Size = new System.Drawing.Size(33, 31);
            this.pbLink3.TabIndex = 19;
            this.pbLink3.TabStop = false;
            // 
            // pbLink2
            // 
            this.pbLink2.Location = new System.Drawing.Point(605, 81);
            this.pbLink2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLink2.Name = "pbLink2";
            this.pbLink2.Size = new System.Drawing.Size(33, 31);
            this.pbLink2.TabIndex = 18;
            this.pbLink2.TabStop = false;
            // 
            // pbLink1
            // 
            this.pbLink1.Location = new System.Drawing.Point(605, 48);
            this.pbLink1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLink1.Name = "pbLink1";
            this.pbLink1.Size = new System.Drawing.Size(33, 31);
            this.pbLink1.TabIndex = 17;
            this.pbLink1.TabStop = false;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel4.Location = new System.Drawing.Point(643, 128);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(166, 19);
            this.linkLabel4.TabIndex = 16;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "GPdotNET v2 User Guide";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel5.Location = new System.Drawing.Point(643, 95);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(106, 19);
            this.linkLabel5.TabIndex = 15;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "GPdotNET Blog";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel6.Location = new System.Drawing.Point(643, 64);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(115, 19);
            this.linkLabel6.TabIndex = 14;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "GPdotNET portal";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.portalLinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label6.Location = new System.Drawing.Point(602, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 28);
            this.label6.TabIndex = 13;
            this.label6.Text = "Links";
            // 
            // pbOpen
            // 
            this.pbOpen.Location = new System.Drawing.Point(21, 74);
            this.pbOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbOpen.Name = "pbOpen";
            this.pbOpen.Size = new System.Drawing.Size(51, 51);
            this.pbOpen.TabIndex = 12;
            this.pbOpen.TabStop = false;
            // 
            // pbNew
            // 
            this.pbNew.Location = new System.Drawing.Point(21, 18);
            this.pbNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbNew.Name = "pbNew";
            this.pbNew.Size = new System.Drawing.Size(51, 51);
            this.pbNew.TabIndex = 11;
            this.pbNew.TabStop = false;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel3.Location = new System.Drawing.Point(20, 313);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(202, 19);
            this.linkLabel3.TabIndex = 10;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Surface Roughness predictions";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.surfaceRoughnessPrediction_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel2.Location = new System.Drawing.Point(20, 288);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(103, 19);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Weld Hardness";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.weldHardness_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel1.Location = new System.Drawing.Point(20, 266);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(85, 19);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Simple Case";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.simple_fun_mod_Clicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label5.Location = new System.Drawing.Point(443, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "TSP Solver";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label4.Location = new System.Drawing.Point(222, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Modeling && Optimization";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label3.Location = new System.Drawing.Point(18, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Modeling&&Predictions";
            // 
            // pbSep2
            // 
            this.pbSep2.Location = new System.Drawing.Point(147, 74);
            this.pbSep2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbSep2.Name = "pbSep2";
            this.pbSep2.Size = new System.Drawing.Size(15, 51);
            this.pbSep2.TabIndex = 4;
            this.pbSep2.TabStop = false;
            // 
            // pbSep1
            // 
            this.pbSep1.Location = new System.Drawing.Point(147, 18);
            this.pbSep1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbSep1.Name = "pbSep1";
            this.pbSep1.Size = new System.Drawing.Size(15, 51);
            this.pbSep1.TabIndex = 3;
            this.pbSep1.TabStop = false;
            // 
            // pbLogoHor
            // 
            this.pbLogoHor.Location = new System.Drawing.Point(210, 18);
            this.pbLogoHor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLogoHor.Name = "pbLogoHor";
            this.pbLogoHor.Size = new System.Drawing.Size(389, 106);
            this.pbLogoHor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLogoHor.TabIndex = 2;
            this.pbLogoHor.TabStop = false;
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(1082, 653);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.ribbonPanel);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GPdotNET";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GPdotNetApp_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GPdotNETApp_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.GPdotNetApp_DragOver);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ribbonPanel.ResumeLayout(false);
            this.gpModelPanel.ResumeLayout(false);
            this.gpExportPanel.ResumeLayout(false);
            this.gpModellingPanel.ResumeLayout(false);
            this.gpCommonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSep2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSep1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogoHor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Tool.Common.GUI.CustomPanel gpModelPanel;
        private Tool.Common.GUI.CustomButton rbtnNewModel;
        private Tool.Common.GUI.CustomPanel gpModellingPanel;
        private Tool.Common.GUI.CustomPanel gpExportPanel;
        private System.Windows.Forms.Panel ribbonPanel;
        private Tool.Common.GUI.CustomButton rbtnCloseModel;
        private Tool.Common.GUI.CustomButton rbtnSaveModel;
        private Tool.Common.GUI.CustomButton rbtnOpenModel;
        private Tool.Common.GUI.CustomButton rbtnExportTest;
        private Tool.Common.GUI.CustomButton rbtnExportModel;
        private Tool.Common.GUI.CustomPanel gpCommonPanel;
        private Tool.Common.GUI.CustomButton rbtnExit;
        private Tool.Common.GUI.CustomButton rbtnInfo;
        private Tool.Common.GUI.CustomButton rbtnOptimize;
        private Tool.Common.GUI.CustomButton rbtnStop;
        private Tool.Common.GUI.CustomButton rbtnRun;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbSep2;
        private System.Windows.Forms.PictureBox pbSep1;
        private System.Windows.Forms.PictureBox pbLogoHor;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pbLink3;
        private System.Windows.Forms.PictureBox pbLink2;
        private System.Windows.Forms.PictureBox pbLink1;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbOpen;
        private System.Windows.Forms.PictureBox pbNew;
        private System.Windows.Forms.LinkLabel linkLabel8;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.LinkLabel linkLabel13;
        private System.Windows.Forms.LinkLabel linkLabel9;
        private System.Windows.Forms.LinkLabel linkLabel10;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatusMessage;
        private System.Windows.Forms.LinkLabel linkLabel12;
        private System.Windows.Forms.LinkLabel linkLabel11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel14;
        private System.Windows.Forms.Label label2;

    }
}