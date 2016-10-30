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
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using GPdotNET.Core;
using GPdotNET.Util;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Panel for selecting which primitive programs will be included in to GP
    /// </summary>
    public partial class FunctionPanel : UserControl
    {
        #region Ctor and Fields
        private XDocument _xmlDoc=null;
        private Dictionary<int, GPFunction> _gpFunctions;
        public Dictionary<int,GPFunction> GPFunctions
        {
            get
            {
                SaveModification();
                return _gpFunctions; 
            }
        }
        
        public FunctionPanel()
        {
            InitializeComponent();
            listView1.CheckBoxes = true;
            listView1.GridLines = true;
            listView1.HideSelection = false;
            if (this.DesignMode)
                return;
            _gpFunctions = GPModelGlobals.LoadFunctionsfromXMLFile(_xmlDoc);
            fillListView(_gpFunctions.Select(x=>x.Value).ToList());
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Proces of filling listView with functions data
        /// </summary>
        /// <param name="funs"></param>
        private void fillListView(List<GPFunction> funs)
        {

            //clear the list
            listView1.Clear();

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Selected";
            colHeader.Width = 60;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Weight";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);

           
            colHeader = new ColumnHeader();
            colHeader.Text = "Name";
            colHeader.Width = 70;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Definition";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Aritry";
            colHeader.Width = 50;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Parameters";
            colHeader.Width = 80;
            listView1.Columns.Add(colHeader);


            colHeader = new ColumnHeader();
            colHeader.Text = "Description";
            colHeader.Width = 200;
            listView1.Columns.Add(colHeader);

            

            colHeader = new ColumnHeader();
            colHeader.Text = "ExcelDefinition";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "ReadOnly";
            colHeader.Width = 0;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "IsDistribution";
            colHeader.Width = 0;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "ID";
            colHeader.Width = 0;
            listView1.Columns.Add(colHeader);

            for (int i = 0; i <funs.Count; i++)
            {
                var fun = funs[i];
                ListViewItem LVI = listView1.Items.Add(" ");
                LVI.Checked = fun.Selected;
                LVI.SubItems.Add(fun.Weight.ToString());
               

                LVI.SubItems.Add(fun.Name.ToString());
                

                LVI.SubItems.Add(fun.Definition.ToString());
                LVI.SubItems.Add(fun.Aritry.ToString());
                LVI.SubItems.Add(fun.Parameters.ToString());
                LVI.SubItems.Add(fun.Description.ToString());
                LVI.SubItems.Add(fun.ExcelDefinition.ToString());
                LVI.SubItems.Add(fun.IsReadOnly.ToString());
                LVI.SubItems.Add(fun.IsDistribution.ToString());
                LVI.SubItems.Add(fun.ID.ToString());
 
            }
        }

        /// <summary>
        /// Selection changed listView enevt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                var ind= int.Parse(listView1.SelectedIndices[0].ToString());
                textBox1.Text= _gpFunctions[ind].Weight.ToString();
            }
        }

        /// <summary>
        /// Fire event of save new value for Weight of selected fustion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                int newWeight = int.Parse(textBox1.Text);

                //find selected row from listVeiew
                if (listView1.SelectedIndices.Count > 0)
                {
                    var ind = int.Parse(listView1.SelectedIndices[0].ToString());

                    _gpFunctions[ind].Weight = newWeight;

                    //If the user change selection state of function
                    SaveModification();

                    fillListView(_gpFunctions.Values.ToList());

                    listView1.SelectedIndices.Add(ind);


                    
                }
                else
                {
                    MessageBox.Show("First select listView row then modifi value.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Save modification user made in listView in to backend list of GPFunctions.
        /// </summary>
        private void SaveModification()
        {
            for (int i=0;i<listView1.Items.Count; i++)
            {
                 ListViewItem LVI = listView1.Items[i];
                 var gpFUn=_gpFunctions[i];
                 if (gpFUn.Selected != LVI.Checked)
                 {
                     gpFUn.Selected = LVI.Checked;
                 }
            }
        }
        #endregion
        
        #region Public Methods

        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            button1.Enabled = p;
            listView1.Enabled = p;
        }

        /// <summary>
        /// Deserilization of function
        /// </summary>
        /// <param name="p"></param>
        public void SelectFunctions(string p)
        {
            var funs = p.Split(';');

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem LVI = listView1.Items[i];
                if (funs.Length > i)
                {
                    var st = funs[i].Split(',');
                    if (st.Length == 2)
                    {
                        LVI.Checked = st[0] == "1" ? true : false;
                        LVI.SubItems[1].Text = st[1];
                    }
                    else
                    {
                        LVI.Checked = funs[i] == "1" ? true : false;
                    }
                }
                else
                    LVI.Checked = false;
                
            }  
        }

        /// <summary>
        /// Serilization of functions
        /// Serialization of Checked, and Weight columns.
        /// </summary>
        /// <returns></returns>
        public string GetFunctionState()
        {
            var funs ="";

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem LVI = listView1.Items[i];
                string str = LVI.Checked == true ? "1" : "0";
                str+=","+LVI.SubItems[1].Text+";";
                funs += str;

            }

            return funs;
        }
        #endregion

        public static string FunctionToString()
        {
            return "-";
        }
        public static void FunctionsFromString(string strFUn)
        {
            return ;
        }
    }
}
