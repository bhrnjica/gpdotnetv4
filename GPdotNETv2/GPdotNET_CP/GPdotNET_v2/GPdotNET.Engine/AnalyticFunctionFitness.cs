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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNET.Core;

namespace GPdotNET.Engine
{

    public class AnalyticFunctionFitness : IFitnessFunction
    {
        private GPNode _funToOptimize;
        public bool IsMinimize { get; set; }
        public GPNode FunToOptimize 
        {
            get {
                return _funToOptimize;
            }
            set {
                _funToOptimize = value;
            }
        }
        public float Evaluate(IChromosome chromosome, IFunctionSet functionSet)
        {
            GANumChromosome ch = chromosome as GANumChromosome;
            if (ch == null)
                return 0;
            else
            {
                //prepare terminals
                var term = Globals.gpterminals.SingleTrainingData;
                for (int i = 0; i < ch.val.Length; i++)
                    term[i] = ch.val[i];

                var y = functionSet.Evaluate(_funToOptimize, -1);
               

                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;

                //Save output in to output variable
                term[term.Length - 1] = y;

                if (IsMinimize)
                    y *= -1;

                return (float)y;
            }
        }
    }
}
