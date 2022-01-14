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
    public class ANNLayer:Layer
    {
        

        public ANNLayer(int inputs, int neurons, IANNActivation fun)
        {
            m_InputCount=inputs;
            m_NeuronCount = neurons;
            m_Neurons= new ANNNeuron[m_NeuronCount];
            m_Gradients = new double[m_NeuronCount];

            for (int i = 0; i < m_NeuronCount; i++)
                m_Neurons[i] = new ANNNeuron(m_InputCount, fun);

        }

        public override double[] CalculateOutput(double[] input)
        {
            var output = new double[m_NeuronCount];
            for (int i = 0; i < m_NeuronCount; i++)
            {
                var neuro = m_Neurons[i];
                output[i] = neuro.Evaluate(input);
            }
               

            m_output = output;
            return m_output;
        }

        public override string[] GenerateFormula(string[] input)
        {
            string[] formula = new string[m_NeuronCount];
            for (int i = 0; i < m_NeuronCount; i++)
            {
                var neuro = m_Neurons[i];
                formula[i] = neuro.Evaluate(input);
            }

            return formula;
        }
    }
}
