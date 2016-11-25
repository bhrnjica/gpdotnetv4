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
using System.Linq;
using System.Windows.Forms;
using GPdotNET.Core;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// This class provide manual way to define function in analytic form, just like ordinary math function
    /// </summary>
    public partial class AnaliticFunctionDef : UserControl
    {
        #region Constructor and Fileds

        /// <summary>
        /// CTor
        /// </summary>
        public AnaliticFunctionDef()
        {
            InitializeComponent();
            inputVars = new Dictionary<string, object>();

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Name";
            colHeader.Width = 60;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Value";
            colHeader.Width = 60;
            listView1.Columns.Add(colHeader);
        }

        Dictionary<int,GPFunction> funs;
        Dictionary<int, GPTerminal> terminals;
        Dictionary<string, object> inputVars;
        #endregion

        #region PrivateMethods
        /// <summary>
        /// add function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (treeCtrlDrawer1.IsEmpty() == false)
            {
                var nod = treeCtrlDrawer1.GetSelectedNode();

                if (nod == null || nod.TreeChildren.Count > 0)
                {
                    MessageBox.Show("Select Leaf Node first.");
                    return;
                }

            }
            if (validateInput(comboBox1.Text))
            {
                int numAritry = GetNumberOfArtitry(comboBox1.Text);
                treeCtrlDrawer1.AddNodeWithChildren(comboBox1.Text, numAritry, treeCtrlDrawer1.GetSelectedNode());
            }
            else
                MessageBox.Show("Input function doesnt exist! Choose the right function from combo box.");
          
        }

        /// <summary>
        /// Validation of input
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool validateInput(string p)
        {
            if (string.IsNullOrEmpty(p))
                return false;
            else if (funs.Values.Where(f => f.Name == p).Count() == 1)
                return true;
            else
                return false;
        }

       
        /// <summary>
        /// Look for function name in to function set and return number of aritry
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int GetNumberOfArtitry(string p)
        {
            if (funs == null)
                throw new Exception("Function set cannot be null.");

          int? ret=  funs.Values.Where(f => f.Name == p).Select(f => f.Aritry).FirstOrDefault();
          if (ret == null)
              return 0;
          else
              return ret.Value;
        }

              
        /// <summary>
        /// add param
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            float con = 0;
            if (float.TryParse(textBox1.Text, out con))
            {
                int count = inputVars.Count(x => x.Key.Contains("C")) + 1;
                string name = "C" + count.ToString();

                inputVars.Add(name, con);
                treeCtrlDrawer1.GetSelectedNode().Content = name;
                treeCtrlDrawer1.Invalidate();

                ListViewItem LVI = listView1.Items.Add(name);
                LVI.SubItems.Add(con.ToString());
                return;
            }
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Param name must be in form of X1, X2, ... or Xn, where n in natural number.");
                return;
            }
            string param= textBox1.Text.ToUpper();
            if (param[0] != 'X')
            {
                MessageBox.Show("Param name must be in form of X1, X2, ... or Xn, where n in natural number.");
                return;
            }

            var val = param.Substring(1, param.Length-1);
            int numb = 0;
            if(!int.TryParse(val,out numb))
            {
                MessageBox.Show("Param name must be in form of X1, X2, ... or Xn, where n in natural number.");
                return;
            }

            if (treeCtrlDrawer1.GetSelectedNode() == null)
            {
                MessageBox.Show("Tree node is not selected.");
                return;
            }

            treeCtrlDrawer1.GetSelectedNode().Content = param;
            treeCtrlDrawer1.Invalidate();

            if (inputVars.ContainsKey(param))
                return;
            inputVars.Add(param, param);
            treeCtrlDrawer1.GetSelectedNode().Content = param;
            treeCtrlDrawer1.Invalidate();

            var rr = listView1.Items.Add(param);
            rr.SubItems.Add(param.ToString());
            return;
        }

        /// <summary>
        /// delete leaf node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm?", "GPdotNET", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                var node = treeCtrlDrawer1.GetSelectedNode();
               
                //Delete dctonary
                inputVars.Remove(node.Content.ToString());
                //delete from listView
                foreach (ListViewItem lvi in listView1.Items)
                {
                    if (lvi.Text == node.Content.ToString())
                    {
                        listView1.Items.Remove(lvi);
                        break;
                    }
                }


                if (!treeCtrlDrawer1.DeleteNode(node))
                {
                    MessageBox.Show("It is posible to delete only non parent node.");
                }

            }
        }

        /// <summary>
        /// Proces of conversion listItems strored in ListView in to collection of Ternimas
        /// </summary>
        private void FromDirToFuns()
        {
            terminals = new Dictionary<int, GPTerminal>();

            var list = inputVars.Where(x => x.Key.Contains("X")).OrderBy(x => x.Key); ;
            int numVar = list.Count();
            for (int i = 0; i < numVar; i++)
            {
                var t = new GPTerminal();
                t.IsConstant = false;
                t.Name = list.ElementAt(i).Key;
                t.Index = i;
                terminals.Add(t.Index, t);
            }

            list = inputVars.Where(x => x.Key.Contains("C")).OrderBy(x=>x.Key);
            for (int i = 0; i < list.Count(); i++)
            {
                var t = new GPTerminal();
                t.IsConstant = true;
                t.Name = list.ElementAt(i).Key;
                t.fValue = float.Parse(list.ElementAt(i).Value.ToString());
                t.Index = numVar+i;
                terminals.Add(t.Index,t);
            }


        }
