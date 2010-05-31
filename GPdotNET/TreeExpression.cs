//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2010 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
////////////////////////////////////////////////////////////////////////////////////////// 
using System;
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
