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

namespace GPdotNET.Engine
{
    /// <summary>
    /// Implement Binary type of GA chromosome, which will be use in future GA implementation 
    /// </summary>
    public class GABinChromosome
#if MEMORY_POOLING
                : Pool<GABinChromosome>,IChromosome
#else
 : IChromosome
#endif
    {

        #region Fields
        private int length = 0;			            // chromosome's length
        private ulong val = 0;		                // chromosome's value 
        private float fitness = float.MinValue;	    // chromosome's fitness

        public static IFunctionSet functionSet;

        /// <summary>
        /// Chromosome's maximum length
        /// </summary>
        public const int MaxLength = 1024;

        /// <summary>
        /// Chromosome's length
        /// </summary>
        public int Length
        {
            get { return length; }
        }

        /// <summary>
        /// Chromosome's value
        /// </summary>
        public ulong Value
        {
            get { return val & (0xFFFFFFFFFFFFFFFF >> (64 - length)); }
        }

        /// <summary>
        /// Max possible chromosome's value
        /// </summary>
        public ulong MaxValue
        {
            get { return 0xFFFFFFFFFFFFFFFF >> (64 - length); }
        }
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

        #endregion

        #region Ctor and initialisation
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GABinChromosome()
        {
            
            fitness = float.MinValue;
        }

        public GABinChromosome(int length)
        {
            this.length = Math.Max(2, Math.Min(MaxLength, length));
            // randomize the chromosome
            Generate();
        }

        /// <summary>
        /// Create deep copy of the chromoseme
        /// </summary>
        /// <returns></returns>
        public IChromosome Clone()
        {
            var clone = GABinChromosome.NewChromosome();
            clone.length= length;
            clone.val= val;
            clone.fitness= fitness;

            return clone;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Generate(int param = 0)
        {
            if(functionSet!=null)
                length = functionSet.GetNumVariables();

            byte[] bytes = new byte[length * 10];

            // generate value
            Globals.radn.NextBytes(bytes);
            val = BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Mutate()
        {
            val ^= ((ulong)1 << Globals.radn.Next(length));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch2"></param>
        public void Crossover(IChromosome ch2, int index1 = -1, int index2 = -1)
        {
            GABinChromosome p = (GABinChromosome)ch2;

            // check for correct pair
            if ((p != null) && (p.length == length))
            {
                int crossOverPoint = length - Globals.radn.Next(length - 1);
                ulong mask1 = 0xFFFFFFFFFFFFFFFF >> crossOverPoint;
                ulong mask2 = ~mask1;

                ulong v1 = val;
                ulong v2 = p.val;

                // calculate new values
                val = (v1 & mask1) | (v2 & mask2);
                p.val = (v2 & mask1) | (v1 & mask2);
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
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetRandomNode(int nodeCout)
        {
            if (nodeCout < 3)
                throw new Exception("Invalid number of chromosoem nodes.");
            //TODO:
            return Globals.radn.Next(3, nodeCout + 1);
        }




        /// <summary>
        /// STring representation for the chromosome. It is also used for serializing to txt file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            ulong tval = val;
            char[] chars = new char[length];

            for (int i = length - 1; i >= 0; i--)
            {
                chars[i] = (char)((tval & 1) + '0');
                tval >>= 1;
            }

            // return the result string
            return new string(chars);
        }

        public IChromosome FromString(string strCromosome)
        {
            return null;
        }
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IChromosome other)
        {
            if (other == null)
                return 1;
            return other.Fitness.CompareTo(this.Fitness);
        }


#region Memory Pool
        /// <summary>
        /// Main method for creating the node. We need thin in order to make memory pool for GPNode
        /// </summary>
        /// <returns></returns>
        public static GABinChromosome NewChromosome()
        {
#if MEMORY_POOLING
                var ch= Get();
                ch.fitness = float.MinValue;
                return ch;
#else
            return new GABinChromosome();
#endif
        }

        public void Destroy()
        {
#if MEMORY_POOLING
            if (this != null)
            {
                this.fitness = float.MinValue;
                Free(this);
            }
#endif
        }

#endregion

    }
}
