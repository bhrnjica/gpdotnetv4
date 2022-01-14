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
    public class SoftmaxLayer : Layer
    {
        //public int m_InputCount;
        //public int m_NeuronCount;
        //public ANNNeuron[] m_Neurons;
        //public double[] m_output;
        //public double[] m_Gradients;

        public SoftmaxLayer(int inputs, int neurons, IANNActivation fun)
        {
            m_InputCount=inputs;
            m_NeuronCount = neurons;
            m_Neurons= new ANNNeuron[m_NeuronCount];
            m_Gradients = new double[m_NeuronCount];

            if (fun == null)
                fun = new Linear();

            for (int i = 0; i < m_NeuronCount; i++)
                m_Neurons[i] = new ANNNeuron(m_InputCount, fun);

        }

        public override double[] CalculateOutput(double[] input)
        {
            var output = new double[m_NeuronCount];
            for (int i = 0; i < m_NeuronCount; i++)
                output[i] = m_Neurons[i].Evaluate(input);

            m_output = Softmax(output);
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

        private double[] Softmax(double[] calculatedOutput)
        {
            double max = calculatedOutput[0];
            for (int i = 0; i < calculatedOutput.Length; ++i)
            {
                if (calculatedOutput[i] > max)
                    max = calculatedOutput[i];
            }

            double scale = 0.0;
            for (int i = 0; i < calculatedOutput.Length; ++i)
            {
                scale += Math.Exp(calculatedOutput[i] - max);
            }

            double[] result = new double[calculatedOutput.Length];
            for (int i = 0; i < calculatedOutput.Length; ++i)
            {
                result[i] = Math.Exp(calculatedOutput[i] - max) / scale;
                //store output to neuron for later use
                m_Neurons[i].m_Output=result[i];
            }

            return result;
        }
    }
}
