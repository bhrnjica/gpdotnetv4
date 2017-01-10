namespace GPdotNET.Tool.Common
{
    partial class ExperimentPanel
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
            //this.btnLoadTesting.Click -= btnLoadTrainig_Click;
            //this.btnLoadTesting.Click -= btnLoadTesting_Click;
            //this.btnSetToGP.Click -= btnLoadTesting_Click;

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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnSetToGP = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.presentigeRadio = new System.Windows.Forms.RadioButton();
            this.numberRadio = new System.Windows.Forms.RadioButton();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.numberRadio);
            this.groupBox5.Controls.Add(this.presentigeRadio);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.numericUpDown1);
            this.groupBox5.Location = new System.Drawing.Point(4, 251);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(506, 89);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select last ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(111, 29);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(60, 26);
            this.numericUpDown1.TabIndex = 14;
            // 
            // btnSetToGP
            // 
            this.btnSetToGP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetToGP.Enabled = false;
            this.btnSetToGP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSetToGP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSetToGP.Location = new System.Drawing.Point(533, 295);
            this.btnSetToGP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSetToGP.Name = "btnSetToGP";
            this.btnSetToGP.Size = new System.Drawing.Size(170, 45);
            this.btnSetToGP.TabIndex = 3;
            this.btnSetToGP.Text = "Start Modeling";
            this.btnSetToGP.UseVisualStyleBackColor = true;
            this.btnSetToGP.Click += new System.EventHandler(this.btnSetupToModel_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(4, 5);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(698, 236);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(533, 251);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 38);
            this.button1.TabIndex = 27;
            this.button1.Text = "Load data...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // presentigeRadio
            // 
            this.presentigeRadio.AutoSize = true;
            this.presentigeRadio.Checked = true;
            this.presentigeRadio.Location = new System.Drawing.Point(176, 17);
            this.presentigeRadio.Name = "presentigeRadio";
            this.presentigeRadio.Size = new System.Drawing.Size(187, 24);
            this.presentigeRadio.TabIndex = 16;
            this.presentigeRadio.TabStop = true;
            this.presentigeRadio.Text = "% for testing. (0-50%)";
            this.presentigeRadio.UseVisualStyleBackColor = true;
            // 
            // numberRadio
            // 
            this.numberRadio.AutoSize = true;
            this.numberRadio.Location = new System.Drawing.Point(176, 47);
            this.numberRadio.Name = "numberRadio";
            this.numberRadio.Size = new System.Drawing.Size(209, 24);
            this.numberRadio.TabIndex = 17;
            this.numberRadio.Text = "# for testing. (0-n/2 rows)";
            this.numberRadio.UseVisualStyleBackColor = true;
            // 
            // ExperimentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSetToGP);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox5);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ExperimentPanel";
            this.Size = new System.Drawing.Size(714, 352);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSetToGP;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton numberRadio;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton presentigeRadio;
    }
}
