using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// Interface for implementing Custom fitness function for chromosome evaluation.
    /// Important note is that every class derived form this interface must implement Serialize atribut in order to allow serilization of Population.
    /// </summary>
    public interface IFitnessFunction
    {
        void Evaluate(List<ushort> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c);
    }
}
