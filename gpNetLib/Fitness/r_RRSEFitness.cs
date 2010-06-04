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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Root Relative Squared Error (rRRSE, with the small "r" indicating that it is based on the relative error rather than the absolute).
    /// The rRRSE fitness function of GPdotNET is, as expected, based on the standard root relative squared error, 
    /// which is usually based on the absolute error, but obviously the relative error can also be used in order to create a slightly 
    /// different fitness measure.The relative squared error is relative to what it would have been if a simple predictor had been used. 
    /// More specifically, this simple predictor is just the average of the actual values. Thus, the relative squared error takes the 
    /// total squared error and normalizes it by dividing by the total squared error of the simple predictor. By taking the square
    /// root of the relative squared error one reduces the error to the same dimensions as the quantity being predicted.
    /// </summary>
    [Serializable]
    public class r_RRSEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<int> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {
            c.Fitness = 0;
            double rowFitness = 0.0;
            double val1 = 0;
            double val2 = 0;
            double y;
            // copy constants

            //Translate chromosome to list expressions
            int indexOutput = gpTerminalSet.NumConstants + gpTerminalSet.NumVariables;
            for (int i = 0; i < gpTerminalSet.RowCount; i++)
            {
                // evalue the function
                y = gpFunctionSet.Evaluate(lst, gpTerminalSet, i);
                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;

                val1 += Math.Pow(((y - gpTerminalSet.TrainingData[i][indexOutput]) / gpTerminalSet.TrainingData[i][indexOutput]), 2.0);
                val2 += Math.Pow(((gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue) / gpTerminalSet.AverageValue), 2.0);

            }

            rowFitness =Math.Sqrt(val1 / val2);

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
            {
                //if output is not a number return infinity fitness
                c.Fitness = 0;
                return;
            }
            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness)) * 1000.0);
        }

        #endregion
    }
}
