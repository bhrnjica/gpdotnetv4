﻿//////////////////////////////////////////////////////////////////////////////////////////
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
using System.Diagnostics;

namespace GPdotNET.Core
{
    /// <summary>
    /// Interface for FunctionSet. With this interface you can implement your own functionSet.
    /// </summary>
    public interface IFunctionSet 
    {
        List<GPFunction> GetFunctions();
        List<GPTerminal> GetTerminals();
        
        double Evaluate(GPNode treeExpression, int rowIndex);
        double Evaluate(GPFunction fun, params double[] tt);

        string DecodeExpression(GPNode treeExpression, bool bExcel = false);

        int GetNumVariables();
        double GetTerminalMaxValue(int index);
        double GetTerminalMinValue(int index);

    }
}
