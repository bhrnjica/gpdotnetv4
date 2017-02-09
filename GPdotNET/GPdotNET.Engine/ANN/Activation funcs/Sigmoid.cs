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
    public class Sigmoid: IANNActivation
    {
        private double m_alpha;
        
        public Sigmoid(double alpha)
        {
            m_alpha = alpha;
        }

        public double Calculate(double x)
        {
            var retVal = Math.Exp(-1.0 * m_alpha * x);

            return (1 / (1 + retVal));
        }


        public double Derivative(double input)
        {
            //return (m_alpha * input * (1 - input));
            double y = Calculate(input);

            return (m_alpha * y * (1 - y));
        }

        public string StringFormula(string value)
        {
            return string.Format("1 / 1 (1+ Exp(-1 * {0} * ({1})))", m_alpha, value);
        }
    }
}
