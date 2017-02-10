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
using System.Globalization;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Main class for creating and running program.
    /// </summary>
    public class BPFactory: AIFactory
    {
        

        private int                 m_expRowCount;


        //Constructor
        public BPFactory()
        {
            m_ExpectedValue = -1;
        }

        /// <summary>
        /// Before we start GP prepare all neccessery information
        /// </summary>
        /// <param name="termSet"></param>
        /// <param name="funSet"></param>
        /// <param name="annParams"></param>
        public override void PrepareAlgorithm(Experiment expData,ANNParameters annParams=null)
        {
            if (annParams == null || expData == null)
                throw new Exception("Argument value cannot be null");
           

            //reset iteration and network
            if (m_Network == null)
            {
                m_IterationCounter = 0;
                m_Network = new BPNeuralNetwork(annParams, expData.GetColumnInputCount_FromNormalizedValue(), expData.GetColumnOutputCount_FromNormalizedValue());
                m_Network.InitializeNetwork();
            }

            m_Experiment            = expData;
            m_Parameters              = annParams;

            IsAlgorthmPrepared      = true;
            StopIteration           = false;

            m_expRowCount           = m_Experiment.GetRowCount();

            //Send report for iteration
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = ProgramState.Started,
                LearningError = -1,
                CurrentIteration = 0,
                LearnOutput = null,
            };

            ReportProgress(rp);
        }
       
        internal override float RunIteration()
        {
            double error = 0.0;

            if(m_expRowCount<=0)
                m_expRowCount = m_Experiment.GetRowCount();

            // run learning procedure for all samples
            for (int i = 0; i < m_expRowCount; i++)
            {
                //retrieve input and output for specific row
                var input = m_Experiment.GetNormalizedInput(i);
                var output = m_Experiment.GetNormalizedOutput(i);

                // compute the network's output
                var oo= m_Network.CalculateOutputs(input);

                // chek how current solution is good
                error += m_Network.CalculateError(output);

                // recalculate weights and update them
                m_Network.RecalculateWeights(input);

            }

            m_ExpectedValue = 0;

            CalculateModel( ProgramState.Running);

            return (float)(error / m_expRowCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GenerateFormula()
        {
            var input = new string[m_Experiment.GetColumnInputCount_FromNormalizedValue()];
            for(int i=0; i < input.Length; i++)
            {
                input[i] = "X"+(i + 1).ToString()+" ";
            }

            // compute the network's output
            var formula = m_Network.GenerateFormula(input);
            return formula;
        }

        public override float CalculateModel(ProgramState state = ProgramState.Running)
        {
            //prepare training result
            int outputCount = m_Experiment.GetColumnOutputCount();
            double[][] model = new double[outputCount][];

            for (int j = 0; j < outputCount; j++)
                model[j] = new double[m_expRowCount];

            var temp = m_ExpectedValue;
            // run learning procedure for all samples
            for (int i = 0; i < m_expRowCount; i++)
            {
                //retrieve input and output for specific row
                var input = m_Experiment.GetNormalizedInput(i);
                var output = m_Experiment.GetRowFromOutput(i);

                // compute the network's output
                var norOut = m_Network.CalculateOutputs(input);

                //denormalize output
                var outVal = m_Experiment.GetDenormalizedOutputRow(norOut);
                for (int j = 0; j < outputCount; j++)
                    model[j][i] = outVal[j];

                //calculate learning error
                for (int j = 0; j < outputCount; j++)
                    m_ExpectedValue += Math.Abs((float)(outVal[j] - output[j]));

            }

            //prediction if exist
            double[][] prediction = null;
            if (m_Experiment.IsTestDataExist())
            {
                int testCount = m_Experiment.GetRowCount(true);

                if (testCount > 0)
                {
                    prediction = new double[outputCount][];

                    for (int j = 0; j < outputCount; j++)
                        prediction[j] = new double[testCount];

                    for (int i = 0; i < testCount; i++)
                    {
                        //retrieve input and output for specific row
                        var input = m_Experiment.GetNormalizedInput(i, true);

                        // compute the network's output
                        var norOut = m_Network.CalculateOutputs(input);

                        //denormalize output
                        var outVal = m_Experiment.GetDenormalizedOutputRow(norOut);
                        for (int j = 0; j < outputCount; j++)
                            prediction[j][i] = outVal[j];
                    }
                }
            }



            //Send report for iteration
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = state,
                LearningError = m_ExpectedValue,
                CurrentIteration = m_IterationCounter,
                LearnOutput = model,
                PredicOutput = prediction
            };

            ReportProgress(rp);
            //return average error
            return 0;
        }


        public override double[] CalculateModelForExport(bool isTest)
        {
            //prepare training result
            int outputCount = m_Experiment.GetColumnOutputCount();
            var rowCount = m_Experiment.GetRowCount(isTest);
            double[] model = new double[rowCount];
            
            if (!isTest)
            {
                // run learning procedure for all samples
                for (int i = 0; i < m_expRowCount; i++)
                {
                    //retrieve input and output for specific row
                    var input = m_Experiment.GetNormalizedInput(i);
                    var output = m_Experiment.GetRowFromOutput(i);

                    // compute the network's output
                    var norOut = m_Network.CalculateOutputs(input);

                    //denormalize output
                    var outVal = m_Experiment.GetDenormalizedOutputRow(norOut);
                    model[i] = outVal[0];
                }
                return model;
            }
            else
            {
                if (m_Experiment.IsTestDataExist())
                {
                    int testCount = m_Experiment.GetRowCount(true);

                    if (testCount > 0)
                    {
                        for (int i = 0; i < testCount; i++)
                        {
                            //retrieve input and output for specific row
                            var input = m_Experiment.GetNormalizedInput(i, true);

                            // compute the network's output
                            var norOut = m_Network.CalculateOutputs(input);

                            //denormalize output
                            var outVal = m_Experiment.GetDenormalizedOutputRow(norOut);
                            model[i] = outVal[0];
                        }
                    }
                }

                //return average error
                return model;
            }

            
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
            str += m_Network.WeightsToString();
           // str += m_psoAlgorithm.SaveAlgoritm();
            return str;
        }

    }
}
