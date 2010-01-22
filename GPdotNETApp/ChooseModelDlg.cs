using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNETApp
{
    public partial class ChooseModelDlg : Form
    {
        public bool TimeSeries{get;set;}

       
        public ChooseModelDlg()
        {
            InitializeComponent();
        }
        private void ChooseModelDlg_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void ChooseModelDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioButton1.Checked)
                TimeSeries = false;
            else
                TimeSeries = true;
        }
    }
}
