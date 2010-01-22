using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;
using System.Diagnostics;

//Interface for diferent kind os selection methods

namespace gpNetLib.Selections
{
    [Serializable]
    public class EliteSelection: ISelection
    {
        //Select size number of the best chromosomes in population
        public IEnumerable<GPChromosome> Select(List<GPChromosome> population, int numberSelections = 1)
        {
            foreach(var p in population)
             yield return p;
        }
        
    }
}
