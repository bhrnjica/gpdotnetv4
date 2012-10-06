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
using GPdotNET.Core;
using System.Linq;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Main class which implement general workflow of GA and GP. It manipulates with chromosomes, evaluates them, calculates stats, select fittes, etc..
    /// </summary>
    public class CHPopulation
    {
        #region Fields
        
        internal List<IChromosome> chromosomes;
        internal IChromosome bestChromosome;

        private IFitnessFunction fitnessFunction;
        private GPParameters gpParameters;
        private IFunctionSet functionSet;
  
        public float fitnessAvg{get;set;}
        private float fitnessMax;
       // private float fitnessMin;
        private float fitnessSum;
        #endregion

        #region ctor
        public CHPopulation()
        {
            chromosomes = new List<IChromosome>();
        }
        #endregion

        #region Initialization Methods

        /// <summary>
        /// Initialisation of chromosomes in Genetic programming
        /// </summary>
        /// <param name="size">Size of the chromosomes, default value is 1000</param>
        /// <param name="initMethod">Initializastion method, default HalfandHalf</param>
        public void InitPopulation(
            GPTerminalSet termSet,
            IFunctionSet funSet,
            GPParameters gpParams=null
            )
        {
            
            if (gpParams == null)
                gpParameters = new GPParameters();
            else
                gpParameters = gpParams;

            if (gpParameters.algorithmType == Algorithm.GA)
                GANumChromosome.functionSet = funSet;

            //
            GPChromosome.MaxOperationLevel = gpParameters.maxOperationLevel;
            

            fitnessFunction = gpParameters.GPFitness;


            if (funSet == null)
                throw new Exception("FunctionSet is not defined, and cannot be null!");
            else
                {
                    functionSet = funSet;
                    Globals.functions = functionSet;
                }

            if (termSet == null)
                throw new Exception("TerminalSet is not defined, and cannot be null!");
            else
                Globals.gpterminals = termSet;

            GeneratePopulation(gpParameters.popSize);
        }

        private void GeneratePopulation(int popSize)
        {
            if (gpParameters.algorithmType == Algorithm.GA)
               GAInitialization(popSize);
            else
                switch (gpParameters.einitializationMethod)
                {
                    case GPInitializationMethod.FullInitialization:
                        FullInitialization(popSize);
                        break;
                    case GPInitializationMethod.GrowInitialization:
                        GrowInitialization(popSize);
                        break;
                    case GPInitializationMethod.HalfHalfInitialization:
                        HalfHalfInitialization(popSize);
                        break;
                    default:
                        HalfHalfInitialization(popSize);
                        break;
                }

            //After initialization wee need evaluate each chromosoe
            EvaluatePopulation();
        }

        /// <summary>
        /// Full initialization method, all choromosomes have the same level.
        /// </summary>
        /// <param name="size"></param>
        private void GAInitialization(int size)
        {
            //Chromosome generation with exact level 
            for (int i = 0; i < size; i++)
            {
                IChromosome c;
                // generate new chromosome
                if(gpParameters.chromosomeKind== GAChromosome.Continue)
                    c = GenerateGANumChromosome();
                else
                    c = GenerateGABinChromosome();

                //add to poppulation
                chromosomes.Add(c);
            }
        }

        /// <summary>
        /// Full initialization method, all choromosomes have the same level.
        /// </summary>
        /// <param name="size"></param>
        private void FullInitialization(int size)
        {
            //Chromosome generation with exact level 
            for (int i = 0; i < size; i++)
            {
                // generate new chromosome
                IChromosome c = GenerateGPChromosome(GetMaxInitializeLevel());

                //add to poppulation
                chromosomes.Add(c);
            }
        }
        /// <summary>
        /// Every chromosome have randomly generated  levels between 2 and maximumValue.
        /// </summary>
        /// <param name="size"></param>
        private void GrowInitialization(int size)
        {
            int levels = 2;
            //Chromosome generation with exact level 
            for (int i = 0; i < size; i++)
            {
                //Randomly choose which level chromose will have
                levels = Globals.radn.Next(2, GetMaxInitializeLevel() + 1);

                // generate new chromosome
                IChromosome c = GenerateGPChromosome(levels);

                // pridruzivanje populaciji
                chromosomes.Add(c);
            }
        }
        /// <summary>
        /// HalfAndHalf method of initialization,all chromosomes is grouped in equal number of chromosome
        /// and generates with the same levels.
        /// </summary>
        /// <param name="size"></param>
        private void HalfHalfInitialization(int size)
        {
            //The max init depth of tree
            int n = GetMaxInitializeLevel();

            //Make equal group for each level
            int br = (size / n);

            //Create equal number of choromosomes for each level 
            for (int i = 2; i <= n; i++)
            {
                if (i == n)//at the end take the rest 
                    br = (size - ((i - 2) * br));
                //Chromosome generation with exact level 
                for (int j = 0; j < br; j++)
                {
                    // create new chromosome
                    IChromosome c = GenerateGPChromosome(i);

                    // add to chromosomes
                    chromosomes.Add(c);
                }
            }
        }
        #endregion

        #region Chromosome generation

        /// <summary>
        /// Randomly generate chromosome with exact number of levels
        /// </summary>
        /// <param name="levels">Tree leveles</param>
        /// <returns></returns>
        public GPChromosome GenerateGPChromosome(int levels)
        {
           
            GPChromosome ch = GPChromosome.NewChromosome();
            ch.expressionTree = GPChromosome.GenerateTreeStructure(levels);
            return ch;

        }

        private IChromosome GenerateGABinChromosome()
        {
            var ch = GABinChromosome.NewChromosome();
            ch.Generate();
            return ch;
        }

        private IChromosome GenerateGANumChromosome()
        {
            var ch = GANumChromosome.NewChromosome();
            ch.Generate();
            return ch;
        }
        
        #endregion

        #region Genetic Operators

        public void Crossover()
        {

            //if mutation is definded
            if (gpParameters.probCrossover == 0)
                return;
            for (int i = 1; i < gpParameters.popSize; i += 2)
            {
               
                if (Globals.radn.NextDouble() <= gpParameters.probCrossover)
                {
                    int k = Globals.radn.Next(0, gpParameters.popSize);
                    int l = Globals.radn.Next(0, gpParameters.popSize);

                    // cloning the chromosome and prepare for crossover
                    var ch1 = chromosomes[k].Clone();  
                    var ch2 = chromosomes[l].Clone();
                 
                    // crossover
                    ch1.Crossover(ch2);

                    //reset fitness
                    ch1.Fitness = float.MinValue;
                    ch2.Fitness = float.MinValue;

                    //add new chromosomes to chromosomes
                    chromosomes.Add(ch1);
                    chromosomes.Add(ch2);
                }
            }

            
            
        }

        public void Mutate()
        {
            //if mutation is definded
            if (gpParameters.probMutation == 0)
                return;
            for (int i = 0; i < gpParameters.popSize; i++)
            {
                // 
                if (Globals.radn.NextDouble() <= gpParameters.probMutation)
                {
                    int k = Globals.radn.Next(0, gpParameters.popSize);
                    var ch1 = chromosomes[k].Clone();
                   
                    ch1.Mutate();
                    ch1.Fitness = float.MinValue;
                    chromosomes.Add(ch1);

                }
            }
    
        }
        #endregion

        #region Evaluation Population
        public void EvaluatePopulation()
        {
            //Use parallel only with GP 
            if (gpParameters.bParalelGP && gpParameters.algorithmType==Algorithm.GP)
            {
                int count= chromosomes.Count;
                System.Threading.Tasks.Parallel.For(0, count, (i) =>
                    {
                        if (chromosomes[i].Fitness == float.MinValue)
                            chromosomes[i].Evaluate(fitnessFunction);
                    }
                    );
            }
            else
            {
                for (int i = 0; i < chromosomes.Count; i++)
                {
                    if (chromosomes[i].Fitness == float.MinValue)
                        chromosomes[i].Evaluate(fitnessFunction); 
                }
            }
        }
        internal void CalculatePopulation()
        {
            // calculate basic stat of the population
            fitnessMax = float.MinValue;
            fitnessSum = 0;

            if (chromosomes != null && chromosomes.Count>0)
                bestChromosome = chromosomes[0];
            else
                return;

           int counter = 0;
           for (int i = 0; i < chromosomes.Count; i++)
             {
                 if (float.IsNaN(chromosomes[i].Fitness))
                     continue;
                 counter++;
                float fitness = chromosomes[i].Fitness;
                // sum calc
                fitnessSum += fitness;

                // cal the best chromosome
                if (fitness > fitnessMax)
                 {
                    fitnessMax = fitness;
                    bestChromosome = chromosomes[i];
                 }
              }

            fitnessAvg =(float) Math.Round(fitnessSum / counter,4);
        }
        #endregion

        #region Various Getter Helpers methods

        

        /// <summary>
        /// Return maximum levlel shoromosome can has after genetic operation 
        /// </summary>
        /// <returns></returns>
        public int GetMaxOperationLevel()
        {
            
            return gpParameters.maxOperationLevel;
        }

        /// <summary>
        /// Returns maximum level of expression tree which can be created during initialization.
        /// This is ordinary GP parameter user sets.
        /// </summary>
        /// <returns>Maximum lelev</returns>
        public int GetMaxInitializeLevel()
        {
            return gpParameters.maxInitLevel;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fFun"></param>
        public void SetFitnessFunction(IFitnessFunction fFun)
        {
            fitnessFunction = fFun;
           
        }
        #endregion

        #region Selection Methods
        /// <summary>
        /// Vršenje selekcije u populaciji 
        /// </summary>
        public void Selection()
        {
            // na osnovu vjerojatnosti selekcije izračunamo broj hromosoma koji prezivljavaju 
            // u narednu generaciju npr ako je 5% vjerojatnost Selekcije tada ako je 100 velicina populacije 
            // tada 5 hromosoma prelazi u novu generaciju
            int repNumber = (int)(gpParameters.probReproduction * gpParameters.popSize);

            //remove all bad chromosomes
            chromosomes.RemoveAll(ch =>
                {
                    if (float.IsNaN(ch.Fitness))
                    {
                        ch.Destroy();
                        return true;
                    }
                    else
                        return false;
                }

                );
           
            //sort the chromosomes
            chromosomes.Sort();

            //Elitism number of very best chromosome to survive to new generation
            List<IChromosome> elist = null;
            if (gpParameters.elitism > 0)
            {
                elist = new List<IChromosome>();
                for (int i = 0; i < gpParameters.elitism; i++)
                    elist.Add(chromosomes[i].Clone());
            }

            // vrsenje selekcije odredjenom metodom
            int numb = gpParameters.popSize - repNumber - gpParameters.elitism;
            switch (gpParameters.eselectionMethod)
            {
                case GPSelectionMethod.FitnessProportionateSelection:
                    FitnessProportionateSelection(numb);
                    break;
                case GPSelectionMethod.Rankselection:
                    RankSelection(numb);
                    break;
                case GPSelectionMethod.TournamentSelection:
                    TournamentSelection(numb);
                    break;
                case GPSelectionMethod.StochasticUniversalSelection:
                    UniversalStohasticSelection(numb);
                    break;
                case GPSelectionMethod.FUSSelection:
                    FUSSSelection(numb);
                    break;
                case GPSelectionMethod.SkrgicSelection:
                    SkrgicSelection(numb);
                    break;
                default:
                    FitnessProportionateSelection(numb);
                    break;
            }


            if (gpParameters.elitism > 0)
                chromosomes.AddRange(elist);
            
            //SOmtimes there is no good chromosomes so we need to generate a new
            if (chromosomes.Count< gpParameters.popSize)
                GeneratePopulation(gpParameters.popSize - chromosomes.Count);

        }
        /// <summary>
        /// Fitness šproportionate selection.
        /// </summary>
        /// <param name="size"></param>
        private void FitnessProportionateSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();


            int currentSize = chromosomes.Count;
            double sumOfFitness = 0;

            //calculate sum of fitness
            for (int i = 0; i < currentSize; i++)
                sumOfFitness += chromosomes[i].Fitness;
            

            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;

            for (int i = 0; i < currentSize; i++)
            {
                // cumulative normalized fitness
                s += (chromosomes[i].Fitness / sumOfFitness);
                rangeMax[i] = s;
            }

            // select chromosomes from old chromosomes to the new chromosomes
            for (int j = 0; j < size; j++)
            {
                // get wheel value
                double wheelValue = Globals.radn.NextDouble();
                // find the chromosome for the wheel value
                for (int i = 0; i < currentSize; i++)
                {
                    //double wheelValue = rand.NextDouble();
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new population
                        var ch = chromosomes[i].Clone();
                        newPopulation.Add(ch);
                        chromosomes[i].Fitness = float.MinValue;

                        break;
                    }
                }
            }

           
            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }
        private void RankSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            // velicinaPopulacije of current chromosomes
            int currentSize = chromosomes.Count;

            // sort current chromosomes
            //chromosomes.Sort();

            // calculate amount of ranges in the wheel
            double ranges = currentSize * (currentSize + 1) / 2;

            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;

            for (int i = 0, n = currentSize; i < currentSize; i++, n--)
            {
                s += ((double)n / ranges);
                rangeMax[i] = s;
            }

            // select chromosomes from old chromosomes to the new chromosomes
            for (int j = 0; j < size; j++)
            {
                // get wheel value
                double wheelValue = Globals.radn.NextDouble();
                // find the chromosome for the wheel value
                for (int i = 0; i < currentSize; i++)
                {
                    // get wheel value
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new chromosomes
                        newPopulation.Add(chromosomes[i].Clone());
                        break;
                    }
                    //Debug.Assert(false);
                }
            }

            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }
        private void TournamentSelection(int size)
        {
            // velicinaPopulacije of current chromosomes
            int currentSize = chromosomes.Count;
            List<IChromosome> tourn = new List<IChromosome>((int)gpParameters.SelParam1);
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();

            for (int j = 0; j < size; j++)
            {
                currentSize = chromosomes.Count;
                for (int i = 0; i < gpParameters.SelParam1 && i < currentSize; i++)
                {
                    int ind = Globals.radn.Next(currentSize);
                    tourn.Add(chromosomes[ind]);

                }

                tourn.Sort();
                newPopulation.Add(tourn[0].Clone());
                chromosomes.Remove(tourn[0]);
                tourn.Clear();
            }

            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }
        private void UniversalStohasticSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            //chromosomes.Sort();

            int currentSize = chromosomes.Count();
            float fitnessSum = chromosomes.Sum(c => c.Fitness);

            // get random distance value
            float randDist = (float)Globals.radn.NextDouble(0, 1.0 / (double)size);
            float partFitnes = 0;
            for (int j = 0; j < size; j++)
            {
                partFitnes = 0;
                for (int i = 0; i < chromosomes.Count; i++)
                {
                    partFitnes += chromosomes[i].Fitness / fitnessSum;

                    if (randDist <= partFitnes)
                    {
                        newPopulation.Add(chromosomes[i].Clone());
                        break;
                    }
                }
                randDist += 1.0F / (float)size;
            }

            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }
        private void FUSSSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            //Maximum fitness
            double fitnessMax = chromosomes.Max(x => x.Fitness);
            double rnd;
            double dif;
            int selIndex = 0;

            for (int j = 0; j < size; j++)
            {
                rnd = Globals.radn.NextDouble(0, fitnessMax);
                dif = Math.Abs(chromosomes[0].Fitness - rnd);
                selIndex = 0;
                for (int i = 1; i < chromosomes.Count; i++)
                {
                    double curDif = Math.Abs(chromosomes[i].Fitness - rnd);
                    if (dif > curDif)
                    {
                        dif = curDif;
                        selIndex = i;
                    }
                }
                newPopulation.Add(chromosomes[selIndex].Clone());
            }

            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }
        private void SkrgicSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            //  double k = 0.2; //additionalParameter;
            double fitnessMax = chromosomes.Max(x => x.Fitness) * (1.0 + gpParameters.SelParam1);
            for (int i = 0; i < size; i++)
            {
                //Slucajni index iz populacije
                int randomIndex = Globals.radn.Next(0, chromosomes.Count);
                //Slucajni broj izmedju 0-maxFitnes ukljucujuci i maxFitness koje je prethodno vec izracunat kod evaluacije hromosoma
                double randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);

                while (true)
                {

                    //Akoje slucajno generirani broj manji ili jednak fitnesu slucajnog hromosoma selektuj hromosom
                    if (randomFitness <= chromosomes[randomIndex].Fitness * (1.0 + gpParameters.SelParam1 / fitnessMax))
                    {
                        newPopulation.Add(chromosomes[randomIndex].Clone());
                        break;
                    }

                    randomIndex = Globals.radn.Next(0, chromosomes.Count);
                    randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);
                }
            }

            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }
        #endregion

        internal List<IChromosome> GetChromosomes()
        {
            return chromosomes;
        }

        internal int GetpopSize()
        {
            if (chromosomes != null)
                return chromosomes.Count;
            else
                return 0;
        }

        internal IFunctionSet GetFunctionSet()
        {
            return Globals.functions;
        }

        internal GPTerminalSet GetTerminalSet()
        {
          
            return Globals.gpterminals;
        }

        internal GPParameters GetParameters()
        {
            return gpParameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fSet"></param>
        public void SetFunctionSet(IFunctionSet fSet)
        {
            functionSet = fSet;
            Globals.functions = fSet;
        }

        internal void SetTerminalSet(GPTerminalSet tSet)
        {

            Globals.gpterminals=tSet;
        }

        internal void SetParameters(GPParameters gpp)
        {
            gpParameters = gpp;
        }
    }
}
