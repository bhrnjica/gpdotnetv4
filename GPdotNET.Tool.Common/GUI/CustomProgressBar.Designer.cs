namespace GPdotNET.Tool.Common.GUI
{
    partial class CustomProgressBar
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
            this.thePB = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // thePB
            // 
            this.thePB.Location = new System.Drawing.Point(0, 0);
            this.thePB.Margin = new System.Windows.Forms.Padding(0);
            this.thePB.Name = "thePB";
            this.thePB.Size = new System.Drawing.Size(133, 27);
            this.thePB.TabIndex = 0;
            // 
            // TheBestProgressBarEver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.thePB);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TheBestProgressBarEver";
            this.Size = new System.Drawing.Size(133, 27);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ProgressBar thePB;
    }
}
