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

namespace GPdotNET.Core
{
    //Type of running program
    public enum ProgramState
    {
        Started = 1,
        Running = 2,
        Finished = 3,
    }

    /// <summary>
    /// Delegate for reporting about current evolution
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void EvolutionHandler(object sender, ProgressIndicatorEventArgs e);

    /// <summary>
    /// Class for passing information about current evolution
    /// </summary>
    public class ProgressIndicatorEventArgs: EventArgs
    {
        public int CurrentIteration{get;set;}
        public IChromosome BestChromosome { get; set; }
        public float AverageFitness{get;set;}
        public float LearningError { get; set; }

        public double[][] LearnOutput { get; set; }
        public double[][] PredicOutput { get; set; }

        public ProgramState ReportType { get; set; }
    }   
}
