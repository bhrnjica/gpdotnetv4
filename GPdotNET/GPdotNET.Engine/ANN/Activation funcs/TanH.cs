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
    public class TanH: IANNActivation
    {
        public TanH()
        {
        }

        public double Calculate(double x)
        {
            if (x < -10.0) return -1.0;
            else if (x > 10.0) return 1.0;
            else return Math.Tanh(x);
        }


        public double Derivative(double x)
        {
            double y = Calculate(x);
            return (1 - y) * (1 + y);
        }

        public string StringFormula(string value)
        {
            return string.Format("Tanh({0})", value);
        }

    }
}
