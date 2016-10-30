namespace GPdotNET.Tool.Common
{
    partial class SettingsPanel
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
            cmbSelectionMethods.SelectedIndexChanged -= this.cmbSelectionMethods_SelectedIndexChanged;
            button3.Click -= button3_Click;
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
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPopSize = new System.Windows.Forms.TextBox();
            this.cmbFitnessFuncs = new System.Windows.Forms.ComboBox();
            this.cmbInitMethods = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbSelParam2 = new System.Windows.Forms.Label();
            this.txtSelParam2 = new System.Windows.Forms.TextBox();
            this.lbSelParam1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtSelParam1 = new System.Windows.Forms.TextBox();
            this.txtElitism = new System.Windows.Forms.TextBox();
            this.cmbSelectionMethods = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtRandomConstNum = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtRandomConsFrom = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtRandomConsTo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioIsParallel = new System.Windows.Forms.RadioButton();
            this.radioSingleCore = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtProbEncaptulation = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txtProbPermutation = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtProbMutation = new System.Windows.Forms.TextBox();
            this.txtProbCrossover = new System.Windows.Forms.TextBox();
            this.txtProbReproduction = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Reprodukcija = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOperationTreeDepth = new System.Windows.Forms.TextBox();
            this.txtInitTreeDepth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtBroodSize = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.isProtectedOperation = new System.Windows.Forms.CheckBox();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.txtPopSize);
            this.groupBox8.Controls.Add(this.cmbFitnessFuncs);
            this.groupBox8.Controls.Add(this.cmbInitMethods);
            this.groupBox8.Controls.Add(this.label35);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox8.Location = new System.Drawing.Point(21, 15);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox8.Size = new System.Drawing.Size(358, 154);
            this.groupBox8.TabIndex = 43;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Population";
            // 
            // label6
            // 
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(190, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 25);
            this.label6.TabIndex = 30;
            this.label6.Text = "(50-5000)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPopSize
            // 
            this.txtPopSize.Location = new System.Drawing.Point(105, 23);
            this.txtPopSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPopSize.Name = "txtPopSize";
            this.txtPopSize.Size = new System.Drawing.Size(74, 26);
            this.txtPopSize.TabIndex = 1;
            this.txtPopSize.Text = "500";
            // 
            // cmbFitnessFuncs
            // 
            this.cmbFitnessFuncs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFitnessFuncs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFitnessFuncs.Location = new System.Drawing.Point(111, 63);
            this.cmbFitnessFuncs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFitnessFuncs.Name = "cmbFitnessFuncs";
            this.cmbFitnessFuncs.Size = new System.Drawing.Size(236, 28);
            this.cmbFitnessFuncs.TabIndex = 31;
            // 
            // cmbInitMethods
            // 
            this.cmbInitMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInitMethods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbInitMethods.Location = new System.Drawing.Point(111, 103);
            this.cmbInitMethods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbInitMethods.Name = "cmbInitMethods";
            this.cmbInitMethods.Size = new System.Drawing.Size(236, 28);
            this.cmbInitMethods.TabIndex = 29;
            // 
            // label35
            // 
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label35.Location = new System.Drawing.Point(10, 65);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(92, 32);
            this.label35.TabIndex = 32;
            this.label35.Tag = "";
            this.label35.Text = "Fitness:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(4, 106);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 28);
            this.label12.TabIndex = 16;
            this.label12.Tag = "";
            this.label12.Text = "Initialization:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.lbSelParam2);
            this.groupBox6.Controls.Add(this.txtSelParam2);
            this.groupBox6.Controls.Add(this.lbSelParam1);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.txtSelParam1);
            this.groupBox6.Controls.Add(this.txtElitism);
            this.groupBox6.Controls.Add(this.cmbSelectionMethods);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox6.Location = new System.Drawing.Point(21, 177);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox6.Size = new System.Drawing.Size(645, 157);
            this.groupBox6.TabIndex = 42;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Selection";
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(238, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 43;
            this.label3.Text = "(0-10)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSelParam2
            // 
            this.lbSelParam2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbSelParam2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSelParam2.Location = new System.Drawing.Point(340, 102);
            this.lbSelParam2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSelParam2.Name = "lbSelParam2";
            this.lbSelParam2.Size = new System.Drawing.Size(114, 28);
            this.lbSelParam2.TabIndex = 41;
            this.lbSelParam2.Tag = "";
            this.lbSelParam2.Text = "Parameter 2:";
            this.lbSelParam2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbSelParam2.Visible = false;
            // 
            // txtSelParam2
            // 
            this.txtSelParam2.Location = new System.Drawing.Point(465, 100);
            this.txtSelParam2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSelParam2.Name = "txtSelParam2";
            this.txtSelParam2.Size = new System.Drawing.Size(94, 26);
            this.txtSelParam2.TabIndex = 42;
            this.txtSelParam2.Text = "0";
            this.txtSelParam2.Visible = false;
            // 
            // lbSelParam1
            // 
            this.lbSelParam1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbSelParam1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSelParam1.Location = new System.Drawing.Point(9, 97);
            this.lbSelParam1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSelParam1.Name = "lbSelParam1";
            this.lbSelParam1.Size = new System.Drawing.Size(114, 28);
            this.lbSelParam1.TabIndex = 39;
            this.lbSelParam1.Tag = "";
            this.lbSelParam1.Text = "Parameter 1:";
            this.lbSelParam1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbSelParam1.Visible = false;
            // 
            // label23
            // 
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(248, 23);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(96, 25);
            this.label23.TabIndex = 38;
            this.label23.Text = "(0-PopSize)";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(51, 23);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 25);
            this.label19.TabIndex = 18;
            this.label19.Tag = "";
            this.label19.Text = "Elitism:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSelParam1
            // 
            this.txtSelParam1.Location = new System.Drawing.Point(134, 95);
            this.txtSelParam1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSelParam1.Name = "txtSelParam1";
            this.txtSelParam1.Size = new System.Drawing.Size(94, 26);
            this.txtSelParam1.TabIndex = 40;
            this.txtSelParam1.Text = "0";
            this.txtSelParam1.Visible = false;
            // 
            // txtElitism
            // 
            this.txtElitism.Location = new System.Drawing.Point(135, 20);
            this.txtElitism.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtElitism.Name = "txtElitism";
            this.txtElitism.Size = new System.Drawing.Size(94, 26);
            this.txtElitism.TabIndex = 37;
            this.txtElitism.Text = "1";
            // 
            // cmbSelectionMethods
            // 
            this.cmbSelectionMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectionMethods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSelectionMethods.Location = new System.Drawing.Point(134, 57);
            this.cmbSelectionMethods.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbSelectionMethods.Name = "cmbSelectionMethods";
            this.cmbSelectionMethods.Size = new System.Drawing.Size(426, 28);
            this.cmbSelectionMethods.TabIndex = 3;
            this.cmbSelectionMethods.SelectedIndexChanged += new System.EventHandler(this.cmbSelectionMethods_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(9, 62);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 28);
            this.label13.TabIndex = 17;
            this.label13.Tag = "";
            this.label13.Text = "Method:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.txtRandomConstNum);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.txtRandomConsFrom);
            this.groupBox4.Controls.Add(this.label29);
            this.groupBox4.Controls.Add(this.txtRandomConsTo);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(678, 178);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(292, 155);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Random constants";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(192, 103);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 38);
            this.button3.TabIndex = 24;
            this.button3.Text = "Generate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtRandomConstNum
            // 
            this.txtRandomConstNum.Location = new System.Drawing.Point(130, 109);
            this.txtRandomConstNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRandomConstNum.Name = "txtRandomConstNum";
            this.txtRandomConstNum.Size = new System.Drawing.Size(49, 26);
            this.txtRandomConstNum.TabIndex = 23;
            this.txtRandomConstNum.Text = "6";
            // 
            // label27
            // 
            this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label27.Location = new System.Drawing.Point(42, 35);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(80, 25);
            this.label27.TabIndex = 18;
            this.label27.Tag = "";
            this.label27.Text = "From: ";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label28.Location = new System.Drawing.Point(51, 109);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 31);
            this.label28.TabIndex = 22;
            this.label28.Tag = "";
            this.label28.Text = "Count:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRandomConsFrom
            // 
            this.txtRandomConsFrom.Location = new System.Drawing.Point(130, 32);
            this.txtRandomConsFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRandomConsFrom.Name = "txtRandomConsFrom";
            this.txtRandomConsFrom.Size = new System.Drawing.Size(49, 26);
            this.txtRandomConsFrom.TabIndex = 19;
            this.txtRandomConsFrom.Text = "0";
            // 
            // label29
            // 
            this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label29.Location = new System.Drawing.Point(87, 72);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(34, 25);
            this.label29.TabIndex = 21;
            this.label29.Tag = "";
            this.label29.Text = "To: ";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRandomConsTo
            // 
            this.txtRandomConsTo.Location = new System.Drawing.Point(130, 69);
            this.txtRandomConsTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRandomConsTo.Name = "txtRandomConsTo";
            this.txtRandomConsTo.Size = new System.Drawing.Size(49, 26);
            this.txtRandomConsTo.TabIndex = 20;
            this.txtRandomConsTo.Text = "10";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioIsParallel);
            this.groupBox3.Controls.Add(this.radioSingleCore);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(780, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(190, 155);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Type of procesors";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label39);
            this.groupBox2.Controls.Add(this.txtProbEncaptulation);
            this.groupBox2.Controls.Add(this.label40);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.label37);
            this.groupBox2.Controls.Add(this.txtProbPermutation);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtProbMutation);
            this.groupBox2.Controls.Add(this.txtProbCrossover);
            this.groupBox2.Controls.Add(this.txtProbReproduction);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.Reprodukcija);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(387, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(384, 151);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Probability of gp operations";
            // 
            // label39
            // 
            this.label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label39.Location = new System.Drawing.Point(669, 72);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(104, 28);
            this.label39.TabIndex = 35;
            this.label39.Tag = "";
            this.label39.Text = " (0,0 -1,00)";
            this.label39.Visible = false;
            // 
            // txtProbEncaptulation
            // 
            this.txtProbEncaptulation.Location = new System.Drawing.Point(540, 69);
            this.txtProbEncaptulation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbEncaptulation.Name = "txtProbEncaptulation";
            this.txtProbEncaptulation.Size = new System.Drawing.Size(120, 26);
            this.txtProbEncaptulation.TabIndex = 34;
            this.txtProbEncaptulation.Visible = false;
            // 
            // label40
            // 
            this.label40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label40.Location = new System.Drawing.Point(396, 72);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(112, 28);
            this.label40.TabIndex = 33;
            this.label40.Tag = "";
            this.label40.Text = "Encaptilation:";
            this.label40.Visible = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox3.Location = new System.Drawing.Point(782, 69);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(161, 24);
            this.checkBox3.TabIndex = 32;
            this.checkBox3.Text = "Enable decimation";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox2.Location = new System.Drawing.Point(782, 34);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(131, 24);
            this.checkBox2.TabIndex = 31;
            this.checkBox2.Text = "Enable editing";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // label37
            // 
            this.label37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label37.Location = new System.Drawing.Point(669, 37);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(116, 28);
            this.label37.TabIndex = 30;
            this.label37.Tag = "";
            this.label37.Text = " (0,0 -1,00)";
            this.label37.Visible = false;
            // 
            // txtProbPermutation
            // 
            this.txtProbPermutation.Location = new System.Drawing.Point(540, 32);
            this.txtProbPermutation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbPermutation.Name = "txtProbPermutation";
            this.txtProbPermutation.Size = new System.Drawing.Size(120, 26);
            this.txtProbPermutation.TabIndex = 29;
            this.txtProbPermutation.Visible = false;
            // 
            // label38
            // 
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label38.Location = new System.Drawing.Point(396, 37);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(112, 28);
            this.label38.TabIndex = 28;
            this.label38.Tag = "";
            this.label38.Text = "Permutation:";
            this.label38.Visible = false;
            // 
            // label10
            // 
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(285, 72);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 28);
            this.label10.TabIndex = 27;
            this.label10.Tag = "";
            this.label10.Text = " (0,0 -1,00)";
            // 
            // label11
            // 
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(285, 35);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 28);
            this.label11.TabIndex = 26;
            this.label11.Tag = "";
            this.label11.Text = " (0,0 -1,00)";
            // 
            // label15
            // 
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(285, 114);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 28);
            this.label15.TabIndex = 23;
            this.label15.Tag = "";
            this.label15.Text = " (0,0 -0,50)";
            // 
            // txtProbMutation
            // 
            this.txtProbMutation.Location = new System.Drawing.Point(156, 69);
            this.txtProbMutation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbMutation.Name = "txtProbMutation";
            this.txtProbMutation.Size = new System.Drawing.Size(120, 26);
            this.txtProbMutation.TabIndex = 25;
            // 
            // txtProbCrossover
            // 
            this.txtProbCrossover.Location = new System.Drawing.Point(156, 31);
            this.txtProbCrossover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbCrossover.Name = "txtProbCrossover";
            this.txtProbCrossover.Size = new System.Drawing.Size(120, 26);
            this.txtProbCrossover.TabIndex = 24;
            // 
            // txtProbReproduction
            // 
            this.txtProbReproduction.Location = new System.Drawing.Point(156, 111);
            this.txtProbReproduction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProbReproduction.Name = "txtProbReproduction";
            this.txtProbReproduction.Size = new System.Drawing.Size(120, 26);
            this.txtProbReproduction.TabIndex = 23;
            // 
            // label16
            // 
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(12, 72);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 28);
            this.label16.TabIndex = 13;
            this.label16.Tag = "";
            this.label16.Text = "Mutation:";
            // 
            // label17
            // 
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(12, 35);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 28);
            this.label17.TabIndex = 12;
            this.label17.Tag = "";
            this.label17.Text = "Crossover:";
            // 
            // Reprodukcija
            // 
            this.Reprodukcija.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reprodukcija.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Reprodukcija.Location = new System.Drawing.Point(12, 114);
            this.Reprodukcija.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Reprodukcija.Name = "Reprodukcija";
            this.Reprodukcija.Size = new System.Drawing.Size(117, 28);
            this.Reprodukcija.TabIndex = 11;
            this.Reprodukcija.Tag = "";
            this.Reprodukcija.Text = "Reproduction:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOperationTreeDepth);
            this.groupBox1.Controls.Add(this.txtInitTreeDepth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(21, 341);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(369, 151);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Max Tree depth";
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(285, 91);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 28);
            this.label7.TabIndex = 26;
            this.label7.Tag = "";
            this.label7.Text = " (3-17)";
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(285, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 28);
            this.label4.TabIndex = 23;
            this.label4.Tag = "";
            this.label4.Text = " (3-17)";
            // 
            // txtOperationTreeDepth
            // 
            this.txtOperationTreeDepth.Location = new System.Drawing.Point(156, 86);
            this.txtOperationTreeDepth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOperationTreeDepth.Name = "txtOperationTreeDepth";
            this.txtOperationTreeDepth.Size = new System.Drawing.Size(120, 26);
            this.txtOperationTreeDepth.TabIndex = 24;
            this.txtOperationTreeDepth.Text = "6";
            // 
            // txtInitTreeDepth
            // 
            this.txtInitTreeDepth.Location = new System.Drawing.Point(156, 34);
            this.txtInitTreeDepth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInitTreeDepth.Name = "txtInitTreeDepth";
            this.txtInitTreeDepth.Size = new System.Drawing.Size(120, 26);
            this.txtInitTreeDepth.TabIndex = 23;
            this.txtInitTreeDepth.Text = "5";
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 28);
            this.label2.TabIndex = 12;
            this.label2.Tag = "";
            this.label2.Text = "Operation depth:";
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 28);
            this.label5.TabIndex = 11;
            this.label5.Tag = "";
            this.label5.Text = "Initialize depth:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.txtBroodSize);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox5.Location = new System.Drawing.Point(404, 346);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(369, 151);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Crossover Improvements";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // label8
            // 
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(285, 91);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 28);
            this.label8.TabIndex = 26;
            this.label8.Tag = "";
            this.label8.Text = " (3-17)";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(285, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 28);
            this.label9.TabIndex = 23;
            this.label9.Tag = "";
            this.label9.Text = " (2-10)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(156, 86);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 26);
            this.textBox1.TabIndex = 24;
            this.textBox1.Text = "6";
            this.textBox1.Visible = false;
            // 
            // txtBroodSize
            // 
            this.txtBroodSize.Location = new System.Drawing.Point(156, 34);
            this.txtBroodSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBroodSize.Name = "txtBroodSize";
            this.txtBroodSize.Size = new System.Drawing.Size(120, 26);
            this.txtBroodSize.TabIndex = 23;
            this.txtBroodSize.Text = "5";
            // 
            // label14
            // 
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(12, 91);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(135, 28);
            this.label14.TabIndex = 12;
            this.label14.Tag = "";
            this.label14.Text = "Operation depth:";
            this.label14.Visible = false;
            // 
            // label18
            // 
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(12, 38);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(135, 28);
            this.label18.TabIndex = 11;
            this.label18.Tag = "";
            this.label18.Text = "Brood Size:";
            // 
            // isProtectedOperation
            // 
            this.isProtectedOperation.AutoSize = true;
            this.isProtectedOperation.Location = new System.Drawing.Point(787, 360);
            this.isProtectedOperation.Name = "isProtectedOperation";
            this.isProtectedOperation.Size = new System.Drawing.Size(183, 24);
            this.isProtectedOperation.TabIndex = 46;
            this.isProtectedOperation.Text = "Protected operations";
            this.isProtectedOperation.UseVisualStyleBackColor = true;
            // 
            // SettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.isProtectedOperation);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsPanel";
            this.Size = new System.Drawing.Size(984, 497);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPopSize;
        private System.Windows.Forms.ComboBox cmbFitnessFuncs;
        private System.Windows.Forms.ComboBox cmbInitMethods;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lbSelParam2;
        private System.Windows.Forms.TextBox txtSelParam2;
        private System.Windows.Forms.Label lbSelParam1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtSelParam1;
        private System.Windows.Forms.TextBox txtElitism;
        private System.Windows.Forms.ComboBox cmbSelectionMethods;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtRandomConstNum;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtRandomConsFrom;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtRandomConsTo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioIsParallel;
        private System.Windows.Forms.RadioButton radioSingleCore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtProbEncaptulation;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtProbPermutation;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtProbMutation;
        private System.Windows.Forms.TextBox txtProbCrossover;
        private System.Windows.Forms.TextBox txtProbReproduction;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label Reprodukcija;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOperationTreeDepth;
        private System.Windows.Forms.TextBox txtInitTreeDepth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtBroodSize;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox isProtectedOperation;
    }
}
