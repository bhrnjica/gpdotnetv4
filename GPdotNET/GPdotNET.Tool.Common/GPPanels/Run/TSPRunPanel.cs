//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2013 Bahrudin Hrnjica                                                 //
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
    /// Class implenents simulation of Running GA in TSP Problem
    /// </summary>
    public partial class TSPRunPanel : BaseRunPanel
    {
        #region Ctor and Fields
        protected LineItem gpDataLine;
        protected LineItem gpModelLine;

        public TSPRunPanel()
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
            label32.Text = "Shortest Path found";
            
        }

        /// <summary>
        /// On size event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.cityMapDrawer != null)
            {
                var height = (this.Height / 2) - progressBar1.Height;
                var width = this.Width - groupBox7.Width - 25;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.cityMapDrawer.Location = new System.Drawing.Point(xPos, height + groupBox7.Location.Y + 5);
                this.cityMapDrawer.Size = new System.Drawing.Size(width, height-tbShortestPath.Height-15);
				
			
				//label
            	this.label1.Location = new System.Drawing.Point(groupBox7.Location.X, groupBox7.Location.Y+groupBox7.Height+groupBox7.Padding.Top);
            	this.label1.Size = new System.Drawing.Size(97, 17);
				this.tbShortestPath.Location = new System.Drawing.Point(label1.Width+groupBox7.Location.X, groupBox7.Location.Y+groupBox7.Height+groupBox7.Padding.Top-3);
                this.tbShortestPath.Size = new System.Drawing.Size(this.Width - label1.Width - 15, 22);
				
				//progressBar1.Size=new Size(this.Width-groupBox7.Padding.Left-groupBox7.Padding.Right-5,10);
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
            if (prevFitness < ch.Fitness && ch is GAVChromosome)
            {
                var chr = ch as GAVChromosome;
                if(ch.Fitness!=0)
                    eb_currentFitness.Text = ((1000.0 - ch.Fitness) / ch.Fitness).ToString("#.#####");
                else
                    eb_currentFitness.Text = "n/a";
                prevFitness = ch.Fitness;
                eb_bestSolutionFound.Text = currentEvoution.ToString();
                tbShortestPath.Text = chr.ToString().Split(';')[1];
                DrawPath(chr.Value);

              
            }

            
        }
        
        /// <summary>
        /// Drwaing City Map with coordiantes
        /// </summary>
        /// <param name="p"></param>
        public void DrawCityMap(double[][] data)
        {
            if (data.Length > 100)
                return;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    //data[i][0] - x coordinate
                    //data[i][1] - y coordinate

                    //finding max value
                    if (data[i][0] > maxX)
                        maxX = (int)Math.Ceiling(data[i][0]);
                    if (data[i][1] > maxY)
                        maxY = (int)Math.Ceiling(data[i][1]);

                    //finding min value
                    if (data[i][0] < minX)
                        minX = (int)Math.Floor(data[i][0]);
                    if (data[i][1] < minY)
                        minY = (int)Math.Floor(data[i][1]);
                }
            }

            cityMapDrawer.SetData(maxX, maxY, minX, minY, data);
            cityMapDrawer.Invalidate();

        }

        public void DrawPath(int[] path)
        {
            //var _path = new int[20] { 1, 3, 7, 5, 4, 2, 6, 8, 9, 10, 11, 13, 12, 14, 15, 16, 17, 18, 19, 0 };
            cityMapDrawer.SetPath(path);
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
        }
    }

}
