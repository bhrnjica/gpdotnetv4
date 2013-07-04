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
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using GPdotNET.Engine;
using GPdotNET.Core;
using System.Threading;
using GPdotNET.Tool.Common;
using System.IO;
using System.Globalization;
using GPdotNET.Util;

namespace GPdotNET.App
{
    public partial class MainWindow
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool OpenFromFile()
        {
            var strFile = GPModelGlobals.GetFileFromOpenDialog("GPdotNET file format", "*.gpa");

           if (string.IsNullOrEmpty(strFile))
               return false;

           if (!CloseCurrentModel())
               return false;

           bool retVal= Open(strFile);

           return retVal; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        private bool Open(string strFile)
        {
            
            if (!File.Exists(strFile))
            {
                MessageBox.Show("File doesnt exist!");
                return false;
            }

            _filePath = strFile;

            string buffer;
            ResetProgram();
            // open selected file and retrieve the content
            using (StreamReader reader = System.IO.File.OpenText(strFile))
            {
                //read TrainingData in to buffer
                buffer = reader.ReadToEnd();
                reader.DiscardBufferedData();
                //reader.Close();
            }

            //define the lines from file
            var lines = (from l in buffer.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                         where l[0] != '!' && l[0] != '\r'
                         select l.IndexOf('!') == -1 ? l : l.Remove(l.IndexOf('!'))
                         ).ToArray();

            //Line 1: GPModel 
            int mod = int.Parse(lines[0].Replace("\r", ""));
            _GPModel = (GPModelType)mod;

            //Optimization of analitic function is not implemented yet
            if (_GPModel == GPModelType.AnaliticFunctionOptimization)
            {
                MessageBox.Show("Open Analytic function optimization file is not implemented.!");
                return false;
            }
            LoadModelWizard(_GPModel);


            //Line 2: Training data
            if (_dataPanel != null)
            {

                double[][] data = _dataPanel.loadData(lines[1].Replace("\r", ""));
                if (data != null)
                    _dataPanel.LoadTrainingData(data, _GPModel);
            }


            //Line 3: Testing data
            if (_dataPanel != null)
            {
                //
                double[][] data = _dataPanel.loadData(lines[2].Replace("\r", ""));
                if (data != null)
                    _dataPanel.LoadTestingData(data);
            }



            //Line 4: TimeSeries  data
            if (_dataPanel != null)
            {
                //
                double[][] data = _dataPanel.loadSeriesData(lines[3].Replace("\r", ""));
                if (data != null)
                    _dataPanel.LoadSeriesData(data);
            }

            //Line 5: GP Parameters
            _setPanel.SetParameters(lines[4]);

            //Line 6:  GP Functions
            if(_funPanel!=null)
                _funPanel.SelectFunctions(lines[5]);

            //Line 7: GP type of Running program
            if (_baseRunPanel != null)
                _baseRunPanel.SetTypeofRun(lines[6]);

            //Line 8 to Line8+popSize: Current GP Population
            int currentLine = MainPopulationFromString(lines, _mainFactory, 6);
            if (_mainFactory != null && _mainFactory.GetFunctionSet() != null)
            {
                var e = new ProgressIndicatorEventArgs();
                e.ReportType = ProgramState.Finished;
                e.CurrentEvolution = 0;
                e.BestChromosome = _mainFactory.BestChromosome();
                e.AverageFitness = _mainFactory.GetAverageFitness();
                //enable GP engine
                _runningEngine = 1;
                ReportEvolution(e);
                _runningEngine = 0;
            }
            else
                currentLine++;

            //If GA exist read maximum and minumum values of variables
            if (currentLine < lines.Length)
            {
                currentLine++;
                string ss = lines[currentLine];
                if (_optimizePanel != null)
                    _optimizePanel.SetMaximumAndMinimumValues(ss);
            }

            //Line 8+popSIze to Current GA Population if exist
            if (currentLine < lines.Length)
            {

                currentLine = SecondPopulationFromString(lines, _secondFactory, currentLine, 2);

                if (_secondFactory!=null && _secondFactory.GetFunctionSet() != null)
                {
                    var e = new ProgressIndicatorEventArgs();
                    e.ReportType = ProgramState.Finished;
                    e.CurrentEvolution = 0;
                    e.BestChromosome = _secondFactory.BestChromosome();
                    e.AverageFitness = _secondFactory.GetAverageFitness();
                    //enable GP engine
                    _runningEngine = 2;
                    ReportEvolution(e);
                    _runningEngine = 0;
                }
            }

            //
            
            StringBuilder sb= new StringBuilder();
            for (int i = currentLine; i < lines.Length; i++ )
            {
                sb.Append(lines[i]);

            }
            var rtf= sb.ToString();
            if(rtf!=null && rtf[0]!='-')
              _infoPanel.InfoText = rtf;


            _isFileDirty = false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        bool SaveToFile(string strFile)
        {
            if (strFile == null)
                return false;
            if (_setPanel == null)
                return false;
            if (Globals.gpterminals == null || Globals.gpterminals.TrainingData == null)
            {
                MessageBox.Show("Cannot save empty model!");
                return false;
            }
            //Optimization of analitic function is not implemented yet
            if (_GPModel == GPModelType.AnaliticFunctionOptimization)
            {
                MessageBox.Show("Saving Analytic function optimization model is not implemented.!");
                return false;
            }

            // open selected file and retrieve the content
            using (TextWriter tw = new StreamWriter(strFile))
            {

                tw.Flush();
                //Line1: Moel type and header information
                tw.WriteLine("!GPdotNET v2.0 File format ");
                tw.WriteLine("!");
                tw.WriteLine("!line 1: GP Model  1- symbolic regression; 2 -symbolic regression with optimisation, 3 - time series, 4- analytic optimisation, 5- TSP");
                int model = (int)_GPModel;
                tw.WriteLine(model.ToString());

                //Line2: Training DATA 
                tw.WriteLine("!line 2 Training Data");
                string data;
                if (_dataPanel != null)
                {
                    data = _dataPanel.GetStringFromData(_dataPanel.Training);
                    if (data == null)
                        tw.WriteLine("-");
                    else
                        tw.WriteLine(data);
                }
                else
                    tw.WriteLine("-");

                //Line3: Teting DATA 
                tw.WriteLine("!line 3 Testing Data");
                if (_dataPanel != null)
                {
                    data = _dataPanel.GetStringFromData(_dataPanel.Testing);
                    if (data == null)
                        tw.WriteLine("-");
                    else
                        tw.WriteLine(data);
                }
                else
                    tw.WriteLine("-");

                //Line4: Series DATA 
                tw.WriteLine("!line 4 TimeSeries Data");
                if (_dataPanel != null)
                {
                    data = _dataPanel.GetStringFromData(_dataPanel.TimeSeries);
                    if (data == null)
                        tw.WriteLine("-");
                    else
                        tw.WriteLine(data);
                }
                else
                    tw.WriteLine("-");

                //Line 5: GP Parameters
                tw.WriteLine("!line 5: GP PArameters is sorted in the following order");
                tw.WriteLine("!popSIze;Fitness;Initialization;InitDepth;OperationDept;Elitism;Sel Method;Param1;Param2;Const_From;COnst_To;Con_COut;CrossOverProb;MutatProb;SeleProb;PermutationProb;EncaptulationProb;EnableEditing;EnableDecimation");
                data = _setPanel.ParametersToString();
                if (data == null)
                    tw.WriteLine("-");
                else
                    tw.WriteLine(data);

                //Line 6:  GP Functions
                tw.WriteLine("!line 6:- Selected Function");
                data = _funPanel==null?null: _funPanel.GetFunctionState();
                if (data == null)
                    tw.WriteLine("-");
                else
                    tw.WriteLine(data);

                //Line 7: GP type of Running program
                tw.WriteLine("!line 7  Type of Running program 0- means generation number, 1 - fitness value ;");
                tw.WriteLine("!        e.g. 1;700 - run program until max fitness is greate or equel than 700 ");
                tw.WriteLine("!             0;500 - run program for 500 evolutions ");

                if (_baseRunPanel != null)
                {
                    data = _baseRunPanel.GetTypeofRun();
                    if (data == null)
                        tw.WriteLine("-");
                    else
                        tw.WriteLine(data);
                }
                else
                    tw.WriteLine("-");

                //Line 8 to Line8+popSize: Current GP Population
                tw.WriteLine("!Line 8 Population: size;bestfitness:bestchromosometree");
                if (_baseRunPanel != null)
                {
                    GPFactory fac = _mainFactory;

                    var str = PreparePopulationForSave(fac);
                    if (str != null && str.Length > 0)
                    {
                        tw.WriteLine(str[0]);
                    }
                    else
                        tw.WriteLine("-");
                    if (str != null && str.Length > 1)
                    {
                        tw.WriteLine("!Line  represent chromosomes in population");
                        for (int i = 1; i < str.Length; i++)
                            tw.WriteLine(str[i]);
                    }
                    else
                        tw.WriteLine("-");
                }
                else 
                {
                    tw.WriteLine("!Line 8 Population: size;bestfitness:bestchromosometree");
                    tw.WriteLine("-");
                    tw.WriteLine("!Line  represent chromosomes in population");
                    tw.WriteLine("-");
                }

                //Next Line GA Population if exist
                if (_optimizePanel != null)
                {
                   
                    tw.WriteLine("!Line GA Terminals: maximum and minimum values of variables");
                    string st = PrepareMinMaxValues();
                    if (st != null)
                        tw.WriteLine(st);
                    else
                        tw.WriteLine("-");

                    tw.WriteLine("!Line GA Population: size; bestfitness: bestchromosometree; typeOfOptimization;");

                    string[] str = PreparePopulationForSave(_secondFactory);
                    if (str != null && str.Length > 0)
                    {
                        //wnen working with optimization wee need to know is Maximum or Minimum
                        var s =str[0] /*+ ";"*/ + (_optimizePanel.IsMinimum() ? "1" : "0");
                        tw.WriteLine(s);

                         //store function for optimization

                        tw.WriteLine("!Line GA Function to optimize");                       
                        if (_GPModel == GPModelType.SymbolicRegressionWithOptimization)
                        {
                            var ch = _secondFactory.BestChromosome() as GPChromosome;
                            if (ch != null)
                                s = ch.GetExpression().ToString();
                            else
                                s = "-";
                        }
                        else if (_GPModel == GPModelType.AnaliticFunctionOptimization)
                            s = _funDefinit.TreeNodeToGPNode().ToString();
                        else
                            throw new Exception("Wrong GPModel type!");

                       //write to file 
                       tw.WriteLine(s);
                    }
                    else
                        tw.WriteLine("-");
                    if (str != null && str.Length > 1)
                    {
                        tw.WriteLine("!Line  represent GA chromosomes in population");
                        for (int i = 1; i < str.Length; i++)
                            tw.WriteLine(str[i]);
                    }
                    else
                        tw.WriteLine("-");
                }
                else
                {
                    tw.WriteLine("!Line GA Terminals: maximum and minimum values of variables");
                    tw.WriteLine("-");
                    tw.WriteLine("!Line GA Population: size; bestfitness: bestchromosometree; typeOfOptimization; functionToOptimize");
                    tw.WriteLine("-");
                    tw.WriteLine("!Line GA Function to optimize");
                    tw.WriteLine("-");
                    tw.WriteLine("!Line  represent GA chromosomes in population");
                    tw.WriteLine("-");
                }

                //RTF Model Info
                tw.WriteLine("!Line  RTF text represent Model Info");
                tw.WriteLine(_infoPanel.InfoText);
               // tw.Close();
                _isFileDirty = false;
                return true;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="lines"></param>
       /// <param name="factory"></param>
       /// <param name="curLine"></param>
       /// <param name="typeChromosome"></param>
       /// <returns></returns>
        private int MainPopulationFromString(string[] lines, GPFactory factory, int curLine)
        {
            curLine++;
            //Line 8: populationSize; maxFitness; BestChromosome
            if (lines.Length <= curLine)
            {
                //MessageBox.Show("Fie is corrupt!");
                return -1;
            }
            var str = lines[curLine].Split(';');


            if (lines[curLine] == "-" || lines[curLine] == "-\r")
                return curLine+=2;

            if (_GPModel == GPModelType.TSP)
                PrepareTSP(false);
            else if(_GPModel == GPModelType.ALOC)
                PrepareALOC(false);
            else if (_GPModel == GPModelType.Transport)
                PrepareTrans(false);
            else
                PrepareGP(false);
            
           
            int popSize = 0;
            if(!int.TryParse(str[0], out popSize))
                popSize=0;
            
            List<IChromosome> chromosomes= new List<IChromosome>();

            for (int i = 0; i < popSize; i++)
            {
                IChromosome ch = null;
                if (_GPModel == GPModelType.TSP || _GPModel == GPModelType.ALOC )
                    ch = GAVChromosome.CreateFromString(lines[i + curLine + 1]);
                else if (_GPModel == GPModelType.Transport)
                    ch = GAMChromosome.CreateFromString(lines[i + curLine + 1]);
                else
                    ch = GPChromosome.CreateFromString(lines[i + curLine + 1]);
                chromosomes.Add(ch);
            }

            factory.SetChromosomes(chromosomes);

            factory.CalculatePopulation();
            return popSize+7;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="factory"></param>
        /// <param name="curLine"></param>
        /// <param name="typeChromosome"></param>
        /// <returns></returns>
        private int SecondPopulationFromString(string[] lines, GPFactory factory, int curLine, int typeChromosome = 1)
        {
            curLine++;
            //Line 8: populationSize; maxFitness; BestChromosome
            if (lines.Length <= curLine)
            {
                //MessageBox.Show("Fie is corrupt!");
                return -1;
            }
            var str = lines[curLine].Split(';');


            if (lines[curLine] == "-" || lines[curLine] == "-\r")
                return curLine += 3;

            //first number is popSIze
            int popSize = 0;
            if (!int.TryParse(str[0], out popSize))
                popSize = 0;

            if (_optimizePanel != null)
            {
                //last number i +s optimization type
                string optType = str[str.Length - 1];
                if (optType == "0" || optType == "0\r")
                    _optimizePanel.SetOptType(false);
                else
                    _optimizePanel.SetOptType(true);

                if (typeChromosome == 1)
                    PrepareGP(false);
                else if (typeChromosome == 2)
                {
                    PrepareGA(false);
                    GANumChromosome.functionSet = factory.GetFunctionSet();
                }
            }

            curLine++;
            //Get Optimization function

            List<IChromosome> chromosomes = new List<IChromosome>();
            curLine++;
            for (int i = 0; i < popSize; i++)
            {
                var ch = GANumChromosome.CreateFromString(lines[i + curLine]);
                chromosomes.Add(ch);
            }
            if (factory != null)
            {
                factory.SetChromosomes(chromosomes);
                factory.CalculatePopulation();
            }
            return popSize==0?2:popSize + curLine;

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        private string[] PreparePopulationForSave(GPFactory factory)
        {
            string[] str;
            if (factory != null)
            {
                //var pop = factory.GetPopulation();
                var popSize = factory.GetpopSize();
                var chroms = factory.GetChromosomes();
                var best = factory.BestChromosome();

                if (popSize == 0)
                {
                    str = new string[popSize + 1];
                    str[0] = "0;-";
                    return str;
                }

               str = new string[popSize + 1];
               
               str[0] = popSize.ToString()+";";
               str[0]+=best == null ? "-;-" : best.ToString();

               for (int i = 0; i < popSize; i++)
                 str[i + 1] = chroms[i].ToString();
               
               return str;
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string PrepareMinMaxValues()
        {
            if (_secondFactory != null)
            {
               var fs = _secondFactory.GetFunctionSet();
               var ts= fs.GetTerminals().Values.Where(x=>x.IsConstant==false).ToList();

               if (ts != null && ts.Count>0)
               {
                   string str = "";
                   foreach (var t in ts)
                   {
                       str+=t.minValue.ToString(CultureInfo.InvariantCulture)+";"+t.maxValue.ToString(CultureInfo.InvariantCulture);
                       str += "\t";
                   }

                   return str;
               }
            }

            return null;
        }
       
	}
}
