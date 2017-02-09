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
            button1.Click -= button1_Click;
            listView1.Click -= listView1_Click;
            
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
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // eDuration
            // 
            this.eb_durationInMin.Margin = new System.Windows.Forms.Padding(5);
            // 
            // eTimeleft
            // 
            this.eb_timeleft.Margin = new System.Windows.Forms.Padding(5);
            // 
            // eTimeToCompleate
            // 
            this.eb_timeToCompleate.Margin = new System.Windows.Forms.Padding(5);
            // 
            // eTimePerRun
            // 
            this.eb_timePerRun.Margin = new System.Windows.Forms.Padding(5);
            // 
            // eTimeStart
            // 
            this.eb_timeStart.Margin = new System.Windows.Forms.Padding(5);
            // 
            // bestFitnessAtGenerationEditBox
            // 
            this.eb_bestSolutionFound.Margin = new System.Windows.Forms.Padding(5);
            // 
            // textBox3
            // 
            this.eb_maximumFitness.Margin = new System.Windows.Forms.Padding(5);
            this.eb_maximumFitness.Visible = false;
            // 
            // label31
            // 
            this.label31.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.Size = new System.Drawing.Size(179, 24);
            // 
            // currentErrorBox
            // 
            this.eb_currentFitness.Margin = new System.Windows.Forms.Padding(5);
            // 
            // brojIteracija
            // 
            this.m_eb_iterations.Margin = new System.Windows.Forms.Padding(5);
            // 
            // currentIteration
            // 
            this.eb_currentIteration.Margin = new System.Windows.Forms.Padding(5);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 396);
            // 
            // zedFitness
            // 
            this.zedFitness.Location = new System.Drawing.Point(353, 16);
            this.zedFitness.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.zedFitness.Size = new System.Drawing.Size(268, 194);
            // 
            // chkOptimumType
            // 
            this.chkOptimumType.Visible = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(353, 218);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(267, 127);
            this.listView1.TabIndex = 19;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 357);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Min:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(379, 353);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 22);
            this.textBox1.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(485, 353);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(64, 22);
            this.textBox2.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 357);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Max:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 348);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 28);
            this.button1.TabIndex = 24;
            this.button1.Text = "update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OptimizePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "OptimizePanel";
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.zedFitness, 0);
            this.Controls.SetChildIndex(this.groupBox7, 0);
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



    }
}
