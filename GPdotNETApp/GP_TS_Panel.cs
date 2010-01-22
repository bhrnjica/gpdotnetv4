using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPdotNETLib;
using System.IO;
using ZedGraph;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using GPdotNETApp.Properties;
namespace GPdotNETApp
{
    public partial class GP_TS_Panel : Form
    {
        public string filePath;

        GPPopulation population;
        public GPFunctionSet functionSet{get;private set;}
        public GPTerminalSet terminalSet{get;private set;}
        public bool IsGPRunning{ get; private set; }
        GPParameters param;
        GPParameters GPParameters
        {
            get
            {
                if (param == null)
                   param= new GPParameters();
                return param;
            }
            set
            {
                param = value;
            }
        }

        //Varijabla za izracunavanje vremena jednog una
        DateTime vrijemeZaJedanRun;
        DateTime pocetakEvolucije;
        //Lista funkcija koje se ucitavaju iz xml datoteke
        XDocument doc;
        public List<GPFunction> functionSetsList;

        //Model sa podacima za Graf
        LineItem modelItem;
        LineItem modelTestIten;
        LineItem maxFitness, avgFitness;//, rSquare;
        
        //Podaci experimenta za treniranje GP modela
        public double[][] GPTrainingData{get;private set;}
        double[,] gpModel;

        //Podaci experimenta za testiranje GP modela
        public double[][] GPTestingData {get;private set;}
        double[,] gpTestModel;

        //POdaci o time series prediction

        public bool TimeSeriesPrediction {get;set;}
        double[] timeSerie;

        public double[] GPConstants {get;private set;}

        //karakteristke populacije
        public GPChromosome GPBestHromosome {get;private set;}

        int velPopulacije;
        bool bParalel;
        int busloviEvolucije;
        float conditionValue;

        bool ignoreediting = true;

        public GP_TS_Panel()
        {
            InitializeComponent();
            TimeSeriesPrediction=false;
            terminalSet = new GPTerminalSet();
            functionSet = new GPFunctionSet();
            cmetodaGeneriranja.SelectedIndex = 2;
            cmetodaSelekcije.SelectedIndex = 6;
            cfitnessFunction.SelectedIndex = 1;
            IsGPRunning = false;
                      
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Podesavanje grafika            
            this.zedEksperiment.GraphPane.Title.Text = Resources.SR_ExperimentModel;
            this.zedEksperiment.GraphPane.XAxis.Title.Text = Resources.SR_ControlPoint;
            this.zedEksperiment.GraphPane.YAxis.Title.Text = Resources.SR_FunctionValue;

            this.zedChart.GraphPane.Title.Text = Resources.SR_SolutionGraph;
            this.zedChart.GraphPane.XAxis.Title.Text = Resources.SR_NumPoints;
            this.zedChart.GraphPane.YAxis.Title.Text = Resources.SR_Output;

            this.zedGraphPopulation.GraphPane.Title.Text = Resources.SR_Populaction;
            this.zedGraphPopulation.GraphPane.XAxis.Title.Text = Resources.SR_Generation;
            this.zedGraphPopulation.GraphPane.YAxis.Title.Text = Resources.SR_FittValue;

            maxFitness = zedGraphPopulation.GraphPane.AddCurve(Resources.SR_MaxFit, null, null, Color.Red, ZedGraph.SymbolType.None);
            avgFitness = zedGraphPopulation.GraphPane.AddCurve(Resources.SR_AvgFitness, null, null, Color.Blue, ZedGraph.SymbolType.None);
           // rSquare = zedGraphPopulation.GraphPane.AddCurve(Resources.SR_RSquare, null, null, Color.Gray, ZedGraph.SymbolType.None);

            //Podešavanje grafa
            zedGraphPopulation.GraphPane.XAxis.Scale.Max = 500;
            zedGraphPopulation.GraphPane.XAxis.Scale.Min = 0;
            zedGraphPopulation.GraphPane.YAxis.Scale.Min = 0;
            zedGraphPopulation.GraphPane.YAxis.Scale.Max = 1000;
            this.zedGraphPopulation.GraphPane.AxisChange(this.CreateGraphics());

            comboBox2.SelectedIndex = 0;

            NapuniGridViewSaDefinisanimFunkcijama();
            //Ako se ucitava model iz datoteke ovdje je potrebno 
            //selektovati one funkcije koje su selektovane u datoteci
            if (population != null)
            {
                //TO DO: Implement Time Series TrainingData serilization
                InitGPFormFile();
            }
            else
            {
                if (!TimeSeriesPrediction)
                    tabControl1.TabPages.Remove(tabPageSeries);
            }
        }

