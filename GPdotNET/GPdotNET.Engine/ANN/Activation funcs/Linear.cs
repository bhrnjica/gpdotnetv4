//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Artifical Intelligence Tool                                                 //
// Copyright 2006-2014 Bahrudin Hrnjica                                                 //
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
using GPdotNET.Core.Interfaces;

namespace GPdotNET.Engine.ANN
{
    public class Linear: IANNActivation
    {

        public Linear()
        {
        }

        public double Calculate(double x)
        {
            return x;
        }


        public double Derivative(double x)
        {
            return 0;
        }
        public string StringFormula(string value)
        {
            return value;
        }

    }
}
