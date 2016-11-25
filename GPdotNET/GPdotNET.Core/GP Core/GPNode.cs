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
using System.Text;
using System.Collections.Generic;
using System;
using System.Threading;


namespace GPdotNET.Core
{
    /// <summary>
    /// Base class for chromosome representation in tree. This class can use memory pooling,
    /// to reduce time for alocation and delocation memory. 
    /// To use memory pooling use MEMORY_POOLING as a conditional compilation.
    /// </summary>
    public class GPNode
         #if MEMORY_POOLING
              :Pool<GPNode>
         #endif   
    {
        //additional member for store additional info to Node.
        public bool marked;
        //main member
        public int value;
        public GPNode[] children;

        //Helper for handling with iteration throught tree structure
        private static ThreadLocal<Stack<GPNode>> _tl_stack = new ThreadLocal<Stack<GPNode>>(() =>
        {
            return new Stack<GPNode>(50);
        });
        //Helper for handling with iteration throught tree structure
        private static ThreadLocal<Queue<GPNode>> _tl_queue = new ThreadLocal<Queue<GPNode>>(() =>
        {
            return new Queue<GPNode>(50);
        });
        //Helper for handling with iteration throught tree structure
        private static ThreadLocal<Queue<GPNode>> _tl_clonequeue = new ThreadLocal<Queue<GPNode>>(() =>
        {
            return new Queue<GPNode>(50);
        });

        /// <summary>
        /// Helper method for converting to string
        /// </summary>
        /// <returns></returns>
        public StringBuilder ToStringBuilder()
        {
            StringBuilder sb = new StringBuilder();

            //Collection holds tree nodes
            _tl_stack.Value.Clear();
            Stack<GPNode> dataTree = _tl_stack.Value;

            //current node
            GPNode node = null;


            //Add tail recursion
            dataTree.Push(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                if (node.value == -1)
                    continue;

                //Add value to string 
                sb.Append(node.value.ToString());

                if (node.children != null)
                    for (int i = node.children.Length-1;i >=0 ; i--)
                        dataTree.Push(node.children[i]);
            }
            return sb;

        }

        /// <summary>
        /// Create list of node values
        /// </summary>
        /// <returns></returns>
        public List<int> ToList()
        {
            List<int> lst = new List<int>(30);
            //Collection holds tree nodes
            Stack<GPNode> dataTree = new Stack<GPNode>();

            //current node
            GPNode node = null;


            //Add tail recursion
            dataTree.Push(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                if (node.value == -1)
                    continue;
                //Add value to string 
                lst.Add(node.value);
                if (node.children != null)
                {
                    int count= node.children.Length - 1;
                    for (int i = count; i >= 0; i--)
                        dataTree.Push(node.children[i]);
                }
            }
            return lst;

        }
        /// <summary>
        /// Create list of node values
        /// </summary>
        /// <returns></returns>
        public void ToList(List<int> lst)
        {
            if (lst == null)
                throw new Exception("lst cannot be null!");

            //Collection holds tree nodes
            _tl_stack.Value.Clear();
            Stack<GPNode> dataTree = _tl_stack.Value;

            //current node
            GPNode node = null;
            int counter=0;

            //Add tail recursion
            dataTree.Push(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                if (node.value == -1)
                    continue;
                //Add value to string 
                if (lst.Count < counter+1)
                    lst.Add(node.value);
                else
                    lst[counter] = node.value;
                //increase counter
                counter++;

                if (node.children != null)
                {
                    int count = node.children.Length - 1;
                    for (int i = count; i >= 0; i--)
                        dataTree.Push(node.children[i]);
                }
            }
        }

        /// <summary>
        /// To string override  method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToStringBuilder().ToString();
        }

        /// <summary>
        /// Count nodes in Tree
        /// </summary>
        /// <returns></returns>
        public int NodeCount()
        {
            //start counter from 0
            int count = 0;

            //Collection holds tree nodes
            _tl_stack.Value.Clear();
            Stack<GPNode> dataTree = _tl_stack.Value;

            //current node
            GPNode node = null;


            //Add tail recursion
            dataTree.Push(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                //some empty node can apear because of memory pooling
                if (node.value == -1)
                    continue;

                //count node
                count++;

                if (node.children != null)
                    for (int i = node.children.Length - 1; i >= 0; i--)
                        dataTree.Push(node.children[i]);
            }

            return count;
        }

        /// <summary>
        /// Returns i th Node from the tree based on Depth-First search method
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GPNode NodeAt(int index)
        {
            //start counter from 0
            int count = 0;

            //Collection holds tree nodes
            _tl_queue.Value.Clear();
            Queue<GPNode> dataTree = _tl_queue.Value;

            //current node
            GPNode node = null;


            //Add tail recursion
            dataTree.Enqueue(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();

                if (node.value == -1)
                    continue;

                //count node
                count++;

                //when the counter is equel to index return curretn node
                if (count == index)
                    return node;

                if (node.children != null)
                    for (int i = 0; i < node.children.Length; i++)
                        dataTree.Enqueue(node.children[i]);
            }

            return node;
        }

        /// <summary>
        /// Returns level of certain node. Not implemented
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int LevelOfNode(int index)
        {
            return 0;
        }

        /// <summary>
        /// Make deep copy of the Node
        /// </summary>
        /// <returns></returns>
        public GPNode Clone()
        {
            var rootclone = GPNode.NewNode();

            //Collection holds tree nodes
            _tl_queue.Value.Clear();
            _tl_clonequeue.Value.Clear();

            Queue<GPNode> dataTree = _tl_queue.Value;
            Queue<GPNode> cloneTree = _tl_clonequeue.Value;

            //current node
            GPNode node = null;
            GPNode clone = null;


            //Add tail recursion
            dataTree.Enqueue(this);
            cloneTree.Enqueue(rootclone);
            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();
                clone = cloneTree.Dequeue();

                clone.value = node.value;
                clone.marked = node.marked;
                if (node.children != null)
                {
                    clone.children = new GPNode[node.children.Length];

                    for (int i = 0; i < node.children.Length; i++)
                    {
                        clone.children[i] = GPNode.NewNode();
                        dataTree.Enqueue(node.children[i]);
                        cloneTree.Enqueue(clone.children[i]);
                    }
                }
            }

            return rootclone;
        }

        /// <summary>
        /// Main method for creating the node. We need thin in order to make memory pool for GPNode
        /// </summary>
        /// <returns></returns>
        public static GPNode NewNode()
        {
#if MEMORY_POOLING
                var n= Get();
#else
                var n= new GPNode();
#endif
                n.value = -1;
                return n;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Generate(int param)
        {
            int levels = param;
            //Create the first node
            value = 0;

            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            GPNode node = null;


            //Add to list and ncrease index
            dataTree.Enqueue(this);

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
        }

        /// <summary>
        /// Memory poolong helper method for destroying to pool
        /// </summary>
        /// <param name="node"></param>
        public static void DestroyNode(GPNode node)
        {
#if MEMORY_POOLING
              node.value = -1;
              Free(node);
#endif
            node = null;

        }

        /// <summary>
        /// Memory poolong helper method for destroying to pool
        /// </summary>
        /// <param name="nodes"></param>
        public static void DestroyNodes(GPNode[] nodes)
        {
            if (nodes == null)
                return;

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].children == null || nodes[i].children.Length == 0)
                    DestroyNode(nodes[i]);
                else
                {
                    DestroyNodes(nodes[i].children);
                    nodes[i].children = null;
                    DestroyNode(nodes[i]);
                }
                nodes[i] = null;
            }
            return;
        }
    }

}
