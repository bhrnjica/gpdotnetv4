using GPdotNET.Tool.Common.GUI;
namespace GPdotNET.Tool.Common
{
    partial class RunPanel
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
            this.components = new System.ComponentModel.Container();
            this.zedModel = new ZedGraph.ZedGraphControl();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // eDuration
            // 
            this.eb_durationInMin.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // eTimeleft
            // 
            this.eb_timeleft.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // eTimeToCompleate
            // 
            this.eb_timeToCompleate.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // eTimePerRun
            // 
            this.eb_timePerRun.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // eTimeStart
            // 
            this.eb_timeStart.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // bestFitnessAtGenerationEditBox
            // 
            this.eb_bestSolutionFound.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // textBox3
            // 
            this.eb_maximumFitness.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            
            // 
            // currentErrorBox
            // 
            this.eb_currentFitness.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // brojIteracija
            // 
            this.m_eb_iterations.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // currentIteration
            // 
            this.eb_currentIteration.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
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
            this.zedFitness.Size = new System.Drawing.Size(264, 181);
            // 
            // zedModel
            // 
            this.zedModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedModel.Location = new System.Drawing.Point(352, 204);
            this.zedModel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.zedModel.Name = "zedModel";
            this.zedModel.ScrollGrace = 0D;
            this.zedModel.ScrollMaxX = 0D;
            this.zedModel.ScrollMaxY = 0D;
            this.zedModel.ScrollMaxY2 = 0D;
            this.zedModel.ScrollMinX = 0D;
            this.zedModel.ScrollMinY = 0D;
            this.zedModel.ScrollMinY2 = 0D;
            this.zedModel.Size = new System.Drawing.Size(265, 181);
            this.zedModel.TabIndex = 1;
            // 
            // RunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.zedModel);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "RunPanel";
            this.Controls.SetChildIndex(this.zedModel, 0);
            this.Controls.SetChildIndex(this.zedFitness, 0);
            this.Controls.SetChildIndex(this.groupBox7, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedModel;

    }
}
