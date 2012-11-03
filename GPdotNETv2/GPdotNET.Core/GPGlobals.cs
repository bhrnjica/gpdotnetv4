﻿//////////////////////////////////////////////////////////////////////////////////////////
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
using System.Linq;
using System.Text;

namespace GPdotNET.Core
{
    public static class Globals
    {
        public static int MaxTerminalIndex = 1999;
        public static int StartTerminalIndex = 1000;
        public static int StartFunctionIndex = 2000;

        public static ThreadSafeRandom radn = new ThreadSafeRandom();

        public static GPTerminalSet gpterminals;
        public static IFunctionSet functions;


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
                if (rowIndex == -1)//
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
                consts[i] = (double)decimal.Round(val, 5);
            }

            return consts;
        }

        //String representation of GPNode
        public static string GetGPNodeStringRep(int nodeId)
        {
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

        //Calculate model agains specific data 
        public static double[] CalculateGPModel(GPNode node, bool btrainingData=true)
        {
            double[][] data = btrainingData ? gpterminals.TrainingData : gpterminals.TestingData;

            var model = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                model[i] = functions.Evaluate(node, i, btrainingData);

            return model;
        }
    }
}
