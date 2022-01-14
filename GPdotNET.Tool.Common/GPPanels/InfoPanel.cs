//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2012 Bahrudin Hrnjica                                                 //
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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using GPdotNET.Core;
using GPdotNET.Util;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Panel for selecting which primitive programs will be included in to GP
    /// </summary>
    public partial class InfoPanel : UserControl
    {
        #region Ctor and Fields
        public string InfoText
        {
            get 
            {
                return richTextBox1.Rtf;
            }
            set
            {
                richTextBox1.Rtf = value;
            }
        }
        public InfoPanel()
        {
            InitializeComponent();
           
            if (this.DesignMode)
                return;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var strPath = GPModelGlobals.GetFileFromOpenDialog("Rich text files ", "*.rtf");

            if (strPath!=null)
            {
                richTextBox1.LoadFile(strPath, RichTextBoxStreamType.RichText);
            }
        }

      
    }
}
