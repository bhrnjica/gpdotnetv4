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

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Currently avaliable types of models
    /// </summary>
    public enum GPModelType
    {
        SymbolicRegression=1,
        SymbolicRegressionWithOptimization=2,
        TimeSeries=3,
        AnaliticFunctionOptimization=4,
        TSP = 5,
        ALOC = 6,
        Transport = 7,
    }
    public enum GPRunType
    {
        GPModelling = 1,
        GAOptimization = 2,
        GATSPProblem = 3,
        GAALOCProblem = 4,
        GATransport = 5,
    }
}
