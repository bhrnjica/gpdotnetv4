using GPdotNET.Core;
using GPdotNET.Engine;
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

namespace GPdotNET.Util
{
    /// <summary>
    /// GPdotNET 4.0 implements the Relative Absolute Error (RAE) fitness function. The RAE fitness function of GPdotNET is, 
    /// as expected, based on the standard relative absolute error, which, on its turn, is based on the absolute error.
    /// The relative absolute error is very similar to the relative squared error in the sense that it is 
    /// also relative to a simple predictor, which is just the average of the actual values. In this case, though,
    /// the error is just the total absolute error instead of the total squared error. Thus, the relative absolute
    /// error takes the total absolute error and normalizes it by dividing by the total absolute error of the simple predictor.
    /// </summary>
    
    public class RAEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public float Evaluate(IChromosome ch, IFunctionSet functionSet)
        {
            var expTree = ((GPChromosome)ch).expressionTree;

            double fitness = 0;
            double rowFitness = 0.0;
            double y, val1=0, val2=0;

            //index of output parameter
            int indexOutput = Globals.gpterminals.NumConstants + Globals.gpterminals.NumVariables;

            for (int i = 0; i < Globals.gpterminals.RowCount; i++)
            {
                // evalue the function agains eachh rowData
                y = functionSet.Evaluate(expTree, i);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    return float.NaN;

                //Calculate square error
                val1 += Math.Abs((y - Globals.gpterminals.TrainingData[i][indexOutput]));
                val2 += Math.Abs((Globals.gpterminals.TrainingData[i][indexOutput] - Globals.gpterminals.AverageValue));
            }

            rowFitness = val1 / val2;

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
                fitness = float.NaN;
            else
                fitness = (float)((1.0 / (1.0 + rowFitness)) * 1000.0);

            return (float)Math.Round(fitness, 2);
        }

        #endregion
    }
}
