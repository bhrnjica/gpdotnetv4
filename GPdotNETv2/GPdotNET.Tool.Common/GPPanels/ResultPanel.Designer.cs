namespace GPdotNET.Tool.Common
{
    partial class ResultPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.enooptMatematickiModel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.treeCtrlDrawer1 = new GPdotNET.Tool.Common.TreeCtrlDrawer();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(3, 18);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(97, 116);
            this.listView1.TabIndex = 26;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(100, 13);
            this.label41.TabIndex = 27;
            this.label41.Text = "Random Constants:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label42.Location = new System.Drawing.Point(115, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(105, 13);
            this.label42.TabIndex = 28;
            this.label42.Text = "GPModel (Tree form)";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(12, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "GPModel(Simple text)";
            // 
            // enooptMatematickiModel
            // 
            this.enooptMatematickiModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.enooptMatematickiModel.Location = new System.Drawing.Point(3, 165);
            this.enooptMatematickiModel.Multiline = true;
            this.enooptMatematickiModel.Name = "enooptMatematickiModel";
            this.enooptMatematickiModel.ReadOnly = true;
            this.enooptMatematickiModel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.enooptMatematickiModel.Size = new System.Drawing.Size(397, 65);
            this.enooptMatematickiModel.TabIndex = 31;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(319, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 24);
            this.button1.TabIndex = 32;
            this.button1.Text = "Save to png ...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeCtrlDrawer1
            // 
            this.treeCtrlDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCtrlDrawer1.AutoScroll = true;
            this.treeCtrlDrawer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.treeCtrlDrawer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeCtrlDrawer1.Location = new System.Drawing.Point(106, 21);
            this.treeCtrlDrawer1.Name = "treeCtrlDrawer1";
            this.treeCtrlDrawer1.Size = new System.Drawing.Size(294, 113);
            this.treeCtrlDrawer1.TabIndex = 29;
            // 
            // ResultPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.enooptMatematickiModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeCtrlDrawer1);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.listView1);
            this.Name = "ResultPanel";
            this.Size = new System.Drawing.Size(403, 233);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private TreeCtrlDrawer treeCtrlDrawer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox enooptMatematickiModel;
        private System.Windows.Forms.Button button1;

    }
}
