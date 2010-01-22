using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;
using System.Diagnostics;

//RouletteWheel selection 
namespace gpNetLib.Selections
{
    [Serializable]
    public class RouletteWheelSelection: ISelection
    {
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {
            Debug.Assert(population.Count > 0);
            int currentSize = population.Count;
            double fitnessSum = population.Sum(c => c.Fitness);
            
            
            // select Population from old Population to the new Population
            while(true)
            {
                // get wheel value
                double wheelValue = GPPopulation.rand.NextDouble(0, fitnessSum,true);
                double partFitnes = 0;
                // find the chromosome for the wheel value
                for (int i = currentSize-1; i >=0; i--)
                {
                    partFitnes += population[i].Fitness;
                    if (wheelValue <= partFitnes)
                        yield return population[i];
                    
                }
            }
        }
        
    }
}
