// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// gpNETLib 
using System.Collections.Generic;

namespace GPNETLib
{
    /// <summary>
    /// Interface for implementing Custom fitness function for chromosome evaluation.
    /// Important note is that every class derived form this interface must implement Serialize atribut in order to allow serilization of population.
    /// </summary>
    public interface IFitnessFunction
    {
        void Evaluate(List<int> lst, GPFunctionSet gpFunctionSet, GPTerminalSet gpTerminalSet, GPChromosome c);
    }
}
