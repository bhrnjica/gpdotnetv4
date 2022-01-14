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
using GPdotNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNET.Engine.ANN
{
    public abstract class Layer
    {
        public int m_InputCount;
        public int m_NeuronCount;
        public ANNNeuron[] m_Neurons;
        public double[] m_output;
        public double[] m_Gradients;

        public abstract double[] CalculateOutput(double[] input);

        public abstract string[] GenerateFormula(string[] input);

    }
}
