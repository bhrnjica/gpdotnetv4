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
using GPdotNET.Engine.ANN;
using GPdotNET.Core.Experiment;
using System.Globalization;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Main interface for creating and running program.
    /// </summary>
    public abstract class AIFactory
    {
        //event for reporting the progress of  iteration
        public event EvolutionHandler ReportIteration;

        protected bool IsAlgorthmPrepared;

        //Controlling Iteration process
        public  static bool StopIteration{get;set;}
        protected int m_IterationCounter;//iteration counter
        protected float m_ExpectedValue;//represent learning error in ANN, and fitness value in GP/GA

        protected NeuralNetwork m_Network;
        protected Experiment m_Experiment;
        protected ANNParameters m_Parameters;

        public virtual string SaveFactory()
        {
            var str = m_ExpectedValue.ToString(CultureInfo.InvariantCulture) +";";
            str += m_Network.WeightsToString();
            return str;
        }

        public virtual int LoadFactory(string strWeights)
        {
            var wi = strWeights.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            m_ExpectedValue = (float)double.Parse(wi[0], CultureInfo.InvariantCulture);
            var retVal = m_Network.WeightsFromString(wi.Skip(1).ToArray());
            return retVal;
        }

        public AIFactory()
        {
            m_ExpectedValue = -1;
        }

        protected void ReportProgress(ProgressIndicatorEventArgs rp)
        {
            //Report the iteration is ready to start
            if (ReportIteration != null)
                ReportIteration(this, rp);
        }

        protected abstract void FinishIteration();

        internal abstract float RunIteration();

        public abstract float CalculateModel(ProgramState state = ProgramState.Running);

        public abstract double[] CalculateModelForExport(bool isTest);

        public abstract string GenerateFormula();

        public abstract void PrepareAlgorithm(Experiment expData, ANNParameters annParams = null);

        public void PrepareAlgorithm(Experiment expData, NeuralNetwork ann)
        {
            if (ann == null || expData == null)
                throw new Exception("Argument value cannot be null");

            //preparing the variables
            m_Network = ann;
            m_Experiment = expData;
            m_Parameters = m_Network.Parameters;
            m_IterationCounter = 0;

            IsAlgorthmPrepared = true;
            StopIteration = false;
            
            //report 
            var rp = new ProgressIndicatorEventArgs()
            {
                ReportType = ProgramState.Started,
                LearningError = float.MaxValue,
                CurrentIteration = 0,
                LearnOutput = null,

            };
            ReportProgress(rp);
        }
        
        private bool CanContinue(float terValue, int termType)
        {
            if (StopIteration)
                return false;

            else if (termType == 0)
            {
                if (!(terValue <= m_IterationCounter))
                    return true;
                else
                    return false;
            }
            else if (termType == 1)
            {
                if (!(terValue >= m_ExpectedValue))
                    return true;
                else
                    return false;
            }
            else
                return true;
        }


        public void StartIteration(float terValue, int termType)
        {
            if (!IsAlgorthmPrepared)
                throw new Exception("Algorithm is not prepared!");

            //iterate until one of terminations criteria are met
            while (true)
            {
                //increase evolution
                m_IterationCounter++;

                RunIteration();

                if (!CanContinue(terValue, termType))
                {
                    FinishIteration();
                    break;
                }
            }
        }


        public void SetExperiment(Experiment exp)
        {
            m_Experiment = exp;
        }

        public void SetANNParameters(ANNParameters annp)
        {
            m_Parameters = annp;
        }

        /// <summary>
        /// Resets factory to initial state
        /// </summary>
        public void ResetFactory()
        {
            m_Network = null;
            m_IterationCounter = 0;
            m_Parameters = null;
        }

        public void SetCurrentIteration(int currIter)
        {
            m_IterationCounter = currIter;
        }
    }
}
