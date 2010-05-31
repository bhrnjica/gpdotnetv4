// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// gpNETLib 
using System;

namespace GPNETLib
{
    [Serializable]
    public class GPChromosome: IComparable
    {
        // tree root
        public FunctionTree Root = new FunctionTree();

        // chromosome's fitness. Fitness is always float value. No meter which of type chromosem is.
        private float fitness = 0;
        /// <summary>
        /// Fitness property
        /// </summary>
        public float Fitness
        {
            get { return fitness; }
            set { fitness = value; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public GPChromosome()
        { }
        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public GPChromosome Clone()
        {
            return new GPChromosome(this);
        }
        /// <summary>
        /// Copy constructor
        /// </summary>
        public GPChromosome(GPChromosome source)
        {
            //Helper for cloning
            Root = (FunctionTree)source.Root.Clone();
            fitness = source.Fitness;
        }
        /// <summary>
        /// Get string representation of the chromosome. Return the chromosome
        /// in reverse polish notation (postfix notation).
        /// </summary>
        public override string ToString()
        {
            return Root.ToString();
        }
        /// <summary>
        /// Compare two chromosomes
        /// </summary>
        #region IComparable Members

        public int CompareTo(object obj)
        {
            GPChromosome o = (GPChromosome)obj;
            return (Fitness == o.Fitness) ? 0 : (Fitness < o.Fitness) ? 1 : -1;
        }

        #endregion
    }
}
