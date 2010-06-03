//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2010 Bahrudin Hrnjica                                                 //
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
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using GPdotNET.Properties;
using GPNETLib;
using ZedGraph;
namespace GPdotNET
{
    public partial class GP_TS_Panel : Form
    {
        public string filePath;

        GPPopulation population;
        public GPFunctionSet functionSet { get; private set; }
        public GPTerminalSet terminalSet { get; private set; }
        public double[] GPConstants { get; private set; }
        public GPChromosome GPBestHromosome { get; private set; }
        public bool IsGPRunning { get; private set; }
        
        GPParameters parameters;
        
        //Varijabla za izracunavanje vremena jednog una
        DateTime vrijemeZaJedanRun;
        DateTime pocetakEvolucije;
        
        //Lista funkcija koje se ucitavaju iz xml datoteke
        XDocument doc;
        public List<GPFunction> functionSetsList;

        
        //GP Training data
        public double[][] GPTrainingData { get; private set; }
        double[,] gpModel;

        //GP Testing data
        public double[][] GPTestingData { get; private set; }
        double[,] gpTestModel;

        //Time series prediction
        public bool TimeSeriesPrediction { get; set; }
        double[] timeSerie;

        

        //Model sa podacima za Graf
        LineItem modelItem;
        LineItem modelTestIten;
        LineItem maxFitness, avgFitness;
        

        //internal helper
        int velPopulacije;
        bool bParalel;
        int busloviEvolucije;
        float conditionValue;
        int tekucaEvolucija = 1;
        bool ignoreediting = true;


        public GP_TS_Panel()
        {
            InitializeComponent();

            terminalSet = new GPTerminalSet();
            functionSet = new GPFunctionSet();
            cmetodaGeneriranja.SelectedIndex = 2;
            cmetodaSelekcije.SelectedIndex = 0;
          
           evjerojatnostMutacije.Text = (5.0 / 100.0).ToString();
           evjerojatnostPermutacije.Text = (5.0 / 100.0).ToString();
           evjerojatnostReprodukcije.Text = (20.0 / 100.0).ToString();
           evjerojatnostUkrstanja.Text = (90.0 / 100.0).ToString();
           textBox6.Text = 1.ToString();
           IsGPRunning = false;

                      
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Podesavanje grafika            
            this.zedEksperiment.GraphPane.Title.Text = Resources.SR_ExperimentModel;
            this.zedEksperiment.GraphPane.XAxis.Title.Text = Resources.SR_ControlPoint;
            this.zedEksperiment.GraphPane.YAxis.Title.Text = Resources.SR_FunctionValue;

            this.zedChart.GraphPane.Title.Text = Resources.SR_GPModelSimulation;
            this.zedChart.GraphPane.XAxis.Title.Text = Resources.SR_NumPoints;
            this.zedChart.GraphPane.YAxis.Title.Text = Resources.SR_Output;

            this.zedGraphPopulation.GraphPane.Title.Text = Resources.SR_FitnessGraphSimulation;
            this.zedGraphPopulation.GraphPane.XAxis.Title.Text = Resources.SR_Generation;
            this.zedGraphPopulation.GraphPane.YAxis.Title.Text = Resources.SR_FittValue;

            maxFitness = zedGraphPopulation.GraphPane.AddCurve(Resources.SR_MaxFit, null, null, Color.Red, ZedGraph.SymbolType.None);
            avgFitness = zedGraphPopulation.GraphPane.AddCurve(Resources.SR_AvgFitness, null, null, Color.Blue, ZedGraph.SymbolType.None);
         
            //Podešavanje grafa
            zedGraphPopulation.GraphPane.XAxis.Scale.Max = 500;
            zedGraphPopulation.GraphPane.XAxis.Scale.Min = 0;
            zedGraphPopulation.GraphPane.YAxis.Scale.Min = 0;
            zedGraphPopulation.GraphPane.YAxis.Scale.Max = 1;
            this.zedGraphPopulation.GraphPane.AxisChange(this.CreateGraphics());
            
            comboBox2.SelectedIndex = 0;

            NapuniGridViewSaDefinisanimFunkcijama();
            //Ako se ucitava model iz datoteke ovdje je potrebno 
            //selektovati one funkcije koje su selektovane u datoteci
            if (population != null)
            {
                //TO DO: Implement Time Series TrainingData serilization
                InitFile();
            }

           if (!TimeSeriesPrediction)
                tabControl1.TabPages.Remove(Series);
            
        }

        private void InitFile()
        {
            ignoreediting = true;
            PodesiFunkcijeIzDatoteke();
            functionSet = population.gpFunctionSet;
            terminalSet = population.gpTerminalSet;
            parameters = population.gpParameters;
            TimeSeriesPrediction=terminalSet.IsTimeSeries;

            for (int i = 0; i < dataGridViewBuiltInFunction.Rows.Count; i++)
            {
                string strName = dataGridViewBuiltInFunction.Rows[i].Cells[2].Value.ToString();
                for (int j = 0; j < functionSet.functions.Count; j++)
                {
                    if (functionSet.functions[j].Name == strName)
                    {
                        dataGridViewBuiltInFunction.Rows[i].Cells[0].Value = true;
                        break;
                    }
                    else
                    {
                        dataGridViewBuiltInFunction.Rows[i].Cells[0].Value = false;
                    }
                }
            }
            dataGridViewBuiltInFunction.EndEdit();
            GeneriranjeFunkcija();
            ignoreediting = false;
            //ucitavanje  parametara
            Debug.Assert(parameters != null);

            cmetodaGeneriranja.SelectedIndex = (int)parameters.einitializationMethod;
            cmetodaSelekcije.SelectedIndex = (int)parameters.eselectionMethod;
            ebSelParam1.Text = parameters.SelParam1.ToString();
            ebSelParam1.Text = parameters.SelParam1.ToString();
            epocetnaDubinaDrveta.Text = parameters.maxInitLevel.ToString();
            edubinaUkrstanja.Text = parameters.maxCossoverLevel.ToString();
            edubinaMutacije.Text = parameters.maxMutationLevel.ToString();
            textBox6.Text = parameters.elitism.ToString();
            evjerojatnostUkrstanja.Text = parameters.probCrossover.ToString();
            evjerojatnostMutacije.Text = parameters.probMutation.ToString();
            evjerojatnostReprodukcije.Text = parameters.probReproduction.ToString();
            evjerojatnostPermutacije.Text = parameters.probPermutation.ToString();

            evelicinaPopulacije.Text = population.population.Count.ToString();
            //Ucitavanje modela 

            //    terminalSet.NumConstants
            GPConstants = new double[terminalSet.NumConstants];
            for (int i = 0; i < terminalSet.NumConstants; i++)
            {
                GPConstants[i] = terminalSet.TrainingData[0][terminalSet.NumVariables + i];
            }
            PopunuGridSaKonstantama();

            //Testing TrainingData

            GPTrainingData = new double[terminalSet.NumVariables + 1][];
            for (int i = 0; i < terminalSet.NumVariables + 1; i++)
            {
                GPTrainingData[i] = new double[terminalSet.RowCount];

                for (int j = 0; j < terminalSet.RowCount; j++)
                {
                    if (i == terminalSet.NumVariables)
                        GPTrainingData[i][j] = terminalSet.TrainingData[j][terminalSet.TrainingData[0].Length - 1];
                    else
                        GPTrainingData[i][j] = terminalSet.TrainingData[j][i];
                }

            }
            //Na osnovu experimenta definisemo kakav ce modle biti
            gpModel = new double[GPTrainingData[0].Length, 2];
            // update list and chart
            UpdateDataGridView();
            UpdateChart(GPTrainingData);

            //testni podaci
            if (population.gpTerminalSet.TestingData != null)
            {
                GPTestingData = population.gpTerminalSet.TestingData;
                NapuniGridTestnimPodacima();
            }
            GPBestHromosome = population.bestChromosome;
            // set current iteration's info
            currentErrorBox.Text = GPBestHromosome.Fitness.ToString("F6");
            

            IzracunajModel();
            this.zedChart.Invalidate();
            IzracunajModel();
            PrikaziModel();

        }

