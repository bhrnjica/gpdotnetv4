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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GPdotNET.Core;
using GPdotNET.Util;

namespace GPdotNET.Tool.Common
{
    public delegate string NodeValue(int nValue);
    public delegate Brush NodeBackground(bool tag);

    /// <summary>
    /// This Class implements Grapg tree drawing control
    /// </summary>
    public partial class TreeCtrlDrawer : Panel
    {
        #region CTor and Private fields

        TreeNodeDrawer m_RootNode;
        LayeredTreeDraw _ltd;
        NodeValue _funNodeValueCallback = null;
        NodeBackground _funNodeBackgroundCallback=null;

        StringFormat sf;
        Pen connPen;
        Pen nodePen;
        SolidBrush nodeBrush;
        Font font;
        SolidBrush frgFColor;
        SolidBrush frgTColor ;

        public TreeCtrlDrawer()
            : base()
        {

            this.BackColor = SystemColors.Window;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;

            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            connPen = new Pen(Color.Black, 1.3f);
            nodePen = new Pen(Color.Gray, 2f);

            //this color depends of backgound of the Result panel
            nodeBrush = new SolidBrush(Color.FromArgb(201, 217, 239));
            font = new Font(new FontFamily("Arial"), 10f, FontStyle.Bold);
            frgFColor = new SolidBrush(Color.Black);
            frgTColor = new SolidBrush(Color.DarkBlue);
        }

        #endregion

        #region Properties
        public List<TreeNodeDrawer> Nodes
        {
            get;
            private set;
        }

        public TreeNodeDrawer RootNode
        {
            get
            {
                return m_RootNode;
            }
        }
        #endregion

        #region Private Internal and Protected Methods
       
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Nodes != null)
                {
                    sf.Dispose();
                    connPen.Dispose();
                    nodePen.Dispose();
                    nodeBrush.Dispose();
                    font.Dispose();
                    frgFColor.Dispose();
                    frgTColor.Dispose();

                    Nodes.Clear();
                    Nodes = null;
                    m_RootNode = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// On Paint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {


            base.OnPaint(e);

            drawNode(e.Graphics);
            drawConnection(e.Graphics);

        }

        /// <summary>
        /// Delets selected node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal bool DeleteNode(TreeNodeDrawer node)
        {
            //it is posibble to delete leaf node only

            if (node.TreeChildren == null || node.TreeChildren.Count == 0)
            {
                //delete node from all parent which has node as a children
                foreach (var no in Nodes)
                    no.TreeChildren.Remove(node);

                //delete all connection to deleted node
                _ltd.Connections.RemoveAll(c => c.IgnChild == node);

                //delete node from the collection
                bool retVal = Nodes.Remove(node);

                //redraw the layout
                RefreshTree();

                return retVal;
            }
            return false;
        }

        /// <summary>
        /// Helpper methods for adding several node in to hiearchiy manner
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numAritry"></param>
        /// <param name="treeNodeDrawer"></param>
        internal void AddNodeWithChildren(object value, int numAritry, TreeNodeDrawer treeNodeDrawer)
        {
            TreeNodeDrawer parentNode;
            if (treeNodeDrawer == null)
                parentNode = AddNode(value,false, treeNodeDrawer);
            else
            {
                parentNode = treeNodeDrawer;
                parentNode.Content = value;
            }

            for(int i=0; i<numAritry; i++)
                AddNode("o", false, parentNode);
           
            RefreshTree();
        }

        /// <summary>
        /// on mouse click event (virual method)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (Nodes == null)
                return;


            //Drawing the Node
            foreach (var node in Nodes)
            {
                if (node.Rect.Contains(e.Location))
                    node.Selected = true;
                else
                    node.Selected = false;
            }

