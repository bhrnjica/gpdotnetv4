using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;
using System.Diagnostics;

//Friend of mine (Fikret Skrgic) gave me an idea of this kind of selection.

namespace gpNetLib.Selections
{
    [Serializable]
    public class SkrgicSelection: ISelection
    {
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {

            Debug.Assert(population.Count > 0);
            double k = 2; //additionalParameter;
            double fitnessMax = population[0].Fitness * (1.0 + k);
            //Slucajni index iz populacije
            int randomIndex = GPPopulation.rand.Next(0, population.Count);
            //Slucajni broj izmedju 0-maxFitnes ukljucujuci i maxFitness koje je prethodno vec izracunat kod evaluacije hromosoma
            double randomFitness = GPPopulation.rand.NextDouble(0, fitnessMax, true/*include MaxValue*/);
            while (true)
            {
                //Akoje slucajno generirani broj manji ili jednak fitnesu slucajnog hromosoma selektuj hromosom
                if (randomFitness <= population[randomIndex].Fitness * (1.0 + k / fitnessMax))
                    yield return population[randomIndex];

                randomIndex = GPPopulation.rand.Next(0, population.Count);
                randomFitness = GPPopulation.rand.NextDouble(0, fitnessMax, true/*include MaxValue*/);
            }
       } 
    }
}
