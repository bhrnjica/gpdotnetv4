using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPdotNETLib;
using System.Diagnostics;

namespace GPdotNETTestApplication
{
    public partial class BehchmarkAlgoritm : Form
    {
        public BehchmarkAlgoritm()
        {
            InitializeComponent();
        }
        private void BehchmarkAlgoritm_Load(object sender, EventArgs e)
        {
            TestUtility.TerminaliIFunkcije();
            

            GPPopulation.GPFunctionSet = TestUtility.functionSet;
            GPPopulation.GPFunctionSet.functions = TestUtility.functionSet.functions.Where(x => x.Aritry == 2).ToList();
            GPPopulation.GPTerminalSet = TestUtility.terminalSet;
            GPPopulation.GPParameters = new GPParameters();
            GPPopulation.GPParameters.einitializationMethod = EInitializationMethod.FullInitialization;
        }
        void PrepareForRun()
        {
            GPPopulation.GPParameters.maxInitLevel = int.Parse(epocetnaDubinaDrveta.Text);
            GPPopulation.GPParameters.maxMutationLevel = int.Parse(edubinaMutacije.Text);
            GPPopulation.GPParameters.maxCossoverLevel = int.Parse(edubinaUkrstanja.Text);
            GPPopulation.GPParameters.probMutation = int.Parse(evjerojatnostMutacije.Text);
            GPPopulation.GPParameters.probPermutation = int.Parse(evjerojatnostPermutacije.Text);
            GPPopulation.GPParameters.probReproduction = int.Parse(evjerojatnostReprodukcije.Text);
            GPPopulation.GPParameters.probCrossover = int.Parse(evjerojatnostUkrstanja.Text);
        }

        //Sequential run
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor rr = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            EnableControls(false);
            label6.Text = "";
            label9.Text = "";
            label12.Text = "";
            int popSize = int.Parse(evelicinaPopulacije.Text);
            GPPopulation pop = new GPPopulation(popSize, TestUtility.terminalSet, TestUtility.functionSet, GPPopulation.GPParameters,false);
            int generations=int.Parse(textBox1.Text);

            Stopwatch stoperia = new Stopwatch();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < generations; i++)
                pop.StartEvolution();
            secVrijeme = sw.Elapsed;
            label6.Text = sw.Elapsed.TotalSeconds.ToString() + "sec";
            UseWaitCursor = false;
            button2.Enabled = true;

            this.Cursor = rr;
        }

        private void EnableControls(bool enable=false)
        {
            evelicinaPopulacije.Enabled = enable;
            textBox1.Enabled = enable;
            groupBox1.Enabled = enable;
            groupBox2.Enabled = enable;
        }
        TimeSpan secVrijeme;
        //ParallelRun
        private void button2_Click(object sender, EventArgs e)
        {
            Cursor rr = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            if (secVrijeme.TotalSeconds == 0)
            {       
                MessageBox.Show("Run Sequential first.");
                    return;
            }
            int popSize = int.Parse(evelicinaPopulacije.Text);
            GPPopulation pop = new GPPopulation(popSize, TestUtility.terminalSet, TestUtility.functionSet, GPPopulation.GPParameters, true);
            int generations = int.Parse(textBox1.Text);

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < generations; i++)
                pop.StartEvolution();
            double v=sw.Elapsed.TotalSeconds;
            double speedup=Math.Round(secVrijeme.TotalSeconds / v,4);

            label9.Text = speedup.ToString()+" X";
            label12.Text = v.ToString() + "sec";

            EnableControls(true);
            //button2.Enabled = false;
            this.Cursor = rr;
        }

        
    }
}
