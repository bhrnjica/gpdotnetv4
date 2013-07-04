using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNET.Core;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace GPdotNET.Util
{
    public static class GPModelGlobals
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="filePath"></param>
       /// <returns></returns>
        public static List<GPFunction> GetFunctionsFromXML(string filePath)
        {
            try
            {
                // Loading from a file, you can also load from a stream
                var doc = XDocument.Load(filePath);
                // 
                var q = from c in doc.Descendants("FunctionSet")
                        select new GPFunction
                        {

                            Selected = bool.Parse(c.Element("Selected").Value),
                            Weight = int.Parse(c.Element("Weight").Value),
                            Name = c.Element("Name").Value,
                            Definition = c.Element("Definition").Value,
                            ExcelDefinition = c.Element("ExcelDefinition").Value,
                            Aritry = ushort.Parse(c.Element("Aritry").Value),
                            Description = c.Element("Description").Value,
                            IsReadOnly = bool.Parse(c.Element("ReadOnly").Value),
                            IsDistribution = bool.Parse(c.Element("IsDistribution").Value),
                            ID = ushort.Parse(c.Element("ID").Value)

                        };
                var retval = q.Where(p => p.Selected == true).ToList();
                return retval;
            }
            catch (Exception)
            {
                
                throw new Exception("Fiel not exist!");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpTrainigData"></param>
        /// <param name="consts"></param>
        /// <param name="gpTestingData"></param>
        /// <returns></returns>
        public static GPTerminalSet GenerateTerminalSet(double[][] gpTrainigData, double[] consts, double[][] gpTestingData)
        {
            int numConst=0;
            if (consts != null)
                numConst = consts.Length;

            if (gpTrainigData == null)
                throw new Exception("Training data cannot be null!");

            GPTerminalSet terminalSet = new GPTerminalSet();

            //First define the initial properties of the terminalset
            terminalSet.NumConstants = numConst;
            terminalSet.NumVariables = (gpTrainigData[0].Length - 1);
            terminalSet.RowCount = gpTrainigData.Length;

            int numOfVariables = terminalSet.NumVariables + terminalSet.NumConstants + 1/*Output Value of experiment*/;

            //Generate training data
            terminalSet.TrainingData = new double[terminalSet.RowCount][];

            for (int j = 0; j < terminalSet.RowCount; j++)
            {
                terminalSet.TrainingData[j] = new double[numOfVariables];

                for (int i = 0; i < numOfVariables ; i++)
                {
                    if (i < terminalSet.NumVariables)//input variable
                        terminalSet.TrainingData[j][i] = gpTrainigData[j][i];
                    else if (i >= terminalSet.NumVariables && i < numOfVariables - 1)//constants
                        terminalSet.TrainingData[j][i] = consts[i - terminalSet.NumVariables];
                    else
                        terminalSet.TrainingData[j][i] = gpTrainigData[j][i - terminalSet.NumConstants];//output variable
                }
            }

            //
          //  terminalSet.CalculateStat();
            
            //Generate training data if exist
            if (gpTestingData != null)
            {
                //Generate training data
                terminalSet.TestingData = new double[gpTestingData.Length][];

                for (int j = 0; j < gpTestingData.Length; j++)
                {
                    terminalSet.TestingData[j] = new double[numOfVariables];

                    for (int i = 0; i < numOfVariables; i++)
                    {
                        if (i < terminalSet.NumVariables)//input variable
                            terminalSet.TestingData[j][i] = gpTestingData[j][i];
                        else if (i >= terminalSet.NumVariables && i < numOfVariables - 1)//constants
                            terminalSet.TestingData[j][i] = consts[i - terminalSet.NumVariables];
                        else
                            terminalSet.TestingData[j][i] = gpTestingData[j][i - terminalSet.NumConstants];
                    }
                }
            }
            return terminalSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpTrainigData"></param>
        /// <param name="consts"></param>
        /// <param name="gpTestingData"></param>
        /// <returns></returns>
        public static GPTerminalSet GenerateTRANSPORTTerminalSet(double[][] gpTrainigData)
        {
            GPTerminalSet terminalSet = new GPTerminalSet();

            //First define the initial properties of the terminalset
            terminalSet.NumConstants = 0;
            terminalSet.NumVariables = gpTrainigData[0].Length - 1;
            terminalSet.RowCount = gpTrainigData.Length-1;

            //Generate training data
            terminalSet.TrainingData = new double[terminalSet.RowCount][];
            //
            terminalSet.Destinations = new int[terminalSet.NumVariables];
            terminalSet.Sources = new int[terminalSet.RowCount];
            for (int i = 0; i < gpTrainigData.Length; i++)
            {
                if (i == 0)
                {
                    for (int j = 1; j < gpTrainigData[i].Length; j++)
                        terminalSet.Destinations[j-1] = (int)gpTrainigData[i][j];
                }
                else
                {
                    terminalSet.TrainingData[i-1] = new double[terminalSet.NumVariables];

                    for (int j = 0; j < gpTrainigData[i].Length; j++)
                    {
                        if(j==0)
                            terminalSet.Sources[i-1] = (int)gpTrainigData[i][j];
                        else
                            terminalSet.TrainingData[i - 1][j - 1] = gpTrainigData[i][j];

                    }
                }
            }
            return terminalSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpTrainigData"></param>
        /// <param name="consts"></param>
        /// <param name="gpTestingData"></param>
        /// <returns></returns>
        public static GPTerminalSet GenerateTSPTerminalSet(double[][] tspMapCity)
        {
            GPTerminalSet terminalSet = new GPTerminalSet();

            //First define the initial properties of the terminalset
            terminalSet.NumConstants = 0;
            terminalSet.NumVariables = tspMapCity.Length;
            terminalSet.RowCount = tspMapCity.Length;

            //Generate training data
            terminalSet.TrainingData = new double[terminalSet.RowCount][];

            for (int j = 0; j < terminalSet.RowCount; j++)
            {
                terminalSet.TrainingData[j] = new double[2];

                for (int i = 0; i < 2; i++)
                {
                    terminalSet.TrainingData[j][i] = tspMapCity[j][i];
                }
            }
            return terminalSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpTrainigData"></param>
        /// <param name="consts"></param>
        /// <param name="gpTestingData"></param>
        /// <returns></returns>
        public static GPTerminalSet GenerateALOCTerminalSet(double[][] gridLocation)
        {
            GPTerminalSet terminalSet = new GPTerminalSet();

            //First define the initial properties of the terminalset
            terminalSet.NumConstants = 0;
            terminalSet.NumVariables = gridLocation[0].Length;
            terminalSet.RowCount = gridLocation.Length;

            //Generate training data
            terminalSet.TrainingData = new double[terminalSet.RowCount][];

            for (int j = 0; j < terminalSet.RowCount; j++)
            {
                terminalSet.TrainingData[j] = new double[terminalSet.NumVariables];

                for (int i = 0; i < terminalSet.NumVariables; i++)
                {
                    terminalSet.TrainingData[j][i] = gridLocation[j][i];
                }
            }
            return terminalSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double[] GenerateConstants(float from, float to, int number)
        {
            if (number==0)
                return null;

            if (from >= to)
            {
                MessageBox.Show("'From' has to be less than 'To' variable.");
                return null;
            }

            var con = new double[number];

            for (int i = 0; i < number; i++)
               con[i]= Math.Round((Globals.radn.Next((int)from, (int)to) + Globals.radn.NextDouble()),5);
            

            return con;
        }


        /// <summary>
        /// Opens standard dialog and select the filename with spec extension
        /// </summary>
        /// <param name="fileDescription"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetFileFromOpenDialog(string fileDescription = "Comma separated files ", string extension = "*.csv")
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*", extension, fileDescription);
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return null;
        }

        /// <summary>
        /// Opens standard save dialog and enter the filename with spec extension
        /// </summary>
        /// <param name="fileDescription"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetFileFromSaveDialog(string fileDescription = "GPdotNET standard file", string extension = "*.gpa")
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*", extension, fileDescription);
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return null;
        }

        /// <summary>
        /// Load xml dimensionin data and put in to nxm dim array
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static double[][] LoadDataFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    string buffer;

                    // open selected file and retrieve the content
                    using (StreamReader reader = System.IO.File.OpenText(fileName))
                    {
                        //read TrainingData in to buffer
                        buffer = reader.ReadToEnd();
                        reader.DiscardBufferedData();
                        //reader.Close();
                    }

                    //define the row
                    string[] rows;

                    //extract data but remove comments
                    rows = buffer.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(x => (x != null && x.Length > 0 && x[0] != '!')).Select(x => x).ToArray();

                    //Define the columns
                    string[] cols = rows[0].Split(';');

                    double[][] data;

                    //Define inner TrainingData
                    data = new double[rows.Length][];

                    for (int k = 0; k < rows.Length; k++)
                    {
                        data[k] = new double[cols.Length];

                        for (int j = 0; j < cols.Length; j++)
                        {

                            cols = rows[k].Split(';');
                            //with letter M we define very large number.
                            //with letter L we define very smal number
                            if (cols[j].ToUpper() == "M")
                                data[k][j] = double.MaxValue;
                            else if (cols[j].ToUpper() == "L")
                                data[k][j] = double.MinValue;
                            else
                                data[k][j] = double.Parse(cols[j], CultureInfo.InvariantCulture);

                        }
                    }

                    return data;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Load nxm dimensionin data and put in to nxm dim array
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static double[][] LoadTimeSeriesFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    string buffer;

                    // open selected file and retrieve the content
                    using (StreamReader reader = System.IO.File.OpenText(fileName))
                    {
                        //read TrainingData in to buffer
                        buffer = reader.ReadToEnd();
                        reader.DiscardBufferedData();
                        //reader.Close();
                    }

                    //define the row
                    string[] rows;

                    rows = buffer.Split(new char[] { '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    //Define the columns
                    string[] cols = rows[0].Split(';');

                    double[][] data;

                    //Define inner TrainingData
                    data = new double[rows.Length][];

                    for (int k = 0; k < rows.Length; k++)
                    {
                        data[k] = new double[cols.Length];

                        for (int j = 0; j < cols.Length; j++)
                        {
                            cols = rows[k].Split(';');
                            data[k][j] = double.Parse(cols[j], CultureInfo.InvariantCulture);

                        }
                    }

                    return data;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Save function in to XML file
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="funs"></param>
        private static void SaveToXML(XDocument doc, List<GPFunction> funs)
        {

            //persist changes made in functions, so the second time user open the app has the same set of checken function ready for GP
            XElement functionSets = doc.Root;
            //ispod 

            for (int i = 0; i < funs.Count; i++)
            {
                functionSets.Elements("FunctionSet").ElementAt(i).Element("Selected").Value = funs[i].Selected.ToString();

                //Da li je funkcija read only ako nije onda nek se spreme promjene
                if (bool.Parse(functionSets.Elements("FunctionSet").ElementAt(i).Element("ReadOnly").Value) == false)
                {
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Aritry").Value = funs[i].Aritry.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Name").Value = funs[i].Name.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Description").Value = funs[i].Description.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Definition").Value = funs[i].Definition.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Weight").Value = funs[i].Weight.ToString();
                }
            }

            //the path of sunction file
            string strPath = Application.StartupPath + "\\Resources_Files\\FunctionSet.xml";
            // When app is deployed with ClickOnce we have diferent path of file FunctionSet.xml
            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    try
            //    {
            //        strPath = ApplicationDeployment.CurrentDeployment.DataDirectory + @"\FunctionSet.xml";
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(Resources.SR_Cannot_Read + ex.Message);
            //    }
            //}
            doc.Save(strPath);
        }

        /// <summary>
        /// Loads function from xml
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Dictionary<int, GPFunction> LoadFunctionsfromXMLFile(XDocument doc = null)
        {
            Dictionary<int, GPFunction> funs = new Dictionary<int, GPFunction>();

            string strPath = Application.StartupPath + "/Resources_Files/FunctionSet.xml";

            try
            {

                // Loading from a file, you can also load from a stream
                doc = XDocument.Load(strPath);
                // 
                var q = from c in doc.Root.Elements("FunctionSet")
                        select new GPFunction
                        {

                            Selected = bool.Parse(c.Element("Selected").Value),
                            Weight = int.Parse(c.Element("Weight").Value),
                            Name = c.Element("Name").Value,
                            Definition = c.Element("Definition").Value,
                            ExcelDefinition = c.Element("ExcelDefinition").Value,
                            MathematicaDefinition = c.Element("MathematicaDefinition").Value,
                            Aritry = ushort.Parse(c.Element("Aritry").Value),
                            Description = c.Element("Description").Value,
                            IsReadOnly = bool.Parse(c.Element("ReadOnly").Value),
                            IsDistribution = bool.Parse(c.Element("IsDistribution").Value),
                            ID = ushort.Parse(c.Element("ID").Value),
                            Parameters = c.Element("Parameters").Value

                        };
                if (funs != null)
                {
                    if (funs.Count > 0)
                        funs.Clear();
                }

                //
                funs = q.ToDictionary(v => (int)v.ID, v => v);
                return funs;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static bool IsNumber<T>(this T obj)
        {
            Type objType = obj.GetType();

            if (objType.IsPrimitive)
            {
                if (objType == typeof(object) ||
                    objType == typeof(string) ||
                    objType == typeof(bool))
                    return false;

                return true;
            }

            return false;
        }

        public static Image LoadImageFromName(string name)
        {
            Assembly asm = Assembly.GetEntryAssembly();
            // string appName = Assembly.GetEntryAssembly().GetName().Name;
            var pic = asm.GetManifestResourceStream(name/*"GPdotNET.App.Resources.gpabout.png"*/);
            return Image.FromStream(pic);
        }

        public static Icon LoadIconFromName(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var pic = asm.GetManifestResourceStream(name/*"GPdotNET.App.Resources.gpabout.png"*/);
            return new Icon(pic);
        }
 
     }
}
