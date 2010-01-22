using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the R-square fitness. The R-square fitness function of GPdotNET 4.0 is, 
    /// based on the standard R-square, which returns the square of the Pearson product moment correlation coefficient. 
    /// The Pearson product moment correlation coefficient is a dimensionless index that ranges from -1 to 1 and 
    /// reflects the extent of a linear relationship between the predicted values and the target values.
    /// </summary>
    [Serializable]
    public class PearsonsCFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<ushort> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {
            c.Fitness = 0;
            double valTP = 0.0;
            double valT = 0.0;
            double valP = 0.0;
            double valTT = 0;
            double valPP = 0;
            double SS_err = 0.0;
            double SS_tot = 0.0;
            double y;
            double PC = 0;
            // copy constants

            //Translate chromosome to list expressions
            int indexOutput = gpTerminalSet.NumConstants + gpTerminalSet.NumVariables;
            int numSamples = gpTerminalSet.RowCount;
            for (int i = 0; i < gpTerminalSet.RowCount; i++)
            {
                // evalue the function
                y = gpFunctionSet.Evaluate(lst, gpTerminalSet, i);
                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                {
                   
                    //if output is not a number return infinity fitness
                    y = 0;
                    c.Fitness = 0;
                    c.RSquare = 0;
                    return;
                }

                
                valTP += gpTerminalSet.TrainingData[i][indexOutput] * y;
                valT += gpTerminalSet.TrainingData[i][indexOutput];
                valP += y;
                valTT += gpTerminalSet.TrainingData[i][indexOutput] * gpTerminalSet.TrainingData[i][indexOutput];
                valPP += y*y;

                SS_err += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2);
                SS_tot += Math.Pow(gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue, 2);
            }

            //Pearson coeficient
            PC = (numSamples * valTP - valT * valP) / (Math.Sqrt((numSamples * valTT - valT * valT) * (numSamples * valPP - valP * valP)));

            if (double.IsNaN(PC) || double.IsInfinity(PC) || Math.Abs(PC)>1)
            {

                //if output is not a number return infinity fitness
                c.Fitness = float.NegativeInfinity;
                c.RSquare = float.NegativeInfinity;
                return;
            }
            //Fitness
            c.Fitness = (float)(PC*PC * 1000.0);

            //R Square
            c.RSquare = (float)(1 - (SS_err / SS_tot));
        }

        #endregion
    }
}
