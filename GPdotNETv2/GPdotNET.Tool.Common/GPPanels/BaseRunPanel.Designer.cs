using GPdotNET.Tool.Common.GUI;
namespace GPdotNET.Tool.Common
{
    partial class BaseRunPanel
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
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
            this.bestFitnessAtGenerationEditBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.currentErrorBox = new System.Windows.Forms.TextBox();
            this.brojIteracija = new System.Windows.Forms.TextBox();
            this.currentIterationBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.zedFitness = new ZedGraph.ZedGraphControl();
            this.progressBar1 = new GPdotNET.Tool.Common.GUI.CustomProgressBar();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.groupBox7.Controls.Add(this.bestFitnessAtGenerationEditBox);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.textBox3);
            this.groupBox7.Controls.Add(this.label31);
            this.groupBox7.Controls.Add(this.comboBox2);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.currentErrorBox);
            this.groupBox7.Controls.Add(this.brojIteracija);
            this.groupBox7.Controls.Add(this.currentIterationBox);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox7.Location = new System.Drawing.Point(12, 13);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(247, 272);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Evolution";
            // 
            // eDuration
            // 
            this.eDuration.Location = new System.Drawing.Point(122, 240);
            this.eDuration.Name = "eDuration";
            this.eDuration.ReadOnly = true;
            this.eDuration.Size = new System.Drawing.Size(119, 20);
            this.eDuration.TabIndex = 35;
            // 
            // label44
            // 
            this.label44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label44.Location = new System.Drawing.Point(15, 240);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(100, 18);
            this.label44.TabIndex = 34;
            this.label44.Text = "Duration(min):";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eTimeleft
            // 
            this.eTimeleft.Location = new System.Drawing.Point(122, 217);
            this.eTimeleft.Name = "eTimeleft";
            this.eTimeleft.ReadOnly = true;
            this.eTimeleft.Size = new System.Drawing.Size(119, 20);
            this.eTimeleft.TabIndex = 33;
            // 
            // label43
            // 
            this.label43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label43.Location = new System.Drawing.Point(15, 217);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(100, 18);
            this.label43.TabIndex = 32;
            this.label43.Text = "Avg. time left (min):";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eTimeToCompleate
            // 
            this.eTimeToCompleate.Location = new System.Drawing.Point(122, 195);
            this.eTimeToCompleate.Name = "eTimeToCompleate";
            this.eTimeToCompleate.ReadOnly = true;
            this.eTimeToCompleate.Size = new System.Drawing.Size(119, 20);
            this.eTimeToCompleate.TabIndex = 31;
            // 
            // eTimePerRun
            // 
            this.eTimePerRun.Location = new System.Drawing.Point(122, 173);
            this.eTimePerRun.Name = "eTimePerRun";
            this.eTimePerRun.ReadOnly = true;
            this.eTimePerRun.Size = new System.Drawing.Size(119, 20);
            this.eTimePerRun.TabIndex = 30;
            // 
            // eTimeStart
            // 
            this.eTimeStart.Location = new System.Drawing.Point(122, 151);
            this.eTimeStart.Name = "eTimeStart";
            this.eTimeStart.ReadOnly = true;
            this.eTimeStart.Size = new System.Drawing.Size(119, 20);
            this.eTimeStart.TabIndex = 29;
            // 
            // label18
            // 
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(0, 195);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(115, 18);
            this.label18.TabIndex = 28;
            this.label18.Text = "Avg. finish time:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(7, 176);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 18);
            this.label14.TabIndex = 27;
            this.label14.Text = "Cur. iteration (sec):";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(27, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 26;
            this.label9.Text = "Run started at:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bestFitnessAtGenerationEditBox
            // 
            this.bestFitnessAtGenerationEditBox.Location = new System.Drawing.Point(150, 128);
            this.bestFitnessAtGenerationEditBox.Name = "bestFitnessAtGenerationEditBox";
            this.bestFitnessAtGenerationEditBox.ReadOnly = true;
            this.bestFitnessAtGenerationEditBox.Size = new System.Drawing.Size(91, 20);
            this.bestFitnessAtGenerationEditBox.TabIndex = 25;
            // 
            // label33
            // 
            this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(14, 131);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(130, 18);
            this.label33.TabIndex = 24;
            this.label33.Text = "Changed at generation:";
            // 
            // label32
            // 
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label32.Location = new System.Drawing.Point(14, 84);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(101, 18);
            this.label32.TabIndex = 22;
            this.label32.Text = "Best fitness:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(150, 104);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(91, 20);
            this.textBox3.TabIndex = 21;
            this.textBox3.Text = "1000,00";
            // 
            // label31
            // 
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(14, 107);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(101, 18);
            this.label31.TabIndex = 20;
            this.label31.Text = "Max fitness:";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Generation number",
            "Fitness >="});
            this.comboBox2.Location = new System.Drawing.Point(13, 33);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(135, 21);
            this.comboBox2.TabIndex = 19;
            // 
            // label21
            // 
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(14, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(76, 18);
            this.label21.TabIndex = 18;
            this.label21.Text = "Envolve until:";
            // 
            // currentErrorBox
            // 
            this.currentErrorBox.Location = new System.Drawing.Point(150, 81);
            this.currentErrorBox.Name = "currentErrorBox";
            this.currentErrorBox.ReadOnly = true;
            this.currentErrorBox.Size = new System.Drawing.Size(91, 20);
            this.currentErrorBox.TabIndex = 3;
            // 
            // brojIteracija
            // 
            this.brojIteracija.Location = new System.Drawing.Point(150, 34);
            this.brojIteracija.Name = "brojIteracija";
            this.brojIteracija.Size = new System.Drawing.Size(91, 20);
            this.brojIteracija.TabIndex = 17;
            this.brojIteracija.Text = "500";
            // 
            // currentIterationBox
            // 
            this.currentIterationBox.Location = new System.Drawing.Point(150, 59);
            this.currentIterationBox.Name = "currentIterationBox";
            this.currentIterationBox.ReadOnly = true;
            this.currentIterationBox.Size = new System.Drawing.Size(91, 20);
            this.currentIterationBox.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(14, 62);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(101, 18);
            this.label20.TabIndex = 0;
            this.label20.Text = "Generation:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Location = new System.Drawing.Point(11, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Progress ...";
            // 
            // zedFitness
            // 
            this.zedFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedFitness.Location = new System.Drawing.Point(265, 13);
            this.zedFitness.Name = "zedFitness";
            this.zedFitness.ScrollGrace = 0D;
            this.zedFitness.ScrollMaxX = 0D;
            this.zedFitness.ScrollMaxY = 0D;
            this.zedFitness.ScrollMaxY2 = 0D;
            this.zedFitness.ScrollMinX = 0D;
            this.zedFitness.ScrollMinY = 0D;
            this.zedFitness.ScrollMinY2 = 0D;
            this.zedFitness.Size = new System.Drawing.Size(208, 112);
            this.zedFitness.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.BarColor = System.Drawing.SystemColors.Highlight;
            this.progressBar1.CenterText = null;
            this.progressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressBar1.Location = new System.Drawing.Point(8, 311);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar1.MarqueeAnimationSpeed = 100;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ShowPercentage = false;
            this.progressBar1.Size = new System.Drawing.Size(458, 23);
            this.progressBar1.Step = 10;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Blocks;
            this.progressBar1.TabIndex = 17;
            this.progressBar1.Value = 0;
            // 
            // BaseRunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.zedFitness);
            this.Name = "BaseRunPanel";
            this.Size = new System.Drawing.Size(473, 338);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBox7;
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
        protected System.Windows.Forms.TextBox bestFitnessAtGenerationEditBox;
        protected System.Windows.Forms.Label label33;
        protected System.Windows.Forms.Label label32;
        protected System.Windows.Forms.TextBox textBox3;
        protected System.Windows.Forms.Label label31;
        protected System.Windows.Forms.ComboBox comboBox2;
        protected System.Windows.Forms.Label label21;
        protected System.Windows.Forms.TextBox currentErrorBox;
        protected System.Windows.Forms.TextBox brojIteracija;
        protected System.Windows.Forms.TextBox currentIterationBox;
        protected System.Windows.Forms.Label label20;
        protected CustomProgressBar progressBar1;
        protected System.Windows.Forms.Label label3;
        protected ZedGraph.ZedGraphControl zedFitness;
       

    }
}
