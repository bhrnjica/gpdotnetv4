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
                else
                    return GPModelType.SymbolicRegression;
            }
        }
        public NewGPModel()
        {
            InitializeComponent();
        }
    }
}
