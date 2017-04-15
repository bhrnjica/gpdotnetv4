using GPdotNET.Core.Experiment;
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
using System.Linq;
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
        private System.Windows.Forms.ComboBox cmbBox3 = new System.Windows.Forms.ComboBox();


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
            cmbBox2.Items.Add("Gauss");

            cmbBox2.Size = new System.Drawing.Size(0, 0);
            cmbBox2.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox2 });
            cmbBox2.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox2.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox2.Hide();

            //forth row combobox
            cmbBox3.Items.Add("Ignore");
            cmbBox3.Items.Add("Average");
            cmbBox3.Items.Add("Max");
            cmbBox3.Items.Add("Min");

            cmbBox3.Size = new System.Drawing.Size(0, 0);
            cmbBox3.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox3 });
            cmbBox3.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox3.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox3.Hide();

            numericUpDown1.Maximum = int.MaxValue;
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
        /// Returns output data for updating the chart for GP model 
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
            if (data == null)
                return;
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

            //forth row is going to represent missing values action
            LVI = listView1.Items.Add("Missing Value:");
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add("Ignore");

                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //insert data
            for (int j = 0; j < numRow; j++)
            {
                LVI = listView1.Items.Add((j + 1).ToString());
                LVI.UseItemStyleForSubItems = false;
                for (int i = 0; i < numCol; i++)
                {
                    if (data[j][i]=="n/a")
                    {
                        System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                        itm.ForeColor = Color.Red;
                        itm.Text = data[j][i];
                        LVI.SubItems.Add(itm);             
                    }
                     
                    else
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
            if (li == null || row > 4|| row < 1 || col < 1)
                return;

            ComboBox combo = null;
            if (row == 1)
                combo = cmbBox;
            else if (row == 2)
                combo = cmbBox1;
            else if (row == 3)
                combo = cmbBox2;
            else
                combo = cmbBox3;

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
               return col[0].ColumnDataType == ColumnDataType.Categorical;
            }
        }

        public bool IsBinarylOutput
        {
            get
            {
                var col = Experiment.GetColumnsFromOutput();
                return col[0].ColumnDataType == ColumnDataType.Binary;
            }
        }

        public ColumnDataType GetOutputColumnType()
        {
            var col = Experiment.GetColumnsFromOutput();
            return col[0].ColumnDataType;
        }
        private void btnSetupToModel_Click(object sender, EventArgs e)
        {
            // if()
            var tabs = this.Parent.Parent as TabControl;
            if(tabs.TabCount>2)
            {
                if (MessageBox.Show("The current calculated model will be discarded. Do you want to continue?", "GPdotNET", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }
            StartModelling();
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartModelling()
        {
            var colProperties = ParseColumns();
            if(colProperties==null)
            {
                MessageBox.Show("Column metadata is invalid.", "GPdotNET");
                //throw;
                return;
            }

            try
            {

               var retVal =  Experiment.Prepare(colProperties, (int)numericUpDown1.Value, presentigeRadio.Checked==true);
                if (!retVal)
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GPdotNET");
                //throw;
                return;
            }

            if (Experiment.GetColumnOutputCount() < 1)
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

            //send event about data for vaidation
            if (DataPredictionLoaded != null && (int)numericUpDown1.Value >= 0 /*&& (100 - (int)numericUpDown1.Value) <= 100*/)
                DataPredictionLoaded(this, new EventArgs());
        }

        /// <summary>
        /// Pars experimental data and define properties for each column
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
            var fifthRow    = listView1.Items[4];
 
            for(int i=1; i <firstRow.SubItems.Count; i++)
            {
                int colIndex = i;
                string colName = firstRow.SubItems[i].Text;
                string colType = secondRow.SubItems[i].Text;
                string paramType = thirdRow.SubItems[i].Text;
                string normType = forthRow.SubItems[i].Text;
                string missingValue = fifthRow.SubItems[i].Text;

                lst.Add(new ColumnProperties() { ColIndex = colIndex, ColName = colName, ColType = colType, ParamType = paramType, NormType = normType, MissingValue = missingValue });
            }


            return lst;
        }

        private void setColumn(List<ColumnProperties> cols )
        {
            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = cols.Count;
            int numRow = 5;

            ColumnHeader colHeader = null;
            colHeader = new ColumnHeader();
            colHeader.Text = " ";
            colHeader.Width = 120;
            listView1.Columns.Add(colHeader);
            //
            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();

                //if (header == null)
                //{
                //    if (i + 1 == numCol)
                //        colHeader.Text = "y";
                //    else
                //        colHeader.Text = "x" + (i + 1).ToString();
                //}
                //else
                //    colHeader.Text = header[i];

                colHeader.Text = cols[i].ColName;
                colHeader.Width = 100;
                colHeader.TextAlign = HorizontalAlignment.Center;

                listView1.Columns.Add(colHeader);
            }
            //first row is going to represent column names
            ListViewItem LVI = listView1.Items.Add("Column Name:");
            for (int i = 0; i < numCol; i++)
            {
                //if (Experiment.HeaderAsString == null)
                //{
                //    if (i + 1 == numCol)
                //        LVI.SubItems.Add("y");
                //    else
                //        LVI.SubItems.Add("x" + (i + 1).ToString());
                //}
                //else
                //    LVI.SubItems.Add(Experiment.HeaderAsString[i]);

                LVI.SubItems.Add(cols[i].ColName);
                LVI.BackColor = SystemColors.MenuHighlight;

            }

            //second row is going to represent the type of each column (input parameter, output variable)
            LVI = listView1.Items.Add("Column Type:");
            for (int i = 0; i < numCol; i++)
            {
                //LVI.SubItems.Add("numeric");
                LVI.SubItems.Add(cols[i].ColType);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }
            //second row is going to represent is the column input, output or ignored column
            LVI = listView1.Items.Add("Param Type:");
            for (int i = 0; i < numCol; i++)
            {
                //if (i + 1 >= numCol)
                //    LVI.SubItems.Add("output");
                //else
                //    LVI.SubItems.Add("input");
                LVI.SubItems.Add(cols[i].ParamType);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //third row is going to represent is the normalization for colum
            LVI = listView1.Items.Add("Normalization:");
            for (int i = 0; i < numCol; i++)
            {
                //LVI.SubItems.Add("MinMax");
                LVI.SubItems.Add(cols[i].NormType);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //forth row is going to represent missing values action
            LVI = listView1.Items.Add("Missing Value:");
            for (int i = 0; i < numCol; i++)
            {
                //LVI.SubItems.Add("Ignore");
                LVI.SubItems.Add(cols[i].MissingValue);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ExperimentToString()
        {
           return  Experiment.GetExperimentToString( ParseColumns(), (int)numericUpDown1.Value, presentigeRadio.Checked);
        }

        public void ExperimentFromString(string exp)
        {
            Experiment = new Experiment();
            var pstr = exp.Split(';');
            int columnCount = 0;
            try
            {
                //test procent
                var p = pstr[0].Split(':');
                if (p.Length == 2)
                {
                    if (p[1] == "1")
                        numberRadio.Checked = true; 
                }

                int temp = 0;
                if (!int.TryParse(p[0], out temp))
                    temp = 0;
                numericUpDown1.Value = temp;

                ///col count
                temp = 0;
                if (!int.TryParse(pstr[1], out temp))
                    temp = 0;

                columnCount = temp;

                //load columnMetaData
                var cols = loadMetaDataColumn(columnCount, pstr);

                setColumn(cols);
                var  ind = columnCount * 6 + 2;
                var strData = pstr.Skip(ind).ToArray();
                var rowCount = strData.Length / columnCount;
                
                //insert data
                int index = 0;
                string[][] expDataString = new string[rowCount][];
                for (int j = 0; j < rowCount; j++)
                {
                    index += j;
                    var LVI = listView1.Items.Add((j + 1).ToString());
                    LVI.UseItemStyleForSubItems = false;
                    expDataString[j] = new string[columnCount];
                    for (int i = 0; i < columnCount; i++)
                    {
                        index = j* columnCount + i;
                        if (strData[index] == "n/a")
                        {
                            System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                            itm.ForeColor = Color.Red;
                            itm.Text = strData[index];
                            LVI.SubItems.Add(itm);
                        }

                        else
                            LVI.SubItems.Add(strData[index].ToString());

                        expDataString[j][i]= strData[index];
                    }

                }
                Experiment.TrainDataAsString = expDataString;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private List<ColumnProperties> loadMetaDataColumn(int columnCount, string[] pstr)
        {
            var cols = new List<ColumnProperties>();
            int index = 2;
            int metaDataCount = 6;
           for(int i = 0; i < columnCount; i++)
            {
                var ind = i * metaDataCount + index;
                var col = new ColumnProperties();
                col.ColIndex = int.Parse(pstr[ind]);
                col.ColName = pstr[ind + 1];
                col.ColType = pstr[ind + 2];
                col.ParamType = pstr[ind + 3];
                col.NormType = pstr[ind + 4];
                col.MissingValue = pstr[ind + 5];

                cols.Add(col);

            }

            return cols;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ColumnProperties> GetColumnMetadata()
        {
            return ParseColumns();
        }


        /// <summary>
        /// GP specific method for retrieing training data
        /// </summary>
        /// <returns></returns>
        public double[][] GetTrainingData()
        {
            try
            {
                return Experiment.GetDataForGP(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GP specific method for retrieing testing data
        /// </summary>
        /// <returns></returns>
        public double[][] GetTestingData()
        {
            try
            {
                return Experiment.GetDataForGP(true);
            }
            catch (Exception)
            {
                return null;
            }                
        }

        public int GetClassCount()
        {
            if (GetOutputColumnType() == ColumnDataType.Categorical || GetOutputColumnType() == ColumnDataType.Binary)
            {
                var cols = Experiment.GetColumnsFromOutput();
                if (cols == null || cols.Count > 0)
                {
                    var c = cols[0];
                    return c.Statistics.Categories.Count;
                }
            }
            //
            return 0;
        }
    }
}
