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

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Currently avaliable types of models
    /// </summary>
    public enum GPModelType
    {
        SR = 1,                                 //SymbolicRegression
        SRO = 2,                                // Symbolic Regression With Optimization
        TS=3,                                   //Times Series
        AO=4,                                   //Optimization of Analytic function 
        TSP = 5,                                //traveling salesman problem
        AP = 6,                                 //Assignment problem
        TP = 7,                                 //Transportation problem
        ANNMODEL = 8,                           //Modeling and prediction with ANN
        GPMODEL = 9,                            //New GUI For GP Models
    }
    /// <summary>
    /// Currently implemented solvers
    /// </summary>
    public enum GPRunType
    {
        //Modelling with Genetic Progamming
        GPMODSolver = 1,
        //Optimization with Genetic Algorithm
        GAOPTSolver = 2,
        //TSP Solver with Genetic Algorithm
        GATSPSolver = 3,
        //AP Solver with Genetic Algorithm
        GAAPSolver = 4,
        //TP Solver with Genetic Algorithm
        GATPSolver = 5,
        //Modelling with Artifical Neural Network
        ANNMODSolver = 6,
    }
}