#endregion

        #region Public Methods
        /// <summary>
        /// Load function in to combobox
        /// </summary>
        /// <param name="functions"></param>
        public void LoadFuns(Dictionary<int,GPFunction> functions)
        {
            funs = functions;

            foreach (var fun in funs)
                comboBox1.Items.Add(fun.Value.Name);
        }

        /// <summary>
        /// Returns id of TErminal/Fun for given name
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public int GetNodeValue(object val)
        {
            
            if(val==null)
                throw new Exception("Argument is invalud!");

            if (inputVars.ContainsKey(val.ToString()))
            {
                var list = terminals.Values.Where(x => x.Name == val.ToString()).Select(x => x.Index).FirstOrDefault();

                return 1000 + list;
            }

            for(int i=0;i<funs.Count; i++)
            {
                if(funs[i].Name==val.ToString())
                {
                    return i + 2000;
                }
            }

           return 1000;

        }
        /// <summary>
        /// Getter for terninal list list
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,GPTerminal> GetTerminals()
        {
            return terminals;
        }

        /// <summary>
        /// Converts treeDrawer in to GPNode 
        /// </summary>
        /// <returns> GPnode tree structure</returns>
        public GPNode TreeNodeToGPNode()
        {
            if (treeCtrlDrawer1.RootNode == null)
                return null;
            //Create list of terminals
            FromDirToFuns();

            //Collection holds tree nodes
            Queue<GPNode> dataTree = new Queue<GPNode>();
            Queue<TreeNodeDrawer> ctrls = new Queue<TreeNodeDrawer>();

            //current node
            GPNode root = GPNode.NewNode();
            TreeNodeDrawer treeCtrl=null;

            ctrls.Enqueue(treeCtrlDrawer1.RootNode);
            dataTree.Enqueue(root);

            while (ctrls.Count > 0)
            {
                //get next node
                var node = dataTree.Dequeue();
                treeCtrl = ctrls.Dequeue();
                node.value = GetNodeValue(treeCtrl.Content);

                if (treeCtrl.TreeChildren != null && treeCtrl.TreeChildren.Count>0)
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
        /// Enable or disable controls when program is runnign
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            groupBox1.Enabled = p;
            treeCtrlDrawer1.Enabled = p;
        }

        /// <summary>
        /// Returns terminal name in order to define list of terminal
        /// </summary>
        /// <returns></returns>
        public List<string> GetTerminalNames()
        {
            return inputVars.Where(x => x.Key.Contains("X")).Select(x => x.Value.ToString()).ToList();
        }

        /// <summary>
        /// TO DO
        /// Serilization to string
        /// </summary>
        /// <returns></returns>
        public string FunctionToString()
        {

            return "";
        }

        /// <summary>
        /// TO DO
        /// Deserialization from file
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool StringToFunction(string str)
        {
            return false;
        }

#endregion
    }
}
