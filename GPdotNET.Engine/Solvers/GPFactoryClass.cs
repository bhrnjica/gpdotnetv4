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
using System.Threading;
using GPdotNET.Core.Experiment;
using GPdotNET.Engine.ANN;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Main class for creating and running GP program for clasiffication.
    /// </summary>
    public class GPFactoryClass : GPFactory
    {
        public GPFactoryClass() : base()
        {

        }

        public double[][] CalculateTrainModel(GPChromosome ch)
        {
            return CalculateModel(ch, true);
        }

        public double[][] CalculateTestModel(GPChromosome ch)
        {
            return CalculateModel(ch, false);
        }

        /// <summary>
        /// Calculates model/prediction using best chromosome. Also it denormalized valuse if they  were normalized
        /// </summary>
        /// <param name="ch"> bestchromosomes</param>
        /// <param name="testData"></param>
        /// <returns></returns>
        protected override double[][] CalculateModel(GPChromosome ch, bool btrainingData = true)
        {
            if (ch != null)
            {
                var pts = GPdotNET.Core.Globals.CalculateGPModel(ch.expressionTree, btrainingData);
                if (pts == null)
                    return null;

                //calculate output
                var model = new double[1][];
                var outv = new double[1][];
                if (m_Experiment != null)
                {
                    outv[0] = new double[1];
                    model[0] = new double[pts.Length];
                    for (int i = 0; i < pts.Length; i++)
                    {
                        outv[0][0] = pts[i];
                        var outv1 = m_Experiment.GetGPDenormalizedOutputRow(outv[0]);
                        model[0][i] = outv1[0];
                    }
                }
                else
                    model[0] = null;

                return model;
            }
            else
                return null;
        }
    }
}
