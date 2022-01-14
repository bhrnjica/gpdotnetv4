using GPdotNET.Core.System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GPdotNET.Core.Statistics;
namespace GPdotNET.Core.Experiment
{
    //Class for defining ColumnData
    public class ColumnProperties
    {
        public int ColIndex { get; set; }
        public string ColName { get; set; }
        public string ColType { get; set; }
        public string ParamType { get; set; }
        public string NormType { get; set; }
        public string MissingValue { get; set; }
        public bool IsIngored {
            get
            {
                return (ColType.Contains("string") || ParamType.Contains(ParameterType.Ignored.ToString()));
            }
        }

        public override string ToString()
        {
            string retVal = "";
            retVal += ColIndex.ToString(CultureInfo.InvariantCulture)+";";
            retVal += ColName + ";";
            retVal += ColType + ";";
            retVal += ParamType + ";";
            retVal += NormType + ";";
            retVal += MissingValue;

            return retVal;
        }
        
    }

    //Statistic for ColumnData
    public class Statistics
    {
        public double Mean;
        public double Median;
        public double Mode;
        public double Range;
        public double Min;
        public double Max;
        public double StdDev;
        public List<string> Categories;
    }

    //Type of the columndata
    public enum ColumnDataType  
    {
        Unknown=0,
        Numeric,
        Binary,
        Categorical
    }

    //Normalization method
    public enum NormalizationType
    {
        MinMax=0,
        Gauss,
        Custom,
        Identity,
        None
    }

    //Parameter type
    public enum ParameterType
    {
        Input, //-treat column as input parameter or feature
        Output, // - treat column ss output value or lable
        Ignored, // ignore columns in modelling
    }

    public enum MissingRowValue
    {
        Ignore,//remove the row from the experiment
        Average,//recalculate the column and put average value in all missing rows
        Max,//recalculate the column and put Max value in all missing rows
        Min //recalculate the column and put Min value in all missing rows
    }

    /// <summary>
    /// Represent the variable of the experiment.
    /// </summary>
    public class ColumnData
    {
        #region Fields
        NormalizationType   m_normalizationType;//normalization type of colum values
        ColumnDataType      m_ColType;//type of the column
        ParameterType       m_ParamType;
        Statistics          m_Statistics;//statistic of the column
        MissingRowValue     m_MissingValue; //MissingValue in row 
        string[]            m_RealValues;//real  column value exstracted from the file in string format
        double[]            m_NumericValues;//if the colum  is numeric it holds numeric representation of the real value
        double[][]          m_NormalizedValues;// before apply to the solver column has to be normalized

        public ColumnData(bool isOutput = false)
        {
            if (isOutput)
                m_ParamType = ParameterType.Output;
            m_ColType = ColumnDataType.Numeric;
        }

        #endregion

        #region Properties
        public NormalizationType Normalization { get { return m_normalizationType; } }
        internal bool           IsTest{get;private set;} // if it is test data, normalization must be performed by external stats parameters
        public bool             IsOutput { get { return m_ParamType == ParameterType.Output; } }
        internal string[]       RealValues { get { return m_RealValues; } set { m_RealValues = value; } }
        public string           Name { get; set; }//Name of the column in experiment
        internal int            RowCount { get { return m_RealValues==null ? 0 : m_RealValues.Length; } }
        public ColumnDataType   ColumnDataType { get { return m_ColType; } set { m_ColType = value; } }
        public Statistics       Statistics { get { return m_Statistics; } }

        internal MissingRowValue MissingValue { get { return m_MissingValue; } set { m_MissingValue = value; } }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Set values for the Column in string format, by passing the normalization type
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="normalization"></param>
        internal void SetData(string[] cols, NormalizationType normalization= NormalizationType.MinMax)
        {
            m_normalizationType = normalization;
            m_RealValues = new string[cols.Length];
            Array.Copy(cols, this.m_RealValues, cols.Length);
        }

        public string GetData(int rowIndex)
        {
            if (m_RealValues.Length > rowIndex && rowIndex >= 0)
                return m_RealValues[rowIndex];
            else
                return null;
        }
       
