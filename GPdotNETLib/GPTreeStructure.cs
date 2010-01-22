using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GPdotNETLib
{

    #region GPTreeNode
    /// <summary>
    /// Represents a single Tree Node
    /// </summary>
    /// <typeparam name="T">Type of the Data Value at the node</typeparam>
    [Serializable]
    public class GPTreeNode : ICloneable
    {
        #region Node Data
        private ushort mValue;
        private GPTreeNode mParent = null;
        private List<GPTreeNode> mNodes = null;
        #endregion // Node Data

        #region CTORs

        public GPTreeNode()
        {
            mValue = ushort.MaxValue;
            mParent=null;
        }

        /// <summary>
        /// creates a new root node, and sets Value to value.
        /// </summary>
        /// <param name="value"></param>
        public GPTreeNode(ushort value)
        {
            mValue = value;
            mParent=null;
        }

        /// <summary>
        /// Creates a new node as child of parent, and sets Value to value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        internal GPTreeNode(ushort value, GPTreeNode parent)
        {
            mValue = value;
            mParent=parent;
        }
        #endregion // CTORs
        #region ICloneable Members
        public object Clone()
        {
            GPTreeNode clone = new GPTreeNode(Value,null);
            if (mNodes != null)
            {
                foreach (var node in mNodes)
                    clone.Nodes.Add((GPTreeNode)node.CloneWithParent(clone));
            }
            return clone;
        }
        private GPTreeNode CloneWithParent(GPTreeNode parent)
        {
            GPTreeNode clone= new GPTreeNode(mValue,parent);
            if (mNodes != null)
            {
                foreach (var node in mNodes)
                    clone.Nodes.Add((GPTreeNode)node.CloneWithParent(clone));
            }
            return clone;
        }
        #endregion
        #region Data Access
       /// <summary>The Value of the Node</summary>
        public ushort Value
        {
            get
            {
                
                return mValue;
            }
            set
            {
                mValue = value;
                
            }
        }
        #endregion // Data Access

        #region Navigation

        /// <summary>returns the parent node, or null if this is a root node</summary>
        public GPTreeNode Parent { get { return mParent; } }

        /// <summary>
        /// returns all child nodes as a NodeList<T>. 
        /// <para><b>Implementation note:</b> Childs always returns a non-null collection. 
        /// This collection is created on demand at the first access. To avoid unnecessary 
        /// creation of the collection, use HasChilds to check if the node has any child nodes</para>
        /// </summary>
        public List<GPTreeNode> Nodes
        {
            get
            {
                if (mNodes == null)
                    mNodes = new List<GPTreeNode>();
                return mNodes;
            }
        }

        /// <summary>
        /// returns true if the node has child nodes.
        /// See also Implementation Note under this.Childs
        /// </summary>
        public bool HasChildren { get { return mNodes != null && mNodes.Count != 0; } }

        /// <summary>
        /// returns true if this node is a root node. (Equivalent to this.Parent==null)
        /// </summary>
        public bool IsRoot { get { return mParent == null; } }

        public bool IsAncestorOf(GPTreeNode node)
        {
            GPTreeNode parent = node.Parent;
            while (parent != null && parent != this)
                parent = parent.Parent;
            return parent != null;
        }

        public bool IsChildOf(GPTreeNode node)
        {
            return !IsAncestorOf(node);
        }

        public bool IsInLineWith(GPTreeNode node)
        {
            return node == this ||
                   node.IsAncestorOf(this) ||
                   node.IsChildOf(node);
        }

        public int Depth
        {
            get
            {
                int depth = 0;
                GPTreeNode node = mParent;
                while (node != null)
                {
                    ++depth;
                    node = node.mParent;
                }
                return depth;
            }
        }

        #endregion // Navigation

        #region Node Path

        public GPTreeNode GetNodeAt(int index)
        {
            return Nodes[index];
        }

        public GPTreeNode GetNodeAt(IEnumerable<int> index)
        {
            GPTreeNode node = this;
            foreach (int elementIndex in index)
            {
                node = node.Nodes[elementIndex];
            }
            return node;
        }

        public GPTreeNode GetNodeAt(params int[] index)
        {
            return GetNodeAt(index as IEnumerable<int>);
        }

        public int[] GetIndexPathTo(GPTreeNode node)
        {
            List<int> index = new List<int>();
            while (node != this && node.mParent != null)
            {
                index.Add(node.mParent.Nodes.IndexOf(node));
                node = node.mParent;
            }

            if (node != this)
                throw new ArgumentException("node is not a child of this");

            index.Reverse();
            return index.ToArray();
        }

        public System.Collections.IList GetNodePath()
        {
            List<GPTreeNode> list = new List<GPTreeNode>();
            GPTreeNode node = mParent;

            while (node != null)
            {
                list.Add(node);
                node = node.Parent;
            }
            list.Reverse();
            list.Add(this);

            return list;
        }

        public IList<ushort> GetElementPath()
        {
            List<ushort> list = new List<ushort>();
            GPTreeNode node = mParent;

            while (node != null)
            {
                list.Add(node.Value);
                node = node.Parent;
            }
            list.Reverse();
            list.Add(this.mValue);

            return list;
        }
        #endregion // Node Path

        #region Modify
        /// <summary>
        /// Removes the current node and all child nodes recursively from it's parent.
        /// Throws an InvalidOperationException if this is a root node.
        /// </summary>
        public void Remove()
        {
            if (mParent == null)
                throw new InvalidOperationException("cannot remove root node");
            Detach();
        }

        /// <summary>
        /// Detaches this node from it's parent. 
        /// Postcondition: this is a root node.
        /// </summary>
        /// <returns></returns>
        public GPTreeNode Detach()
        {
            if (mParent != null)
                Siblings.Remove(this);
            return this;
        }
        public List<GPTreeNode> Siblings
        {
            get { return mParent != null ? mParent.Nodes : null; }
        }
        #endregion // Modify

        #region Enumerators
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (mNodes != null)
                foreach (var child in mNodes)
                    sb.Append(child.ToString());

            // dodavanja vrijednosti genu
            sb.Append(mValue.ToString());
            sb.Append(";");
            return sb.ToString();
        }
        internal IEnumerable<GPTreeNode> NodeEnumeratorBreadthFirst
        {
            get
            {
                Queue<GPTreeNode> todo = new Queue<GPTreeNode>();
                todo.Enqueue(this);
                while (0 < todo.Count)
                {
                    GPTreeNode node = todo.Dequeue();
                    if (node.mNodes != null)
                    {
                        foreach (GPTreeNode child in node.mNodes)
                            todo.Enqueue(child);
                    }
                    yield return node;
                }
            }
        }
        internal IEnumerable<ushort> NodeValueEnumeratorBreadthFirst
        {
            get
            {
                foreach (var node in NodeEnumeratorBreadthFirst)
                    yield return node.Value;
            }
        }
        internal IEnumerable<GPTreeNode> NodeEnumeratorDepthFirst
        {
            get
            {
                if (mNodes != null)
                {
                    foreach (GPTreeNode child in mNodes)
                    {
                        IEnumerator<GPTreeNode> childEnum = child.NodeEnumeratorDepthFirst.GetEnumerator();
                        while (childEnum.MoveNext())
                            yield return childEnum.Current;
                    }
                }
                yield return this;
            }
        }

        internal IEnumerable<ushort> NodeValueEnumeratorDepthFirst
        {
            get
            {
                foreach (var node in NodeEnumeratorDepthFirst)
                    yield return node.Value;
            }
        }
        #endregion

    }

    #endregion // GPTreeNode
} // GPdotNETLib

