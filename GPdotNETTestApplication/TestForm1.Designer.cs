namespace GPdotNETTestApplication
{
    partial class TestForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components=null;

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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.wpfTreeDrawerCtrl1 = new gpWpfTreeDrawerLib.wpfTreeDrawerCtrl();
            this.button1 = new System.Windows.Forms.Button();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.wpfTreeDrawerCtrl2 = new gpWpfTreeDrawerLib.wpfTreeDrawerCtrl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelParent2 = new System.Windows.Forms.Label();
            this.labelParent1 = new System.Windows.Forms.Label();
            this.elementHost4 = new System.Windows.Forms.Integration.ElementHost();
            this.wpfTreeDrawerCtrl4 = new gpWpfTreeDrawerLib.wpfTreeDrawerCtrl();
            this.labelOffspring2 = new System.Windows.Forms.Label();
            this.elementHost3 = new System.Windows.Forms.Integration.ElementHost();
            this.wpfTreeDrawerCtrl3 = new gpWpfTreeDrawerLib.wpfTreeDrawerCtrl();
            this.labelOffspring1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(3, 23);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(320, 155);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.wpfTreeDrawerCtrl1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(526, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Test Crossover";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // elementHost2
            // 
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(329, 23);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(321, 155);
            this.elementHost2.TabIndex = 2;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.wpfTreeDrawerCtrl2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelParent2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelParent1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.elementHost4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelOffspring2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.elementHost3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.elementHost1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelOffspring1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.elementHost2, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(653, 362);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // labelParent2
            // 
            this.labelParent2.AutoSize = true;
            this.labelParent2.Location = new System.Drawing.Point(329, 0);
            this.labelParent2.Name = "labelParent2";
            this.labelParent2.Size = new System.Drawing.Size(44, 13);
            this.labelParent2.TabIndex = 7;
            this.labelParent2.Text = "Parent2";
            // 
            // labelParent1
            // 
            this.labelParent1.AutoSize = true;
            this.labelParent1.Location = new System.Drawing.Point(3, 0);
            this.labelParent1.Name = "labelParent1";
            this.labelParent1.Size = new System.Drawing.Size(50, 13);
            this.labelParent1.TabIndex = 7;
            this.labelParent1.Text = "Parent 1:";
            // 
            // elementHost4
            // 
            this.elementHost4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost4.Location = new System.Drawing.Point(329, 204);
            this.elementHost4.Name = "elementHost4";
            this.elementHost4.Size = new System.Drawing.Size(321, 155);
            this.elementHost4.TabIndex = 4;
            this.elementHost4.Text = "elementHost4";
            this.elementHost4.Child = this.wpfTreeDrawerCtrl4;
            // 
            // labelOffspring2
            // 
            this.labelOffspring2.AutoSize = true;
            this.labelOffspring2.Location = new System.Drawing.Point(329, 181);
            this.labelOffspring2.Name = "labelOffspring2";
            this.labelOffspring2.Size = new System.Drawing.Size(55, 13);
            this.labelOffspring2.TabIndex = 1;
            this.labelOffspring2.Text = "Offspring2";
            // 
            // elementHost3
            // 
            this.elementHost3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost3.Location = new System.Drawing.Point(3, 204);
            this.elementHost3.Name = "elementHost3";
            this.elementHost3.Size = new System.Drawing.Size(320, 155);
            this.elementHost3.TabIndex = 3;
            this.elementHost3.Text = "elementHost3";
            this.elementHost3.Child = this.wpfTreeDrawerCtrl3;
            // 
            // labelOffspring1
            // 
            this.labelOffspring1.AutoSize = true;
            this.labelOffspring1.Location = new System.Drawing.Point(3, 181);
            this.labelOffspring1.Name = "labelOffspring1";
            this.labelOffspring1.Size = new System.Drawing.Size(58, 13);
            this.labelOffspring1.TabIndex = 0;
            this.labelOffspring1.Text = "Offspring 1";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(405, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Test Mutation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(284, 379);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Test Shrink Mutation";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(38, 379);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Test Permutation";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(163, 379);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Test Hoist Mutation";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // TestForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 416);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Name = "TestForm1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TestForm1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private gpWpfTreeDrawerLib.wpfTreeDrawerCtrl wpfTreeDrawerCtrl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private gpWpfTreeDrawerLib.wpfTreeDrawerCtrl wpfTreeDrawerCtrl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelOffspring2;
        private System.Windows.Forms.Label labelOffspring1;
        private System.Windows.Forms.Integration.ElementHost elementHost4;
        private gpWpfTreeDrawerLib.wpfTreeDrawerCtrl wpfTreeDrawerCtrl4;
        private System.Windows.Forms.Integration.ElementHost elementHost3;
        private gpWpfTreeDrawerLib.wpfTreeDrawerCtrl wpfTreeDrawerCtrl3;
        private System.Windows.Forms.Label labelParent2;
        private System.Windows.Forms.Label labelParent1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

