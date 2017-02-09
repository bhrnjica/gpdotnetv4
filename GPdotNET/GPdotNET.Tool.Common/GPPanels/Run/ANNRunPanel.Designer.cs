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
            this.eb_durationInMin = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.eb_timeleft = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.eb_timeToCompleate = new System.Windows.Forms.TextBox();
            this.eb_timePerRun = new System.Windows.Forms.TextBox();
            this.eb_timeStart = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.eb_bestSolutionFound = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.eb_currentError = new System.Windows.Forms.TextBox();
            this.m_eb_iterations = new System.Windows.Forms.TextBox();
            this.eb_currentIteration = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.zedFitness = new ZedGraph.ZedGraphControl();
            this.zedModel = new ZedGraph.ZedGraphControl();
            this.progressBar1 = new GPdotNET.Tool.Common.GUI.CustomProgressBar();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // eb_durationInMin
            // 
            this.eb_durationInMin.Location = new System.Drawing.Point(183, 369);
            this.eb_durationInMin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_durationInMin.Name = "eb_durationInMin";
            this.eb_durationInMin.ReadOnly = true;
            this.eb_durationInMin.Size = new System.Drawing.Size(176, 26);
            this.eb_durationInMin.TabIndex = 35;
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
            // eb_timeleft
            // 
            this.eb_timeleft.Location = new System.Drawing.Point(183, 334);
            this.eb_timeleft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_timeleft.Name = "eb_timeleft";
            this.eb_timeleft.ReadOnly = true;
            this.eb_timeleft.Size = new System.Drawing.Size(176, 26);
            this.eb_timeleft.TabIndex = 33;
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
            // eb_timeToCompleate
            // 
            this.eb_timeToCompleate.Location = new System.Drawing.Point(183, 300);
            this.eb_timeToCompleate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_timeToCompleate.Name = "eb_timeToCompleate";
            this.eb_timeToCompleate.ReadOnly = true;
            this.eb_timeToCompleate.Size = new System.Drawing.Size(176, 26);
            this.eb_timeToCompleate.TabIndex = 31;
            // 
            // eb_timePerRun
            // 
            this.eb_timePerRun.Location = new System.Drawing.Point(183, 266);
            this.eb_timePerRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_timePerRun.Name = "eb_timePerRun";
            this.eb_timePerRun.ReadOnly = true;
            this.eb_timePerRun.Size = new System.Drawing.Size(176, 26);
            this.eb_timePerRun.TabIndex = 30;
            // 
            // eb_timeStart
            // 
            this.eb_timeStart.Location = new System.Drawing.Point(183, 232);
            this.eb_timeStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_timeStart.Name = "eb_timeStart";
            this.eb_timeStart.ReadOnly = true;
            this.eb_timeStart.Size = new System.Drawing.Size(176, 26);
            this.eb_timeStart.TabIndex = 29;
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
            // eb_bestSolutionFound
            // 
            this.eb_bestSolutionFound.Location = new System.Drawing.Point(225, 164);
            this.eb_bestSolutionFound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_bestSolutionFound.Name = "eb_bestSolutionFound";
            this.eb_bestSolutionFound.ReadOnly = true;
            this.eb_bestSolutionFound.Size = new System.Drawing.Size(134, 26);
            this.eb_bestSolutionFound.TabIndex = 25;
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
            // eb_currentError
            // 
            this.eb_currentError.Location = new System.Drawing.Point(225, 125);
            this.eb_currentError.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_currentError.Name = "eb_currentError";
            this.eb_currentError.ReadOnly = true;
            this.eb_currentError.Size = new System.Drawing.Size(134, 26);
            this.eb_currentError.TabIndex = 3;
            // 
            // m_eb_iterations
            // 
            this.m_eb_iterations.Location = new System.Drawing.Point(225, 52);
            this.m_eb_iterations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_eb_iterations.Name = "m_eb_iterations";
            this.m_eb_iterations.Size = new System.Drawing.Size(134, 26);
            this.m_eb_iterations.TabIndex = 17;
            this.m_eb_iterations.Text = "1000";
            // 
            // eb_currentIteration
            // 
            this.eb_currentIteration.Location = new System.Drawing.Point(225, 91);
            this.eb_currentIteration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eb_currentIteration.Name = "eb_currentIteration";
            this.eb_currentIteration.ReadOnly = true;
            this.eb_currentIteration.Size = new System.Drawing.Size(134, 26);
            this.eb_currentIteration.TabIndex = 1;
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
            this.groupBox7.Controls.Add(this.eb_durationInMin);
            this.groupBox7.Controls.Add(this.label44);
            this.groupBox7.Controls.Add(this.eb_timeleft);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.eb_timeToCompleate);
            this.groupBox7.Controls.Add(this.eb_timePerRun);
            this.groupBox7.Controls.Add(this.eb_timeStart);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.eb_bestSolutionFound);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.comboBox2);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.eb_currentError);
            this.groupBox7.Controls.Add(this.m_eb_iterations);
            this.groupBox7.Controls.Add(this.eb_currentIteration);
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
        protected System.Windows.Forms.TextBox eb_durationInMin;
        protected System.Windows.Forms.Label label44;
        protected System.Windows.Forms.TextBox eb_timeleft;
        protected System.Windows.Forms.Label label43;
        protected System.Windows.Forms.TextBox eb_timeToCompleate;
        protected System.Windows.Forms.TextBox eb_timePerRun;
        protected System.Windows.Forms.TextBox eb_timeStart;
        protected System.Windows.Forms.Label label18;
        protected System.Windows.Forms.Label label14;
        protected System.Windows.Forms.Label label9;
        protected System.Windows.Forms.TextBox eb_bestSolutionFound;
        protected System.Windows.Forms.Label label33;
        protected System.Windows.Forms.Label label32;
        protected System.Windows.Forms.ComboBox comboBox2;
        protected System.Windows.Forms.Label label21;
        protected System.Windows.Forms.TextBox eb_currentError;
        protected System.Windows.Forms.TextBox m_eb_iterations;
        protected System.Windows.Forms.TextBox eb_currentIteration;
        protected System.Windows.Forms.Label label20;
        protected System.Windows.Forms.GroupBox groupBox7;
        protected ZedGraph.ZedGraphControl zedFitness;
        protected ZedGraph.ZedGraphControl zedModel;



    }
}
