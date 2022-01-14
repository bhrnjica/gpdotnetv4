﻿using GPdotNET.Core;
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

namespace GPdotNET.Util
{
    /// <summary>
    /// Implement Mean Square Error fitness function
    /// </summary>
   
    public class MSEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

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

                var temp = y - Globals.gpterminals.TrainingData[i][indexOutput];
                rowFitness += temp*temp;
            }

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
                fitness = float.NaN;
            else
                fitness = (float)((1.0 / (1.0 + rowFitness / Globals.gpterminals.RowCount)) * 1000.0);

            return (float)Math.Round(fitness, 2);
        }

        #endregion
    }
}
