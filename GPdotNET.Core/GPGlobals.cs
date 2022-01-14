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
using GPdotNET.Core.Experiment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GPdotNET.Core
{
    //Set of static global methods avaliable anywhere in the program.
    public static class Globals
    {
        public static int MaxTerminalIndex = 1999;
        public static int StartTerminalIndex = 1000;
        public static int StartFunctionIndex = 2000;

        public static ThreadSafeRandom radn = new ThreadSafeRandom();

        //in case of GP MUltyClass problem class ount
        public static int classCount = 0;
        public static GPTerminalSet gpterminals;
        public static IFunctionSet functions;
        public static GPParameters gpparameters;


        /// <summary>
        /// GenerateChromosome Node value. Values from 1000 to 19999 are terminals,
        /// and values greater the 1999 are function index
        /// </summary>
        /// <param name="isFunction"> If true Function will be choosed, otherwise terminal index</param>
        /// <returns></returns>
        static public int GenerateNodeValue(bool isFunction)
        {

            if (isFunction)
                return StartFunctionIndex + functions.GetRandomFunction();
            else
                return StartTerminalIndex + Globals.radn.Next(functions.GetTerminals().Count);
        }

        
        /// <summary>
        /// Returns true is the current node valu holds index of function, otherwize returns false-
        /// </summary>
        /// <param name="functionIndex"></param>
        /// <returns>Returns true is the current node valu holds index of function, otherwize returns false</returns>
        public static bool IsFunction(int functionIndex)
        {
            if (functionIndex >= Globals.StartFunctionIndex)
                return true;
            else if (functionIndex < Globals.StartTerminalIndex)
                throw new Exception("Invalid terminal index!");
            else
                return false;
        }

        /// <summary>
        /// Returns aritry of the function
        /// </summary>
        /// <param name="functionIndex"></param>
        /// <returns></returns>
        public static int GetFunctionAritry(int funID)
        {
            validateFunctionSet();
            funID -= 2000;
            int retVal = functions.GetAritry(funID);
            if(retVal==-1)
                throw new Exception("Invalid Function ID!");

            return retVal;
        }

        


        /// <summary>
        /// Return the number of terminals
        /// </summary>
        /// <returns></returns>
        public static int GetTerminalVarCount()
        {
            return gpterminals.NumVariables;
           
        }

        /// <summary>
        /// Returns specific row from terminaldata
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="bTraining"></param>
        /// <returns></returns>
        public static double[] GetTerminalRow(int rowIndex, bool bTraining= true)
        {
            if (bTraining)
            {
                if (rowIndex == -1)
                    return gpterminals.SingleTrainingData;
                else
                    return gpterminals.TrainingData[rowIndex];
            }
            else
            {
                return gpterminals.TestingData[rowIndex];
            }
        }
        /// <summary>
        /// Returns specific row from terminaldata
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="bTraining"></param>
        /// <returns></returns>
        public static double GetTerminalValue(int row, int col)
        {
            return gpterminals.TrainingData[row][col];
        }

        /// <summary>
        /// Generate random n numbers between definde interval.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double[] GenerateRandomConstants(int from, int to, int number)
        {
            var consts = new double[number];

            for (int i = 0; i < number; i++)
            {
                decimal val = (decimal)(radn.Next(from, to) + radn.NextDouble());
                consts[i] = (double)Math.Round(val, 5);
            }

            return consts;
        }

        //String representation of GPNode
        public static string GetGPNodeStringRep(int nodeId)
        {
            validateFunctionSet();
            //function
            if (IsFunction(nodeId))
            {
                nodeId -= Globals.StartFunctionIndex;
                return functions.GetFunctions()[nodeId].Name;
            }
            else
            {
                nodeId -= Globals.StartTerminalIndex;
                return functions.GetTerminals()[nodeId].Name;
                
            }
        }

        //String representation of GPNode
        public static int GetNodeValueFromStringRep(string content)
        {
            validateFunctionSet();

            var n=content[0].ToString().ToLower();
            //terminal can be defined with x or r.
            if (n=="x" || n=="r")
            {
                return functions.GetTerminals().Where(x => x.Value.Name == content).Select(x => x.Value.Index).FirstOrDefault();
               
            }
            else
            {
                return functions.GetFunctions().Where(x => x.Value.Name == content).Select(x => x.Value.ID).FirstOrDefault();
               

            }
        }
      
        //Calculate model agains specific data 
        public static double[] CalculateGPModel(GPNode node, bool btrainingData=true)
        {
            validateFunctionSet();
            double[][] data = btrainingData ? gpterminals.TrainingData : gpterminals.TestingData;

            if (data == null)
                return null;

            var model = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                model[i] = functions.Evaluate(node, i, btrainingData);

            return model;
        }
        /// <summary>
        /// Returns number of rows from current Terminal Set.
        /// </summary>
        /// <param name="trainingData"></param>
        /// <returns></returns>
        public static int GetRowCout(bool trainingData=true)
        {
            if (trainingData)
               return  gpterminals.TrainingData.Length;
            else
              return  gpterminals.TestingData.Length;
        }
        private static void validateFunctionSet()
        {
            if (functions != null && functions.GetFunctions() != null)
                return;
            var fs = new GPFunctionSet();
            var fnsc = GenerateGPFunctionsFromXML();
            fs.SetFunction(fnsc);
            functions = fs;
        }
        public static Dictionary<int, GPFunction> GenerateGPFunctionsFromXML()
        {
            try
            {
                var q= GetFunctionsFromXML();
                var retval = q.ToDictionary(v => v.ID, v => v);
                return retval;
            }
            catch (Exception)
            {

                throw new Exception("Fiel not exist!");
            }

        }
        public static List<GPFunction> GetFunctionsFromXML()
        {
            try
            {
                // Loading from a file, you can also load from a stream
                var doc = XDocument.Load("Assets\\FunctionSet.xml");
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

                throw new Exception("File not exist!");
            }
        }

        public static GPFunctionSet GetFunctionSet()
        {
            validateFunctionSet();
            return functions as GPFunctionSet;
        }
    }
}
