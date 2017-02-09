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
using GPdotNET.Core;
using GPdotNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GPdotNET.Engine.ANN
{
    /// <summary>
    /// Artifical Neural Network implementation of the Neuron
    /// </summary>
    public class ANNNeuron
    {
        #region Fields
        //neuron activation function
        public IANNActivation function = null;

        //number of weights
        private int         m_Count;
        public double[]     m_Weights;
        public double[]     m_PrevDeltaWeights;

        //bias for the neuron
        public double       m_Biases;
        public double       m_PrevDeltaBiases;

        //output value of the neuron
        public double       m_Output;

        //initial weights bounds
        public static double       m_WeightMin=0, m_WeightMax=1;
        #endregion

        #region Ctor
        /// <summary>
       /// Neuron constructor must get weights count and activation function in order to successfuly create the neuron
       /// </summary>
       /// <param name="count"></param>
       /// <param name="fun"></param>
        public ANNNeuron(int count, IANNActivation fun)
        {
            //
            m_Count = count;
            function = fun;
            InitWeights();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initila weiths
        /// </summary>
        /// <param name="count"></param>
        private void InitWeights()
        {
            //initil weights array
            m_Weights = new double[m_Count];
            m_PrevDeltaWeights = new double[m_Count];

            //Get random initial values
            for (int i = 0; i < m_Count; i++)
                m_Weights[i] = Globals.radn.NextDouble(m_WeightMin, m_WeightMax);

            m_Biases = Globals.radn.NextDouble(m_WeightMin, m_WeightMax);
        }
        #endregion 

        #region Public methods
        public double Evaluate(double[] input)
        {
            if (input == null || input.Length != m_Count)
                throw new ArgumentNullException("input value cannot be null or empty.");

            if(function==null)
                throw new ArgumentNullException("Activation function cannot be null.");

            double val = 0;
            for (int i = 0; i < m_Count; i++)
                val += m_Weights[i] * input[i];

            val += m_Biases; 

            //calculate output value by activation fucntion 
            var retVal= function.Calculate(val);


            this.m_Output=retVal;

            return retVal;
        }

        public string Evaluate(string[] input)
        {
            if (input == null || input.Length != m_Count)
                throw new ArgumentNullException("input value cannot be null or empty.");

            if (function == null)
                throw new ArgumentNullException("Activation function cannot be null.");

            string val = "";
            for (int i = 0; i < m_Count; i++)
                val += m_Weights[i].ToString(CultureInfo.InvariantCulture)+"*"+ input[i];

            val = val + "+" + m_Biases.ToString(CultureInfo.InvariantCulture);

            //calculate output value by activation fucntion 
            var retVal = function.StringFormula(val);


           
            return retVal;
        }

        /// <summary>
        /// calculate derivation of the neuron value
        /// </summary>
        /// <returns></returns>
        internal double Derivative()
        {
            return function.Derivative(m_Output);
        }
        #endregion
    }
}
