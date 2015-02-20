using GPdotNET.Tool.Common.GUI;
namespace GPdotNET.Tool.Common
{
    partial class ANNRunPanel
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
            this.eDuration = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.eTimeleft = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.eTimeToCompleate = new System.Windows.Forms.TextBox();
            this.eTimePerRun = new System.Windows.Forms.TextBox();
            this.eTimeStart = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bestSolutionAtIteration = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.currentError = new System.Windows.Forms.TextBox();
            this.iterationNumber = new System.Windows.Forms.TextBox();
            this.currentIteration = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.zedFitness = new ZedGraph.ZedGraphControl();
            this.zedModel = new ZedGraph.ZedGraphControl();
            this.progressBar1 = new GPdotNET.Tool.Common.GUI.CustomProgressBar();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // eDuration
            // 
            this.eDuration.Location = new System.Drawing.Point(183, 369);
            this.eDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eDuration.Name = "eDuration";
            this.eDuration.ReadOnly = true;
            this.eDuration.Size = new System.Drawing.Size(176, 26);
            this.eDuration.TabIndex = 35;
            // 
            // label44
            // 
            this.label44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label44.Location = new System.Drawing.Point(22, 369);
            this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(150, 28);
            this.label44.TabIndex = 34;
            this.label44.Text = "Duration(min):";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eTimeleft
            // 
            this.eTimeleft.Location = new System.Drawing.Point(183, 334);
            this.eTimeleft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eTimeleft.Name = "eTimeleft";
            this.eTimeleft.ReadOnly = true;
            this.eTimeleft.Size = new System.Drawing.Size(176, 26);
            this.eTimeleft.TabIndex = 33;
            // 
            // label43
            // 
            this.label43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label43.Location = new System.Drawing.Point(22, 334);
            this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(150, 28);
            this.label43.TabIndex = 32;
            this.label43.Text = "Avg. time left (min):";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eTimeToCompleate
            // 
            this.eTimeToCompleate.Location = new System.Drawing.Point(183, 300);
            this.eTimeToCompleate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eTimeToCompleate.Name = "eTimeToCompleate";
            this.eTimeToCompleate.ReadOnly = true;
            this.eTimeToCompleate.Size = new System.Drawing.Size(176, 26);
            this.eTimeToCompleate.TabIndex = 31;
            // 
            // eTimePerRun
            // 
            this.eTimePerRun.Location = new System.Drawing.Point(183, 266);
            this.eTimePerRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eTimePerRun.Name = "eTimePerRun";
            this.eTimePerRun.ReadOnly = true;
            this.eTimePerRun.Size = new System.Drawing.Size(176, 26);
            this.eTimePerRun.TabIndex = 30;
            // 
            // eTimeStart
            // 
            this.eTimeStart.Location = new System.Drawing.Point(183, 232);
            this.eTimeStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eTimeStart.Name = "eTimeStart";
            this.eTimeStart.ReadOnly = true;
            this.eTimeStart.Size = new System.Drawing.Size(176, 26);
            this.eTimeStart.TabIndex = 29;
            // 
            // label18
            // 
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(0, 300);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(172, 28);
            this.label18.TabIndex = 28;
            this.label18.Text = "Avg. finish time:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(10, 271);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(162, 28);
            this.label14.TabIndex = 27;
            this.label14.Text = "Cur. iteration (sec):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(40, 238);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 28);
            this.label9.TabIndex = 26;
            this.label9.Text = "Run started at:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bestSolutionAtIteration
            // 
            this.bestSolutionAtIteration.Location = new System.Drawing.Point(225, 164);
            this.bestSolutionAtIteration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bestSolutionAtIteration.Name = "bestSolutionAtIteration";
            this.bestSolutionAtIteration.ReadOnly = true;
            this.bestSolutionAtIteration.Size = new System.Drawing.Size(134, 26);
            this.bestSolutionAtIteration.TabIndex = 25;
            // 
            // label33
            // 
            this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(19, 159);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(195, 28);
            this.label33.TabIndex = 24;
            this.label33.Text = "Changed at:";
            // 
            // label32
            // 
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32.Location = new System.Drawing.Point(21, 129);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(152, 28);
            this.label32.TabIndex = 22;
            this.label32.Text = "Error:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Iteration number",
            "Error<="});
            this.comboBox2.Location = new System.Drawing.Point(19, 51);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(201, 28);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(21, 25);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(114, 28);
            this.label21.TabIndex = 18;
            this.label21.Text = "Envolve until:";
            // 
            // currentError
            // 
            this.currentError.Location = new System.Drawing.Point(225, 125);
            this.currentError.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.currentError.Name = "currentError";
            this.currentError.ReadOnly = true;
            this.currentError.Size = new System.Drawing.Size(134, 26);
            this.currentError.TabIndex = 3;
            // 
            // iterationNumber
            // 
            this.iterationNumber.Location = new System.Drawing.Point(225, 52);
            this.iterationNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iterationNumber.Name = "iterationNumber";
            this.iterationNumber.Size = new System.Drawing.Size(134, 26);
            this.iterationNumber.TabIndex = 17;
            this.iterationNumber.Text = "1000";
            // 
            // currentIteration
            // 
            this.currentIteration.Location = new System.Drawing.Point(225, 91);
            this.currentIteration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.currentIteration.Name = "currentIteration";
            this.currentIteration.ReadOnly = true;
            this.currentIteration.Size = new System.Drawing.Size(134, 26);
            this.currentIteration.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(21, 95);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(152, 28);
            this.label20.TabIndex = 0;
            this.label20.Text = "Iteration:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.eDuration);
            this.groupBox7.Controls.Add(this.label44);
            this.groupBox7.Controls.Add(this.eTimeleft);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.eTimeToCompleate);
            this.groupBox7.Controls.Add(this.eTimePerRun);
            this.groupBox7.Controls.Add(this.eTimeStart);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.bestSolutionAtIteration);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.comboBox2);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.currentError);
            this.groupBox7.Controls.Add(this.iterationNumber);
            this.groupBox7.Controls.Add(this.currentIteration);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox7.Location = new System.Drawing.Point(22, 14);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox7.Size = new System.Drawing.Size(370, 413);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Iterations";
            // 
            // zedFitness
            // 
            this.zedFitness.Location = new System.Drawing.Point(401, 14);
            this.zedFitness.Margin = new System.Windows.Forms.Padding(6);
            this.zedFitness.Name = "zedFitness";
            this.zedFitness.ScrollGrace = 0D;
            this.zedFitness.ScrollMaxX = 0D;
            this.zedFitness.ScrollMaxY = 0D;
            this.zedFitness.ScrollMaxY2 = 0D;
            this.zedFitness.ScrollMinX = 0D;
            this.zedFitness.ScrollMinY = 0D;
            this.zedFitness.ScrollMinY2 = 0D;
            this.zedFitness.Size = new System.Drawing.Size(312, 172);
            this.zedFitness.TabIndex = 18;
            // 
            // zedModel
            // 
            this.zedModel.Location = new System.Drawing.Point(401, 246);
            this.zedModel.Margin = new System.Windows.Forms.Padding(6);
            this.zedModel.Name = "zedModel";
            this.zedModel.ScrollGrace = 0D;
            this.zedModel.ScrollMaxX = 0D;
            this.zedModel.ScrollMaxY = 0D;
            this.zedModel.ScrollMaxY2 = 0D;
            this.zedModel.ScrollMinX = 0D;
            this.zedModel.ScrollMinY = 0D;
            this.zedModel.ScrollMinY2 = 0D;
            this.zedModel.Size = new System.Drawing.Size(312, 172);
            this.zedModel.TabIndex = 21;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.BarColor = System.Drawing.SystemColors.Highlight;
            this.progressBar1.CenterText = null;
            this.progressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressBar1.Location = new System.Drawing.Point(4, 461);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.progressBar1.MarqueeAnimationSpeed = 100;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ShowPercentage = false;
            this.progressBar1.Size = new System.Drawing.Size(710, 25);
            this.progressBar1.Step = 10;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Value = 0;
            // 
            // ANNRunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.zedModel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.zedFitness);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ANNRunPanel";
            this.Size = new System.Drawing.Size(723, 490);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected CustomProgressBar progressBar1;
        protected System.Windows.Forms.TextBox eDuration;
        protected System.Windows.Forms.Label label44;
        protected System.Windows.Forms.TextBox eTimeleft;
        protected System.Windows.Forms.Label label43;
        protected System.Windows.Forms.TextBox eTimeToCompleate;
        protected System.Windows.Forms.TextBox eTimePerRun;
        protected System.Windows.Forms.TextBox eTimeStart;
        protected System.Windows.Forms.Label label18;
        protected System.Windows.Forms.Label label14;
        protected System.Windows.Forms.Label label9;
        protected System.Windows.Forms.TextBox bestSolutionAtIteration;
        protected System.Windows.Forms.Label label33;
        protected System.Windows.Forms.Label label32;
        protected System.Windows.Forms.ComboBox comboBox2;
        protected System.Windows.Forms.Label label21;
        protected System.Windows.Forms.TextBox currentError;
        protected System.Windows.Forms.TextBox iterationNumber;
        protected System.Windows.Forms.TextBox currentIteration;
        protected System.Windows.Forms.Label label20;
        protected System.Windows.Forms.GroupBox groupBox7;
        protected ZedGraph.ZedGraphControl zedFitness;
        protected ZedGraph.ZedGraphControl zedModel;



    }
}
