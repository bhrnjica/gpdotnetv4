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
using GPdotNET.Core.Experiment;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Main class for creating and running program.
    /// </summary>
    public class GPFactory
    {
        public  event EvolutionHandler ReportEvolution;

        private bool IsAlgorthmPrepared;
        public  static bool StopIteration{get;set;}
        private int m_IterationCounter;
        

        private CHPopulation Population;
        public Experiment m_Experiment;
        public GPFactory()
        {
            Population = new CHPopulation(); 
        }



        /// <summary>
        /// Before we start GP prepare all neccessery information
        /// </summary>
        /// <param name="termSet"></param>
        /// <param name="funSet"></param>
        /// <param name="gpParams"></param>
        public void PrepareAlgorithm(GPTerminalSet termSet,GPFunctionSet funSet,GPParameters gpParams=null)
        {
            m_IterationCounter = 0;
            if(Population ==null)
                Population = new CHPopulation();

            Population.InitPopulation(termSet, funSet, gpParams);
            Population.CalculatePopulation();
            
            IsAlgorthmPrepared = true;

            StopIteration = false;


            //calculate model and prediction
            GPChromosome ch = Population.bestChromosome as GPChromosome;
            double[][] model = CalculateModel(ch);
            double[][] prediction = CalculateModel(ch, false);
            
            //Report the evolution has been started
            if (ReportEvolution != null)
                ReportEvolution(this,
                        new ProgressIndicatorEventArgs()
                        {
                            ReportType = ProgramState.Started,
                            AverageFitness = Population.fitnessAvg,
                            BestChromosome = Population.bestChromosome,
                            CurrentIteration = 0,LearnOutput= model,PredicOutput= prediction
                        });
        }

        public double[][] CalculateTrainModel(GPChromosome ch)
        {
            return CalculateModel(ch, true);
        }

        public double[][] CalculateTestModel(GPChromosome ch)
        {
            return CalculateModel(ch, false);
        }

        /// <summary>
        /// Calculates model/prediction using best chromosome. Also it denormalized valuse if they  were normalized
        /// </summary>
        /// <param name="ch"> bestchromosomes</param>
        /// <param name="testData"></param>
        /// <returns></returns>
        protected virtual double[][] CalculateModel(GPChromosome ch, bool btrainingData = true)
        {
            if (ch != null)
            {
                var pts = GPdotNET.Core.Globals.CalculateGPModel(ch.expressionTree, btrainingData);
                if (pts == null)
                    return null;

                //calculate output
                var model = new double[1][];
                var outv = new double[1][];
                if (m_Experiment != null)
                {
                    outv[0] = new double[1];
                    model[0] = new double[pts.Length];
                    for (int i = 0; i < pts.Length; i++)
                    {
                        outv[0][0] = pts[i];
                        var outv1 = m_Experiment.GetDenormalizedOutputRow(outv[0]);
                        model[0][i] = outv1[0];
                    }
                }
                else
                    model[0] = pts;//old version

                return model;
            }
            else
                return null;
        }

        /// <summary>
        /// Main function for running GP
        /// </summary>
        /// <param name="terValue">termination value</param>
        /// <param name="termType">type of termination</param>
        public void StartEvolution(float terValue, int termType)
        {
            if(!IsAlgorthmPrepared)
                throw new Exception("Algorithm is not prepared!");

            if (Population == null )
                throw new Exception("Population is null!");

            //before we start set variable to initial value
            StopIteration = false;


            while (CanContinue(terValue,termType))
            {
                //increase evolution
                m_IterationCounter++;

                Population.Crossover();

                Population.Mutate();

                Population.EvaluatePopulation();

                Population.Selection();

                Population.CalculatePopulation();

                //calculate model and prediction
                GPChromosome ch = Population.bestChromosome as GPChromosome;
                double[][] model = CalculateModel(ch);
                double[][] prediction = CalculateModel(ch, false);


                if (ReportEvolution != null)
                    ReportEvolution(this, 
                            new ProgressIndicatorEventArgs() 
                                {
                                    ReportType = CanContinue(terValue, termType) ? ProgramState.Running : ProgramState.Finished,
                                    AverageFitness=Population.fitnessAvg,
                                    BestChromosome= Population.bestChromosome,
                                    CurrentIteration=m_IterationCounter,
                                    LearnOutput = model,
                                    PredicOutput=prediction
                                });
            }            
        }

        /// <summary>
        /// Method for chacking several condition due to continualtion of the alogoritham
        /// </summary>
        /// <param name="terValue">termination value</param>
        /// <param name="termType">type of termination</param>
        /// <returns></returns>
        private bool CanContinue(float terValue, int termType)
        {
            //First condition is if the current best fitness is equal to maximum fitness
            if (Population.bestChromosome==null || Population.bestChromosome.Fitness == 1000.0f)
                return false;

            if (termType == 0)
            {
                if (!StopIteration && !(terValue <= m_IterationCounter))
                    return true;
                else
                    return false;
            }
            else
            {
                if (Population.bestChromosome == null)
                    return true;
                if (!StopIteration && !(terValue <= Population.bestChromosome.Fitness))
                    return true;
                else
                    return false;
            }
        }

        public GPParameters GetParameters()
        {
            if (Population == null)
                return null;
            return Population.GetParameters();
        }

        public GPTerminalSet GetTerminalSet()
        {
            if (Population == null)
                return null;
            return Population.GetTerminalSet();
        }

        public GPFunctionSet GetFunctionSet()
        {
            if (Population == null)
                return null;
            return (GPFunctionSet)Population.GetFunctionSet();
        }

        public CHPopulation GetPopulation()
        {
            return Population;
        }

        public List<IChromosome> GetChromosomes()
        {
            if (Population == null)
                return null;
            return Population.GetChromosomes();
        }

        public int GetpopSize()
        {
            if (Population == null)
                return 0;
            return Population.GetpopSize();
        }

        public IChromosome BestChromosome()
        {
            if (Population == null)
                return null;
            return Population.bestChromosome;
        }

        public void SetBestChromosome(IChromosome ch)
        {
            Population.bestChromosome=ch;
        }

        public void SetChromosomes(List<IChromosome> chs)
        {
            Population.chromosomes = chs;
        }

        public void SetTerminalSet(GPTerminalSet gpTSet)
        {
            Population.SetTerminalSet(gpTSet);
        }

        public void SetFunctionSet(GPFunctionSet gpFSet)
        {
            Population.SetFunctionSet(gpFSet);
        }

        public void SetGPParameter(GPParameters gpp)
        {
            Population.SetParameters(gpp);
        }

        public float GetAverageFitness()
        {
            if (Population == null)
                return 0;
           return Population.fitnessAvg;
        }

        public void CalculatePopulation()
        {
            if (Population == null)
                return;
            Population.CalculatePopulation();
        }

        public void ResetSolution()
        {
            Population.bestChromosome= null;
            Population.chromosomes.Clear();
        }

        public void SetCurrentIteration(int currIter)
        {
            m_IterationCounter = currIter;
        }
    }
}