        internal double? GetNumericValue(int rowIndex)
        {
            if (m_NumericValues.Length > rowIndex && rowIndex >= 0)
                return m_NumericValues[rowIndex];
            else
                return null;
        }

        /// <summary>
        /// Return array of normalized value for specific row
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        internal double[] GetNormalizedValue(int rowIndex)
        {
            if (m_NormalizedValues.Length > rowIndex && rowIndex >= 0)
                return m_NormalizedValues[rowIndex];
            else
                return null;
        }

        internal string GetDataFromNormalized(double[] normalizedValue, Statistics normParams = null)
        {
            switch (m_ColType)
            {
                case ColumnDataType.Unknown:
                    throw new Exception("Cannot resolve column type.");

                case ColumnDataType.Numeric:
                    {
                        double val = GetNumericFromNormalized(normalizedValue[0], normParams);
                        return val.ToString(CultureInfo.InvariantCulture);
                    }
                case ColumnDataType.Binary:
                    {
                        string val = GetBinaryFromNormalized(normalizedValue[0]);
                        return val.ToString(CultureInfo.InvariantCulture);
                    }
                case ColumnDataType.Categorical:
                    {
                        string val = GetCategoricalFromNormalized(normalizedValue);
                        return val;
                    }
                default:
                    throw new Exception("Cannot resolve column type.");

            }
        }
        
        /// <summary>
        /// Calculate real values from normalized values. Normalized data are normalized by testing data statistics.
        /// </summary>
        /// <param name="normalizedValue"></param>
        /// <param name="normParams">parameters in case of testing data</param>
        /// <returns></returns>
        internal double GetNumericFromNormalized(double normalizedValue, Statistics normParams=null)
        {
            if (ColumnDataType != Core.Experiment.ColumnDataType.Numeric)
                throw new Exception("Column type is not Numeric.");

            //check if train or test data should be denormalized
            Statistics stat=null;
            if (normParams != null)
                stat = normParams;
            else
                stat = m_Statistics;

            //perform denormalization
            double retVal=0;
            if(m_normalizationType== NormalizationType.Gauss)
                retVal = normalizedValue * stat.StdDev + stat.Mean;
            else if(m_normalizationType == NormalizationType.MinMax)
                retVal = normalizedValue * ((stat.Max - stat.Min)) + stat.Min;
            else if(m_normalizationType== NormalizationType.Custom)
                 {
                    double factor   = IsOutput ? 1.7 : 2.0;
                    double trashold = IsOutput ? 0.85 : 1.0;
                    retVal= (normalizedValue + trashold) / (factor / (m_Statistics.Max - m_Statistics.Min)) + m_Statistics.Min;
                }
            else if (m_normalizationType == NormalizationType.None)
                retVal = normalizedValue;
            else
                throw new Exception("Unknown normalization data type.");

           return retVal;
        }

        internal double GetNumericFromNormalized_Category(double[] normalizedValues)
        {
            if (ColumnDataType != Core.Experiment.ColumnDataType.Categorical)
                throw new Exception("Column type is not Categorical.");

            if (normalizedValues != null && normalizedValues.Length != Statistics.Categories.Count)
                throw new Exception("Inconsistent number of category");

            double maxVal = normalizedValues[0];
            int index = 0;
            for(int i=0;i<normalizedValues.Length; i++)
            {
                if(maxVal< normalizedValues[i])
                {
                    maxVal = normalizedValues[i];
                    index = i;
                }
                   
            }

            return index;//because numeric value of category column is index of categoryName
        }

        internal double GetNumericFromNormalized_Binary(double normalizedValue)
        {
            if (ColumnDataType != Core.Experiment.ColumnDataType.Binary)
                throw new Exception("Column type is not Binary.");

            if (normalizedValue < 0.5)
                return 0;
            else
                return 1.0;
        }

