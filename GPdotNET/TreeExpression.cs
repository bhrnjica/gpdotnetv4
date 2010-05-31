// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// GPdotNET 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET
{
    public partial class TreeExpression : Form
    {
        public gpWpfTreeDrawerLib.wpfTreeDrawerCtrl TreeDrawer
        {
            get
            { return wpfTreeDrawerCtrl1; }
        }
        public TreeExpression()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog slv = new SaveFileDialog();
            slv.DefaultExt = "Bmp";
            slv.FileName = DateTime.UtcNow.Ticks.ToString();
            slv.Filter = "Bmp Images|*.bmp";
            slv.FilterIndex = 2;
            slv.Title = "Save an Image File";
            slv.RestoreDirectory = true;
            DialogResult ret = slv.ShowDialog();
            
            if(ret== DialogResult.OK)
                TreeDrawer.SaveAsBitmap(slv.FileName);
        }
    }
}
