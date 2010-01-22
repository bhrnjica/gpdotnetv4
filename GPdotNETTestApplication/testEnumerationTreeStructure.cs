using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPdotNETLib;
using gpWpfTreeDrawerLib;

namespace GPdotNETTestApplication
{
    public partial class testEnumerationTreeStructure : Form
    {
        public wpfTreeDrawerCtrl tre1 { get { return wpfTreeDrawerCtrl1; } }
        GPPopulation population;
        public testEnumerationTreeStructure()
        {
            InitializeComponent();
            TestUtility.TerminaliIFunkcije();
        }

        private void testEnumerationTreeStructure_Load(object sender, EventArgs e)
        {
            population = new GPPopulation(500, TestUtility.terminalSet, TestUtility.functionSet, null, false);
            for (int i = 0; i < 50; i++)
                population.StartEvolution();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GPChromosome ch1 = population.SelectChromosomeFromPopulation();
            textBox1.Text="";
            textBox2.Text = "";
            textBox1.Text=ch1.ToString();


            foreach (var index in ch1.NodeValueEnumeratorBreadthFirst)
                textBox2.Text += index.ToString() + ";";


            wpfTreeDrawerCtrl1.DrawTreeExpressionIndex(ch1.FunctionTree);

        }
    }
}
