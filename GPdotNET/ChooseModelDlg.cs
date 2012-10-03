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
