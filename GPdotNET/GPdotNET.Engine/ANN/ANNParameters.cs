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
using GPdotNET.Core.Experiment;
using GPdotNET.Engine.PSO;
namespace GPdotNET.Engine.ANN
{
    public enum LearningAlgoritm
    {
        BP,
        PSO,
        GA
    }
    public class ANNParameters
    {
        //Number of iterations
        //public int m_Iterations;

        //
        public double m_Momentum;

        //Activation function
        public IANNActivation m_ActFunction;

        //Parameters of activation function
        public double m_ActFuncParam1;

        //learning rate interval
        public double m_LearningRate;

        //Number of hidden nodes
        public int m_NumHiddenLayers;

        //Number of neuron in hiddne number
        public int m_NeuronsInHiddenLayer;

        public NormalizationType m_inputNormalization;
        public NormalizationType m_outputNormalization;

        public LearningAlgoritm m_LearningAlgo;


        //Problem type
        public bool m_IsClasificationProblem;

        public PSOParameters m_PSOParameters;
        public ANNParameters()
        {
            //
            m_LearningAlgo = LearningAlgoritm.BP;
            m_PSOParameters = new PSOParameters();
            m_IsClasificationProblem = false;
            m_Momentum = 0.4;
            m_inputNormalization = NormalizationType.MinMax;
            m_outputNormalization = NormalizationType.MinMax;
            m_ActFunction = new Sigmoid(1.0);
            m_ActFuncParam1 = 1.0;
            m_LearningRate = 0.1;
            m_NumHiddenLayers = 1;
            m_NeuronsInHiddenLayer = 50;
            
        }

        
    }
}
