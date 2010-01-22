using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GPdotNETLib;

namespace GPdotNETTestApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SelectionTest());

           // MemorytestOfMaxChromosomesInPopulation();
        }

        private static void MemorytestOfMaxChromosomesInPopulation()
        {
            TestUtility.TerminaliIFunkcije();
            GPPopulation pop = new GPPopulation();
            
            GPPopulation.GPFunctionSet = TestUtility.functionSet;
            GPPopulation.GPFunctionSet.functions = TestUtility.functionSet.functions.Where(x => x.Aritry == 2).ToList();
            GPPopulation.GPTerminalSet = TestUtility.terminalSet;
            GPPopulation.GPParameters = new GPParameters();

            long count = 1;
            while (true)
            {
                try
                {
                    GPChromosome c1 = new GPdotNETLib.GPChromosome(0);
                    c1.GenerateChromosome(6);
                    int lev=c1.Levels;
                    int coun21t = c1.NodeEnumeratorBreadthFirst.Count();
                    pop.Population.Add(c1);
                    count++;
                }
                catch
                {
                    Console.WriteLine(count.ToString());
                    Console.Read();
                    //With full initialization 
                   // 398293 chromosomes  with 6 level and 2 arguments arirty
                    // Means the you can make 63 * 441200 =27.795.600 nodes in one population
                }
            }
        }
    }
}
