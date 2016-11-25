//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2012 Bahrudin Hrnjica                                                 //
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using GPdotNET.Core;

namespace GPdotNET.Core
{
    /// <summary>
    /// This interface should be derived when declaring any chromosome in the application.
    /// It contains declaration of standard methods for every chromosome in GPdotNET.
    /// </summary>
    public interface IChromosome : IComparable<IChromosome>
    {
        /// <summary>
        /// Chromosome's fintess value
        /// </summary>
        float Fitness { get; set; }

        /// <summary>
        /// Generate random chromosome value
        /// </summary>
        void Generate(int param=0);

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        IChromosome Clone();

        /// <summary>
        /// Mutation operator
        /// </summary>
        void Mutate();

        /// <summary>
        /// Crossover operator
        /// </summary>
        void Crossover(IChromosome ch2,int index1=-1, int index2=-1);

        /// <summary>
        /// Evaluate chromosome with specified fitness function
        /// </summary>
        void Evaluate(IFitnessFunction function);
        
        /// <summary>
        /// Static member for creating chromosome from string
        /// </summary>
        /// <param name="strCromosome"></param>
        /// <returns></returns>
        IChromosome FromString(string strCromosome);

        /// <summary>
        /// Interface method when using memory pooling
        /// </summary>
        /// <param name="ch"></param>
        void Destroy();
    }
}
