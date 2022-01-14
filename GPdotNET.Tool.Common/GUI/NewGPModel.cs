using GPdotNET.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common
{
    public partial class NewGPModel : Form
    {

        public GPModelType ModelType
        {
            get
            {
                if(checkBox1.Checked)
                {
                    if (comboBox1.SelectedIndex==0)
                        return GPModelType.SR;
                    else if (comboBox1.SelectedIndex == 1)
                        return GPModelType.SRO;
                    else if (comboBox1.SelectedIndex == 2)
                        return GPModelType.TS;
                    else if (comboBox1.SelectedIndex == 3)
                        return GPModelType.AO;
                    else if (comboBox1.SelectedIndex == 4)
                        return GPModelType.TSP;
                    else if (comboBox1.SelectedIndex == 5)
                        return GPModelType.AP;
                    else if (comboBox1.SelectedIndex == 6)
                        return GPModelType.TP;

                    else
                        return GPModelType.SR;
                }
                else
                {
                    if (comboBox2.SelectedIndex == 0)
                        return GPModelType.GPMODEL;
                    else //(comboBox2.SelectedIndex == 1)
                        return GPModelType.ANNMODEL;

                }
                
            }
        }
        public NewGPModel()
        {
            InitializeComponent();
            this.pictureBox1.Image = GPModelGlobals.LoadImageFromName("GPdotNET.App.Resources.gpdotnet_ico48.png");
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                comboBox2.Enabled = false;
                comboBox1.Enabled = true;
            }
            else
            {
                checkBox2.Checked = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = true;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = true;
            }
            else
            {
                checkBox1.Checked = true;
                comboBox2.Enabled = false;
                comboBox1.Enabled = true;
            }

        }
    }
}
