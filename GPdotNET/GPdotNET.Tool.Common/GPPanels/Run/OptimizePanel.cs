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
using System.Windows.Forms;
using GPdotNET.Core;

namespace GPdotNET.Tool.Common
{
   /// <summary>
   /// Class which implement simulation of optimization with GA 
   /// </summary>
    public partial class OptimizePanel : BaseRunPanel
    {
        #region CTor
        /// <summary>
        /// Ctor
        /// </summary>
        public OptimizePanel()
        {
            InitializeComponent();
            listView1.HideSelection = false;
            PrepareGraphs();
        }
        #endregion

        #region  Private and Protected Methods
        /// <summary>
        /// Overiden methods ofr initilaization of graphs
        /// </summary>
        protected override void PrepareGraphs()
        {
            base.PrepareGraphs();
            label32.Text = "Optimal Value: ";
            comboBox2.Enabled = false;


        }

        
        /// <summary>
        /// Update value of input variable for which is get the best optimum function value
        /// </summary>
        /// <param name="chomosome"></param>
        private void UpdateOptimizationResult(IChromosome chomosome)
        {
            var ch = chomosome as GPdotNET.Engine.GANumChromosome;
            if (ch == null)
                return;

            for (int i = 0; i < listView1.Items.Count; i++)
            {

                ListViewItem LVI = listView1.Items[i];
                LVI.SubItems[3].Text = Math.Round(ch.val[i], 5).ToString();
            }


        }

        /// <summary>
        /// Click event in listView in order to select current row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices == null || listView1.SelectedIndices.Count == 0)
                return;
            var selIndex = listView1.SelectedIndices[0];

            ListViewItem LVI = listView1.Items[selIndex];
            
            textBox1.Text = LVI.SubItems[1].Text;
            textBox2.Text = LVI.SubItems[2].Text;

        }

        /// <summary>
        /// Click event for Update button which update min and max value of selected item in ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices == null || listView1.SelectedIndices.Count == 0)
                return;
            var selIndex = listView1.SelectedIndices[0];

            ListViewItem LVI = listView1.Items[selIndex];
            
            double num = 0;
            if (!double.TryParse(textBox1.Text, out num))
            {
                MessageBox.Show("Min value is not a number! Please try again.");
                return;
            }
            if (!double.TryParse(textBox2.Text, out num))
            {
                MessageBox.Show("Max value is not a number! Please try again.");
                return;
            }
            LVI.SubItems[1].Text = textBox1.Text;
            LVI.SubItems[2].Text = textBox2.Text;
        }

        /// <summary>
        /// On size event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.listView1 != null)
            {
                var heig = (this.Height / 2) - progressBar1.Height;
                var wei = this.Width - groupBox7.Width - 25;
                var xPos = groupBox7.Width + groupBox7.Location.X + groupBox7.Margin.Left;

                this.listView1.Location = new System.Drawing.Point(xPos, heig + groupBox7.Location.Y+10);
                this.listView1.Size = new System.Drawing.Size(wei, heig - 45);
				
				//
				this.label1.Location = new System.Drawing.Point(this.listView1.Left+this.listView1.Padding.Left, this.listView1.Top+this.listView1.Height+7);
                this.label1.Size = new System.Drawing.Size(50, 17);
				
				this.textBox1.Location = new System.Drawing.Point(this.listView1.Left+this.listView1.Padding.Left+textBox1.Width, this.listView1.Top+this.listView1.Height+5);
                this.textBox1.Size = new System.Drawing.Size(50, 17);
				
				this.label2.Location = new System.Drawing.Point(this.listView1.Left+this.listView1.Padding.Left+2*textBox1.Width, this.listView1.Top+this.listView1.Height+7);
                this.label2.Size = new System.Drawing.Size(50, 17);
				
				this.textBox2.Location = new System.Drawing.Point(this.listView1.Left+this.listView1.Padding.Left+3*textBox1.Width, this.listView1.Top+this.listView1.Height+5);
                this.textBox2.Size = new System.Drawing.Size(50, 17);
				
				this.button1.Location = new System.Drawing.Point(this.listView1.Left+this.listView1.Padding.Left+4*textBox1.Width+5, this.listView1.Top+this.listView1.Height+3);
                this.button1.Size = new System.Drawing.Size(65, 28);
            }

        }

        public override void EnableCtrls(bool p)
        {
            base.EnableCtrls(p);
            comboBox2.Enabled = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Report prograss the same as in bse class
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

            // When fitness is changed, model needs to be refreshed
            if (prevFitness < ch.Fitness)
            {
                var fitStr = ch.Fitness;
                if (chkOptimumType.Checked)
                    fitStr = -1 * ch.Fitness;

                eb_currentFitness.Text = Math.Round(fitStr, 5).ToString("#.#####");

                prevFitness = ch.Fitness;
                eb_bestSolutionFound.Text = currentEvoution.ToString();
                UpdateOptimizationResult(ch);

            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vars"></param>
        public void FillTerminalBounds(List<string> vars)
        {

            if (vars == null)
                return;

            CreateColumns();

            for (int i = 0; i < vars.Count; i++)
            {
                ListViewItem LVI = listView1.Items.Add(vars[i]);
                LVI.SubItems.Add("0");
                LVI.SubItems.Add("0");
                LVI.SubItems.Add("-");
            }
        }

        private void CreateColumns()
        {
            listView1.Clear();

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Vars";
            colHeader.Width = 40;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Min Value";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);


            colHeader = new ColumnHeader();
            colHeader.Text = "Max Value";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Optimum";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);
        }
        
        

        /// <summary>
        /// Returns list of Terminals with min and max values. The values is read fro listView 
        /// </summary>
        /// <returns></returns>
        public List<GPTerminal> GetTerminalBounds()
        {
            var terms = new List<GPTerminal>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
               
                ListViewItem LVI = listView1.Items[i];
                GPTerminal t = new GPTerminal();
                t.Name= LVI.SubItems[0].Text;
                t.minValue = float.Parse(LVI.SubItems[1].Text);
                t.maxValue = float.Parse(LVI.SubItems[2].Text);
                terms.Add(t);
            }

            return terms;
        }

        /// <summary>
        /// Deserilization of minimum and maximum values of input vars
        /// </summary>
        /// <param name="ss"></param>
        public void SetMaximumAndMinimumValues(string ss)
        {
            var vars = ss.Replace("\r", "").Split(new char[]{'\t'}, StringSplitOptions.RemoveEmptyEntries);

            if (listView1.Items.Count == 0)
            {
                CreateColumns();

                for (int i = 0; i < vars.Length; i++)
                {
                    ListViewItem LVI = listView1.Items.Add("X"+(i+1).ToString());
                    LVI.SubItems.Add("0");
                    LVI.SubItems.Add("0");
                    LVI.SubItems.Add("-");
                }
            }

            for (int i = 0; i < vars.Length; i++)
            {
                var str = vars[i].Split(';');
                ListViewItem LVI = listView1.Items[i];
                LVI.SubItems[1].Text=str[0];
                LVI.SubItems[2].Text=str[1];
            }

        }

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevFitness = float.MinValue;
            if(gpMaxFitnLine!=null)
                gpMaxFitnLine.Clear();
            if (gpAvgFitnLine != null)
                gpAvgFitnLine.Clear();
                
        }

        
        #endregion

        public static string OptimizeToString()
        {
            return "-";
        }

        public static void OptimizeFromString(string strOpt)
        {
            return ;
        }
    }
}
