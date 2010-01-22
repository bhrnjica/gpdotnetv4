using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;
using System.Diagnostics;

//FUSS selection is based on http://www.idsia.ch/idsiareport/IDSIA-04-04.pdf research paper
namespace gpNetLib.Selections
{
    [Serializable]
    public class FUSSSelection: ISelection
    {
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {
            Debug.Assert(population.Count > 0);
            //Maximum fitness
            double fitnessMax = population[0].Fitness;
            double rnd ;
            double dif ;
            int selIndex = 0;
            while(true)
            {
                rnd = GPPopulation.rand.NextDouble(0, fitnessMax, true);
                dif = Math.Abs(population[0].Fitness - rnd);
                selIndex = 0;
                for (int i = 1; i < population.Count; i++)
                {
                    double curDif = Math.Abs(population[i].Fitness - rnd);
                    if (dif > curDif)
                    {
                        dif = curDif;
                        selIndex=i;
                    }
               }
                yield return population[selIndex];
                

            }
        }
        
    }
}
