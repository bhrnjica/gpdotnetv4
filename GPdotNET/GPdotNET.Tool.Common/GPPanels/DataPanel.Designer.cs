namespace GPdotNET.Tool.Common
{
    partial class DataPanel
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
            this.btnLoadTesting.Click -= btnLoadTrainig_Click;
            this.btnLoadTesting.Click -= btnLoadTesting_Click;
            this.btnSetToGP.Click -= btnLoadTesting_Click;

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
            this.label47 = new System.Windows.Forms.Label();
            this.txtNrVariables = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtNrTestSeries = new System.Windows.Forms.TextBox();
            this.btnSetToGP = new System.Windows.Forms.Button();
            this.txtNrSeries = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.btnLoadTesting = new System.Windows.Forms.Button();
            this.btnLoadTraining = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label47
            // 
            this.label47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label47.Location = new System.Drawing.Point(9, 80);
            this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(125, 22);
            this.label47.TabIndex = 28;
            this.label47.Tag = "";
            this.label47.Text = "Nr. Series for test:";
            // 
            // txtNrVariables
            // 
            this.txtNrVariables.Location = new System.Drawing.Point(143, 48);
            this.txtNrVariables.Margin = new System.Windows.Forms.Padding(4);
            this.txtNrVariables.Name = "txtNrVariables";
            this.txtNrVariables.Size = new System.Drawing.Size(73, 22);
            this.txtNrVariables.TabIndex = 27;
            // 
            // label46
            // 
            this.label46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label46.Location = new System.Drawing.Point(35, 52);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(112, 22);
            this.label46.TabIndex = 26;
            this.label46.Tag = "";
            this.label46.Text = "Nr. Variables:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtNrTestSeries);
            this.groupBox5.Controls.Add(this.btnSetToGP);
            this.groupBox5.Controls.Add(this.label47);
            this.groupBox5.Controls.Add(this.txtNrVariables);
            this.groupBox5.Controls.Add(this.label46);
            this.groupBox5.Controls.Add(this.txtNrSeries);
            this.groupBox5.Controls.Add(this.label45);
            this.groupBox5.Location = new System.Drawing.Point(4, 174);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(352, 107);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Settings";
            this.groupBox5.Visible = false;
            // 
            // txtNrTestSeries
            // 
            this.txtNrTestSeries.Location = new System.Drawing.Point(143, 76);
            this.txtNrTestSeries.Margin = new System.Windows.Forms.Padding(4);
            this.txtNrTestSeries.Name = "txtNrTestSeries";
            this.txtNrTestSeries.Size = new System.Drawing.Size(73, 22);
            this.txtNrTestSeries.TabIndex = 29;
            // 
            // btnSetToGP
            // 
            this.btnSetToGP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetToGP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSetToGP.Location = new System.Drawing.Point(228, 71);
            this.btnSetToGP.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetToGP.Name = "btnSetToGP";
            this.btnSetToGP.Size = new System.Drawing.Size(100, 28);
            this.btnSetToGP.TabIndex = 3;
            this.btnSetToGP.Text = "Set to GP";
            this.btnSetToGP.UseVisualStyleBackColor = true;
            this.btnSetToGP.Click += new System.EventHandler(this.btnSetToGP_Click);
            // 
            // txtNrSeries
            // 
            this.txtNrSeries.Location = new System.Drawing.Point(143, 16);
            this.txtNrSeries.Margin = new System.Windows.Forms.Padding(4);
            this.txtNrSeries.Name = "txtNrSeries";
            this.txtNrSeries.ReadOnly = true;
            this.txtNrSeries.Size = new System.Drawing.Size(73, 22);
            this.txtNrSeries.TabIndex = 25;
            // 
            // label45
            // 
            this.label45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label45.Location = new System.Drawing.Point(60, 20);
            this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(75, 22);
            this.label45.TabIndex = 24;
            this.label45.Tag = "";
            this.label45.Text = "Nr.Serie:";
            // 
            // btnLoadTesting
            // 
            this.btnLoadTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTesting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadTesting.Location = new System.Drawing.Point(376, 248);
            this.btnLoadTesting.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadTesting.Name = "btnLoadTesting";
            this.btnLoadTesting.Size = new System.Drawing.Size(152, 31);
            this.btnLoadTesting.TabIndex = 26;
            this.btnLoadTesting.Text = "Prediction Data...";
            this.btnLoadTesting.UseVisualStyleBackColor = true;
            this.btnLoadTesting.Click += new System.EventHandler(this.btnLoadTesting_Click);
            // 
            // btnLoadTraining
            // 
            this.btnLoadTraining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTraining.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadTraining.Location = new System.Drawing.Point(376, 216);
            this.btnLoadTraining.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadTraining.Name = "btnLoadTraining";
            this.btnLoadTraining.Size = new System.Drawing.Size(152, 31);
            this.btnLoadTraining.TabIndex = 26;
            this.btnLoadTraining.Text = "Training Data..";
            this.btnLoadTraining.UseVisualStyleBackColor = true;
            this.btnLoadTraining.Click += new System.EventHandler(this.btnLoadTrainig_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(4, 4);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(523, 162);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // DataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnLoadTesting);
            this.Controls.Add(this.btnLoadTraining);
            this.Controls.Add(this.groupBox5);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataPanel";
            this.Size = new System.Drawing.Size(537, 287);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtNrVariables;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtNrTestSeries;
        private System.Windows.Forms.Button btnSetToGP;
        private System.Windows.Forms.TextBox txtNrSeries;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Button btnLoadTesting;
        private System.Windows.Forms.Button btnLoadTraining;
        private System.Windows.Forms.ListView listView1;
    }
}
