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
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace GPdotNET.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class GPFunctionSet : IFunctionSet
    {
         //Collections of functions and terminals. They are separated in two diferent collection
        // cause cleaner logic
        private Dictionary<int, GPFunction> functions = new Dictionary<int, GPFunction>();
        private List<int> funChooser = null;
        private Dictionary<int, GPTerminal> terminals = new Dictionary<int, GPTerminal>();
        
        //localthread tokens var, for reusing list emements within one thread
        private ThreadLocal<List<int>> _tokens = new ThreadLocal<List<int>>(() =>
        {
            return new List<int>(50);
        });

        //localthread arguments var, for reusing list emements within one thread
        private ThreadLocal<Stack<double>> _arguments = new ThreadLocal<Stack<double>>(() =>
        {
            return new Stack<double>(50);
        });

        //localthread tokens var, for reusing list emements within one thread
        private ThreadLocal<double[]> _args = new ThreadLocal<double[]>(() =>
        {
            return new double[5];
        });

        public GPFunctionSet()
        { }


        /// <summary>
        /// Evaluates expression 
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="tt"></param>
        /// <returns></returns>
        public double Evaluate(GPFunction fun, params double[] tt)
        {
            switch (fun.ID)
            {
                case 0:
                    {
                        return tt[0] + tt[1];
                    }
                case 1:
                    {
                        return tt[0] - tt[1];
                    }
                case 2:
                    {
                        return tt[0] * tt[1];
                    }
                case 3://protected divison
                    {
                        if (tt[1] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return tt[0] / tt[1];
                    }

                case 4:
                    {
                        return tt[0] + tt[1] + tt[2];
                    }
                case 5:
                    {
                        return tt[0] - tt[1] - tt[2];
                    }
                case 6:
                    {
                        return tt[0] * tt[1] * tt[2];
                    }
                case 7:
                    {
                        if (tt[2] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        if (tt[1] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return tt[0] / tt[1] / tt[2];
                    }

                case 8:
                    {
                        return tt[0] + tt[1] + tt[2] + tt[3];
                    }
                case 9:
                    {
                        return tt[0] - tt[1] - tt[2] - tt[3];
                    }
                case 10:
                    {
                        return tt[0] * tt[1] * tt[2] * tt[3];
                    }
                case 11:
                    {
                        if (tt[3] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        if (tt[2] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        if (tt[1] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 1;
                        return tt[0] / tt[1] / tt[2] / tt[3];
                    }

                case 12:
                    {
                        return Math.Pow(tt[0], 2);
                    }
                case 13:
                    {
                        return Math.Pow(tt[0], 3);
                    }
                case 14:
                    {
                        return Math.Pow(tt[0], 4);
                    }

                case 15:
                    {
                        return Math.Pow(tt[0], 5);
                    }

                case 16:
                    {
                        return Math.Pow(tt[0], 1 / 3.0);
                    }
                case 17:
                    {
                        return Math.Pow(tt[0], 1 / 4.0);
                    }

                case 18:
                    {
                        return Math.Pow(tt[0], 1 / 5.0);
                    }
                case 19:
                    {
                        if (tt[0] == 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return 1.0 / tt[0];
                    }
                case 20:
                    {
                        return Math.Abs(tt[0]);
                    }
                case 21:
                    {
                        return Math.Floor(tt[0]);
                    }
                case 22:
                    {
                        return Math.Ceiling(tt[0]);
                    }
                case 23:
                    {
                        return Math.Truncate(tt[0]);
                    }
                case 24:
                    {
                        return Math.Round(tt[0]);
                    }
                case 25:
                    {
                        return Math.Sin(tt[0]);
                    }
                case 26:
                    {
                        return Math.Cos(tt[0]);
                    }
                case 27:
                    {
                        return Math.Tan(tt[0]);
                    }

                case 28:
                    {
                        if (tt[0] > 1 && tt[0] < -1 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return Math.Asin(tt[0]);
                    }
                case 29:
                    {
                        if (tt[0] > 1 && tt[0] < -1 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return Math.Acos(tt[0]);
                    }
                case 30:
                    {
                        return Math.Atan(tt[0]);
                    }
                case 31:
                    {
                        return Math.Sinh(tt[0]);
                    }
                case 32:
                    {
                        return Math.Cosh(tt[0]);
                    }
                case 33:
                    {
                        return Math.Tanh(tt[0]);
                    }
                case 34:
                    {
                        if (tt[0] < 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return Math.Sqrt(tt[0]);
                    }
                case 35:
                    {
                        return Math.Pow(Math.E, tt[0]);
                    }
                case 36:
                    {
                        if (tt[0] <= 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return Math.Log10(tt[0]);
                    }
                case 37:
                    {
                        if (tt[0] <= 0 && Globals.gpparameters.isProtectedOperationEnabled)
                            return 0;
                        return Math.Log(tt[0], Math.E);
                    }
                case 38:
                    {
                        return tt[0] * tt[0] + tt[0] * tt[1] + tt[1] * tt[1];
                    }
                case 39:
                    {
                        return tt[0] * tt[0] * tt[0] + tt[1] * tt[1] * tt[1] + tt[2] * tt[2] * tt[2] + tt[0] * tt[1] * tt[2] + tt[0] * tt[1] + tt[1] * tt[2] + tt[0] * tt[2];
                    }
                case 40:
                    {
                        return tt[0] * tt[0] * tt[1];
                    }
                case 41:
                    {
                        return tt[0] * tt[0] * tt[1] * tt[1];
                    }
                case 42:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[1];
                    }
                case 43:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[1] * tt[1];
                    }
                case 44:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[1] * tt[1] * tt[1];
                    }
                case 45:
                    {
                        return tt[0] * tt[0] * tt[1] * tt[1] * tt[1];
                    }
                case 46:
                    {
                        return tt[0] * tt[1] * tt[1] * tt[1];
                    }
                case 47:
                    {
                        return tt[0] * tt[0] * tt[0] * tt[0] * tt[1] * tt[1] * tt[1] * tt[1];
                    }
                default:
                    {
                        return double.NaN;
                    }
            }
        }

        /// <summary>
        /// Converts GPNode in to expression 
        /// </summary>
        /// <param name="treeExpression">chromosome in S-expression</param>
        /// <param name="rowIndex"> current row to evaluate choromosme</param>
        /// <param name="btrainingData">whether is training or testing data</param>
        /// <returns></returns>
        public double Evaluate(GPNode treeExpression, int rowIndex, bool btrainingData = true)
        {
            //get the array from tree nodes
            treeExpression.ToList(_tokens.Value);
            var tokens = _tokens.Value;
            //count all tokens
            int countT = tokens.Count();

            //get terminal for specific position
            double[] terminalRow = Globals.GetTerminalRow(rowIndex, btrainingData);

            //Stack for evaluation
            Stack<double> arguments = _arguments.Value;//new Stack<double>();

            //the maximum aritry is 4
            // double[] val = new double[5];

            for (int i = countT - 1; i >= 0; i--)
            {
                //skip invalid tokens
                if (tokens[i] < 1000)
                    continue;

                // Put terminal in to Stack for leter function evaluation
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    arguments.Push(terminalRow[tokens[i] - 1000]);
                    //reset node
                    tokens[i] = 0;
                }
                else
                {
                    //create real index value
                    int ind = tokens[i] - 2000;

                    //make current token invalid
                    //or reset token position
                    tokens[i] = 0;

                    //prepare function arguments for evaluation
                    int count = functions[ind].Aritry;

                    //Extract variables
                    for (int j = 0; j < count; j++)
                    {
                        var num = arguments.Pop();
                        if (double.IsNaN(num) || double.IsInfinity(num))
                        {
                            ResetTokensList(tokens, arguments, i);
                            return double.NaN;
                        }
                        _args.Value[j] = num;
                    }

                    double result = Evaluate(functions[ind], _args.Value);
                    _args.Value[0] = 0;
                    _args.Value[1] = 0;
                    _args.Value[2] = 0;
                    _args.Value[3] = 0;
                    _args.Value[4] = 0;
                   
                    //check if number is valid
                    if (double.IsNaN(result) || double.IsInfinity(result))
                    {
                        //reset the rest of elements in the list
                        ResetTokensList(tokens, arguments, i);
                        return double.NaN;
                    }

                    //inser subresult in to stack
                    arguments.Push(result);   
                }
            }
            // return the only value from stack
            Debug.Assert(arguments.Count == 1);
            return arguments.Pop();
        }

        private static void ResetTokensList(List<int> tokens, Stack<double> arguments, int i)
        {
            //reset the rest of elements in the list
            for (; i >= 0; i--)
                tokens[i] = 0;

            arguments.Clear();
        }

        /// <summary>
        /// Decoding treeExpression to polishnotation 
        /// </summary>
        /// <param name="treeExpression"></param>
        /// <returns></returns>
        public string DecodeExpression(GPNode treeExpression, bool bExcel=false)
        {
            //Prepare chromoseme for evaluation
            var tokens = treeExpression.ToList();
            int countT = tokens.Count;

            //Stack fr evaluation
            Stack<string> expression = new Stack<string>();

            for (int i = countT - 1; i >= 0; i--)
            {
 
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    string varaiable = terminals[tokens[i] - 1000].Name;
                    expression.Push(varaiable);
                }
                else
                {
                    //prepare function arguments for evaluation
                    int count = functions[tokens[i] - 2000].Aritry;
                    string function = bExcel ? functions[tokens[i] - 2000].ExcelDefinition : functions[tokens[i] - 2000].Definition;

                   
                    for (int j = 1; j <= count; j++)
                    {
                        string oldStr = "x" + (j).ToString();
                        string newStr = expression.Pop();
                        if (bExcel)
                            newStr += " ";
                        function = function.Replace(oldStr, newStr);
                    }

                    //Izracunavanje rezultata 
                    expression.Push("(" + function + ")");
                   
                }
            }
            // return the only value from stack
            Debug.Assert(expression.Count == 1);
            // return arguments.Pop();
            return expression.Pop();
        }

        public string DecodeExpression(GPNode treeExpression, int langOption)
        {
            //Prepare chromoseme for evaluation
            var tokens = treeExpression.ToList();
            int countT = tokens.Count;

            //Stack fr evaluation
            Stack<string> expression = new Stack<string>();

            for (int i = countT - 1; i >= 0; i--)
            {

                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    string varaiable = terminals[tokens[i] - 1000].Name;
                    if (!varaiable.EndsWith(" "))//we add extre space at the end to recognize variable index and value
                        varaiable += " ";
                    expression.Push(varaiable);
                }
                else
                {
                    //prepare function arguments for evaluation
                    int count = functions[tokens[i] - 2000].Aritry;
                    string function="";

                    if(langOption==1)
                        function = functions[tokens[i] - 2000].MathematicaDefinition;
                    else if(langOption==2)
                        function = functions[tokens[i] - 2000].ExcelDefinition;
                    else if (langOption == 3)
                        function = functions[tokens[i] - 2000].RDefinition;
                    else
                        function = functions[tokens[i] - 2000].Definition;

                    for (int j = 1; j <= count; j++)
                    {
                        string oldStr = "x" + (j).ToString();
                        string newStr = expression.Pop();
                        function = function.Replace(oldStr, newStr);
                    }

                    //Izracunavanje rezultata 
                    expression.Push("(" + function + ")");

                }
            }
            // return the only value from stack
            Debug.Assert(expression.Count == 1);
            // return arguments.Pop();
            return expression.Pop();
        }

        public Dictionary<int,GPFunction> GetFunctions()
        {
            return functions;
        }

        public Dictionary<int,GPTerminal> GetTerminals()
        {
            return terminals;
        }

        public int GetNumVariables()
        {
            if (terminals == null)
                return 0;
            return terminals.Values.Count(x=>x.IsConstant==false);
        }

        
        public double GetTerminalMaxValue(int index)
        {
            if (terminals == null)
                return 0;
            return terminals[index].maxValue;
        }

        public double GetTerminalMinValue(int index)
        {
            if (terminals == null)
                return 0;
            return terminals[index].minValue;
        }

        public void SetTerimals(Dictionary<int, GPTerminal> list)
        {
          

            terminals = list;
        }

        public void SetFunction(Dictionary<int, GPFunction> list, bool bSelected = true)
        {
            functions = list;
            //Clear old functions
            if (functions == null)
                functions = new Dictionary<int,GPFunction>();

            
            funChooser = new List<int>();
            var lst = bSelected ? list.Where(f => f.Value.Selected == true) : list;

            foreach (var op in lst)
            {
                for (int i = 0; i < op.Value.Weight; i++)
                {
                   funChooser.Add(op.Value.ID);
                }
            }
        }

        public int GetAritry(int funID)
        {
            if (functions != null && functions.Count > 0)
                return functions[funID].Aritry;
            throw new Exception("Function id is not defined.");
        }

        public int GetRandomFunction()
        {
            int val = Globals.radn.Next(funChooser.Count);
            return funChooser[val];
        }
    }
}
