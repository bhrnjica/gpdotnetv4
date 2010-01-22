using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Relative Absolute Error (rRAE, with the small "r" indicating that it is based on the relative 
    /// error rather than the absolute) fitness function. The rRAE fitness function of GPdotNET is, as expected, based on the 
    /// standard relative absolute error, which is usually based on the absolute error, but obviously the relative error can 
    /// also be used in order to create a slightly different fitness measure.
    /// The relative absolute error is very similar to the relative squared error in the sense that it is also relative to a 
    /// simple predictor, which is just the average of the actual values. In this case, though, the error is just the total 
    /// absolute error instead of the total squared error. Thus, the relative absolute error takes the total absolute error and 
    /// normalizes it by dividing by the total absolute error of the simple predictor.

    /// </summary>
    [Serializable]
    public class r_RAEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<ushort> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {
            c.Fitness = 0;
            double rowFitness = 0.0;
            double val1 = 0;
            double val2 = 0;
            double SS_err = 0.0;
            double SS_tot = 0.0;
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
                {
                    //if output is not a number return infinity  fitness
                    c.Fitness = float.NegativeInfinity;
                    c.RSquare = float.NegativeInfinity;
                    return;
                }

                //Calculate relative error
                val1 += Math.Abs(((y - gpTerminalSet.TrainingData[i][indexOutput]) / gpTerminalSet.TrainingData[i][indexOutput]));
                val2 += Math.Abs(((gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue) / gpTerminalSet.AverageValue));

                SS_err += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2);
                SS_tot += Math.Pow(gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue, 2);
            }

            rowFitness = val1 / val2;

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
            {
                //if output is not a number return infinity fitness
                c.Fitness = float.NegativeInfinity;
                c.RSquare = float.NegativeInfinity;
                return;
            }
            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness)) * 1000.0);

            //R Square
            c.RSquare = (float)(1 - (SS_err / SS_tot));
        }

        #endregion
    }
}
