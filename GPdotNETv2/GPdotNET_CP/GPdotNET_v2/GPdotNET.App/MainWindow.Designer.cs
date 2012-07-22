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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.gpCommonPanel = new GPdotNET.Tool.Common.GUI.CustomPanel();
            this.rbtnExit = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.rbtnInfo = new GPdotNET.Tool.Common.GUI.CustomButton();
            this.gpModelPanel = new GPdotNET.Tool.Common.GUI.CustomPanel();
            this.rbtnSaveAsModel = new GPdotNET.Tool.Common.GUI.CustomButton();
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.gpCommonPanel.SuspendLayout();
            this.gpModelPanel.SuspendLayout();
            this.gpExportPanel.SuspendLayout();
            this.gpModellingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(133, 111);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(828, 480);
            this.tabControl1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(964, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gpCommonPanel);
            this.panel1.Controls.Add(this.gpModelPanel);
            this.panel1.Controls.Add(this.gpExportPanel);
            this.panel1.Controls.Add(this.gpModellingPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 105);
            this.panel1.TabIndex = 9;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Location = new System.Drawing.Point(0, 111);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(130, 480);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // gpCommonPanel
            // 
            this.gpCommonPanel.Caption = "Common";
            this.gpCommonPanel.Controls.Add(this.rbtnExit);
            this.gpCommonPanel.Controls.Add(this.rbtnInfo);
            this.gpCommonPanel.Location = new System.Drawing.Point(710, 4);
            this.gpCommonPanel.Name = "gpCommonPanel";
            this.gpCommonPanel.Size = new System.Drawing.Size(204, 100);
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
            this.rbtnExit.Location = new System.Drawing.Point(79, 9);
            this.rbtnExit.Name = "rbtnExit";
            this.rbtnExit.Size = new System.Drawing.Size(64, 68);
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
            this.rbtnInfo.Location = new System.Drawing.Point(8, 9);
            this.rbtnInfo.Name = "rbtnInfo";
            this.rbtnInfo.Size = new System.Drawing.Size(64, 68);
            this.rbtnInfo.TabIndex = 3;
            this.rbtnInfo.Text = "Info";
            this.rbtnInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnInfo.UseVisualStyleBackColor = true;
            this.rbtnInfo.Click += new System.EventHandler(this.rbtnInfo_Click);
            // 
            // gpModelPanel
            // 
            this.gpModelPanel.Caption = "GP Model";
            this.gpModelPanel.Controls.Add(this.rbtnSaveAsModel);
            this.gpModelPanel.Controls.Add(this.rbtnSaveModel);
            this.gpModelPanel.Controls.Add(this.rbtnOpenModel);
            this.gpModelPanel.Controls.Add(this.rbtnNewModel);
            this.gpModelPanel.Location = new System.Drawing.Point(10, 3);
            this.gpModelPanel.Name = "gpModelPanel";
            this.gpModelPanel.Size = new System.Drawing.Size(293, 100);
            this.gpModelPanel.TabIndex = 6;
            // 
            // rbtnSaveAsModel
            // 
            this.rbtnSaveAsModel.BackColor = System.Drawing.Color.Transparent;
            this.rbtnSaveAsModel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnSaveAsModel.filename = null;
            this.rbtnSaveAsModel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnSaveAsModel.FlatAppearance.BorderSize = 0;
            this.rbtnSaveAsModel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnSaveAsModel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rbtnSaveAsModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnSaveAsModel.folder = null;
            this.rbtnSaveAsModel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rbtnSaveAsModel.img = null;
            this.rbtnSaveAsModel.img_back = null;
            this.rbtnSaveAsModel.img_click = null;
            this.rbtnSaveAsModel.img_on = null;
            this.rbtnSaveAsModel.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.rbtnSaveAsModel.InfoComment = "";
            this.rbtnSaveAsModel.InfoImage = "";
            this.rbtnSaveAsModel.InfoTitle = "";
            this.rbtnSaveAsModel.IsToolTipEnabled = false;
            this.rbtnSaveAsModel.Location = new System.Drawing.Point(221, 9);
            this.rbtnSaveAsModel.Name = "rbtnSaveAsModel";
            this.rbtnSaveAsModel.Size = new System.Drawing.Size(64, 68);
            this.rbtnSaveAsModel.TabIndex = 3;
            this.rbtnSaveAsModel.Text = "Save As";
            this.rbtnSaveAsModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnSaveAsModel.UseVisualStyleBackColor = true;
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
            this.rbtnSaveModel.Location = new System.Drawing.Point(149, 9);
            this.rbtnSaveModel.Name = "rbtnSaveModel";
            this.rbtnSaveModel.Size = new System.Drawing.Size(64, 68);
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
            this.rbtnOpenModel.Location = new System.Drawing.Point(77, 9);
            this.rbtnOpenModel.Name = "rbtnOpenModel";
            this.rbtnOpenModel.Size = new System.Drawing.Size(64, 68);
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
            this.rbtnNewModel.IsToolTipEnabled = false;
            this.rbtnNewModel.Location = new System.Drawing.Point(5, 9);
            this.rbtnNewModel.Name = "rbtnNewModel";
            this.rbtnNewModel.Size = new System.Drawing.Size(64, 68);
            this.rbtnNewModel.TabIndex = 0;
            this.rbtnNewModel.Text = "New";
            this.rbtnNewModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnNewModel.UseVisualStyleBackColor = true;
            
            // 
            // gpExportPanel
            // 
            this.gpExportPanel.Caption = "Export GP result";
            this.gpExportPanel.Controls.Add(this.rbtnExportTest);
            this.gpExportPanel.Controls.Add(this.rbtnExportModel);
            this.gpExportPanel.Location = new System.Drawing.Point(559, 3);
            this.gpExportPanel.Name = "gpExportPanel";
            this.gpExportPanel.Size = new System.Drawing.Size(147, 100);
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
            this.rbtnExportTest.Location = new System.Drawing.Point(76, 8);
            this.rbtnExportTest.Name = "rbtnExportTest";
            this.rbtnExportTest.Size = new System.Drawing.Size(64, 68);
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
            this.rbtnExportModel.Location = new System.Drawing.Point(5, 9);
            this.rbtnExportModel.Name = "rbtnExportModel";
            this.rbtnExportModel.Size = new System.Drawing.Size(64, 68);
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
            this.gpModellingPanel.Location = new System.Drawing.Point(311, 3);
            this.gpModellingPanel.Name = "gpModellingPanel";
            this.gpModellingPanel.Size = new System.Drawing.Size(242, 100);
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
            this.rbtnOptimize.Location = new System.Drawing.Point(175, 10);
            this.rbtnOptimize.Name = "rbtnOptimize";
            this.rbtnOptimize.Size = new System.Drawing.Size(64, 68);
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
            this.rbtnStop.Location = new System.Drawing.Point(89, 10);
            this.rbtnStop.Name = "rbtnStop";
            this.rbtnStop.Size = new System.Drawing.Size(64, 68);
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
            this.rbtnRun.Location = new System.Drawing.Point(3, 10);
            this.rbtnRun.Name = "rbtnRun";
            this.rbtnRun.Size = new System.Drawing.Size(64, 68);
            this.rbtnRun.TabIndex = 2;
            this.rbtnRun.Text = "Run";
            this.rbtnRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.rbtnRun.UseVisualStyleBackColor = true;
            this.rbtnRun.Click += new System.EventHandler(this.run_GP);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(964, 616);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GPdotNET";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.gpCommonPanel.ResumeLayout(false);
            this.gpModelPanel.ResumeLayout(false);
            this.gpExportPanel.ResumeLayout(false);
            this.gpModellingPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
        private Tool.Common.GUI.CustomButton rbtnSaveAsModel;
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

        

    }
}