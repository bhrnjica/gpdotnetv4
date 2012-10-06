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
using System.Globalization;
using System.Windows.Forms;

namespace GPdotNET.Tool.Common
{
   /// <summary>
   /// Main panel for loading and defining Training data for GP and GA
   /// </summary>
    public partial class DataPanel : UserControl
    {
        #region Ctor and Fields
        public event EventHandler DataLoaded;
        public event EventHandler DataPredictionLoaded; 

        private double[][] _trainig;
        public double[][] Training
        {
            get
            {
                return _trainig;
            }
        }
        private double[][] _testing;
        public double[][] Testing
        {
            get
            {
                return _testing;
            }
        }
        private double[] _timeSeries;
        public double[] TimeSeries
        {
            get
            {
                return _timeSeries;
            }
        }


        public DataPanel()
        {
            InitializeComponent();
          

            //initial values of control vars
            txtNrVariables.Text = "10";
            txtNrTestSeries.Text = "10";
            txtNrSeries.Text = "0";
            this.Load+=(x,y)=>
            {

            };
            //Sample how to load data in to grid

            //ColumnHeader ch1 = new ColumnHeader();
            //ch1.Text = "KOlona1";
            //listView1.Columns.Add(ch1);
            //ch1 = new ColumnHeader();
            //ch1.Text = "KOlona2";
            //listView1.Columns.Add(ch1);
            //ch1 = new ColumnHeader();
            //ch1.Text = "KOlona3";
            //listView1.Columns.Add(ch1);
            //ch1 = new ColumnHeader();
            //ch1.Text = "KOlona4";
            //listView1.Columns.Add(ch1);

            //ListViewItem LVI = listView1.Items.Add("ime cols");
            //LVI.SubItems.Add(R.Next(100).ToString()); // 0..99
            //LVI.SubItems.Add(R.Next(100).ToString());
            //LVI.SubItems.Add(((10 + R.Next(10)) * 50).ToString());
            //LVI.SubItems.Add(((10 + R.Next(10)) * 50).ToString());

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// loads training data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTrainig_Click(object sender, EventArgs e)
        {
            var strFile= CommonMethods.GetFileFromOpenDialog();
            var data=CommonMethods.LoadDataFromFile(strFile);

            LoadTrainingData(data);
        }

       

        /// <summary>
        /// SHow 2D data in grid
        /// </summary>
        /// <param name="data"></param>
        private void FillGridWithData(double[][] data)
        {

            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = data[0].Length;
            int numRow = data.Length;

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Pos";
            colHeader.Width = 100;
            listView1.Columns.Add(colHeader);

            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();
                if (i + 1 == numCol)
                    colHeader.Text = "Y";
                else
                    colHeader.Text = "X" + (i + 1).ToString();

                colHeader.Width = 100;
                listView1.Columns.Add(colHeader);
            }

            for (int j = 0; j < numRow; j++)
            {
                ListViewItem LVI = listView1.Items.Add((j+1).ToString());

                for (int i = 0; i < numCol; i++)
                   LVI.SubItems.Add(data[j][i].ToString());
                
            }
        }

        /// <summary>
        /// Fire event for loading test data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTesting_Click(object sender, EventArgs e)
        {
            var strFile = CommonMethods.GetFileFromOpenDialog();
            var data = CommonMethods.LoadDataFromFile(strFile);

            LoadTestingData(data);
        }

