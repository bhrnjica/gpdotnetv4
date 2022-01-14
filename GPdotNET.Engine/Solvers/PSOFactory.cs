//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2012 Bahrudin Hrnjica                                                 //
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
using GPdotNET.Core;
using System.Threading;
using System.Threading.Tasks;
using GPdotNET.Engine.ANN;
using GPdotNET.Core.Interfaces;
using GPdotNET.Core.Experiment;
using GPdotNET.Engine.PSO;
using System.Globalization;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Main class for creating and running program.
    /// </summary>
    public class PSOFactory: AIFactory
    {
        private int                 m_expRowCount;
        private ParticleSwarm      m_psoAlgorithm;
        //Constructor
        public PSOFactory()
        {
            m_ExpectedValue = -1;
        }

        /// <summary>
        /// Before we start solver prepare all neccessery information
        /// </summary>
        /// <param name="termSet"></param>
        /// <param name="funSet"></param>
        /// <param name="annParams"></param>
        public override void PrepareAlgorithm(Experiment expData, ANNParameters annParams = null)
        {
            if (annParams == null || expData == null)
                throw new Exception("Argument value cannot be null");
           

            //reset iteration and network
            if (m_Network == null)
            {
                m_IterationCounter = 0;


                //depending on the type of the colum create adequate neural network
                var colType = expData.GetOutputColumnType();

                if(colType== ColumnDataType.Binary)//Binary Clasification
                    m_Network = new BCNeuralNetwork(annParams, expData.GetColumnInputCount_FromNormalizedValue(), expData.GetColumnOutputCount_FromNormalizedValue());
                else//multiclass classification
                    m_Network = new MCNeuralNetwork(annParams, expData.GetColumnInputCount_FromNormalizedValue(), expData.GetColumnOutputCount_FromNormalizedValue());

                m_Network.InitializeNetwork();

                
            }

            //
            m_Experiment            = expData;

            PSOParameters swarm = null;
            if (m_Parameters != null)
                swarm = m_Parameters.m_PSOParameters;
            else
            {
                m_psoAlgorithm = null;
                swarm = annParams.m_PSOParameters;
            }
               

            //
            m_Parameters              = annParams;
            m_Parameters.m_PSOParameters = swarm;

            m_expRowCount           = m_Experiment.GetRowCount();
            IsAlgorthmPrepared      = true;
            StopIteration           = false;

            float newfitness = 0;
            if (m_psoAlgorithm == null)
            {
                //initilaize swarm
                m_Parameters.m_PSOParameters.m_Dimension = m_Network.GetWeightsAndBiasCout();
                m_psoAlgorithm = new ParticleSwarm(m_Parameters.m_PSOParameters, CrossEntropy);
                //init 
                newfitness = m_psoAlgorithm.InitSwarm();
            }
           // else
            newfitness = m_psoAlgorithm.RunSwarm();


             var model = CalculateModel(false);
            
            //Send report for iteration
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = ProgramState.Started,
                LearningError = newfitness,
                CurrentIteration = 0,
                LearnOutput = model,
            };

            ReportProgress(rp);
        }

        internal override float RunIteration()
        {
            //calculate model
            var newFitness = m_psoAlgorithm.RunSwarm();
            var model = CalculateModel(false);
            var prediction = CalculateModel(true);
                       
            //Send report for iteration
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = ProgramState.Running,
                LearningError = (float)newFitness,
                CurrentIteration = m_IterationCounter,
                LearnOutput = model,
                PredicOutput = prediction
            };

            ReportProgress(rp);

            return 0;
        }

        public override float CalculateModel( ProgramState state= ProgramState.Running)
        {
            //prepare training result
            var model = CalculateModel(false);
            var prediction = CalculateModel(true);

            //Send report for iteration
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = state,
                LearningError = (float) m_psoAlgorithm.m_BestGlobalFitness,
                CurrentIteration = m_IterationCounter,
                LearnOutput = model,
                PredicOutput = prediction
            };

            ReportProgress(rp);
            return 0;
        }

        public override double[] CalculateModelForExport(bool isTest)
        {
            var val =  CalculateModel(isTest);

            return val[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GenerateFormula()
        {
            var input = new string[m_Experiment.GetColumnInputCount_FromNormalizedValue()];
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = "X" + (i + 1).ToString() + " ";
            }

            // compute the network's output
            var formula = m_Network.GenerateFormula(input);
            return formula;
        }


        private double[][] CalculateModel(bool testData=false)
        {
            //prepare training result
            int outputCount = m_Experiment.GetColumnOutputCount();
            int rowCount    = m_Experiment.GetRowCount(testData);
            double[][] model = new double[outputCount][];

            for (int j = 0; j < outputCount; j++)
                model[j] = new double[rowCount];

            // run learning procedure for all samples
            for (int i = 0; i < rowCount; i++)
            {
                //retrieve input and output for specific row
                var input = m_Experiment.GetNormalizedInput(i, testData);
                var output = m_Experiment.GetRowFromOutput(i, testData);

                // compute the network's output
                var norOut = m_Network.CalculateOutputs(input);

                //denormalize output
                var outVal = m_Experiment.GetDenormalizedOutputRow(norOut);
                for (int j = 0; j < outputCount; j++)
                    model[j][i] = outVal[j];

            }

            return model;
        }

        private double CrossEntropy(double[] newPosition)
        {
            m_Network.RecalculateWeights(newPosition);

            double sce = 0.0; // sum of cross entropy

            // run learning procedure for all samples
            for (int i = 0; i < m_expRowCount; i++)
            {
                //retrieve input and output for specific row
                var input = m_Experiment.GetNormalizedInput(i);
                var output = m_Experiment.GetNormalizedOutput(i);

                // compute the network's output
                var oo = m_Network.CalculateOutputs(input);

                // chek how current solution is good
                sce += m_Network.CalculateError(output);
            }

            return -sce;
        }

        protected override void FinishIteration()
        {
            
            //Send report for iteration
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = ProgramState.Finished,
                LearningError = m_ExpectedValue,
                CurrentIteration = m_IterationCounter,
                LearnOutput = null,
            };

            ReportProgress(rp);
        }

        public override string SaveFactory()
        {
            var str = m_ExpectedValue.ToString(CultureInfo.InvariantCulture) + ";";
            str += m_Network.WeightsToString() +";";
            str += m_psoAlgorithm.SaveAlgoritm();
            return str;
        }

        public override int LoadFactory(string strWeights)
        {
            var wi = strWeights.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            m_ExpectedValue = (float)double.Parse(wi[0], CultureInfo.InvariantCulture);
            var index = m_Network.WeightsFromString(wi.Skip(1).ToArray());
            m_psoAlgorithm.ParticleFromSTring(wi.Skip(index+1).ToArray());
            return index;
        }

    }
}
