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
using System.Linq;
namespace GPdotNET.Core
{
    /// <summary>
    /// This class provides training and testing (if exists) data for GP.
    /// </summary>
    public interface ITerminalSet
    {
         double[][] TrainingData { get; set; }
         int NumVariables { get; set; }
         int NumConstants { get; set; }

         int[] Sources { get; set; }
         int[] Destinations { get; set; }

         double AverageValue { get; set; }
         double MaxValue { get; set; }
         double MinValue { get; set; }
         bool IsTimeSeries { get; set; }
         int RowCount { get; set; }

       

        //Calculate statistic data for TerminalSet.
        void CalculateStat();
    }
}
