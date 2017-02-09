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
    public class BipolarSign: IANNActivation
    {
        private double m_alpha;

        public BipolarSign(double alpha=1)
        {
            m_alpha = alpha;
        }

        public double Calculate(double x)
        {
             var retVal=  ( ( 2 / ( 1 + Math.Exp( -m_alpha * x ) ) ) - 1 );

             return retVal;
        }

        public double Derivative(double input)
        {
            //return (m_alpha * (1 - input * input) / 2);

            double y = Calculate(input);
            return (m_alpha * ( 1 - y * y ) / 2);
        }

        public string StringFormula(string value)
        {
            return string.Format("((2/(1 +  Exp(-{0} * {1})) -1)", m_alpha, value);
        }

    }
}
