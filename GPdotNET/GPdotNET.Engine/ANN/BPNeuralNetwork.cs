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
    /// Back Propagation Neural Network
    /// </summary>
    public class BPNeuralNetwork : NeuralNetwork
    {
        

        #region Ctor and Initialization
        public BPNeuralNetwork(ANNParameters param, int inputCount, int outputCount)
            : base(param, inputCount, outputCount)
        {
          

        }
        public override void InitializeNetwork()
        {
            //
            m_Layers = new ANNLayer[m_Parameters.m_NumHiddenLayers + 1];

            //create neurons in hidden layers
            for(int i=0; i<m_Parameters.m_NumHiddenLayers; i++)
            {
                if(i==0)
                    m_Layers[i] = new ANNLayer(m_InputCount, m_Parameters.m_NeuronsInHiddenLayer, m_Parameters.m_ActFunction);
                else
                    m_Layers[i] = new ANNLayer(m_Parameters.m_NeuronsInHiddenLayer, m_Parameters.m_NeuronsInHiddenLayer, m_Parameters.m_ActFunction);
            }
                
            //create neurons and error array for the last layer, which is usualy 1
            m_Layers[m_Parameters.m_NumHiddenLayers] = new ANNLayer(m_Parameters.m_NeuronsInHiddenLayer, m_OutputCount, m_Parameters.m_ActFunction);
           
        }
        #endregion

        #region Private Methods
        private double GradientOLayer(double[] realOutput)
        {
            
            var outLayer = m_Layers.LastOrDefault();
            if (outLayer == null)
                throw new Exception("Layer collection is empty");

            //
            double totalError = 0;
            var neuroCount= outLayer.m_Neurons.Length;
            ///
            for(int i=0; i < neuroCount; i++)
            {
                var neuro = outLayer.m_Neurons[i];
                var der = neuro.Derivative();

                var err = (realOutput[i] - neuro.m_Output);
                outLayer.m_Gradients[i] = der * err;
                totalError += err * err;
            }

            //
            return totalError*0.5;
        }

        private void GradientHLayer(Layer hLayer, Layer prevLayer)
        {
            if (hLayer == null || prevLayer==null)
                throw new Exception("The Layer cannot be null");

            //
            for (int i = 0; i < hLayer.m_NeuronCount; i++)
            {
                var neuro = hLayer.m_Neurons[i];
                var der = neuro.Derivative();

                var err = 0.0;

                for (int j = 0; j < prevLayer.m_NeuronCount; j++)

                    err += prevLayer.m_Gradients[j] * prevLayer.m_Neurons[j].m_Weights[i]; 

                //calculate gradient for each inputs
                hLayer.m_Gradients[i] = err*der;
               
            }
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

            //calculate gradient for output layer
            double e = GradientOLayer(realOutput);

            //calculate gradients for hidden layers
            for (int i = m_Parameters.m_NumHiddenLayers - 1; i >= 0; i--)
            {

                //calculate gradients for current layer 
                GradientHLayer(m_Layers[i], m_Layers[i + 1]);
            }

            //return squared error of the output
            return e;
            
        }

        /// <summary>
        /// Calculates diference between current and desired solution then recalculates weights and biases 
        /// </summary>
        /// <param name="input"> input from the previous layer</param>
        public override void RecalculateWeights(double[] input)
        {
           //iterate all layers in the Neural Network
           for(int i=0; i<m_Layers.Length; i++)
           {
               var layer= m_Layers[i]; 

               for(int j=0;j<layer.m_NeuronCount; j++)
               {
                   var neuro= layer.m_Neurons[j];

                   //Calculate weghts
                   for(int k=0; k<neuro.m_Weights.Length; k++)
                   {
                       //set input for current layer
                       double inputValue = 0;
                       if(i==0)
                           inputValue=input[k];
                       else
                       {
                           var prevLay=  m_Layers[i - 1];
                           inputValue = prevLay.m_output[k];
                       }
                           

                       //Calculate delta 
                       double delta = m_Parameters.m_LearningRate * (1.0 - m_Parameters.m_Momentum) * layer.m_Gradients[j] * inputValue;

                       //recalculate current bias based on learning rate and gradients and momentum and previous delta
                        delta += m_Parameters.m_LearningRate * m_Parameters.m_Momentum * neuro.m_PrevDeltaWeights[k];

                        neuro.m_Weights[k] += delta;

                       //store delta for later use
                       neuro.m_PrevDeltaWeights[k] = delta;
                   }

                   //Calculate delta 
                   var bdelta = m_Parameters.m_LearningRate * (1.0 - m_Parameters.m_Momentum) * layer.m_Gradients[j] * 1.0;
                   
                  //recalculate current bias based on learning rate and gradients and momentum and previous delta
                   bdelta += m_Parameters.m_LearningRate * m_Parameters.m_Momentum * neuro.m_PrevDeltaBiases;

                   neuro.m_Biases += bdelta;
                   //store bias delta for later use
                   neuro.m_PrevDeltaBiases = bdelta;
               }

           }
        }

        #endregion
    }
}
