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
       // private GPParameters gpParameters;
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
                Globals.gpparameters = new GPParameters();
            else
                Globals.gpparameters = gpParams;

            if (Globals.gpparameters.algorithmType == Algorithm.GA)
            {
                if (Globals.gpparameters.chromosomeKind == GAChromosome.Continue)
                    GANumChromosome.functionSet = funSet;
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.TSP)
                    GAVChromosome.functionSet = funSet;
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.ALOC)
                    GAVChromosome.functionSet = funSet;
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.TP)
                {
                    GAMChromosome.terminalSet = termSet;
                    GAMChromosome.functionSet = funSet;
                }
                else
                    GABinChromosome.functionSet = funSet;
            }
            else
            GPChromosome.MaxOperationLevel = Globals.gpparameters.maxOperationLevel;

            //init default fitness type
            if (Globals.gpparameters.GPFitness == null)
                Globals.gpparameters.GPFitness = new RMSEFitness();

            fitnessFunction = Globals.gpparameters.GPFitness;


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

            var siz = Globals.gpparameters.popSize - chromosomes.Count;
            if(siz>0)
                GeneratePopulation(siz);
        }

        private void GeneratePopulation(int popSize)
        {
            if (Globals.gpparameters.algorithmType == Algorithm.GA)
               GAInitialization(popSize);
            else
                switch (Globals.gpparameters.einitializationMethod)
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
                if(Globals.gpparameters.chromosomeKind== GAChromosome.Continue)
                    c = GenerateGANumChromosome();
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.Binary)
                    c = GenerateGABinChromosome();
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.TSP)
                    c = GenerateGATSPChromosome();
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.ALOC)
                    c = GenerateGAAlocChromosome();
                else if (Globals.gpparameters.chromosomeKind == GAChromosome.TP)
                    c = GenerateGAMatrixChromosome();
                else
                    c = GenerateGATSPChromosome();
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
            ch.Generate(levels);
            return ch;

        }

        private IChromosome GenerateGABinChromosome()
        {
            var ch = GABinChromosome.NewChromosome();
            ch.Generate();
            return ch;
        }

        private IChromosome GenerateGATSPChromosome()
        {
            var ch = GAVChromosome.NewChromosome();
            ch.Generate();
            
            return ch;
        }
        private IChromosome GenerateGAAlocChromosome()
        {
            var ch = GAVChromosome.NewChromosome();
            ch.Generate();
            return ch;
        }
        private IChromosome GenerateGANumChromosome()
        {
            var ch = GANumChromosome.NewChromosome();
            ch.Generate();
            return ch;
        }
        private IChromosome GenerateGAMatrixChromosome()
        {
            
            var ch = GAMChromosome.NewChromosome();
            ch.Generate();
            return ch;
        }
        #endregion

        #region Genetic Operators

        public void Crossover()
        {

            //if mutation is definded
            if (Globals.gpparameters.probCrossover == 0)
                return;
            for (int i = 1; i < Globals.gpparameters.popSize; i += 2)
            {
               
                if (Globals.radn.NextDouble() <= Globals.gpparameters.probCrossover)
                {
                    int k = Globals.radn.Next(0, Globals.gpparameters.popSize);
                    int l = Globals.radn.Next(0, Globals.gpparameters.popSize);

                    int counter = 1;
                    //brood size rrecombination
                    while(counter<= Globals.gpparameters.broodSize)
                    {
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

                        //
                        counter++;
                    }
                    
                }
            }

            
            
        }

        public void Mutate()
        {
            //if mutation is definded
            if (Globals.gpparameters.probMutation == 0)
                return;
            for (int i = 0; i < Globals.gpparameters.popSize; i++)
            {
                // 
                if (Globals.radn.NextDouble() <= Globals.gpparameters.probMutation)
                {
                    int k = Globals.radn.Next(0, Globals.gpparameters.popSize);
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
            if (Globals.gpparameters.bParalelGP && Globals.gpparameters.algorithmType==Algorithm.GP)
            {
                int count = chromosomes.Count;
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
            
            return Globals.gpparameters.maxOperationLevel;
        }

        /// <summary>
        /// Returns maximum level of expression tree which can be created during initialization.
        /// This is ordinary GP parameter user sets.
        /// </summary>
        /// <returns>Maximum lelev</returns>
        public int GetMaxInitializeLevel()
        {
            return Globals.gpparameters.maxInitLevel;
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
            int repNumber = (int)(Globals.gpparameters.probReproduction * Globals.gpparameters.popSize);

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
            if (Globals.gpparameters.elitism > 0)
            {
                elist = new List<IChromosome>();
                for (int i = 0; i < Globals.gpparameters.elitism; i++)
                    elist.Add(chromosomes[i].Clone());
            }

            // vrsenje selekcije odredjenom metodom
            int numb = Globals.gpparameters.popSize - repNumber - Globals.gpparameters.elitism;
            switch (Globals.gpparameters.eselectionMethod)
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


            if (Globals.gpparameters.elitism > 0)
                chromosomes.AddRange(elist);
            
            //SOmtimes there is no good chromosomes so we need to generate a new
            if (chromosomes.Count< Globals.gpparameters.popSize)
                GeneratePopulation(Globals.gpparameters.popSize - chromosomes.Count);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        private void RankSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            // velicinaPopulacije of current chromosomes
            int currentSize = chromosomes.Count;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        private void TournamentSelection(int size)
        {
            // velicinaPopulacije of current chromosomes
            int currentSize = chromosomes.Count;
            List<IChromosome> tourn = new List<IChromosome>((int)Globals.gpparameters.SelParam1);
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();

            for (int j = 0; j < size; j++)
            {
                currentSize = chromosomes.Count;
                for (int i = 0; i < Globals.gpparameters.SelParam1 && i < currentSize; i++)
                {
                    int ind = Globals.radn.Next(currentSize);
                    tourn.Add(chromosomes[ind]);

                }

                if (tourn.Count > 0)
                {
                    tourn.Sort();
                    newPopulation.Add(tourn[0].Clone());
                    chromosomes.Remove(tourn[0]);
                    tourn.Clear();
                }
            }

            // old population is going to die
            for (int i = 0; i < chromosomes.Count; i++)
                chromosomes[i].Destroy();
            chromosomes.Clear();

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        private void UniversalStohasticSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            //chromosomes.Sort();

            //int currentSize = chromosomes.Count();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        private void SkrgicSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();
            //  double k = 0.2; //additionalParameter;
            double fitnessMax = chromosomes.Max(x => x.Fitness) * (1.0 + Globals.gpparameters.SelParam1);
            for (int i = 0; i < size; i++)
            {
                //Slucajni index iz populacije
                int randomIndex = Globals.radn.Next(0, chromosomes.Count);
                //Slucajni broj izmedju 0-maxFitnes ukljucujuci i maxFitness koje je prethodno vec izracunat kod evaluacije hromosoma
                double randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);

                while (true)
                {

                    //Akoje slucajno generirani broj manji ili jednak fitnesu slucajnog hromosoma selektuj hromosom
                    if (randomFitness <= chromosomes[randomIndex].Fitness * (1.0 + Globals.gpparameters.SelParam1 / fitnessMax))
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal List<IChromosome> GetChromosomes()
        {
            return chromosomes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal int GetpopSize()
        {
            if (chromosomes != null)
                return chromosomes.Count;
            else
                return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IFunctionSet GetFunctionSet()
        {
            return Globals.functions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal GPTerminalSet GetTerminalSet()
        {
          
            return Globals.gpterminals;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal GPParameters GetParameters()
        {
            return Globals.gpparameters;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tSet"></param>
        internal void SetTerminalSet(GPTerminalSet tSet)
        {

            Globals.gpterminals=tSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpp"></param>
        internal void SetParameters(GPParameters gpp)
        {
            Globals.gpparameters = gpp;
        }
    }
}
