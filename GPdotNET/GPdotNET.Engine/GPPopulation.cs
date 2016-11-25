//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2010 Bahrudin Hrnjica                                                 //
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
    public class GPPopulation
    {
        #region Fields
        public List<GPChromosome> chromosomes;
        public GPChromosome bestChromosome;
        private IFitnessFunction fitnessFunction;
        private GPParameters gpParameters;
        private GPFunctionSet functionSet;
  
        public float fitnessAvg{get;set;}
        private float fitnessMax;
        private float fitnessMin;
        private float fitnessSum;
        #endregion

        #region ctor
        public GPPopulation()
        {
            chromosomes = new List<GPChromosome>();
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
            GPFunctionSet funSet,
            GPParameters gpParams=null
            )
        {
            
            if (gpParams == null)
                gpParameters = new GPParameters();
            else
                gpParameters = gpParams;

            

            fitnessFunction = gpParameters.GPFitness;


            if (funSet == null)
                throw new Exception("FunctionSet is not defined, and cannot be null!");
            {
                functionSet = funSet;
                Globals.functions = functionSet;
            }

            if (funSet == null)
                throw new Exception("TerminalSet is not defined, and cannot be null!");
            Globals.terminals = termSet;

            GeneratePopulation(gpParameters.popSize);
        }

        private void GeneratePopulation(int popSize)
        {
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
        private void FullInitialization(int size)
        {
            //Chromosome generation with exact level 
            for (int i = 0; i < size; i++)
            {
                // generate new chromosome
                GPChromosome c = GenerateChromosome(GetMaxInitializeLevel());

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
                GPChromosome c = GenerateChromosome(levels);

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
                    GPChromosome c = GenerateChromosome(i);

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
        public GPChromosome GenerateChromosome(int levels)
        {
            GPChromosome ch = GPChromosome.NewChromosome();
            ch.expressionTree = GenerateTreeStructure(levels);
            return ch;

        }

        /// <summary>
        /// Generate Tree structure for chromosome representation
        /// </summary>
        /// <param name="levels"></param>
        /// <returns></returns>
        public GPNode GenerateTreeStructure(int levels)
        {
            //Create the first node
            GPNode root = GPNode.NewNode();
            root.value = 0;

            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            GPNode node = null;


            //Add to list and ncrease index
            dataTree.Enqueue(root);

            while (dataTree.Count > 0)
            {

                node = dataTree.Dequeue();
                int level = node.value;

                //Generate Chromosome node value
                if (node.value < levels)
                    node.value = Globals.GenerateNodeValue(true);
                else
                    node.value = Globals.GenerateNodeValue(false); ;

                //Node children generatio  
                if (node.value > 1999)
                {
                    int aritry = functionSet.GetFunctions()[node.value-2000].Aritry;
                    //Create children of node  
                    node.children = new GPNode[aritry];

                    for (int i = 0; i < aritry; i++)
                    {
                        GPNode child = GPNode.NewNode();
                        child.value = level + 1;

                        node.children[i] = child;
                        dataTree.Enqueue(node.children[i]);
                    }

                }
            }

            return root;

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
                    int k = Globals.radn.Next(1, gpParameters.popSize);

                    // cloning the chromosome and prepare for crossover
                    var ch1 = chromosomes[k-1].Clone();
                    ch1.fitness = -1;
                    var ch2 = chromosomes[k].Clone();
                    ch2.fitness = -1;

                    // ukrštanje
                    ApplyCrossover(ch1, ch2);

                    //add new chromosomes to chromosomes
                    chromosomes.Add(ch1);
                    chromosomes.Add(ch2);
                }
            }

            
            
        }

        private void ApplyCrossover(GPChromosome ch1, GPChromosome ch2)
        {
            //Get random numbers between 0 and maximum index
            int index1 = GetRandomNode(ch1.expressionTree.NodeCount());
            int index2 = GetRandomNode(ch2.expressionTree.NodeCount());

            //Exchange the geene material
            Crossover(ch1.expressionTree, ch2.expressionTree, index1, index2);


            //if some tree exceed the levels trim it
            ch1.Trim(GetMaxOperationLevel());
            ch2.Trim(GetMaxOperationLevel());

        }

        public void Crossover(GPNode ch1, GPNode ch2, int index1, int index2)
        {
            //We dont want to crossover root
            if (index1 == 1 || index2 == 1)
                throw new Exception("Wrong index number for Crossover operation!");

            //start counter from 0
            int count = 0;

            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            GPNode node = null;

            //Add tail recursion
            dataTree.Enqueue(ch1);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();

                //count node
                count++;

                //when the counter is equel to index return curretn node
                if (count == index1)
                    break;

                if (node.children != null)
                    for (int i = node.children.Length - 1; i >= 0; i--)
                        dataTree.Enqueue(node.children[i]);

            }

            var node1 = node;


            //Add tail recursion
            count = 0;
            dataTree.Clear();
            dataTree.Enqueue(ch2);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();

                //count node
                count++;

                //when the counter is equel to index return curretn node
                if (count == index2)
                    break;

                if (node.children != null)
                    for (int i = node.children.Length - 1; i >= 0; i--)
                        dataTree.Enqueue(node.children[i]);
            }


            //Exchange  nodes
            GPNode tempNode = GPNode.NewNode();
            tempNode.value = node1.value;
            if (node1.children != null)
            {
                tempNode.children = new GPNode[node1.children.Length];
                for (int i = 0; i < tempNode.children.Length; i++)
                    tempNode.children[i] = node1.children[i];
            }

            //
            node1.value = node.value;
            node1.children = null;
            if (node.children != null)
            {
                node1.children = null;
                node1.children = new GPNode[node.children.Length];
                for (int i = 0; i < node.children.Length; i++)
                {
                    node1.children[i] = node.children[i];

                }
            }
            //
            node.value = tempNode.value;
            node.children = tempNode.children;

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
                    var ch1 = chromosomes[i].Clone();
                    ch1.fitness = -1;
                    ApplyMutation(ch1);
                    chromosomes.Add(ch1);

                }
            }
    
        }

        private void ApplyMutation(GPChromosome ch1)
        {
            int index1 = GetRandomNode(ch1.expressionTree.NodeCount());
            Mutate(ch1.expressionTree, index1, GetMaxOperationLevel());
        }

        public void Mutate(GPNode root, int index1, int maxLevels)
        {
            //We dont want to crossover root
            if (index1 == 1)
                throw new Exception("Wrong index number for Mutate operation!");

            //start counter from 0
            int count = 0;

            //Collection holds tree nodes
            Queue<Tuple<int, GPNode>> dataTree = new Queue<Tuple<int, GPNode>>();
            //current node
            Tuple<int, GPNode> tuplenode = null;

            //Add tail recursion
            dataTree.Enqueue(new Tuple<int, GPNode>(0, root));

            while (dataTree.Count > 0)
            {
                //get next node
                tuplenode = dataTree.Dequeue();

                //count node
                count++;

                //perform mutation
                if (count == index1)
                {
                    int level = maxLevels - tuplenode.Item1;
                    if (level < 0)
                        throw new Exception("Level is not a correct number!");
                    int rnd = Globals.radn.Next(level + 1);
                    if (level <= 1 || rnd == 0)
                    {
                        tuplenode.Item2.value = Globals.GenerateNodeValue(false);
                        GPNode.DestroyNodes(tuplenode.Item2.children);
                        tuplenode.Item2.children = null;
                    }
                    else
                    {
                        var node = GenerateTreeStructure(rnd);
                        tuplenode.Item2.value = node.value;
                        tuplenode.Item2.children = node.children;
                    }
                    break;
                }

                if (tuplenode.Item2.children != null)
                    for (int i = tuplenode.Item2.children.Length - 1; i >= 0; i--)
                        dataTree.Enqueue(new Tuple<int, GPNode>(tuplenode.Item1 + 1, tuplenode.Item2.children[i]));

            }
        }

        #endregion

        #region Evaluation Population
        public void EvaluatePopulation()
        {
            for (int i = 0; i < chromosomes.Count; i++)
            {
                if (chromosomes[i].fitness == -1)
                    EvaluateChromosome(chromosomes[i]);
            }
        }

        public void EvaluateChromosome(GPChromosome c)
        {
            c.fitness = fitnessFunction.Evaluate(c.expressionTree, functionSet);
        }

        internal void CalculatePopulation()
        {
            // traženje najboljih hromosoma
            fitnessMax = 0;
            fitnessSum = 0;

            if (bestChromosome == null)
               bestChromosome = chromosomes[0];

            for (int i = 0; i < gpParameters.popSize; i++)
             {
                float fitness = (chromosomes[i]).fitness;
                // sum calc
                fitnessSum += fitness;

                // cal the best chromosome
                if (fitness > fitnessMax)
                 {
                    fitnessMax = fitness;
                    bestChromosome = chromosomes[i];
                 }
              }
            fitnessAvg = fitnessSum / gpParameters.popSize;
        }
        #endregion

        #region Various Getter Helpers methods

        /// <summary>
        /// We have to implement smarter algorithm for selecting nodes. Since order of nodes in the container 
        /// is so that the first iare function and then terminal, about half list contains terminal, 
        /// and we have to provide better probability for selecting function insead of terminal
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int GetRandomNode(int nodeCout)
        {
            if (nodeCout < 3)
                throw new Exception("Invalid number of chromosoem nodes.");
            //TODO:
            return Globals.radn.Next(3,nodeCout+1);
        }

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
        /// <param name="fSet"></param>
        public void SetFunctionSet(GPFunctionSet fSet)
        {
            functionSet = fSet;
            Globals.functions = fSet;
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

            //sort the chromosomes
            chromosomes.Sort();

            //Elitism number of very best chromosome to survive to new generation
            List<GPChromosome> elist = null;
            if (gpParameters.elitism > 0)
            {
                elist = new List<GPChromosome>();
                int lastIndex= chromosomes.Count-1;
                for (int i = 0; i < gpParameters.elitism; i++)
                    elist.Add(chromosomes[lastIndex-i].Clone());
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
            List<GPChromosome> newPopulation = new List<GPChromosome>();


            int currentSize = chromosomes.Count;
            double sumOfFitness = 0;

            //calculate sum of fitness
            for (int i = 0; i < currentSize; i++)
                sumOfFitness += chromosomes[i].fitness;
            

            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;

            for (int i = 0; i < currentSize; i++)
            {
                // cumulative normalized fitness
                s += (chromosomes[i].fitness / sumOfFitness);
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
                    if (wheelValue <= rangeMax[i] && chromosomes[i].fitness!=-1)
                    {
                        // add the chromosome to the new population
                        var ch = chromosomes[i].Clone();
                        newPopulation.Add(ch);
                        chromosomes[i].fitness = -1;

                        break;
                    }
                }
            }

            // survived chromosomes
            chromosomes.AddRange(newPopulation);
            
            // old population is goint to die
            GPChromosome.DestroyChromosomes(ref chromosomes);
        }
        private void RankSelection(int size)
        {
            // new chromosomes, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
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

            // survived chromosomes
            chromosomes.AddRange(newPopulation);

            // old population is goint to die
            GPChromosome.DestroyChromosomes(ref chromosomes);
        }
        private void TournamentSelection(int size)
        {
            // velicinaPopulacije of current chromosomes
            int currentSize = chromosomes.Count;
            List<GPChromosome> tourn = new List<GPChromosome>((int)gpParameters.SelParam1);
            // new chromosomes, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();

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

            // survived chromosomes
            chromosomes.AddRange(newPopulation);

            // old population is goint to die
            GPChromosome.DestroyChromosomes(ref chromosomes);
        }
        private void UniversalStohasticSelection(int size)
        {
            // new chromosomes, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            //chromosomes.Sort();

            int currentSize = chromosomes.Count();
            float fitnessSum = chromosomes.Sum(c => c.fitness);

            // get random distance value
            float randDist = (float)Globals.radn.NextDouble(0, 1.0 / (double)size);
            float partFitnes = 0;
            for (int j = 0; j < size; j++)
            {
                partFitnes = 0;
                for (int i = 0; i < chromosomes.Count; i++)
                {
                    partFitnes += chromosomes[i].fitness / fitnessSum;

                    if (randDist <= partFitnes)
                    {
                        newPopulation.Add(chromosomes[i].Clone());
                        break;
                    }
                }
                randDist += 1.0F / (float)size;
            }

            // survived chromosomes
            chromosomes.AddRange(newPopulation);

            // old population is goint to die
            GPChromosome.DestroyChromosomes(ref chromosomes);
        }
        private void FUSSSelection(int size)
        {
            // new chromosomes, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            //Maximum fitness
            double fitnessMax = chromosomes.Max(x => x.fitness);
            double rnd;
            double dif;
            int selIndex = 0;

            for (int j = 0; j < size; j++)
            {
                rnd = Globals.radn.NextDouble(0, fitnessMax);
                dif = Math.Abs(chromosomes[0].fitness - rnd);
                selIndex = 0;
                for (int i = 1; i < chromosomes.Count; i++)
                {
                    double curDif = Math.Abs(chromosomes[i].fitness - rnd);
                    if (dif > curDif)
                    {
                        dif = curDif;
                        selIndex = i;
                    }
                }
                newPopulation.Add(chromosomes[selIndex].Clone());
            }

            // survived chromosomes
            chromosomes.AddRange(newPopulation);

            // old population is goint to die
            GPChromosome.DestroyChromosomes(ref chromosomes);
        }
        private void SkrgicSelection(int size)
        {
            // new chromosomes, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            //  double k = 0.2; //additionalParameter;
            double fitnessMax = chromosomes.Max(x => x.fitness) * (1.0 + gpParameters.SelParam1);
            for (int i = 0; i < size; i++)
            {
                //Slucajni index iz populacije
                int randomIndex = Globals.radn.Next(0, chromosomes.Count);
                //Slucajni broj izmedju 0-maxFitnes ukljucujuci i maxFitness koje je prethodno vec izracunat kod evaluacije hromosoma
                double randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);

                while (true)
                {

                    //Akoje slucajno generirani broj manji ili jednak fitnesu slucajnog hromosoma selektuj hromosom
                    if (randomFitness <= chromosomes[randomIndex].fitness * (1.0 + gpParameters.SelParam1 / fitnessMax))
                    {
                        newPopulation.Add(chromosomes[randomIndex].Clone());
                        break;
                    }

                    randomIndex = Globals.radn.Next(0, chromosomes.Count);
                    randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);
                }
            }

            // survived chromosomes
            chromosomes.AddRange(newPopulation);

            // old population is goint to die
            GPChromosome.DestroyChromosomes(ref chromosomes);
        }
        #endregion

    }
}
