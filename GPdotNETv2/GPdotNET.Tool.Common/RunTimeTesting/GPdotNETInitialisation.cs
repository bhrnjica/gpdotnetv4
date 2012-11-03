
using System.Xml.Linq;
using GPdotNET.Core;
using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using GPdotNET.Tool.Common;
namespace GPdotNET.Tool
{
    public static class GPdotNETRunTimeTest
    {
        public static Dictionary<int,GPFunction> GenerateGPFunctionsFromXML()
        {
           
            //get the full location of the unittest assembly
            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //get the folder that's in
            string theDirectory = Path.GetDirectoryName(fullPath);

            string filePath = theDirectory + "\\RunTimeTesting\\FunctionSet.xml";
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
                            ID = int.Parse(c.Element("ID").Value)

                        };
                var retval = q.ToDictionary(v => v.ID, v => v);
                return retval;
            }
            catch (Exception)
            {

                throw new Exception("Fiel not exist!");
            }
            
        }
        public static double[][] LoadTrainingData(string fileName = "sample1_traindata.csv")
        {
            //get the full location of the unittest assembly
            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //get the folder that's in
            string theDirectory = Path.GetDirectoryName(fullPath);

            return CommonMethods.LoadDataFromFile(theDirectory + "\\RunTimeTesting\\" + fileName);
           // return GPdotNET.Engine.GPModelGlobals.LoadGPData(theDirectory + "\\RunTimeTesting\\" + fileName);
        }
        public static double[][] LoadTestData(string fileName = "sample1_testdata.csv")
        {
            //get the full location of the unittest assembly
            string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //get the folder that's in
            string theDirectory = Path.GetDirectoryName(fullPath);

            return CommonMethods.LoadDataFromFile(theDirectory + "\\RunTimeTesting\\" + fileName);
            //return GPdotNET.Engine.GPModelGlobals.LoadGPData(theDirectory + "\\RunTimeTesting\\" + fileName);
        }
        
    }
}