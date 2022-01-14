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
using System.Linq;
using System.Collections.Generic;
namespace GPdotNET.Core
{
    /// <summary>
    /// This class provides training and testing (if exists) data for GP.
    /// </summary>
    
    public class GPTerminalSet:ITerminalSet
    {   //Structure of Terminal set: first independent variables x, than random constant R and last index position is Output Y
        //x1,x2, ... , xn, R1,R2, ... ,Rn, Y - one row in dataarray
        public double[][] TrainingData { get; set; }
        public double[] SingleTrainingData { get; set; }
        public double[][] TestingData { get; set; }

        public int[] Sources { get; set; }
        public int[] Destinations { get; set; }

        public int NumVariables { get; set; }
        public int NumConstants { get; set; }
        public double AverageValue { get; set; }
        public double StdDev { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public bool IsTimeSeries { get; set; }
        public int RowCount { get; set; }

        private Dictionary<int,GPTerminal> _terminals;
        

        public GPTerminalSet()
        {
            TrainingData = null;
            TestingData = null;
            IsTimeSeries = false;
        }

        //Calculate statistic data for TerminalSet.
        public void CalculateStat()
        {
            if (TrainingData == null)
                throw new Exception("Terminal set is empty!");
            if (NumVariables == 0)
                throw new Exception("The number of variables is 0!");

            int yindex = TrainingData[0].Length - NumConstants + NumConstants - 1;
            RowCount = (short)TrainingData.Length;

            var stat = from p1 in Enumerable.Range(0, RowCount)
                       from p2 in TrainingData
                       select p2[yindex];

            MaxValue = stat.Max();
            MinValue = stat.Min();
            AverageValue = stat.Average();
            StdDev = 0;
        }

        /// <summary>
        /// Generate Terminals from Training Data
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, GPTerminal> GetTerminals()
        {
            _terminals = new Dictionary<int,GPTerminal>();
            //terminals as input variable
            for (int i = 0; i < NumVariables; i++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = false;
                ter.Name = "X"+(i+1).ToString();
                ter.Index = i;
                _terminals.Add(ter.Index,ter);

            }
            // terminal as random constants
            for (int j = 0; j < NumConstants; j++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = true;
                ter.Name = "R" + (j+1).ToString();
                ter.Index = j + NumVariables;
                _terminals.Add(ter.Index ,ter);
            }
            
            CalculateStat();
            return _terminals;
        }

        /// <summary>
        /// Helper for serialization 
        /// </summary>
        /// <returns></returns>
        public string ToStringMaxMinValues()
        {
            string str = "";
            

            for (int i = 0; i < NumVariables; i++)
            {
                double min = double.MaxValue;
                double max = double.MinValue;

                for (int j = 0; j < TrainingData.Length; j++)
                {
                    if (TrainingData[j][i] > max)
                        max = TrainingData[j][i];
                    if (TrainingData[j][i] < min)
                        min = TrainingData[j][i];
                }

                // 
                str += min.ToString()+";"+max.ToString() + "\t";

            }

            return str;
        }
    }
}
