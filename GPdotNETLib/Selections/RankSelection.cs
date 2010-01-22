using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;
using System.Diagnostics;

//Interface fo rdiferent kind os selection methods
namespace gpNetLib.Selections
{
    [Serializable]
    public class RankSelection:ISelection
    {
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {
            Debug.Assert(population.Count > 0);

            // size of current Population
            int currentSize = population.Count;
            while(true)
            {
                int r =currentSize-1- GPPopulation.rand.Next(0, currentSize);
                Debug.Assert(r>=0 && r<currentSize);
                yield return population[r];
            }
        }
        
    }
}
