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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace GPNETLib
{
    //Main class of GP.
    [Serializable]
    public class GPPopulation
    {
        // random number generator
        //Kod nove implementacije neide argument u konstruktoru
        static public ThreadSafeRandom rand = new ThreadSafeRandom(/*(int)DateTime.Now.Ticks*/);

        //Parameters of Genetic Programming
        public GPParameters gpParameters;
        //Set of function and terminas
        public GPFunctionSet gpFunctionSet;
        //Set of TrainingData for testing and training TrainingData for prediction
        public GPTerminalSet gpTerminalSet;
        
               
        //Koristenje multyCode procesora u paralelno računanje
        public bool ParalelComputing { get; set; }

        //Container for population
        public  List<GPChromosome> population;

        //Temporary container
        private List<GPChromosome> tempCrossover1;
        private List<GPChromosome> tempCrossover2;
        private List<GPChromosome> tempMutation;
        private List<GPChromosome> tempPermutation;

        private ConcurrentStack<GPChromosome> temCross;

        public int popSize = 0;

        //vrijednosti funkcije max min, srednja
        public double fitnessMax = 0;
        public double fitnessSum = 0;
        public double fitnessAvg = 0;        

        //najbolji hromosom u populaciji
        public GPChromosome bestChromosome = null;
        /// <summary>
        /// Constructor for population.
        /// </summary>
        /// <param name="size">Size of population.Number must be greather than 0.</param>
        /// <param name="terminalSet">Terminal set.Must be nonnull.</param>
        /// <param name="functionSet">Function set. Must be nonnull.</param>
        /// <param name="parameters">Parameter of GP. If it pass a null value default parameter is initialized.</param>
        /// <param name="paralelComp">True for parallel multy core evaluation. False for secvencial computing.</param>
        public GPPopulation(int size,   GPTerminalSet terminalSet,
                                        GPFunctionSet functionSet,
                                        GPParameters parameters, 
                                        bool paralelComp)
        {
            ParalelComputing = paralelComp;
            //During population creating the size must be grether than 1, 
            //and papameters must be non null
            if (size < 1)
                return;
            //Ako nisu definisani parametri definisi defaultne
            if (parameters == null)
                gpParameters = new GPParameters();
            else
                gpParameters = parameters;

            if (functionSet == null)
                return;
            if (terminalSet == null)
                return;        
            //
            gpFunctionSet = functionSet;
            gpTerminalSet = terminalSet;
           
            //Extend capasity three time cause crossover and mutation
            population= new List<GPChromosome>(3* size);
            if (ParalelComputing)
            {
                tempCrossover1 = new List<GPChromosome>(size);
                tempCrossover2 = new List<GPChromosome>(size);
                tempMutation = new List<GPChromosome>();
                tempPermutation = new List<GPChromosome>();
                temCross = new ConcurrentStack<GPChromosome>();
            }
            

            //Creating starting Chromosome
            GPChromosome ancestor = new GPChromosome();

            //Initialize population
            InitPopulation(size);

            //Evaluacija pocetne populacije
            if (ParalelComputing)
                EvaluationParallel();
            else
                EvaluationSec();
         
            //Calculate population statistic parameters
            CalculatePopulation();
            
        }
        public void InitPopulation(int size)
        {
            switch (gpParameters.einitializationMethod)
            {
                case EInitializationMethod.GrowInitialization:
                    GrowInitialization(size);
                    break;
                case EInitializationMethod.FullInitialization:
                    FullInitialization(size);
                    break;
                case EInitializationMethod.HalfHalfInitialization:
                    HalfHalfInitialization(size);
                    break;
                default:
                    throw new Exception("Initialization Method is unknown!");
                //break;
            }
            if(population!=null)
                popSize = population.Count;
        }
        #region Initial Method
        /// <summary>
        /// Randomly generate tree program structures
        /// </summary>
        /// <param name="size"></param>
        private void GrowInitialization(int size)
        {
            //is the same as FillInit, in generate method 
            // chromosomes will be generated regarding initparametrs type
            FullInitialization(size);
        }
        //Generate program strutures with specified level dept
        private void FullInitialization(int size)
        {
            for (int i = 0; i < size; i++)
            {
                // kreiranje novog hromosoma
                GPChromosome c = GenerateChromosome(gpParameters.maxInitLevel);
                //collect chromosome in to population
                population.Add(c);
            }
            
        }
        /// <summary>
        /// Mixed version of previous two init methods
        /// </summary>
        /// <param name="size"></param>
        private void HalfHalfInitialization(int size)
        {
            //The max init depth of tree
            int n = gpParameters.maxInitLevel;

            //Faktor ravnomjernog rasporeda dubine hromosoma
            int br = size / (gpParameters.maxInitLevel - 1);
            //Ravomjerna inicijalizacija hromosoma shodno dubini drveta
            for (int i = 2; i <= n; i++)
            {
                if (i == n)//kad i dodje do n onda uzima preostali dio populacije da je inicijalizira
                    br = size - ((i - 2) * br);
                //Promjena dubine drveta shodno metodi Inicijalizacije 
                for (int j = 0; j < br; j++)
                {
                    // kreiranje novog hromosoma
                    GPChromosome c = GenerateChromosome(i);

                    // pridruzivanje populaciji
                    population.Add(c);
                }
            }
        }
#endregion
        #region Chromosome generation
        /// <summary>
        /// Proces of randomly generating chromosome
        /// </summary>
        /// <param name="initLevel"></param>
        /// <returns></returns>
        private GPChromosome GenerateChromosome(int initLevel)
        {
            GPChromosome c = new GPChromosome();

            // Prvi cvor (korijen) generiramo kao funkciju
            GenerateGene(c.Root, true);
            GenerateChromosome(c.Root, initLevel-1);

            return c;
        }

        private void GenerateChromosome(FunctionTree treeNode, int level)
        {
            if (level <= 0)
            {
                GenerateGene(treeNode, false);
                treeNode.SubFunctionTree = null;
            }
            else
            {
                //Ako nije maximalni novo onda treba postupiti shodno metodi inicijalizacije
                if (gpParameters.einitializationMethod == EInitializationMethod.FullInitialization ||
                    gpParameters.einitializationMethod == EInitializationMethod.HalfHalfInitialization)
                    GenerateGene(treeNode, true);
                else//A ako je u pitanju rastuca slucajno generisi cvor
                    GenerateGene(treeNode);
            }

            // Kada se je generirao Cvor (GEN) gornji kod, tada se trea generirati i 
            // potomci toga Gena. Odnosno generiraj onoliko potomaka koliko funkcija ima argumenata
            if (treeNode.NodeValue>1999)
            {   
                //Number of aritry of node function  
                int numAritry = gpFunctionSet.functions[treeNode.NodeValue - 2000].Aritry;

                //Formiraj onoliko potomaka koliko ima argumenata
                treeNode.SubFunctionTree = new List<FunctionTree>(numAritry);

                //Svaki novonastali potomak inicijaliziraj i generiraj
                for (int i = 0; i < numAritry; i++)
                {
                    // Formiraj potomak
                    FunctionTree child = new FunctionTree();

                    //Generiraj ga s tim da se nivo smanji za jedan
                    GenerateChromosome(child, level - 1);
                   
                    // Kad se generira pridruzi potomke njgovom rodtelju
                    treeNode.SubFunctionTree.Add(child);
                }
            }
        }
        private void GenerateGene(FunctionTree gPGenNode)
        {
            // potrebno je dati više vjerojatnost da se generiše funkcija u odnosu na terminale
           GenerateGene(gPGenNode, (rand.Next(4) != 3));
        }
        private void GenerateGene(FunctionTree gPGenNode, bool isFunction)
        {
           
            //Ako je broj terminala veći od 1000
            if (gpFunctionSet.terminals.Count > 999)
                throw new Exception("Maximum number of terminals must be less than 999");

            // Brojevi veci od 2000 oznacavat ce funkcije
            if (isFunction)
            {
                short f = (short)rand.Next(gpFunctionSet.functions.Count);
                gPGenNode.NodeValue = (short)(2000 + f);
            }

            //Brojevi od 1000-2000 oznacavat ce slobodne koeficijente i ulazne parametre
            else
            {
                //Slucajno biramo jedan od terminala
                short r = (short)rand.Next(gpFunctionSet.terminals.Count);
                gPGenNode.NodeValue = (short)(1000 + r);
            }
        }
#endregion
        #region Mating process
        private void Crossover()
        {
            if (gpParameters.probCrossover == 0)
                return;
            //for (int i = 1; i < popSize; i += 2)
            if (ParalelComputing)
            {
                Parallel.For(1, popSize/2,  (i) =>
                {
                    // dogadjanje ukrstanja shodno vjerojatnosti
                    if (rand.NextDouble() <= gpParameters.probCrossover)
                    {
                        // kloniranje oba hromosoma i priprema za ukrštanje
                        
                        GPChromosome c1 = (population[i*2 - 1]).Clone();
                        GPChromosome c2 = (population[i*2]).Clone();

                        // ukrštanje
                        CrossOverHromosomes(c1.Root, c2.Root);

                        // pridruživanje dva nova potomka u populaciju
                        temCross.Push(c1);
                        temCross.Push(c2);
                    }
                }
                );
            }
            else
            {
                for (int i = 1; i < popSize; i += 2)
                {
                    // dogadjanje ukrstanja shodno vjerojatnosti
                    if (rand.NextDouble() <= gpParameters.probCrossover)
                    {
                        // kloniranje oba hromosoma i priprema za ukrštanje
                        GPChromosome c1 = (population[i - 1]).Clone();
                        GPChromosome c2 = (population[i]).Clone();

                        // ukrštanje
                        CrossOverHromosomes(c1.Root, c2.Root);

                        // pridruživanje dva nova potomka u populaciju
                        population.Add(c1);
                        population.Add(c2);
                    }
                }
            }
            
        }

        public void CrossOverHromosomes(FunctionTree c1, FunctionTree c2)
        {
            FunctionTree node = c1;// root u privremenu varijablu od koje se bira slucajno potomak
            while (true)
            {
                if (node.SubFunctionTree == null)
                {
                    node= Swap(node,c2);
                    break;
                }
                // slucajno izabrani broj 
                int r = rand.Next(node.SubFunctionTree.Count);
                //Slucajno izabrani potomak
                FunctionTree child = node.SubFunctionTree[r];
                

                //Ako slucajno izabrani potomak nema potomaka onda se on ukrsta ili ako se generise slucajno broj 0
                if ((child.SubFunctionTree == null) || (rand.Next(gpParameters.maxCossoverLevel) == 0/*(int)(gpParameters.maxCossoverLevel/2)*/))
                {
                    // izvrsi zamjenu genetskog materijala
                    node.SubFunctionTree[r]=Swap(node.SubFunctionTree[r], c2);
                    break;//prekini petlju jer je ukrstanje napravljeno
                }

                // Udju u potomke potomka
                node = child;
            }

            // Na kraju poravnati dubinu na ogranicenu ako je doslo do povecaja dubine
            Trim(c1, gpParameters.maxCossoverLevel);
            Trim(c2,gpParameters.maxCossoverLevel);
        }
        private void Trim(FunctionTree geneNode, int level)
        {
            // i ako je nivo 0 tada 
            if (level == 1 || geneNode.SubFunctionTree == null)
            {
                // odstrani potomke
                if (geneNode.SubFunctionTree != null)
                {
                    geneNode.SubFunctionTree.Clear();
                    //postavi null za potimke da se delocira memorija
                    geneNode.SubFunctionTree = null;
                }
                // a cvor funkcija generiraj kao terminal
                if(geneNode.NodeValue>=2000)
                    GenerateGene(geneNode, false);
            }
            else //Ako nivo nije 0 tada udji u novo za 1 manji
            {
                // prolazi kroz potomke sa novoom za 1 manjim
                for (int i = 0; i < geneNode.SubFunctionTree.Count; i++)
                    Trim(geneNode.SubFunctionTree[i], level - 1);
            }
        }

        private FunctionTree Swap(FunctionTree c1, FunctionTree c2)
        {
            //privremena varijabla
            FunctionTree node2 = c2;

            while (true)
            {
                if (node2.SubFunctionTree == null)
                {
                    // zamjena cvorova
                    FunctionTree tempNode = c2;
                    c2 = c1;
                    return tempNode;
                }
                // slucajno izabiremo potomak od node2-a
                int r = rand.Next(node2.SubFunctionTree.Count);

                //pohranjujemo potomak u varijablu child i sad od njega poslije trazimo tacku ukrstanja
                FunctionTree child = node2.SubFunctionTree[r];

                // swap the random node2, if it is an end node2 or
                // random generator "selected" this node2
                //Ako child nema potomaka tada tj ako je terminal tada je to tacka zamjene
                if ((child.SubFunctionTree == null) || (rand.Next(gpParameters.maxCossoverLevel) == 0/*(int)(gpParameters.maxCossoverLevel / 2)*/))
                {
                    // zamjena cvorova
                    FunctionTree tempNode = node2.SubFunctionTree[r];
                    node2.SubFunctionTree[r] = c1;
                    return tempNode;
                }

                //Ako child ima potomke tada se vracamo na pocetak i 
                //ponovo slucajno biramo potomak sad od child-a  
                node2 = child;
            }
        }

        private void Mutation()
        {
            //Ako je mutacija 0% ne ulazi u petlju
            if (gpParameters.probMutation == 0)
                return;
            for (int i = 0; i < popSize; i++)
            {
                // generiranje slučajnog broja i provjera vjerojatnosti mutacije
                if (rand.NextDouble() <= gpParameters.probMutation)
                {
                    // kreiranje novog hromosoma
                    // kloniranje  hromosoma i priprema za mutiranje
                    GPChromosome c = (population[i]).Clone();
                    // mutacija
                    MutateChromosome(c.Root);

                    // pridruživanje mutanta u populaciju
                    if (ParalelComputing)
                        tempMutation.Add(c);
                    else
                        population.Add(c);

                }
            }            
        }

        public void MutateChromosome(FunctionTree c)
        {
            //Refeentni nivo
            int currentLevel = 0;
            //Maximalni nivo mutacija. -1 ide zbog brojanja od nule
            int maxLevel = gpParameters.maxMutationLevel - 1;
            // current node
            FunctionTree node = c;

            while (true)
            {
                // Ako je node bez potomaka onda treba generiratido nivoa maximalnog za mutaciju
                if (node.SubFunctionTree == null)
                {
                    //Ako je tekuci nivo dostigao maximalni nivo mutacije formiraj Terminal
                    if (currentLevel >= maxLevel)
                        GenerateGene(node, false);

                    else //Ako je tekuci nivo manji od maximalnog onda generiraj dalje
                        GenerateChromosome(node, currentLevel - 1);
                    break;
                }

                //Ovdje pocinjemo proces mutacije. Slucajno generisemo tačku mutacije. Odnosno generiramo slucajnog 
                // potomka. Koji ce mutirati ili ce mutirati njegov potomak. +1 znaci generiranje brojeva do vrijednosti Cout
                // ako je generirani broj == Count tada taj cvor mutira inace mutira njegov generirani potomak
                int r = rand.Next(node.SubFunctionTree.Count+1);


                //Ako smo generirali broj koji je isti kao i broj potomaka argumenata funkcija tada je odluka da taj cvor mutira
                //Ovo mi je malo bezvez nacin odluke koji ce cvor mutirati jer je u glavnom broj argumenata 2 tako da vec prvi cvorovi 
                // najvjerojatnije da ce mutirati
                if (r == node.SubFunctionTree.Count)
                {
                    // Kada smo odabrali da ce doticni cvor da mutira 
                    // ponovo ga regegeneriramo. 
                    if (currentLevel >= maxLevel)//Ako je level veci ili jedna maximalnom levelu tada moramo generirati terminal
                        GenerateGene(node, false);
                    else
                        GenerateGene(node);

                    // Ako smo regenerirani cvor postao terminal tada njegovi potomci 
                    // trebaju biti odbacenu
                    if (node.NodeValue<2000)// || currentLevel >= maxLevel)
                        node.SubFunctionTree = null;

                    else//Ako je novogenerirani (mutirani cvor ) funkcija tada dalje regeneriramo hromosom
                    {
                        // ako mutirani cvor ne posjeduje potomke
                        // potrebno ih je formirati
                        int count = 0;
                        if (node.SubFunctionTree == null)
                        {
                            //I to onoliko koliko ima argumanata mutirani cvor
                            count = gpFunctionSet.functions[node.NodeValue - 2000].Aritry;
                            node.SubFunctionTree = new List<FunctionTree>(count);
                        }
                        else
                        {
                            count = gpFunctionSet.functions[node.NodeValue - 2000].Aritry;
                            //Debug.Assert(count == gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry);
                        }
                        // Sada uskladjujemo potomke sa brojem argumenata 
                        //funkcije koje se formirala nakon mutacije
                        //To znaci da akomutirani cvor ima isti broj argumenata kao i prije mutacije
                        // tu ne radimo nista ostavljamo ga kakav jeste
                        //A ako je taj broj razlicit potrebno je to usladiti
                        if (node.SubFunctionTree.Count != count)
                        {
                            //Pa ako je broj argumenata veci tada odstranjujemo visak
                            if (node.SubFunctionTree.Count > count)
                            {
                                // remove extra children
                                node.SubFunctionTree.RemoveRange(count, node.SubFunctionTree.Count - count);
                            }
                            else//A ako je manji dodajemo 
                            {
                                // add missing children
                                for (int i = node.SubFunctionTree.Count; i < count; i++)
                                {
                                    // create new child
                                    FunctionTree child = new FunctionTree();
                                    GenerateChromosome(child, currentLevel - 1);
                                    // add the new child
                                    node.SubFunctionTree.Add(child);
                                }
                            }
                        }
                                            }
                    break;//Kada se dogodi mutacija tada for petlja nema vise sta da vrti
                }

                // Ako se nije desila mutacija na tekucem novou tada nivo pomjeramo za jedan vise i ponavljamo petlju
                node = node.SubFunctionTree[r];
                currentLevel++;
            }
        }
        void Permutation()
        {
            //Ako je mutacija 0% ne ulazi u petlju
            if (gpParameters.probPermutation == 0)
                return;
            for (int i = 0; i < popSize; i++)
            {
                // generiranje slučajnog broja i provjera vjerojatnosti mutacije
                if (rand.NextDouble() <= gpParameters.probPermutation)
                {
                    // kreiranje novog hromosoma
                    // kloniranje  hromosoma i priprema za mutiranje
                    GPChromosome c = (population[i]).Clone();
                    GPChromosome p = c.Clone();
                    // mutacija
                    PermutateChromosome(c.Root);

                    // pridruživanje mutanta u populaciju
                    if (ParalelComputing)
                        tempPermutation.Add(c);
                    else
                        population.Add(c);

                }
            }       
        }

        public void PermutateChromosome(FunctionTree c)
        {
            // current node
            FunctionTree node = c;

            while (true)
            {
                if (node.SubFunctionTree == null)
                {
                    break;
                }
                //Ovdje pocinjemo proces mutacije. Slucajno generisemo tačku mutacije. Odnosno generiramo slucajnog 
                // potomka. Koji ce mutirati ili ce mutirati njegov potomak
                int r = rand.Next(node.SubFunctionTree.Count+1);
                int count = node.SubFunctionTree.Count;
                //Ako smo generirali broj koji je isti kao i broj potomaka argumenata funkcija tada je odluka da taj cvor mutira
                //Ovo mi je malo bezvez nacin odluke koji ce cvor mutirati jer je u glavnom broj argumenata 2 tako da vec prvi cvorovi 
                // najvjerojatnije da ce mutirati
                if (r == count)
                {
                    // Kada smo odabrali da ce doticni cvor da permutira 
                    // generiramo slucajnu permutaciju na osnovu broja argumenata
                    int aritry = count;

                    //Ako je aritry == 1 onda nema smisla praviti permutaciju 
                    if (aritry <= 1)
                        continue;
                    //Ako je Aritry == 2 tada imamo samo jednu permutaciju pa 
                    // je definšemo na sljedeci nacin
                    else if (aritry == 2)
                    {
                        FunctionTree temp = null;
                        temp = node.SubFunctionTree[0];
                        node.SubFunctionTree[0] = node.SubFunctionTree[1];
                        node.SubFunctionTree[1] = temp;

                    }
                    else
                    {
                        for (int ii = 0; ii < aritry; ii++)
                        {
                            FunctionTree temp = null;
                            int temIndex = rand.Next(aritry);
                            int tempIndex2 = rand.Next(aritry);
                            if (temIndex != tempIndex2)
                            {
                                temp = node.SubFunctionTree[temIndex];
                                node.SubFunctionTree[temIndex] = node.SubFunctionTree[tempIndex2];
                                node.SubFunctionTree[tempIndex2] = temp;
                            }

                        }
                    }

                    break;//Kada se dogodi mutacija tada for petlja nema vise sta da vrti
                }

                // prelazimo na novi nivo
                node = node.SubFunctionTree[r];
            }
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
            int repNumber = (int)(gpParameters.probReproduction * popSize);

            //Elitism number of very best chromosome to survive to new generation
            List<GPChromosome> elist=null;
            if(gpParameters.elitism>0)
                elist = population.Distinct().OrderByDescending(x => x.Fitness).Take(gpParameters.elitism).ToList();

            // vrsenje selekcije odredjenom metodom
            int numb = popSize - repNumber - gpParameters.elitism;
            switch (gpParameters.eselectionMethod)
            {
                case ESelectionMethod.EFitnessProportionateSelection:
                    FitnessProportionateSelection(numb);
                    break;
                case ESelectionMethod.Rankselection:
                    RankSelection(numb);
                    break;
                case ESelectionMethod.TournamentSelection:
                    TournamentSelection(numb);
                    break;
                case ESelectionMethod.StochasticUniversalSelection:
                    UniversalStohasticSelection(numb);
                    break;
                case ESelectionMethod.FUSSelection:
                    FUSSSelection(numb);
                    break;
                case ESelectionMethod.SkrgicSelection:
                    SkrgicSelection(numb);
                    break;
                default:
                    FitnessProportionateSelection(numb);
                    break;
            }
            

            if (gpParameters.elitism > 0)
                population.AddRange(elist);
            if (repNumber > 0)
                InitPopulation(repNumber);
            //Calculate population statistic parameters
            CalculatePopulation();

            Debug.Assert(population.Count==popSize);

        }
        private void FitnessProportionateSelection(int size)
        {
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();

            // velicinaPopulacije of current population
            // population.Sort();
            int currentSize = population.Count;
            double sumOfFitness = 0;
            //calculate sum of fitness
            for (int i = 0; i < currentSize; i++)
            {
                sumOfFitness += population[i].Fitness;
            }

            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;

            for (int i = 0; i < currentSize; i++)
            {
                // cumulative normalized fitness
                s += (population[i].Fitness / sumOfFitness);
                rangeMax[i] = s;
            }

            // select population from old population to the new population
            for (int j = 0; j < size; j++)
            {
                // get wheel value
                double wheelValue = rand.NextDouble();
                // find the chromosome for the wheel value
                for (int i = 0; i < currentSize; i++)
                {
                    //double wheelValue = rand.NextDouble();
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new population
                        newPopulation.Add(population[i]);
                        break;
                    }
                }
            }

            // empty current population
            // move elements from new to current population
            population.Clear();
            // newPopulation.Sort();
            population.AddRange(newPopulation);
            Debug.Assert(population.Count == size);

        }
        private void RankSelection(int size)
        {
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            // velicinaPopulacije of current population
            int currentSize = population.Count;

            // sort current population
            population.Sort();

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

            // select population from old population to the new population
            for (int j = 0; j < size; j++)
            {
                // get wheel value
                double wheelValue = rand.NextDouble();
                // find the chromosome for the wheel value
                for (int i = 0; i < currentSize; i++)
                {
                    // get wheel value
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new population
                        newPopulation.Add(population[i]);
                        break;
                    }
                    //Debug.Assert(false);
                }
            }

            // empty current population
            population.Clear();
            population.AddRange(newPopulation);
        }
        private void TournamentSelection(int size)
        {
            // velicinaPopulacije of current population
            int currentSize = population.Count;
            List<GPChromosome> tourn = new List<GPChromosome>((int)gpParameters.SelParam1);
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            
            for (int j = 0; j < size; j++)
            {
                currentSize = population.Count;
                for (int i = 0; i < gpParameters.SelParam1 && i < currentSize; i++)
                {
                    int ind = rand.Next(currentSize);
                    tourn.Add(population[ind]);
                    
                }
                
                tourn.Sort();
                newPopulation.Add(tourn[0]);
                population.Remove(tourn[0]);
                tourn.Clear();
            }

            // empty current population
            // move elements from new to current population
            population.Clear();
            // newPopulation.Sort();
            population.AddRange(newPopulation);
            Debug.Assert(population.Count == size);
        }
        private void UniversalStohasticSelection(int size)
        {
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            population.Sort();

            int currentSize = population.Count();
            float fitnessSum = population.Sum(c => c.Fitness);

            // get random distance value
            float randDist = (float)GPPopulation.rand.NextDouble(0, 1.0 / (double)size);
            float partFitnes = 0;
            for (int j = 0; j < size; j++)
            {
                partFitnes = 0;
                for (int i = 0; i < population.Count; i++)
                {
                    partFitnes += population[i].Fitness / fitnessSum;

                    if (randDist <= partFitnes)
                    {
                        newPopulation.Add(population[i]);
                        break;
                    }
                }
                randDist += 1.0F / (float)size;
            }

            // empty current population
            population.Clear();

            //Create new population
            population.AddRange(newPopulation);
            newPopulation.Clear();
        }
        private void FUSSSelection(int size)
        {
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            //Maximum fitness
            double fitnessMax = population.Max(x => x.Fitness);
            double rnd;
            double dif;
            int selIndex = 0;

            for (int j = 0; j < size; j++)
            {
                rnd = GPPopulation.rand.NextDouble(0, fitnessMax);
                dif = Math.Abs(population[0].Fitness - rnd);
                selIndex = 0;
                for (int i = 1; i < population.Count; i++)
                {
                    double curDif = Math.Abs(population[i].Fitness - rnd);
                    if (dif > curDif)
                    {
                        dif = curDif;
                        selIndex = i;
                    }
                }
                newPopulation.Add(population[selIndex]);
            }

            // empty current population
            population.Clear();

            //Create new population
            population.AddRange(newPopulation);
        }
        private void SkrgicSelection(int size)
        {
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
          //  double k = 0.2; //additionalParameter;
            double fitnessMax = population.Max(x => x.Fitness) * (1.0 + gpParameters.SelParam1);
            for (int i = 0; i < size; i++)
            {
                //Slucajni index iz populacije
                int randomIndex = GPPopulation.rand.Next(0, population.Count);
                //Slucajni broj izmedju 0-maxFitnes ukljucujuci i maxFitness koje je prethodno vec izracunat kod evaluacije hromosoma
                double randomFitness = GPPopulation.rand.NextDouble(0, fitnessMax/*, true include MaxValue*/);

                while (true)
                {

                    //Akoje slucajno generirani broj manji ili jednak fitnesu slucajnog hromosoma selektuj hromosom
                    if (randomFitness <= population[randomIndex].Fitness * (1.0 + gpParameters.SelParam1 / fitnessMax))
                    {
                        newPopulation.Add(population[randomIndex]);
                        break;
                    }

                    randomIndex = GPPopulation.rand.Next(0, population.Count);
                    randomFitness = GPPopulation.rand.NextDouble(0, fitnessMax/*, true include MaxValue*/);
                }
            }

            // empty current population
            population.Clear();

            //Create new population
            population.AddRange(newPopulation);


        }
        #endregion
        #region Evolution process
        /// <summary>
        /// Evolution process
        /// </summary>
        public void StartEvolution()
        {
            if (ParalelComputing)
                StartEvolutionParallel();
            else
                StartEvolutionSec();
        }
        /// <summary>
        /// Start evolution in sequntial mode
        /// </summary>
        public void StartEvolutionSec()
        {
            if (temCross == null)
                temCross = new ConcurrentStack<GPChromosome>();
            Crossover();
            Mutation();
            Permutation();
            EvaluationSec();
            Selection();
        }

       /// <summary>
       /// Start evolution in parallel mode
       /// </summary>
        public void StartEvolutionParallel()
        {
            Parallel.Invoke(
            () => Crossover(),
            ()=>Mutation(),
            ()=>Permutation()
            );

            population.AddRange(temCross.ToList());
            population.AddRange(tempMutation);
            population.AddRange(tempPermutation);
            tempMutation.Clear();
            tempCrossover1.Clear();
            tempCrossover2.Clear();
            tempPermutation.Clear();
            temCross.Clear();
            EvaluationParallel();
            Selection();
            
        }
        /// <summary>
        /// Evaluation of chromosome
        /// </summary>
        /// <param name="c"></param>
        private void EvaluateChromosome(GPChromosome c)
        {
            c.Fitness = 0;
            List<int> lst = new List<int>();

            FunctionTree.ToListExpression(lst, c.Root);
            gpParameters.GPFitness.Evaluate(lst, gpFunctionSet, gpTerminalSet, c);
        }
        /// <summary>
        /// Evaluation of chromosomes in population in sequntial mode
        /// </summary>
        private void EvaluationSec()
        {
            int count = population.Count;
            for (int i = 0; i < count; i++)
                EvaluateChromosome(population[i]);
        }
        /// <summary>
        /// Evaluation of chromosomes in population in parallel mode
        /// </summary>
        private void EvaluationParallel()
        {
            int count = population.Count;
            Parallel.For(0, count, (cc) =>
            {
                EvaluateChromosome(population[cc]);
            });
        }
        private void CalculatePopulation()
        {
            // traženje najboljih hromosoma
            fitnessMax = 0;
            fitnessSum = 0;
            if (ParalelComputing)
            {
                var q =
                  (from p in population.AsParallel()
                   orderby p.Fitness descending
                   select p);
                int index = 0;
                foreach (GPChromosome c in q)
                {
                    //The first chromosome is the best
                    if (index == 0)
                    {
                        fitnessMax = c.Fitness;
                        bestChromosome = c;
                        index++;
                    }
                    fitnessSum += c.Fitness;
                }
                fitnessAvg = fitnessSum / popSize;
            }
            else
            {
                if (bestChromosome == null)
                    bestChromosome = population[0];

                for (int i = 0; i < popSize; i++)
                {
                    float fitness = (population[i]).Fitness;
                    // kakulacija suma funkcija
                    fitnessSum += fitness;

                    // izračuvavanje najboljeg hromosoma
                    if (fitness > fitnessMax)
                    {
                        fitnessMax = fitness;
                        bestChromosome = population[i];
                    }
                }
                fitnessAvg = fitnessSum / popSize;
            }
        }
        #endregion
        #region Serilization
        /// <summary>
        /// Loads a population from a file.
        /// </summary>
        /// <param name="filePath">The file path of the saved population.</param>
        /// <returns>The loaded population.</returns>
        public static GPPopulation Load(string filePath)
        {
            FileStream s = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryFormatter b = new BinaryFormatter();
            GPPopulation population = (GPPopulation)b.Deserialize(s);
            s.Close();
            return population;
        }
        /// <summary>
        /// Saves the current population.
        /// </summary>
        /// <param name="filePath">The path to the file that this population is saved in.</param>
        public void Save(string filePath)
        {
            FileStream s = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, this);
            s.Close();
        }
        #endregion
    }
        
}
