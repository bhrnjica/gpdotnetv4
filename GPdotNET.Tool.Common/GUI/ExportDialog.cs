using GPdotNET.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common.GUI
{
    public partial class ExportDialog : Form
    {
        public ExportDialog()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 1;
            this.pictureBox1.Image = GPModelGlobals.LoadImageFromName("GPdotNET.App.Resources.gpdotnet_ico48.png");
        }

        public bool isAnnModelExport { get; set; }

        public int SelectedOption
        {
            get 
            {
                return listBox1.SelectedIndex;
            }
        }

        public string SelectedItem
        {
            get
            {
                return listBox1.SelectedItem.ToString();
            }
        }

        private void ExportDialog_Load(object sender, EventArgs e)
        {
            if(isAnnModelExport)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Excel");
            }
            listBox1.SelectedItem = "Excel";
        }
    }
}
