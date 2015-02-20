﻿using GPdotNET.Core.Experiment;
using GPdotNET.Util;
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
using System.Globalization;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common
{
   
   /// <summary>
   /// Main panel for loading and defining Training data for GP and GA
   /// </summary>
    public partial class ExperimentPanel : UserControl
    {

        #region Ctor and Fields
        public event EventHandler DataLoaded;
        public event EventHandler DataPredictionLoaded;

        private GPModelType _problemType= GPModelType.ANNMODEL;

        //listview items
        private ListViewItem li;
        private int X = 0;
        private int Y = 0;
        private int subItemSelected = 0;
        private System.Windows.Forms.ComboBox cmbBox = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbBox1 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbBox2 = new System.Windows.Forms.ComboBox();


        public ExperimentPanel()
        {
            InitializeComponent();

            //first row combobox
            cmbBox.Items.Add("numeric");
            cmbBox.Items.Add("categorical");
            cmbBox.Items.Add("binary");
            cmbBox.Items.Add("..string..");
          
            cmbBox.Size = new System.Drawing.Size(0, 0);
            cmbBox.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox });
            cmbBox.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox.Hide();

            //second row combobox
            cmbBox1.Items.Add("ignore");
            cmbBox1.Items.Add("input");
            cmbBox1.Items.Add("output");

            cmbBox1.Size = new System.Drawing.Size(0, 0);
            cmbBox1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox1 });
            cmbBox1.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox1.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox1.Hide();

            //third row combobox
            cmbBox2.Items.Add("none");
            cmbBox2.Items.Add("MinMax");
            cmbBox2.Items.Add("Gaus");

            cmbBox2.Size = new System.Drawing.Size(0, 0);
            cmbBox2.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox2 });
            cmbBox2.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox2.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox2.Hide();
            numericUpDown1.Maximum = 50;
            numericUpDown1.Minimum = 0;
          
        }


        #region Cell ComboBox Events
        private void CmbKeyPress(object sender, KeyPressEventArgs e)
        {
            var combo = sender as ComboBox;

            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                combo.Hide();
            }
        }

        private void CmbFocusOver(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            combo.Hide();
        }

        private void CmbSelected(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            int sel = combo.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = combo.Items[sel].ToString();
                li.SubItems[subItemSelected].Text = itemSel;
            }
        }
        #endregion

        #endregion

        #region Private Methods


        #endregion

        #region Public Methods



        /// <summary>
        /// Hiding or showing controls for diferent type of programs
        /// </summary>
        /// <param name="p"></param>
        public void SetProblemType(GPModelType type)
        {
            _problemType = type;
            if (_problemType == GPModelType.TS)
            {
                groupBox5.Visible = true;
                //btnLoadTesting.Text="Load Series";
                //btnLoadTesting.Visible = true;
                //btnLoadTraining.Visible = false;
            }
			else if(_problemType== GPModelType.SR || _problemType == GPModelType.SRO)
			{
				var sz = this.listView1.Size;
                //this.listView1.Size = new System.Drawing.Size(sz.Width, sz.Height + groupBox5.Height-2*btnLoadTesting.Height);				
			}
            else if (_problemType == GPModelType.TSP )
            {
                groupBox5.Visible = false;
                //btnLoadTesting.Text = "Load Cities Map";
                //btnLoadTesting.Visible = true;
                //btnLoadTraining.Visible = false;

                var sz = this.listView1.Size;
               // this.listView1.Size = new System.Drawing.Size(sz.Width, sz.Height + groupBox5.Height-btnLoadTesting.Height);
            }
            else if (_problemType == GPModelType.AP || _problemType == GPModelType.TP)
            {
                groupBox5.Visible = false;
                //btnLoadTesting.Text = "Load Data";
                //btnLoadTesting.Visible = true;
                //btnLoadTraining.Visible = false;

                var sz = this.listView1.Size;
                //this.listView1.Size = new System.Drawing.Size(sz.Width, sz.Height + groupBox5.Height - btnLoadTesting.Height);
            }
        }
                
        /// <summary>
        /// Retirns output data for updating the chart for GP model 
        /// </summary>
        /// <returns></returns>
        public double[][] GetOutputValues()
        {
            double [][]retVal= new double[Experiment.GetRowCount()][];

            for(int i=0; i< Experiment.GetRowCount(); i++)
            {
                retVal[i]= new double[2];
                retVal[i][0] = i + 1;
                retVal[i][1]=Experiment.GetRowFromOutput(i)[0];
            }
            return retVal;
        }

      
        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            btnSetToGP.Enabled = p;
            //btnLoadTesting.Enabled = p;
            //btnLoadTraining.Enabled = p;

            //txtNrVariables.Enabled = p;
            //txtNrTestSeries.Enabled = p;
        }


        #endregion

        public Experiment Experiment { get; set; }
        public bool IsDatReady { get; set; }
        private void FillDataGrid(string[] header, string[][] data)
        {
            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = data[0].Length;
            int numRow = data.Length;

            ColumnHeader colHeader = null;
            colHeader = new ColumnHeader();
            colHeader.Text = " ";
            colHeader.Width = 120;
            listView1.Columns.Add(colHeader);
            //
            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();

                if (header == null)
                {
                    if (i + 1 == numCol)
                        colHeader.Text = "y";
                    else
                        colHeader.Text = "x" + (i + 1).ToString();
                }
                else
                    colHeader.Text = header[i];


                colHeader.Width = 100;
                colHeader.TextAlign = HorizontalAlignment.Center;

                listView1.Columns.Add(colHeader);
            }
            //first row is going to represent column names
            ListViewItem LVI = listView1.Items.Add("Column Name:");
            for (int i = 0; i < numCol; i++)
            {
                if(Experiment.HeaderAsString==null)
                {
                    if (i + 1 == numCol)
                        LVI.SubItems.Add("y");
                    else
                        LVI.SubItems.Add("x" + (i + 1).ToString());
                }
                else
                    LVI.SubItems.Add(Experiment.HeaderAsString[i]);
                

                LVI.BackColor = SystemColors.MenuHighlight;

            }

            //second row is going to represent the type of each column (input parameter, output variable)
            LVI = listView1.Items.Add("Column Type:");
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add("numeric");
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }
            //second row is going to represent is the column input, output or ignored column
            LVI = listView1.Items.Add("Param Type:");
            for (int i = 0; i < numCol; i++)
            {
                if (i + 1 >= numCol)
                    LVI.SubItems.Add("output");
                else
                    LVI.SubItems.Add("input");

                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //third row is going to represent is the normalization for colum
            LVI = listView1.Items.Add("Normalization:");
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add("MinMax");

                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //insert data
            for (int j = 0; j < numRow; j++)
            {
                LVI = listView1.Items.Add((j + 1).ToString());

                for (int i = 0; i < numCol; i++)
                {
                    LVI.SubItems.Add(data[j][i].ToString());
                }

            }

        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView1.HitTest(X, Y);
            var row = info.Item.Index;
            var col = info.Item.SubItems.IndexOf(info.SubItem);

            li = info.Item;
            subItemSelected = col;
            //only first and second Row process the mouse input 
            if (li == null || row > 3|| row < 1 || col < 1)
                return;

            ComboBox combo = null;
            if (row == 1)
                combo = cmbBox;
            else if (row == 2)
                combo = cmbBox1;
            else
                combo = cmbBox2;

            var subItm = li.SubItems[col];
            combo.Bounds =  subItm.Bounds;
            combo.Show();
            combo.Text = subItm.Text;
            combo.SelectAll();
            combo.Focus();
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
           
            X = e.X;
            Y = e.Y;

           // Console.WriteLine(string.Format("Row={0}:Col={1} val='{2}'", row, col, value));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ImportExperimentalData dlg = new ImportExperimentalData();
            if(dlg.ShowDialog()== DialogResult.OK)
            {
                if(dlg.ExperimentalData==null)
                    return;

                Experiment = dlg.ExperimentalData;

                FillDataGrid(dlg.ExperimentalData.HeaderAsString, dlg.ExperimentalData.TrainDataAsString);

                
                IsDatReady = false;
                btnSetToGP.Enabled = true;
            }
        }

        public bool IsCategoricalOutput 
        { 
            get
            {
                var col=Experiment.GetColumnsFromOutput();
               return col[0].ColumnDataType != ColumnDataType.Numeric;
            }
        }
        private void btnSetupToModel_Click(object sender, EventArgs e)
        {
            var colProperties= ParseColumns();
            try
            {
                
                Experiment.Prepare(colProperties, 100-(int)numericUpDown1.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GPdotNET");
                //throw;
                return;
            }

            if(Experiment.GetColumnOutputCount()<1)
            {
                MessageBox.Show("No output column is defined. Select one column to be output colum.");
                IsDatReady = false;
                return;
            }
            if (Experiment.GetColumnOutputCount() > 1)
            {
                MessageBox.Show("Multiple output column is not defined.");
                IsDatReady = false;
                return;
            }
            IsDatReady = true;
            //send event about data
            if (DataLoaded != null)
                DataLoaded(this, new EventArgs());

            //send event about data
            if (DataPredictionLoaded != null && (100- (int)numericUpDown1.Value)<100)
                DataPredictionLoaded(this, new EventArgs());
        }

        /// <summary>
        /// Pars experimental data na define properties for each column
        /// </summary>
        /// <returns></returns>
        private List<ColumnProperties> ParseColumns()
        {
            var lst = new List<ColumnProperties>();
            //f name of the columns
            var firstRow    = listView1.Items[0];
            var secondRow   = listView1.Items[1];
            var thirdRow    = listView1.Items[2];
            var forthRow    = listView1.Items[3];
 
            for(int i=1; i <firstRow.SubItems.Count; i++)
            {
                int colIndex = i;
                string colName = firstRow.SubItems[i].Text;
                string colType = secondRow.SubItems[i].Text;
                string paramType = thirdRow.SubItems[i].Text;
                string normType = forthRow.SubItems[i].Text;

                lst.Add(new ColumnProperties() { ColIndex = colIndex, ColName = colName, ColType = colType, ParamType = paramType, NormType = normType });
            }


            return lst;
        }




     
    }
}
