namespace GPdotNET.Tool.Common
{
    partial class OptimizePanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkTypeOfOptimization = new System.Windows.Forms.CheckBox();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkTypeOfOptimization);
            this.groupBox7.Controls.SetChildIndex(this.chkTypeOfOptimization, 0);
            this.groupBox7.Controls.SetChildIndex(this.label20, 0);
            this.groupBox7.Controls.SetChildIndex(this.currentIterationBox, 0);
            this.groupBox7.Controls.SetChildIndex(this.brojIteracija, 0);
            this.groupBox7.Controls.SetChildIndex(this.currentErrorBox, 0);
            this.groupBox7.Controls.SetChildIndex(this.label21, 0);
            this.groupBox7.Controls.SetChildIndex(this.comboBox2, 0);
            this.groupBox7.Controls.SetChildIndex(this.label31, 0);
            this.groupBox7.Controls.SetChildIndex(this.textBox3, 0);
            this.groupBox7.Controls.SetChildIndex(this.label32, 0);
            this.groupBox7.Controls.SetChildIndex(this.label33, 0);
            this.groupBox7.Controls.SetChildIndex(this.bestFitnessAtGenerationEditBox, 0);
            this.groupBox7.Controls.SetChildIndex(this.label9, 0);
            this.groupBox7.Controls.SetChildIndex(this.label14, 0);
            this.groupBox7.Controls.SetChildIndex(this.label18, 0);
            this.groupBox7.Controls.SetChildIndex(this.eTimeStart, 0);
            this.groupBox7.Controls.SetChildIndex(this.eTimePerRun, 0);
            this.groupBox7.Controls.SetChildIndex(this.eTimeToCompleate, 0);
            this.groupBox7.Controls.SetChildIndex(this.label43, 0);
            this.groupBox7.Controls.SetChildIndex(this.eTimeleft, 0);
            this.groupBox7.Controls.SetChildIndex(this.label44, 0);
            this.groupBox7.Controls.SetChildIndex(this.eDuration, 0);
            // 
            // textBox3
            // 
            this.textBox3.Visible = false;
            // 
            // label31
            // 
            this.label31.Visible = false;
            // 
            // zedFitness
            // 
            this.zedFitness.Location = new System.Drawing.Point(265, 13);
            this.zedFitness.Size = new System.Drawing.Size(201, 158);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(265, 177);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(201, 104);
            this.listView1.TabIndex = 19;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Min:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(284, 315);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(49, 20);
            this.textBox1.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(364, 315);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(49, 20);
            this.textBox2.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Min:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(417, 314);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkTypeOfOptimization
            // 
            this.chkTypeOfOptimization.AutoSize = true;
            this.chkTypeOfOptimization.Location = new System.Drawing.Point(150, 11);
            this.chkTypeOfOptimization.Name = "chkTypeOfOptimization";
            this.chkTypeOfOptimization.Size = new System.Drawing.Size(67, 17);
            this.chkTypeOfOptimization.TabIndex = 36;
            this.chkTypeOfOptimization.Text = "Minimum";
            this.chkTypeOfOptimization.UseVisualStyleBackColor = true;
            // 
            // OptimizePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Name = "OptimizePanel";
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.zedFitness, 0);
            this.Controls.SetChildIndex(this.groupBox7, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.textBox2, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkTypeOfOptimization;



    }
}
