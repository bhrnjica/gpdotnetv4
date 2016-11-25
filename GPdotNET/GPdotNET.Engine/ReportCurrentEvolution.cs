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

namespace GPdotNET.Core
{
    /// <summary>
    /// Delegate for reporting about current evolution
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void EvolutionHandler(object sender, ReportCurrentEvolutionEventArgs e);

    /// <summary>
    /// Class for passing information about current evolution
    /// </summary>
    public class ReportCurrentEvolutionEventArgs: EventArgs
    {
        public int CurrentEvolution{get;set;}
        public IChromosome BestChromosome { get; set; }
        public float AverageFitness{get;set;}
        public ReportGPType ReportType { get; set; }
    }


    public enum ReportGPType
    {
        Started = 1,
        Running = 2,
        Finished = 3,
    }
   
}