        internal void InitializeAsTestData(Statistics stat)
        {
            IsTest = true;
            InitializeData(stat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns row count number</returns>
        internal void InitializeData(Statistics stat = null)
        {
            if (m_RealValues != null && m_RealValues.Length > 0)
            {
                //m_ColType = GetColumType(m_RealValues[0]);
                switch (m_ColType)
                {
                    case ColumnDataType.Unknown:
                        throw new Exception("Cannot resolve column type.");

                    case ColumnDataType.Numeric:
                        RealDataToNumeric(stat);
                        break;
                    case ColumnDataType.Binary:
                        RealDataToBinary(stat);
                        break;
                    case ColumnDataType.Categorical:
                        RealDataToCategoric(stat);
                        break;
                    default:
                        throw new Exception("Cannot resolve column type.");

                }
            }

            //
            return;
        }

        internal void SetNormalization(NormalizationType normalizationType)
        {
            m_normalizationType = normalizationType;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// calculates statistic for the column
        /// </summary>
        private void CalculateStats()
        {
            if (m_RealValues == null)
                throw new Exception("Column is empty.");

            if (m_Statistics == null)
                m_Statistics = new Statistics();

            m_Statistics.Mean = m_NumericValues.Where(x => !double.IsNaN(x)).Average();
            m_Statistics.Max = m_NumericValues.Where(x => !double.IsNaN(x)).Max();
            m_Statistics.Min = m_NumericValues.Where(x => !double.IsNaN(x)).Min();

            //replace missing value
            replaceMissingValue();

            //calculate median= middle value from the array
            var median = m_NumericValues.MedianOf();
            m_Statistics.Median = median;

            //range value
            m_Statistics.Range = m_Statistics.Max - m_Statistics.Min;

            //Standard deviation
            m_Statistics.StdDev = m_NumericValues.Stdev();
        }

        private void replaceMissingValue()
        {
            //replace missingValues
            for (int i = 0; i < m_NumericValues.Length; i++)
            {
                if (double.IsNaN(m_NumericValues[i]))
                {
                    if (m_MissingValue == MissingRowValue.Average)
                        m_NumericValues[i] = Statistics.Mean;
                    else if (m_MissingValue == MissingRowValue.Max)
                        m_NumericValues[i] = Statistics.Max;
                    else if (m_MissingValue == MissingRowValue.Min)
                        m_NumericValues[i] = Statistics.Min;
                    else
                        throw new Exception("Missing value for column=" + Name + " is not defined.");

                    setRealValueFromNumeric(i, m_NumericValues[i]);
                }
            }
        }

        private void setRealValueFromNumeric(int index, double value)
        {
            switch (m_ColType)
            {
                case ColumnDataType.Unknown:
                    throw new Exception("Column type is not known.");
                case ColumnDataType.Numeric:
                    m_RealValues[index] = value.ToString();
                    break;
                case ColumnDataType.Binary:
                    if(value<0.5)
                        m_RealValues[index]= Statistics.Categories[0];
                    else
                        m_RealValues[index] = Statistics.Categories[1];
                    break;
                case ColumnDataType.Categorical:
                    int cat=(int)value;
                    if(cat>Statistics.Categories.Count-1)
                        cat=Statistics.Categories.Count-1;
                    m_RealValues[index] = Statistics.Categories[cat];
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// based on the normalization paameters and values returns the coresponded category
        /// Converts normalized value in to categorical values
        /// </summary>
        /// <param name="normalizedValue"></param>
        /// <param name="normParams"></param>
        /// <returns></returns>
        private string GetCategoricalFromNormalized(double[] normalizedValue, Statistics normParams = null)
        {
           //
            Statistics stat = null;
            if (normParams != null)
                stat = normParams;
            else
                stat = m_Statistics;
            double maxVal = normalizedValue[0];
            int index=0;
            for (int i = 0; i < normalizedValue.Length; i++ )
            {
                if(normalizedValue[i]>maxVal)
                {
                    maxVal = normalizedValue[i];
                    index = i;
                }
                  
            }
            return stat.Categories[index];
        }

        /// <summary>
        /// Converts numeric value (which represent index of the category) in to Category values
        /// </summary>
        /// <param name="numeric"></param>
        /// <param name="normParams"></param>
        /// <returns></returns>
        public string GetCategoryFromNumeric(double numeric, Statistics normParams)
        {
            string catValue = "";
            Statistics stat = null;
            if (normParams == null)
                stat = m_Statistics;
            else
                stat = normParams;

            for (int i = 0; i < stat.Categories.Count; i++)
            {
                if (i <= numeric && numeric < i + 1)
                {
                    catValue = stat.Categories[i];
                    break;
                }
            }

            return catValue;
        }

        /// <summary>
        /// Converts numeric value (which represent index of the category) in to Binary Category values
        /// </summary>
        /// <param name="numeric"></param>
        /// <param name="normParams"></param>
        /// <returns></returns>
        public string GetBinaryClassFromNumeric(double numeric, Statistics normParams)
        {
             
            Statistics stat = null;
            if (normParams == null)
                stat = m_Statistics;
            else
                stat = normParams;

            if(numeric<0.5)
                return stat.Categories[0];
            else
                return stat.Categories[1];

        }

        /// <summary>
        /// Converts normalized value (0 -1) in to binary representation of te column value
        /// </summary>
        /// <param name="normalizedValue"></param>
        /// <param name="normParams"></param>
        /// <returns></returns>
        private string GetBinaryFromNormalized(double normalizedValue, Statistics normParams = null)
        {
            var numeric = GetNumericFromNormalized(normalizedValue, normParams);
            string catValue = GetCategoryFromNumeric(numeric, normParams);
            return catValue;
        }

        /// <summary>
        /// Code real values in to numeric data typa
        /// </summary>
        private void RealDataToNumeric(Statistics stat = null)
        {
            //Create numeric data
            m_NumericValues = new double[m_RealValues.Length];
            for (int i = 0; i < m_RealValues.Length; i++)
            {
                string str = m_RealValues[i];
                double v;
                if (double.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out v))
                    m_NumericValues[i] = v;
                else if(isMissingValue(str))
                    m_NumericValues[i] = double.NaN;
                else
                    throw new Exception("The Values of " + Name + " column cannot be converted to numeric value. Try to change the type of the column.");
            }
            //calculate stats
            if (stat == null)
                CalculateStats();
            else//this is testing data we used stat from training in order to get correct normalization
            {
                m_Statistics = stat;
                replaceMissingValue();
            }
                

            //normalize values
            m_NormalizedValues = new double[m_RealValues.Length][];
            for (int i = 0; i < m_RealValues.Length; i++)
            {
                m_NormalizedValues[i]= new double[1];

                if (m_normalizationType == NormalizationType.Gauss)
                    m_NormalizedValues[i][0] = (m_NumericValues[i] - m_Statistics.Mean) / m_Statistics.StdDev;
                else if (m_normalizationType == NormalizationType.MinMax)
                    m_NormalizedValues[i][0] = (m_NumericValues[i] - m_Statistics.Min) / (m_Statistics.Max - m_Statistics.Min);
                else if (m_normalizationType == NormalizationType.None)
                    m_NormalizedValues[i][0] = m_NumericValues[i];
                else if (m_normalizationType == NormalizationType.Custom)
                {
                    double factor = IsOutput ? 1.7 : 2.0;
                    double trashold = IsOutput ? 0.85 : 1.0;
                    m_NormalizedValues[i][0] = (m_NumericValues[i] - m_Statistics.Min) * (factor / (m_Statistics.Max - m_Statistics.Min)) - trashold;
                }

                else
                    throw new Exception("Unknown normalization data type.");
            }
        }

        /// <summary>
        /// Converts real values in to numeric and normalized values, based on the statistic parameters
        /// </summary>
        /// <param name="stat"></param>
        private void RealDataToBinary(Statistics stat = null)
        {
            //Create numeric and normalized  data
            m_NumericValues = new double[m_RealValues.Length];
            m_NormalizedValues = new double[m_RealValues.Length][];
            //define binary column
            //first value should be 0 and second 1
            List<string> classes = null;
            if (stat == null)
            {
                classes = m_RealValues.Where(x => x != "n/a").Distinct().OrderBy(x => x).ToList();
                if (m_Statistics == null)
                    m_Statistics = new Statistics();
                m_Statistics.Categories = classes;
            }
            else//usualy this is data for testing and validation
                classes = stat.Categories;

            if (classes.Count != 2)
                throw new Exception("Binary column type should has only 2 classes.");


            for (int i = 0; i < m_RealValues.Length; i++)
            {
                var val = m_RealValues[i];
                m_NormalizedValues[i]= new double[1];

                var c = classes.Where(x => x == val).FirstOrDefault();
                if (c != null)
                {
                    //in case of binary column type real and normalized value are the same
                    m_NumericValues[i] = classes.IndexOf(c);
                    m_NormalizedValues[i][0]= m_NumericValues[i];
                }
                else if(isMissingValue(val))
                    m_NumericValues[i] = double.NaN;//missing value
                else
                    throw new Exception("Data in " + Name + " column cannot be null.");
            }

            //calculate stats
            if (stat == null)
                CalculateStats();
            else//this is testing data we used stat from training in order to get correct normalization
            {
                m_Statistics = stat;
                replaceMissingValue();
            }
                

        }

        public static bool isMissingValue(string val)
        {
            return string.IsNullOrEmpty(val) || val.Trim() == "n/a" || val.Trim() == "-";
        }

        /// <summary>
        /// Converts real categories in to numeric and normalized values. In case of normalized value it apply 1 of N category rule.
        /// </summary>
        /// <param name="stat"></param>
        private void RealDataToCategoric(Statistics stat = null)
        {
            //Create numeric data
            m_NumericValues = new double[m_RealValues.Length];

            //define clasess
            //for each classes we asign natural value
            //1. class - 0
            //2. class - 1
            //3. class - 2
            //...
            List<string> classes = null;
            if (stat == null)
            {
                classes = m_RealValues.Where(x=>x!="n/a").Distinct().OrderBy(x => x).ToList();
                if (m_Statistics == null)
                    m_Statistics = new Statistics();
                m_Statistics.Categories = classes;
            }
            else//usualy this is data for testing and validation
                classes = stat.Categories;


            for (int i = 0; i < m_RealValues.Length; i++)
            {
                var val = m_RealValues[i];

                var c = classes.Where(x => x == val).FirstOrDefault();
                if (c != null)
                {
                    m_NumericValues[i] = classes.IndexOf(c);
                }
                else if(val=="n/a" && m_MissingValue!= MissingRowValue.Ignore)
                    m_NumericValues[i] = double.NaN;//missing value
                else
                    throw new Exception("Data in " + Name + " column cannot be null.");
            }

            //calculate stats
            if (stat == null)
                CalculateStats();
            else//this is testing data we used stat from training in order to get correct normalizationž
            {
                m_Statistics = stat;
                replaceMissingValue();
            }

            //
            NormalizeCategoricColumn();
            
        }

        /// <summary>
        /// COnverts numeric values (index of category collection in to 1 of N (0,1) values)
        /// The method creates array which has length of Category count.
        /// Example: Red, Gree, Blue - 3 categories  - real values
        ///             0,  1,  2    - 3 numbers     - numeric values
        ///             
        /// Normalized values for Blues category:
        ///          Blue  =  (0,0,1)  - three values which sum is 1,
        ///          Red   =  (1,0,0)
        ///          Green =  (0,1,0)
        /// </summary>
        private void NormalizeCategoricColumn()
        {
            //normalize values
            m_NormalizedValues = new double[m_RealValues.Length][];
            //
            for (int i = 0; i < m_RealValues.Length; i++)
            {
                m_NormalizedValues[i] = new double[m_Statistics.Categories.Count];

                for(int j=0; j< m_Statistics.Categories.Count; j++)
                {
                    if (j == m_NumericValues[i])
                        m_NormalizedValues[i][j] = 1;
                    else
                        m_NormalizedValues[i][j] = 0;
                }
            }
        }

        /// <summary>
        /// Based of the value it analizes and determines column type 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private ColumnDataType DetermineColumType(string strValue)
        {
            ////obsolute
            double val;
            if (double.TryParse(strValue, NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                return ColumnDataType.Numeric;
            else
            {
                //Count how meny distinct values
                int cats = m_RealValues.Distinct().Count();

                if (cats > 2)//if nore than two
                    return ColumnDataType.Categorical;
                else if (cats == 2)//if only two 
                    return ColumnDataType.Binary;
                else
                    return ColumnDataType.Unknown;
            }

        }
        #endregion
    }

    
}
