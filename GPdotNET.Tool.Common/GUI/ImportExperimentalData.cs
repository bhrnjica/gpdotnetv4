using GPdotNET.Core.Experiment;
using GPdotNET.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common
{
    public partial class ImportExperimentalData : Form
    {
        private string originData = "";
        public ImportExperimentalData()
        {
            InitializeComponent();
        }

        //Import file
        private void button1_Click(object sender, EventArgs e)
        {
            var strFile = GPModelGlobals.GetFileFromOpenDialog("","");
            if (strFile == null)
                return;
            var data = string.Join(Environment.NewLine, File.ReadAllLines(strFile).Where(l => !l.StartsWith("#") && !l.StartsWith("!")));
            originData = data;
            textBox3.Text = data;
            ProcesData();
            
            if (!string.IsNullOrEmpty(data))
                button2.Enabled = true;
        }

        private void ProcesData()
        {
            var data = originData;
            if (string.IsNullOrEmpty(data))
                return;

            if (checkBox2.Checked)
                data = data.Replace(";", "\t|\t");
            if (checkBox3.Checked)
                data = data.Replace(",", "\t|\t");
            if (checkBox4.Checked)
                data = data.Replace(" ", "\t|\t");
            if (checkBox6.Checked)
                data = data.Replace("\t", "\t|\t");
            if (checkBox5.Checked)
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                    data = data.Replace(textBox2.Text[0], '|');
            }

            //if header is present separate data with horizontal line
            if (checkBox1.Checked)
            {
                var index = data.IndexOf(Environment.NewLine);
                var index2 = data.IndexOf(Environment.NewLine, index + 1);
                int counter=0;
                while(counter<index2-index)
                {
                    data=data.Insert(index,"-");
                    counter++;
                }
                data = data.Insert(index, Environment.NewLine);
            }
            

            textBox3.Text = data;
        }
        
        public Experiment ExperimentalData
        {
            get;set;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("No file is selected!");
                return;
            }
                
            ExperimentalData = new Experiment();

            var colDelimiter = GetColumDelimiter();

            ExperimentalData.LoadExperiment(originData, colDelimiter, checkBox1.Checked,radioButton1.Checked);
        }


        private char[] GetColumDelimiter()
        {
            var col = new List<char>();

            if (checkBox2.Checked)
                col.Add(';');
            if (checkBox3.Checked)
                col.Add(',');
            if (checkBox4.Checked)
                col.Add(' ');
            if (checkBox6.Checked)
                col.Add('\t');
            if (checkBox5.Checked)
                col.Add(textBox2.Text[0]);

            return col.ToArray();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var ch = sender as CheckBox;
            if (ch.Name == "checkBox5")
            {
                if (ch.Checked)
                    textBox2.Enabled = true;
                else
                {
                    textBox2.Text = "";
                    textBox2.Enabled = false;
                }
            }
            ProcesData();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
