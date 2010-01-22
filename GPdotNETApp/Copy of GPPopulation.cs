using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace FunctinTreeApp
{
    class GPPopulation
    {
        // random number generator
        private ThreadSafeRandom rand = new ThreadSafeRandom((int)DateTime.Now.Ticks);
        private GPFunctionSet gpFunctionSet;
        //Koristenje multyCode procesora u paralelno računanje
        public bool ParalelComputing { get; set; }
        //
        public List<GPChromosome> population = new List<GPChromosome>();
        private int popSize = 0;
        //vrijednosti funkcije max min, srednja
        public double fitnessMax = 0;
        public double fitnessSum = 0;
        public double fitnessAvg = 0;

        //Parameters of Genetic Programming
        private GPParameters gpParameters;
        //najbolji hromosom u populaciji
        private GPChromosome bestChromosome = null;

        public GPPopulation(int size, GPParameters parameters, GPFunctionSet functionSet, bool paralelComp)
        {
            ParalelComputing = paralelComp;
            //Duringpopulation creating the size must be grether than 1, 
            //and papameters must be non null
            if (size < 1)
                return;
            if (parameters == null)
                return;
            if (functionSet == null)
                return;
            //
            gpFunctionSet = functionSet;

            //Extend capasity three time cause crossover and mutation
            population.Capacity = 3 * size;
            gpParameters = parameters;
            //Creating starting Chromosome
            GPChromosome ancestor = new GPChromosome();

            //Initialize population
            InitPopulation(size);
        }

        private void InitPopulation(int size)
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
            popSize = population.Count;
        }

        private void GrowInitialization(int size)
        {
            //is the same as FillInit, in generate method 
            // chromosomes will be generated regarding initparametrs type
            FullInitialization(size);
        }

        private void FullInitialization(int size)
        {

            if (ParalelComputing)
            {
                Parallel.For(0, size, (int i) =>
                {
                    // kreiranje novog hromosoma
                    GPChromosome c = GenerateChromosome(gpParameters.maxInitLevel);
                    // izračunavanje funkcije cilja nad hromosomom
                    EvaluateChromosome(c);
                    // pridruzivanje populaciji
                    population.Add(c);
                }
                );
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    // kreiranje novog hromosoma
                    GPChromosome c = GenerateChromosome(gpParameters.maxInitLevel);
                    // izračunavanje funkcije cilja nad hromosomom
                    EvaluateChromosome(c);
                    // pridruzivanje populaciji
                    population.Add(c);
                }
            }
        }

        private void HalfHalfInitialization(int size)
        {
            //Inicjalna dubina drveta hroosoma
            int n = gpParameters.maxInitLevel;
            //Faktor ravnomjernog rasporeda dubine hromosoma
            int br = size / (gpParameters.maxInitLevel - 1);

            if (ParalelComputing)
            {
                Parallel.For(2, n + 1,
                    (int depth) =>
                    {
                        int jj = br;
                        if (depth == n)//kad i dodje do n onda uzima preostali dio populacije da je inicijalizira
                            jj = size - ((depth - 2) * br);

                        for (int j = 0; j < jj; j++)
                        {
                            // kreiranje novog hromosoma
                            GPChromosome c = GenerateChromosome(depth);
                            // izračunavanje funkcije cilja nad hromosomom
                            EvaluateChromosome(c);
                            // pridruzivanje populaciji
                            lock (population)
                            {
                                population.Add(c);
                            }
                        }
                    });
            }
            else
            {
                //Razvomjerna inicijalizacija hromosoma shodno dubini drveta
                for (int i = 2; i <= n; i++)
                {
                    if (i == n)//kad i dodje do n onda uzima preostali dio populacije da je inicijalizira
                        br = size - ((i - 2) * br);
                    //Promjena dubine drveta shodno metodi Inicijalizacije 
                    for (int j = 0; j < br; j++)
                    {
                        // kreiranje novog hromosoma
                        GPChromosome c = GenerateChromosome(i);
                        // izračunavanje funkcije cilja nad hromosomom
                        EvaluateChromosome(c);
                        // pridruzivanje populaciji
                        population.Add(c);
                    }
                }
            }
        }

        private void EvaluateChromosome(GPChromosome c)
        {
            System.Threading.Thread.Sleep(20);
            c.Fitness = 0;
        }

        private GPChromosome GenerateChromosome(int initLevel)
        {
            GPChromosome c = new GPChromosome();
            // Prvi cvor (korijen) generiramo kao funkciju
            GenerateGene(c.Root.NodeValue, true);
            c.Root.MaxLevel = initLevel - 1;
            GenerateChromosome(c.Root);
            return c;
        }

        private void GenerateChromosome(FunctionTree treeNode)
        {

            Queue<FunctionTree> que = new Queue<FunctionTree>();
            que.Enqueue(treeNode);

            while (que.Count > 0)
            {
                FunctionTree node = que.Dequeue();
                // ako je nivo dosegao nulu tada zavrsavamo generiranje i definisemo Terminal
                if (node.MaxLevel == 0)
                {
                    GenerateGene(node.NodeValue, false);
                    continue;
                }
                // Ako se nije doslo do nultog nivoa
                else
                {
                    //Ako nije maximalni novo onda treba postupiti shodno metodi inicijalizacije
                    if (gpParameters.einitializationMethod == EInitializationMethod.FullInitialization ||
                        gpParameters.einitializationMethod == EInitializationMethod.HalfHalfInitialization)
                        GenerateGene(node.NodeValue, true);
                    else//A ako je u pitanju rastuca slucajno generisi cvor
                        GenerateGene(node.NodeValue);
                }
                // Kada se je generirao Cvor (GEN) gornji kod, tada se treba generirati i 
                // potomci toga Gena. Odnosno generiraj onoliko potomaka koliko funkcija ima argumenata
                if (node.NodeValue.IsFunction)
                {
                    // formiranje potomaka - grana  korijena. Formira se onoliko koliko funkcija korijena ima argumenata
                    int numAritry = gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry;
                    //Formiraj dva numAritry cvorova koliko ima argumetata funkcija
                    node.SubFunctionTree = new List<FunctionTree>();

                    //Svaki novonastali potomak inicijaliziraj i generiraj
                    for (int i = 0; i < numAritry; i++)
                    {
                        // Formiraj potomak
                        FunctionTree child = new FunctionTree();
                        child.MaxLevel = node.MaxLevel - 1;
                        // Rekurzivno generiraj ga s tim da se nivo smanji za jedan
                        // Generate(child, treeNodeLevel - 1);
                        // Kad se generira pridruzi potomke njgovom rodtelju
                        node.SubFunctionTree.Add(child);
                        que.Enqueue(child);
                    }
                }

            }
        }
        private void GenerateGene(GPGenNode gPGenNode)
        {
            // potrebno je dati više vjerojatnost da se generiše funkcija u odnosu na terminale
            GenerateGene(gPGenNode, (rand.Next(4) != 3));
        }

        private void GenerateGene(GPGenNode gPGenNode, bool isFunction)
        {
            //Specifikacija vrste gena
            gPGenNode.IsFunction = isFunction;

            //Ako je broj terminala veći od 1000
            if (gpFunctionSet.terminals.Count > 999)
                throw new Exception("Maximum number of terminals must be less than 999");
            // Brojevi veci od 2000 oznacavat ce funkcije
            if (gPGenNode.IsFunction)
                gPGenNode.IndexValue = 2000 + rand.Next(gpFunctionSet.functions.Count);

            //Brojevi od 1000-2000 oznacavat ce slobodne koeficijente i ulazne parametre
            else
                //Slucajno biramo jedan od terminala
                gPGenNode.IndexValue = 1000 + rand.Next(gpFunctionSet.terminals.Count);
        }

        private void Crossover()
        {
            GC.Collect();
            if (gpParameters.probCrossover == 0)
                return;
            if (ParalelComputing)
            {
                Parallel.For(1, popSize,2, (int i) =>
                {
                    // dogadjanje ukrstanja shodno vjerojatnosti
                    if (rand.NextDouble() <= gpParameters.probCrossover)
                    {
                        // kloniranje oba hromosoma i priprema za ukrštanje
                        GPChromosome c1 = (population[i - 1]).Clone();
                        GPChromosome c2 = (population[i]).Clone();

                        // ukrštanje
                        CrossOverHromosomes(c1, c2);
                        // izračunavanje funkcije cilja nad potomcima
             //           EvaluateChromosome(c1);
             //           EvaluateChromosome(c2);

                        // pridruživanje dva nova potomka u populaciju
                        lock (population)
                        {
                            population.Add(c1);
                        }
                        lock (population)
                        {
                            population.Add(c2);
                        }

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
                        CrossOverHromosomes(c1, c2);
                        // izračunavanje funkcije cilja nad potomcima
            //            EvaluateChromosome(c1);
            //            EvaluateChromosome(c2);

                        // pridruživanje dva nova potomka u populaciju
                        population.Add(c1);
                        population.Add(c2);

                    }
                }
            }
            
        }

        private void CrossOverHromosomes(GPChromosome c1, GPChromosome c2)
        {
            GPChromosome p1 = c1;
            GPChromosome p2 = c2;

            // provjera da li hromosoma koji se ukrsta sa ovim nije null
            if (c2 != null)
            {
                // ako root nema potomaka ili ako se generira 0 tada je tacka ukrstanja root
                if ((c1.Root.SubFunctionTree == null) || (rand.Next(gpParameters.maxCossoverLevel) == 0))
                    // izvrsi zamjenu genetskog materijala
                 //   c1.Root = c2.RandomSwap(c1.Root);
                    RandomSwap(c1.Root,c2.Root);
                else // ako je slucajni generiran broj veci od nule
                {
                    FunctionTree node = c1.Root;// root u privremenu varijablu od koje se bira slucajno potomak

                    for (; ; )
                    {
                        // slucajno izabrani broj 
                        int r = rand.Next(gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry);

                        //Slucajno izabrani potomak
                        FunctionTree child = node.SubFunctionTree[r];

                        //Ako slucajno izabrani potomak nema potomaka onda se on ukrsta ili ako se generise slucajno broj 0
                        if ((child.SubFunctionTree == null) || (rand.Next(gpParameters.maxCossoverLevel) == 0))
                        {
                            // izvrsi zamjenu genetskog materijala
                            //node.SubFunctionTree[r] = c2.RandomSwap(child);
                            RandomSwap(node.SubFunctionTree[r], c2.Root);
                            break;//prekini petlju jer je ukrstanje napravljeno
                        }

                        // Udju u potomke potomka
                        node = child;
                    }
                }
                // Na kraju poravnati dubinu na ogranicenu ako je doslo do povecaja dubine
                c1.Root.MaxLevel = gpParameters.maxCossoverLevel;
                c2.Root.MaxLevel = gpParameters.maxCossoverLevel;
                Trim(c1.Root);
                Trim(c2.Root);

                Debug.Assert(c1.Root.MaxLevel <= gpParameters.maxCossoverLevel);
                Debug.Assert(c2.Root.MaxLevel <= gpParameters.maxCossoverLevel);

            }
            //Ako je hromosom null tada se operacija ne desava jer se nema sta ukrstati sa null
        }

        private void Trim(FunctionTree geneNode)
        {
          //  int level = gpParameters.maxCossoverLevel;
            Queue<FunctionTree> que = new Queue<FunctionTree>();
            que.Enqueue(geneNode);
            while (que.Count > 0)
            {
                FunctionTree node = que.Dequeue();
                // ako node ima potomke
                if (node.SubFunctionTree != null)
                {
                    // i ako je nivo 0 tada 
                    if (node.MaxLevel == 1)
                    {
                        // odstrani potomke
                        node.SubFunctionTree.Clear();
                        //postavi null za potimke da se delocira memorija
                        node.SubFunctionTree = null;
                        // a cvor generiraj kao terminal
                        GenerateGene(node.NodeValue, false);
                    }
                    else //Ako nivo nije 0 tada udji u novo za 1 manji
                    {
                        // prolazi kroz potomke sa novoom za 1 manjim
                        int level = node.MaxLevel - 1;
                        for (int i = 0; i < node.SubFunctionTree.Count; i++)
                        {
                            node.SubFunctionTree[i].MaxLevel = level;
                            que.Enqueue(node.SubFunctionTree[i]);
                            // Trim(node.SubFunctionTree[i], level - 1);
                        }
                    }
                }
            }
        }

        private void RandomSwap(FunctionTree c1, FunctionTree c2)
        {
            FunctionTree tempNode = null;

            // ako root nema potomaka ili ako se slucalnogenerise nivo za ukrstanje 0, tada se root ukrsta sa sourceom
            if ((c1.SubFunctionTree == null) || (rand.Next(gpParameters.maxCossoverLevel) == 0))
            {
                // zamjena roota i sourcea. 
                // tempNode je privremeno da se moze izvrsiti zamjena
                tempNode = c1;
                c1 = c2;
                c2 = tempNode;
            }
            else
            {
                //privremena varijabla
                FunctionTree node = c1;

                for (; ; )
                {
                    // slucajno izabiremo potomak od node-a
                    int r = rand.Next(gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry);
                    //pohranjujemo potomak u varijablu child i sad od njega poslije trazimo tacku ukrstanja
                    FunctionTree child = node.SubFunctionTree[r];

                    // swap the random node, if it is an end node or
                    // random generator "selected" this node
                    //Ako child nema potomaka tada tj ako je terminal tada je to tacka zamjene
                    if ((child.SubFunctionTree == null) || (rand.Next(gpParameters.maxCossoverLevel) == 0))
                    {
                        // zamjena cvorova
                        tempNode = child;
                        node.SubFunctionTree[r] = c2;
                        c2 = tempNode;
                        break;
                    }

                    //Ako child ima potomke tada se vracamo na pocetak i 
                    //ponovo slucajno biramo potomak sad od child-a  
                    node = child;
                }

            }
        }

        private void Mutate()
        {
            //Ako je mutacija 0% ne ulazi u petlju
            if (gpParameters.probMutation == 0)
                return;
            if (ParalelComputing)
            {
                Parallel.For(0, popSize, (int i) =>
                {
                    // generiranje slučajnog broja i provjera vjerojatnosti mutacije
                    if (rand.NextDouble() <= gpParameters.probMutation)
                    {
                        // kreiranje novog hromosoma
                        // kloniranje  hromosoma i priprema za mutiranje
                        GPChromosome c = (population[i]).Clone();

                        // mutacija
                        MutateChromosome(c);

                        // izračunavanje funkcije cilja nad mutantom
        //                EvaluateChromosome(c);

                        // pridruživanje mutanta u populaciju
                        lock (population)
                        {
                            population.Add(c);
                        }

                    }
                }
                );
            }
            else
            {
                for (int i = 0; i < popSize; i++)
                {
                    // generiranje slučajnog broja i provjera vjerojatnosti mutacije
                    if (rand.NextDouble() <= gpParameters.probMutation)
                    {
                        // kreiranje novog hromosoma
                        // kloniranje  hromosoma i priprema za mutiranje
                        GPChromosome c = (population[i]).Clone();

                        // mutacija
                        MutateChromosome(c);

                        // izračunavanje funkcije cilja nad mutantom
               //         EvaluateChromosome(c);

                        // pridruživanje mutanta u populaciju
                        population.Add(c);

                    }
                }
            }
            
        }

        private void MutateChromosome(GPChromosome c)
        {
            //Refeentni nivo
            int currentLevel = 0;
            //Maximalni nivo mutacija. -1 ide zbog brojanja od nule
            int maxLevel = gpParameters.maxMutationLevel - 1;
            // current node
            FunctionTree node = c.Root;

            for (; ; )
            {
                // Ako je node bez potomaka onda treba generiratido nivoa maximalnog za mutaciju
                if (node.SubFunctionTree == null)
                {
                    //Ako je tekuci nivo dostigao maximalni nivo mutacije formiraj Terminal
                    if (currentLevel >= maxLevel)
                        GenerateGene(node.NodeValue, false);

                    else //Ako je tekuci nivo manji od maximalnog onda generiraj dalje
                    // generate subtree
                    {
                        node.MaxLevel = rand.Next(maxLevel - currentLevel);
                        GenerateChromosome(node);
                    }
                    break;
                }

                //Ovdje pocinjemo proces mutacije. Slucajno generisemo tačku mutacije. Odnosno generiramo slucajnog 
                // potomka. Koji ce mutirati ili ce mutirati njegov potomak
                int r = rand.Next(gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry + 1);


                //Ako smo generirali broj koji je isti kao i broj potomaka argumenata funkcija tada je odluka da taj cvor mutira
                //Ovo mi je malo bezvez nacin odluke koji ce cvor mutirati jer je u glavnom broj argumenata 2 tako da vec prvi cvorovi 
                // najvjerojatnije da ce mutirati
                if (r == gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry)
                {
                    // Kada smo odabrali da ce doticni cvor da mutira 
                    // ponovo ga regegeneriramo. 
                    if (currentLevel >= maxLevel)//Ako je level veci ili jedna maximalnom levelu tada moramo generirati terminal
                        GenerateGene(node.NodeValue, false);
                    else
                        GenerateGene(node.NodeValue);

                    // Ako smo regenerirani cvor postao terminal tada njegovi potomci 
                    // trebaju biti odbacenu
                    if (node.NodeValue.IsFunction == false)// || currentLevel >= maxLevel)
                        node.SubFunctionTree = null;

                    else//Ako je novogenerirani (mutirani cvor ) funkcija tada dalje regeneriramo hromosom
                    {
                        // ako mutirani cvor ne posjeduje potomke
                        // potrebno ih je formirati
                        int count = 0;
                        if (node.SubFunctionTree == null)
                        {
                            //I to onoliko koliko ima argumanata mutirani cvor
                            count = gpFunctionSet.functions[node.NodeValue.IndexValue - 2000].Aritry;
                            node.SubFunctionTree = new List<FunctionTree>();
                        }
                        else
                            count = node.SubFunctionTree.Count;


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
                                    child.MaxLevel = rand.Next(maxLevel - currentLevel);
                                    GenerateChromosome(child);
                                    // add the new child
                                    node.SubFunctionTree[i] = child;
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

            Debug.Assert(c.Root.MaxLevel <= gpParameters.maxMutationLevel);
        }

        /// <summary>
        /// Vršenje selekcije u populaciji 
        /// </summary>
        public void Selection()
        {
            // na osnovu vjerojatnosti selekcije izračunamo broj hromosoma koji prezivljavaju 
            // u narednu generaciju npr ako je 5% vjerojatnost Selekcije tada ako je 100 velicina populacije 
            // tada 5 hromosoma prelazi u novu generaciju
            int randomAmount = (int)(gpParameters.probReproduction * popSize);

            // vrsenje selekcije odredjenom metodom
            switch (gpParameters.eselectionMethod)
            {
                case ESelectionMethod.EliteSelection:
                    EliteSelection(popSize - randomAmount);
                    break;
                case ESelectionMethod.Rankselection:
                    RankSelection(popSize - randomAmount);
                    break;
                case ESelectionMethod.RouletteWheelSelection:
                    RouletteWheelSelection(popSize - randomAmount);
                    break;
                default:
                    EliteSelection(popSize - randomAmount);
                    break;
            }
            if (randomAmount > 0)
                InitPopulation(randomAmount);

            //Calculate population statistic parameters
            CalculatePopulation();

            Debug.Assert(population.Count==popSize);

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
                foreach (GPChromosome c in population)
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

                    // izračuvavanje optimalne  vrijednosti
                    if (fitness > fitnessMax)
                    {
                        fitnessMax = fitness;
                        bestChromosome = population[i];
                    }
                }
                fitnessAvg = fitnessSum / popSize;
            }
        }
        private void EliteSelection(int size)
        {
            int currentSize = population.Count;

            if (ParalelComputing)
            {
                List<GPChromosome> lst = population.AsParallel().OrderBy(x => x.Fitness).Take(size).ToList();
                population.Clear();
                population= null;
                population = lst;
            }
            else
            {
                // sort population
                population.Sort();
                // remove bad population
                population.RemoveRange(size, currentSize - size);
            }
     /*       // half of popuation size
            int n = size / 2;
            //Shuffle population
            int index1, index2;
            GPChromosome temp = null;
            for (int i = 0; i < n; i++)
            {
                index1 = rand.Next(size);
                index2 = rand.Next(size);

                // swap two population
                temp = population[index1];
                population[index1] = population[index2];
                population[index2] = temp;

                temp = null;
            }*/
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
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new population
                        newPopulation.Add(population[i].Clone());
                        break;
                    }
                }
            }

            // empty current population
            population.Clear();
            // move elements from new to current population
            // !!! moving is done to reduce objects cloning
            population=newPopulation;
        }
        private void RouletteWheelSelection(int size)
        {
            // new population, initially empty
            List<GPChromosome> newPopulation = new List<GPChromosome>();
            // velicinaPopulacije of current population
            int currentSize = population.Count;

            // calculate summary fitness of current population
            double fitnessSum = 0;
            for (int i = 0; i < currentSize; i++)
            {
                fitnessSum += population[i].Fitness;
            }

            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;
            int k = 0;

            for (int r = 0; r < currentSize; r++)
            {
                // cumulative normalized fitness
                s += (population[r].Fitness / fitnessSum);
                rangeMax[k++] = s;
            }

            // select population from old population to the new population
            for (int j = 0; j < size; j++)
            {
                // get wheel value
                double wheelValue = rand.NextDouble();
                // find the chromosome for the wheel value
                for (int ii = 0; ii < currentSize; ii++)
                {
                    if (wheelValue > rangeMax[ii])
                    {
                        // add the chromosome to the new population
                        newPopulation.Add(population[ii].Clone());
                        break;
                    }
                }
            }

            // empty current population
            // move elements from new to current population
            population.Clear();
            population = newPopulation;

        }
        public void StartEvolution(int i)
        {
            Console.WriteLine("----POČETAK----");
            DateTime pocetak, zavrsetak;
            TimeSpan ukupno;
            pocetak = DateTime.Now;
            Crossover();
            zavrsetak = DateTime.Now;
            ukupno = zavrsetak - pocetak;
            Console.WriteLine("Ukrštanje:{0} traje: {1} sec",i.ToString(),ukupno.TotalSeconds.ToString());

            pocetak = DateTime.Now;
            Mutate();
            zavrsetak = DateTime.Now;
            ukupno = zavrsetak - pocetak;
            Console.WriteLine("Mutacija:{0} traje: {1} sec", i.ToString(), ukupno.TotalSeconds.ToString());

            pocetak = DateTime.Now;
            int count = population.Count;
            if (this.ParalelComputing)
            {
                
                Parallel.For(0, count, (cc) =>
                    {
                        EvaluateChromosome(population[cc]);
                    });
            }
            else
            {
                for(int jj =0;jj<count;jj++)
                    EvaluateChromosome(population[jj]);
            }
            Selection();
            zavrsetak = DateTime.Now;
            ukupno = zavrsetak - pocetak;
            Console.WriteLine("Selekcija:{0} traje: {1} sec", i.ToString(), ukupno.TotalSeconds.ToString());

            pocetak = DateTime.Now;
            GC.Collect();
            zavrsetak = DateTime.Now;
            ukupno = zavrsetak - pocetak;
            Console.WriteLine("GCCOLLECT:{0} traje: {1} sec", i.ToString(), ukupno.TotalSeconds.ToString());
            Console.WriteLine("----KRAJ----");
        }
        public void StartEvolutionP()
        {
            ParalelComputing = false;
            Parallel.Invoke(
           ()=> Crossover(),
           () =>  Mutate());
            ParalelComputing = true;
            Selection();
            GC.Collect();
        }


    }
        
}
