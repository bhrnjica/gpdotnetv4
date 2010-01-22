using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Absolute/Hits fitness function. For some Symbolic Regression problems it 
    /// is important to evolve a model that performs well for all fitness cases within a certain absolute 
    /// error (the precision) of the correct value.
    /// </summary>
    [Serializable]
    public class AEHitsFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<ushort> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {
            //Ova vrijednost treba da se implementiran u korisnickom interfesju da je moze sam podeisti
            //Precision
            double p=0.01;

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
                    //if output is not a number return infinity fitness
                    c.Fitness = float.NegativeInfinity;
                    c.RSquare = float.NegativeInfinity;
                    return;
                }

                //Calculate apsolute error
                if (Math.Abs(y - gpTerminalSet.TrainingData[i][indexOutput]) > p)
                    rowFitness += 0;
                else
                    rowFitness += 1;

                SS_err += Math.Pow(y - gpTerminalSet.TrainingData[i][indexOutput], 2);
                SS_tot += Math.Pow(gpTerminalSet.TrainingData[i][indexOutput] - gpTerminalSet.AverageValue, 2);
            }

          
            //Fitness
            c.Fitness = (float)rowFitness;

            //R Square
            c.RSquare = (float)(1 - (SS_err / SS_tot));
        }

        #endregion
    }
}
