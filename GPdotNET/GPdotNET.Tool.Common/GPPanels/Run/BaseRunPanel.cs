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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using GPdotNET.Core;
using ZedGraph;
using System.Globalization;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// This class is base class for running program. It contains basic properties and methods for manitoring and reporting during running GP or GA
    /// </summary>
    public partial class BaseRunPanel : UserControl
    {
        #region Ctor and Fields 
        protected LineItem gpAvgFitnLine;
        protected LineItem gpMaxFitnLine;
        protected DateTime m_startTime;
        protected float prevFitness = float.MinValue;
        protected DateTime prevIterationTime;

        public float TerminationValue
        {
            get
            {
                float val= 500;
                if (float.TryParse(m_eb_iterations.Text, out val))
                    return val;
                else
                    return 500;
            }
        }
        public int TerminationType 
        {
            get
            {

              return  comboBox2.SelectedIndex;
            }
        }

        public BaseRunPanel()
        {
            InitializeComponent();
            comboBox2.SelectedIndex = 0; 

        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Method is calld whe the panel need to be prepared for running program.
        /// Methods initialize and prepare grapgs for receiving points
        /// </summary>
        protected virtual void PrepareGraphs()
        {

            ///Fitness simulation chart
            zedFitness.GraphPane.Title.Text = "GP Fitness Simulation";
            zedFitness.GraphPane.XAxis.Title.Text = "Evolution";
            zedFitness.GraphPane.YAxis.Title.Text = "Value";

            gpMaxFitnLine = zedFitness.GraphPane.AddCurve("Maximum", null, null, Color.Red, ZedGraph.SymbolType.None);
            gpMaxFitnLine.Symbol.Border = new Border(Color.Green, 0.1f);
            this.zedFitness.GraphPane.AxisChange(this.CreateGraphics());

            gpAvgFitnLine = zedFitness.GraphPane.AddCurve("Average", null, null, Color.Blue, ZedGraph.SymbolType.None);
            gpAvgFitnLine.Symbol.Border = new Border(Color.Cyan, 0.1f);
            this.zedFitness.GraphPane.AxisChange(this.CreateGraphics());
        }

        /// <summary>
        /// Virtual method for positioning graphs on the right place and position
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.zedFitness != null)
            {

                var heig = (this.Height / 2) - progressBar1.Height;
                var wei = this.Width - groupBox7.Width - 25;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.zedFitness.Location = new System.Drawing.Point(xPos, groupBox7.Location.Y);
                this.zedFitness.Size = new System.Drawing.Size(wei, heig);

            }

        }

        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public virtual void EnableCtrls(bool p)
        {
            comboBox2.Enabled = p;
            m_eb_iterations.Enabled = p;
        }

        /// <summary>
        /// One of three report methods during the run of programm.
        /// Caled when the Program ids finished
        /// </summary>
        public virtual void FinishProgress()
        {
            if (comboBox2.SelectedIndex == 1)
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Maximum = 100;
                progressBar1.Minimum = 0;
                progressBar1.Value = 100;
            }
            else
            {

                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Maximum = 100;
                progressBar1.Minimum = 0;
                progressBar1.Value = 100;
            }

            progressBar1.MarqueeAnimationSpeed = 0;

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update current fitness simulation 
        /// </summary>
        /// <param name="currEvolution">current evolution of GP Model </param>
        /// <param name="max">maximum fitness value of the current evolution</param>
        /// <param name="avg">average fitness value of the current evolution</param>
        public void UpdateChartFitness(int currEvolution, double max, double avg, bool isFinished = false)
        {

            if (currEvolution + 1 < gpMaxFitnLine.Points.Count)
                gpMaxFitnLine.Clear();

            if (currEvolution + 1 < gpAvgFitnLine.Points.Count)
                gpAvgFitnLine.Clear();

            if(gpMaxFitnLine.Points.Count <= currEvolution)
            {
                gpMaxFitnLine.AddPoint(currEvolution, max);
                gpAvgFitnLine.AddPoint(currEvolution, avg);
            }
            if (currEvolution % 10 == 0 || isFinished)
                zedFitness.RestoreScale(zedFitness.GraphPane);
        }


        

        /// <summary>
        /// One of three report methods during the run of programm.
        /// This methods is called when the evolution starts or population created.
        /// </summary>
        /// <param name="val"></param>
        public void StartProgress(string val)
        {
            progressBar1.MarqueeAnimationSpeed = 10;
            if (comboBox2.SelectedIndex == 1)
            {
                //progressBar1.DisplayStyle = ProgressBarDisplayText.CustomText;
                //progressBar1.CustomText = val+"/"+ "1000.00";
                //progressBar1.Style = ProgressBarStyle.Marquee;
                //progressBar1.MarqueeAnimationSpeed = 40;

            }
            else
            {
                int max = 500;
                if (!int.TryParse(m_eb_iterations.Text, out max))
                    max = 500;
                progressBar1.Style = ProgressBarStyle.Blocks;
                // progressBar1.DisplayStyle = ProgressBarDisplayText.Percentage;
                progressBar1.Maximum = max;
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
            }

            m_startTime = DateTime.Now;
            prevIterationTime = m_startTime;

            //we dont overide start and duration time
            if (string.IsNullOrEmpty(eb_timeStart.Text))
            {
                eb_timeStart.Text = m_startTime.ToString();
                eb_durationInMin.Text = "0";
            }

        }

        /// <summary>
        /// Report method. It is called every time when the evolution is finish. It report about properties which need to be show on screen 
        /// </summary>
        /// <param name="currentEvoution">Number of evolution passed.</param>
        /// <param name="avgFitness">Average fiutness</param>
        /// <param name="ch"> Best shromosome found in current evolution</param>
        /// <param name="runType"></param>
        public virtual void ReportProgress(int currentEvoution, float avgFitness, IChromosome ch, int runType)
        {
            if (ch == null)
                return;
            var totEvolution = currentEvoution;
            if (currentEvoution == 0)
                totEvolution = (int.Parse(string.IsNullOrEmpty(eb_currentIteration.Text) ? "0" : eb_currentIteration.Text));

            eb_currentIteration.Text = totEvolution.ToString();

            if (runType == 3)
                UpdateChartFitness(totEvolution, ch.Fitness, avgFitness, true);
            else
                UpdateChartFitness(totEvolution, ch.Fitness, avgFitness, false);

            if (runType == 1)
                StartProgress(ch.Fitness.ToString());
            else if (runType == 3)
                FinishProgress();
            else
            {
                if (comboBox2.SelectedIndex == 0)
                    progressBar1.Value = currentEvoution;
            }

            if (runType == 2)
            {
                //calculation time
                var currIterationTime = DateTime.Now - prevIterationTime;
                double sec = currIterationTime.TotalSeconds;

                eb_timePerRun.Text = Math.Round(sec, 2).ToString();

                if (comboBox2.SelectedIndex == 1)
                {
                    eb_timeToCompleate.Text = "undefine";
                    eb_timeleft.Text = "undefine";
                }
                else
                {
                    int broj = 0;
                    if (!int.TryParse(m_eb_iterations.Text, out broj))
                        broj = 500;

                    DateTime datToFinish = m_startTime.AddSeconds(sec * (broj - currentEvoution));

                    eb_timeToCompleate.Text = datToFinish.ToString();
                    eb_timeleft.Text = Math.Round((datToFinish - m_startTime).TotalMinutes, 3).ToString();

                }
                double durationMinutes = 0;
                if(!double.TryParse(eb_durationInMin.Text.Replace(',', '.'),NumberStyles.Float, CultureInfo.InvariantCulture, out durationMinutes))
                {
                    if (eb_durationInMin.Text.Contains("."))
                        eb_durationInMin.Text = eb_durationInMin.Text.Replace(".",",");
                    else
                        eb_durationInMin.Text = eb_durationInMin.Text.Replace(",", ".");

                    //
                    double.TryParse(eb_durationInMin.Text, out durationMinutes);
                }


                eb_durationInMin.Text = Math.Round((durationMinutes + (sec / 60.0)), 2).ToString();

                //chage previous time in to current for the next iteration
                prevIterationTime = DateTime.Now;
            }

        }

        /// <summary>
        /// Deserilization of run condition
        /// </summary>
        /// <param name="p"></param>
        public void SetTypeofRun(string p)
        {
            var funs = p.Split(';');
            comboBox2.SelectedIndex = funs[0] == "1" ? 1 : 0;
            if(funs.Length<2)
                m_eb_iterations.Text = "500";
            else
                m_eb_iterations.Text = funs[1];
            
            if (funs.Length>2)
                chkOptimumType.Checked = funs[2] == "1" ? true : false;
            if (funs.Length > 3)
                eb_currentIteration.Text = funs[3];
            if (funs.Length > 4)
                eb_currentFitness.Text = funs[4];
            if (funs.Length > 5)
                eb_maximumFitness.Text = funs[5];
            if (funs.Length > 6)
                eb_maximumFitness.Text = funs[6];
            if (funs.Length > 7)
                eb_bestSolutionFound.Text = funs[7];
            if (funs.Length > 8)
                eb_timeStart.Text = funs[8];
            if (funs.Length > 9)
                eb_timePerRun.Text = funs[9];
            if (funs.Length > 10)
                eb_timeToCompleate.Text = funs[10];
            if (funs.Length > 11)
                eb_timeleft.Text = funs[11];
            if (funs.Length > 12)
                eb_durationInMin.Text = funs[12];
            if (funs.Length > 13)
                fillFitness(funs[13]);
        }

        /// <summary>
        /// creates fitness graphs from string points
        /// </summary>
        /// <param name="v"></param>
        private void fillFitness(string v)
        {
            if (v == "\r")
                return;
            var strs = v.Split('#');
            for (int i = 0; i < strs.Length; i++)
            {
                var ss = strs[i].Split(':');
                double x  = double.Parse(ss[0], CultureInfo.InvariantCulture);
                double y1 = double.Parse(ss[1], CultureInfo.InvariantCulture);
                double y2 = double.Parse(ss[2], CultureInfo.InvariantCulture);

                gpMaxFitnLine.AddPoint(x,y1);
                gpAvgFitnLine.AddPoint(x, y2);

            }
        }

        /// <summary>
        /// Deserilization of run condition
        /// </summary>
        /// <returns></returns>
        public string GetTypeofRun()
        {
            string fitness = "";
            for(int i=0;i< gpMaxFitnLine.Points.Count; i++)
            {
                var p = "";
                if (i > 0)
                    p = "#";
                var mF = gpMaxFitnLine.Points[i].X.ToString(CultureInfo.InvariantCulture) + 
                    ":" + gpMaxFitnLine.Points[i].Y.ToString(CultureInfo.InvariantCulture) +
                    ":" + gpAvgFitnLine.Points[i].Y.ToString(CultureInfo.InvariantCulture);
                
                
                //
                fitness += p + mF;
            }
           
            if (comboBox2.SelectedIndex != -1)
            {
                //
                string str = comboBox2.SelectedIndex.ToString() + ";"
                            + m_eb_iterations.Text + ";"
                            + (chkOptimumType.Checked ? "1" : "0") + ";"
                            + eb_currentIteration.Text + ";"
                            + eb_currentFitness.Text + ";"
                            + eb_maximumFitness.Text + ";"
                            + eb_maximumFitness.Text + ";"
                            + eb_bestSolutionFound.Text + ";"
                            + eb_timeStart.Text + ";"
                            + eb_timePerRun.Text + ";"
                            + eb_timeToCompleate.Text + ";"
                            + eb_timeleft.Text + ";"
                            + eb_durationInMin.Text + ";"
                            + fitness;
                ;
                            
                return str;


               
            }
            return null;


        }

        /// <summary>
        /// Check if there is a prev solution
        /// </summary>
        /// <returns></returns>
        public bool HasPrevSoluton()
        {

            return prevFitness > float.MinValue;
        }

        /// <summary>
        /// If program needs to find minimum istead of maximum
        /// </summary>
        /// <returns></returns>
        public bool IsMinimum()
        {
            return chkOptimumType.Checked;
        }

        /// <summary>
        /// Set opt type
        /// </summary>
        /// <param name="isMinimum"></param>
        public void SetOptType(bool isMinimum)
        {
            chkOptimumType.Checked = isMinimum;
        }

        public int GetCurrentIteration()
        {
            return int.Parse(eb_currentIteration.Text);
        }
        #endregion

    }

}
