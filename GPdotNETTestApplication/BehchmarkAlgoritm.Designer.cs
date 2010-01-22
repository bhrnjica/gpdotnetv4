namespace GPdotNETTestApplication
{
    partial class BehchmarkAlgoritm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.evelicinaPopulacije = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edubinaMutacije = new System.Windows.Forms.TextBox();
            this.edubinaUkrstanja = new System.Windows.Forms.TextBox();
            this.epocetnaDubinaDrveta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.evjerojatnostPermutacije = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.evjerojatnostMutacije = new System.Windows.Forms.TextBox();
            this.evjerojatnostUkrstanja = new System.Windows.Forms.TextBox();
            this.evjerojatnostReprodukcije = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Reprodukcija = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Run sequential";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(375, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Run parallel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // evelicinaPopulacije
            // 
            this.evelicinaPopulacije.Location = new System.Drawing.Point(138, 28);
            this.evelicinaPopulacije.Name = "evelicinaPopulacije";
            this.evelicinaPopulacije.Size = new System.Drawing.Size(81, 20);
            this.evelicinaPopulacije.TabIndex = 3;
            this.evelicinaPopulacije.Text = "500";
            // 
            // label1
            // 
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(45, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Population size";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.edubinaMutacije);
            this.groupBox1.Controls.Add(this.edubinaUkrstanja);
            this.groupBox1.Controls.Add(this.epocetnaDubinaDrveta);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(262, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 139);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Max Tree Depth";
            // 
            // label8
            // 
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(190, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 18);
            this.label8.TabIndex = 27;
            this.label8.Tag = "";
            this.label8.Text = " (3-25)";
            // 
            // label7
            // 
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(190, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 18);
            this.label7.TabIndex = 26;
            this.label7.Tag = "";
            this.label7.Text = " (3-25)";
            // 
            // label4
            // 
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(190, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 18);
            this.label4.TabIndex = 23;
            this.label4.Tag = "";
            this.label4.Text = " (3-25)";
            // 
            // edubinaMutacije
            // 
            this.edubinaMutacije.Location = new System.Drawing.Point(104, 93);
            this.edubinaMutacije.Name = "edubinaMutacije";
            this.edubinaMutacije.Size = new System.Drawing.Size(81, 20);
            this.edubinaMutacije.TabIndex = 25;
            this.edubinaMutacije.Text = "7";
            // 
            // edubinaUkrstanja
            // 
            this.edubinaUkrstanja.Location = new System.Drawing.Point(104, 56);
            this.edubinaUkrstanja.Name = "edubinaUkrstanja";
            this.edubinaUkrstanja.Size = new System.Drawing.Size(81, 20);
            this.edubinaUkrstanja.TabIndex = 24;
            this.edubinaUkrstanja.Text = "7";
            // 
            // epocetnaDubinaDrveta
            // 
            this.epocetnaDubinaDrveta.Location = new System.Drawing.Point(104, 22);
            this.epocetnaDubinaDrveta.Name = "epocetnaDubinaDrveta";
            this.epocetnaDubinaDrveta.Size = new System.Drawing.Size(81, 20);
            this.epocetnaDubinaDrveta.TabIndex = 23;
            this.epocetnaDubinaDrveta.Text = "7";
            // 
            // label3
            // 
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 18);
            this.label3.TabIndex = 13;
            this.label3.Tag = "";
            this.label3.Text = "Mutation depth:";
            // 
            // label2
            // 
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 12;
            this.label2.Tag = "";
            this.label2.Text = "Crossover depth:";
            // 
            // label5
            // 
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(8, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 11;
            this.label5.Tag = "";
            this.label5.Text = "Initialize depth:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label37);
            this.groupBox2.Controls.Add(this.evjerojatnostPermutacije);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.evjerojatnostMutacije);
            this.groupBox2.Controls.Add(this.evjerojatnostUkrstanja);
            this.groupBox2.Controls.Add(this.evjerojatnostReprodukcije);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.Reprodukcija);
            this.groupBox2.Location = new System.Drawing.Point(12, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 122);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Probability of gp operations";
            // 
            // label37
            // 
            this.label37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label37.Location = new System.Drawing.Point(446, 24);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(63, 18);
            this.label37.TabIndex = 30;
            this.label37.Tag = "";
            this.label37.Text = " (0,0 -1,00)";
            // 
            // evjerojatnostPermutacije
            // 
            this.evjerojatnostPermutacije.Location = new System.Drawing.Point(360, 21);
            this.evjerojatnostPermutacije.Name = "evjerojatnostPermutacije";
            this.evjerojatnostPermutacije.Size = new System.Drawing.Size(81, 20);
            this.evjerojatnostPermutacije.TabIndex = 29;
            this.evjerojatnostPermutacije.Text = "0,05";
            // 
            // label38
            // 
            this.label38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label38.Location = new System.Drawing.Point(264, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(75, 18);
            this.label38.TabIndex = 28;
            this.label38.Tag = "";
            this.label38.Text = "Permutation:";
            // 
            // label10
            // 
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(190, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 18);
            this.label10.TabIndex = 27;
            this.label10.Tag = "";
            this.label10.Text = " (0,0 -1,00)";
            // 
            // label11
            // 
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(190, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 18);
            this.label11.TabIndex = 26;
            this.label11.Tag = "";
            this.label11.Text = " (0,0 -1,00)";
            // 
            // label15
            // 
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(190, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 18);
            this.label15.TabIndex = 23;
            this.label15.Tag = "";
            this.label15.Text = " (0,0 -0,50)";
            // 
            // evjerojatnostMutacije
            // 
            this.evjerojatnostMutacije.Location = new System.Drawing.Point(104, 57);
            this.evjerojatnostMutacije.Name = "evjerojatnostMutacije";
            this.evjerojatnostMutacije.Size = new System.Drawing.Size(81, 20);
            this.evjerojatnostMutacije.TabIndex = 25;
            this.evjerojatnostMutacije.Text = "0,25";
            // 
            // evjerojatnostUkrstanja
            // 
            this.evjerojatnostUkrstanja.Location = new System.Drawing.Point(104, 20);
            this.evjerojatnostUkrstanja.Name = "evjerojatnostUkrstanja";
            this.evjerojatnostUkrstanja.Size = new System.Drawing.Size(81, 20);
            this.evjerojatnostUkrstanja.TabIndex = 24;
            this.evjerojatnostUkrstanja.Text = "0,95";
            // 
            // evjerojatnostReprodukcije
            // 
            this.evjerojatnostReprodukcije.Location = new System.Drawing.Point(104, 89);
            this.evjerojatnostReprodukcije.Name = "evjerojatnostReprodukcije";
            this.evjerojatnostReprodukcije.Size = new System.Drawing.Size(81, 20);
            this.evjerojatnostReprodukcije.TabIndex = 23;
            this.evjerojatnostReprodukcije.Text = "0,20";
            // 
            // label16
            // 
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(8, 60);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 18);
            this.label16.TabIndex = 13;
            this.label16.Tag = "";
            this.label16.Text = "Mutation:";
            // 
            // label17
            // 
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(8, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 18);
            this.label17.TabIndex = 12;
            this.label17.Tag = "";
            this.label17.Text = "Crossover:";
            // 
            // Reprodukcija
            // 
            this.Reprodukcija.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Reprodukcija.Location = new System.Drawing.Point(8, 92);
            this.Reprodukcija.Name = "Reprodukcija";
            this.Reprodukcija.Size = new System.Drawing.Size(78, 18);
            this.Reprodukcija.TabIndex = 11;
            this.Reprodukcija.Tag = "";
            this.Reprodukcija.Text = "Reproduction:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(23, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 18);
            this.label6.TabIndex = 30;
            this.label6.Tag = "";
            this.label6.Text = "Time1";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.SeaGreen;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(138, 322);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(244, 18);
            this.label9.TabIndex = 31;
            this.label9.Tag = "";
            this.label9.Text = "Speedup";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(415, 322);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 18);
            this.label12.TabIndex = 32;
            this.label12.Tag = "";
            this.label12.Text = "Time2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(138, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(81, 20);
            this.textBox1.TabIndex = 34;
            this.textBox1.Text = "100";
            // 
            // label13
            // 
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(45, 93);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 15);
            this.label13.TabIndex = 33;
            this.label13.Text = "Generations:";
            // 
            // BehchmarkAlgoritm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 428);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.evelicinaPopulacije);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "BehchmarkAlgoritm";
            this.Text = "BehchmarkAlgoritm";
            this.Load += new System.EventHandler(this.BehchmarkAlgoritm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox evelicinaPopulacije;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edubinaMutacije;
        private System.Windows.Forms.TextBox edubinaUkrstanja;
        private System.Windows.Forms.TextBox epocetnaDubinaDrveta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox evjerojatnostPermutacije;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox evjerojatnostMutacije;
        private System.Windows.Forms.TextBox evjerojatnostUkrstanja;
        private System.Windows.Forms.TextBox evjerojatnostReprodukcije;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label Reprodukcija;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label13;
    }
}