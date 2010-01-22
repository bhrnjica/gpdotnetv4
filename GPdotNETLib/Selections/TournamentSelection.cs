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
    public class TournmentSelection: ISelection
    {
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {
            Debug.Assert(population.Count > 0);

            int TournamentSize = 4;
            int currentSize = population.Count;
            List<GPChromosome> tourn = new List<GPChromosome>(TournamentSize);
            
            while(true)
            {
                currentSize = population.Count;
                for (int i = 0; i < TournamentSize && i < currentSize; i++)
                {
                    int ind = GPPopulation.rand.Next(currentSize);
                    tourn.Add(population[ind]);

                }

                tourn.Sort();
                yield return tourn[0];
                tourn.Clear();
            }

            
        }
        
    }
}