        private void InitGPFormFile()
        {
            ignoreediting = true;
            GPBestHromosome = population.BestChromosome;
            functionSet = GPPopulation.GPFunctionSet;
            terminalSet = GPPopulation.GPTerminalSet;
            GPParameters = GPPopulation.GPParameters;

            if(!GPPopulation.GPTerminalSet.IsTimeSeries)
                tabControl1.TabPages.Remove(tabPageSeries);

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
            Debug.Assert(GPParameters != null);

            cmetodaGeneriranja.SelectedIndex = (int)GPParameters.einitializationMethod;
            cmetodaSelekcije.SelectedIndex = (int)GPParameters.eselectionMethod;
            cfitnessFunction.SelectedIndex = (int)GPParameters.efitnessFunction;

            epocetnaDubinaDrveta.Text = GPParameters.maxInitLevel.ToString();
            edubinaUkrstanja.Text = GPParameters.maxCossoverLevel.ToString();
            edubinaMutacije.Text = GPParameters.maxMutationLevel.ToString();

            evjerojatnostUkrstanja.Text = GPParameters.probCrossover.ToString();
            evjerojatnostMutacije.Text = GPParameters.probMutation.ToString();
            evjerojatnostReprodukcije.Text = GPParameters.probReproduction.ToString();
            evjerojatnostPermutacije.Text = GPParameters.probPermutation.ToString();

            evelicinaPopulacije.Text = population.PopulationSize.ToString();
            
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
            if (GPPopulation.GPTerminalSet.TestingData != null)
            {
                GPTestingData = GPPopulation.GPTerminalSet.TestingData;
                NapuniGridTestnimPodacima();
            }
           
            // set current iteration's info
            currentErrorBox.Text = GPBestHromosome.Fitness.ToString("F6");
            if (GPBestHromosome.RSquare >= 0)
                rsquareEditbix.Text = GPBestHromosome.RSquare.ToString("F6");
            else
                rsquareEditbix.Text = 0.ToString("F6");

            //Forimarnje simulacije modela 
            maxFitness.Clear();
            avgFitness.Clear();
            //rSquare.Clear();
            //Podešavanje grafa
            for (int i = 0; i < population.EvolutionHistrory.Count; i++)
            {
                Debug.Assert(population.EvolutionHistrory[i]!=null);
                Debug.Assert(population.EvolutionHistrory[i].BestHromosome != null);
               
                AddPointsToSimulateFitness(i, population.EvolutionHistrory[i].BestHromosome.Fitness, population.EvolutionHistrory[i].AvgFitness/*, population.EvolutionHistrory[i].BestHromosome.RSquare*/);

            }
            currentIterationBox.Text = (population.EvolutionHistrory.Count - 1).ToString();
            bestFitnessAtGenerationEditBox.Text = population.BestCHromosomeApear.ToString();
            eTimeStart.Text = population.RunStarted.ToString();
            eDuration.Text = population.DurationOfRun.TotalMinutes.ToString("F1");
            eTimePerRun.Text = population.EvolutionHistrory[population.EvolutionHistrory.Count-1].Duration.TotalSeconds.ToString("F3");
                

           
            IzracunajModel();
            this.zedChart.Invalidate();
            IzracunajModel();
            PrikaziModel();

        }
        private void NapuniGridViewSaDefinisanimFunkcijama()
        {
            // Loading from a file, you can also load from a stream
            doc = XDocument.Load(@"FunctionSet.xml");
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            col.HeaderText = Resources.SR_Check;

            col.ReadOnly = false;
            // 
            var q = from c in doc.Descendants("FunctionSet")
                    select new GPFunction
                    {
                        Selected = bool.Parse(c.Element("Selected").Value),
                        Weight=int.Parse(c.Element("Weight").Value),
                        Name = c.Element("Name").Value,
                        Definition = c.Element("Definition").Value,
                        ExcelDefinition = c.Element("ExcelDefinition").Value,
                        Aritry = ushort.Parse(c.Element("Aritry").Value),
                        Description = c.Element("Description").Value,
                        IsReadOnly = bool.Parse(c.Element("ReadOnly").Value),
                        IsDistribution = bool.Parse(c.Element("IsDistribution").Value)

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
            dataGridViewBuiltInFunction.Columns[4].HeaderText = Resources.SR_Definition; 
            dataGridViewBuiltInFunction.Columns[5].HeaderText = Resources.SR_ExcelDefinition;
            dataGridViewBuiltInFunction.Columns[6].HeaderText = Resources.SR_ReadOnly;
            dataGridViewBuiltInFunction.Columns[7].HeaderText = Resources.SR_IsDistribution;
            dataGridViewBuiltInFunction.Columns[8].HeaderText = Resources.SR_Weight;

            dataGridViewBuiltInFunction.AutoResizeColumns();
        }
        private bool PodesiParametreGP()
        {
            GPParameters.einitializationMethod = (EInitializationMethod)cmetodaGeneriranja.SelectedIndex;
            GPParameters.eselectionMethod = (ESelectionMethod)cmetodaSelekcije.SelectedIndex;
            GPParameters.efitnessFunction = (EFitnessFunction)cfitnessFunction.SelectedIndex;
            int pocetnaDubina = 0;
            if (!int.TryParse(epocetnaDubinaDrveta.Text, out pocetnaDubina))
            {
                MessageBox.Show(Resources.SR_InitialDepthInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.maxInitLevel = pocetnaDubina;

            int ukrstanjeDubina = 0;
            if (!int.TryParse(edubinaUkrstanja.Text, out ukrstanjeDubina))
            {
                MessageBox.Show(Resources.SR_CrossOverdepthInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.maxCossoverLevel = ukrstanjeDubina;

            int mutacijaDubina = 0;
            if (!int.TryParse(edubinaMutacije.Text, out mutacijaDubina))
            {
                MessageBox.Show(Resources.SR_MutationDepthInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.maxMutationLevel = mutacijaDubina;


            float vjerUkrstanje = 0;
            if (!float.TryParse(evjerojatnostUkrstanja.Text, out vjerUkrstanje))
            {
                MessageBox.Show(Resources.SR_probCrossOverInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.probCrossover = vjerUkrstanje;

            float vjerMutacija = 0;
            if (!float.TryParse(evjerojatnostMutacije.Text, out vjerMutacija))
            {
                MessageBox.Show(Resources.SR_ProbMutationInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.probMutation = vjerMutacija;

            float vjerSelekcija = 0;
            if (!float.TryParse(evjerojatnostReprodukcije.Text, out vjerSelekcija))
            {
                MessageBox.Show(Resources.SR_ProbSelectionInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.probReproduction = vjerSelekcija;

            float vjerPermutacija = 0;
            if (!float.TryParse(evjerojatnostPermutacije.Text, out vjerPermutacija))
            {
                MessageBox.Show(Resources.SR_ProbPermutationInvalid, Properties.Resources.SR_ApplicationName);
                return false;
            }
            GPParameters.probPermutation = vjerPermutacija;


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
            doc.Save(@"FunctionSet.xml");
        }

        private void GeneriranjeFunkcija()
        {
            SpasiFunkcijeuXML();

            //Kada se dijalog zatvori onda izvucu sve funkcije koje su odabrane
            var q = from c in functionSetsList
                    where c.Selected == true
                    select c;
            //Prvi ocisti stare
            functionSet.functions.Clear();
            //Ubaci nove funkcije shodno njihovim tezinama 
            // npr. ako je Wight 2 tada dva puta staviti datu operacju u listu
            if (functionSet.functions != null)
                functionSet.functions = new List<GPFunction>();
            foreach (var op in q)
            {
                for (int i = 0; i < op.Weight; i++ )
                    functionSet.functions.Add(op);
            }
            //Shuffle the functions
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
               int numOfVariables = terminalSet.NumVariables+terminalSet.NumConstants+1/*Output Value of experiment*/
            ;
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
                    rsquareEditbix.Text = "0";

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
            IsGPRunning = true;
        }
        private void ZamrzniUSlobodnoVrijeme()
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;
            comboBox2.Enabled = true;
            brojIteracija.Enabled = true;
            IsGPRunning = false;
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
        //Osvjezavanje gridKontrole sa podacima u varijabli TrainingData
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
            if (backgroundWorker1.IsBusy)
                return;
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
            if (population == null)
            {
                maxFitness.Clear();
                avgFitness.Clear();
               // rSquare.Clear();
            }

            //Podesavanje vreman startanja
            vrijemeZaJedanRun = DateTime.Now;

            if (population == null)
            {
                eTimeStart.Text = vrijemeZaJedanRun.ToString("dd/MM/yyyy : hh:mm:ss");
                pocetakEvolucije = vrijemeZaJedanRun;
            }

           
            backgroundWorker1.RunWorkerAsync(conditionValue);
           // DoWorkEventArgs ee = new DoWorkEventArgs(conditionValue);
           // backgroundWorker1_DoWork(null, ee);
            //POdesavanje dugmadi
            ZamrzniZavrijemeRada();
        }
        //DOWOrk
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //pocetna evolucija je nulta 
            int tekucaEvolucija = 0;

            //Argument koji smo proslijedili iz osnovne niti a tice se iteracija algoritma
            Nullable<float> brIteracija = e.Argument as Nullable<float>;

            //Creiranje pocetne populacije, ovdje je moguće nastaviti stopirani evoluciju ali u slucaju da
            // se neki kljucni parametri nisu promjenili.
            if (population == null)
            {
                population = new GPPopulation(velPopulacije, terminalSet, functionSet, GPParameters,bParalel);
                population.RunStarted = DateTime.Now;
            }
            else
            {
                if(population.EvolutionHistrory.Count>0)
                    tekucaEvolucija = population.EvolutionHistrory.Count-1;
                //Akoje populacija vec formirana odnosnoakosmoucitalifile i 
                //pokrećemo evuluciju tamogdje smostali zadnji put.U ovom slucaju nova populacija 
                // se ne formira nego se samo neki parametri mogu promijeniti njih podesavamo  
                GPPopulation.GPParameters = GPParameters;
                population.IsParalelComputing = bParalel;

                //Ako smo populaciju promjenili u parametrima
                //prilikom ponovnog pokretanja trebamo to uskladiti
                int countPop = population.Population.Count();
                if (countPop > velPopulacije)
                {
                    population.Population.RemoveRange(velPopulacije, countPop - velPopulacije);
                    population.PopulationSize = population.Population.Count;
                }
                else if (countPop < velPopulacije)
                    population.InitPopulation(velPopulacije - countPop, false);

            }
            if (GPTestingData != null)
                GPPopulation.GPTerminalSet.TestingData = GPTestingData;
                
            GPBestHromosome= population.BestChromosome;
            GPHistory gpHistory = new GPHistory();
            gpHistory.AvgFitness = population.fitnessAvg;
            gpHistory.BestHromosome = GPBestHromosome;
          //  gpHistory.Duration = 0;
           // gpHistory.Generation = tekucaEvolucija;
            population.EvolutionHistrory.Add(gpHistory);
                       
            //Raportiraj o progresu
            backgroundWorker1.ReportProgress(tekucaEvolucija, population.BestChromosome);

            AddPointsToSimulateFitness(tekucaEvolucija, population.Population[0].Fitness, population.fitnessAvg/*, population.BestChromosome.RSquare*/);
            //Nakon pocetne populacije ide startanje evolucije i 
            tekucaEvolucija++;
            
            while (ProvjeriUslovEvolucije(tekucaEvolucija))
            {
                
                    population.StartEvolution();

                    //Dodavanje tacaka za dijagram
                    AddPointsToSimulateFitness(tekucaEvolucija, population.Population[0].Fitness, population.fitnessAvg/*, population.BestChromosome.RSquare*/);
                    //Save to history
                    gpHistory = new GPHistory();
                    gpHistory.AvgFitness = population.fitnessAvg;
                    gpHistory.BestHromosome = GPBestHromosome;
                   // gpHistory.Duration = 0;
                   // gpHistory.Generation = tekucaEvolucija;
                    population.EvolutionHistrory.Add(gpHistory);

                    //Raportiraj o progresu
                    backgroundWorker1.ReportProgress(tekucaEvolucija, population.BestChromosome);
             
                    //Ako je pritisnuto dugme STOP
                    if (backgroundWorker1.CancellationPending)
                        break;

                    //Povecaj tekucu evoluciju za 1
                    tekucaEvolucija++;
               
            }


        }

        private void AddPointsToSimulateFitness(double iteration, double maxFit, double avgFit/*, float rSqr*/)
        {
            maxFitness.AddPoint(iteration, maxFit);
            avgFitness.AddPoint(iteration, avgFit);
           // rSquare.AddPoint(iteration, (rSqr < 0 ? 0 : rSqr * 1000.0));
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
            /*else if (busloviEvolucije == 2)
            {
                return GPBestHromosome.RSquare < conditionValue;
            }*/
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

                zedGraphPopulation.Invalidate();

                // Compare best fitness with max current fitness
                currentIterationBox.Text = e.ProgressPercentage.ToString();
                if (GPBestHromosome.Fitness< chrom.Fitness)
                {
                    currentErrorBox.Text = chrom.Fitness.ToString("F6");
                    if (chrom.RSquare >= 0)
                        rsquareEditbix.Text = chrom.RSquare.ToString("F6");
                    else
                        rsquareEditbix.Text = 0.ToString("F6");

                    bestFitnessAtGenerationEditBox.Text = e.ProgressPercentage.ToString();

                    GPBestHromosome = chrom;
                    population.BestCHromosomeApear = e.ProgressPercentage; 
                
                    IzracunajModel();
                    this.zedChart.Invalidate();
                }
                //Kada uslov za evaluaciju nije brojiteracija onda se mora povecavati 
                // maximalna vrijednost progres bara
                if(progressBar1.Maximum >= e.ProgressPercentage)
                    progressBar1.Value = e.ProgressPercentage;
                GPHistory his = population.EvolutionHistrory[e.ProgressPercentage];
                his.Duration = razlika;
                vrijemeZaJedanRun = vrijeme;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.SR_ApplicationName);
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                eDuration.Text = ((float)(vrijemeZaJedanRun - pocetakEvolucije).TotalMinutes).ToString("F1");
                population.DurationOfRun = (vrijemeZaJedanRun - pocetakEvolucije);
                progressBar1.Value = progressBar1.Maximum;
                ZamrzniUSlobodnoVrijeme();
                PrikaziModel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void IzracunajModel()
        {
             //Translate chromosome to list expressions
            int indexOutput = terminalSet.NumConstants + terminalSet.NumVariables-1;
            List<ushort> lst = GPBestHromosome.NodeValueEnumeratorDepthFirst.ToList();
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

        private void IzracunajPredikcijskiModel(List<ushort> lst)
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
        private void IzracunajPredikcijskiModelSerie(List<ushort> lst)
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
                dataGridViewRezultat.Columns.Add("colRB", Resources.SR_SampleNumber);
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
                MessageBox.Show(Resources.SR_testingModelError, Resources.SR_ApplicationName);
            }
        }

        private void PrikaziIOptimizirajGPModel()
        {
            List<ushort> lst = GPBestHromosome.NodeValueEnumeratorDepthFirst.ToList();
            enooptMatematickiModel.Text = functionSet.DecodeExpression(lst, terminalSet);
        }
        //Visual presentation of the tree
        private void button1_Click(object sender, EventArgs e)
        {
            if (GPBestHromosome == null)
                return;
            TreeExpression dlg = new TreeExpression();
            dlg.TreeDrawer.DrawTreeExpression(GPBestHromosome.FunctionTree, functionSet);
            dlg.ShowDialog();
            dlg.Dispose();
        }
        //Load testing TrainingData
        private void button2_Click(object sender, EventArgs e)
        {
            if (gpModel == null)
            {
                MessageBox.Show(Properties.Resources.SR_LoadTrainingDataFirst,Properties.Resources.SR_ApplicationName);
                return;
            }
            if (gpModel.Length == 0)
            {
                MessageBox.Show(Properties.Resources.SR_LoadTrainingDataFirst,Properties.Resources.SR_ApplicationName);
                return;
            }
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

                    if (kolone.Length != GPTrainingData.Length)
                    {
                        MessageBox.Show(Properties.Resources.SR_TestingNumberParametersNotValid, Properties.Resources.SR_ApplicationName);
                        return;
                    }

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

            dataGridViewTestData.Columns.Add("colRB",Resources.SR_SampleNumber);
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
                rsquareEditbix.Text = "0";

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
                    //gpModel = new double[TrainingData[0].Length, 2];
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
        //Convert time serie to GP Algoritam
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

        private void cfitnessFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            GPParameters.efitnessFunction = (EFitnessFunction)cfitnessFunction.SelectedIndex;
        }

        private void GP_TS_Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        
        
    }
}
