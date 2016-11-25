//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2013 Bahrudin Hrnjica                                                 //
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
using System.Globalization;
using GPdotNET.Core;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Class implement functionalities of GP tree based chromosome
    /// </summary>
    public class GPChromosome
#if MEMORY_POOLING
                : Pool<GPChromosome>,IChromosome
#else
 : IChromosome
#endif
    {
        #region Fields
        public static int MaxOperationLevel = 15;
        //Fitness valu for the chromosome
        private float fitness;
        public float Fitness
        {
            get
            {
                return fitness;
            }
            set
            {
                fitness = value;
            }
        }

        //expression representation of the chromsome
        public GPNode expressionTree;
        #endregion

        #region Ctor and initialisation
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GPChromosome()
        {
            expressionTree = null;
            fitness = float.MinValue;
        }

        /// <summary>
        /// Create deep copy of the chromoseme
        /// </summary>
        /// <returns></returns>
        public IChromosome Clone()
        {
            var ch = GPChromosome.NewChromosome();
            ch.fitness = this.fitness;
            ch.expressionTree = this.expressionTree.Clone();
            return ch;
        }

        /// <summary>
        /// Generate random chromosome
        /// </summary>
        public void Generate(int param=0)
        {
            //int levels = param;
            //Create the first node
            this.expressionTree = GPNode.NewNode();
            expressionTree.Generate(param);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Mutate()
        {
            int index1 = GetRandomNode(expressionTree.NodeCount());
            ApplyMutate(expressionTree, index1, MaxOperationLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch2"></param>
        public void Crossover(IChromosome ch2, int index1 = -1, int index2 = -1)
        {
            ApplyCrossover(this, (GPChromosome)ch2, index1,index2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function"></param>
        public void Evaluate(IFitnessFunction function)
        {
            fitness = function.Evaluate(this,Globals.functions);
        }

        #endregion

        #region Operations
        /// <summary>
        /// Returns expresssion tree of GPChromosome
        /// </summary>
        /// <returns></returns>
        public GPNode GetExpression()
        {
            return expressionTree;
        }

        /// <summary>
        /// Generate Tree structure for chromosome representation
        /// </summary>
        /// <param name="levels"></param>
        /// <returns></returns>
        static public GPNode GenerateTreeStructure1(int levels)
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
                    node.value = Globals.GenerateNodeValue(false);

                //Node children generatio  
                if (node.value > 1999)
                {
                    int aritry = Globals.GetFunctionAritry(node.value);
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

        internal void ApplyMutate(GPNode root, int index1, int maxLevels)
        {
            //We dont want to mutate root
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
                        var node = GPNode.NewNode();
                        node.Generate(rnd);
                        tuplenode.Item2.value = node.value;
                        tuplenode.Item2.children = node.children;
                    }
                    break;
                }

                if (tuplenode.Item2.children != null)
                    for (int i =0; i< tuplenode.Item2.children.Length; i++)
                        dataTree.Enqueue(new Tuple<int, GPNode>(tuplenode.Item1 + 1, tuplenode.Item2.children[i]));

            }
        }


        private static void ApplyCrossover(GPChromosome ch1, GPChromosome ch2, int index1 = -1, int index2 = -1)
        {
            
            //Get random numbers between 0 and maximum index
            if(index1==-1)
                index1 = GetRandomNode(ch1.expressionTree.NodeCount());
            if (index2 == -1)
                index2 = GetRandomNode(ch2.expressionTree.NodeCount());

            //Exchange the geene material
            Crossover(ch1.expressionTree, ch2.expressionTree, index1, index2);


            //if some tree exceed the levels trim it
            ch1.Trim(MaxOperationLevel);
            ch2.Trim(MaxOperationLevel);

        }

        /// <summary>
        /// We have to implement smarter algorithm for selecting nodes. Since order of nodes in the container 
        /// is so that the first nodes are functions and then terminals, about half list contains terminal, 
        /// and we have to provide better probability for selecting function insead of terminal
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetRandomNode(int nodeCout)
        {
            if (nodeCout < 2)
                throw new Exception("Invalid number of chromosoem nodes.");
            //TODO:get more inteligent random chooser
            return Globals.radn.Next(2, nodeCout + 1);
        }


        internal static void Crossover(GPNode ch1, GPNode ch2, int index1, int index2)
        {
            //We dont want to crossover root
            if (index1 == 1 || index2 == 1)
                throw new Exception("Wrong index number for Crossover operation!");

            //start counter from 0
            int count = 0;
            //create parss for echange
            var part1=ch1.NodeAt(index1).Clone();
            var part2= ch2.NodeAt(index2).Clone();

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
                {
                    node.value = part2.value;
                    node.marked = part2.marked;
                    GPNode.DestroyNodes(node.children);
                    node.children = part2.children;
                    break;
                }

                if (node.children != null)
                    for (int i =0; i< node.children.Length; i++)
                        dataTree.Enqueue(node.children[i]);

            }

            //repeat tail recursion
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
                {
                    node.value = part1.value;
                    node.marked = part1.marked;
                    GPNode.DestroyNodes(node.children);
                    node.children = part1.children;
                    break;
                }

                if (node.children != null)
                    for (int i = 0; i < node.children.Length; i++)
                        dataTree.Enqueue(node.children[i]);
            }
        }

        /// <summary>
        /// We need to have trees with proper levels, so every level with greater than maxOperationLevel wil be trimmed.
        /// First  change functionNode to Terminal at maxumum level, then remove all node which are grater tha maxOparationLevel
        /// </summary>
        /// <param name="maxLevel"></param>
        internal void Trim(int maxLevel)
        {
            //Collection holds tree nodes
            Queue<Tuple<int, GPNode>> dataTree = new Queue<Tuple<int, GPNode>>();

            //current node
            Tuple<int, GPNode> tuplenode = null;


            //Add tail recursion
            dataTree.Enqueue(new Tuple<int, GPNode>(0, this.expressionTree));

            while (dataTree.Count > 0)
            {
                //get next node
                tuplenode = dataTree.Dequeue();

                //if level exceed maximu level caut off children
                if (tuplenode.Item1 >= maxLevel)
                {
                    if (tuplenode.Item2.children != null)
                    {
                        GPNode.DestroyNodes(tuplenode.Item2.children);
                        tuplenode.Item2.children = null;
                        tuplenode.Item2.value = Globals.GenerateNodeValue(false);
                    }
                }
                else
                {
                    if (tuplenode.Item2.children != null)
                        for (int i =0; i< tuplenode.Item2.children.Length; i++)
                            dataTree.Enqueue(new Tuple<int, GPNode>(tuplenode.Item1 + 1, tuplenode.Item2.children[i]));
                }
            }

            return;
        }

        /// <summary>
        /// Get copy of all nodes  where GPNode[index] is newParentIndex node 
        /// </summary>
        /// <param name="index">Index of the newParentIndex node</param>
        /// <returns> Subtree with nodesToMerge</returns>
        internal GPNode Subtree(int index)
        {
            return expressionTree.NodeAt(index);
        }

        /// <summary>
        /// STring representation for the chromosome. It is also used for serializing to txt file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0};{1}", fitness.ToString(CultureInfo.InvariantCulture), expressionTree.ToString());
        }

        /// <summary>
        /// Creates chromosome from string. we need to have terminal prior to create chromosome
        /// </summary>
        /// <param name="strCromosome"></param>
        /// <returns></returns>
        public IChromosome FromString(string strCromosome)
        {
            return GPChromosome.CreateFromString(strCromosome);
        }
        /// <summary>
        /// Create chromosome from string. Chromosome string is stored with the folowing format.
        /// 10.34566;20012005100420032....
        /// fitness;NodeValueNodeValue....
        /// </summary>
        /// <param name="strCromosome">string containing chromosome data</param>
        /// <returns></returns>
        public static GPChromosome CreateFromString(string strCromosome)
        {
            GPChromosome ch = GPChromosome.NewChromosome();
            var items = strCromosome.Replace("\r","").Split(';');

            //Fisrt item is Fitness. Fitness value must always be formated with POINT not COMMA
            float fitness = 0;
            if (!float.TryParse(items[0].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out fitness))
                fitness = 0;
            ch.fitness = fitness;

            int index = 0;
            ch.expressionTree = GPNode.NewNode();

            //string length must be devided by 4
            if (items[1].Length % 4 != 0)
                throw new Exception("Chromosome string length is not correct!");

            //Collection holds tree nodes
            Stack<GPNode> dataTree = new Stack<GPNode>();

            //current node
            GPNode node = null;

            //Add tail recursion
            dataTree.Push(ch.expressionTree);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                //Extract value from string
                int value;
                if (items[1].Length >= index + 4)
                {
                    var str = items[1].Substring(index, 4);
                    if (!int.TryParse(str, out value))
                        value = 0;
                    index += 4;
                    node.value = value;

                    //check if node if function node
                    if (Globals.IsFunction(node.value))
                    {

                        int aritry = Globals.GetFunctionAritry(node.value);

                        node.children = new GPNode[aritry];
                        for (int i = aritry-1; i>=  0; i--)
                        {
                            node.children[i] = GPNode.NewNode();
                            dataTree.Push(node.children[i]);

                        }
                    }
                }
            }

            return ch;
        }
        

        public int CompareTo(IChromosome other)
        {
            if (other == null)
                return 1;
            return other.Fitness.CompareTo(this.Fitness);
        }

        #endregion

#region Memory Pool
        /// <summary>
        /// Main method for creating the node. We need thin in order to make memory pool for GPNode
        /// </summary>
        /// <returns></returns>
        public static GPChromosome NewChromosome()
        {
#if MEMORY_POOLING
                var ch= Get();
                ch.fitness = float.MinValue;
                return ch;
#else
            return new GPChromosome();
#endif
        }
        /// <summary>
        /// Helper for backup unneccesery chromosomes
        /// </summary>
        public void Destroy()
        {
#if MEMORY_POOLING
            if (this != null)
            {
                this.fitness = float.MinValue;
                Free(this);
            }
#endif
        }

#endregion

    }
}
