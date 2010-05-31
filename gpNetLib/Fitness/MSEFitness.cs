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

namespace GPNETLib
{
    /// <summary>
    /// Implement Mean Square Error fitness function
    /// </summary>
    [Serializable]
    public class MSEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<int> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {
            c.Fitness = 0;
            double rowFitness = 0.0;
            double y,temp;
           
            //Translate chromosome to list expressions
            int indexOutput = gpTerminalSet.NumConstants + gpTerminalSet.NumVariables;
            for (int i = 0; i < gpTerminalSet.RowCount; i++)
            {
                // evalue the function
                y = gpFunctionSet.Evaluate(lst, gpTerminalSet, i);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;
                temp=y - gpTerminalSet.TrainingData[i][indexOutput];
                rowFitness += temp*temp;
            }
            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness / indexOutput)) * 1000.0);
        }

        #endregion
    }
}
