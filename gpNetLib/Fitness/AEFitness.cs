using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Absolute Error fitness function. 
    /// </summary>
    [Serializable]
    public class AEFitness:IFitnessFunction
    {
        #region IFitnessFunction Members

        public void Evaluate(List<int> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c)
        {

            c.Fitness = 0;
            double rowFitness = 0.0;
            double y;


            //Translate chromosome to list expressions
            int indexOutput = gpTerminalSet.NumConstants + gpTerminalSet.NumVariables;
            for (int i = 0; i < gpTerminalSet.RowCount; i++)
            {
                // evalue the function
                y = gpFunctionSet.Evaluate(lst, gpTerminalSet, i);
                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y=0;

                //Calculate apsolute error
                rowFitness=Math.Abs(y - gpTerminalSet.TrainingData[i][indexOutput]);
            }

            if (double.IsNaN(rowFitness) || double.IsInfinity(rowFitness))
            {
                //if output is not a number return zero fitness
                c.Fitness = 0;
                return;
            }

            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness / gpTerminalSet.RowCount)) * 1000.0);
        }

        #endregion
    }
}
