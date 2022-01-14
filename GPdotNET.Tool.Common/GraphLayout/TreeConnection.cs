/* This class is part of Code project article: A Graph Tree Drawing Control for WPF,  witten by darrellp | 23 Feb 2009 
 * Full link article http://www.codeproject.com/Articles/29518/A-Graph-Tree-Drawing-Control-for-WPF
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNET.Tool.Common
{
	public struct TreeConnection
	{
		public ITreeNode IgnParent { get; private set; }
		public ITreeNode IgnChild { get; private set; }
		public List<DPoint> LstPt { get; private set; }

		public TreeConnection(ITreeNode ignParent, ITreeNode ignChild, List<DPoint> lstPt) : this()
		{
			IgnChild = ignChild;
			IgnParent = ignParent;
			LstPt = lstPt;
		}
	}
}
