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
            //double rowFitness1 = 0.0;
            //double rowFitness2 = 0.0;
            double SS_err = 0.0;
            double SS_tot = 0.0;
            double y;
            // copy constants

            //Translate chromosome to list expressions
            int indexOutput = gpTerminalSet.NumConstant + gpTerminalSet.NumVariable;
            for (int i = 0; i < gpTerminalSet.RowCount; i++)
            {
                // evalue the function
                y = gpFunctionSet.Evaluate(lst, gpTerminalSet, i);


                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;

               // rowFitness1 += Math.Pow((y - gpTerminalSet.data[i][indexOutput]) / gpTerminalSet.data[i][indexOutput], 2);
               // rowFitness2 += Math.Pow((y - gpTerminalSet.srVrijednost) / gpTerminalSet.srVrijednost, 2);
               // rowFitness += rowFitness1 / rowFitness2;
                rowFitness += Math.Pow(y - gpTerminalSet.data[i][indexOutput], 2) / indexOutput;
                SS_err += Math.Pow(y - gpTerminalSet.data[i][indexOutput], 2);
                SS_tot += Math.Pow(gpTerminalSet.data[i][indexOutput] - gpTerminalSet.srVrijednost, 2);
            }

          
            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness)) * 1000.0);
            //R Square
            c.RSquare = (float)(1 - (SS_err / SS_tot));
        }

        #endregion
    }
}
