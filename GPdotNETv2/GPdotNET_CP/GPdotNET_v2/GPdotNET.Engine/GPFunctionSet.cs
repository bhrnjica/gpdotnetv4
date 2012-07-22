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
using GPdotNET.Core;

namespace GPdotNET.Engine
{
    /// <summary>
    /// Klasa koja posjeduje sve funkcije  i terminale (nazive nezavisno promjenjivih i konstanti) koje se mogu naci u posmatranom programu
    /// Ovaj skup se definise pokretanjem svakog programa, i ucitava se iz XML datoteke u kojoj su smjestene funkcije
    /// </summary>
    public class GPFunctionSet : IFunctionSet
    {
         //Collections of functions and terminals. They are separated in two diferent collection
        // cause cleaner logic
        private  List<GPFunction> functions = new List<GPFunction>();
        private List<GPTerminal> terminals = new List<GPTerminal>();

        public GPFunctionSet()
        { }

        public double Evaluate(GPNode treeExpression, int rowIndex)
        {
            //Helpers
            var tokens= treeExpression.ToList();
            int countT = tokens.Count;

            double[] terminalRow = Globals.GetTerminalRow(rowIndex);
            int numVars = Globals.GetTerminalVarCount();

            //Stack for evaluation
            Stack<double> arguments = new Stack<double>();

            for (int i = countT-1; i >= 0; i--)
            {
                // Put terminav in to Stack for leter function evaluation
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    arguments.Push(terminalRow[tokens[i] - 1000]);
                }
                else
                {
                    //prepare function arguments for evaluation
                    int count = functions[tokens[i] - 2000].Aritry;

                    //Extract variables
                    double[] val = new double[count];
                    for (int j = 0; j < count; j++)
                    {
                        var num=arguments.Pop();
                        if (double.IsNaN(num) || double.IsInfinity(num))
                            return double.NaN;
                        val[j] = num;
                    }

                    double result = Evaluate(functions[tokens[i] - 2000],val);

                    //check if number is valid
                    if (double.IsNaN(result) || double.IsInfinity(result))
                        return double.NaN;

                    //Izracunavanje izraza
                    arguments.Push(result);

                }
            }
            // return the only value from stack
            Debug.Assert(arguments.Count == 1);
            return arguments.Pop();


        }

        public double Evaluate(GPFunction fun, params double[] tt)
        {
            switch (fun.ID)
            {
                case 1:
                    {
                        return tt[0] + tt[1];
                    }
                case 2:
                    {
                        return tt[0] - tt[1];
                    }
                case 3:
                    {
                        return tt[0] * tt[1];
                    }
                case 4://protected divison
                    {
                        if (tt[1] == 0)
                            return 1;
                        return tt[0] / tt[1];
                    }

                case 5:
                    {
                        return tt[0] + tt[1] + tt[2];
                    }
                case 6:
                    {
                        return tt[0] - tt[1] - tt[2];
                    }
                case 7:
                    {
                        return tt[0] * tt[1] * tt[2];
                    }
                case 8:
                    {
                        if (tt[2] == 0)
                            return 1;
                        if (tt[1] == 0)
                            return 1;
                        return tt[0] / tt[1] / tt[2];
                    }

                case 9:
                    {
                        return tt[0] + tt[1] + tt[2] + tt[3];
                    }
                case 10:
                    {
                        return tt[0] - tt[1] - tt[2] - tt[3];
                    }
                case 11:
                    {
                        return tt[0] * tt[1] * tt[2] * tt[3];
                    }
                case 12:
                    {
                        if (tt[3] == 0)
                            return 1;
                        if (tt[2] == 0)
                            return 1;
                        if (tt[1] == 0)
                            return 1;
                        return tt[0] / tt[1] / tt[2] / tt[3];
                    }

                case 13:
                    {
                        return Math.Pow(tt[0], 2);
                    }
                case 14:
                    {
                        return Math.Pow(tt[0], 3);
                    }
                case 15:
                    {
                        return Math.Pow(tt[0], 4);
                    }

                case 16:
                    {
                        return Math.Pow(tt[0], 5);
                    }

                case 17:
                    {
                        return Math.Pow(tt[0], 1 / 3.0);
                    }
                case 18:
                    {
                        return Math.Pow(tt[0], 1 / 4.0);
                    }

                case 19:
                    {
                        return Math.Pow(tt[0], 1 / 5.0);
                    }
                case 20:
                    {
                        if (tt[0] == 0)
                            return 1;
                        return 1.0 / tt[0];
                    }
                case 21:
                    {
                        return Math.Abs(tt[0]);
                    }
                case 22:
                    {
                        return Math.Floor(tt[0]);
                    }
                case 23:
                    {
                        return Math.Ceiling(tt[0]);
                    }
                case 24:
                    {
                        return Math.Truncate(tt[0]);
                    }
                case 25:
                    {
                        return Math.Round(tt[0]);
                    }
                case 26:
                    {
                        return Math.Sin(tt[0]);
                    }
                case 27:
                    {
                        return Math.Cos(tt[0]);
                    }
                case 28:
                    {
                        return Math.Tan(tt[0]);
                    }

                case 29:
                    {
                        if (tt[0] > 1 && tt[0] < -1)
                            return 1;
                        return Math.Asin(tt[0]);
                    }
                case 30:
                    {
                        if (tt[0] > 1 && tt[0] < -1)
                            return 1;
                        return Math.Acos(tt[0]);
                    }
                case 31:
                    {
                        return Math.Atan(tt[0]);
                    }
                case 32:
                    {
                        return Math.Sinh(tt[0]);
                    }
                case 33:
                    {
                        return Math.Cosh(tt[0]);
                    }
                case 34:
                    {
                        if (tt[0] == 0)
                            return 1;
                        return Math.Tanh(tt[0]);
                    }
                case 35:
                    {
                        if (tt[0] > 0)
                            return 1;
                        return Math.Sqrt(tt[0]);
                    }
                case 36:
                    {
                        return Math.Pow(Math.E, tt[0]);
                    }
                case 37:
                    {
                        if (tt[0] > 0)
                            return 1;
                        return Math.Log10(tt[0]);
                    }
                case 38:
                    {
                        return Math.Log(tt[0], Math.E);
                    }
                case 39:
                    {
                        return tt[0] * tt[0] + tt[0] * tt[1] + tt[1] * tt[1];
                    }
                case 40:
                    {
                        return tt[0] * tt[0] * tt[0] + tt[1] * tt[1] * tt[1] + tt[2] * tt[2] * tt[2] + tt[0] * tt[1] * tt[2] + tt[0] * tt[1] + tt[1] * tt[2] + tt[0] * tt[2];
                    }
                default:
                    {
                        return double.NaN;
                    }
            }
        }

