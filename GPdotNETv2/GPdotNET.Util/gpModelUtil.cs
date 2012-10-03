using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNET.Core;
using System.Xml.Linq;
using System.IO;
using GPdotNET.Util;

namespace GPdotNET.Engine
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
            terminalSet.CalculateStat();
            
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
                            terminalSet.TestingData[j][i] = gpTrainigData[j][i - terminalSet.NumConstants];
                    }
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
                throw new Exception("Constant parameter generation fails.");

            var con = new double[number];

            for (int i = 0; i < number; i++)
               con[i]= Math.Round((Globals.radn.Next((int)from, (int)to) + Globals.radn.NextDouble()),5);
            

            return con;
        }
 
     }
}
