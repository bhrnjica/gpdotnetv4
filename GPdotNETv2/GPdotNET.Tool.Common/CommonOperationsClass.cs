
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using GPdotNET.Core;
namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Class implemenzt common operations which can be used across all projects in GPdotNET
    /// </summary>
    public class CommonMethods
    {
        /// <summary>
        /// Opens standard dialog and select the filename with spec extension
        /// </summary>
        /// <param name="fileDescription"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetFileFromOpenDialog(string fileDescription="Comma separated files ",string extension="*.csv")
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = string.Format("{1} ({0})|{0}|All files (*.*)|*.*",extension, fileDescription);
            if(dlg.ShowDialog()== DialogResult.OK)
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
        /// Load nxm dimensionin data and put in to nxm dim array
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static double [][] LoadDataFromFile(string fileName)
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
						reader.Close();
                    }

                    //define the row
                    string[] rows;
                    
                    rows = buffer.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                   

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
                        reader.Close();
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
        public static Dictionary<int,GPFunction> LoadFunctionsfromXMLFile(XDocument doc=null)
        {
            Dictionary<int, GPFunction> funs = new Dictionary<int,GPFunction>();

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
                            Aritry = ushort.Parse(c.Element("Aritry").Value),
                            Description = c.Element("Description").Value,
                            IsReadOnly = bool.Parse(c.Element("ReadOnly").Value),
                            IsDistribution = bool.Parse(c.Element("IsDistribution").Value),
                            ID = ushort.Parse(c.Element("ID").Value),
                            Parameters= c.Element("Parameters").Value

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

    }
}