        public string DecodeWithOptimisationExpression(List<int> tokens, GPTerminalSet gpTerminalSet)
        {
            return "";
            //Prepare chromoseme for evaluation
            //    List<int> tokens = new List<int>();
            //    FunctionTree.ToListExpression(tokens, c.Root);
            int countT = tokens.Count;

            //Stack fr evaluation
            Stack<double> arguments = new Stack<double>();
            Stack<string> expression = new Stack<string>();

            for (int i = 0; i < countT; i++)
            {
                //Debug.Assert(tokens[i] != 2004 || countT <= 2);
                // Ako je token argument onda na osnovu naziv tog argumenta 
                //izvlacimo mu vrijednost iz skupa terminala i konstanti
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    double var;
                    string varaiable = terminals[tokens[i] - 1000].Name;

                    if (terminals[tokens[i] - 1000].IsConstant)
                        var = gpTerminalSet.TrainingData[0][tokens[i] - 1000];
                    else
                        var = double.NaN;
                    arguments.Push(var);
                    expression.Push(varaiable);
                }
                else//Ako je token funkcija tada ubacene argumente evaluiramo preko odredjene funkcije
                {
                    //Evaluacija funkcije. Svaka funkcija ima bar 1 argument
                    int count = functions[tokens[i] - 2000].Aritry;
                    string function = functions[tokens[i] - 2000].Definition;
                    //Ovdje moramo unazad zapisati varijable zbog Staka
                    double[] val = new double[count];
                    for (int j = count; j > 0; j--)
                    {
                        val[j - 1] = arguments.Pop();
                        string oldStr = "x" + (j).ToString();
                        string newStr = expression.Pop();
                        function = function.Replace(oldStr, newStr);
                    }
                    /*
                    //Izracunavanje rezultata 
                    //Ako je neka funkcija statistička distribucija ili neka druga funkcija koja 
                    // zahtjeva ulazne varijable da bi koristila neku statisticku osobinu
                    //
                    double[] values = null;
                    if (functions[tokens[i] - 2000].IsDistribution)
                    {
                        values = new double[gpTerminalSet.NumVariables];
                        for (int k = 0; k < gpTerminalSet.NumVariables; k++)
                            values[k] = gpTerminalSet.TrainingData[numRow][k];
                    }*/
                    double result = Evaluate(functions[tokens[i] - 2000], val);
                    if (!double.IsNaN(result))
                    {
                        expression.Push(result.ToString());
                    }
                    else
                        expression.Push("(" + function + ")");
                    //Izracunavanje izraza
                    arguments.Push(result);

                }
            }
            // return the only value from stack
            Debug.Assert(expression.Count == 1);
            // return arguments.Pop();
            return expression.Pop();
        }
        public string DecodeExpression(List<int> tokens, GPTerminalSet gpTerminalSet)
        {
            //Prepare chromoseme for evaluation
            //    List<int> tokens = new List<int>();
            //    FunctionTree.ToListExpression(tokens, c.Root);
            int countT = tokens.Count;

            //Stack fr evaluation
            Stack<string> expression = new Stack<string>();

            for (int i = 0; i < countT; i++)
            {
                //Debug.Assert(tokens[i] != 2004 || countT <= 2);
                // Ako je token argument onda na osnovu naziv tog argumenta 
                //izvlacimo mu vrijednost iz skupa terminala i konstanti
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    string varaiable = terminals[tokens[i] - 1000].Name;
                    expression.Push(varaiable);
                }
                else//Ako je token funkcija tada ubacene argumente evaluiramo preko odredjene funkcije
                {
                    //Evaluacija funkcije. Svaka funkcija ima bar 1 argument
                    int count = functions[tokens[i] - 2000].Aritry;
                    string function = functions[tokens[i] - 2000].Definition;
                    //Ovdje moramo unazad zapisati varijable zbog Staka
                    double[] val = new double[count];
                    for (int j = count; j > 0; j--)
                    {
                        string oldStr = "x" + (j).ToString();
                        string newStr = expression.Pop();
                        function = function.Replace(oldStr, newStr);
                    }

                    //Izracunavanje rezultata 
                    expression.Push("(" + function + ")");
                    //Izracunavanje izraza
                }
            }
            // return the only value from stack
            Debug.Assert(expression.Count == 1);
            // return arguments.Pop();
            return expression.Pop();
        }
        public string DecodeExpressionInExcellForm(List<int> tokens, GPTerminalSet gpTerminalSet)
        {
            //Prepare chromoseme for evaluation
            //    List<int> tokens = new List<int>();
            //    FunctionTree.ToListExpression(tokens, c.Root);
            int countT = tokens.Count;

            //Stack fr evaluation
            Stack<string> expression = new Stack<string>();

            for (int i = 0; i < countT; i++)
            {
                //Debug.Assert(tokens[i] != 2004 || countT <= 2);
                // Ako je token argument onda na osnovu naziv tog argumenta 
                //izvlacimo mu vrijednost iz skupa terminala i konstanti
                if (tokens[i] >= 1000 && tokens[i] < 2000)
                {
                    string varaiable = terminals[tokens[i] - 1000].Name;
                    expression.Push(varaiable);
                }
                else//Ako je token funkcija tada ubacene argumente evaluiramo preko odredjene funkcije
                {
                    //Evaluacija funkcije. Svaka funkcija ima bar 1 argument
                    int count = functions[tokens[i] - 2000].Aritry;
                    string function = functions[tokens[i] - 2000].ExcelDefinition;
                    //Ovdje moramo unazad zapisati varijable zbog Staka
                    double[] val = new double[count];
                    for (int j = count; j > 0; j--)
                    {
                        string oldStr = "x" + (j).ToString();
                        string newStr = expression.Pop();
                        function = function.Replace(oldStr, newStr);
                    }
                    if (functions[tokens[i] - 2000].IsDistribution)
                    {
                        string oldStr = "xn";
                        string newStr = "X" + gpTerminalSet.NumVariables.ToString();
                        function = function.Replace(oldStr, newStr);
                        function = function.Replace("x0", "X1");
                    }
                    //Izracunavanje rezultata 
                    expression.Push("(" + function + ")");
                    //Izracunavanje izraza
                }
            }
            // return the only value from stack
            Debug.Assert(expression.Count == 1);
            // return arguments.Pop();
            return expression.Pop();
        }

        public List<GPFunction> GetFunctions()
        {
            return functions;
        }

        public List<GPTerminal> GetTerminals()
        {
            return terminals;
        }
        public int GetNumVariables()
        {
            if (terminals == null)
                return 0;
            return terminals.Count(x=>x.IsConstant==false);
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

        public void SetTerimals(List<GPTerminal> list)
        {
            terminals = list;
        }

        public void SetFunction(List<GPFunction> list)
        {
            functions = list;
        }
		
		
        

    }
}
