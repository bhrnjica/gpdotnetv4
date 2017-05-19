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
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using GPdotNET.Core.Experiment;
namespace GPdotNET.Tool.Common
{
   /// <summary>
   /// Class implements calculation of best solution agains Test data
   /// </summary>
    public partial class PreditionPanel : UserControl
    {
        #region CTor and Fields
        private List<LineItem> gpDataLine;
        private List<LineItem> gpModelLine;
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
            gpDataLine = new List<LineItem>();
            gpModelLine = new List<LineItem>();

            zedModel.GraphPane.Title.Text = "Prediction";
            zedModel.GraphPane.XAxis.Title.Text = "Samples";
            zedModel.GraphPane.YAxis.Title.Text = "Output";

            var dl = zedModel.GraphPane.AddCurve("Experiment 1", null, null, Color.Red, ZedGraph.SymbolType.Plus);
            dl.Symbol.Border = new Border(Color.Green, 0.1f);
            gpDataLine.Add(dl);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            var ml = zedModel.GraphPane.AddCurve("Prediction 1", null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            ml.Symbol.Border = new Border(Color.Cyan, 0.1f);
            gpModelLine.Add(ml);
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
                    colHeader.Text = "Ymodel";
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

        /// <summary>
        /// SHow 2D data in grid
        /// </summary>
        /// <param name="data"></param>
        private void FillGridWithData(Experiment exp)
        {

            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = exp.GetColumnInputCount() + exp.GetColumnOutputCount();
            int numRow = exp.GetRowCount(true);

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Pos";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            //add experiment column
            var colss = exp.GetColumns(true);
            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();
                colHeader.Text = colss[i].Name;
                
                //if (i + 1 == numCol)
                //    colHeader.Text = exp.get;
                //else if (i + 2 == numCol)
                //    colHeader.Text = "Ymodel";
                //else if (i + 3 == numCol)
                //    colHeader.Text = "Y";
                //else
                //    colHeader.Text = "X" + (i + 1).ToString();

                //colHeader.Width = 100;
                listView1.Columns.Add(colHeader);
            }

            //add predicted output columns
            var outCols = exp.GetColumnsFromOutput(true);
            for (int i = 0; i < exp.GetColumnOutputCount(); i++)
            {
                colHeader = new ColumnHeader();
                colHeader.Text = outCols[i].Name+"_pred";
                colHeader.Width = 100;
                //if (i + 1 == numCol)
                //    colHeader.Text = exp.get;
                //else if (i + 2 == numCol)
                //    colHeader.Text = "Ymodel";
                //else if (i + 3 == numCol)
                //    colHeader.Text = "Y";
                //else
                //    colHeader.Text = "X" + (i + 1).ToString();

                //colHeader.Width = 100;
                listView1.Columns.Add(colHeader);
            }

            for (int j = 0; j < numRow; j++)
            {
                ListViewItem LVI = listView1.Items.Add((j + 1).ToString());
                LVI.UseItemStyleForSubItems = false;
                int i = 0;
                //add experimental value cells
                for (; i < numCol; i++)
                {
                    System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                    itm.Text=colss[i].GetData(j);
                   
                    if (colss[i].IsOutput)
                        itm.ForeColor = Color.Red;
                    LVI.SubItems.Add(itm);                 
                }
                //add predicted cells
                for (; i < numCol+outCols.Count; i++)
                {
                    System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                    itm.Text = "-";
                    itm.ForeColor = Color.Blue;
                    LVI.SubItems.Add(itm);
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
        Experiment Experiment { get; set; }
        /// <summary>
        /// Update chartz and grid with prediction data
        /// </summary>
        /// <param name="data"></param>
        public void FillPredictionData(Experiment expData)
        {
            Experiment = expData;
            double[][] y=Experiment.GetColumnOutputValues(true);
            UpdateChartDataPoint(y, false);

            double[][] data = Experiment.GetColumnAllValues(true);
            FillGridWithData(expData);
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
                li = gpModelLine[0];
            else
                li = gpDataLine[0];

            li.Clear();
            for (int i = 0; y!=null && i < y.Length; i++)
                li.AddPoint(i + 1, y[i]);

            if (this.zedModel.GraphPane.CurveList.Count == 1)


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

                if (y == null)
                    return;
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
        /// Proces of updating chart with new data
        /// </summary>
        /// <param name="y"></param>
        /// <param name="isGPData"></param>
        public void UpdateChartDataPoint(double[][] y, bool isGPData)
        {
            if (this.zedModel.GraphPane == null)
                return;

            //Add aditional lines
            if(!isGPData)
            {
                InitChart(y);
                if(gpModelLine!=null && gpModelLine.Count>0)
                {
                    foreach (var l in gpModelLine)
                        l.Clear();
                }
            }
               
            for (int j = 0; y!=null && j < y.Length; j++)
            {
                LineItem li = null;
                if (isGPData)
                    li = gpModelLine[j];
                else
                    li = gpDataLine[j];

                li.Clear();
                for (int i = 0; i < y[j].Length; i++)
                    li.AddPoint(i + 1, y[j][i]);
            }
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            this.zedModel.Refresh();
        }

        private void InitChart(double[][] y)
        {
            if(y!=null && y.Length>1)
            {
                for(int i=1; i<y.Length; i++)
                {
                    var dl = zedModel.GraphPane.AddCurve("Experiment "+(i+1).ToString(), null, null, Color.Red, ZedGraph.SymbolType.Plus);
                    dl.Symbol.Border = new Border(Color.Green, 0.1f);
                    gpDataLine.Add(dl);
                    this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

                    var ml = zedModel.GraphPane.AddCurve("Prediction " + (i + 1).ToString(), null, null, Color.Blue, ZedGraph.SymbolType.Plus);
                    ml.Symbol.Border = new Border(Color.Cyan, 0.1f);
                    gpModelLine.Add(ml);
                    this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
                }
                
            }
           
        }
        /// <summary>
        /// Update grid with new data
        /// </summary>
        /// <param name="y"></param>
        public void FillGPPredictionResult(double[][] y)
        {
            // FillGPPredictionResult(y[0]);
            if(Experiment==null && y!=null && y.Length>0)
            {
                FillGPPredictionResult(y[0]);
                return;
            }

            var colOut=Experiment.GetColumnOutputCount();
             var cols= Experiment.GetColumnsFromOutput();
             for (int i = 0; i < listView1.Items.Count; i++)
             {
                 var row = listView1.Items[i];
                 for (int j = 0; j < colOut; j++)
                 {
                     var c = cols[j];
                     double Ymodel = y[j][i];
                     if (c.ColumnDataType== ColumnDataType.Categorical)
                     {
                       var str=  c.GetCategoryFromNumeric(Ymodel, null);
                       row.SubItems[row.SubItems.Count - colOut + j].Text = str;
                     }
                     else if(c.ColumnDataType== ColumnDataType.Binary)
                     {
                         var str = c.GetBinaryClassFromNumeric(Ymodel, null);
                         row.SubItems[row.SubItems.Count - colOut + j].Text = str;
                     }
                     else
                        row.SubItems[row.SubItems.Count - colOut+j].Text = Math.Round(Ymodel, 5).ToString();
                 }
             }
        }
        /// <summary>
        /// Report progress for every evolution
        /// </summary>
        /// <param name="currentEvolution"></param>
        /// <param name="AverageFitness"></param>
        /// <param name="ch"></param>
        /// <param name="reportType"></param>
        public void ReportProgress(int currentEvolution, float AverageFitness, Engine.GPChromosome ch, int reportType, double[][] prediction)
        {
            if (ch == null)
                return;
            if (prevFitnesss < ch.Fitness)
            {
                prevFitnesss = ch.Fitness;
                if (prediction == null)
                {
                    var retVal = GPdotNET.Core.Globals.CalculateGPModel(ch.expressionTree, false);
                    if(retVal==null)
                    {
                        FillGPPredictionResult(retVal);
                        UpdateChartDataPoint(retVal, true);
                    }
                   

                }
                else
                {
                    
                    FillGPPredictionResult(prediction);
                    UpdateChartDataPoint(prediction[0], true);
                }

                
            }
        }

        /// <summary>
        /// Report progress for every evolution
        /// </summary>
        /// <param name="currentEvolution"></param>
        /// <param name="AverageFitness"></param>
        /// <param name="ch"></param>
        /// <param name="reportType"></param>
        public void ReportANNProgress(int currentEvolution, float predictionError, double[][] result, int reportType)
        {
            if (result == null)
                return;
            if (prevFitnesss > predictionError || prevFitnesss==-1)
            {
                prevFitnesss = predictionError;
               //
                FillGPPredictionResult(result);
                UpdateChartDataPoint(result, true);
            }
        }
        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevFitnesss = -1;
            UpdateChartDataPoint((double[][])null, true);
        }
        #endregion

       
    }
}
