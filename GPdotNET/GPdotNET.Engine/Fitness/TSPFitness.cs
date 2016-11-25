//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2013 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See licence section of  http://gpdotnet.codeplex.com/license                         //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNET.Core;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Fitness function for calculation total path in TSP problem.
    /// </summary>
    public class TSPFitness : IFitnessFunction
    {
       
        /// <summary>
        /// Evaluates function agains terminals
        /// </summary>
        /// <param name="chromosome"></param>
        /// <param name="functionSet"></param>
        /// <returns></returns>
public float Evaluate(IChromosome chromosome, IFunctionSet functionSet)
{
    var ch = chromosome as GAVChromosome;
    if (ch == null)
        return 0;
    else
    {
        double y = 0, fitness;
        double[] p1 = null;
        double[] p2 = null;
        int rCount = ch.Length;
        for (int i = 0; i < rCount; i++)
        {
            p1 = Globals.GetTerminalRow(ch.Value[i]);
            if (i + 1 == rCount)
                p2 = Globals.GetTerminalRow(ch.Value[0]);
            else
                p2 = Globals.GetTerminalRow(ch.Value[i+1]);

            // calculate distance betwee two points and make the sum 
            y += Math.Sqrt((p2[0] - p1[0]) * (p2[0] - p1[0]) + (p2[1] - p1[1]) * (p2[1] - p1[1]));

            // check for correct numeric value
            if (double.IsNaN(y) || double.IsInfinity(y))
                return float.NaN;
        }
        //with this value we always search for maximum value of fitness
        fitness = ((1.0 / (1.0 + y)) * 1000.0);

        return (float)fitness;
    }
}
    }
}
