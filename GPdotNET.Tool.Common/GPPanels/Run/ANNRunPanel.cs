//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-201 Bahrudin Hrnjica                                                 //
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
using System.Windows.Forms;
using System.Globalization;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Class implenents simulation of Running GP
    /// </summary>
    public partial class ANNRunPanel : UserControl
    {
        #region Ctor Properties and Fields
        protected LineItem gpDataLine;
        protected LineItem gpModelLine;
        protected LineItem learningErrorLine;
        
        DateTime m_startTime;
        DateTime prevIterationTime;
        float prevLearningError = float.MaxValue;
        public ANNRunPanel()
        {
            InitializeComponent();
            PrepareGraphs();
            comboBox2.SelectedIndex = 0;
        }

        public float TerminationValue
        {
            get
            {
                float val = 500;
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

                return comboBox2.SelectedIndex;
            }
        }
        #endregion

        #region Protected and Private methods

        /// <summary>
        /// Initi props for charts
        /// </summary>
        protected /*override*/ void PrepareGraphs()
        {
            ///Fitness simulation chart
            zedFitness.GraphPane.Title.Text = "Learning Error";
            zedFitness.GraphPane.XAxis.Title.Text = "Iterations";
            zedFitness.GraphPane.YAxis.Title.Text = "Error";

            learningErrorLine = zedFitness.GraphPane.AddCurve("Error", null, null, Color.Red, ZedGraph.SymbolType.None);
           // learningErrorLine.Symbol.Border = new Border(Color.Green, 0.1f);
            this.zedFitness.GraphPane.AxisChange(this.CreateGraphics());

            zedModel.GraphPane.Title.Text = "ANN Model Simulation";
            zedModel.GraphPane.XAxis.Title.Text = "Samples";
            zedModel.GraphPane.YAxis.Title.Text = "Output";

            gpDataLine = zedModel.GraphPane.AddCurve("Data Points", null, null, Color.Red, ZedGraph.SymbolType.None);
            gpDataLine.Symbol.Border = new Border(Color.Green, 0.1f);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());

            gpModelLine = zedModel.GraphPane.AddCurve("ANN Model", null, null, Color.Blue, ZedGraph.SymbolType.Plus);
            this.zedModel.GraphPane.AxisChange(this.CreateGraphics());
        }

        /// <summary>
        /// On size event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            base.OnSizeChanged(e);
            if (this.zedFitness != null)
            {

                var heig = (this.Height / 2) - progressBar1.Height;
                var wei = this.Width - groupBox7.Width - 25;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.zedFitness.Location = new System.Drawing.Point(xPos, groupBox7.Location.Y);
                this.zedFitness.Size = new System.Drawing.Size(wei, heig);

            }

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

        #region Public Methdos
        /// <summary>
        /// Report progress of current evolution
        /// </summary>
        /// <param name="currentEvoution"></param>
        /// <param name="avgFitness"></param>
        /// <param name="ch"></param>
        /// <param name="runType"></param>
        public /*override*/ void ReportProgress(int currentIteration, float learningError, double[][] learning, int runType)
        {
            if (runType == 1)
                StartProgress();
            else if (runType == 2)
            {
                if (comboBox2.SelectedIndex == 0)
                    progressBar1.Value = currentIteration;

                //foreach iteration update learning error
                UpdateChartError(currentIteration, learningError);

                //When fitness is changed, model needs to be refreshed
                if (prevLearningError < 0 ||  prevLearningError > learningError)
                {
                    prevLearningError = learningError;

                    eb_currentError.Text = learningError.ToString("#.#####");

                    eb_bestSolutionFound.Text = currentIteration.ToString();

                    UpdateChartDataPoint(learning);

                }

                //calculation time
                var currIterationTime = DateTime.Now - prevIterationTime;
                prevIterationTime = DateTime.Now;
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

                    DateTime datToFinish = m_startTime.AddSeconds(sec * (broj - currentIteration));

                    eb_timeToCompleate.Text = datToFinish.ToString();
                    eb_timeleft.Text = Math.Round((datToFinish - m_startTime).TotalMinutes, 3).ToString();

                }

                eb_durationInMin.Text = Math.Round((DateTime.Now - m_startTime).TotalMinutes, 3).ToString();
            }
            else if (runType == 3)
            {
                prevLearningError = learningError;
                FinishProgress();
                UpdateChartDataPoint(learning);
                //
                zedFitness.RestoreScale(zedFitness.GraphPane);
                zedFitness.Invalidate();
                zedModel.Invalidate();
            }
               


            
        }

        private void FinishProgress()
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

                //int max = 500;
                //if (!int.TryParse(brojIteracija.Text, out max))
                //    max = 500;
                //progressBar1.Style = ProgressBarStyle.Blocks;
                //progressBar1.Maximum = max;
                //progressBar1.Minimum = 0;
                //progressBar1.Value = 0;
            }
            progressBar1.MarqueeAnimationSpeed = 0;
        }

        private void StartProgress()
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
            eb_timeStart.Text = m_startTime.ToString();
        }

        /// <summary>
        /// Uodate GP Model chart when data or GP model is changed
        /// </summary>
        /// <param name="y">output value</param>
        /// <param name="gpModel"> indicator is it about GPMOdel or Data Point</param>
        public void UpdateChartDataPoint(double[][] y, bool gpModel=true)
        {
            if (this.zedModel.GraphPane == null || y==null)
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

        public void UpdateChartError(int curIter, double currError, bool isFinished = false)
        {

            if (curIter + 1 < learningErrorLine.Points.Count)
                learningErrorLine.Clear();

            learningErrorLine.AddPoint(curIter, currError);

            eb_currentIteration.Text = curIter.ToString();

            if (curIter % 10 == 0 || isFinished)
                zedFitness.RestoreScale(zedFitness.GraphPane);
        }

        

#endregion

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevLearningError = float.MaxValue;
            if (learningErrorLine != null)
                learningErrorLine.Clear();
            if (gpModelLine != null)
                gpModelLine.Clear();

            
            eb_currentIteration.Text = "0";
            eb_currentError.Text = "";
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
        public bool HasPrevSoluton()
        {
            return prevLearningError != float.MaxValue;
        }

        /// <summary>
        /// Deserilization of run condition
        /// </summary>
        /// <returns></returns>
        public string GetTypeofRun()
        {
            string error = "";
            for (int i = 0; i < learningErrorLine.Points.Count; i++)
            {
                var p = "";
                if (i > 0)
                    p = "#";
                var mF = learningErrorLine.Points[i].X.ToString(CultureInfo.InvariantCulture) +
                    ":" + learningErrorLine.Points[i].Y.ToString(CultureInfo.InvariantCulture);


                //
                error += p + mF;
            }
            if (comboBox2.SelectedIndex != -1)
            {
                //
                string str = comboBox2.SelectedIndex.ToString() + ";"
                            + m_eb_iterations.Text.ToString(CultureInfo.InvariantCulture) + ";"
                            + eb_currentIteration.Text + ";"
                            + eb_currentError.Text.ToString(CultureInfo.InvariantCulture) + ";"
                            + eb_bestSolutionFound.Text + ";"
                            + eb_timeStart.Text + ";"
                            + eb_timePerRun.Text + ";"
                            + eb_timeToCompleate.Text + ";"
                            + eb_timeleft.Text + ";"
                            + eb_durationInMin.Text + ";"
                            + error
                            ;

                return str;
            }
            return null;


        }


        /// <summary>
        /// Deserilization of run condition
        /// </summary>
        /// <param name="p"></param>
        public void SetTypeofRun(string p)
        {
            var funs = p.Split(';');
                comboBox2.SelectedIndex = funs[0] == "1" ? 1 : 0;
            if (funs.Length < 2)
                m_eb_iterations.Text = "500";
            else
                m_eb_iterations.Text = double.Parse(funs[1], CultureInfo.InvariantCulture).ToString(); ;

            if (funs.Length > 2)
                eb_currentIteration.Text = funs[2];
            if (funs.Length > 3)
                eb_currentError.Text = double.Parse(funs[3], CultureInfo.InvariantCulture).ToString();
            if (funs.Length > 4)
                eb_bestSolutionFound.Text = funs[4];
            if (funs.Length > 5)
                eb_timeStart.Text = funs[5];
            if (funs.Length > 6)
                eb_timePerRun.Text = funs[6];
            if (funs.Length > 7)
                eb_timeToCompleate.Text = funs[7];
            if (funs.Length > 8)
                eb_timeleft.Text = funs[8];
            if (funs.Length > 9)
                eb_durationInMin.Text = funs[9];
            if (funs.Length > 10)
                fillErrors(funs[10]);
        }

        /// <summary>
        /// creates fitness graphs from string points
        /// </summary>
        /// <param name="v"></param>
        private void fillErrors(string v)
        {
            if (v == "\r")
                return;
            var strs = v.Split('#');
            for (int i = 0; i < strs.Length; i++)
            {
                var ss = strs[i].Split(':');
                double x = double.Parse(ss[0], CultureInfo.InvariantCulture);
                double y1 = double.Parse(ss[1], CultureInfo.InvariantCulture);
                if (y1 < 0)
                    y1 = 0;

                learningErrorLine.AddPoint(x, y1);
               
            }
            zedFitness.Invalidate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex==0)
            {
                if (m_eb_iterations.Text.Contains("0."))
                    m_eb_iterations.Text = "100";
                
            }
            else
            {
                if (!m_eb_iterations.Text.Contains("0."))
                    m_eb_iterations.Text = "0.01";
                
            }
            
        }

        public void EnableCtrls(bool p)
        {
            comboBox2.Enabled = p;
            m_eb_iterations.Enabled = p;
        }

        public void ContinueSearching()
        {
            if(comboBox2.SelectedIndex==0)
            {
                int num = 0;
                int cur = 0;
                int.TryParse(eb_currentIteration.Text, out cur);

                if (int.TryParse(m_eb_iterations.Text, out num))
                    m_eb_iterations.Text = (cur + num).ToString();
            }
               
        }


        public int GetCurrentIteration()
        {
            if (string.IsNullOrEmpty(eb_currentIteration.Text))
                return 0;
            else
               return int.Parse(eb_currentIteration.Text);
        }


    }

}
