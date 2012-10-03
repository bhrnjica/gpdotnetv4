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


namespace GPdotNET.Core
{
    /// <summary>
    /// Base class for chromosome representation in tree. This class can use memory pooling,
    /// to reduce time for alocation and delocation memory. To use memory pooling use MEMORY_POOLING as a conditional compilation.
    /// </summary>
    public class GPNode
         #if MEMORY_POOLING
              :Pool<GPNode>
         #endif   
    {
       // public bool marked;
        public int value;
        public GPNode[] children;


        public StringBuilder ToStringBuilder()
        {
            StringBuilder sb = new StringBuilder();

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

                //Add value to string 
                sb.Append(node.value.ToString());

                if (node.children != null)
                    for (int i = node.children.Length-1;i >=0 ; i--)
                        dataTree.Push(node.children[i]);
            }
            return sb;

        }

        public List<int> ToList()
        {
            List<int> lst = new List<int>();

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

                //Add value to string 
                lst.Add(node.value);

                if (node.children != null)
                    for (int i = node.children.Length - 1; i >= 0; i--)
                        dataTree.Push(node.children[i]);
            }
            return lst;

        }

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
            Queue<GPNode> dataTree = new Queue<GPNode>();

            //current node
            GPNode node = null;


            //Add tail recursion
            dataTree.Enqueue(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();

                //count node
                count++;

                if (node.children != null)
                    for (int i = node.children.Length - 1; i >= 0; i--)
                        dataTree.Enqueue(node.children[i]);
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
            Queue<GPNode> dataTree = new Queue<GPNode>();

            //current node
            GPNode node = null;


            //Add tail recursion
            dataTree.Enqueue(this);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();

                //count node
                count++;

                //when the counter is equel to index return curretn node
                if (count == index)
                    return node;

                if (node.children != null)
                    for (int i = node.children.Length - 1; i >= 0; i--)
                        dataTree.Enqueue(node.children[i]);
            }

            return node;
        }


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
            Queue<GPNode> dataTree = new Queue<GPNode>();
            Queue<GPNode> cloneTree = new Queue<GPNode>();

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
                return Get();
#else
                return new GPNode();
#endif
        }

        public static void DestroyNode(ref GPNode node)
        {
#if MEMORY_POOLING
              node.value = -1;
              Free(node);
              node = null;
#endif
        }

        public static void DestroyNodes(GPNode[] nodes)
        {
            if (nodes == null)
                return;

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].children == null || nodes[i].children.Length == 0)
                    DestroyNode(ref nodes[i]);
                else
                {
                    DestroyNodes(nodes[i].children);
                    nodes[i].children = null;
                    DestroyNode(ref nodes[i]);
                }
            }
            return;
        }
    }

}
