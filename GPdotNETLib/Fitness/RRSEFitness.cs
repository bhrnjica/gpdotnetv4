using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Root Relative Squared Error (RRSE) fitness function. The RRSE fitness function of GPdotNET is, as expected, 
    /// based on the standard root relative squared error, which, on its turn, is based on the absolute error. The relative squared error 
    /// is relative to what it would have been if a simple predictor had been used. More specifically, this simple predictor 
    /// is just the average of the actual values. Thus, the relative squared error takes the total squared error and normalizes
    /// it by dividing by the total squared error of the simple predictor. By taking the square root of the relative squared 
    /// error one reduces the error to the same dimensions as the quantity being predicted.
    /// </summary>
    [Serializable]
    public class RRSEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<ushort> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {
            c.Fitness = 0;
            double rowFitness = 0.0;
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

                //Calculate square error
                rowFitness += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2);
                //Calculate square error
                SS_err += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2);
                SS_tot += Math.Pow(gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue, 2);
            }

            rowFitness =Math.Sqrt(rowFitness / SS_tot);

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
            {
                //if output is not a number return zero fitness
                c.Fitness = float.NegativeInfinity;
                c.RSquare = float.NegativeInfinity;
                return;
            }

            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness / gpTerminalSet.RowCount)) * 1000.0);

            //R Square
            c.RSquare = (float)(1 - (SS_err / SS_tot));
        }

        #endregion
    }
}
