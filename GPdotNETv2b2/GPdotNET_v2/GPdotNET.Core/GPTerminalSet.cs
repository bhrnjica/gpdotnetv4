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
        //training data for optimization and other single row data
        public double[] SingleTrainingData { get; set; }
        public double[][] TestingData { get; set; }

        public int NumVariables { get; set; }
        public int NumConstants { get; set; }
        public double AverageValue { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public bool IsTimeSeries { get; set; }
        public int RowCount { get; set; }

        private List<GPTerminal> _terminals;
        

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

            double maxValue = stat.Max();
            double minValue = stat.Min();
            double averageValue = stat.Average();
        }

        /// <summary>
        /// Generate Terminals from Training Data
        /// </summary>
        /// <returns></returns>
        public List<GPTerminal> GetTerminals()
        {
            _terminals = new List<GPTerminal>();
            //terminals as input variable
            for (int i = 0; i < NumVariables; i++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = false;
                ter.Name = "X"+(i+1).ToString();
                ter.Index = i;
                _terminals.Add(ter);

            }
            // terminal as random constants
            for (int j = 0; j < NumConstants; j++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = true;
                ter.Name = "R" + (j+1).ToString();
                ter.Index = j + NumVariables;
                _terminals.Add(ter);
            }

            return _terminals;
        }
    }
}
