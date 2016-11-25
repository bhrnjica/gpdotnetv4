/* This class is part of Code project article: A Graph Tree Drawing Control for WPF,  witten by darrellp | 23 Feb 2009 
 * Full link article http://www.codeproject.com/Articles/29518/A-Graph-Tree-Drawing-Control-for-WPF
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNET.Tool.Common
{
	public interface ITreeNode
	{
        // PrivateNodeInfo is a cookie used by GraphLayout to keep track of information on
        // a per node basis.  The ITreeNode implementer just has to provide a way to
        // save and retrieve this cookie.
        Object PrivateNodeInfo { get; set; }
		TreeNodeGroup TreeChildren { get; }
		double TreeWidth { get; }
		double TreeHeight { get; }
		bool Collapsed { get; }
        bool Selected { get; }
	}
}
