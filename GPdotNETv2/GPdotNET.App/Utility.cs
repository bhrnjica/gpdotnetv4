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

namespace GPdotNET.App
{
    public static class Utility
    {
        public static Image LoadImageFromName(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var pic = asm.GetManifestResourceStream(name);
            return Image.FromStream(pic);
        }

        public static Icon LoadIconFromName(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var pic = asm.GetManifestResourceStream(name);
            return  new Icon(pic);
        }

        public static void ExportToExcel(double[][] data, int inputVarCount, int constCount, GPNode ch, string strFilePath, bool bTest = false)
        {
            try
            {
                string workSheet = bTest ? "TESTING DATA" : "TRAINING DATA";
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add(workSheet);

                //TITLE
                ws.Cell(1, 1).Value = workSheet;
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
                    ws.Cell(i + 3,1).Value = i + 1;

                    for (int j = 0; j < data[i].Length; j++)
                        ws.Cell(i+3, j+2).Value = data[i][j];

                }
                
               
                //GP Model formula
                string formula = Globals.functions.DecodeExpression(ch, true);
                AlphaCharEnum alphaEnum = new AlphaCharEnum();
                // char alphaStart = Char.Parse("B"); 
                for (int i = 0; i < inputVarCount; i++)
                {
                    string var = "X" + (i + 1).ToString();
                    string cell = alphaEnum.AlphabetFromIndex(2 + i) + "3";
                    // string cell=alphaStart.ToString()+"3";
                    formula = formula.Replace(var, cell);
                    //  alphaStart++;
                }

                for (int i = 0; i < constCount; i++)
                {
                    string var = "R" + (i + 1).ToString();
                    string cell = alphaEnum.AlphabetFromIndex(inputVarCount + 2 + i) + "3";
                    formula = formula.Replace(var, cell);
                    // alphaStart++;
                }
                ws.Cell(3, inputVarCount + constCount + 3).Value = formula;
                //Copy formula from cell
               // ws.Cell(3, inputVarCount + constCount + 2].Copy();
                //And paste to all sample TrainingData
               // for (int j = 1; j < data.Length; j++)
               // {
                //    oSheet.Paste(oSheet.Cells[j + 3, inputVarCount + constCount + 2]);
               // }
                wb.SaveAs(strFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ExportToCSV(double[][] data, int inputVarCount, int constCount, GPNode ch, string strFilePath, bool bTest = false)
        {
            try
            {
                // open selected file and retrieve the content
                using (TextWriter tw = new StreamWriter(strFilePath))
                {


                    string workSheet = bTest ? "TESTING DATA" : "TRAINING DATA";
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
                    var Ygp=Globals.CalculateGPModel(ch, !bTest);
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
