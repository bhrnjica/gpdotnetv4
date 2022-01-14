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
    /// Binary Clasiffer Neural Network
    /// </summary>
    public class BCNeuralNetwork: NeuralNetwork
    {
        #region Ctor Initialization
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="normalizedInputCount">number of input variables after normalization. In case of category column type we need to apply 1 og N rule.</param>
        /// <param name="normalizedOutputCount">number of output variables after normalization. In case of category column type we need to apply 1 og N rule.</param>
        public BCNeuralNetwork(ANNParameters param, int normalizedInputCount, int normalizedOutputCount)
            : base(param, normalizedInputCount, normalizedOutputCount)
        {
          

        }

        public override void InitializeNetwork()
        {
            //
            m_Layers = new Layer[m_Parameters.m_NumHiddenLayers + 1];

            //create neurons in hidden layers
            for(int i=0; i<m_Parameters.m_NumHiddenLayers; i++)
            {
                if(i==0)
                    m_Layers[i] = new ANNLayer(m_InputCount, m_Parameters.m_NeuronsInHiddenLayer, m_Parameters.m_ActFunction);
                else
                    m_Layers[i] = new ANNLayer(m_Parameters.m_NeuronsInHiddenLayer, m_Parameters.m_NeuronsInHiddenLayer, m_Parameters.m_ActFunction);
            }
                
            //create neurons array for the last layer, with logistic Sigfmoid activation
            Layer ly = new ANNLayer(m_Parameters.m_NeuronsInHiddenLayer, m_OutputCount, new Sigmoid(1.0));
            m_Layers[m_Parameters.m_NumHiddenLayers] = ly;
           
        }
        #endregion

        #region Private Methods
        public double Cross_EntropyError(double[] realOutput)
        {
            //last čayer is output layer
            var outLayer = m_Layers.LastOrDefault();
            if (outLayer == null)
                throw new Exception("Layer collection is empty");

            //
            double totalError = 0;
            var neuroCount = outLayer.m_Neurons.Length;
            ///
            for (int i = 0; i < neuroCount; i++)
            {
                var neuro = outLayer.m_Neurons[i];
                //
                totalError += (realOutput[i] * Math.Log(neuro.m_Output) + (1 - realOutput[i]) * Math.Log(1 - neuro.m_Output));
                    
            }

            //
            return totalError ;
        }
        #endregion

        #region Public and Internal  Methods
        
        /// <summary>
        /// Calculation error based on BackPropagation algorithm
        /// </summary>
        /// <param name="realOutput">desired values</param>
        /// <returns></returns>
        public override double CalculateError(double[] realOutput )
        {

            //calculate cross entropy for output layer
            double e = Cross_EntropyError(realOutput);

            //return squared error of the output
            return e;
        }

        /// <summary>
        /// Calculates new weights value by using PSO 
        /// </summary>
        /// <param name="input"> input from the previous layer</param>
        public override void RecalculateWeights(double[] input)
        {
            int l = 0; // points into weights param
           //iterate all layers in the Neural Network
            for (int i = 0; i < m_Layers.Length; i++ )
            {
                var layer = m_Layers[i];
                for (int j = 0; j < layer.m_Neurons.Length; j++)
                {
                    var neuro = layer.m_Neurons[j];
                    for (int k = 0; k < neuro.m_Weights.Length; k++)
                    {
                        //set new value for weight
                        neuro.m_Weights[k] = input[l++];
                    }
                    //set new value for bias
                    neuro.m_Biases=input[l++];
                }
            }
        }

        #endregion
    }
}
