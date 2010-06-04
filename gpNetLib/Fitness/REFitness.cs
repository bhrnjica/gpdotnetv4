using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPNETLib
{
    /// <summary>
    /// GPdotNET 4.0 implements the Relativefitness function. For some Symbolic Regression problems 
    /// it is important to evolve a model that performs well for all fitness cases within a certain 
    /// relative error (the precision) of the correct value.
    /// </summary>
    [Serializable]
    public class REFitness:IFitnessFunction
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
                    c.Fitness = 0;

                rowFitness += Math.Abs((y - gpTerminalSet.TrainingData[i][indexOutput]) / gpTerminalSet.TrainingData[i][indexOutput]);
            }
            //Fitness
            c.Fitness = (float)((1.0 / (1.0 + rowFitness)) * 1000.0);
        }

        #endregion
    }
}
