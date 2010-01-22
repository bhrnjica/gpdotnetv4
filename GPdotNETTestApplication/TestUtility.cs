using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using GPdotNETLib;

namespace GPdotNETTestApplication
{
    static public class TestUtility
    {
        //Lista funkcija koje se ucitavaju iz xml datoteke
        public static XDocument doc;
        public static List<GPFunction> functionSetsList;

        public static GPPopulation population;
        public static GPFunctionSet functionSet;
        public static GPTerminalSet terminalSet;
       

        public static double[][] GenerateExperiment()
        {
            double[][] exp = new double[20][];

            exp[0] = new double[4];
            exp[0][0] = 0.1;
            exp[0][1] = 0.02;
            exp[0][2] = 700;
            exp[0][3] = 142.3806;
            exp[1] = new double[4];
            exp[1][0] = 0.1;
            exp[1][1] = 0.02;
            exp[1][2] = 725;
            exp[1][3] = 114.4588;
            exp[2] = new double[4];
            exp[2][0] = 0.1;
            exp[2][1] = 0.02;
            exp[2][2] = 750;
            exp[2][3] = 103.6912;
            exp[3] = new double[4];
            exp[3][0] = 0.1;
            exp[3][1] = 0.02;
            exp[3][2] = 775;
            exp[3][3] = 89.2577;
            exp[4] = new double[4];
            exp[4][0] = 0.1;
            exp[4][1] = 0.02;
            exp[4][2] = 800;
            exp[4][3] = 81.527;
            exp[5] = new double[4];
            exp[5][0] = 0.1;
            exp[5][1] = 0.02;
            exp[5][2] = 825;
            exp[5][3] = 75.975;
            exp[6] = new double[4];
            exp[6][0] = 0.1;
            exp[6][1] = 0.02;
            exp[6][2] = 850;
            exp[6][3] = 73.414;
            exp[7] = new double[4];
            exp[7][0] = 0.1;
            exp[7][1] = 0.2;
            exp[7][2] = 875;
            exp[7][3] = 75.9897;
            exp[8] = new double[4];
            exp[8][0] = 0.1;
            exp[8][1] = 0.07;
            exp[8][2] = 775;
            exp[8][3] = 104.3891;
            exp[9] = new double[4];
            exp[9][0] = 0.1;
            exp[9][1] = 0.495;
            exp[9][2] = 700;
            exp[9][3] = 172.084;
            exp[10] = new double[4];
            exp[10][0] = 0.1;
            exp[10][1] = 0.495;
            exp[10][2] = 725;
            exp[10][3] = 146.2491;
            exp[11] = new double[4];
            exp[11][0] = 1;
            exp[11][1] = 0.02;
            exp[11][2] = 700;
            exp[11][3] = 170.8625;
            exp[12] = new double[4];
            exp[12][0] = 1;
            exp[12][1] = 0.02;
            exp[12][2] = 725;
            exp[12][3] = 117.7174;
            exp[13] = new double[4];
            exp[13][0] = 10;
            exp[13][1] = 0.02;
            exp[13][2] = 700;
            exp[13][3] = 243.1075;
            exp[14] = new double[4];
            exp[14][0] = 10;
            exp[14][1] = 0.02;
            exp[14][2] = 750;
            exp[14][3] = 165.0988;
            exp[15] = new double[4];
            exp[15][0] = 10;
            exp[15][1] = 0.02;
            exp[15][2] = 875;
            exp[15][3] = 139.9954;
            exp[16] = new double[4];
            exp[16][0] = 10;
            exp[16][1] = 0.02;
            exp[16][2] = 900;
            exp[16][3] = 148.1874;
            exp[17] = new double[4];
            exp[17][0] = 10;
            exp[17][1] = 0.07;
            exp[17][2] = 700;
            exp[17][3] = 265.7264;
            exp[18] = new double[4];
            exp[18][0] = 10;
            exp[18][1] = 0.495;
            exp[18][2] = 875;
            exp[18][3] = 221.6268;
            exp[19] = new double[4];
            exp[19][0] = 10;
            exp[19][1] = 0.495;
            exp[19][2] = 900;
            exp[19][3] = 198.4156;

            return exp;
        }

        static public void TerminaliIFunkcije()
        {
            if (doc != null)
                return;
            Generateterminals();

            // Loading from a file, you can also load from a stream
            doc = XDocument.Load(@"FunctionSet.xml");
            // 
            var q = from c in doc.Descendants("FunctionSet")
                    select new GPFunction
                    {
                        Selected = bool.Parse(c.Element("Selected").Value),
                        Name = c.Element("Name").Value,
                        Definition = c.Element("Definition").Value,
                        Aritry = ushort.Parse(c.Element("Aritry").Value),
                        Description = c.Element("Description").Value,
                        IsReadOnly = bool.Parse(c.Element("ReadOnly").Value)

                    };
            if (functionSetsList != null)
            {
                if (functionSetsList.Count > 0)
                    functionSetsList.Clear();
            }
            functionSetsList = q.ToList();

            //Prvi ocisti stare
            if (functionSet == null)
                functionSet = new GPFunctionSet();
            functionSet.functions.Clear();
            //Ubaci nove funkcije
            functionSet.functions = q.Where(x => x.Selected).ToList();

            //Definisanje terminala
            for (int i = 0; i < 5; i++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = false;
                ter.Name = "X" + (i + 1).ToString();
                ter.Value = i;
                functionSet.terminals.Add(ter);

            }
            for (int j = 0; j < 10; j++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = true;
                ter.Name = "R" + (j + 1).ToString();
                ter.Value = j + 10;
                functionSet.terminals.Add(ter);
            }

        }
        //Generiranje teminala iz experimantalnih podataka i slucajnih konstanti
        static public bool Generateterminals()
        {

            if (terminalSet == null)
                terminalSet = new GPTerminalSet();
            else
                return true;

            int intOD = -10;

            int intDO = 10;

            short numConst = 6;
            double[] GPConstants = new double[numConst];

            for (int i = 0; i < numConst; i++)
            {
                decimal val = (decimal)(GPPopulation.rand.Next(intOD, intDO) + GPPopulation.rand.NextDouble());
                GPConstants[i] = (double)decimal.Round(val, 5);
            }

            double[][] trainingData = GenerateExperiment();
            //Kada znamo broj konstanti i podatke o experimentu sada mozemo popuniti trainingset
            terminalSet.NumConstants = numConst;
            terminalSet.NumVariables = (short)(trainingData.Length - 1);
            terminalSet.RowCount = (short)trainingData[0].Length;

            terminalSet.TrainingData = new double[terminalSet.RowCount][];
            int numOfVariables = terminalSet.NumVariables + terminalSet.NumConstants + 1/*Output Value of experiment*/;
            for (int i = 0; i < terminalSet.RowCount; i++)
            {
                terminalSet.TrainingData[i] = new double[numOfVariables];
                for (int j = 0; j < numOfVariables; j++)
                {
                    if (j < terminalSet.NumVariables)//Nezavisne varijable
                        terminalSet.TrainingData[i][j] = trainingData[j][i];
                    else if (j >= terminalSet.NumVariables && j < numOfVariables - 1)//Konstante
                        terminalSet.TrainingData[i][j] = GPConstants[j - terminalSet.NumVariables];
                    else
                        terminalSet.TrainingData[i][j] = trainingData[j - terminalSet.NumConstants][i];//Izlazna varijabla iz eperimenta
                }
            }
            //Ako smo ucitali podatke za testiranje Predikciju ovjde je ucitavam
            terminalSet.CalculateStat();

            return true;
        }

    }
}
