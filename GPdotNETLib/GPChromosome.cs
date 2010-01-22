using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    [Serializable]
    public class GPChromosome: IComparable<GPChromosome>
    {
        // expression tree structure, node value represent index of FunctionSet and Terminal set.
        //This aproach alows any representation of chromosome
        private GPTreeNode expressionTree;
        public GPTreeNode FunctionTree
        {
            get 
            {
                return expressionTree;
            }
            set
            {
                expressionTree = value;
            }
        }
       
        public float RSquare;
        /// <summary>
        /// Fitness property
        /// chromosome's fitness. Fitness is always float value. No meter which of type chromosem is.
        /// </summary>
        /// 
        private float fitness;
        public float Fitness
        {
            get { return fitness; }
            set { fitness = value; }
        }
        public ushort Levels
        {
            get 
            {
                ushort levels=1;
                CalculateLevel(expressionTree, ref levels);
                return levels;
            }
        }
        #region ctor
        /// <summary>
        /// Constructor
        /// </summary>
        public  GPChromosome()
        {
            fitness = -1;
            expressionTree = new GPTreeNode();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public GPChromosome(float value)
        {
            fitness = value;
            expressionTree = new GPTreeNode();
        }
        /// <summary>
        /// Copy constructor
        /// </summary>
        public GPChromosome(GPChromosome source)
        {
            expressionTree = new GPTreeNode(source.FunctionTree.Value, source.FunctionTree.Parent);
            fitness = source.Fitness;
        }
        /// <summary>
        /// Clone the chromosome
        /// </summary>
        public GPChromosome Clone()
        {
            GPChromosome clone = new GPChromosome(fitness);
            clone.expressionTree = (GPTreeNode)expressionTree.Clone();
            return clone;
        }
        #endregion
        #region Enumerators
        /// <summary>
        /// Get string representation of the chromosome. Return the chromosome
        /// in reverse polish notation (postfix notation).
        /// </summary>
        public override string ToString()
        {
            return FunctionTree.ToString();
        }

        public IEnumerable<GPTreeNode> NodeEnumeratorDepthFirst
        {
            get
            {
                foreach (var node in FunctionTree.NodeEnumeratorDepthFirst)
                    yield return node;
            }
        }

        public IEnumerable<ushort> NodeValueEnumeratorDepthFirst
        {
            get
            {
                foreach (var node in NodeEnumeratorDepthFirst)
                    yield return node.Value;
            }
        }
        public IEnumerable<GPTreeNode> NodeEnumeratorBreadthFirst
        {
            get
            {
                foreach (var node in FunctionTree.NodeEnumeratorBreadthFirst)
                    yield return node;
            }
        }

        public IEnumerable<ushort> NodeValueEnumeratorBreadthFirst
        {
            get
            {
                foreach (var node in NodeEnumeratorBreadthFirst)
                    yield return node.Value;
            }
        }
        #endregion
        #region IComparable Members
        /// <summary>
        /// Compare two chromosomes leads to compare their fitness 
        /// </summary>
        public int CompareTo(GPChromosome chromosome)
        {
            return (Fitness == chromosome.Fitness) ? 0 : (Fitness < chromosome.Fitness) ? 1 : -1;
        }

        #endregion

        private void CalculateLevel(GPTreeNode node,ref ushort depth, ushort counter = 1)
        {
            if (node.HasChildren)
            {
                counter++;
                if (depth < counter)
                    depth = counter;
                
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    CalculateLevel(node.Nodes[i], ref depth, counter);
                }
            }
        }
        public void Trim(int level)
        {
            Trim(FunctionTree,level);
        }
        public void Trim(GPTreeNode node,int level)
        {
            // if level is reach
            if (level == 1 || !node.HasChildren)
            {
                // remove offsprings
                if (node.HasChildren)
                    node.Nodes.Clear();

                // id node value is function change to terminal cause it is the last node
                if (node.Value >= 2000)
                    node.Value=GPPopulation.GPFunctionSet.GenerateGene(false);
            }
            else //if level is not reached
            {
                // go through all children
                for (int i = 0; i < node.Nodes.Count; i++)
                    Trim(node.Nodes[i], level - 1);
            }
        }
        public void GenerateChromosome(int level)
        {
            //Level have to be greater than 1
            if (level <= 1)
                throw new Exception("Chromosome level must be greater than 1!");
           
            //first node have to be a function node
            expressionTree.Value = GPPopulation.GPFunctionSet.GenerateGene(true);

            //Number of aritry of node function  
            int numAritry = GPPopulation.GPFunctionSet.functions[expressionTree.Value - 2000].Aritry;
            //Create childs equal as number of arytry 
            for (int i = 0; i < numAritry; i++)
            {
                // Formiraj potomak
                GPTreeNode child = new GPTreeNode(0, expressionTree);
                expressionTree.Nodes.Add(child);
                //Generiraj ga s tim da se nivo smanji za jedan
                GenerateExpressionTree(child, level - 1);
            }
           
        }

        public void GenerateExpressionTree(GPTreeNode node,int level)
        {

            if (level <= 1)
                node.Value = GPPopulation.GPFunctionSet.GenerateGene(false);
           else
            {
                //Check to see the method of how to generate chromosome 
                // If method is Ramped or Full - we must generate function node cause we are not at last level
                if (GPPopulation.GPParameters.einitializationMethod == EInitializationMethod.FullInitialization ||
                    GPPopulation.GPParameters.einitializationMethod == EInitializationMethod.HalfHalfInitialization)
                    node.Value = GPPopulation.GPFunctionSet.GenerateGene(true);//Generate function Node
                else//The grow method randomly generate chrmosome 
                    node.Value = GPPopulation.GPFunctionSet.GenerateGene();//Generate random Node
               
            }
            //If not is function
            if (node.Value > 1999)
            {
                //Number of aritry of node function  
                int numAritry = GPPopulation.GPFunctionSet.functions[node.Value - 2000].Aritry;
                //Svaki novonastali potomak inicijaliziraj i generiraj
                for (int i = 0; i < numAritry; i++)
                {
                    // Formiraj potomak
                    GPTreeNode child = new GPTreeNode(0, node);
                    node.Nodes.Add(child);
                    //Generiraj ga s tim da se nivo smanji za jedan
                    GenerateExpressionTree(child, level - 1);
                }
            }
            
        }
        public void GenerateRandomExpressionTree(GPTreeNode node, int level)
        {

            if (level <= 1)
                node.Value = GPPopulation.GPFunctionSet.GenerateGene(false);
            else
               node.Value = GPPopulation.GPFunctionSet.GenerateGene();//Generate random Node
            
            //If not is function
            if (node.Value > 1999)
            {
                //Number of aritry of node function  
                int numAritry = GPPopulation.GPFunctionSet.functions[node.Value - 2000].Aritry;
                //Svaki novonastali potomak inicijaliziraj i generiraj
                for (int i = 0; i < numAritry; i++)
                {
                    // Formiraj potomak
                    GPTreeNode child = new GPTreeNode(0, node);
                    node.Nodes.Add(child);
                    //Generiraj ga s tim da se nivo smanji za jedan
                    GenerateExpressionTree(child, level - 1);
                }
            }

        }
        
    }
}
