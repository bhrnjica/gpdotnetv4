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
                var wei = this.Width - groupBox7.Width - 35;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.zedModel.Location = new System.Drawing.Point(xPos, heig + groupBox7.Location.Y);
                this.zedModel.Size = new System.Drawing.Size(wei, heig);
            }

        }
        #endregion

        #region Public Methdos
        /// <summary>
        /// Report progress opf current evolution
        /// </summary>
        /// <param name="currentEvoution"></param>
        /// <param name="avgFitness"></param>
        /// <param name="ch"></param>
        /// <param name="runType"></param>
        public override void ReportProgress(int currentEvoution, float avgFitness, IChromosome ch, int runType)
        {
            if (ch == null)
                return;
            base.ReportProgress(currentEvoution, avgFitness, ch, runType);

            

            //When fitness is changed, model needs to be refreshed
            if (prevFitness < ch.Fitness && ch is GPChromosome)
            {
                var chr = ch as GPChromosome;
                currentErrorBox.Text = ch.Fitness.ToString();
                prevFitness = ch.Fitness;
                bestFitnessAtGenerationEditBox.Text = currentEvoution.ToString();

                var pts = GPdotNET.Core.Globals.CalculateGPModel(chr.expressionTree);
                UpdateChartDataPoint(pts);

            }

            
        }

        /// <summary>
        /// Uodate GP Model cahrt when data or GP model is changed
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
        /// Deserilization of run condition
        /// </summary>
        /// <param name="p"></param>
        public void SetTypeofRun(string p)
        {
            var funs = p.Split(';');
            comboBox2.SelectedIndex = funs[0] == "1" ? 1 : 0;
            brojIteracija.Text = funs[1];
            
        }

        /// <summary>
        /// Serilization of run condition
        /// </summary>
        /// <returns></returns>
        public string GetTypeofRun()
        {

            if (comboBox2.SelectedIndex != -1) 
            {
                string str = comboBox2.SelectedIndex.ToString() + ";" + brojIteracija.Text;
                return str;
            }
            return null;
           

        }

#endregion

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevFitness = float.MinValue;
        }

        /// <summary>
        /// Check if there is a prev solution
        /// </summary>
        /// <returns></returns>
        public bool HasPrevSoluton()
        {
            return prevFitness >= 0;
        }
    }

}
