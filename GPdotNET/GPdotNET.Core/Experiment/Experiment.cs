using GPdotNET.Core.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPdotNET.Core.Experiment
{
    /// <summary>
    /// Main class for handling with eperimental data. 
    /// Note: In the future should be only class for loading experimental data in to GPdotNET. 
    /// </summary>
    public class Experiment
    {
        #region Ctor and Fields

        string[][]          m_strData; //loaded string of data
        string[]            m_strHeader;//columns header 

        List<ColumnData>    m_trainData;//columns for training 
        List<ColumnData>    m_testData;//columns for testing
   
        public Experiment()
        {

        }
        #endregion

        #region Public members


        #region String data manipulation 

        /// <summary>
        /// Returns Header 
        /// </summary>
        public string[] HeaderAsString { get { return m_strHeader; } }

        /// <summary>
        /// Returns loaded experiment as string 
        /// </summary>
        public string[][] TrainDataAsString { get { return m_strData; } set { m_strData = value; } }
        #endregion


        #region Column Specific Methods
        /// <summary>
        /// Number of all column after Category columns perfom rule 1-of N
        /// </summary>
        /// <returns></returns>
        public int GetColumnCount_FromNormalizedValue()
        {
            int counter = 0;
            for (int i = 0; i < m_trainData.Count; i++)
            {
                if (m_trainData[i].ColumnDataType == ColumnDataType.Categorical)
                    counter += m_trainData[i].Statistics.Categories.Count;
                else
                    counter++;
            }

            return counter;
        }
        /// <summary>
        /// Number of all column after Category columns perfom rule 1-of N
        /// </summary>
        /// <returns></returns>
        public int GetColumnInputCount_FromNormalizedValue()
        {
            var data = GetData(false);
            var inCols= data.Where(x => !x.IsOutput).ToList();

            int counter = 0;
            for (int i = 0; i < inCols.Count; i++)
            {
                if (inCols[i].ColumnDataType == ColumnDataType.Categorical)
                    counter += inCols[i].Statistics.Categories.Count;
                else
                    counter++;
            }

            return counter;
        }

        
        /// <summary>
        /// Returns Count of normalized value. 
        /// Diference Count between numericValues and normalizedValues is in case of Clasification type of data
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetColumnOutputCount_FromNormalizedValue()
        {
            var data = GetData(false);
            var outCols = data.Where(x => x.IsOutput).ToList();

            int counter = 0;
            for (int i = 0; i < outCols.Count; i++)
            {
                if (outCols[i].ColumnDataType == ColumnDataType.Categorical)
                    counter += outCols[i].Statistics.Categories.Count;
                else
                    counter++;
            }

            return counter;
        }

        /// <summary>
        /// Returns Count of input columns
        /// </summary>
        /// <returns></returns>
        public int GetColumnInputCount()
        {
            var data = GetData(false);
            return data.Where(x => !x.IsOutput).Count();
        }
        /// <summary>
        /// Returns Count of output columns
        /// </summary>
        /// <returns></returns>
        public int GetColumnOutputCount()
        {
            var data = GetData(false);
            return data.Where(x => x.IsOutput).Count();
        }
        /// <summary>
        /// Returns list of output columns
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public List<ColumnData> GetColumnsFromOutput(bool testData = false)
        {
            var data = GetData(testData);
            return data.Where(x => x.IsOutput).ToList();
        }
        /// <summary>
        /// Returns list of input columns
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public List<ColumnData> GetColumnsFromInput(bool testData = false)
        {
            var data = GetData(testData);

            return data.Where(x => !x.IsOutput).ToList();
        }

        /// <summary>
        /// Return the type of the output column
        /// </summary>
        /// <returns></returns>
        public ColumnDataType GetOutputColumnType()
        {
            var cols = GetColumnsFromOutput();
            if (cols == null || cols.Count == 0)
                throw new Exception("Experimenta must hav at least one output colum.");

            return cols.LastOrDefault().ColumnDataType;
        }

        /// <summary>
        /// Returns all columns from experiment
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public List<ColumnData> GetColumns(bool testData = false)
        {
            var data = GetData(testData);
            return data;
        }

        /// <summary>
        /// Returns values for output c
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public double[][] GetColumnOutputValues(bool testData)
        {
            //can be more output columns t
            var outCols = GetColumnsFromOutput(testData);

            double[][] values = new double[outCols.Count][];

            for (int j = 0; j < outCols.Count; j++)
            {
                var col = outCols[j];
                values[j] = new double[col.RealValues.Length];
                for (int i = 0; i < col.RealValues.Length; i++)
                    values[j][i] = col.GetNumericValue(i).Value;
            }


            return values;
        }

        /// <summary>
        /// Returns values for input 
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public double[][] GetColumnInputValues(bool testData)
        {
            //
            var inCols = GetColumnsFromInput(testData);

            double[][] values = new double[inCols.Count][];

            for (int j = 0; j < inCols.Count; j++)
            {
                var col = inCols[j];
                values[j] = new double[col.RealValues.Length];
                for (int i = 0; i < col.RealValues.Length; i++)
                    values[j][i] = col.GetNumericValue(i).Value;
            }


            return values;
        }

        /// <summary>
        /// Get 2D array of values from all columns 
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public double[][] GetColumnAllValues(bool testData)
        {
            var data = GetData(testData);

            int numRow = data[0].RowCount;
            int numCol = data.Count;

            double[][] val = new double[numRow][];

            for (int i = 0; i < numRow; i++)
            {
                val[i] = GetRowDataNumeric(i, testData);
            }

            return val;
        }
        #endregion


        #region Row specific Methods
        /// <summary>
        /// Returns number of rows in the experiment from training or testing data
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        public int GetRowCount(bool testData = false)
        {
            if(!testData)
            {
                if (m_trainData != null)
                    return m_trainData.FirstOrDefault().RowCount;
            }
            else
            {
                if (m_testData != null)
                    return m_testData.FirstOrDefault().RowCount;
            }

            throw new Exception (testData ? "Test Data is null.": "Train Data is null.");
        }

        /// <summary>
        /// Returns single row from the Experiment in string format regadless of input and output.
        /// Retrns strings as they stored in the array,
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public string[] GetRowData(int rowIndex, bool testData = false)
        {
            var data=GetData(testData);
           
            string[] str = new string[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                var col = data[i];
                str[i] = col.GetData(rowIndex);
            }

            return str;
        }

        /// <summary>
        /// Retrieve single row data in numeric format regadless of input output vars.
        /// Categorical types returns in their numeric representation
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public double[] GetRowDataNumeric(int rowIndex, bool testData = false)
        {
            var data = GetData(testData);

            double[] str = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                var col = data[i];
                var value = col.GetNumericValue(rowIndex);
                if (value == null)
                    throw new Exception("Experimental data is not numeric and cannot be retrieved.");

                str[i] = value.Value;
            }

            return str;
        }

        /// <summary>
        /// Retrieve output row data. It is always double value
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public double[] GetRowFromOutput(int rowIndex, bool testData = false)
        {
            var outputCols = GetColumnsFromOutput(testData);
            double[] values = new double[outputCols.Count];
            for (int i = 0; i < outputCols.Count; i++)
            {
                var col = outputCols[i];
                var value = col.GetNumericValue(rowIndex);
                if (value == null)
                    throw new Exception("Experimental data is not numeric and cannot be retrieved.");

                values[i] = value.Value;
            }

            return values;
        }

        /// <summary>
        ///  Retrieve inut row data. It is always double value
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public double[] GetRowFromInput(int rowIndex, bool testData = false)
        {
            var outputCols = GetColumnsFromInput(testData);
            double[] values = new double[outputCols.Count];
            for (int i = 0; i < outputCols.Count; i++)
            {
                var col = outputCols[i];
                var value = col.GetNumericValue(rowIndex);
                if (value == null)
                    throw new Exception("Experimental data is not numeric and cannot be retrieved.");

                values[i] = value.Value;
            }

            return values;
        }
        #endregion


        #region From rowIndex to Normalized Data
        /// <summary>
        /// Returns the normalized values for specific rowIndex. It return row for all columns 
        /// regadles of input and output column type with the order as they created
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public double[] GetNormalizedRow(int rowIndex, bool testData = false)
        {
            var data = GetData(testData);
            int count = GetColumnCount_FromNormalizedValue();
            var retVal = new List<double>();
            for (int i = 0; i < data.Count; i++)
            {
                var col = data[i];
                var value = col.GetNormalizedValue(rowIndex);
                if (value == null)
                    throw new Exception("Experimental data is not numeric and cannot be retrieved.");

                retVal.AddRange(value);
            }

            return retVal.ToArray();
        }
        /// <summary>
        /// Retrieve normalized row data. It is always double value
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public double[] GetNormalizedInput(int rowIndex, bool testData=false)
        {
            
            var inCols=GetColumnsFromInput(testData);
            var retVal = new List<double>();

            for (int i = 0; i < inCols.Count; i++)
            {
                var col = inCols[i];
                var value = col.GetNormalizedValue(rowIndex);
                if (value == null)
                    throw new Exception("Experimental data is not numeric and cannot be retrieved.");

                retVal.AddRange(value);
            }

            return retVal.ToArray();
        }
        
        /// <summary>
        /// Retrieve normalized row data. It is always double value
        /// </summary>
        /// <param name="rowIndex">row index</param>
        /// <param name="testData">returns from testing or training data set</param>
        /// <returns></returns>
        public double[] GetNormalizedOutput(int rowIndex, bool testData=false)
        {
            var inCols = GetColumnsFromOutput(testData);
            var retVal = new List<double>();

            for (int i = 0; i < inCols.Count; i++)
            {
                var col = inCols[i];
                var value = col.GetNormalizedValue(rowIndex);
                if (value == null)
                    throw new Exception("Experimental data is not numeric and cannot be retrieved.");

                retVal.AddRange(value);
            }

            return retVal.ToArray();
        }

        public double[][] GetDataForGP(bool testData=false)
        {
            if (testData && m_testData == null)
                return null;

            var rowCount = GetRowCount(testData);
            var colCount = GetColumnInputCount_FromNormalizedValue();

            double[][] data = new double[rowCount][];
            
            for(int i=0; i<rowCount; i++)
            {
                data[i] = new double[colCount+1];//+1 for output column
                var intCol = GetNormalizedInput(i, testData);

                for (int j=0; j<colCount; j++)
                    data[i][j]=intCol[j];

                var output = GetNormalizedOutput(i, testData);
               
                var outputType = GetOutputColumnType();
                if (outputType == ColumnDataType.Numeric)
                    data[i][colCount] = output[0];
                else if (outputType == ColumnDataType.Binary)
                {
                    //binary values 0 or 1
                    data[i][colCount] = output[0];
                }
                else if (outputType == ColumnDataType.Categorical)
                {
                    var outValue = GetDenormalizedOutputRow(output);
                    data[i][colCount] = outValue[0];
                    //for (int k=0; k< output.Length; k++)
                    //{
                    //    if(output[k]==1.0)
                    //    {
                    //        data[i][colCount] = k + 1.0;
                    //        break;
                    //    }                         
                    //}
                }
                else
                    throw new Exception("Invalid output column type.");
            }

            return data;
        }
        #endregion


        #region From Normalized to Real Data

        /// <summary>
        /// Retrieveing real values in string format from normalized data
        /// </summary>
        /// <param name="normalizedRow"></param>
        /// <returns></returns>
        public string[] GetDenormalizedRealRow(double[] normalizedRow)
        {
            if (normalizedRow == null || normalizedRow.Length != GetColumnCount_FromNormalizedValue())
                throw new Exception("Columns does not match with normalized data and cannot be denormalized.");

            string[] str = new string[m_trainData.Count];
            int rowIndex = 0;
            for (int i = 0; i < m_trainData.Count; i++)
            {
                var col = m_trainData[i];
                if(col.ColumnDataType== ColumnDataType.Categorical)//in case of categoric col type 1 of N rule shoould be met 
                 {
                    int cts= col.Statistics.Categories.Count;
                    double[] input= new double[cts];
                    //
                    for(int j=0; j<cts; j++)
                    {
                        input[j] = normalizedRow[rowIndex];
                        rowIndex++;

                    }
                    //
                    var value = col.GetDataFromNormalized(input);
                    str[i] = value;

                }
                else
                {
                    double[] input= new double[1];
                    input[0] = normalizedRow[rowIndex];
                    rowIndex++;
                    //
                    var value = col.GetDataFromNormalized(input);
                    str[i] = value;
                    
                }               
                
            }

            return str;
        }

        
       
        /// <summary>
        /// Returns real numeric row values (all columns) from normalized row (all columns).
        /// </summary>
        /// <param name="normalizedRow"></param>
        /// <returns></returns>
        public double[] GetDenormalizedRow(double[] normalizedRow)
        {
            if (normalizedRow == null || normalizedRow.Length != GetColumnCount_FromNormalizedValue())
                throw new Exception("Column number does not match. normalizedInputRow has diferent elements than number of input columns in the experiment.");

            //
            var eCols = GetColumns();
            var retVal = new double[eCols.Count];

            //
            int rowIndex = 0;
            for (int i = 0; i < eCols.Count; i++)
            {
                var col = eCols[i];
                if (col.ColumnDataType == ColumnDataType.Numeric)
                {
                    retVal[i] = col.GetNumericFromNormalized(normalizedRow[rowIndex]);
                    rowIndex++;
                }
                else if (col.ColumnDataType == ColumnDataType.Categorical)
                {
                    double[] row = new double[col.Statistics.Categories.Count];
                    for (int j = 0; j < col.Statistics.Categories.Count; j++)
                    {
                        row[j] = normalizedRow[rowIndex];
                        rowIndex++;
                    }
                    retVal[i] = col.GetNumericFromNormalized_Category(row);
                }
                else if(col.ColumnDataType == ColumnDataType.Binary)
                {
                    retVal[i] = col.GetNumericFromNormalized_Binary(normalizedRow[rowIndex]);
                    rowIndex++;
                }
                else
                    throw new Exception("The colum type is unknown.");

            }

            return retVal;
        }

        /// <summary>
        /// Return real output row from normalized output row
        /// </summary>
        /// <param name="normalizedOutputRow"></param>
        /// <returns></returns>
        public double[] GetDenormalizedOutputRow(double[] normalizedOutputRow)
        {
            if (normalizedOutputRow == null || normalizedOutputRow.Length != GetColumnOutputCount_FromNormalizedValue())
                throw new Exception("Column number does not match. normalizedInputRow has diferent elements than number of input columns in the experiment.");

            //
            var outputCols = GetColumnsFromOutput();
            var retVal = new double[outputCols.Count];

            //
            int rowIndex = 0;
            for (int i = 0; i < outputCols.Count; i++)
            {
                var col = outputCols[i];
                if (col.ColumnDataType == ColumnDataType.Numeric)
                {
                    retVal[i] = col.GetNumericFromNormalized(normalizedOutputRow[rowIndex]);
                    rowIndex++;
                }
                else if (col.ColumnDataType == ColumnDataType.Categorical)
                {
                    double[] row = new double[col.Statistics.Categories.Count];
                    for (int j = 0; j < col.Statistics.Categories.Count; j++)
                    {
                        row[j] = normalizedOutputRow[rowIndex];
                        rowIndex++;
                    }
                    retVal[i] = col.GetNumericFromNormalized_Category(row);
                }
                else if (col.ColumnDataType == ColumnDataType.Binary)
                {
                    retVal[i] = col.GetNumericFromNormalized_Binary(normalizedOutputRow[rowIndex]);
                    rowIndex++;
                }
                else
                    throw new Exception("The colum type is unknown.");

            }

            return retVal;
        }

        /// <summary>
        /// Denormalizes output value for GP Solver
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double[] GetGPDenormalizedOutputRow(double[] normalizedOutputRow)
        {
            //
            var outputCols = GetColumnsFromOutput();
            var retVal = new double[outputCols.Count];

            //
            int rowIndex = 0;
            for (int i = 0; i < outputCols.Count; i++)
            {
                var col = outputCols[i];
                if (col.ColumnDataType == ColumnDataType.Numeric)
                {
                    retVal[i] = col.GetNumericFromNormalized(normalizedOutputRow[rowIndex]);
                    rowIndex++;
                }
                else if (col.ColumnDataType == ColumnDataType.Categorical || col.ColumnDataType == ColumnDataType.Binary)
                {
                    retVal[i] = getCategoryFromNumeric(normalizedOutputRow[i], outputCols[0].Statistics.Categories.Count);
                    
                }
                else
                    throw new Exception("The colum type is unknown.");

            }

            return retVal;
        }

        /// <summary>
        /// calculate category value by converting numeric value
        /// </summary>
        /// <param name="numericValue"></param>
        /// <param name="classCount"></param>
        /// <returns></returns>
        public static double getCategoryFromNumeric(double numericValue, int classCount)
        {
            //calculate sigmoid for the fitenss
            var val1 = Math.Exp(-1.0 * numericValue);
            var sigm = classCount * (1.0 / (1.0 + val1));

            //we dont want boundary values
            if (sigm == 0 || sigm == classCount)
                return double.NaN;
            var retVal = Math.Truncate(sigm);
            return retVal;
        }

        /// <summary>
        /// Returns real input row from normalized input row
        /// </summary>
        /// <param name="normalizedInputRow"></param>
        /// <returns></returns>
        public double[] GetDenormalizedInputRow(double[] normalizedInputRow)
        {
            if (normalizedInputRow == null || normalizedInputRow.Length != GetColumnInputCount_FromNormalizedValue())
                throw new Exception("Column number does not match. normalizedInputRow has diferent elements than number of input columns in the experiment.");

            //
            var outputCols = GetColumnsFromInput();
            var retVal = new double[outputCols.Count];

            //
            int rowIndex = 0;
            for (int i = 0; i < outputCols.Count; i++)
            {
                var col = outputCols[i];
                if(col.ColumnDataType== ColumnDataType.Numeric)
                {
                    retVal[i] = col.GetNumericFromNormalized(normalizedInputRow[rowIndex]);
                    rowIndex++;
                }           
                else if (col.ColumnDataType == ColumnDataType.Categorical)
                {
                    double[] row= new double[col.Statistics.Categories.Count];
                    for (int j = 0; j < col.Statistics.Categories.Count;j++ )
                    {
                        row[j] = normalizedInputRow[rowIndex];
                        rowIndex++;
                    }
                    retVal[i] = col.GetNumericFromNormalized_Category(row);
                }
                else if (col.ColumnDataType == ColumnDataType.Binary)
                {
                    retVal[i] = col.GetNumericFromNormalized_Binary(normalizedInputRow[rowIndex]);
                    rowIndex++;
                }
                else
                    throw new Exception("The colum type is unknown.");
                    
            }

            return retVal;
        }
        #endregion




        #region Loading from File and Preparing the Experiment
        /// <summary>
        /// Initialize Experiment with string data, with specific formating 
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="columDelimiter"></param>
        /// <param name="isFirstRowHeader"></param>
        /// <param name="isFloatingPoint"></param>
        public void LoadExperiment(string strData, char[] columDelimiter, bool isFirstRowHeader, bool isFloatingPoint=true)
        {
            //define the row
            string[] rows = strData.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            //Define the columns
            var colCount = rows[0].Split(columDelimiter).Count();
            var rowCount = rows.Length;
            int headerCount = 0;
            ///
            m_strHeader = null;
            if(isFirstRowHeader)
               headerCount++;

            m_strData = new string[rowCount-headerCount][];
            
            //
            for (int i = 0; i < rowCount; i++)
            {
                var row = rows[i].Split(columDelimiter);

                //column creation for each row
                if (i == 0 && isFirstRowHeader)
                     m_strHeader= new string[colCount];
                else 
                    m_strData[i - headerCount] = new string[colCount];
               
                if(row.Length != colCount)
                {
                    MessageBox.Show("Data is not consistant.", "GPdotNET");
                    m_strData = null;
                    return;
                }
                //column enumeratrion
                for (int j = 0; j < colCount; j++)
                {
                    //value format
                    var value = "";
                    if(string.IsNullOrEmpty(row[j]))
                        value = "n/a";
                    else
                        value = row[j];

                    //
                    if (i == 0 && isFirstRowHeader)
                        m_strHeader[j] = value;
                    else 
                        m_strData[i - headerCount][j] = value;
                                        
                       
                }
            }


        }

        /// <summary>
        /// Prepares the data to use in Modelling.It merges user defined column properties with imported data
        /// Preparation is conversion string aray in to meaningfull columns data
        /// </summary>
        /// <param name="colProp"></param>
        /// <param name="precentTraining"></param>
        public bool Prepare(List<ColumnProperties> colProp, int rows, bool ispresentige=true)
        {

            //remove all row which is marked Ignore for missing value
            string[][] strData= ignoreRowsWithMissingValues(colProp);

            //row col count
            var colCount = colProp.Count;
            var rowCount = strData.Length;

            //
            int trainCount = 0;
            if (ispresentige)
                trainCount = (int)Math.Ceiling(rowCount * ((100-rows) / 100.0));
            else
                trainCount = rowCount - rows;

            int testCount = rowCount - trainCount;

            if(trainCount<testCount)
            {
                MessageBox.Show("GPdotNET", "Invalid number of testing data.");
                return false;
            }



            var dataTrn = strData.Skip(0).Take(trainCount).ToArray();
            var dataTst = strData.Skip(trainCount).Take(testCount).ToArray();
            

            if (dataTrn != null)
            {
                if (m_trainData == null)
                    m_trainData = new List<ColumnData>();
                else
                    m_trainData.Clear();
                

                //first go thru all col
                for (int j = 0; j < colCount; j++)
                {
                    //skip column if type is string or parameter is ignored
                    if (colProp[j].ColType.Contains("string") || colProp[j].ParamType == "ignore")
                        continue;

                    var col = CreateColumn(colProp[j]);

                    //go thru all rows for each col
                    col.RealValues = new string[trainCount];
                    for (int i = 0; i < dataTrn.Length; i++)
                        col.RealValues[i] = dataTrn[i][j];
                    //
                    col.InitializeData();

                    m_trainData.Add(col);


                }

            }
            //test data
            if (dataTst != null && dataTst.Length > 0)
            {
                if (m_testData == null)
                    m_testData = new List<ColumnData>();
                else
                    m_testData.Clear();

                //first go thru all col
                for (int j = 0; j < colCount; j++)
                {
                    //skip column if type is string or parameter is ignored
                    if (colProp[j].ColType == "string" || colProp[j].ParamType == "ignore")
                        continue;

                    var col = CreateColumn(colProp[j]);

                    //go thru all rows for each col
                    col.RealValues = new string[dataTst.Length];
                    for (int i = 0; i < dataTst.Length; i++)
                        col.RealValues[i] = dataTst[i][j];
                    //
                    var trainCol = m_trainData.Where(x => x.Name == col.Name).FirstOrDefault();
                    if (trainCol != null)
                    {
                        col.InitializeAsTestData(trainCol.Statistics);
                        m_testData.Add(col);
                    }
                }

            }
            else
                m_testData = null;

            return true;
        }

        private string[][] ignoreRowsWithMissingValues(List<ColumnProperties> colProp)
        {
            List<int> ignoredIndex = new List<int>();
            
            //parse all string data remember row index for missingValues 
            for (int j = 0; j < colProp.Count; j++ )
            {
                //chech if current column ignores row with missing value
                if (colProp[j].MissingValue == "Ignore" && !colProp[j].IsIngored)
                {
                    for(int i=0;i<m_strData.Length; i++)
                    {
                        bool retVal = ColumnData.isMissingValue(m_strData[i][j]);
                        if (retVal)
                            ignoredIndex.Add(i);
                    }
                }
            }

            //go thru all rows and remove those with remembered index
            int ignoredRows=ignoredIndex.Distinct().ToList().Count;
            string [][] filteredData= new string[m_strData.Length-ignoredRows][];
            int index=0;
            for (int i = 0; i < m_strData.Length; i++)
            {
                if(!ignoredIndex.Contains(i))
                {
                    filteredData[index] = m_strData[i];
                    index++;
                }
            }

            return filteredData;
        }


        /// <summary>
        /// Main method for Experiment initialization. It requires train and test data. Test data can be null
        /// </summary>
        /// <param name="trainData"></param>
        /// <param name="testData"></param>
        /// <param name="normalization"></param>
        public void InitExperiment(double[][] trainData, double[][] testData, NormalizationType normalization= NormalizationType.Gauss)
        {
            try
            {
                //

                m_trainData = new List<ColumnData>();
                for (int j = 0; j < trainData[0].Length; j++) 
                {
                    bool isOutput=false;

                    if (j + 1 == trainData[0].Length)
                        isOutput = true;

                    ColumnData dat = new ColumnData(isOutput);
                    var col = new string[trainData.Length];
                    for (int i = 0; i < trainData.Length; i++)
                    {
                        col[i] = trainData[i][j].ToString();
                    }
                   
                    dat.SetData(col, normalization);
                    m_trainData.Add(dat);
                }

                if (testData != null)
                {

                    m_testData = new List<ColumnData>();
                    for (int j = 0; j < testData[0].Length; j++)
                    {
                        bool isOutput = false;

                        if (j + 1 == testData[0].Length)
                            isOutput = true;

                        ColumnData dat = new ColumnData(isOutput);
                        var col = new string[testData.Length];
                        for (int i = 0; i < testData.Length; i++)
                        {
                            col[i] = testData[i][j].ToString();
                        }

                        dat.SetData(col, normalization);
                        m_testData.Add(dat);
                    }
                }

               // m_testData = testData;

                //code data and calculate statistic
                InitiaizeData();
                InitiaizeTestData();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Helper static method for fast converzn data in to double array
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static double[][] LoadData(string filePath, char delimeter = ';')
        {
            try
            {
                //define the row
                string[] rows = GetRowsFromText(filePath, delimeter);

                //Define the columns
                var colCount = rows[0].Split(delimeter).Count();
                var rowCount = rows.Length;

                double[][] data = new double[rowCount][];

                for (int i = 0; i < rowCount; i++)
                {
                    var row = rows[i].Split(delimeter);
                    data[i] = new double[colCount];
                    for (int j = 0; j < colCount; j++)
                    {
                        double v = 0;
                        if (double.TryParse(row[j],NumberStyles.Number, CultureInfo.InvariantCulture,out v))
                            data[i][j] = v;
                    }
                }

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Check if test data is available
        /// </summary>
        /// <returns></returns>
        public bool IsTestDataExist()
        {
            if (m_testData != null && m_testData.Count > 0)
                return true;
            else
                return false;
        }
        #endregion


      
        #endregion

        #region Private Members
        /// <summary>
        /// initialization of train data
        /// </summary>
        private void InitiaizeData()
        {

            for (int i = 0; i < m_trainData.Count; i++ )
            {
                ColumnData col= m_trainData[i];
                //set column name is doesnt exist
                if (string.IsNullOrEmpty(col.Name))
                    col.Name = i + 1 >= m_trainData.Count ? "y" + (i + 1).ToString() : "x" + (i + 1).ToString();

                col.InitializeData();
            }
        }
        /// <summary>
        /// Initialization of test data
        /// </summary>
        private void InitiaizeTestData()
        {
            if (m_testData == null)
                return;
            for (int i = 0; i < m_testData.Count; i++ )
            {
                //get stat from train column
                var tcol = m_trainData[i];
                var stat = tcol.Statistics;

                //init test column
                var trainCol = m_testData[i];
                if (string.IsNullOrEmpty(trainCol.Name))
                    trainCol.Name = tcol.Name;

                trainCol.InitializeAsTestData(stat);
            }
        }

        /// <summary>
        /// Creates columns based of the columns properties argument
        /// </summary>
        /// <param name="colProp"></param>
        /// <returns></returns>
        private ColumnData CreateColumn(ColumnProperties colProp)
        {
            //determine column type
            ColumnDataType colType;
            if (colProp.ColType == "numeric")
                colType = ColumnDataType.Numeric;
            else if (colProp.ColType == "binary")
                colType = ColumnDataType.Binary;
            else if (colProp.ColType == "categorical")
                colType = ColumnDataType.Categorical;
            else
                colType = ColumnDataType.Unknown;

            
            //create column data type
            var isOutput = colProp.ParamType == "output";

            ColumnData col = new ColumnData(isOutput);

            if (colProp.NormType == "MinMax")
                col.SetNormalization(NormalizationType.MinMax);
            else if (colProp.NormType == "Gauss")
                col.SetNormalization(NormalizationType.Gauss);
            else if (colProp.NormType == "none")
                col.SetNormalization(NormalizationType.None);
            else
                col.SetNormalization(NormalizationType.Custom);


            //set missing value action
            if (colProp.MissingValue == "Ignore")
                col.MissingValue = MissingRowValue.Ignore;
            else if (colProp.MissingValue == "Average")
                col.MissingValue = MissingRowValue.Average;
            else if (colProp.MissingValue == "Max")
                col.MissingValue = MissingRowValue.Max;
            else
                col.MissingValue = MissingRowValue.Min;
            
            //set column name and type
            col.Name = colProp.ColName;
            col.ColumnDataType=colType;
            return col;
        }

        /// <summary>
        /// Retrieve content from file as array of rows
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        private static string[] GetRowsFromText(string filePath, char delimeter)
        {
            var buffer = "";


            buffer = FileNameUtility.ReadTextFile(filePath);

            //define the row
            string[] rows;

            //extract data but remove comments
            rows = buffer.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(x => (x != null && x.Length > 0 && x[0] != '!')).Select(x => x).ToArray();

            return rows;
        }

        /// <summary>
        /// Load experiment from file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimeter"></param>
        /// <param name="normalizationType"></param>
        /// <returns></returns>
        private List<ColumnData> LoadDataFromFile(string filePath, char delimeter = ';', NormalizationType normalizationType = NormalizationType.Gauss)
        {
            try
            {
                //define the row
                string[] rows = GetRowsFromText(filePath, delimeter);

                //Define the columns
                var colCount = rows[0].Split(delimeter).Count();
                var rowCount = rows.Length;

                string[][] strdata = new string[colCount][];

                //Transform experimental data from row->col in to col->ros
                for (int i = 0; i < colCount; i++)
                {
                    strdata[i] = new string[rows.Length];

                    for (int j = 0; j < rowCount; j++)
                    {
                        var row = rows[j].Split(delimeter);
                        strdata[i][j] = row[i];

                    }

                }

                //Contruct exprerimental data
                var colsData = new List<ColumnData>();
                for (int i = 0; i < colCount; i++)
                {
                    var isOutput = i + 1 >= colCount;
                    ColumnData col = new ColumnData(isOutput);
                    col.SetData(strdata[i], normalizationType);
                    colsData.Add(col);
                }


                return colsData;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Helper for getting right columns from the experiment
        /// </summary>
        /// <param name="testData"></param>
        /// <returns></returns>
        private List<ColumnData> GetData(bool testData)
        {
            if (testData)
                return m_testData;
            else
                return m_trainData;
        }
        #endregion


        public string GetExperimentToString(List<ColumnProperties> list, int testProcent, bool isPresentige = true)
        {
            string retVal = "";
            var cols = list;
            try
            {
                var presentige = isPresentige ? "0" : "1";
                //test procent
                retVal += testProcent.ToString(CultureInfo.InvariantCulture) + ":"+presentige + ";";

                //number of columns
                retVal += cols.Count.ToString(CultureInfo.InvariantCulture) + ";";

                //columnProperties
                foreach (ColumnProperties col in cols)
                {
                    retVal += col.ToString() + ";";
                }
                
                foreach(var ss in m_strData)
                {
                    foreach(var s in ss)
                        retVal += s + ";";
                }
                
                return retVal;
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static Experiment GetExperimentFromString(string strExperiment)
        {
            var pstr = strExperiment.Split(';');
            int testProcent = 0;
            int presentige = 0;
            try
            {
                //test procent
                var p = pstr[0].Split(':');
                if(p.Length==2)
                {
                    if (p[1] == "1")
                        presentige = 1;
                }
                int temp = 0;
                if (!int.TryParse(p[0], out temp))
                    temp = 0;
                testProcent = temp;

                int numCol;
                int numRow;
                //number of columns
                temp = 0;
                if (!int.TryParse(pstr[1], out temp))
                    temp = 0;
                numCol = temp;

                //number of rows
                temp = 0;
                if (!int.TryParse(pstr[2], out temp))
                    temp = 0;
                numRow = temp;

                //columnProperties
                List<ColumnProperties> cols = new List<ColumnProperties>();
                int si=3;
                for (int i = 0; i < numCol; i++ )
                {
                    var col = new ColumnProperties();
                    col.ColIndex = i + 1;
                    si+=i+1;
                    col.ColName=pstr[si];
                    si += i + 1;
                    col.ColType=pstr[si];
                    si += i + 1;
                    col.ParamType=pstr[si];
                    si += i + 1;
                    col.NormType=pstr[si];
                    si += i + 1;
                    col.MissingValue=pstr[si];
                }


                //alocate data
                string[][] str= new string[numRow][];
                for (int i = 0; i < numRow; i++)
                    str[i]= new string[numCol];

                //load data
                int k = si + 1;
                for (int j = 0; j < numCol; j++)
                {
                    for (int i = 0; i < numRow; i++)
                    {
                        str[i][j]= pstr[k];
                        k++;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
