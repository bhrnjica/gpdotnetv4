using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    [Serializable]
    public class GPHistory
    {
        //public int Generation { get; set; }
        public double AvgFitness { get; set; }
        public GPChromosome BestHromosome { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
