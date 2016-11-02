using GPdotNET.Core;
using GPdotNET.Core.Experiment;
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
    /// GPdotNET 4.0 implements the Fitness for Clasification problems. It calculates division between correct and totaln rows.
    /// </summary>

    public class ClassFitness : IFitnessFunction
    {
        #region IFitnessFunction Members
        public ColumnDataType m_ProblemType = ColumnDataType.Binary;
        public ClassFitness(ColumnDataType ptype)
        {
            m_ProblemType = ptype;
        }
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

                //Calculate apsolute error
                if (m_ProblemType == ColumnDataType.Binary)
                {
                    if (y < 0)
                        rowFitness += (0 - Globals.gpterminals.TrainingData[i][indexOutput]) == 0 ? 1 : 0;
                    else
                        rowFitness += (1 - Globals.gpterminals.TrainingData[i][indexOutput]) == 0 ? 1 : 0;
                }
                else if (m_ProblemType == ColumnDataType.Categorical)
                {
                    int valClass = -1;
                    //calculate sigmoid for the fitenss
                    var val1 = Math.Exp(-1.0 * y);
                    val1 = Globals.classCount * (1 / (1 + val1));

                    for (int j = 1; j <= Globals.classCount; j++)
                    {
                        if (val1 <= j)
                        {
                            valClass = j - 1;
                            break;
                        }
                    }
                    //check if the value is correct 
                    var val = Globals.gpterminals.TrainingData[i][indexOutput] == valClass ? 1 : 0;
                    rowFitness += val;
                }
                else
                    throw new Exception("Problem type is unknown!");

            }

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
                fitness = float.NaN;
            else
                fitness = (float)(rowFitness / Globals.gpterminals.RowCount) * 1000.0;

            return (float)Math.Round(fitness, 2);
        }
        #endregion

    }
}
