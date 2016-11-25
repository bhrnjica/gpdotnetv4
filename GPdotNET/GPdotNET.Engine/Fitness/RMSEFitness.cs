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
using GPdotNET.Core;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Default Fitness Function in GPdotNET
    /// GPdotNET 4.0 implements the Root Mean Squared Error (RMSE). The RMSE fitness function of GPdotNET is, as expected, 
    /// based on the standard root AverageValue squared error. By taking the square root of the AverageValue squared error one 
    /// reduces the error to the same dimensions as the quantity being predicted.
    /// </summary>

    public class RMSEFitness : IFitnessFunction
    {
        
        public float Evaluate(IChromosome ch, IFunctionSet functionSet)
        {
            var expTree = ((GPChromosome)ch).expressionTree;

            double fitness = 0;
            double rowFitness = 0.0;
            double y;
           
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
            }

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
                fitness = float.NaN;
            else//Rootmean square error
                fitness = ((1.0 / (1.0 + Math.Sqrt(rowFitness / Globals.gpterminals.RowCount))) * 1000.0);

            return (float)Math.Round(fitness,2);
        }

    }
}
