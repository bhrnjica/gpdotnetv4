using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using GPdotNET.Engine;
using GPdotNET.Core;
using GPdotNET.Util;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Globalization;
using GPdotNET.Core.Experiment;

namespace GPdotNET.Util
{
    public static class Utility
    {
        /// <summary>
        /// Exports data and the model to excel for ann models
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        public static void ExportToExcel(Experiment experiment, double[] y1, double[] y2, string strFilePath)
        {
            try
            {
                //
                var wb = new XLWorkbook();
                var ws1 = wb.Worksheets.Add("TRAINING DATA");
                var ws2 = ws1;
                if (experiment.IsTestDataExist())
                    ws2 = wb.Worksheets.Add("TESTING DATA");
                else
                    ws2 = null;

                ws1.Cell(1, 1).Value = "Training Data";

                if(ws2 != null)
                    ws2.Cell(1, 1).Value = "Testing Data";

                writeDataToExcel(experiment, ws1, false);
                if (experiment.IsTestDataExist())
                    writeDataToExcel(experiment, ws2, true);
                //change header of the output column
                ws1.Cell(2, experiment.GetColumnInputCount_FromNormalizedValue() + 3).Value = "Yann";
                if (ws2 != null)
                    ws2.Cell(2, experiment.GetColumnInputCount_FromNormalizedValue() + 3).Value = "Yann";

                for (int i=0; i< experiment.GetRowCount(); i++)
                {
                    ws1.Cell(3+i, experiment.GetColumnInputCount_FromNormalizedValue() + 3).Value = y1[i];
                    if (ws2 != null && y2.Length >= i+1)
                        ws2.Cell(3+i, experiment.GetColumnInputCount_FromNormalizedValue() + 3).Value = y2[i];
                }

                //
                wb.SaveAs(strFilePath);

                return;
                //writing formula is to big for excel.
                ////GP Model formula
                string formula = "";// strFormula;
                AlphaCharEnum alphaEnum = new AlphaCharEnum();

                //make a formula to denormalize value
                var cols = experiment.GetColumnsFromInput();
                int inputVarCount = experiment.GetColumnInputCount_FromNormalizedValue();
                var diff = experiment.GetColumnInputCount_FromNormalizedValue() - experiment.GetColumnInputCount();//diference between column count and normalized culm count due to Category column clasterization
                for (int i = inputVarCount - 1; i >= 0; i--)
                {
                    string var = "X" + (i + 1).ToString() + " ";
                    string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";

                    //make a formula to denormalize value
                    var col = cols[i - diff];
                    string replCell = cell;
                    if (col.ColumnDataType == ColumnDataType.Categorical)
                    {
                        formula = formula.Replace(var, replCell);
                        if (diff > 0)
                            diff -= 1;
                    }
                    else if (col.ColumnDataType == ColumnDataType.Binary)
                    {
                        formula = formula.Replace(var, replCell);
                    }
                    else
                    {
                        replCell = createNormalizationFormulaForColumn(col, cell);
                        formula = formula.Replace(var, replCell);
                    }

                }

                //in case of category output
                //category output is precalculated with sigmoid miltpy with Class count.
                var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();
                if (outCol.ColumnDataType == ColumnDataType.Categorical || outCol.ColumnDataType == ColumnDataType.Binary)
                {
                    int cc = outCol.Statistics.Categories.Count;
                    //then C1<1,C2<2,C3<3.....
                    //var val1 = Math.Exp(-1.0 * normalizedOutputRow[i]);
                    // val1 = outputCols[0].Statistics.Categories.Count * (1 / (1 + val1));
                    formula = "TRUNC(" + cc.ToString() + "*(1/(1+Exp(-1*" + formula + "))),0)";

                }
                else
                {
                    var normFormula = createDeNormalizationFormulaForOutput(outCol, formula);
                    formula = normFormula;
                }

                //in case of decimal point, semicolon of Excell formula must be replaced with comma
                if ("." == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                    formula = formula.Replace(";", ",");

                ws1.Cell(3, inputVarCount + 3).Value = formula;
                if ((ws2 != null))
                    ws2.Cell(3, inputVarCount + 3).Value = formula;

                //
                wb.SaveAs(strFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Exports data and the model to excel for newver GPdotNET version
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        public static void ExportToExcel(Experiment experiment, int inputVarCount, int constCount, GPNode ch, string strFilePath)
        {
            try
            {
                //
                var wb = new XLWorkbook();
                var ws1 = wb.Worksheets.Add("TRAINING DATA");
                var ws2 = ws1;
                if (Globals.gpterminals.TestingData != null)
                    ws2 = wb.Worksheets.Add("TESTING DATA");
                else
                    ws2 = null;


                ws1.Cell(1, 1).Value = "Training Data";
                //
                if(ws2 != null)
                    ws2.Cell(1, 1).Value = "Testing Data";

                writeDataToExcel(experiment, ws1, false);

                if (Globals.gpterminals.TestingData != null)
                    writeDataToExcel(experiment, ws2, true);


                //GP Model formula
                string formula = Globals.functions.DecodeExpression(ch, true);
                AlphaCharEnum alphaEnum = new AlphaCharEnum();

                //make a formula to denormalize value
                var cols = experiment.GetColumnsFromInput();
                var diff = inputVarCount - cols.Count;//diference between column count and normalized culm count due to Category column clasterization
                for (int i = inputVarCount - 1; i >= 0; i--)
                {
                    string var = "X" + (i + 1).ToString() + " ";
                    string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";

                    //make a formula to denormalize value
                    var col = cols[i-diff];
                    string replCell = cell;
                    if (col.ColumnDataType == ColumnDataType.Categorical)
                    {
                        formula = formula.Replace(var, replCell);
                        if(diff>0)
                            diff -= 1;
                    }
                    else if (col.ColumnDataType == ColumnDataType.Binary)
                    {
                        formula = formula.Replace(var, replCell);
                    } 
                    else
                    {
                        replCell = createNormalizationFormulaForColumn(col, cell);
                        formula = formula.Replace(var, replCell);
                    }
                    
                }

                //Replace random constants with real values
                for (int i = constCount - 1; i >= 0; i--)
                {
                    string var = "R" + (i + 1).ToString() + " ";
                    string constValue = Globals.gpterminals.TrainingData[0][i + inputVarCount].ToString();
                    formula = formula.Replace(var, constValue);
                }

                //in case of category output
                //category output is precalculated with sigmoid miltpy with Class count.
                var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();
                if(outCol.ColumnDataType == ColumnDataType.Categorical || outCol.ColumnDataType == ColumnDataType.Binary)
                {
                    int cc = outCol.Statistics.Categories.Count;
                    //then C1<1,C2<2,C3<3.....
                    //var val1 = Math.Exp(-1.0 * normalizedOutputRow[i]);
                    // val1 = outputCols[0].Statistics.Categories.Count * (1 / (1 + val1));
                    formula = "TRUNC("+cc.ToString()+"*(1/(1+Exp(-1*" + formula + "))),0)";
                   
                }
                else
                {
                    var normFormula  = createDeNormalizationFormulaForOutput(outCol, formula);
                    formula = normFormula;
                }

                //in case of decimal point, semicolon of Excell formula must be replaced with comma
                if ("." == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                    formula = formula.Replace(";", ",");

                ws1.Cell(3, inputVarCount + 3).Value = formula;

                if (ws2 != null)
                    ws2.Cell(3, inputVarCount + 3).Value = formula;
                //
                wb.SaveAs(strFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Export result for older version V3, V2...
        /// </summary>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        public static void ExportToExcel(int inputVarCount, int constCount, GPNode ch, string strFilePath)
        {
            try
            {
                //
                var wb = new XLWorkbook();
                var ws1 = wb.Worksheets.Add("TRAINING DATA");
                var ws2 = ws1;
                if (Globals.gpterminals.TestingData != null)
                    ws2 = wb.Worksheets.Add("TESTING DATA");
                else
                    ws2 = null;


                ws1.Cell(1, 1).Value = "Training Data";
                if(ws2!=null)
                    ws2.Cell(1, 1).Value = "Testing Data";

                writeDataToExcel(ws1, inputVarCount, constCount, Globals.gpterminals.TrainingData);
                if(Globals.gpterminals.TestingData!=null)
                    writeDataToExcel(ws2, inputVarCount, constCount, Globals.gpterminals.TestingData);


                //GP Model formula
                string formula = Globals.functions.DecodeExpression(ch, true);
                AlphaCharEnum alphaEnum = new AlphaCharEnum();
               
                //Normalized input
                for (int i = inputVarCount - 1; i >= 0; i--)
                {
                    string var = "X" + (i + 1).ToString() + " ";
                    string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";
                    formula = formula.Replace(var, cell);
                }
                //Random constants
                for (int i = constCount-1; i >=0; i--)
                {
                    string var = "R" + (i + 1).ToString() + " ";
                    string cell = alphaEnum.AlphabetFromIndex(inputVarCount + 2 + i) + "3";
                    formula = formula.Replace(var, cell);
                }

                ws1.Cell(3, inputVarCount + constCount + 3).Value = formula;

                if (Globals.gpterminals.TestingData != null)
                    ws2.Cell(3, inputVarCount + constCount + 3).Value = formula;
                //
                wb.SaveAs(strFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  Write data set to excell worksheet
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="data"></param>
        private static void writeDataToExcel(IXLWorksheet ws, int inputVarCount, int constCount, double[][] data)
        {
            //TITLE
           
            ws.Range("A1", "D1").Style.Font.Bold = true;
            ws.Range("A1", "D1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            //COLUMNS NAMES
            //RowNumber
            ws.Cell(2, 1).Value = "Nr";
            //Input variable names
            for (int i = 0; i < inputVarCount; i++)
            {
                ws.Cell(2, i + 2).Value = "X" + (i + 1).ToString();
            }
            //COnstants
            for (int i = 0; i < constCount; i++)
            {
                ws.Cell(2, inputVarCount + i + 2).Value = "R" + (i + 1).ToString();
            }
            //Output names
            ws.Cell(2, inputVarCount + constCount + 2).Value = "Y";
            ws.Cell(2, inputVarCount + constCount + 3).Value = "Ygp";

            //Add Data.
            for (int i = 0; i < data.Length; i++)
            {
                ws.Cell(i + 3, 1).Value = i + 1;

                for (int j = 0; j < data[i].Length; j++)
                    ws.Cell(i + 3, j + 2).Value = data[i][j];

            }
        }

        /// <summary>
        /// Write data set to excell worksheet
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="ws"></param>
        /// <param name="isTest"></param>
        private static void writeDataToExcel(Experiment experiment, IXLWorksheet ws, bool isTest=false)
        {
            //TITLE
            ws.Range("A1", "D1").Style.Font.Bold = true;
            ws.Range("A1", "D1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            //COLUMNS NAMES
            //RowNumber
            ws.Cell(2, 1).Value = "Nr";

            //getinput parameter column
            var cols= experiment.GetColumns().Where(x=>!x.IsOutput).ToList();
            int cellIndex = 2;//starting with offset of 2 cells  
            //Input variable names
            for (int i = 0; i < cols.Count; i++)
            {

                if(cols[i].ColumnDataType == ColumnDataType.Categorical)
                {
                    for(int j=0; j< cols[i].Statistics.Categories.Count; j++)
                    {
                        ws.Cell(2, cellIndex).Value = cols[i].Name + "(class = "+ cols[i].Statistics.Categories[j] + ")";
                        cellIndex ++;
                    }
                }
                else if (cols[i].ColumnDataType == ColumnDataType.Binary)
                {
                    ws.Cell(2, cellIndex).Value = cols[i].Name + "("+ cols[i].Statistics.Categories[0] + " = 0, "+ cols[i].Statistics.Categories[1] + " = 1)";
                    cellIndex++;
                }
                else
                {
                    ws.Cell(2, cellIndex).Value = cols[i].Name;
                    cellIndex ++;
                }
                    
            }
            
            //Output names
            var outCol = experiment.GetColumns().Where(x => x.IsOutput).FirstOrDefault();
            string nameCol = outCol.Name;
            string nameColgp = "Ygp";
            //in case of binary or categ expand name with class names too
            if (outCol.ColumnDataType == ColumnDataType.Categorical || outCol.ColumnDataType == ColumnDataType.Binary)
            {
                nameCol = nameCol + " (";
                nameColgp = nameColgp + " (";
                for (int i=0; i < outCol.Statistics.Categories.Count; i++)
                {
                    var c = outCol.Statistics.Categories[i];
                    nameCol += c +"="+i.ToString() + ",";
                    nameColgp += c + "=" + i.ToString() + ",";
                }
                nameCol = nameCol.Substring(0, nameCol.Length - 1) + ")";
                nameColgp = nameColgp.Substring(0, nameColgp.Length - 1) + ")";
            }

            //
            ws.Cell(2, cellIndex).Value = nameCol;
            ws.Cell(2, cellIndex + 1).Value = nameColgp;

            //Add Data.         
            for (int i = 0; i < experiment.GetRowCount(isTest); i++)
            {
               ws.Cell(i + 3, 1).Value = i + 1;
              //get normalized and numeric row
               var row = experiment.GetRowFromInput(i,isTest);
               var row_norm = experiment.GetNormalizedInput(i, isTest);
                cellIndex = 2;//starting with offset of 2 cells  
                for (int j = 0; j < cols.Count; j++)
                {

                    if (cols[j].ColumnDataType == ColumnDataType.Categorical )
                    {
                        for (int k = 0; k < cols[j].Statistics.Categories.Count; k++)
                        {
                            ws.Cell(i + 3, cellIndex).Value = row_norm[cellIndex - 2];
                            cellIndex++;
                        }
                    }
                    else if(cols[j].ColumnDataType == ColumnDataType.Binary)
                    {
                        ws.Cell(i + 3, cellIndex).Value = row_norm[cellIndex - 2];
                        //
                        cellIndex++;
                    }
                    else
                    {
                        ws.Cell(i + 3, cellIndex).Value = row[j];
                        //
                        cellIndex++;
                    }

                    if (j + 1 == cols.Count())//add output value
                        ws.Cell(i + 3, cellIndex).Value = experiment.GetRowFromOutput(i,isTest)[0];

                }
            }
        }

     
        /// <summary>
        /// returning the excel formula of normalization
        /// </summary>
        /// <param name="col"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        private static string createNormalizationFormulaForColumn(ColumnData col, string varName)
        {
            //
            if (col.Normalization == NormalizationType.Gauss)
            {
                //
                var str = string.Format("(({0}-{1})/{2})", varName, col.Statistics.Mean, col.Statistics.StdDev);
                return str;
            }
            else if (col.Normalization == NormalizationType.MinMax)
            {
                //
                var str = string.Format("(({0}-{1})/({2}-{1}))", varName, col.Statistics.Min, col.Statistics.Max);
                return str;
            }
            else if (col.Normalization == NormalizationType.Custom)
            {
                return varName;
            }
            else if (col.Normalization == NormalizationType.None)
            {
                return varName;
            }
            else
                throw new Exception("Unknown normalization data type.");

        }

        /// <summary>
        /// returning the excel formula of denormalization
        /// </summary>
        /// <param name="col"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        private static string createDeNormalizationFormulaForOutput(ColumnData col, string varName)
        {
            //
            if (col.Normalization == NormalizationType.Gauss)
            {
                //
                var str = string.Format("(({2}*{0}+{1}))", varName, col.Statistics.Mean, col.Statistics.StdDev);
                return str;
            }
            else if (col.Normalization == NormalizationType.MinMax)
            {
                //
                var str = string.Format("({0}*{1}+{2})", varName, col.Statistics.Max- col.Statistics.Min, col.Statistics.Min);
                return str;
            }
            else if (col.Normalization == NormalizationType.Custom)
            {
                return varName;
            }
            else if (col.Normalization == NormalizationType.None)
            {
                return varName;
            }
            else
                throw new Exception("Unknown normalization data type.");

        }

        /// <summary>
        /// export training data to csv file
        /// </summary>
        /// <param name="data"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        /// <param name="btrainingData"></param>
        public static void ExportToCSV(double[][] data, int inputVarCount, int constCount, GPNode ch, string strFilePath, bool btrainingData = true)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {


                    string workSheet = btrainingData ? "TRAINING DATA":"TESTING DATA";
                    tw.Flush();
                    //TITLE
                    tw.WriteLine(workSheet);

                    //COLUMNS NAMES
                    //RowNumber
                    string line = "Nr;";
                    //Input variable names
                    for (int i = 0; i < inputVarCount; i++)
                        line = line + "X" + (i + 1).ToString() + ";";

                    //COnstants
                    for (int i = 0; i < constCount; i++)
                        line = line + "R" + (i + 1).ToString() + ";";

                    //Output names
                    line = line + "Y;";
                    line = line + "Ygp";
                    tw.WriteLine(line);


                    //Add Data.
                    var Ygp=Globals.CalculateGPModel(ch, btrainingData);
                    for (int i = 0; i < data.Length; i++)
                    {
                        line = "";
                        line = (i + 1).ToString() + ";";

                        for (int j = 0; j < data[i].Length; j++)
                            line =line+ (data[i][j]).ToString() + ";";

                        //calculate Ygp
                        line = line + Ygp[i];

                        tw.WriteLine(line);
                    }

                    //GP Model formula
                    
                    //tw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Export training data and model formula to Mathamtica . For GPdotNET v2,v2
        /// </summary>
        /// <param name="data"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        /// <param name="btrainingData"></param>
        public static void ExportToMathematica(double[][] data, int inputVarCount, int constCount, GPNode ch, string strFilePath, bool btrainingData = true)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {


                    string workSheet = btrainingData ? "TRAINING DATA" : "TESTING DATA";
                    tw.Flush();

                    //Add Data.
                    string cmd = "data={";
                    //
                    for (int i = 0; i < data.Length; i++)
                    {
                        string line = "{";

                        //input variable
                        for (int j = 0; j < Globals.GetTerminalVarCount(); j++)
                        {
                            line += data[i][j].ToString(CultureInfo.InvariantCulture);
                            if (j + 1 < Globals.GetTerminalVarCount())
                                line += ",";
                            else
                            {
                                line +=","+ data[i][data[i].Length-1].ToString(CultureInfo.InvariantCulture);
                                line += "}";
                            }
                        }
                        //
                        cmd += line;
                        if (i+ 1 < data.Length)
                            cmd += ",";
                        else
                            cmd += "};";
                        
                    }
                    tw.WriteLine(cmd); 

                    //GP Model formula
                    string formula ="gpModel="+ Globals.functions.DecodeExpression(ch, 1);
                    AlphaCharEnum alphaEnum = new AlphaCharEnum();
                    for (int i = inputVarCount - 1; i >= 0; i--)
                    {
                        string var = "x" + (i + 1).ToString() + " ";
                        string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";
                        formula = formula.Replace(var, cell);
                    }
                   for (int i = constCount - 1; i >= 0; i--)
                    {
                        string var = "R" + (i + 1).ToString();
                        string vall = data[0][Globals.GetTerminalVarCount() + i].ToString(CultureInfo.InvariantCulture);
                        if (vall[0] == '-')
                            vall = "(" + vall + ")";

                        formula = formula.Replace(var, vall);
                    }
                   

                    tw.WriteLine(formula+";"); 
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Export training data and model formula to Mathamtica .For GPdotNET v4, ...
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        /// <param name="isTest"></param>
        public static void ExportToMathematica(Experiment experiment, int inputVarCount, int constCount, GPNode ch, string strFilePath, bool isTest = false)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {


                    string workSheet = !isTest ? "TRAINING DATA" : "TESTING DATA";
                    tw.Flush();
                    //Add Data.
                    var cols = experiment.GetColumnsFromInput();
                    string cmd = "data={";
                    var rowCount = experiment.GetRowCount(isTest);
                    var colCount = experiment.GetColumnInputCount();
                    //
                    for (int i = 0; i < rowCount; i++)
                    {
                        string line = "{";
                        //get normalized and numeric row
                        var row = experiment.GetRowFromInput(i, isTest);
                        var row_norm = experiment.GetNormalizedInput(i, isTest);
                        var cellIndex = 2;//starting with offset of 2 cells  
                                          //input variable
                        for (int j = 0; j < cols.Count; j++)
                        {
                            if (cols[j].ColumnDataType == ColumnDataType.Categorical)
                            {
                                int clsCount = cols[j].Statistics.Categories.Count;
                                for (int k = 0; k < clsCount; k++)
                                {
                                    line += row_norm[cellIndex - 2].ToString(CultureInfo.InvariantCulture);
                                    cellIndex++;

                                    if(k+1 != clsCount)
                                        line += ",";
                                }
                            }
                            else if (cols[j].ColumnDataType == ColumnDataType.Binary)
                            {
                                line += row_norm[cellIndex - 2].ToString(CultureInfo.InvariantCulture);
                                cellIndex++;
                            }
                            else
                            {
                                line += row[j].ToString(CultureInfo.InvariantCulture);
                                //
                                cellIndex++;
                            }

                            if (j + 1 != cols.Count())
                                line += ",";
                            else
                            {
                                line += "," + experiment.GetRowFromOutput(i, isTest)[0].ToString(CultureInfo.InvariantCulture);
                                line += "}";
                            }
                        }
                        //
                        cmd += line;
                        if (i + 1 < rowCount)
                            cmd += ",";
                        else
                            cmd += "};";

                    }
                    tw.WriteLine(cmd);

                    //GP Model formula
                    string formula = Globals.functions.DecodeExpression(ch, 1);
                    List<string> inputArgs = new List<string>();
                    AlphaCharEnum alphaEnum = new AlphaCharEnum();
                    var diff = inputVarCount - cols.Count;//diference between column count and normalized culm count due to Category column clasterization
                    for (int i = inputVarCount - 1; i >= 0; i--)
                    {
                        string var = "X" + (i + 1).ToString() + " ";
                        string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";
                        
                        //make a formula to denormalize value
                        var col = cols[i - diff];
                        string replCell = cell;
                        if (col.ColumnDataType == ColumnDataType.Categorical)
                        {
                            //formula = formula.Replace(var, replCell);
                            if (diff > 0)
                                diff -= 1;
                        }
                        else if (col.ColumnDataType == ColumnDataType.Binary)
                        {
                            //formula = formula.Replace(var, replCell);
                        }
                        else
                        {
                            replCell = createNormalizationFormulaForColumn(col, var);
                            formula = formula.Replace(var, replCell);
                        }
                        // 
                        inputArgs.Add(var);

                    }
                    for (int i = constCount - 1; i >= 0; i--)
                    {
                        string var = "R" + (i + 1).ToString();
                        string vall = Globals.gpterminals.TrainingData[0][i + inputVarCount].ToString(CultureInfo.InvariantCulture);
                        if (vall[0] == '-')
                            vall = "(" + vall + ")";

                        formula = formula.Replace(var, vall);
                    }

                    //in case of category output
                    //category output is precalculated with sigmoid miltpy with Class count.
                    var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();
                    if (outCol.ColumnDataType == ColumnDataType.Categorical || outCol.ColumnDataType == ColumnDataType.Binary)
                    {
                        int cc = outCol.Statistics.Categories.Count;
                        //then C1<1,C2<2,C3<3.....
                        formula ="IntegerPart[" + cc.ToString(CultureInfo.InvariantCulture) + "*(1/(1+Exp[-1*" + formula + "]))]";                   
                    }
                    else//for numeric output we need to denormalize formula
                    {
                        var normFormula = createDeNormalizationFormulaForOutput(outCol, formula);
                        formula = normFormula;
                    }

                    //add model name and arguments
                    formula = "gpModel[{0}]:=" + formula;

                    //add arguments to the model
                    string arguments = "";
                    for(int i=0; i < inputArgs.Count; i++)
                    {
                        var a = inputArgs[i];
                        if(formula.Contains(a))
                        {
                            if (i == 0)
                                a = a.Replace(" ","_");
                            else
                                a = a.Replace(" ", "_,"); ;
                            //
                            arguments = a + arguments;
                        }      
                    }
                    formula = string.Format(formula, arguments);
                    tw.WriteLine(formula + ";");
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Export training data and model formula to r Language .For GPdotNET v4, ...
        /// </summary>
        /// <param name="experiment"></param>
        /// <param name="inputVarCount"></param>
        /// <param name="constCount"></param>
        /// <param name="ch"></param>
        /// <param name="strFilePath"></param>
        /// <param name="isTest"></param>
        public static void ExportToR(Experiment experiment, int inputVarCount, int constCount, GPNode ch, string strFilePath, bool isTest = false)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {
                    string workSheet = !isTest ? "TRAINING DATA" : "TESTING DATA";
                    tw.Flush();

                    //Add Data.
                    var cols = experiment.GetColumnsFromInput();
                    var rowCount = experiment.GetRowCount(isTest);
                    var colCount = experiment.GetColumnInputCount();

                    //GP Model formula
                    //
                    string formula = Globals.functions.DecodeExpression(ch, 3);
                    List<string> inputArgs = new List<string>();
                    AlphaCharEnum alphaEnum = new AlphaCharEnum();
                    var diff = inputVarCount - cols.Count;//diference between column count and normalized culm count due to Category column clasterization
                    for (int i = inputVarCount - 1; i >= 0; i--)
                    {
                        string var = "X" + (i + 1).ToString() + " ";
                        string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";
                        //make a formula to denormalize value
                        var col = cols[i - diff];
                        string replCell = cell;

                        if (col.ColumnDataType == ColumnDataType.Categorical)
                        {
                            //decreas diference between nurmalized and numeric columns
                            if (diff > 0)
                                diff -= 1;
                        }
                        else if (col.ColumnDataType == ColumnDataType.Numeric)
                        {
                            replCell = createNormalizationFormulaForColumn(col, var);
                            formula = formula.Replace(var, replCell);
                        }

                        // 
                        inputArgs.Add(var);

                    }
                    for (int i = constCount - 1; i >= 0; i--)
                    {
                        string var = "R" + (i + 1).ToString();
                        string vall = Globals.gpterminals.TrainingData[0][i + inputVarCount].ToString(CultureInfo.InvariantCulture);
                        if (vall[0] == '-')
                            vall = "(" + vall + ")";

                        formula = formula.Replace(var, vall);
                    }

                    //in case of category output
                    //category output is precalculated with sigmoid miltpy with Class count.
                    var outCol = experiment.GetColumnsFromOutput().FirstOrDefault();
                    if (outCol.ColumnDataType == ColumnDataType.Categorical || outCol.ColumnDataType == ColumnDataType.Binary)
                    {
                        int cc = outCol.Statistics.Categories.Count;
                        //then C1<1,C2<2,C3<3.....
                        formula = " return (" + "trunc(" + cc.ToString(CultureInfo.InvariantCulture) + "*(1/(1+exp(-1*" + formula + ")))))";
                    }
                    else
                    {
                        var normFormula = createDeNormalizationFormulaForOutput(outCol, formula);
                        formula = normFormula;
                    }


                    //add name  of the mode
                    formula = @"gpModel<- function({0}) {{" + formula;

                    //add arguments to the model
                    string arguments = "";
                    for (int i = 0; i < inputArgs.Count; i++)
                    {
                        var a = inputArgs[i];
                        if (formula.Contains(a))
                        {
                            if (i == 0)
                                a = a.Replace(" ", "");
                            else
                                a = a.Replace(" ", ",");
                            //
                            arguments = a + arguments;
                        }
                    }
                    formula = string.Format(formula, arguments);
                    formula = formula + " }";
                    tw.WriteLine(formula + ";");
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
