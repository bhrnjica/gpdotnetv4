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
            this.zedModel = new ZedGraph.ZedGraphControl();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedFitness
            // 
            this.zedFitness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedFitness.Location = new System.Drawing.Point(265, 13);
            this.zedFitness.Size = new System.Drawing.Size(198, 147);
            // 
            // zedModel
            // 
            this.zedModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedModel.Location = new System.Drawing.Point(264, 166);
            this.zedModel.Name = "zedModel";
            this.zedModel.ScrollGrace = 0D;
            this.zedModel.ScrollMaxX = 0D;
            this.zedModel.ScrollMaxY = 0D;
            this.zedModel.ScrollMaxY2 = 0D;
            this.zedModel.ScrollMinX = 0D;
            this.zedModel.ScrollMinY = 0D;
            this.zedModel.ScrollMinY2 = 0D;
            this.zedModel.Size = new System.Drawing.Size(199, 147);
            this.zedModel.TabIndex = 1;
            // 
            // RunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.zedModel);
            this.Name = "RunPanel";
            this.Controls.SetChildIndex(this.zedModel, 0);
            this.Controls.SetChildIndex(this.zedFitness, 0);
            this.Controls.SetChildIndex(this.groupBox7, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedModel;

    }
}
