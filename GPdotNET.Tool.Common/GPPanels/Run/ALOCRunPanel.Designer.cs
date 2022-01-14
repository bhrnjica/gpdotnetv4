using GPdotNET.Tool.Common.GUI;
namespace GPdotNET.Tool.Common
{
    partial class ALOCRunPanel
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
            this.tboptimalLayout = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.zedFitness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedFitness.Location = new System.Drawing.Point(353, 16);
            this.zedFitness.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.zedFitness.Size = new System.Drawing.Size(275, 181);
            // 
            // chkOptimumType
            // 
            this.chkOptimumType.Checked = true;
            this.chkOptimumType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOptimumType.Visible = true;
            // 
            // tbShortestPath
            // 
            this.tboptimalLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboptimalLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tboptimalLayout.ForeColor = System.Drawing.Color.Crimson;
            this.tboptimalLayout.Location = new System.Drawing.Point(353, 207);
            this.tboptimalLayout.Multiline = true;
            this.tboptimalLayout.Name = "tbShortestPath";
            this.tboptimalLayout.Size = new System.Drawing.Size(275, 182);
            this.tboptimalLayout.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Optimal Layout:";
            // 
            // ALOCRunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tboptimalLayout);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ALOCRunPanel";
            this.Controls.SetChildIndex(this.zedFitness, 0);
            this.Controls.SetChildIndex(this.groupBox7, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.tboptimalLayout, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tboptimalLayout;
        private System.Windows.Forms.Label label1;

    }
}
