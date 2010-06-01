//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2010 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;

namespace GPNETLib
{
    [Serializable]
    public class GPChromosome : IComparable, IEquatable<GPChromosome>
    {
        // tree root
        public FunctionTree Root = new FunctionTree();

        // chromosome's fitness. Fitness is always float value. No meter which of type chromosem is.
        private float fitness = 0;
        //Used with Equatable to increase diversity in population
        private float _diversity = 0.001f;
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
        #region IEquatable<GPChromosome> Members

        public bool Equals(GPChromosome other)
        {
            if (other == null) return false;
            float f1 = fitness + _diversity;
            float f2 = fitness - _diversity;
            return (other.Fitness <= f1 && other.Fitness > f2);

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
