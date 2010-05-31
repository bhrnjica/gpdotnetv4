// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// gpNETLib 
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
                rowFitness += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2) / indexOutput;
            }

          
            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness)) * 1000.0);
        }

        #endregion
    }
}
