/* This class is part of Code project article: A Graph Tree Drawing Control for WPF,  witten by darrellp | 23 Feb 2009 
 * Full link article http://www.codeproject.com/Articles/29518/A-Graph-Tree-Drawing-Control-for-WPF
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GPdotNET.Tool.Common
{
	public class TreeNodeGroup : IEnumerable<ITreeNode>
	{
		Collection<ITreeNode> _col = new Collection<ITreeNode>();

		public int Count
		{
			get
			{
				return _col.Count;
			}
		}

		public ITreeNode this[int index]
		{
			get { return _col[index]; }
		}

		public void Add(ITreeNode tn)
		{
			_col.Add(tn);
		}

        public void Remove(ITreeNode tn)
        {
            _col.Remove(tn);
        }

		internal ITreeNode LeftMost()
		{
			return _col.First();
		}

		internal ITreeNode RightMost()
		{
			return _col.Last();
		}

		#region IEnumerable<IGraphNode> Members

		public IEnumerator<ITreeNode> GetEnumerator()
		{
			return _col.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _col.GetEnumerator();
		}

		#endregion
	}
}
