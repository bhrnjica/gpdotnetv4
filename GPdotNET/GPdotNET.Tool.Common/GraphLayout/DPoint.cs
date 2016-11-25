/* This class is part of Code project article: A Graph Tree Drawing Control for WPF,  witten by darrellp | 23 Feb 2009 
 * Full link article http://www.codeproject.com/Articles/29518/A-Graph-Tree-Drawing-Control-for-WPF
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNET.Tool.Common
{
	// Since I'm trying to allow GraphLayout to be used from various
	// platforms which have different definitions of points, I make
	// my own point structure here to be used by all platforms.
	public struct DPoint
	{
		public double X;
		public double Y;

		public DPoint(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
