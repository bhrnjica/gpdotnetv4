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
using System.Windows;
using System.Drawing;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Helper ofr implementation Tree Drawer Control
    /// </summary>
	public class TreeNodeDrawer : ITreeNode
	{
        #region TreeNode Members

        public object PrivateNodeInfo { get; set; }

        public TreeNodeGroup TreeChildren { get; private set; }

        public Size NodeSize
        {
            get;
            set;
        }

        public double TreeHeight
        {
            get
            {
                return NodeSize.Height;
            }
        }

        public double TreeWidth
        {
            get
            {
                return NodeSize.Width;
            }
        }
       
        private bool _collapsed;
		public bool Collapsed
		{
			get { return _collapsed; }
			set { _collapsed=value; }
		}

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        private object _content;
        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public bool Tag { get; set; }
        private Rectangle _rect;
        public Rectangle Rect
        {
            get
            {
                return _rect;
            }
            set { _rect = value; }
        }

        #endregion
        #region Constructors
        public TreeNodeDrawer(int width=55, int height=28)
		{
            NodeSize = new Size(width, height);
			TreeChildren = new TreeNodeGroup();
			
		}
		#endregion

	}
}