            Invalidate();
        }

        /// <summary>
        /// Drawing allnodes in three control
        /// </summary>
        /// <param name="gr"></param>
        private void drawNode(Graphics gr)
        {
            if (Nodes == null)
                return;

            foreach (var tn in Nodes)
            {

                Point ptLocation = new Point(0, 0);
                if (tn != null)
                {
                    ptLocation = new Point(this.Margin.Left + (int)_ltd.X(tn) + base.AutoScrollPosition.X,
                                           this.Margin.Top + (int)_ltd.Y(tn) + base.AutoScrollPosition.Y);
                }
                tn.Rect = new Rectangle(ptLocation, tn.NodeSize);
            }

            //Drawing the Node
            int counter = 1;
            foreach (var node in Nodes)
            {
                if(_funNodeBackgroundCallback!=null)
                    gr.FillRectangle(_funNodeBackgroundCallback(node.Tag), node.Rect);

                counter++;

                if (node.Selected)
                    gr.FillRectangle(nodeBrush, node.Rect);

                gr.DrawRectangle(nodePen, node.Rect);

                if (node.Content == null)
                    return;

                if (_funNodeValueCallback != null && node.Content.IsNumber())
                {
                    int val = (int)node.Content;
                    gr.DrawString(_funNodeValueCallback(val), font, val >= 2000 ? frgFColor : frgTColor, node.Rect, sf);
                }
                else
                    gr.DrawString(node.Content.ToString(), font, frgFColor, node.Rect, sf);
            }
        }

        /// <summary>
        /// Proces of drqaing connection
        /// </summary>
        /// <param name="dc"></param>
        private void drawConnection(Graphics dc)
        {
            if (Nodes == null || _ltd == null)
                return;

            if (_ltd.Connections != null)
            {


                Point ptLast = new Point(this.Margin.Left, this.Margin.Top);
                bool fHaveLastPoint = false;

                foreach (TreeConnection tcn in _ltd.Connections)
                {
                    fHaveLastPoint = false;
                    foreach (DPoint dpt in tcn.LstPt)
                    {
                        if (!fHaveLastPoint)
                        {
                            ptLast = PtFromDPoint(tcn.LstPt[0]);
                            fHaveLastPoint = true;
                            continue;
                        }
                        dc.DrawLine(connPen, PtFromDPoint(dpt), ptLast);
                        ptLast = PtFromDPoint(dpt);
                    }
                }
            }
        }

        /// <summary>
        /// Converts point
        /// </summary>
        /// <param name="dPoint"></param>
        /// <returns></returns>
        private Point PtFromDPoint(DPoint dPoint)
        {
            return new Point((int)dPoint.X + base.AutoScrollPosition.X + this.Margin.Left, (int)dPoint.Y + base.AutoScrollPosition.Y + this.Margin.Top);

        }

        /// <summary>
        /// Init comp
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TreeCtrlDrawer
            // 
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Return selected node
        /// </summary>
        /// <returns></returns>
        public TreeNodeDrawer GetSelectedNode()
        {
            if (Nodes == null)
                return null;
            //Drawing the Node
            foreach (var node in Nodes)
                if (node.Selected)
                    return node;
            return null;
        }

        /// <summary>
        /// Converts treeDrawer in to GPNode 
        /// </summary>
        /// <returns> GPnode tree structure</returns>
        public GPNode ToGPNode(bool tag=false)
        {
            if (this.RootNode == null)
                return null;

            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            Queue<TreeNodeDrawer> ctrls = new Queue<TreeNodeDrawer>();

            //current node
            GPNode root = GPNode.NewNode();
            TreeNodeDrawer treeCtrl = null;

            ctrls.Enqueue(this.RootNode);
            dataTree.Enqueue(root);

            while (ctrls.Count > 0)
            {
                //get next node
                var node = dataTree.Dequeue();
                treeCtrl = ctrls.Dequeue();
                node.value = (int)treeCtrl.Content;
                node.marked = tag;

                if (treeCtrl.TreeChildren != null && treeCtrl.TreeChildren.Count > 0)
                {
                    node.children = new GPNode[treeCtrl.TreeChildren.Count];
                    for (int i = 0; i < treeCtrl.TreeChildren.Count; i++)
                    {
                        node.children[i] = GPNode.NewNode();
                        dataTree.Enqueue(node.children[i]);

                        ctrls.Enqueue((TreeNodeDrawer)treeCtrl.TreeChildren[i]);
                    }
                }
            }

            return root;

        }

        /// <summary>
        /// Return selected node index
        /// </summary>
        /// <returns></returns>
        public int GetSelectedNodeIndex()
        {
            if (Nodes == null)
                return 0;
            //Drawing the Node
            int counter = 1;
            foreach (var node in Nodes)
            {
                if (node.Selected)
                    return counter;
                counter++;
            }
            return 0;
        }

        /// <summary>
        /// Check is tree is empty without root
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return m_RootNode == null;
        }

        /// <summary>
        /// Clear all nodes and connections from the control
        /// </summary>
        public void Clear()
        {
            if (Nodes == null)
                return;
            Nodes.Clear();
        }

        /// <summary>
        /// Methods fo add new node in to tree structure
        /// </summary>
        /// <param name="nodeValue"></param>
        /// <param name="tnParent"></param>
        /// <returns></returns>
        public TreeNodeDrawer AddNode(object nodeValue, bool tag, TreeNodeDrawer tnParent = null)
        {
            if (tnParent != null && m_RootNode == null)
                throw new Exception("The m_RootNode three is null.");

            TreeNodeDrawer tnNew = new TreeNodeDrawer();
            tnNew.Content = nodeValue;
            tnNew.Tag = tag;
            //this is root if parent is null
            if (tnParent != null)
                tnParent.TreeChildren.Add(tnNew);
            else
                m_RootNode = tnNew;

            if (Nodes == null)
                Nodes = new List<TreeNodeDrawer>();

            Nodes.Add(tnNew);
            return tnNew;
        }

        /// <summary>
        /// Prepare for Tree node draw.
        /// </summary>
        /// <param name="gpRoot"></param>
        /// <param name="funNodeValue">delegate for callbaack for retrieve GPNode string representation</param>
        public void DrawTreeExpression(GPNode gpRoot,NodeValue funNodeValue, NodeBackground funNodeBackground=null)
        {
            Clear();

            //define callbacks for 
            _funNodeValueCallback = funNodeValue;
            _funNodeBackgroundCallback = funNodeBackground;

            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            Queue<TreeNodeDrawer> ctrls = new Queue<TreeNodeDrawer>();

            //current node
            GPNode node = null;
            TreeNodeDrawer treeCtrl = null;
            treeCtrl = AddNode(gpRoot.value,gpRoot.marked,null);

            ctrls.Enqueue(treeCtrl);
            dataTree.Enqueue(gpRoot);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();
                treeCtrl = ctrls.Dequeue();

                if (node.children != null)
                    for (int i = 0; i < node.children.Length; i++)
                    {
                        var tn = AddNode(node.children[i].value,node.children[i].marked, treeCtrl);
                        dataTree.Enqueue(node.children[i]);
                        ctrls.Enqueue(tn);
                    }
            }

            _ltd = new LayeredTreeDraw(m_RootNode, 17.5, 17.5, 17.5, VerticalJustification.top);
            _ltd.LayoutTree();

            //Auto enable scroll for new content size
            this.AutoScrollMinSize = new Size((int)_ltd.PxOverallWidth + this.Margin.Left + this.Margin.Right, (int)_ltd.PxOverallHeight + this.Margin.Top + this.Margin.Bottom);
            Invalidate();

        }

        /// <summary>
        /// Refresh tree control
        /// </summary>
        public void RefreshTree()
        {
            //Auto enable scroll for new content size
            _ltd = new LayeredTreeDraw(m_RootNode, 17.5, 17.5, 17.5, VerticalJustification.top);
            _ltd.LayoutTree();
            this.AutoScrollMinSize = new Size((int)_ltd.PxOverallWidth + this.Margin.Left + this.Margin.Right, (int)_ltd.PxOverallHeight + this.Margin.Top + this.Margin.Bottom);
            Invalidate();
        }

        /// <summary>
        /// Svae tree control in to image
        /// </summary>
        /// <param name="path"></param>
        public void SaveAsImage(string path = "C:\\a.png")
        {
            //set bitmap state for control
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AutoScroll = false;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            this.VerticalScroll.Enabled = false;
            this.VerticalScroll.Visible = false;

            var size=new Size((int)_ltd.PxOverallWidth + this.Margin.Left + this.Margin.Right, (int)_ltd.PxOverallHeight + this.Margin.Top + this.Margin.Bottom);

            Bitmap bmp = new Bitmap(size.Width, size.Height);
            
            Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
          
            Graphics gBmp = Graphics.FromImage(bmp);

           
            

            gBmp.FillRectangle(nodeBrush, rect);
            drawNode(gBmp);
            drawConnection(gBmp);

            

            this.DrawToBitmap(bmp, rect);

            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            //Restore state
            
            this.HorizontalScroll.Enabled = true;
            this.HorizontalScroll.Visible = true;
            this.VerticalScroll.Enabled = true;
            this.VerticalScroll.Visible = true;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            
            //Redraw th control
            Invalidate(true);

        }

        #endregion

    }
}
