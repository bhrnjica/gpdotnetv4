//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2012 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
namespace GPdotNET.Core
{
    /// <summary>
    /// Interface for implementing Custom fitness function for chromosome evaluation.
    /// Important note is that every class derived form this interface must implement Serialize atribute in order to allow serilization of population.
    /// </summary>
    public interface IFitnessFunction
    {
       float Evaluate(IChromosome chromosome, IFunctionSet functionSet);
    }
}