        /// <summary>
        /// Before GP is started Series has to be converted in to Training and testing data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetToGP_Click(object sender, EventArgs e)
        {

            try
            {
                int nrSeries, nrVariables, nrSeriesForTesting;

                nrSeries = int.Parse(txtNrSeries.Text);
                nrVariables = int.Parse(txtNrVariables.Text);
                nrSeriesForTesting = int.Parse(txtNrTestSeries.Text);


                if (_timeSeries == null)
                {
                    MessageBox.Show("Load time series data first.");
                    return;
                }

                //check for consistence of variables
                if (_timeSeries.Length <= nrVariables + 1)
                {
                    MessageBox.Show("Number of vars must be lower than number of series.");
                    return;
                }
                
                if (_timeSeries.Length <= nrSeriesForTesting)
                {
                    MessageBox.Show("Number of testing samples must be lower that number of series.");
                    return;
                }
                if (nrVariables > nrSeriesForTesting)
                {
                    MessageBox.Show("Number of testing samples must be greather than number of input varibles.");
                    return;
                }

                if (_timeSeries.Length < nrVariables + nrSeriesForTesting+1)
                {
                    MessageBox.Show("Invalid number of varables and sample of testing series. Try to set up diferent values.");
                    return;
                }
                if (_timeSeries.Length - 2 * nrVariables - nrSeriesForTesting < 0)
                {
                    MessageBox.Show("Cannot converti series in to GP model. Try to set up diferent values.");
                    return;
                }

                int numberTreningData = _timeSeries.Length - nrVariables + 1 - nrSeriesForTesting;

                //Create training data
                _trainig = new double[numberTreningData][];
                for (int i = 0; i < numberTreningData; i++)
                {
                    //_trainig[i] = new double[numberTreningData];
                    _trainig[i] = new double[nrVariables + 1];
                    for (int j = i; j < nrVariables + 1 + i; j++)
                        _trainig[i][j - i] = _timeSeries[j];

                }

                //Create testing data
                _testing = new double[nrSeriesForTesting][];
                int k = 0;
                for (int i = _timeSeries.Length - nrSeriesForTesting; i < _timeSeries.Length; i++)
                {
                    _testing[k] = new double[nrVariables + 1];
                    int l = 0;
                    for (int j = i - nrVariables; l<nrVariables+1; j++, l++)
                        _testing[k][l] = _timeSeries[j];
                    k++;

                }

                //send event about dataLoading
                if (DataLoaded != null)
                    DataLoaded(this, new EventArgs());

                //send event about dataLoading
                if (DataPredictionLoaded != null)
                    DataPredictionLoaded(this, new EventArgs());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 

        /// <summary>
        /// event fired for loading series
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadSeries_Click(object sender, EventArgs e)
        {
            var strFile = CommonMethods.GetFileFromOpenDialog();
            var data = CommonMethods.LoadTimeSeriesFromFile(strFile);

            LoadSeriesData(data);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load data for training GO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        
        public bool LoadTrainingData(double[][] data)
        {
            if (data != null)
            {
                _trainig = data;
                FillGridWithData(data);
                //send event about dataLoading
                if (DataLoaded != null)
                    DataLoaded(this, new EventArgs());

                return true;
            }
            else
            {
                MessageBox.Show("The data were not loaded!");
                return false;
            }
        }
        /// <summary>
        /// Loads series from 2D array
        /// </summary>
        /// <param name="data"></param>
        public void LoadSeriesData(double[][] data)
        {
            if (data == null)
            {
                _timeSeries = null;
                MessageBox.Show("The data were not loaded!");
                return;
            }
            if (data != null && data[0].Length == 1)
            {
                FillGridWithData(data);
                _timeSeries = new double[data.Length];
                for (int i = 0; i < data.Length; i++)
                    _timeSeries[i] = data[i][0];

                txtNrSeries.Text = data.Length.ToString();
            }
            else if (data[0].Length != 1)
            {
                data = null;
                _timeSeries = null;
                MessageBox.Show("Incorect data format!");
            }
            else
            {
                _timeSeries = null;
                MessageBox.Show("The data were not loaded!");
            }

            //Set trainig  and testing data size
            if(Testing!=null)
               txtNrTestSeries.Text = Testing.Length.ToString();
            if(Training!=null)
                txtNrVariables.Text = (Training[0].Length-1).ToString();
               
        }

       
        /// <summary>
        /// Hiding or showing controls for diferent taype of programs
        /// </summary>
        /// <param name="p"></param>
        public void SetTimeSeries(bool p)
        {
            groupBox5.Visible = p;
            btnLoadSeries.Visible = p;
            btnLoadTesting.Visible = !p;
            btnLoadTraining.Visible = !p;
        }

        /// <summary>
        /// Load testing data from 2D array
        /// </summary>
        /// <param name="data"></param>
        public void LoadTestingData(double[][] data)
        {
            if (data != null)
            {
                _testing = data;
                //send event about dataLoading
                if (DataPredictionLoaded != null)
                    DataPredictionLoaded(this, new EventArgs());
            }
            else
                MessageBox.Show("The prediction data were not loaded!");
        }
        
        /// <summary>
        /// Retirns output data for updating the chart for GP model 
        /// </summary>
        /// <returns></returns>
        public double [] GetOutputValues()
        {
            double[] output = new double[_trainig.Length];
            int outputIndex=_trainig[0].Length-1;
            for (int i = 0; i < _trainig.Length; i++)
            {
                output[i] = _trainig[i][outputIndex];
            }
            return output;
        }


        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            btnSetToGP.Enabled = p;
            btnLoadSeries.Enabled = p;
            btnLoadTesting.Enabled = p;
            btnLoadTraining.Enabled = p;

            txtNrVariables.Enabled = p;
            txtNrTestSeries.Enabled = p;


        }

        /// <summary>
        /// Serilize 2D array in to string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetStringFromData(double[][] data)
        {
            if (data == null)
                return null;
            else
            {
                string str = "";
                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < data[0].Length; j++)
                    {
                        str += data[i][j].ToString(CultureInfo.InvariantCulture) + ";";
                    }

                    str += "\t";
                }


                return str;
            }
        }

        /// <summary>
        /// Serilize 1D data in to string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetStringFromData(double[] data)
        {
            if (data == null)
                return null;
            else
            {
                string str = "";
                for (int i = 0; i < data.Length; i++)
                {
                    str += data[i].ToString(CultureInfo.InvariantCulture) + ";";
                    
                }


                return str;
            }
        }

        /// <summary>
        /// Extract numbers from string and return dwo dimension array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public double[][] loadData(string p)
        {
            //Training data

            var strdata = p.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            double[][] data = null;

            if (strdata.Length > 1)
            {
                data = new double[strdata.Length][];
                for (int i = 0; i < strdata.Length; i++)
                {
                    var cols = strdata[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    data[i] = new double[cols.Length];
                    for (int j = 0; j < cols.Length; j++)
                        if (cols[j] != "\r")
                            data[i][j] = double.Parse(cols[j], CultureInfo.InvariantCulture);
                }
            }

            return data;

        }

        /// <summary>
        /// Extract numbers from string and return dwo dimension array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public double[][] loadSeriesData(string p)
        {
            //Training data

            var strdata = p;
            double[][] data = null;

            if (strdata.Length > 1)
            {
                var cols = strdata.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                data = new double[cols.Length][];
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i] != "\r")
                    {
                        data[i]= new double[1];
                        data[i][0] = double.Parse(cols[i], CultureInfo.InvariantCulture);
                    }
                }
            }

            return data;

        }
        #endregion
    }
}
