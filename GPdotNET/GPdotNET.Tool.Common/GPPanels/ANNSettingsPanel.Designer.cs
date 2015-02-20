namespace GPdotNET.Tool.Common
{
    partial class ANNSettingsPanel
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
            this.label2 = new System.Windows.Forms.Label();
            this.textNumHiddenLayers = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioIsParallel = new System.Windows.Forms.RadioButton();
            this.radioSingleCore = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textNeuronsOfEachLAyer = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.cmbActivationFuncs = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMomentum = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtActFunParam1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textLearningRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textSWeight = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textCWeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textIWeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textParticles = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(270, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 28);
            this.label2.TabIndex = 42;
            this.label2.Tag = "";
            this.label2.Text = " (0 -10)";
            // 
            // textNumHiddenLayers
            // 
            this.textNumHiddenLayers.Location = new System.Drawing.Point(181, 51);
            this.textNumHiddenLayers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNumHiddenLayers.Name = "textNumHiddenLayers";
            this.textNumHiddenLayers.Size = new System.Drawing.Size(82, 26);
            this.textNumHiddenLayers.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(-19, 50);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(189, 28);
            this.label12.TabIndex = 16;
            this.label12.Tag = "";
            this.label12.Text = "Layers:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioIsParallel);
            this.groupBox1.Controls.Add(this.radioSingleCore);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(716, 320);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(232, 159);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type of procesors";
            this.groupBox1.Visible = false;
            // 
            // radioIsParallel
            // 
            this.radioIsParallel.AutoSize = true;
            this.radioIsParallel.Checked = true;
            this.radioIsParallel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioIsParallel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioIsParallel.Location = new System.Drawing.Point(32, 98);
            this.radioIsParallel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioIsParallel.Name = "radioIsParallel";
            this.radioIsParallel.Size = new System.Drawing.Size(111, 24);
            this.radioIsParallel.TabIndex = 1;
            this.radioIsParallel.TabStop = true;
            this.radioIsParallel.Text = "Multy Core ";
            this.radioIsParallel.UseVisualStyleBackColor = true;
            // 
            // radioSingleCore
            // 
            this.radioSingleCore.AutoSize = true;
            this.radioSingleCore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioSingleCore.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioSingleCore.Location = new System.Drawing.Point(32, 49);
            this.radioSingleCore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioSingleCore.Name = "radioSingleCore";
            this.radioSingleCore.Size = new System.Drawing.Size(111, 24);
            this.radioSingleCore.TabIndex = 0;
            this.radioSingleCore.Text = "Single core";
            this.radioSingleCore.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(-19, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 28);
            this.label1.TabIndex = 47;
            this.label1.Tag = "";
            this.label1.Text = "Neurons of each Layer:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textNeuronsOfEachLAyer
            // 
            this.textNeuronsOfEachLAyer.Location = new System.Drawing.Point(181, 111);
            this.textNeuronsOfEachLAyer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textNeuronsOfEachLAyer.Name = "textNeuronsOfEachLAyer";
            this.textNeuronsOfEachLAyer.Size = new System.Drawing.Size(82, 26);
            this.textNeuronsOfEachLAyer.TabIndex = 48;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textNeuronsOfEachLAyer);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textNumHiddenLayers);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(546, 28);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(393, 167);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hidden Layers";
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(270, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 28);
            this.label5.TabIndex = 49;
            this.label5.Tag = "";
            this.label5.Text = " (0 -inf)";
            // 
            // label35
            // 
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label35.Location = new System.Drawing.Point(0, 133);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(149, 32);
            this.label35.TabIndex = 32;
            this.label35.Tag = "";
            this.label35.Text = "Activation function:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbActivationFuncs
            // 
            this.cmbActivationFuncs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivationFuncs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActivationFuncs.Location = new System.Drawing.Point(202, 132);
            this.cmbActivationFuncs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbActivationFuncs.Name = "cmbActivationFuncs";
            this.cmbActivationFuncs.Size = new System.Drawing.Size(204, 28);
            this.cmbActivationFuncs.TabIndex = 31;
            this.cmbActivationFuncs.SelectedIndexChanged += new System.EventHandler(this.cmbActivationFuncs_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(37, 61);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 28);
            this.label17.TabIndex = 33;
            this.label17.Tag = "";
            this.label17.Text = "Momentum:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMomentum
            // 
            this.txtMomentum.Location = new System.Drawing.Point(201, 57);
            this.txtMomentum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMomentum.Name = "txtMomentum";
            this.txtMomentum.Size = new System.Drawing.Size(120, 26);
            this.txtMomentum.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(330, 58);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 28);
            this.label11.TabIndex = 35;
            this.label11.Tag = "";
            this.label11.Text = " (0,0 -1,00)";
            // 
            // label16
            // 
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(85, 172);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 28);
            this.label16.TabIndex = 37;
            this.label16.Tag = "";
            this.label16.Text = "arg:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtActFunParam1
            // 
            this.txtActFunParam1.Location = new System.Drawing.Point(200, 172);
            this.txtActFunParam1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtActFunParam1.Name = "txtActFunParam1";
            this.txtActFunParam1.Size = new System.Drawing.Size(82, 26);
            this.txtActFunParam1.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(306, 175);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 28);
            this.label10.TabIndex = 41;
            this.label10.Tag = "";
            this.label10.Text = " (0,0 -5,00)";
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(25, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 28);
            this.label4.TabIndex = 44;
            this.label4.Tag = "";
            this.label4.Text = "Learning Rate:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textLearningRate
            // 
            this.textLearningRate.Location = new System.Drawing.Point(202, 93);
            this.textLearningRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textLearningRate.Name = "textLearningRate";
            this.textLearningRate.Size = new System.Drawing.Size(120, 26);
            this.textLearningRate.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(331, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 28);
            this.label3.TabIndex = 46;
            this.label3.Tag = "";
            this.label3.Text = " (min= 0.0005)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textLearningRate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtActFunParam1);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtMomentum);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.cmbActivationFuncs);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(21, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(517, 217);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textSWeight);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.textCWeight);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.textIWeight);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.textParticles);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(25, 242);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(463, 192);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Particle Swarm Optimization Parameters";
            // 
            // textSWeight
            // 
            this.textSWeight.Location = new System.Drawing.Point(194, 155);
            this.textSWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textSWeight.Name = "textSWeight";
            this.textSWeight.Size = new System.Drawing.Size(120, 26);
            this.textSWeight.TabIndex = 60;
            // 
            // label18
            // 
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(323, 114);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 28);
            this.label18.TabIndex = 59;
            this.label18.Tag = "";
            this.label18.Text = " ( - )";
            // 
            // textCWeight
            // 
            this.textCWeight.Location = new System.Drawing.Point(195, 117);
            this.textCWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textCWeight.Name = "textCWeight";
            this.textCWeight.Size = new System.Drawing.Size(120, 26);
            this.textCWeight.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(323, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 28);
            this.label6.TabIndex = 57;
            this.label6.Tag = "";
            this.label6.Text = " (< 1.0)";
            // 
            // textIWeight
            // 
            this.textIWeight.Location = new System.Drawing.Point(195, 74);
            this.textIWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textIWeight.Name = "textIWeight";
            this.textIWeight.Size = new System.Drawing.Size(120, 26);
            this.textIWeight.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(18, 78);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 28);
            this.label7.TabIndex = 55;
            this.label7.Tag = "";
            this.label7.Text = "Inertia Weight:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(323, 156);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 28);
            this.label8.TabIndex = 54;
            this.label8.Tag = "";
            this.label8.Text = " ( - )";
            // 
            // label9
            // 
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(8, 153);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 28);
            this.label9.TabIndex = 52;
            this.label9.Tag = "";
            this.label9.Text = "Social Weight:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(323, 39);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 28);
            this.label13.TabIndex = 51;
            this.label13.Tag = "";
            this.label13.Text = "(10 - 1000)";
            // 
            // textParticles
            // 
            this.textParticles.Location = new System.Drawing.Point(194, 38);
            this.textParticles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textParticles.Name = "textParticles";
            this.textParticles.Size = new System.Drawing.Size(120, 26);
            this.textParticles.TabIndex = 50;
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(-4, 42);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(146, 28);
            this.label14.TabIndex = 49;
            this.label14.Tag = "";
            this.label14.Text = "Particles:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(-7, 114);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(149, 32);
            this.label15.TabIndex = 48;
            this.label15.Tag = "";
            this.label15.Text = "Cognitive Weight:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ANNSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ANNSettingsPanel";
            this.Size = new System.Drawing.Size(984, 497);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioIsParallel;
        private System.Windows.Forms.RadioButton radioSingleCore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNumHiddenLayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNeuronsOfEachLAyer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox cmbActivationFuncs;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMomentum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtActFunParam1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textLearningRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textIWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textParticles;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textSWeight;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textCWeight;
    }
}
