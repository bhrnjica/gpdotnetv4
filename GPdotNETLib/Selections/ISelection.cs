using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNETLib;

//Interface fo rdiferent kind os selection methods
namespace gpNetLib.Selections
{
    public interface ISelection
    {
        IEnumerable<GPChromosome> Select(List<GPChromosome> population,int numberSelections = 1);
        
    }
}
