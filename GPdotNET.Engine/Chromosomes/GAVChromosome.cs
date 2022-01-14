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
    /// Implement Vector type of  GA chromosome, that is used for TSP and Alocation problems 
    /// </summary>
    public class GAVChromosome
#if MEMORY_POOLING
                : Pool<GAVChromosome>,IChromosome
#else
 : IChromosome
#endif
    {

        #region Fields
        protected int length = 0;			            // chromosome's length
        protected int[] value;		                    // chromosome's value 
        protected float fitness = float.MinValue;	    // chromosome's fitness

        public static IFunctionSet functionSet;

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
        public int[] Value
        {
            get { return value; }
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
        public GAVChromosome()
        {
            
            fitness = float.MinValue;
        }

        public GAVChromosome(int length)
        {
            this.length = length;
            // randomize the chromosome
            Generate();
        }

        /// <summary>
        /// Create deep copy of the chromoseme
        /// </summary>
        /// <returns></returns>
        public IChromosome Clone()
        {
            var clone = GAVChromosome.NewChromosome();
            clone.length= length;
            clone.value = new int[clone.length];
            for (int i = 0; i < length; i++)
               clone.value[i]= value[i];
            
            clone.fitness = fitness;
            return clone;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Generate(int param = 0)
        {
            if(functionSet!=null)
                length = functionSet.GetNumVariables();
            
            value= new int[length];
            for (int i = 0; i < length; i++)
                value[i] = i;
            for (int i = 0, n = length >> 1; i < n; i++)
            {
                int randInd1 = Globals.radn.Next(length);
                int randInd2 = Globals.radn.Next(length);

                var temp = value[randInd1];
                value[randInd1] = value[randInd2];
                value[randInd2] = temp;
            }
         
        }

        /// <summary>
        /// 
        /// </summary>
        public void Mutate()
        {
            int randInd1 = Globals.radn.Next(length);
            int randInd2 = Globals.radn.Next(length);

            if(randInd1>randInd2)
            {
                int temp= randInd2;
                randInd2=randInd1;
                randInd1=temp;
            }
            int j = 0;
            for (int i = randInd1; i <= randInd2; i++,j++)
            {
                if (i > (randInd2 - j))
                    break;
                var temp1 = value[i];
                var temp2 = value[randInd2-j];
                value[i] = temp2;
                value[randInd2 - j] = temp1;
            }
        }

        /// <summary>
        /// Take random indexes and create two offspring based on parts.
        /// </summary>
        /// <param name="ch2"></param>
        public void Crossover(IChromosome parent2, int index1 = -1, int index2 = -1)
        {
            var ch2 = parent2 as GAVChromosome;
            if (ch2 == null)
                throw new Exception("Chromosome cannot be null!");

            int randInd1 = Globals.radn.Next(length);
            int randInd2 = Globals.radn.Next(ch2.Length);

            Crossover(this,ch2,randInd1,randInd2);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch1"></param>
        /// <param name="ch2"></param>
        /// <param name="randInd1"></param>
        /// <param name="randInd2"></param>
        internal static void Crossover(GAVChromosome ch1, GAVChromosome ch2, int randInd1, int randInd2)
        {
            if (randInd1 > randInd2)
            {
                int temp = randInd1;
                randInd1 = randInd2;
                randInd2 = temp;
            }

            //exchange genetic material
            for (int i = randInd1; i < randInd2; i++)
            {
                int temp = ch1.Value[i];
                ch1.Value[i] = ch2.Value[i];
                ch2.Value[i] = temp;
            }

            //fix if we have duplication
            int startedIndex1 = randInd1;
            int startedIndex2 = randInd2 - 1;
            for (int i = 0; i < ch1.Length; i++)
            {
                if (randInd1 <= i && i < randInd2)
                    continue;

                if (startedIndex1 < randInd2)
                {
                    //check for duplicate for child1
                    for (int j = randInd1; j < randInd2; j++)
                    {
                        bool isBreak = false;
                        if (ch1.Value[i] == ch1.Value[j])
                        {
                            for (int k = startedIndex1; k < randInd2; k++)
                            {
                                int val = ch2.Value[k];
                                bool unique = true;

                                for (int l = randInd1; l < randInd2; l++)
                                {
                                    if (val == ch1.Value[l])
                                    {
                                        unique = false;
                                        break;
                                    }
                                }
                                if (unique)
                                {
                                    ch1.Value[i] = val;
                                    startedIndex1 = k + 1;
                                    isBreak = true;
                                    break;
                                }
                            }
                        }
                        if (isBreak)
                            break;
                    }
                }
                //
                if (startedIndex2 >= randInd1)
                {
                    //check for duplicate for child2
                    for (int j = randInd1; j < randInd2; j++)
                    {
                        bool isBreak = false;
                        if (ch2.Value[i] == ch2.Value[j])
                        {
                            for (int k = startedIndex2; k >= randInd1; k--)
                            {
                                int val = ch1.Value[k];
                                bool unique = true;

                                for (int l = randInd1; l < randInd2; l++)
                                {
                                    if (val == ch2.Value[l])
                                    {
                                        unique = false;
                                        break;
                                    }
                                }
                                if (unique)
                                {
                                    ch2.Value[i] = val;
                                    startedIndex2 = k - 1;
                                    isBreak = true;
                                    break;
                                }
                            }
                        }
                        if (isBreak)
                            break;
                    }
                }
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


        public bool validate()
        {
            var ss = value.OrderBy(x => x).ToList();
            if (length != ss.Count())
                throw new Exception("Length is not corect.");
            for (int i = 0; i < length - 1; i++)
            {
                if (ss[i] == ss[i + 1])
                    throw new Exception("Chromosome is not valid!");
            }

            return true;
        }
        #endregion

        #region Operations

        /// <summary>
        /// 
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
        /// STring representation for the chromosome. 
        /// </summary>
        /// <returns></returns>
        public void Order(bool asc)
        {
            int[] tval = new int[length];

            //first find starting index, we want to star path from staring index
            int startingIndex = 0;
            for (int i = 0; i < length; i++)
            {
                if (asc)
                {
                    if (value[i] == 0)
                        startingIndex = i;
                }
                else
                {
                    if (value[i] == length - 1)
                        startingIndex = i;
                }
            }
            for (int j = 0; j < length; j++)
            {
                int i = startingIndex + j;
                if (i >= length)
                    i = i - length;

                tval[j] = value[i];

            }

            value = tval;
        }

        /// <summary>
        /// String representation for the chromosome which begins with first point. 
        /// </summary>
        /// <returns></returns>
        public string ToOrderString()
        {
            // int[] tval = value;
            string v = "";

            //first find starting index, we want to star path from staring index
            int startingIndex = 0;
            for (int i = 0; i < length; i++)
            {
                if (value[i] == 0)
                    startingIndex = i;
            }
            for (int j = 0; j < length; j++)
            {
                int i = startingIndex + j;
                if (i >= length)
                    i = i - length;

                v += "C" + (value[i] + 1).ToString();
                v += "-";
            }
            v += "C1";
            // return the result string
            return v;
        }

        /// <summary>
        /// String representation for the chromosome. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //
            string v = "";

            for (int i = 0; i < length; i++)
            {
                v += (value[i] + 1).ToString();
                if (i + 1 <length)
                  v += "_";
            }
            // return the result string
            return string.Format("{0};{1}", fitness.ToString(CultureInfo.InvariantCulture), v.ToString());
            
        }

        /// <summary>
        /// Generate chromosome from string
        /// </summary>
        /// <param name="strCromosome"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create chromosome from string. Chromosome string is stored with the folowing format.
        /// 10.34566;C1-C2-C234-....
        /// fitness;City1, CIty2,....
        /// </summary>
        /// <param name="strCromosome">string containing chromosome data</param>
        /// <returns></returns>
        public static GAVChromosome CreateFromString(string strCromosome)
        {
            GAVChromosome ch = GAVChromosome.NewChromosome();
            var items = strCromosome.Replace("\r", "").Split(';');

            //Fisrt item is Fitness. Fitness value must always be formated with POINT not COMMA
            float fitness = 0;
            if (!float.TryParse(items[0].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out fitness))
                fitness = 0;
            ch.fitness = fitness;
            
            try
            {
                var str = items[1].ToString().Split('_');
                ch.length = str.Length;
                ch.value = new int[ch.length];

                for (int i = 0; i < ch.length; i++)
                   ch.value[i] = int.Parse(str[i]) - 1;

                return ch;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

#region Memory Pool
        /// <summary>
        /// Main method for creating the node. We need thin in order to make memory pool for GPNode
        /// </summary>
        /// <returns></returns>
        public static GAVChromosome NewChromosome()
        {
#if MEMORY_POOLING
                var ch= Get();
                ch.fitness = float.MinValue;
                return ch;
#else
            return new GAVChromosome();
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
