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
    public class TRANSFitness : IFitnessFunction
    {
        public bool IsMinimize { get; set; }
        /// <summary>
        /// Evaluates function agains terminals
        /// </summary>
        /// <param name="chromosome"></param>
        /// <param name="functionSet"></param>
        /// <returns></returns>
        public float Evaluate(IChromosome chromosome, IFunctionSet functionSet)
        {
            var ch = chromosome as GAMChromosome;
            if (ch == null)
                return 0;
            else
            {
                double y = 0, fitness;

                for (int i = 0; i < ch.NumRow; i++)
                {
                    for (int j = 0; j < ch.NumCol; j++)
                    {
                        var v = Globals.GetTerminalValue(i, j);
                        if (IsMinimize)
                        {
                            if (double.MinValue == v)
                                return float.PositiveInfinity;
                        }
                        else
                        {
                            if (double.MaxValue == v)
                                return float.NegativeInfinity;
                        }

                        // calculate distance between two points and make the sum 
                        y += v * ch.Value[i][j];

                        // check for correct numeric value
                        if (double.IsNaN(y) || double.IsInfinity(y))
                            return float.NaN;
                    }
                }
                //with this value we always search for maximum value of fitness
                if(IsMinimize)
                    fitness = ((1.0 / (1.0 + y)) * 1000.0);
                else
                    fitness = y;

                return (float)fitness;
            }
        }
    }
}
