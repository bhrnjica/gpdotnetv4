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
using GPdotNET.Engine.ANN;
using GPdotNET.Core;
namespace GPdotNET.Engine
{
    /// <summary>
    ///Common Interface of Neural Network
    /// </summary>
    public abstract class NeuralNetwork
    {
        #region Fields
        //Layers in ANN [Layer][position]
        internal Layer[]       m_Layers;

        //Neural network parameters
        protected ANNParameters   m_Parameters;
        public ANNParameters Parameters { get { return m_Parameters; } }

        //
        internal int              m_InputCount;
        internal int              m_OutputCount;
        internal double[]         m_Output;
        #endregion

        #region Ctor and Initialization
        public NeuralNetwork()
        { 
        }
        public NeuralNetwork(ANNParameters param, int inputCount, int outputCount)
        {
            m_Parameters = param;
            m_InputCount = inputCount;
            m_OutputCount = outputCount;

        }
        /// <summary>
        /// Initialization of the network
        /// </summary>
        public abstract void InitializeNetwork();
        #endregion
        /// <summary>
        /// Calculate the output for a given input for the Neural Network
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[] CalculateOutputs(double[] input)
        {
            double[] output = input;

            for (int i = 0; i < m_Layers.Length; i++)
            {
                output = m_Layers[i].CalculateOutput(output);
            }

            m_Output = output;

            return output;
        }

        /// <summary>
        /// Return the total number of all weight and biases 
        /// </summary>
        /// <returns></returns>
        public int GetWeightsAndBiasCout()
        {
            int l = 0; // points into weights param
            //iterate all layers in the Neural Network
            for (int i = 0; i < m_Layers.Length; i++)
            {
                var layer = m_Layers[i];
                for (int j = 0; j < layer.m_Neurons.Length; j++)
                {
                    var neuro = layer.m_Neurons[j];
                    for (int k = 0; k < neuro.m_Weights.Length; k++)
                    {
                        //set new value for weight
                        l++;
                    }
                    //set new value for bias
                    l++;
                }
            }
            return l;
        }
        /// <summary>
        /// Calculation error based on specific method algorithm
        /// </summary>
        /// <param name="realOutput">desired values</param>
        /// <returns></returns>
        public abstract double CalculateError(double[] realOutput);

        /// <summary>
        /// Calculates diference between current and desired solution then recalculates weights and biases 
        /// </summary>
        /// <param name="input"> input from the previous layer</param>
        public abstract void RecalculateWeights(double[] input);

    }
}
