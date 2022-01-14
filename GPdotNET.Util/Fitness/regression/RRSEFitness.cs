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
    /// GPdotNET 4.0 implements the Root Relative Squared Error (RRSE) fitness function. The RRSE fitness function is 
    /// based on the standard root relative squared error, which is based on the absolute error. The relative squared error 
    /// is relative to what it would have been if a simple predictor had been used. More specifically, this simple predictor 
    /// is just the average of the actual values. Thus, the relative squared error takes the total squared error and normalizes
    /// it by dividing by the total squared error of the simple predictor. By taking the square root of the relative squared 
    /// error one reduces the error to the same dimensions as the quantity being predicted.
    /// </summary>
    
    public class RRSEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public float Evaluate(IChromosome ch, IFunctionSet functionSet)
        {
            var expTree = ((GPChromosome)ch).expressionTree;

            double fitness = 0;
            double rowFitness = 0.0;
            double y, SS_tot = 0;

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
                rowFitness += Math.Pow(y - Globals.gpterminals.TrainingData[i][indexOutput], 2);
                SS_tot += Math.Pow(Globals.gpterminals.TrainingData[i][indexOutput] - Globals.gpterminals.AverageValue, 2);
            }

            rowFitness =Math.Sqrt(rowFitness / SS_tot);

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
                fitness = float.NaN;
            else
                fitness = (float)((1.0 / (1.0 + rowFitness / Globals.gpterminals.RowCount)) * 1000.0);

            return (float)Math.Round(fitness, 2);
        }

        #endregion
    }
}
