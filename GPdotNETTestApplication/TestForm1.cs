using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gpWpfTreeDrawerLib;
using GPdotNETLib;

namespace GPdotNETTestApplication
{
    public partial class TestForm1 : Form
    {
        public wpfTreeDrawerCtrl tre1 { get { return wpfTreeDrawerCtrl1; } }
        public wpfTreeDrawerCtrl tre2 { get { return wpfTreeDrawerCtrl2; } }
        public wpfTreeDrawerCtrl tre3 { get { return wpfTreeDrawerCtrl1; } }
        public wpfTreeDrawerCtrl tre4 { get { return wpfTreeDrawerCtrl2; } }
        GPPopulation population;
        public TestForm1()
        {
            InitializeComponent();
            TestUtility.TerminaliIFunkcije();
        }
        private void TestForm1_Load(object sender, EventArgs e)
        {
            GPPopulation.GPFunctionSet = TestUtility.functionSet;
            GPPopulation.GPFunctionSet.functions = TestUtility.functionSet.functions.Where(x => x.Aritry == 2).ToList();
           population = new GPPopulation(500, TestUtility.terminalSet, TestUtility.functionSet, null, false);
           for (int i = 0; i < 50; i++)
               population.StartEvolution();
        }
        //Crossover test
        private void button1_Click(object sender, EventArgs e)
        {
            GPChromosome ch1 = population.SelectChromosomeFromPopulation();
            GPChromosome ch2 = population.SelectChromosomeFromPopulation();
            ch2.GenerateChromosome(6);
            GPChromosome po1 = ch1.Clone();
            GPChromosome po2 = ch2.Clone();

            //Ukupan broj cvorova po1
            int l1 = po1.NodeEnumeratorBreadthFirst.Count();
            //Slucajni cvor
            int rand1 = GPPopulation.rand.Next(1,l1);
            //Ukupan broj cvorova po2
            int l2 = po2.NodeEnumeratorBreadthFirst.Count();
            //Slucajni cvor
            int rand2 = GPPopulation.rand.Next(1, l2);

            //Ukrstanje
            var po1list = po1.NodeEnumeratorBreadthFirst.ElementAt(rand1);
            var po2list = po2.NodeEnumeratorBreadthFirst.ElementAt(rand2);

            var parent1 = po1.NodeEnumeratorBreadthFirst.ElementAt(rand1).Parent;
            var parent2 = po2.NodeEnumeratorBreadthFirst.ElementAt(rand2).Parent;

            var index1=parent1.Nodes.IndexOf(po1list);
            var index2 = parent2.Nodes.IndexOf(po2list);

            po1list.Detach();
            po2list.Detach();

            parent1.Nodes.Insert(index1, po2list);
            parent2.Nodes.Insert(index2, po1list);


            labelParent1.Text =string.Format( "Parent 1. NodeCount={0}, Levels={1}" , ch1.NodeEnumeratorDepthFirst.Count(), ch1.Levels);
            labelParent2.Text = string.Format("Parent 2. NodeCount={0}, Levels={1}", ch2.NodeEnumeratorDepthFirst.Count(), ch2.Levels);
            labelOffspring2.Text = string.Format("Ofspring 1. NodeCount={0}, Levels={1}", po1.NodeEnumeratorDepthFirst.Count(), po1.Levels);
            labelOffspring1.Text = string.Format("Ofspring 2. NodeCount={0}, Levels={1}", po2.NodeEnumeratorDepthFirst.Count(), po2.Levels);
           
           
            wpfTreeDrawerCtrl1.DrawTreeExpression(ch1.FunctionTree,GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl2.DrawTreeExpression(ch2.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl3.DrawTreeExpression(po2.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl4.DrawTreeExpression(po1.FunctionTree, GPPopulation.GPFunctionSet);
            


        }
        //Mutation test
        private void button2_Click(object sender, EventArgs e)
        {
            GPChromosome ch1 = population.SelectChromosomeFromPopulation();
           //Clone chromosome
            GPChromosome po1 = ch1.Clone();
           // population.Mutation();

            //Ukupan broj cvorova po1
            int l1 = po1.NodeValueEnumeratorBreadthFirst.Count();
            //Slucajni cvor
            int rand1 = GPPopulation.rand.Next(1, l1);
            
            //Ukrstanje
            var po1list = po1.NodeEnumeratorBreadthFirst.ElementAt(rand1);
            //Remove children
            po1list.Nodes.Clear();
            po1.GenerateExpressionTree(po1list, 6);
            po1.Trim(po1.FunctionTree,6);

            int depth1 = ch1.Levels;
            labelParent1.Text = string.Format("Parent 1. NodeCount={0}, Levels={1}", ch1.NodeValueEnumeratorBreadthFirst.Count(),depth1);
            labelOffspring1.Text = string.Format("Ofspring 1. NodeCount={0}, Levels={1}", po1.NodeEnumeratorBreadthFirst.Count(), po1.Levels);

            wpfTreeDrawerCtrl1.DrawTreeExpression(ch1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl3.DrawTreeExpression(po1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl2.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl4.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
            
        }
        //Shrink mutation test
        private void button3_Click(object sender, EventArgs e)
        {
            GPChromosome ch1 = population.SelectChromosomeFromPopulation();
            //Clone chromosome
            GPChromosome po1 = ch1.Clone();


            //Ukupan broj cvorova po1
            int l1 = po1.NodeValueEnumeratorBreadthFirst.Count();
            //Slucajni cvor
            int rand1 = GPPopulation.rand.Next(1, l1);

            //slucajninod za ukrstanje
            var po1list = po1.NodeEnumeratorBreadthFirst.ElementAt(rand1);
            //Remove children
            po1list.Nodes.Clear();
            //If random node is terminal shink their parent if it has parent
            if(po1list.Parent.Parent!=null)
             po1list = po1list.Parent;

            po1list.Nodes.Clear();
            po1list.Value = GPPopulation.GPFunctionSet.GenerateGene(false); ;

            int depth1 = ch1.Levels;
            labelParent1.Text = string.Format("Parent 1. NodeCount={0}, Levels={1}", ch1.NodeEnumeratorBreadthFirst.Count(), depth1);
            labelOffspring1.Text = string.Format("Ofspring 1. NodeCount={0}, Levels={1}", po1.NodeEnumeratorBreadthFirst.Count(), po1.Levels);

            wpfTreeDrawerCtrl1.DrawTreeExpression(ch1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl3.DrawTreeExpression(po1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl2.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl4.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
        }
        //Hoist Mutation
        private void button5_Click(object sender, EventArgs e)
        {
            GPChromosome ch1 = population.SelectChromosomeFromPopulation();
            //Clone chromosome
            GPChromosome po1 = ch1.Clone();


            //Ukupan broj cvorova po1
            int l1 = po1.NodeEnumeratorBreadthFirst.Count();
            //Slucajni cvor
           
            GPTreeNode po1list;
            while (true)
            {
                //If chromosome has level less than 3 dont make Hoist mutation
                if (po1.Levels < 3)
                    return;
                int rand1 = GPPopulation.rand.Next(1, l1);
                //slucajninod za ukrstanje
                po1list = po1.NodeEnumeratorBreadthFirst.ElementAt(rand1);
                //we want to be a function node
                if (po1list.HasChildren)
                    break;

            }
            //Now the chromosome become random node tree
            po1.FunctionTree = po1list;
                       

            int depth1 = ch1.Levels;
            labelParent1.Text = string.Format("Parent 1. NodeCount={0}, Levels={1}", ch1.NodeEnumeratorBreadthFirst.Count(), depth1);
            labelOffspring1.Text = string.Format("Ofspring 1. NodeCount={0}, Levels={1}", po1.NodeEnumeratorBreadthFirst.Count(), po1.Levels);

            wpfTreeDrawerCtrl1.DrawTreeExpression(ch1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl3.DrawTreeExpression(po1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl2.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl4.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
        }
        //Test permutation
        private void button4_Click(object sender, EventArgs e)
        {
            GPChromosome ch1 = population.SelectChromosomeFromPopulation();
            //Clone chromosome
            GPChromosome po1 = ch1.Clone();


            //Ukupan broj cvorova po1
            int l1 = po1.NodeEnumeratorBreadthFirst.Count();
            

            //Ukrstanje
            GPTreeNode po1list=null;
            while(true)
            {
               //Slucajni cvor
               int rand1 = GPPopulation.rand.Next(1, l1);
               po1list = po1.NodeEnumeratorBreadthFirst.ElementAt(rand1);
               if (po1list.HasChildren)
               {
                   if (po1list.Nodes.Count == 2)
                   {
                       GPTreeNode temp = null;
                       temp = po1list.Nodes[0];
                       po1list.Nodes[0] = po1list.Nodes[1];
                       po1list.Nodes[1] = temp;
                       break;
                   }
                   else if (po1list.Nodes.Count > 2)
                   {
                       GPTreeNode temp = null;
                       int temIndex = GPPopulation.rand.Next(po1list.Nodes.Count);
                       int tempIndex2 = GPPopulation.rand.Next(po1list.Nodes.Count);
                       if (temIndex != tempIndex2)
                       {
                           temp = po1list.Nodes[temIndex];
                           po1list.Nodes[temIndex] = po1list.Nodes[tempIndex2];
                           po1list.Nodes[tempIndex2] = temp;
                           break;
                       }
                   }
               }
            }
            int depth1 = ch1.Levels;
            labelParent1.Text = string.Format("Parent 1. NodeCount={0}, Levels={1}", ch1.NodeEnumeratorBreadthFirst.Count(), depth1);
            labelOffspring1.Text = string.Format("Ofspring 1. NodeCount={0}, Levels={1}", po1.NodeEnumeratorBreadthFirst.Count(), po1.Levels);

            wpfTreeDrawerCtrl1.DrawTreeExpression(ch1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl3.DrawTreeExpression(po1.FunctionTree, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl2.DrawTreeExpression(null, GPPopulation.GPFunctionSet);
            wpfTreeDrawerCtrl4.DrawTreeExpression(null, GPPopulation.GPFunctionSet);


        }

       

       
    }
}
