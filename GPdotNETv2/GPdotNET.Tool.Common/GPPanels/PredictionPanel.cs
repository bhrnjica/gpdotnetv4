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
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace GPdotNET.Tool.Common
{
   /// <summary>
   /// Class implements calculation of best solution agains Test data
   /// </summary>
    public partial class PreditionPanel : UserControl
    {
        #region CTor and Fields
        private LineItem gpDataLine;
        private LineItem gpModelLine;
        private float prevFitnesss = -1;
        private double[][] _trainig;
        public double[][] Training
        {
            get
            {
                return _trainig;
            }
        }

        /// <summary>
        /// CTOR
        /// </summary>
        public PreditionPanel()
        {
            InitializeComponent();
          
            //this.Load+=(x,y)=>
            //{

            //};

            zedModel.GraphPane.Title.Text = "GP Model Prediction";
            zedModel.GraphPane.XAxis.Title.Text = "Samples";
            zedModel.GraphPane.YAxis.Title.Text = "Output";

            gpDataLine = zedModel.GraphPane.AddCurve("Data Points", null, null, Color.Red, ZedGraph.SymbolType.Plus);
            gpDataLine.Symbol.Border = new Border(Color.Green, 0.1f);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            gpModelLine = zedModel.GraphPane.AddCurve("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            gpModelLine.Symbol.Border = new Border(Color.Cyan, 0.1f);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// SHow 2D data in grid
        /// </summary>
        /// <param name="data"></param>
        private void FillGridWithData(double[][] data)
        {
            //persist the data
            _trainig = data;

            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = data[0].Length + 2;
            int numRow = data.Length;

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Pos";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();

                if (i + 1 == numCol)
                    colHeader.Text = "R";
                else if (i + 2 == numCol)
                    colHeader.Text = "Ygp";
                else if (i + 3 == numCol)
                    colHeader.Text = "Y";
                else
                    colHeader.Text = "X" + (i + 1).ToString();

                colHeader.Width = 100;
                listView1.Columns.Add(colHeader);
            }

            for (int j = 0; j < numRow; j++)
            {
                ListViewItem LVI = listView1.Items.Add((j + 1).ToString());

                for (int i = 0; i < numCol; i++)
                {
                    if (i + 1 == numCol)//Ygp
                        LVI.SubItems.Add("-");
                    else if (i + 2 == numCol)//Ygp
                        LVI.SubItems.Add("-");
                    else
                        LVI.SubItems.Add(data[j][i].ToString());
                }
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Update chartz and grid with prediction data
        /// </summary>
        /// <param name="data"></param>
        public void FillPredictionData(double[][]data)
        {
            UpdateChartDataPoint(GetOutputValues(data), false);
            FillGridWithData(data);
        }

        /// <summary>
        /// Returns array of output values
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public double[] GetOutputValues(double[][] data)
        {
            double[] output = new double[data.Length];
            int outputIndex = data[0].Length - 1;
            for (int i = 0; i < data.Length; i++)
            {
                output[i] = data[i][outputIndex];
            }
            return output;
        }

        /// <summary>
        /// Proces of updating chart with new data
        /// </summary>
        /// <param name="y"></param>
        /// <param name="isGPData"></param>
        public void UpdateChartDataPoint(double[] y, bool isGPData)
        {
            if (this.zedModel.GraphPane == null)
                return;

            LineItem li = null;
            if (isGPData)
                li = gpModelLine;
            else
                li = gpDataLine;

            li.Clear();
            for (int i = 0; i < y.Length; i++)
                li.AddPoint(i + 1, y[i]);


            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            this.zedModel.Refresh();
        }

        /// <summary>
        /// Update grid with new data
        /// </summary>
        /// <param name="y"></param>
        public void FillGPPredictionResult(double[] y)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var row = listView1.Items[i];
                //
                double Ygp=y[i];
                row.SubItems[row.SubItems.Count - 2].Text = Math.Round(Ygp, 5).ToString();
                float Ydata = 0;
                if (float.TryParse(row.SubItems[row.SubItems.Count - 3].Text, out Ydata))
                {

                    row.SubItems[row.SubItems.Count - 1].Text = Math.Round(Ydata - Ygp,5).ToString();
                }  
                else
                    row.SubItems[row.SubItems.Count - 1].Text = "-";
            }
        }
       
        /// <summary>
        /// Report progress for every evolution
        /// </summary>
        /// <param name="currentEvolution"></param>
        /// <param name="AverageFitness"></param>
        /// <param name="ch"></param>
        /// <param name="reportType"></param>
        public void ReportProgress(int currentEvolution, float AverageFitness, Engine.GPChromosome ch, int reportType)
        {
            if (prevFitnesss < ch.Fitness)
            {
                prevFitnesss = ch.Fitness;
                var retVal=GPdotNET.Core.Globals.CalculateGPModel(ch.expressionTree, false);
                FillGPPredictionResult(retVal);
                UpdateChartDataPoint(retVal, true);
            }
        }

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevFitnesss = -1;
        }
        #endregion

       
    }
}
