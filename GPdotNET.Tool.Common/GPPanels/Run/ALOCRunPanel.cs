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
    /// Class implenents simulation of Running GA in Location Alocation Problem
    /// </summary>
    public partial class ALOCRunPanel : BaseRunPanel
    {
        #region Ctor and Fields
        protected LineItem gpDataLine;
        protected LineItem gpModelLine;

        public ALOCRunPanel()
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
            label32.Text = "Optimal Solution";
            
        }

        /// <summary>
        /// On size event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.tboptimalLayout != null)
            {
                var heig = (this.Height / 2) - progressBar1.Height;
                var wei = this.Width - groupBox7.Width - 25;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.tboptimalLayout.Location = new System.Drawing.Point(xPos, heig + groupBox7.Location.Y);
                this.tboptimalLayout.Size = new System.Drawing.Size(wei, heig);
                //progressBar1.Location= new System.Drawing.Point(groupBox7.Location.X,zedModel.Top+zedModel.Height+zedModel.Padding.Top+10);
                //progressBar1.Size=new Size(this.Width-groupBox7.Padding.Left-groupBox7.Padding.Right-10,progressBar1.Height);
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
            if (prevFitness < ch.Fitness )
            {
                var chr = ch;// as GAVChromosome;
                if (chkOptimumType.Checked)
                {
                    if (ch.Fitness != 0)
                        eb_currentFitness.Text = ((1000.0 - ch.Fitness) / ch.Fitness).ToString("#.#####");
                    else
                        eb_currentFitness.Text = "n/a";
                }
                else
                {
                    if (ch.Fitness != 0)
                        eb_currentFitness.Text = ch.Fitness.ToString("#.#####");
                    else
                        eb_currentFitness.Text = "n/a";
                }
                prevFitness = ch.Fitness;
                eb_bestSolutionFound.Text = currentEvoution.ToString();

                var s ="";
                if(ch is GAVChromosome)
                    s= chr.ToString().Split(';')[1].Replace("_", "\t").Replace("\n", Environment.NewLine);
                else
                    s = chr.ToString().Split(';')[1].Replace("_", "\t").Replace(":", Environment.NewLine);

                tboptimalLayout.Text = s;
            }            
        }

        
#endregion

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {

            this.eb_currentIteration.Text = "0";
            this.eb_currentFitness.Text = "0";
            prevFitness = float.MinValue;
            if (gpMaxFitnLine != null)
                gpMaxFitnLine.Clear();
            if (gpAvgFitnLine != null)
                gpAvgFitnLine.Clear();
        }

    }

}
