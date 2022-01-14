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
    /// GPdotNET 4.0 implements the Fitness for Clasification problems. It calculates division between number of correct and total rows.
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

                //this fitness algoritm is specialized only for Binary and Classification problems
                if (m_ProblemType == ColumnDataType.Categorical || m_ProblemType == ColumnDataType.Binary)
                {
                   var valClass = Experiment.getCategoryFromNumeric(y, Globals.classCount);

                    if (double.IsNaN(valClass))
                        return float.NaN;
                    //check is the calculated value correct 
                    var val = Globals.gpterminals.TrainingData[i][indexOutput] == valClass ? 1 : 0;
                    //add the result to the fitness
                    rowFitness += val;
                }
                else//thros exception if the problems is not as expected
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
