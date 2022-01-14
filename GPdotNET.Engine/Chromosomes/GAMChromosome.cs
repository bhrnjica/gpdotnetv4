//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2013 Bahrudin Hrnjica                                                 //
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
    /// Implement Matrix type of  GA chromosome, that is used for Transport problem 
    /// </summary>
    public class GAMChromosome
#if MEMORY_POOLING
                : Pool<GAMChromosome>,IChromosome
#else
 : IChromosome
#endif
    {

        #region Fields
        protected int cols = 0;			                // number of chromosome's columns
        protected int rows = 0;			                // number of chromosome's rows
        protected int[][] value;		                 // chromosome's value 
        protected float fitness = float.MinValue;	    // chromosome's fitness

        public static ITerminalSet terminalSet;
        public static IFunctionSet functionSet;

        /// <summary>
        /// Chromosome's NumCol
        /// </summary>
        public int NumCol
        {
            get { return cols; }
        }
        /// <summary>
        /// Chromosome's NumRow
        /// </summary>
        public int NumRow
        {
            get { return rows; }
        }

        /// <summary>
        /// Chromosome's value
        /// </summary>
        public int[][] Value
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
        public GAMChromosome()
        {
            
            fitness = float.MinValue;
        }

        public GAMChromosome(int col, int row)
        {
            fitness = float.MinValue;
            value = null;
            this.cols = col;
            this.rows = row;
            // randomize the chromosome
            Generate();
        }

        /// <summary>
        /// Create deep copy of the chromoseme
        /// </summary>
        /// <returns></returns>
        public IChromosome Clone()
        {
            var clone = GAMChromosome.NewChromosome();
            clone.cols = cols;
            clone.rows = rows;
            clone.fitness = fitness;

            clone.value = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                clone.value[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    clone.Value[i][j] = value[i][j];
                }
            }
            return clone;
        }

    /// <summary>
    /// Initialize randomly matrix chromosome for transport problem based on book 
    /// Genetic Algorithm+Data Structure= Evolution programs
    /// </summary>
    /// <param name="r">r- number of rows (sources)</param>
    /// <param name="c">c- number of columns (destination)</param>
    /// <param name="src">src[] - source values</param>
    /// <param name="dest">dest[] - destination values</param>
    /// <returns>Cost matrix cost(i,j)</returns>
    internal static int[][] GenerateMatrix(int r, int c, int[] src, int[] dest)
    {
        //initi values
        int tot = r * c;
        var lst = new List<int>(tot);

        //prepare values for generation
        int counter = 0;
        var vals = new int[r][];
        for (int i = 0; i < r; i++)
        {
            vals[i] = new int[c];
            for (int j = 0; j < c; j++)
            {
                lst.Add(counter);
                counter++;
            }
        }

        while (true)
        {
            //exit from while must be before list is empty
            if (lst.Count == 0)
                throw new Exception("null");

            int ind = Globals.radn.Next(lst.Count);
            int q = lst[ind];

            int i = (int)Math.Floor((double)(q) / (double)(c));
            int j = q % c;

            //if element is visited generate again random number
            if (vals[i][j] != 0)
                continue;
                
            lst.RemoveAt(ind);

            int val = Math.Min(src[i], dest[j]);
            vals[i][j] = val; 
            src[i] = src[i] - val;
            dest[j] = dest[j] - val;

            bool canBreak = true;
            for (int k = 0; k < r; k++)
                if (src[k] != 0)
                {
                    canBreak = false;
                    break;
                }

            if (canBreak == false)
                continue;

            for (int k = 0; k < c; k++)
                if (dest[k] != 0)
                {
                    canBreak = false;
                    break;
                }
            //if all sources and destination are zero, generation is finished
            if (canBreak)
                return vals;
        }
    }
        /// <summary>
        /// 
        /// </summary>
        public void Generate(int param = 0)
        {
            if (terminalSet != null)
            {
                cols = terminalSet.NumVariables;
                rows = terminalSet.RowCount;
            }
            else
                throw new Exception("Chromosome cannot be generated due TerminalSet is null.");

            //prepare helpers
            int [] localSource= new int[terminalSet.Sources.Length];
            int [] localDestination= new int[terminalSet.Destinations.Length];

            for(int i=0;i<terminalSet.Sources.Length; i++)
                localSource[i]=terminalSet.Sources[i];
            for(int i=0;i<terminalSet.Destinations.Length; i++)
                localDestination[i]=terminalSet.Destinations[i];

            value= GenerateMatrix(localSource.Length, localDestination.Length, localSource, localDestination);

            Validate();
        }
        /// <summary>
        ///  Crossover based on Book Genetic Algorithm +Data Structure = Evolution Programs.
        /// </summary>
        private void Validate()
        {
            var sour1 = new int[NumRow];
            var dest1 = new int[NumCol];
           
            for (int i = 0; i < NumRow; i++)
            {
                int sum1 = 0;
                for (int j = 0; j < NumCol; j++)
                  sum1 += Value[i][j];
                  
                sour1[i] = sum1;
            }



            for (int j = 0; j < NumCol; j++)
            {
                int sum1 = 0;
                for (int i = 0; i < NumRow; i++)
                  sum1 += Value[i][j];
               
                dest1[j] = sum1;
            }

            //testing
            for (int i = 0; i < NumRow; i++)
            {
                if (sour1[i] != terminalSet.Sources[i])
                    throw new Exception("Chromosome Source is not valid.");
            }
            for (int j = 0; j < NumCol; j++)
            {
                if (dest1[j] != terminalSet.Destinations[j])
                    throw new Exception("Chromosome Destination is not valid.");
            }
        }

        /// <summary>
        ///  Mutation based on Book Genetic Algorithm +Data Structure = Evolution Programs.
        /// </summary>
        public void Mutate()
        {
            //choose random number of cols and rows
            int locRows = Globals.radn.Next(1,rows);
            int locCols = Globals.radn.Next(1,cols);
            //define array for holding random indexs
            var localRows = new int[locRows];
            var localCols = new int[locCols];
                                   
            //generate random source
            int counter = 0;
            while (true)
            {
                var num = Globals.radn.Next(rows);

                var found = false;
                for (var i = 0; i < localRows.Length; i++)
                {
                    if (localRows[i] == num)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    localRows[counter] = num;
                    counter++;
                }
                if (counter  == localRows.Length)
                    break;
            }
           
            //generate random destination
            counter = 0;
            while (true)
            {
                var num = Globals.radn.Next(cols);

                var found = false;
                for (var i = 0; i < localCols.Length; i++)
                {
                    if (localCols[i] == num)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    localCols[counter] = num;
                    counter++;
                }
                if (counter == localCols.Length)
                    break;
            }

            //perform mutation
            Mutate(locRows, locCols, localRows, localCols, this);
            
        }

        internal static int[][] Mutate(int r, int c, int[] rs, int[] cs,GAMChromosome ch)
        {
            var source = new int[r];
            var destination = new int[c];

            //calculate Source for random submatrix
            for (int i = 0; i < rs.Length; i++)
                for (int j = 0; j < cs.Length; j++)
                    source[i]+= ch.value[rs[i]][cs[j]];

            //calculate Destination for random submatrix
            for (int i = 0; i < cs.Length; i++)
                for (int j = 0; j < rs.Length; j++)
                    destination[i] += ch.value[rs[j]][cs[i]];

            var subMatrix= GAMChromosome.GenerateMatrix(r, c, source, destination);
          
            //merge generated submatrix to matrix 
            for (int i = 0; i < rs.Length; i++)
                for (int j = 0; j < cs.Length; j++)
                    ch.value[rs[i]][cs[j]] = subMatrix[i][j];

                return subMatrix;
        }
        /// <summary>
        /// Crossover based on Book Genetic Algorithm +Data Structure = Evolution Programs.
        /// </summary>
        /// <param name="ch2"></param>
        public void Crossover(IChromosome parent2, int index1 = -1, int index2 = -1)
        {
            
            GAMChromosome ch2= parent2 as GAMChromosome;
            if(ch2==null)
                throw new Exception("ch2 cannot be null!");

            int[] srcREM1 = new int[rows];
            int[] destREM1 = new int[cols];
            int[] srcREM2 = new int[rows];
            int[] destREM2 = new int[cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double m1 = value[i][j] + ch2.value[i][j];
                    double d = Math.Floor(m1 / 2.0);
                    int r = (((int)m1) % 2);
                    
                    value[i][j] = (int)d;
                    ch2.value[i][j] = (int)d;

                    srcREM1[i] += r;
                    destREM1[j] += r;
                    srcREM2[i] += r;
                    destREM2[j] += r;
                }
            }
            for (int i = 0; i < rows; i++)
            {
                srcREM1[i] /= 2;
                srcREM2[i] /= 2;
            }
            for (int j = 0; j < cols; j++)
            { 
                destREM1[j] /= 2;
                destREM2[j] /= 2; 
            }
            var mat1 = GenerateMatrix(rows, cols, srcREM1, destREM1);
            var mat2 = GenerateMatrix(rows, cols, srcREM2, destREM2);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    value[i][j] += mat1[i][j];
                    ch2.value[i][j] += mat2[i][j];
                }
            }
           
            return;

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
        /// String representation for the chromosome. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //
            string v = "";

            for (int i = 0; i < value.Length; i++)
            {
                for (int j = 0; j < value[i].Length; j++)
                {
                    v += value[i][j].ToString();
                    if(value[i].Length!=(j+1))
                        v += '_';
                }
                if (value.Length != (i + 1))
                    v += ':'.ToString();
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
            return CreateFromString(strCromosome);
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
        /// Create chromosome from string. 
        /// </summary>
        /// <param name="strCromosome">string containing chromosome data</param>
        /// <returns></returns>
        public static GAMChromosome CreateFromString(string strCromosome)
        {
            GAMChromosome ch = GAMChromosome.NewChromosome();
            var items = strCromosome.Replace("\r","").Split(';');
            
            
            //Fisrt item is Fitness. Fitness value must always be formated with POINT not COMMA
            float fitness = 0;
            if (!float.TryParse(items[0].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out fitness))
                fitness = 0;
            ch.fitness = fitness;

            try
            {
                var strrows = items[1].Split(':');
                ch.rows= strrows.Length;
                ch.value = new int[ch.rows][];

                for (int i = 0; i < ch.rows; i++)
                {
                    var strcols = strrows[i].Split('_');
                    ch.cols = strcols.Length;
                    ch.value[i] = new int[ch.cols];

                    for (int j = 0; j < ch.cols; j++)
                       ch.value[i][j] = int.Parse(strcols[j]);
                    
                }

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
        public static GAMChromosome NewChromosome()
        {
#if MEMORY_POOLING
                var ch= Get();
                ch.fitness = float.MinValue;
                ch.cols= 0;
                ch.rows = 0;
                return ch;
#else
            return new GAMChromosome();
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
