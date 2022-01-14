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
namespace GPdotNET.Core
{
    //In GP Terminal can be input variable or random  constant, so this class can hold Terminal 
    // whatever it is (variable or constant)
    // e.g. x1, x2, ... ili R1, R2 ...
    // Every terminal implements those methods and members.
    public class GPTerminal
    {
        //The name of the terminal, used for printing results of gp model
        public string Name { get; set; }

        //This is position of terminal in array
        public int Index { get; set; }

        //Values
        public double fValue { get; set; }
        public double minValue { get; set; }
        public double maxValue { get; set; }

        //Indicator is this terminal variable or constant
        public bool IsConstant { get; set; }

        //Used for printing gp model
        public override string ToString()
        {
            return Name;
        }
    }
}