        private void PodesiFunkcijeIzDatoteke()
        {
            for (int i = 0; i < functionSetsList.Count; i++)
            {
                var row = functionSetsList[i];
                int count=population.gpFunctionSet.functions.Count(x=>x.Name==row.Name);
                if(count==0)
                    row.Selected=false;
                else 
                {
                    row.Weight = count;
                }
            }
            dataGridViewBuiltInFunction.Refresh();
        }
        private void NapuniGridViewSaDefinisanimFunkcijama()
        {
            string strPath = "FunctionSet.xml";
            // When app is deployed with ClickOnce we have diferent path of file FunctionSet.xml
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    strPath = ApplicationDeployment.CurrentDeployment.DataDirectory + @"\FunctionSet.xml";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file. Error message: " + ex.Message);
                }
            }
            // Loading from a file, you can also load from a stream
            doc = XDocument.Load(strPath);
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            col.HeaderText = Resources.SR_Check;

            col.ReadOnly = false;
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
            if (functionSetsList != null)
            {
                if (functionSetsList.Count > 0)
                    functionSetsList.Clear();
            }
            try
            {
                functionSetsList = q.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, Properties.Resources.SR_ApplicationName);
            }

            dataGridViewBuiltInFunction.DataSource = functionSetsList;

            dataGridViewBuiltInFunction.Columns[0].ReadOnly = false;
            dataGridViewBuiltInFunction.Columns[1].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[2].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[3].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[4].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[5].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[6].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[7].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[8].ReadOnly = false;

            dataGridViewBuiltInFunction.Columns[0].HeaderText = Resources.SR_Selected;
            dataGridViewBuiltInFunction.Columns[1].HeaderText = Resources.SR_Aritry;
            dataGridViewBuiltInFunction.Columns[2].HeaderText = Resources.SR_Name;
            dataGridViewBuiltInFunction.Columns[3].HeaderText = Resources.SR_Description;
            dataGridViewBuiltInFunction.Columns[3].Visible = false;
            dataGridViewBuiltInFunction.Columns[4].HeaderText = Resources.SR_Definition;
            dataGridViewBuiltInFunction.Columns[5].HeaderText = Resources.SR_ExcelDefinition;
            dataGridViewBuiltInFunction.Columns[5].Visible = false;
            dataGridViewBuiltInFunction.Columns[6].HeaderText = Resources.SR_ReadOnly;
            dataGridViewBuiltInFunction.Columns[6].Visible = false;
            dataGridViewBuiltInFunction.Columns[7].HeaderText = Resources.SR_IsDistribution;
            dataGridViewBuiltInFunction.Columns[7].Visible = false;
            dataGridViewBuiltInFunction.Columns[8].HeaderText = Resources.SR_Weight;
            dataGridViewBuiltInFunction.Columns[9].HeaderText = "ID";
            dataGridViewBuiltInFunction.Columns[9].Visible = false;

            dataGridViewBuiltInFunction.AutoResizeColumns();
        }
        private bool PodesiParametreGP()
        {
            if (parameters == null)
                parameters = new GPParameters();

            parameters.einitializationMethod = (EInitializationMethod)cmetodaGeneriranja.SelectedIndex;
            //Selection and selection parameters
            parameters.eselectionMethod = (ESelectionMethod)cmetodaSelekcije.SelectedIndex;

            float sel = 0;
            if (!float.TryParse(ebSelParam1.Text, out sel))
            {
                MessageBox.Show(Resources.SR_SelecParamInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.SelParam1 = sel;

            int pocetnaDubina = 0;
            if (!int.TryParse(epocetnaDubinaDrveta.Text, out pocetnaDubina))
            {
                MessageBox.Show(Resources.SR_InitialDepthInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.maxInitLevel = pocetnaDubina;

            int ukrstanjeDubina = 0;
            if (!int.TryParse(edubinaUkrstanja.Text, out ukrstanjeDubina))
            {
                MessageBox.Show(Resources.SR_CrossOverdepthInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.maxCossoverLevel = ukrstanjeDubina;

            int mutacijaDubina = 0;
            if (!int.TryParse(edubinaMutacije.Text, out mutacijaDubina))
            {
                MessageBox.Show(Resources.SR_MutationDepthInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.maxMutationLevel = mutacijaDubina;
            int elitism = 0;
            if (!int.TryParse(textBox6.Text, out elitism))
            {
                MessageBox.Show(Resources.SR_ElitismInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            if (!int.TryParse(evelicinaPopulacije.Text, out velPopulacije))
            {
                MessageBox.Show(Resources.SR_PopulationSizeInvalid, Resources.SR_ApplicationName);
                return false;
            }
            if (elitism >= velPopulacije)
            {
                MessageBox.Show(Resources.SR_ElitismInvalid, Resources.SR_ApplicationName);
                return false;
            }
            parameters.elitism = elitism;

            float vjerUkrstanje = 0;
            if (!float.TryParse(evjerojatnostUkrstanja.Text, out vjerUkrstanje))
            {
                MessageBox.Show(Resources.SR_probCrossOverInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.probCrossover = vjerUkrstanje;

            float vjerMutacija = 0;
            if (!float.TryParse(evjerojatnostMutacije.Text, out vjerMutacija))
            {
                MessageBox.Show(Resources.SR_ProbMutationInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.probMutation = vjerMutacija;

            float vjerSelekcija = 0;
            if (!float.TryParse(evjerojatnostReprodukcije.Text, out vjerSelekcija))
            {
                MessageBox.Show(Resources.SR_ProbSelectionInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.probReproduction = vjerSelekcija;

            float vjerPermutacija = 0;
            if (!float.TryParse(evjerojatnostPermutacije.Text, out vjerPermutacija))
            {
                MessageBox.Show(Resources.SR_ProbPermutationInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            parameters.probPermutation = vjerPermutacija;


            return true;
        }
        private void SpasiFunkcijeuXML()
        {
            dataGridViewBuiltInFunction.EndEdit();
            // Kada se napusta dijalog potrebno je Updateati XML datoteku sa promjenama
            XElement functionSets = doc.Root;//Prvo se izvuce korijen a to je FunctionSets
            //ispod 

            for (int i = 0; i < functionSetsList.Count; i++)
            {
                functionSets.Elements("FunctionSet").ElementAt(i).Element("Selected").Value = functionSetsList[i].Selected.ToString();

                //Da li je funkcija read only ako nije onda nek se spreme promjene
                if (bool.Parse(functionSets.Elements("FunctionSet").ElementAt(i).Element("ReadOnly").Value) == false)
                {
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Aritry").Value = functionSetsList[i].Aritry.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Name").Value = functionSetsList[i].Name.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Description").Value = functionSetsList[i].Description.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Definition").Value = functionSetsList[i].Definition.ToString();
                    functionSets.Elements("FunctionSet").ElementAt(i).Element("Weight").Value = functionSetsList[i].Weight.ToString();
                }
            }

            //Nastale promjene zapisuju se u XML file
            string strPath = "FunctionSet.xml";
            // When app is deployed with ClickOnce we have diferent path of file FunctionSet.xml
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    strPath = ApplicationDeployment.CurrentDeployment.DataDirectory + @"\FunctionSet.xml";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.SR_Cannot_Read + ex.Message);
                }
            }
            doc.Save(strPath);
        }

        private void GeneriranjeFunkcija()
        {
            SpasiFunkcijeuXML();

            //Kada se dijalog zatvori onda izvucu sve funkcije koje su odabrane
            var q = from c in functionSetsList
                    where c.Selected == true
                    select c;
            //Clear old functions
            functionSet.functions.Clear();
            foreach (var op in q)
            {
                for (int i = 0; i < op.Weight; i++)
                    functionSet.functions.Add(op);
            }
            
            //load new 
           // functionSet.functions = q.ToList();

            //Shuffle the functions (apear problems during serilization, so it is commnted)
         /*   int count = functionSet.functions.Count;
            for (int i = 0; i < count; i++)
             {
               int randIndex = GPPopulation.rand.Next(count);
               int randIndex2 = GPPopulation.rand.Next(count);
               GPFunction temp = functionSet.functions[randIndex];
               functionSet.functions[randIndex] = functionSet.functions[randIndex2];
               functionSet.functions[randIndex2] = temp;
            }*/
        }
        void TestniSkupFunkcija()
        {
            functionSet = new GPFunctionSet();
            GeneriranjeFunkcija();
            return;
        }
        void TerminaliIzExperimenta()
        {
            int numVariable = terminalSet.NumVariables;
            int numConst = terminalSet.NumConstants;
            if (functionSet.terminals == null)
                functionSet.terminals = new List<GPTerminal>();
            else
                functionSet.terminals.Clear();

            //Definisanje terminala
            for (int i = 0; i < numVariable; i++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = false;
                ter.Name = "X"+(i+1).ToString();
                ter.Value = i;
                functionSet.terminals.Add(ter);

            }
            for (int j = 0; j < numConst; j++)
            {
                //Terminali
                GPTerminal ter = new GPTerminal();
                ter.IsConstant = true;
                ter.Name = "R" + (j+1).ToString();
                ter.Value = j+numVariable;
                functionSet.terminals.Add(ter);
            }
        }
        //Generiranje teminala iz experimantalnih podataka i slucajnih konstanti
        bool Generateterminals()
        {
            if (GPTrainingData == null)
            {
                MessageBox.Show(Resources.SR_TrainingDataFirst, Properties.Resources.SR_ApplicationName);
                return false;
            }
            if(terminalSet==null)
                terminalSet= new GPTerminalSet();

            
            terminalSet.IsTimeSeries = TimeSeriesPrediction;
            
            int intOD = 0;
            if (!int.TryParse(intervalOdTextBox.Text, out intOD))
            {
                MessageBox.Show(Resources.SR_ConstantIncorectNumber, Properties.Resources.SR_ApplicationName);
                return false;
            }
            int intDO = 0;
            if (!int.TryParse(intervalDoTextBox.Text, out intDO))
            {
                MessageBox.Show(Resources.SR_ConstantIncorectNumber, Properties.Resources.SR_ApplicationName);
                return false;
            }
            short numConst = 0;
            if (!short.TryParse(brojKonstantiTextBox.Text, out numConst))
            {
                MessageBox.Show(Resources.SR_ConstantIncorectNumber, Properties.Resources.SR_ApplicationName);
                return false;
            }
            if (GPConstants == null)
                GenerateConstants(intOD, intDO, numConst);

            //Kada znamo broj konstanti i podatke o experimenti sada mozemo popuniti podatke
            terminalSet.NumConstants = numConst;
            terminalSet.NumVariables=(short)(GPTrainingData.Length-1);
            terminalSet.RowCount = (short)GPTrainingData[0].Length;
            
            terminalSet.TrainingData = new double[terminalSet.RowCount][];
            int numOfVariables = terminalSet.NumVariables+terminalSet.NumConstants+1/*Output Value of experiment*/;
            for (int i = 0; i < terminalSet.RowCount; i++)
            {
                terminalSet.TrainingData[i] = new double[numOfVariables];
                for (int j = 0; j < numOfVariables; j++)
                {
                    if (j < terminalSet.NumVariables)//Nezavisne varijable
                        terminalSet.TrainingData[i][j] = GPTrainingData[j][i];
                    else if (j >= terminalSet.NumVariables && j < numOfVariables-1)//Konstante
                        terminalSet.TrainingData[i][j] = GPConstants[j - terminalSet.NumVariables];
                    else
                        terminalSet.TrainingData[i][j] = GPTrainingData[j - terminalSet.NumConstants][i];//Izlazna varijabla iz eperimenta
                }
            }
            //Ako smo ucitali podatke za testiranje Predikciju ovjde je ucitavam
            terminalSet.CalculateStat();
            // Sada na osnovu experimenta formiramo terminale
            TerminaliIzExperimenta();
            return true;
        }

        //Generiranje konstanti
        private void button3_Click(object sender, EventArgs e)
        {
            if (population != null)
            {
                DialogResult retVal = MessageBox.Show(Resources.SR_ConstantModified, Resources.SR_ApplicationName, MessageBoxButtons.YesNo);

                if (DialogResult.No == retVal)
                {
                    return;
                }
                else
                {
                    population = null;
                    currentErrorBox.Text = "0";

                }
            }
            int intOD = 0;
            if (!int.TryParse(intervalOdTextBox.Text, out intOD))
            {
                MessageBox.Show(Resources.SR_ConstantIncorectNumber, Resources.SR_ApplicationName);
                return ;
            }
            int intDO = 0;
            if (!int.TryParse(intervalDoTextBox.Text, out intDO))
            {
                MessageBox.Show(Resources.SR_ConstantIncorectNumber, Resources.SR_ApplicationName);
                return ;
            }
            int numConst = 0;
            if (!int.TryParse(brojKonstantiTextBox.Text, out numConst))
            {
                MessageBox.Show(Resources.SR_ConstantIncorectNumber, Resources.SR_ApplicationName);
                return ;
            }
            GenerateConstants(intOD, intDO, numConst);
            MessageBox.Show(Resources.SR_Compleated, Resources.SR_ApplicationName);
        }
        //Generiranje konstanti iz podrucja vrijednosti
        public void GenerateConstants(int from, int to, int number)
        {
            GPConstants = new double[number];

            for (int i = 0; i < number; i++)
            {
                decimal val =(decimal) (GPPopulation.rand.Next(from, to) + GPPopulation.rand.NextDouble());
                GPConstants[i] = (double)decimal.Round(val, 5);
            }

            PopunuGridSaKonstantama();
            
        }

        private void PopunuGridSaKonstantama()
        {
            dataGridViewRandomConstants.Columns.Clear();
            dataGridViewRandomConstants.Rows.Clear();
            dataGridViewRandomConstants.Columns.Add("colRB", Resources.SR_SampleNumber);
            dataGridViewRandomConstants.Columns.Add("colConst", Resources.SR_Constant);
            dataGridViewRandomConstants.Columns[0].Width = 30;
            dataGridViewRandomConstants.Columns[1].Width = 70;

            for (int i = 0; i < GPConstants.Length; i++)
            {
                int r = dataGridViewRandomConstants.Rows.Add();
                dataGridViewRandomConstants.Rows[r].Cells[0].Value = "R" + (i + 1).ToString();
                dataGridViewRandomConstants.Rows[r].Cells[1].Value = GPConstants[i];
            }
        }

        private void ZamrzniZavrijemeRada()
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            comboBox2.Enabled = false;
            brojIteracija.Enabled = false;
        }
        private void ZamrzniUSlobodnoVrijeme()
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;
            comboBox2.Enabled = true;
            brojIteracija.Enabled = true;
        }
        //Ucitavanje podataka iz datoteke
        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            // show file selection dialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                zedChart.GraphPane.CurveList.Clear();
                zedEksperiment.GraphPane.CurveList.Clear();
                if (terminalSet.TrainingData != null)
                    terminalSet.TrainingData = null;
                StreamReader reader = null;

                try
                {
                    // open selected file
                    reader = System.IO.File.OpenText(openFileDialog1.FileName);
                    //read TrainingData in to buffer
                    string buffer = reader.ReadToEnd();
                    //Remove delimating chars
                    char[] sep = { '\r', '\n' };
                    //define the row
                    string[] vrste = buffer.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //Define the columns
                    string[] kolone = vrste[0].Split(';');
                    //Define inner TrainingData
                    GPTrainingData = new double[kolone.Length][];
                    

                    for (int j = 0; j < kolone.Length; j++)
                    {
                        GPTrainingData[j] = new double[vrste.Length];

                        for (int k = 0; k < vrste.Length; k++)
                        {
                            kolone = vrste[k].Split(';');
                            GPTrainingData[j][k] = double.Parse(kolone[j]);
                        }
                    }

                    //Na osnovu experimenta definisemo kakav ce modle biti
                    gpModel = new double[GPTrainingData[0].Length, 2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.SR_ApplicationName);
                    return;
                }
                finally
                {
                    // close file
                    if (reader != null)
                        reader.Close();
                }

                // update list and chart
                UpdateDataGridView();
                UpdateChart(GPTrainingData);

                // enable "Start" button
                startButton.Enabled = true;
                TimeSeriesPrediction = false;
            }
        }
        private void UpdateChart(double[][] dd)
        {
            double[] x = new double[dd[0].Length];

            int m = dd.Length;
            for (int k = 0; k < dd[0].Length; k++)
                x[k] = k + 1;

            zedChart.GraphPane.AddCurve(Resources.SR_TrainingSet, x, dd[dd.Length - 1], Color.Red, ZedGraph.SymbolType.Triangle);
            modelItem = zedChart.GraphPane.AddCurve(Resources.SR_ModelSet, null, null, Color.Blue, ZedGraph.SymbolType.Triangle);
            zedChart.GraphPane.XAxis.Scale.Max = dd[0].Length+1;
            this.zedChart.GraphPane.AxisChange(this.CreateGraphics());

            zedEksperiment.GraphPane.AddCurve(Resources.SR_TrainingSet, x, dd[dd.Length - 1], Color.Red, ZedGraph.SymbolType.Triangle);
            zedEksperiment.GraphPane.XAxis.Scale.Max = dd[0].Length+1;
            this.zedEksperiment.GraphPane.AxisChange(this.CreateGraphics());
            this.zedEksperiment.Invalidate();
        }
        //Osvjezavanje gridKontrole sa podacima u varijabli GPTrainingData
        private void UpdateDataGridView()
        {
            dataGridViewPodaci.Columns.Clear();
            dataGridViewPodaci.Rows.Clear();

            dataGridViewPodaci.Columns.Add("colRB", Resources.SR_SampleNumber);
            string str;

            for (int i = 0; i < GPTrainingData.Length; i++)
            {
                if (GPTrainingData.Length == i + 1)
                    str = "Y";
                else
                    str = "X" + i.ToString();

                dataGridViewPodaci.Columns.Add(str, str);
            }


            for (int i = 0; i < GPTrainingData[0].Length; i++)
            {
                int r = dataGridViewPodaci.Rows.Add();
                dataGridViewPodaci.Rows[r].Cells[0].Value = i + 1;

                for (int j = 0; j < GPTrainingData.Length; j++)
                    dataGridViewPodaci.Rows[r].Cells[j + 1].Value = GPTrainingData[j][i];

            }
        }
        //Stopiranje algoritma
        private void stopButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }
        //Startanje Algoritma
        private void startButton_Click(object sender, EventArgs e)
        {
            //Freez GUI during run of GP 
            FreezGUI();

            ignoreediting = false;
            if (!PodesiParametreGP())
                return;

            TestniSkupFunkcija();

            if (!Generateterminals())
                return;
            //Velicina populacije
            if (!int.TryParse(evelicinaPopulacije.Text, out velPopulacije))
            {
                MessageBox.Show(Resources.SR_PopulationSizeInvalid, Resources.SR_ApplicationName);
                return;
            }
            
            //Paralelno procesuiranje
            if (radioSingleCore.Checked)
                bParalel = false;
            else
                bParalel = true;
            
            //Uslov za evoluciju
            if (!float.TryParse(brojIteracija.Text, out conditionValue))
            {
                MessageBox.Show(Resources.SR_ConditionInvalid);
                return;
            }

            //Podesavanje progresa
            progressBar1.Minimum = 0;
            int max = (int)conditionValue;
            if (max < 10)
                max = 500;
            progressBar1.Maximum = max;
            busloviEvolucije = comboBox2.SelectedIndex;
            //Podešavanje grafa
            maxFitness.Clear();
            avgFitness.Clear();

            //Podesavanje vreman startanja
            vrijemeZaJedanRun = DateTime.Now;
            pocetakEvolucije = vrijemeZaJedanRun;
            eTimeStart.Text = vrijemeZaJedanRun.ToString("dd/MM/yyyy : hh:mm:ss");


            backgroundWorker1.RunWorkerAsync(conditionValue);

            //POdesavanje dugmadi
            ZamrzniZavrijemeRada();
        }

        
        //DOWOrk
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IsGPRunning = true;

            //Argument koji smo proslijedili iz osnovne niti a tice se iteracija algoritma
            Nullable<float> brIteracija = e.Argument as Nullable<float>;

            //Creiranje pocetne populacije, ovdje je moguće nastaviti stopirani evoluciju ali u slucaju da
            // se neki kljucni parametri nisu promjenili.
            if (population == null)
            {
                //Nakon pocetne populacije ide startanje evolucije i 
                tekucaEvolucija = 1;
                population = new GPPopulation(velPopulacije, terminalSet, functionSet, parameters, null/*kada je NULL onda uzima default*/, bParalel);
            }
            else
            {
                //Akoje populacija vec formirana odnosnoakosmoucitalifile i 
                //pokrećemo evuluciju tamogdje smostali zadnji put.U ovom slucaju nova populacija 
                // se ne formira nego se samo neki parametri mogu promijeniti njih podesavamo  
                population.gpParameters = parameters;
                population.ParalelComputing = bParalel;

                //Ako smo populaciju promjenili u parametrima
                //prilikom ponovnog pokretanja trebamo to uskladiti
                if (population.popSize > velPopulacije)
                    population.population.RemoveRange(velPopulacije, population.popSize - velPopulacije);
                else if (population.popSize < velPopulacije)
                    population.InitPopulation(velPopulacije - population.popSize);

                //Kada smo izvrsili korekturu broja populacije
                population.popSize= population.population.Count();
            }
            if (GPTestingData != null)
                population.gpTerminalSet.TestingData = GPTestingData;

            GPBestHromosome= population.bestChromosome;

            //Raportiraj o progresu
            backgroundWorker1.ReportProgress(tekucaEvolucija, population.bestChromosome);
            
            while (ProvjeriUslovEvolucije(tekucaEvolucija))
            {
                
                    population.StartEvolution();

                    //Dodavanje tacaka za dijagram
                    maxFitness.AddPoint(tekucaEvolucija, population.fitnessMax);
                    avgFitness.AddPoint(tekucaEvolucija, population.fitnessAvg);

                    //Raportiraj o progresu
                    backgroundWorker1.ReportProgress(tekucaEvolucija, population.bestChromosome);
             
                    //Ako je pritisnuto dugme STOP
                    if (backgroundWorker1.CancellationPending)
                        break;

                    //Povecaj tekucu evoluciju za 1
                    tekucaEvolucija++;
               
            }


        }
        /// <summary>
        /// Generiranje uslova za evoluciju
        /// 1. Kada evolucija dostigne odredjeni broj
        /// 2. Kada hromosom popiri fitness veći ili jedna odrejdenom
        /// 3. kada R square poprimi vrijednost veću od odredjene
        /// </summary>
        /// <param name="tekucaEvolucija"></param>
        /// <returns></returns>
       private bool ProvjeriUslovEvolucije(int tekucaEvolucija)
        {
            if (busloviEvolucije == 0)
            {
                return tekucaEvolucija <= (int)conditionValue;
            }
            else if (busloviEvolucije == 1)
            {
                return GPBestHromosome.Fitness <= conditionValue;
            }
            else
                return tekucaEvolucija <= (int)conditionValue;
            
        }
       private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                brojIteracija.Text="500";
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                float flt = (float)800.0;
                brojIteracija.Text = flt.ToString("F6"); 
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                float flt = (float)0.95;
                brojIteracija.Text = flt.ToString("F6"); 
            }
            else
            {
                brojIteracija.Text = "500";
            }
        }

        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                DateTime vrijeme = DateTime.Now;
                TimeSpan razlika = vrijeme - vrijemeZaJedanRun;
                DateTime tt = vrijeme.AddSeconds(razlika.TotalSeconds * (conditionValue - e.ProgressPercentage));
                eTimePerRun.Text = razlika.TotalSeconds.ToString();
                if (busloviEvolucije == 0)
                {
                    eTimeleft.Text = ((float)(tt - DateTime.Now).TotalMinutes).ToString("F2");
                    eTimeToCompleate.Text = tt.ToString("dd/MM/yyyy : hh:mm:ss");
                    
                }
                else
                {
                    eTimeleft.Text = Resources.SR_Undefined;
                    eTimeToCompleate.Text = Resources.SR_Undefined;
                }
                
                GPChromosome chrom = (GPChromosome)e.UserState;

                // set current iteration's info
                currentIterationBox.Text = e.ProgressPercentage.ToString();
                if (currentErrorBox.Text != chrom.Fitness.ToString("F6"))
                {
                    zedGraphPopulation.GraphPane.YAxis.Scale.Max = chrom.Fitness + 0.5 * chrom.Fitness;
                    if (zedGraphPopulation.GraphPane.XAxis.Scale.Max < e.ProgressPercentage)
                        zedGraphPopulation.GraphPane.XAxis.Scale.Max = e.ProgressPercentage + 5;
                    this.zedGraphPopulation.GraphPane.AxisChange(this.CreateGraphics());
                    
                    currentErrorBox.Text = chrom.Fitness.ToString("F6");
                    bestFitnessAtGenerationEditBox.Text = e.ProgressPercentage.ToString();
                    GPBestHromosome = chrom;
                    IzracunajModel();
                    this.zedChart.Invalidate();
                }
                
                zedGraphPopulation.Invalidate();

                //Kada uslov za evaluaciju nije brojiteracija onda se mora povecavati 
                // maximalna vrijednost progres bara
                if(progressBar1.Maximum >= e.ProgressPercentage)
                    progressBar1.Value = e.ProgressPercentage;

                vrijemeZaJedanRun = vrijeme;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                IsGPRunning = false;
                eDuration.Text = ((float)(vrijemeZaJedanRun - pocetakEvolucije).TotalMinutes).ToString("F1"); 
                progressBar1.Value = progressBar1.Maximum;
                ZamrzniUSlobodnoVrijeme();
                PrikaziModel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UnFreezGUI();
        }
        private void FreezGUI()
        {
            dataGridViewTimeSeries.Enabled = false;
            button4.Enabled = false;
            groupBox5.Enabled = false;
            dataGridViewPodaci.Enabled = false;
            btnUcitaj.Enabled = false;
            button2.Enabled = false;

            evelicinaPopulacije.Enabled = false;
            cmetodaGeneriranja.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            groupBox6.Enabled = false;
            dataGridViewBuiltInFunction.Enabled = false;
            dataGridView2.Enabled = false;
        }
        private void UnFreezGUI()
        {
            dataGridViewTimeSeries.Enabled = true;
            button4.Enabled = true;
            groupBox5.Enabled = true;
            dataGridViewPodaci.Enabled = true;
            btnUcitaj.Enabled = true;
            button2.Enabled = true;

            evelicinaPopulacije.Enabled = true;
            cmetodaGeneriranja.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;
            groupBox6.Enabled = true;
            dataGridViewBuiltInFunction.Enabled = true;
            dataGridView2.Enabled = true;
        }
        private void IzracunajModel()
        {
             //Translate chromosome to list expressions
            int indexOutput = terminalSet.NumConstants + terminalSet.NumVariables-1;
            List<int> lst = new List<int>();
            FunctionTree.ToListExpression(lst, GPBestHromosome.Root);
            double y=0;
            modelItem.Clear();
            for (int i = 0; i < terminalSet.RowCount; i++)
            {
                // evalue the function
                y = functionSet.Evaluate(lst, terminalSet, i);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y=0;

                gpModel[i, 0] = i+1;
                gpModel[i, 1] = y;
                //Napravi graf od novog rjesenja
                modelItem.AddPoint(gpModel[i, 0], gpModel[i, 1]);
            }
            if (!TimeSeriesPrediction)
                IzracunajPredikcijskiModel(lst);
            else
                IzracunajPredikcijskiModelSerie(lst);

        }

        private void IzracunajPredikcijskiModel(List<int> lst)
        {
            double y;
            //Ako testni podaci nisu ucitani onda nista
            if (GPTestingData == null)
                return;
            //Izracunavanje Testnih podataka na osnovu modela
            GPTerminalSet testingSet = new GPTerminalSet();
            testingSet.NumConstants = terminalSet.NumConstants;
            testingSet.NumVariables = (short)(GPTestingData.Length - 1);
            testingSet.RowCount = (short)GPTestingData[0].Length;
            int numOfVariables = testingSet.NumVariables + testingSet.NumConstants + 1/*Output Value of experiment*/;
            testingSet.TrainingData = new double[testingSet.RowCount][];
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                testingSet.TrainingData[i] = new double[numOfVariables];
                for (int j = 0; j < numOfVariables; j++)
                {
                    if (j < testingSet.NumVariables)//Nezavisne varijable
                        testingSet.TrainingData[i][j] = GPTestingData[j][i];
                    else if (j >= terminalSet.NumVariables && j < numOfVariables - 1)//Konstante
                        testingSet.TrainingData[i][j] = terminalSet.TrainingData[0][j];
                    else
                        testingSet.TrainingData[i][j] = GPTestingData[j - terminalSet.NumConstants][i];//Izlazna varijabla iz eperimenta
                }
            }
            testingSet.CalculateStat();

            y = 0;
            modelTestIten.Clear();
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                // evalue the function
                y = functionSet.Evaluate(lst, testingSet, i);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;

                gpTestModel[i, 0] = i + 1;
                gpTestModel[i, 1] = y;
                //Napravi graf od novog rjesenja
                modelTestIten.AddPoint(gpTestModel[i, 0], gpTestModel[i, 1]);
            }
            zedGraphTestData.Invalidate();
        }
        private void IzracunajPredikcijskiModelSerie(List<int> lst)
        {
            double y;
            //Ako testni podaci nisu ucitani onda nista
            if (GPTestingData == null)
                return;
            //Izracunavanje Testnih podataka na osnovu modela
            GPTerminalSet testingSet = new GPTerminalSet();
            testingSet.NumConstants = terminalSet.NumConstants;
            testingSet.NumVariables = GPTestingData.Length - 1;
            testingSet.RowCount = GPTestingData[0].Length;
            int numOfTerminals = testingSet.NumVariables + testingSet.NumConstants + 1/*Output Value of experiment*/;
            testingSet.TrainingData = new double[testingSet.RowCount][];
            
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                testingSet.TrainingData[i] = new double[numOfTerminals];
                for (int j = 0; j < numOfTerminals; j++)
                {
                    if (j < testingSet.NumVariables)//Nezavisne varijable
                        testingSet.TrainingData[i][j] = GPTestingData[j][i];
                    else if (j >= terminalSet.NumVariables && j < numOfTerminals - 1)//Konstante
                        testingSet.TrainingData[i][j] = terminalSet.TrainingData[0][j];
                    else
                        testingSet.TrainingData[i][j] = GPTestingData[j - terminalSet.NumConstants][i];//Izlazna varijabla iz eperimenta
                }
            }
            testingSet.CalculateStat();

            y = 0;
            modelTestIten.Clear();
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                // evalue the function
                y = functionSet.Evaluate(lst, testingSet, i);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;
                int k = testingSet.NumVariables;
                int j=0;
                //Kod time series potrebno je podesiti ulazne varijable u odnosu na neto izracunate vrijednosti
                for (j = i; j < testingSet.RowCount && k>=0; j++)
                {
                    
                    if(k==testingSet.NumVariables)
                        testingSet.TrainingData[j][testingSet.NumVariables +testingSet.NumConstants] = y;
                    else
                        testingSet.TrainingData[j][k] = y;
                    k--;
                }
                gpTestModel[i, 0] = i + 1;
                gpTestModel[i, 1] = y;
                //Napravi graf od novog rjesenja
                modelTestIten.AddPoint(gpTestModel[i, 0], gpTestModel[i, 1]);
            }
            zedGraphTestData.Invalidate();
        }
        private void PrikaziModel()
        {
            try
            {
                dataGridViewRezultat.Columns.Clear();
                dataGridViewRezultat.Rows.Clear();
                dataGridViewRezultat.Columns.Add("colRB", "RB");
                string str;
                for (int i = 1; i < GPTrainingData.Length; i++)
                {
                    str = "X" + i.ToString();
                    dataGridViewRezultat.Columns.Add(str, str);
                }
                dataGridViewRezultat.Columns.Add("colY", "Y");
                dataGridViewRezultat.Columns.Add("colYa", "Ya");
                dataGridViewRezultat.Columns.Add("colD", "R");

                for (int i = 0; i < GPTrainingData[0].Length; i++)
                {
                    int r = dataGridViewRezultat.Rows.Add();
                    dataGridViewRezultat.Rows[r].Cells[0].Value = i + 1;
                    for (int j = 0; j < GPTrainingData.Length; j++)
                    {
                        dataGridViewRezultat.Rows[r].Cells[j+1].Value = GPTrainingData[j][i];

                    }
                    dataGridViewRezultat.Rows[r].Cells[GPTrainingData.Length + 1].Value = gpModel[i,1];

                    dataGridViewRezultat.Rows[r].Cells[GPTrainingData.Length + 2].Value = GPTrainingData[GPTrainingData.Length - 1][i] - gpModel[i,1];
                }

                PrikaziIOptimizirajGPModel();
                PrikaziModelTesta();
            }
            catch
            {
                MessageBox.Show(Resources.SR_ModelCreationError, Resources.SR_ApplicationName);
            }
        }
        private void PrikaziModelTesta()
        {
            //Ako testni podaci nisu ucitani onda nista
            if (GPTestingData == null)
                return;
            try
            {
                //Dodavanje dodatnih kolona na testne podatke
                if (dataGridViewTestData.Columns.Count != terminalSet.NumVariables + 4)
                {
                    dataGridViewTestData.Columns.Add("colYa", "Ya");
                    dataGridViewTestData.Columns.Add("colD", "R");
                }

                for (int i = 0; i < GPTestingData[0].Length; i++)
                {

                    dataGridViewTestData.Rows[i].Cells[GPTestingData.Length + 1].Value = gpTestModel[i, 1];

                    dataGridViewTestData.Rows[i].Cells[GPTestingData.Length + 2].Value = GPTestingData[GPTestingData.Length - 1][i] - gpTestModel[i, 1];
                }

                dataGridViewTestData.Refresh();

            }
            catch
            {
                MessageBox.Show("Greška u formiranju testnog modela podataka");
            }
        }

        private void PrikaziIOptimizirajGPModel()
        {
            List<int> lst = new List<int>();
            FunctionTree.ToListExpression(lst, GPBestHromosome.Root);
            enooptMatematickiModel.Text = functionSet.DecodeExpression(lst, terminalSet);
        }
        //Visual presentation of the tree
        private void button1_Click(object sender, EventArgs e)
        {
            if (GPBestHromosome == null)
                return;
            TreeExpression dlg = new TreeExpression();
            dlg.TreeDrawer.DrawTreeExpression(GPBestHromosome.Root, functionSet);
            dlg.ShowDialog();
            dlg.Dispose();
        }
        //Load testing TrainingData
        private void button2_Click(object sender, EventArgs e)
        {
            // show file selection dialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = null;

                try
                {
                    // open selected file
                    reader = System.IO.File.OpenText(openFileDialog1.FileName);
                    //read TrainingData in to buffer
                    string buffer = reader.ReadToEnd();
                    //Remove delimating chars
                    char[] sep = { '\r', '\n' };
                    //define the row
                    string[] vrste = buffer.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //Define the columns
                    string[] kolone = vrste[0].Split(';');
                    //Define inner TrainingData
                    GPTestingData = new double[kolone.Length][];


                    for (int j = 0; j < kolone.Length; j++)
                    {
                        GPTestingData[j] = new double[vrste.Length];

                        for (int k = 0; k < vrste.Length; k++)
                        {
                            kolone = vrste[k].Split(';');
                            GPTestingData[j][k] = double.Parse(kolone[j]);
                        }
                    }

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Properties.Resources.SR_ApplicationName);
                    return;
                }
                finally
                {
                    // close file
                    if (reader != null)
                        reader.Close();
                }

                NapuniGridTestnimPodacima();
                

            }
        }

        private void NapuniGridTestnimPodacima()
        {
            //Na osnovu experimenta definisemo kakav ce modle biti
            gpTestModel = new double[GPTestingData[0].Length, 2];

            dataGridViewTestData.Columns.Clear();
            dataGridViewTestData.Rows.Clear();

            dataGridViewTestData.Columns.Add("colRB", Resources.SR_SampleNumber);
            string str;

            for (int i = 0; i < GPTestingData.Length; i++)
            {
                if (GPTestingData.Length == i + 1)
                    str = "Y";
                else
                    str = "X" + i.ToString();

                dataGridViewTestData.Columns.Add(str, str);
            }


            for (int i = 0; i < GPTestingData[0].Length; i++)
            {
                int r = dataGridViewTestData.Rows.Add();
                dataGridViewTestData.Rows[r].Cells[0].Value = i + 1;

                for (int j = 0; j < GPTestingData.Length; j++)
                    dataGridViewTestData.Rows[r].Cells[j + 1].Value = GPTestingData[j][i];

            }


            double[] x = new double[GPTestingData[0].Length];

            int m = GPTestingData.Length;
            for (int k = 0; k < GPTestingData[0].Length; k++)
                x[k] = k + 1;

            zedGraphTestData.GraphPane.AddCurve(Resources.SR_TestingData, x, GPTestingData[GPTestingData.Length - 1], Color.Red, ZedGraph.SymbolType.Triangle);
            modelTestIten = zedGraphTestData.GraphPane.AddCurve(Resources.SR_ModelSet, null, null, Color.Blue, ZedGraph.SymbolType.Diamond);
            zedGraphTestData.GraphPane.XAxis.Scale.Max = GPTestingData[0].Length + 1;
            this.zedGraphTestData.GraphPane.AxisChange(this.CreateGraphics());
            this.zedGraphTestData.Invalidate();
        }
        public bool SaveToFile()
        {
            if (population == null)
            {
               
                return false;
            }
            population.Save(filePath);
            return true;
        }
        public bool OpenFromFile(string strFile)
        {
            filePath = strFile;
            population= GPPopulation.Load(filePath);
            return true;
        }

       //Provjera da li u vec napravljenoj populacijo pokusavamo da promijenimo skup funkcija. u potvrdnom slucaju
        // populacija se mora resetvati i rjesenja koja smo dobili ponistiti
        
        private void dataGridViewBuiltInFunction_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ignoreediting)
                return;
            //ako se populacija jos nije izgradila ona nista
            if (population == null)
                return;

            DialogResult retVal = MessageBox.Show(Resources.SR_ChangeFunctionSet, Resources.SR_ApplicationName, MessageBoxButtons.YesNo);

            if (DialogResult.No == retVal)
            {
                functionSetsList[e.RowIndex].Selected = !functionSetsList[e.RowIndex].Selected;
                dataGridViewBuiltInFunction.RefreshEdit();
            }
            else
            {
                population = null;
                currentErrorBox.Text = "0";

            }

           
        }
        /// <summary>
        /// Ucitavanje podataka iz for time serie prediction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            // show file selection dialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                zedChart.GraphPane.CurveList.Clear();
                zedEksperiment.GraphPane.CurveList.Clear();
                if (terminalSet.TrainingData != null)
                    terminalSet.TrainingData = null;
                StreamReader reader = null;

                try
                {
                    // open selected file
                    reader = System.IO.File.OpenText(openFileDialog1.FileName);
                    //read TrainingData in to buffer
                    string buffer = reader.ReadToEnd();
                    //Remove delimating chars
                    char[] sep = { '\r', '\n' };
                    //define the row
                    string[] vrste = buffer.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //Define the columns
                    string[] kolone = vrste[0].Split(';');
                    //Define inner TrainingData
                    timeSerie = new double[vrste.Length];


                   // for (int j = 0; j < kolone.Length; j++)
                    {
                        for (int k = 0; k < vrste.Length; k++)
                        {
                            kolone = vrste[k].Split(';');
                            timeSerie[k] = double.Parse(kolone[0]);
                        }
                    }

                    //Na osnovu experimenta definisemo kakav ce modle biti
                    //gpModel = new double[GPTrainingData[0].Length, 2];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Properties.Resources.SR_ApplicationName);
                    return;
                }
                finally
                {
                    // close file
                    if (reader != null)
                        reader.Close();
                }

                dataGridViewTimeSeries.Columns.Clear();
                dataGridViewTimeSeries.Rows.Clear();

                dataGridViewTimeSeries.Columns.Add("colRB", Resources.SR_SampleNumber);
                dataGridViewTimeSeries.Columns.Add("colSerie", Resources.SR_Value);


                for (int i = 0; i < timeSerie.Length; i++)
                {
                    int r = dataGridViewTimeSeries.Rows.Add();
                    dataGridViewTimeSeries.Rows[r].Cells[0].Value = i + 1;
                    dataGridViewTimeSeries.Rows[r].Cells[1].Value = timeSerie[i];

                }
                textBox1.Text = timeSerie.Length.ToString();
                textBox4.Text = "10";
                textBox5.Text = "10";

            }
        }
        //Converti time serie to GP Algoritam
        private void button5_Click(object sender, EventArgs e)
        {
            if (timeSerie == null)
            {
                MessageBox.Show(Resources.SR_LoadSeriesFirst, Properties.Resources.SR_ApplicationName);
                return;
            }
            int numVariable, numberTestData;
            //Konfiguracije varijabli
            if (!int.TryParse(textBox4.Text, out numVariable))
            {
                MessageBox.Show(Resources.SR_SeriesCorrectNumbVari, Properties.Resources.SR_ApplicationName);
                return;
            }
            //Konfiguracije varijabli
            if (!int.TryParse(textBox5.Text, out numberTestData))
            {
                MessageBox.Show(Resources.Sr_SeriesCorrectNumTestSamples, Properties.Resources.SR_ApplicationName);
                return;
            }
            //Provjera konzistentnosti serije
            if (timeSerie.Length - 2 * numVariable - numberTestData < 0)
            {
                MessageBox.Show(Resources.SR_SeriesCorectNumVartest, Properties.Resources.SR_ApplicationName);
                return;
            }
            int numberTreningData=timeSerie.Length-numVariable-numberTestData;

            //Training TrainingData se sastoji od varijabli i izlazne funkcije
            GPTrainingData = new double[numVariable+1][];
            for (int i = 0; i < numVariable+1; i++)
            {
                GPTrainingData[i] = new double[numberTreningData];
                for (int j = i; j < numberTreningData+i; j++)
                    GPTrainingData[i][j-i]=timeSerie[j];

            }

           //Na osnovu experimenta definisemo kakav ce modle biti
            gpModel = new double[GPTrainingData[0].Length, 2];
            // update list and chart
            UpdateDataGridView();
            UpdateChart(GPTrainingData);


            //POdaci za testiranje

            GPTestingData = new double[numVariable+1][];
            for (int i = 0; i < numVariable + 1; i++)
            {
                GPTestingData[i] = new double[numberTestData];
                int k = 0;
                for (int j = numberTreningData + i; j < numberTreningData + numberTestData+i; j++)
                {
                    GPTestingData[i][k] = timeSerie[j];
                    k++;
                }

            }
            NapuniGridTestnimPodacima();
            TimeSeriesPrediction = true;

        }
        //Selection changed
        private void cmetodaSelekcije_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmetodaSelekcije.SelectedIndex)
            {
                //Fitness proportionate selection
                case 0:
                    lbSelParam1.Visible = false;
                    ebSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    ebSelParam2.Visible = false;
                    break;
                //Rank Selection
                case 1:
                    lbSelParam1.Visible = false;
                    ebSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    ebSelParam2.Visible = false;
                    break;
                //Tournment Selection
                case 2:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Tour Size:";
                    ebSelParam1.Text = 2.ToString();
                    ebSelParam1.Visible = true;
                    lbSelParam2.Visible = false;
                    ebSelParam2.Visible = false;
                    break;
                //Stochastic unversal selection
                case 3:
                    lbSelParam1.Visible = false;
                    ebSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    ebSelParam2.Visible = false;
                    break;
                //FUSS selection
                case 4:
                    lbSelParam1.Visible = false;
                    ebSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    ebSelParam2.Visible = false;
                    break;
                //Skrgic Selection
                case 5:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Nonlinear Keof:";
                    ebSelParam1.Text = (1.0 / 5.0).ToString();
                    ebSelParam1.Visible = true;
                    lbSelParam2.Visible = false;
                    ebSelParam2.Visible = false;
                    break;
                //Default Selection
                default:
                    break;
            }
        }

        
        
    }
}
