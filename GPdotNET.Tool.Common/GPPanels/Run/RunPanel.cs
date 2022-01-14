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
using GPdotNET.Core;
using GPdotNET.Engine;
using ZedGraph;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Class implenents simulation of Running GP
    /// </summary>
    public partial class RunPanel : BaseRunPanel
    {
        #region Ctor and Fields
        protected LineItem gpDataLine;
        protected LineItem gpModelLine;

        public RunPanel()
        {
            InitializeComponent();
            PrepareGraphs();
        }
        #endregion
 
        #region Protected and Private methods

        /// <summary>
        /// Initi props for charts
        /// </summary>
        protected override void PrepareGraphs()
        {
            base.PrepareGraphs();

            zedModel.GraphPane.Title.Text = "GP Model Simulation";
            zedModel.GraphPane.XAxis.Title.Text = "Samples";
            zedModel.GraphPane.YAxis.Title.Text = "Output";

            gpDataLine = zedModel.GraphPane.AddCurve("Data Points", null, null, Color.Red, ZedGraph.SymbolType.Plus);
            gpDataLine.Symbol.Border = new Border(Color.Green, 0.1f);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            gpModelLine = zedModel.GraphPane.AddCurve("GP Model", null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            gpModelLine.Symbol.Border = new Border(Color.Cyan, 0.1f);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
        }

        /// <summary>
        /// On size event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.zedModel != null)
            {
                var heig = (this.Height / 2) - progressBar1.Height;
                var wei = this.Width - groupBox7.Width - 25;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.zedModel.Location = new System.Drawing.Point(xPos, heig + groupBox7.Location.Y);
                this.zedModel.Size = new System.Drawing.Size(wei, heig);
                //progressBar1.Location= new System.Drawing.Point(groupBox7.Location.X,zedModel.Top+zedModel.Height+zedModel.Padding.Top+10);
                //progressBar1.Size=new Size(this.Width-groupBox7.Padding.Left-groupBox7.Padding.Right-10,progressBar1.Height);
            }

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Report progress of current evolution
        /// </summary>
        /// <param name="currentEvoution"></param>
        /// <param name="avgFitness"></param>
        /// <param name="ch"></param>
        /// <param name="runType"></param>
        public void ReportProgress(int currentEvoution, float avgFitness, IChromosome ch, int runType, double [][] model)
        {
            if (ch == null)
                return;

            ReportProgress(currentEvoution, avgFitness, ch, runType);

            //When fitness is changed, model needs to be refreshed
            if (prevFitness <= ch.Fitness && ch is GPChromosome)
            {
                var chr = ch as GPChromosome;
                eb_currentFitness.Text = ch.Fitness.ToString("#.#####");
                prevFitness = ch.Fitness;
                eb_bestSolutionFound.Text = eb_currentIteration.Text;
                if(model==null)
                {
                    var pts = GPdotNET.Core.Globals.CalculateGPModel(chr.expressionTree);
                    UpdateChartDataPoint(pts);

                }
                else
                  UpdateChartDataPoint(model[0]);

            }

            
        }

        /// <summary>
        /// Uodate GP Model chart when data or GP model is changed
        /// </summary>
        /// <param name="y">output value</param>
        /// <param name="gpModel"> indicator is it about GPMOdel or Data Point</param>
        public void UpdateChartDataPoint(double[] y, bool gpModel=true)
        {
            if (this.zedModel.GraphPane == null)
                return;

            LineItem li = null;
            if (gpModel)
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
        /// Uodate GP Model chart when data or GP model is changed
        /// </summary>
        /// <param name="y">output value</param>
        /// <param name="gpModel"> indicator is it about GPMOdel or Data Point</param>
        public void UpdateChartDataPoint(double[][] y, bool gpModel = true)
        {
            if (this.zedModel.GraphPane == null || y == null)
                return;

            LineItem li = null;
            if (gpModel)//ann calculated data
            {
                li = gpModelLine;
                li.Clear();
                for (int i = 0; i < y[0].Length; i++)
                    li.AddPoint(i + 1, y[0][i]);
            }

            else//experimental data
            {
                li = gpDataLine;
                li.Clear();
                for (int i = 0; i < y.Length; i++)
                    li.AddPoint(i + 1, y[i][1]);
            }


            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
            this.zedModel.Refresh();



        }


        #endregion

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevFitness = float.MinValue;
            if (gpMaxFitnLine != null)
                gpMaxFitnLine.Clear();
            if (gpAvgFitnLine != null)
                gpAvgFitnLine.Clear();
            if (gpModelLine != null)
                gpModelLine.Clear();

            eb_currentIteration.Text = "0";
            eb_currentFitness.Text = "";
            eb_bestSolutionFound.Text = 0.ToString();
            eb_timeStart.Text = "";
            eb_timePerRun.Text = "";
            eb_timeToCompleate.Text = "";
            eb_durationInMin.Text = "";
            eb_timeleft.Text = "";
            
        }

        /// <summary>
        /// Check if there is a prev solution
        /// </summary>
        /// <returns></returns>
        public new bool HasPrevSoluton()
        {
            return prevFitness >= 0;
        }
    }

}
