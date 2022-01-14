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
using System.Collections;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Implementation continuous type GA chromosome used in optimization 
    /// </summary>
    public class GANumChromosome
#if MEMORY_POOLING
 : Pool<GANumChromosome>, IChromosome
#else
 : IChromosome
#endif
    {
        public double[] val = null;	
        private float fitness = float.MinValue;
        //Fitness value for the chromosome
        public float Fitness
        {
            get
            {
                return fitness;
            }
            set
            {
                fitness = value;
            }
        }

        public static IFunctionSet functionSet;
        #region Ctor and initialisation
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GANumChromosome()
        {
            if (functionSet == null)
                throw new Exception("TerminalSet is not initialized!");
            fitness = float.MinValue;
            val = new double[functionSet.GetNumVariables()];
            Generate();
        }

        public int CompareTo(IChromosome other)
        {
            if (other == null)
                return 1;
            return other.Fitness.CompareTo(this.Fitness);

        }

        /// <summary>
        /// Create deep copy of the chromoseme
        /// </summary>
        /// <returns></returns>
        public IChromosome Clone()
        {
            var clone = GANumChromosome.NewChromosome();

            clone.val = new double[functionSet.GetNumVariables()];
            for (int i = 0; i < functionSet.GetNumVariables(); i++)
               clone.val[i] = val[i];
            clone.fitness = fitness;

            return clone;
        }

        /// <summary>
        ///  Randomly generate value for each array element between constrains defined in terminal set
        /// </summary>
        public void Generate(int param = 0)
        {
            if(val==null)
                val = new double[functionSet.GetNumVariables()];
            else if (val.Length != functionSet.GetNumVariables())
                val = new double[functionSet.GetNumVariables()];

            for (int i = 0; i < functionSet.GetNumVariables(); i++)
                val[i] = Globals.radn.NextDouble(functionSet.GetTerminalMinValue(i), functionSet.GetTerminalMaxValue(i));

        }

        /// <summary>
        ///  Select array element randomly and randomly change itc value 
        /// </summary>
        public void Mutate()
        {
            //randomly select array element
            int crossoverPoint = Globals.radn.Next(functionSet.GetNumVariables());
            //randomly generate value for the selected element
            val[crossoverPoint] = Globals.radn.NextDouble(functionSet.GetTerminalMinValue(crossoverPoint), functionSet.GetTerminalMaxValue(crossoverPoint));
        }

        /// <summary>
        /// Generate number betwee 0-1. For each array ement of chromosome exchange value based on random number
        /// </summary>
        /// <param name="ch2"></param>
        public void Crossover(IChromosome ch2, int index1 = -1, int index2 = -1)
        {
            GANumChromosome p = (GANumChromosome)ch2;
            int crossoverPoint = Globals.radn.Next(functionSet.GetNumVariables());
            double beta;
            for (int i = crossoverPoint; i < functionSet.GetNumVariables(); i++)
            {
                beta = Globals.radn.NextDouble();
                val[i] = val[i] - beta * (val[i] - p.val[i]);
                p.val[i] = p.val[i] + beta * (val[i] - p.val[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        public void Evaluate(IFitnessFunction function)
        {
            Fitness = function.Evaluate(this, functionSet);
        }

        #endregion

        #region Operations

        /// <summary>
        /// STring representation for the chromosome. It is also used for serializing to txt file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            str+=fitness.ToString(CultureInfo.InvariantCulture)+";";
            for (int i = 0; i < val.Length; i++)
               str += val[i].ToString(CultureInfo.InvariantCulture)+";";
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCromosome"></param>
        /// <returns></returns>
        public IChromosome FromString(string strCromosome)
        {
            return GANumChromosome.CreateFromString(strCromosome);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCromosome"></param>
        /// <returns></returns>
        public static GANumChromosome CreateFromString(string strCromosome)
        {
            var str = strCromosome.Replace(";\r","").Split(';');
            GANumChromosome ch = new GANumChromosome();
            ch.fitness = float.Parse(str[0], CultureInfo.InvariantCulture);
            ch.val= new double[str.Length-1];
            for (int i = 1; i < str.Length; i++)
            {
                ch.val[i-1] = double.Parse(str[i], CultureInfo.InvariantCulture);
 
            }

            return ch;
        }
        #endregion

#region Memory Pool
        /// <summary>
        /// Main method for creating the node. We need thin in order to make memory pool for GPNode
        /// </summary>
        /// <returns></returns>
        public static GANumChromosome NewChromosome()
        {
#if MEMORY_POOLING
                var ch= Get();
                ch.fitness = float.MinValue;
                return ch;
#else
            return new GANumChromosome();
#endif
        }

        public void Destroy()
        {
#if MEMORY_POOLING
            if (this != null)
            {
                this.fitness = float.MinValue;
                val = null;
                Free(this);
            }
#endif
        }

#endregion

    }
}
