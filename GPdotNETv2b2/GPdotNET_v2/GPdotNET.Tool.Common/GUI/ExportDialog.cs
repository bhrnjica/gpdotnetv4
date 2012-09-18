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
        }
        public int SelectedOption
        {
            get 
            {
                return listBox1.SelectedIndex;
            }
        }
    }
}
