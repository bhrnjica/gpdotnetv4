using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GPdotNETLib
{
    [Serializable]
    public class GPPopulation
    {
        //History of evolution
        public List<GPHistory> EvolutionHistrory { get; set; }
        //some statistic for current population
        public double fitnessSum = 0;
        public double fitnessAvg = 0;
        public int BestCHromosomeApear { get; set; }
        public DateTime RunStarted { get; set; }
        public TimeSpan DurationOfRun { get; set; }
        //the best cromosome in the current population
        public GPChromosome BestChromosome
        {
            get;
            private set;
        }

        // thread safe random number generator
        static public ThreadSafeRandom rand = new ThreadSafeRandom(/*(int)DateTime.Now.Ticks*/);
                     
        //Parameters of Genetic Programming
        static public GPParameters GPParameters = new GPParameters();

        //Set of function and terminas
        static public GPFunctionSet GPFunctionSet = new GPFunctionSet();

        //Terminalset for training and testing algorithm
        static public GPTerminalSet GPTerminalSet = new GPTerminalSet();

        //usage parallel processing
        public bool IsParalelComputing { get; set; }

        //Containers for chromosomes
        public List<GPChromosome> Population
        {
            get;
            private set;
        }
        private List<GPChromosome> newPopulation;
        private ConcurrentStack<GPChromosome> newParallelPopulation;

        public int PopulationSize{get;set;}
       
         public GPPopulation()
        {
            Population = new List<GPChromosome>(); 
        }
        /// <summary>
        /// Constructor for Population.
        /// </summary>
        /// <param name="size">Size of Population.Number must be greather than 0.</param>
        /// <param name="terminalSet">Terminal set.Must be nonnull.</param>
        /// <param name="functionSet">Function set. If it pss null value default value is generated. The common 4 oparation +,-,*,/</param>
        /// <param name="parameters">Parameter of GP. If it pass a null value default parameter is initialized.</param>
        /// <param name="fitness">Fitness function for evaluating chromosomes. If it is a null value the default fitness is assined.</param>
        /// <param name="paralelComp">True for parallel multy core computing. False for secvencial computing.</param>
        public GPPopulation(int size, GPTerminalSet terminalSet,
                                      GPFunctionSet functionSet,
                                      GPParameters parameters,
                                      bool paralelComp)
        {
            IsParalelComputing = paralelComp;

            //During Population creating the size must be grether than 1, 
            //and papameters must be non null
            if (size < 50)
                throw new Exception("The size of population must be greater than 49!");

            //Ako nisu definisani parametri definisi defaultne
            if (parameters == null)
                GPParameters = new GPParameters();
            else
                GPParameters = parameters;

            if (EvolutionHistrory == null)
                EvolutionHistrory = new List<GPHistory>();

            if (functionSet == null)
                return;
            if (terminalSet == null)
                return;

            //Define function set and terminal set
            GPFunctionSet = functionSet;
            GPTerminalSet = terminalSet;

            //Extend capasity three time cause crossover and mutation
            Population = new List<GPChromosome>(size);
            newPopulation = new List<GPChromosome>(3 * size);
            if (IsParalelComputing)
                newParallelPopulation = new ConcurrentStack<GPChromosome>();

            //Initialize Population
            InitPopulation(size, false);
            //Evaluacija pocetne populacije
            EvaluationPopulation(false);

            //Generate chromosome until almost all chromosomomes be valid
            while (true)
            {
                Population.RemoveAll(x => float.IsNegativeInfinity(x.Fitness));

                int badChromosomesCount = Population.Count;
                if (PopulationSize - badChromosomesCount < PopulationSize * 0.01)
                    break;

                InitPopulation(PopulationSize - badChromosomesCount, false);
                EvaluationPopulation(false);
            }
            //Sort population for calculation statistic
            Population.Sort();
            CalculatePopulation();
        }
        public void InitPopulation(int size, bool newPop)
        {
            switch (GPParameters.einitializationMethod)
            {
                case EInitializationMethod.GrowInitialization:
                    GrowInitialization(size, newPop);
                    break;
                case EInitializationMethod.FullInitialization:
                    FullInitialization(size, newPop);
                    break;
                case EInitializationMethod.HalfHalfInitialization:
                    HalfHalfInitialization(size, newPop);
                    break;
                default:
                    throw new Exception("Initialization Method is unknown!");
                //break;
            }
            if (Population != null)
                PopulationSize = Population.Count;
        }
        private void GrowInitialization(int size, bool newPop)
        {
            //Random chromosomes generation
            for (int i = 0; i < size; i++)
            {
                // new chromosome
                GPChromosome c = GenerateChromosome(GPParameters.maxInitLevel);
                //collect chromosome in to Population
                if (newPop)
                    newPopulation.Add(c);
                else
                    Population.Add(c);
            }

        }

        private void FullInitialization(int size, bool newPop)
        {
            for (int i = 0; i < size; i++)
            {
                // new chromosome
                GPChromosome c = GenerateChromosome(GPParameters.maxInitLevel);
                //collect chromosome in to Population
                if (newPop)
                    newPopulation.Add(c);
                else
                    Population.Add(c);
            }

        }

        private void HalfHalfInitialization(int size, bool newPop)
        {
            //The max initial depth of tree
            int n = GPParameters.maxInitLevel;

            //factor of uniforms level distribution
            int br = size / (GPParameters.maxInitLevel - 1);
            //Based on definition of method generate equal number of chromosome with level 2,3,...maxInitLevel
            for (int i = 2; i <= n; i++)
            {
                if (i == n)//when i is n generate rest number of chromosomes
                    br = size - ((i - 2) * br);
                
                for (int j = 0; j < br; j++)
                {
                    //chromosome creation
                    GPChromosome c = GenerateChromosome(i);
                    //collect chromosome in to Population
                    if (newPop)
                        newPopulation.Add(c);
                    else
                        Population.Add(c);
                }
            }
        }

        private void EvaluateChromosome(GPChromosome c)
        {
            //Fitness already calculated
            if (c.Fitness != -1)
                return;

            c.Fitness = 0;
            List<ushort> lst=c.NodeValueEnumeratorDepthFirst.ToList();
            GPParameters.GPFitness.Evaluate(lst,GPFunctionSet, GPTerminalSet, c);
        }
        private GPChromosome GenerateChromosome(int initLevel)
        {
            GPChromosome c = new GPChromosome();
            // start generation of chromosome
            c.GenerateChromosome(initLevel);
            return c;
        }
        private void Reproduction()
        {
            //Amount of chromosomes copy from old to the new population
            int size = (int)(PopulationSize * GPParameters.probReproduction);

            if (size > Population.Count)
                InitPopulation(PopulationSize - Population.Count, true);

            if (IsParalelComputing)
                foreach (var p in GPParameters.GPSelectionMethod.Select(Population,size).Take(size))
                 newParallelPopulation.Push(p);
               
            else
                foreach (var p in GPParameters.GPSelectionMethod.Select(Population,size).Take(size))
                  newPopulation.Add(p);
        }
        //Crossover operator, random choose two chromosome and exchange their gene material
        private void Crossover()
        {
            if (GPParameters.probCrossover == 0)
                return;
          
            int numberOfCross = (int)(PopulationSize * GPParameters.probCrossover/2);

            if (IsParalelComputing)
            {
                Parallel.ForEach(Partitioner.Create(1, numberOfCross), (i) =>
                    {
                        for (int j = i.Item1; j < i.Item2; j++)
                            CrossOverChromosomes();
                    });
            }
            else
                for (int i = 1; i < numberOfCross; i++)
                    CrossOverChromosomes();

 //           Console.WriteLine("Ukrstanje");
           
            
        }
        private void CrossOverChromosomes()
        {
            //clone two chromosomes 
            GPChromosome c1 = SelectChromosomeFromPopulation().Clone();
            c1.Fitness = -1;
            GPChromosome c2 = SelectChromosomeFromPopulation().Clone();
            c1.Fitness = -1;

            //Node count in chromosome1
            int l1 = c1.NodeValueEnumeratorBreadthFirst.Count();
            //Random number from node1 count
            int rand1 = GPPopulation.rand.Next(1, l1);
            //Node count in chromosome2
            int l2 = c2.NodeValueEnumeratorBreadthFirst.Count();
            //Random number from node1 count
            int rand2 = GPPopulation.rand.Next(1, l2);

            //crossover
            var part1 = c1.NodeEnumeratorBreadthFirst.ElementAt(rand1);
            var part2 = c2.NodeEnumeratorBreadthFirst.ElementAt(rand2);
            //Get parent from random node
            var parent1 =part1.Parent;
            var parent2 = part2.Parent;
            //Retrieve index position os nodes
            var index1 = parent1.Nodes.IndexOf(part1);
            var index2 = parent2.Nodes.IndexOf(part2);

            //detach node from parent
            part1.Detach();
            part2.Detach();
            //Exchange gene material
            parent1.Nodes.Insert(index1, part2);
            parent2.Nodes.Insert(index2, part1);
            c1.Trim(GPParameters.maxCossoverLevel);
            c2.Trim(GPParameters.maxCossoverLevel);

            Debug.Assert(c1.Levels <= GPParameters.maxCossoverLevel);
            Debug.Assert(c2.Levels <= GPParameters.maxCossoverLevel);
            //Evaluate new chromosomes fitness
            EvaluateChromosome(c1);
            EvaluateChromosome(c2);

            //Store new chromosomes in to new population
            if (IsParalelComputing)
            {
                newParallelPopulation.Push(c1);
                newParallelPopulation.Push(c2);
            }
            else
            {
                newPopulation.Add(c1);
                newPopulation.Add(c2);
            }
        }
        //Mutation operation
        public void Mutation()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //if probability > 0
            if (GPParameters.probMutation <= 0)
                return;
            int numberofMutation = (int)(PopulationSize * GPParameters.probMutation);
            if (IsParalelComputing)
            {
                
               Parallel.ForEach(Partitioner.Create(1, numberofMutation), (chunk) =>
                {
                    for (int i = chunk.Item1; i < chunk.Item2; i++)
                        MutateChromosome();
                });
            }
            else
                for (int i = 1; i < numberofMutation; i++)
                        MutateChromosome();
     //       Console.WriteLine("Mutacija");
            
        }
        private void MutateChromosome()
        {
            
            //Choose clone from population
            GPChromosome c = SelectChromosomeFromPopulation().Clone();
            c.Fitness = -1;

            //Choose clone from population
            int l1 = c.NodeValueEnumeratorBreadthFirst.Count();
            //Random number
            int rand1 = GPPopulation.rand.Next(1, l1);
            //Random node
            var partNode = c.NodeEnumeratorBreadthFirst.ElementAt(rand1);
            //Remove children
            partNode.Value = GPFunctionSet.GenerateGene(false);
            partNode.Nodes.Clear();
            c.GenerateRandomExpressionTree(partNode, GPParameters.maxMutationLevel - partNode.Depth);
            //Debug.Assert(c.Levels<=GPParameters.maxMutationLevel);

            EvaluateChromosome(c);

            // pridruživanje mutanta u populaciju
            if (IsParalelComputing)
                newParallelPopulation.Push(c);
            else
                newPopulation.Add(c);

            
        }
        private void ShrinkMutation()
        {
            //Probability of operation
            if (GPParameters.probMutation == 0)
                return;
            int numberofMutation = (int)(PopulationSize * GPParameters.probMutation);
            if (IsParalelComputing)
            {

                Parallel.ForEach(Partitioner.Create(1, numberofMutation), (chunk) =>
                {
                    for (int i = chunk.Item1; i < chunk.Item2; i++)
                        ShrinkMutationChromosome();
                });
            }
            else
                for (int i = 1; i < numberofMutation; i++)
                    ShrinkMutationChromosome();

       //     Console.WriteLine("ShrinkMutacija");
           
        }
        private void ShrinkMutationChromosome()
        {
            //Choose clone from population
            GPChromosome c = SelectChromosomeFromPopulation().Clone();
            c.Fitness = -1;
            // Mutate
            //Node count
            int l1 = c.NodeValueEnumeratorBreadthFirst.Count();
            //Random number from nodes
            int rand1 = GPPopulation.rand.Next(1, l1);

            //random node
            var partNode = c.NodeEnumeratorBreadthFirst.ElementAt(rand1);
            //Remove children
            partNode.Nodes.Clear();
            //If random node is terminal shink their parent if it has parent
            if (partNode.Parent.Parent != null)
                partNode = partNode.Parent;

            partNode.Nodes.Clear();
            partNode.Value = GPFunctionSet.GenerateGene(false);

            EvaluateChromosome(c);

            // Add chromosome in to new population
            if (IsParalelComputing)
                newParallelPopulation.Push(c);
            else
                newPopulation.Add(c);
        }
        private void HoistMutation()
        {
            //Probability of operation
            if (GPParameters.probMutation == 0)
                return;
            int numberofMutation = (int)(PopulationSize * GPParameters.probMutation);

            if (IsParalelComputing)
            {

                Parallel.ForEach(Partitioner.Create(1, numberofMutation), (chunk) =>
                {
                    for (int i = chunk.Item1; i < chunk.Item2; i++)
                        HoistMutationChromosome();
                });
            }
            else
                for (int i = 1; i < numberofMutation; i++)
                    HoistMutationChromosome();

         //   Console.WriteLine("HOist Mutacija");
           
        }

        private void HoistMutationChromosome()
        {
            //Choose clone from population
            GPChromosome c = SelectChromosomeFromPopulation().Clone();
            c.Fitness = -1;
            //Node count
            int l1 = c.NodeValueEnumeratorBreadthFirst.Count();
            //random node
            GPTreeNode partNode;
            while (true)
            {
                //If chromosome has level less than 3 dont make Hoist mutation
                if (c.Levels < 3)
                    return;
                int rand1 = GPPopulation.rand.Next(1, l1);
                //slucajninod za ukrstanje
                partNode = c.NodeEnumeratorBreadthFirst.ElementAt(rand1);
                //we want to be a function node
                if (partNode.HasChildren)
                    break;
            }
            //Now the chromosome become random node tree
            c.FunctionTree = partNode;

            EvaluateChromosome(c);

            // Add chromosome in to new population
            if (IsParalelComputing)
                newParallelPopulation.Push(c);
            else
                newPopulation.Add(c);
        }
        private void Permutation()
        {
            //Probability of operation
            if (GPParameters.probPermutation == 0)
                return;
            int numberofPermutation = (int)(PopulationSize * GPParameters.probPermutation);
            if (IsParalelComputing)
            {

                Parallel.ForEach(Partitioner.Create(1, numberofPermutation), (chunk) =>
                {
                    for (int i = chunk.Item1; i < chunk.Item2; i++)
                        PermutationChromosome();
                });
            }
            else
             for (int i = 1; i < numberofPermutation; i++)
                    PermutationChromosome();

        //     Console.WriteLine("Permutacija");
            
        }

        private void PermutationChromosome()
        {
            //Choose clone from population
            GPChromosome c = SelectChromosomeFromPopulation().Clone();
            c.Fitness = -1;

            //Count only function nodes nodes
            int l1 = c.NodeValueEnumeratorBreadthFirst.Where(x => x > 1999).Count();
            //Permutation
            GPTreeNode partNode = null;
            while (true)
            {
                //Random node
                int rand1 = GPPopulation.rand.Next(0, l1);
                partNode = c.NodeEnumeratorBreadthFirst.Where(x => x.Value > 1999).ElementAt(rand1);
                if (partNode.HasChildren)
                {
                    if (partNode.Nodes.Count < 2)
                        break;

                    GPTreeNode temp = null;
                    int temIndex = GPPopulation.rand.Next(partNode.Nodes.Count);
                    int tempIndex2 = GPPopulation.rand.Next(partNode.Nodes.Count);
                    if (temIndex != tempIndex2)
                    {
                        temp = partNode.Nodes[temIndex];
                        partNode.Nodes[temIndex] = partNode.Nodes[tempIndex2];
                        partNode.Nodes[tempIndex2] = temp;
                        break;
                    }
                }
                else
                    break;
            }

            EvaluateChromosome(c);

            // Add chromosome in to new population
            if (IsParalelComputing)
                newParallelPopulation.Push(c);
            else
                newPopulation.Add(c);
        }
        /// <summary>
        /// Select chromosome based on selection proportional fitness for mating 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public GPChromosome SelectChromosomeFromPopulation()
        {
            // Choose random method to select chromosome for mating
            int randomMetod = rand.Next(1, 5);

            //Random index
            if (randomMetod == 1)//Rank selection
            {
                int randIndex = rand.Next(0, Population.Count);
                return Population[randIndex];
            }
            
            else if (randomMetod == 2)//Linear SS
            {

                while (true)//Skrgic selection
                {
                    //Slucajni index iz populacije
                    int randomIndex = rand.Next(0, Population.Count);
                    //Slucajni broj izmedju 0-maxFitnes ukljucujuci i maxFitness koje je prethodno vec izracunat kod evaluacije hromosoma
                    double randomFitness = rand.NextDouble(0, Population[0].Fitness, true/*include MaxValue*/);
                    //Akoje slucajno generirani broj manji ili jednak fitnesu slucajnog hromosoma selektuj hromosom
                    if (randomFitness <= Population[randomIndex].Fitness)
                        return Population[randomIndex];
                }

            }
            else if (randomMetod == 3)//Tournament selection TournamentSize=5
            {

                int currentSize = Population.Count;
                List<GPChromosome> tourn = new List<GPChromosome>(4);
                for (int i = 0; i < 5 && i < currentSize; i++)
                {
                    int ind = GPPopulation.rand.Next(currentSize);
                    tourn.Add(Population[ind]);

                }

                tourn.Sort();
                return tourn[0];

            }
            else//roulette wheel selection
            {
                // get wheel value
                double wheelValue = GPPopulation.rand.NextDouble();
                double partFitnes = 0;
                int size = Population.Count;
                // find the chromosome for the wheel value
                for (int i = 0; i < size; i++)
                {
                    partFitnes += Population[i].Fitness / fitnessSum;
                    if (wheelValue <= partFitnes)
                    {
                        // add the chromosome to the new Population
                        return Population[i];
                    }
                }
                // add the chromosome to the new Population
                return Population[0];
            }

        }
        
        //Creating current population. New population is based on Reproduction, and Genetic operators
        // Create population of first popSize chromosomes
        private void CreateCurrentPopulation()
        {
            Population.Clear();
            //newPopulation.Sort();
            Population.AddRange(newPopulation.OrderByDescending(x => x.Fitness).Take(PopulationSize));
            newPopulation.Clear();
        }
        private void CalculatePopulation()
        {

            if (Population.Count < 1)
                return;
            //Population is already sorted so the first cghromosome is best chromosome
            BestChromosome = Population[0];
            fitnessSum = (from p in Population where p.Fitness != float.NegativeInfinity select p.Fitness).Sum();
            fitnessAvg = (from p in Population where p.Fitness != float.NegativeInfinity select p.Fitness).Average();
       }

        //Evaluation fitness of chromosomes in pulation
        //Evaluation for new or current population. In initial there is no new population just current population
        private void EvaluationPopulation(bool newPop = true)
        {

            int count = newPop ? newPopulation.Count : Population.Count;
            if (IsParalelComputing)
            {
                Parallel.For(0, count, (i) =>
                {
                    EvaluateChromosome(newPop ? newPopulation[i] : Population[i]);
                });
            }
            else
            {
                for (int i = 0; i < count; i++)
                    EvaluateChromosome(newPop ? newPopulation[i] : Population[i]);
            }

        }
        
        //Starting Genetic programing algorithm
        public void StartEvolution()
        {
            if (IsParalelComputing)
            {
              //  Console.WriteLine("-----------------");
                if (newParallelPopulation == null)
                    newParallelPopulation = new ConcurrentStack<GPChromosome>();

                Task[] tasks = new Task[6]
                { 
                    Task.Factory.StartNew(() => Reproduction()),
                    Task.Factory.StartNew(() => Crossover()),   
                    Task.Factory.StartNew(() => Mutation()),
                    Task.Factory.StartNew(() => ShrinkMutation()),
                    Task.Factory.StartNew(() => HoistMutation()),
                    Task.Factory.StartNew(() => Permutation())
                };

                // Schedule a task to execute when all previous tasks complete.
                var continuationOnAll = Task.Factory.ContinueWhenAll(
                    tasks.ToArray(), (Task[] completedTasks) => CreateNewPopulation());

                continuationOnAll.Wait();

                foreach (var t in tasks)
                    t.Dispose();
                continuationOnAll.Dispose();

            }
            else
            {
                Reproduction();
                Crossover();
                Mutation();
                ShrinkMutation();
                HoistMutation();
                Permutation();
                //make current population from new population
                CreateCurrentPopulation();
                //Create statistic of current population
                CalculatePopulation();
            }

            
        }
        void CreateNewPopulation()
        {
          //  Console.WriteLine("nova populacija");
            Population.Clear();
            IEnumerable<GPChromosome> q = newParallelPopulation.OrderByDescending(x => x.Fitness).Take(PopulationSize);
            fitnessSum = q.Where(f => f.Fitness != float.NegativeInfinity).Sum(f => f.Fitness);
            fitnessAvg = q.Where(f => f.Fitness != float.NegativeInfinity).Average(f => f.Fitness);
            Population.AddRange(q);
            BestChromosome=Population[0];
            newParallelPopulation.Clear();

        }
        /// <summary>
        /// Loads a Population from a file.
        /// </summary>
        /// <param name="filePath">The file path of the saved Population.</param>
        /// <returns>The loaded Population.</returns>
        public static GPPopulation Load(string filePath)
        {
            FileStream s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryFormatter b = new BinaryFormatter();
            GPPopulation population = (GPPopulation)b.Deserialize(s);
            s.Close();
            return population;
        }
        /// <summary>
        /// Saves the current Population.
        /// </summary>
        /// <param name="filePath">The path to the file that this Population is saved in.</param>
        public void Save(string filePath)
        {
            FileStream s = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, this);
            s.Close();
        }



    }
}
