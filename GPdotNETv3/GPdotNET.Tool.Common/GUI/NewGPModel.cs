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
                if (pageOneLabelrad1.Checked)
                    return GPModelType.SymbolicRegression;
                else if (pageOneLabelrad2.Checked)
                    return GPModelType.SymbolicRegressionWithOptimization;
                else if (pageOneLabelrad3.Checked)
                    return GPModelType.TimeSeries;
                else if (pageOneLabelrad4.Checked)
                    return GPModelType.AnaliticFunctionOptimization;
                else if (pageOneLabelrad5.Checked)
                    return GPModelType.TSP;
                else if (pageOneLabelrad6.Checked)
                    return GPModelType.ALOC;
                else if (pageOneLabelrad7.Checked)
                    return GPModelType.Transport;
                
                else
                    return GPModelType.SymbolicRegression;
            }
        }
        public NewGPModel()
        {
            InitializeComponent();
            this.pictureBox1.Image = GPModelGlobals.LoadImageFromName("GPdotNET.App.Resources.gpdotnet_ico48.png");
        }
    }
}
