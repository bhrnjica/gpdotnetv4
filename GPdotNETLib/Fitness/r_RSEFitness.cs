using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Relative Squared Error (rRSE, with the small "r" indicating that it is based on the relative 
    /// error rather than the absolute) fitness function both with and without parsimony pressure. The version with parsimony 
    /// pressure puts a little pressure on the size of the evolving solutions, allowing the discovery of more compact models.
    /// The rRSE fitness function of GPdotNET is, as expected, based on the standard relative squared error, which is usually
    /// based on the absolute error, but obviously the relative error can also be used in order to create a slightly different fitness measure.
    /// The relative squared error is relative to what it would have been if a simple predictor had been used. More specifically,
    /// this simple predictor is just the average of the actual values. Thus, the relative squared error takes the total squared
    /// error and normalizes it by dividing by the total squared error of the simple predictor.
    /// </summary>
    [Serializable]
    public class r_RSEFitness:IFitnessFunction
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
                    //if output is not a number return zero fitness
                    c.Fitness = float.NegativeInfinity;
                    c.RSquare = float.NegativeInfinity;
                    return;
                }

                val1 += Math.Pow(((y - gpTerminalSet.TrainingData[i][indexOutput]) / gpTerminalSet.TrainingData[i][indexOutput]), 2.0);
                val2 += Math.Pow(((gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue) / gpTerminalSet.AverageValue), 2.0);

                SS_err += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2);
                SS_tot += Math.Pow(gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue, 2);
            }

            rowFitness = val1 / val2;

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
            {
                //if output is not a number return zero fitness
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
