using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;
using System.Diagnostics;

//Stochasticuniversal selection is based on http://www.geatbx.com/docu/algindex-02.html documentation
namespace gpNetLib.Selections
{
    [Serializable]
    public class StohasticUniversalSelection:ISelection
    {
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {
            Debug.Assert(population.Count > 0);
            int currentSize = population.Count;
            double fitnessSum = population.Sum(c => c.Fitness);
            // get wheel value
            double wheelValue = GPPopulation.rand.NextDouble(0, 1.0 / (double)numberSelections);
            double partFitnes = 0;
            // find the chromosome for the wheel value
            for (int i = 0; i < currentSize; i++)
            {
                partFitnes += population[i].Fitness / fitnessSum;
                if (wheelValue <= partFitnes)
                {
                    yield return population[i];
                    wheelValue += 1.0 / (double)numberSelections;
                }
            }
                
        }        
    }
}
