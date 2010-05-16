// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// GPdotNET 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using System.Xml.Linq;
using System.Diagnostics;
using GPNETLib;
using System.Deployment.Application;
namespace GPDOTNET
{
    public partial class GP_TS_Panel : Form
    {
        public string filePath;

        GPPopulation population;
        GPFunctionSet functionSet;
        GPTerminalSet terminalSet;
        GPParameters parameters;
        //Varijabla za izracunavanje vremena jednog una
        DateTime vrijemeZaJedanRun;
        DateTime pocetakEvolucije;
        //Lista funkcija koje se ucitavaju iz xml datoteke
        XDocument doc;
        public List<GPFunction> functionSetsList;

        //Model sa podacima za Graf
        LineItem modelItem;
        LineItem modelTestIten;
        LineItem maxFitness, avgFitness, rSquare;
        
        //Podaci experimenta za treniranje GP modela
        double[][] trainingData;
        double[,] gpModel;

        //Podaci experimenta za testiranje GP modela
        double[][] testingData;
        double[,] gpTestModel;

        //POdaci o time series prediction

        bool timeSeriesPrediction = false;
        double[] timeSerie;

        double[] gpConstants;
        //karakteristke populacije
        GPChromosome bestHromosome;
        int velPopulacije;
        bool bParalel;
        int busloviEvolucije;
        float conditionValue;

        bool ignoreediting = true;
        public GP_TS_Panel()
        {
            InitializeComponent();

            terminalSet = new GPTerminalSet();
            functionSet = new GPFunctionSet();
            cmetodaGeneriranja.SelectedIndex = 2;
            cmetodaSelekcije.SelectedIndex = 0;

           GPParameters parameters = new GPParameters();

           evjerojatnostMutacije.Text = (5.0 / 100.0).ToString();
           evjerojatnostPermutacije.Text = (5.0 / 100.0).ToString();
           evjerojatnostReprodukcije.Text = (20.0 / 100.0).ToString();
           evjerojatnostUkrstanja.Text = (90.0 / 100.0).ToString();

                      
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Podesavanje grafika            
            this.zedEksperiment.GraphPane.Title.Text = "Eksperiment/Model";
            this.zedEksperiment.GraphPane.XAxis.Title.Text = "Kontrol points";
            this.zedEksperiment.GraphPane.YAxis.Title.Text = "Function value";

            this.zedChart.GraphPane.Title.Text = "Solution graph";
            this.zedChart.GraphPane.XAxis.Title.Text = "Number of points";
            this.zedChart.GraphPane.YAxis.Title.Text = "Output";

            this.zedGraphPopulation.GraphPane.Title.Text = "Populations";
            this.zedGraphPopulation.GraphPane.XAxis.Title.Text = "Generation";
            this.zedGraphPopulation.GraphPane.YAxis.Title.Text = "Fitness value";

            maxFitness = zedGraphPopulation.GraphPane.AddCurve("Maximum Fitness", null, null, Color.Red, ZedGraph.SymbolType.None);
            avgFitness = zedGraphPopulation.GraphPane.AddCurve("Average Fitness", null, null, Color.Blue, ZedGraph.SymbolType.None);
            rSquare = zedGraphPopulation.GraphPane.AddCurve("R Square", null, null, Color.Gray, ZedGraph.SymbolType.None);
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
                InitFile();
            }
        }

        private void InitFile()
        {
            ignoreediting = true;
            functionSet = population.gpFunctionSet;
            terminalSet = population.gpTerminalSet;
            parameters = population.gpParameters;

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

            epocetnaDubinaDrveta.Text = parameters.maxInitLevel.ToString();
            edubinaUkrstanja.Text = parameters.maxCossoverLevel.ToString();
            edubinaMutacije.Text = parameters.maxMutationLevel.ToString();

            evjerojatnostUkrstanja.Text = parameters.probCrossover.ToString();
            evjerojatnostMutacije.Text = parameters.probMutation.ToString();
            evjerojatnostReprodukcije.Text = parameters.probReproduction.ToString();
            evjerojatnostPermutacije.Text = parameters.probPermutation.ToString();

            evelicinaPopulacije.Text = population.population.Count.ToString();
            //Ucitavanje modela 

            //    terminalSet.NumConstant
            gpConstants = new double[terminalSet.NumConstant];
            for (int i = 0; i < terminalSet.NumConstant; i++)
            {
                gpConstants[i] = terminalSet.data[0][terminalSet.NumVariable + i];
            }
            PopunuGridSaKonstantama();

            //Testing data

            trainingData = new double[terminalSet.NumVariable + 1][];
            for (int i = 0; i < terminalSet.NumVariable + 1; i++)
            {
                trainingData[i] = new double[terminalSet.RowCount];

                for (int j = 0; j < terminalSet.RowCount; j++)
                {
                    if (i == terminalSet.NumVariable)
                        trainingData[i][j] = terminalSet.data[j][terminalSet.data[0].Length - 1];
                    else
                        trainingData[i][j] = terminalSet.data[j][i];
                }

            }
            //Na osnovu experimenta definisemo kakav ce modle biti
            gpModel = new double[trainingData[0].Length, 2];
            // update list and chart
            UpdateDataGridView();
            UpdateChart(trainingData);

            //testni podaci
            if (population.gpTerminalSet.testingData != null)
            {
                testingData = population.gpTerminalSet.testingData;
                NapuniGridTestnimPodacima();
            }
            bestHromosome = population.bestChromosome;
            // set current iteration's info
            currentErrorBox.Text = bestHromosome.Fitness.ToString("F6");
            if (bestHromosome.RSquare >= 0)
                rsquareEditbix.Text = bestHromosome.RSquare.ToString("F6");
            else
                rsquareEditbix.Text = 0.ToString("F6");

            IzracunajModel();
            this.zedChart.Invalidate();
            IzracunajModel();
            PrikaziModel();

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
            //Open XML file with function definition
            doc = XDocument.Load(strPath);
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            col.HeaderText = "Chcek";

            col.ReadOnly = false;
            // 
            var q = from c in doc.Descendants("FunctionSet")
                    select new GPFunction
                    {
                        Selected = bool.Parse(c.Element("Selected").Value),
                        Name = c.Element("Name").Value,
                        Definition = c.Element("Definition").Value,
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
            functionSetsList = q.ToList();
            dataGridViewBuiltInFunction.DataSource = functionSetsList;           
            dataGridViewBuiltInFunction.Columns[1].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[2].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[3].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[4].ReadOnly = true;
            dataGridViewBuiltInFunction.Columns[5].Visible = false;
            dataGridViewBuiltInFunction.Columns[6].Visible = false;
            dataGridViewBuiltInFunction.AutoResizeColumns();
        }
        private bool PodesiParametreGP()
        {
            if (parameters == null)
                parameters = new GPParameters();

            parameters.einitializationMethod = (EInitializationMethod)cmetodaGeneriranja.SelectedIndex;
            parameters.eselectionMethod = (ESelectionMethod)cmetodaSelekcije.SelectedIndex;

            int pocetnaDubina = 0;
            if (!int.TryParse(epocetnaDubinaDrveta.Text, out pocetnaDubina))
            {
                MessageBox.Show("Initial depth is not valid!");
                return false;
            }
            parameters.maxInitLevel = pocetnaDubina;

            int ukrstanjeDubina = 0;
            if (!int.TryParse(edubinaUkrstanja.Text, out ukrstanjeDubina))
            {
                MessageBox.Show("Corssover depth is not valid!");
                return false;
            }
            parameters.maxCossoverLevel = ukrstanjeDubina;

            int mutacijaDubina = 0;
            if (!int.TryParse(edubinaMutacije.Text, out mutacijaDubina))
            {
                MessageBox.Show("Mutation depth is not valid!");
                return false;
            }
            parameters.maxMutationLevel = mutacijaDubina;


            float vjerUkrstanje = 0;
            if (!float.TryParse(evjerojatnostUkrstanja.Text, out vjerUkrstanje))
            {
                MessageBox.Show("Probability of crossover is not valid!");
                return false;
            }
            parameters.probCrossover = vjerUkrstanje;

            float vjerMutacija = 0;
            if (!float.TryParse(evjerojatnostMutacije.Text, out vjerMutacija))
            {
                MessageBox.Show("Probability of mutation is not valid!");
                return false;
            }
            parameters.probMutation = vjerMutacija;

            float vjerSelekcija = 0;
            if (!float.TryParse(evjerojatnostReprodukcije.Text, out vjerSelekcija))
            {
                MessageBox.Show("Probability of selection is not valid!");
                return false;
            }
            parameters.probReproduction = vjerSelekcija;

            float vjerPermutacija = 0;
            if (!float.TryParse(evjerojatnostPermutacije.Text, out vjerPermutacija))
            {
                MessageBox.Show("Probability of selection is not valid!");
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
            //Ubaci nove funkcije
            functionSet.functions = q.ToList();

        }
        void TestniSkupFunkcija()
        {
            functionSet = new GPFunctionSet();
            GeneriranjeFunkcija();
            return;
        }
        void TerminaliIzExperimenta()
        {
            int numVariable = terminalSet.NumVariable;
            int numConst = terminalSet.NumConstant;
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
            if (trainingData == null)
            {
                MessageBox.Show("Load training data first!");
                return false;
            }
            if(terminalSet==null)
                terminalSet= new GPTerminalSet();

            int intOD = 0;
            if (!int.TryParse(intervalOdTextBox.Text, out intOD))
            {
                MessageBox.Show("Incorect number of constants interval!It must be an integer!");
                return false;
            }
            int intDO = 0;
            if (!int.TryParse(intervalDoTextBox.Text, out intDO))
            {
                MessageBox.Show("Incorect number of constants interval!It must be an integer!");
                return false;
            }
            int numConst = 0;
            if (!int.TryParse(brojKonstantiTextBox.Text, out numConst))
            {
                MessageBox.Show("Incorect number of constants!It must be an integer!");
                return false;
            }
            if (gpConstants == null)
                GenerateConstants(intOD, intDO, numConst);

            //Kada znamo broj konstanti i podatke o experimenti sada mozemo popuniti podatke
            terminalSet.NumConstant = numConst;
            terminalSet.NumVariable=trainingData.Length-1;
            terminalSet.RowCount=trainingData[0].Length;
            
            terminalSet.data = new double[terminalSet.RowCount][];
            int numOfVariables = terminalSet.NumVariable+terminalSet.NumConstant+1/*Output Value of experiment*/;
            for (int i = 0; i < terminalSet.RowCount; i++)
            {
                terminalSet.data[i] = new double[numOfVariables];
                for (int j = 0; j < numOfVariables; j++)
                {
                    if (j < terminalSet.NumVariable)//Nezavisne varijable
                        terminalSet.data[i][j] = trainingData[j][i];
                    else if (j >= terminalSet.NumVariable && j < numOfVariables-1)//Konstante
                        terminalSet.data[i][j] = gpConstants[j - terminalSet.NumVariable];
                    else
                        terminalSet.data[i][j] = trainingData[j - terminalSet.NumConstant][i];//Izlazna varijabla iz eperimenta
                }
            }
            //Ako smo ucitali podatke za testiranje Predikciju ovjde je ucitavam
            terminalSet.Izracunaj();
            // Sada na osnovu experimenta formiramo terminale
            TerminaliIzExperimenta();
            return true;
        }

        //Generiranje konstanti
        private void button3_Click(object sender, EventArgs e)
        {
            if (population != null)
            {
                DialogResult retVal = MessageBox.Show("Changing random constants, current population will be discarded.\n Do you want to continue?", "GPdotNET", MessageBoxButtons.YesNo);

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
                MessageBox.Show("Incorect number of constants interval!It must be an integer!");
                return ;
            }
            int intDO = 0;
            if (!int.TryParse(intervalDoTextBox.Text, out intDO))
            {
                MessageBox.Show("Incorect number of constants interval!It must be an integer!");
                return ;
            }
            int numConst = 0;
            if (!int.TryParse(brojKonstantiTextBox.Text, out numConst))
            {
                MessageBox.Show("Incorect number of constants!It must be an integer!");
                return ;
            }
            GenerateConstants(intOD, intDO, numConst);
            MessageBox.Show("Compleated!");
        }
        //Generiranje konstanti iz podrucja vrijednosti
        public void GenerateConstants(int from, int to, int number)
        {
            gpConstants = new double[number];

            for (int i = 0; i < number; i++)
            {
                decimal val =(decimal) (GPPopulation.rand.Next(from, to) + GPPopulation.rand.NextDouble());
                gpConstants[i] = (double)decimal.Round(val, 5);
            }

            PopunuGridSaKonstantama();
            
        }

        private void PopunuGridSaKonstantama()
        {
            dataGridViewRandomConstants.Columns.Clear();
            dataGridViewRandomConstants.Rows.Clear();
            dataGridViewRandomConstants.Columns.Add("colRB", "RB");
            dataGridViewRandomConstants.Columns.Add("colConst", "Constant");
            dataGridViewRandomConstants.Columns[0].Width = 30;
            dataGridViewRandomConstants.Columns[1].Width = 70;

            for (int i = 0; i < gpConstants.Length; i++)
            {
                int r = dataGridViewRandomConstants.Rows.Add();
                dataGridViewRandomConstants.Rows[r].Cells[0].Value = "R" + (i + 1).ToString();
                dataGridViewRandomConstants.Rows[r].Cells[1].Value = gpConstants[i];
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
                if (terminalSet.data != null)
                    terminalSet.data = null;
                StreamReader reader = null;

                try
                {
                    // open selected file
                    reader = System.IO.File.OpenText(openFileDialog1.FileName);
                    //read data in to buffer
                    string buffer = reader.ReadToEnd();
                    //Remove delimating chars
                    char[] sep = { '\r', '\n' };
                    //define the row
                    string[] vrste = buffer.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //Define the columns
                    string[] kolone = vrste[0].Split(';');
                    //Define inner data
                    trainingData = new double[kolone.Length][];
                    

                    for (int j = 0; j < kolone.Length; j++)
                    {
                        trainingData[j] = new double[vrste.Length];

                        for (int k = 0; k < vrste.Length; k++)
                        {
                            kolone = vrste[k].Split(';');
                            trainingData[j][k] = double.Parse(kolone[j]);
                        }
                    }

                    //Na osnovu experimenta definisemo kakav ce modle biti
                    gpModel = new double[trainingData[0].Length, 2];
                }
                catch (Exception)
                {
                    MessageBox.Show("Error reding file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                UpdateChart(trainingData);

                // enable "Start" button
                startButton.Enabled = true;
                timeSeriesPrediction = false;
            }
        }
        private void UpdateChart(double[][] dd)
        {
            double[] x = new double[dd[0].Length];

            int m = dd.Length;
            for (int k = 0; k < dd[0].Length; k++)
                x[k] = k + 1;

            zedChart.GraphPane.AddCurve("Training Set", x, dd[dd.Length - 1], Color.Red, ZedGraph.SymbolType.Triangle);
            modelItem = zedChart.GraphPane.AddCurve("Model Set", null, null, Color.Blue, ZedGraph.SymbolType.Diamond);
            zedChart.GraphPane.XAxis.Scale.Max = dd[0].Length+1;
            this.zedChart.GraphPane.AxisChange(this.CreateGraphics());

            zedEksperiment.GraphPane.AddCurve("Training set data", x, dd[dd.Length - 1], Color.Red, ZedGraph.SymbolType.Triangle);
            zedEksperiment.GraphPane.XAxis.Scale.Max = dd[0].Length+1;
            this.zedEksperiment.GraphPane.AxisChange(this.CreateGraphics());
            this.zedEksperiment.Invalidate();
        }
        //Osvjezavanje gridKontrole sa podacima u varijabli trainingData
        private void UpdateDataGridView()
        {
            dataGridViewPodaci.Columns.Clear();
            dataGridViewPodaci.Rows.Clear();

            dataGridViewPodaci.Columns.Add("colRB", "RB");
            string str;

            for (int i = 0; i < trainingData.Length; i++)
            {
                if (trainingData.Length == i + 1)
                    str = "Y";
                else
                    str = "X" + i.ToString();

                dataGridViewPodaci.Columns.Add(str, str);
            }


            for (int i = 0; i < trainingData[0].Length; i++)
            {
                int r = dataGridViewPodaci.Rows.Add();
                dataGridViewPodaci.Rows[r].Cells[0].Value = i + 1;

                for (int j = 0; j < trainingData.Length; j++)
                    dataGridViewPodaci.Rows[r].Cells[j + 1].Value = trainingData[j][i];

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
            ignoreediting = false;
            if (!PodesiParametreGP())
                return;

            TestniSkupFunkcija();

            if (!Generateterminals())
                return;

            

            //Velicina populacije
            if (!int.TryParse(evelicinaPopulacije.Text, out velPopulacije))
            {
                MessageBox.Show("Population size is not valid.");
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
                MessageBox.Show("Condition of evolution is not valid. Please enter the float number!");
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
            rSquare.Clear();

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
            //Argument koji smo proslijedili iz osnovne niti a tice se iteracija algoritma
            Nullable<float> brIteracija = e.Argument as Nullable<float>;

            //Creiranje pocetne populacije, ovdje je moguće nastaviti stopirani evoluciju ali u slucaju da
            // se neki kljucni parametri nisu promjenili.
            if (population == null)
            {
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
            if (testingData != null)
                population.gpTerminalSet.testingData = testingData;
                
            //Uslov za kriterij
            if (busloviEvolucije == 2)
                population.fitnessCriteria = false;
            else
                population.fitnessCriteria = true;

            //Nakon pocetne populacije ide startanje evolucije i 
            int tekucaEvolucija=1;

            bestHromosome= population.bestChromosome;

            //Raportiraj o progresu
            backgroundWorker1.ReportProgress(tekucaEvolucija, population.bestChromosome);
            
            while (ProvjeriUslovEvolucije(tekucaEvolucija))
            {
                
                    population.StartEvolution();

                    //Dodavanje tacaka za dijagram
                    maxFitness.AddPoint(tekucaEvolucija, population.fitnessMax);
                    avgFitness.AddPoint(tekucaEvolucija, population.fitnessAvg);
                    rSquare.AddPoint(tekucaEvolucija, population.bestChromosome.RSquare*1000.0);

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
                return bestHromosome.Fitness <= conditionValue;
            }
            else if (busloviEvolucije == 2)
            {
                return bestHromosome.RSquare < conditionValue;
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
                    eTimeleft.Text = "Undefined";
                    eTimeToCompleate.Text = "Undefined";
                }
                
                GPChromosome chrom = (GPChromosome)e.UserState;

                zedGraphPopulation.Invalidate();

                // set current iteration's info
                currentIterationBox.Text = e.ProgressPercentage.ToString();
                if (currentErrorBox.Text != chrom.Fitness.ToString("F6"))
                {
                    currentErrorBox.Text = chrom.Fitness.ToString("F6");
                    if (chrom.RSquare >= 0)
                        rsquareEditbix.Text = chrom.RSquare.ToString("F6");
                    else
                        rsquareEditbix.Text = 0.ToString("F6");

                    bestFitnessAtGenerationEditBox.Text = e.ProgressPercentage.ToString();
                    bestHromosome = chrom;
                    IzracunajModel();
                    this.zedChart.Invalidate();
                }
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
                eDuration.Text = ((float)(vrijemeZaJedanRun - pocetakEvolucije).TotalMinutes).ToString("F1"); 
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
            int indexOutput = terminalSet.NumConstant + terminalSet.NumVariable-1;
            List<int> lst = new List<int>();
            FunctionTree.ToListExpression(lst, bestHromosome.Root);
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
            if (!timeSeriesPrediction)
                IzracunajPredikcijskiModel(lst);
            else
                IzracunajPredikcijskiModelSerie(lst);

        }

        private void IzracunajPredikcijskiModel(List<int> lst)
        {
            double y;
            //Ako testni podaci nisu ucitani onda nista
            if (testingData == null)
                return;
            //Izracunavanje Testnih podataka na osnovu modela
            GPTerminalSet testingSet = new GPTerminalSet();
            testingSet.NumConstant = terminalSet.NumConstant;
            testingSet.NumVariable = testingData.Length - 1;
            testingSet.RowCount = testingData[0].Length;
            int numOfVariables = testingSet.NumVariable + testingSet.NumConstant + 1/*Output Value of experiment*/;
            testingSet.data = new double[testingSet.RowCount][];
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                testingSet.data[i] = new double[numOfVariables];
                for (int j = 0; j < numOfVariables; j++)
                {
                    if (j < testingSet.NumVariable)//Nezavisne varijable
                        testingSet.data[i][j] = testingData[j][i];
                    else if (j >= terminalSet.NumVariable && j < numOfVariables - 1)//Konstante
                        testingSet.data[i][j] = terminalSet.data[0][j];
                    else
                        testingSet.data[i][j] = testingData[j - terminalSet.NumConstant][i];//Izlazna varijabla iz eperimenta
                }
            }
            testingSet.Izracunaj();

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
            if (testingData == null)
                return;
            //Izracunavanje Testnih podataka na osnovu modela
            GPTerminalSet testingSet = new GPTerminalSet();
            testingSet.NumConstant = terminalSet.NumConstant;
            testingSet.NumVariable = testingData.Length - 1;
            testingSet.RowCount = testingData[0].Length;
            int numOfTerminals = testingSet.NumVariable + testingSet.NumConstant + 1/*Output Value of experiment*/;
            testingSet.data = new double[testingSet.RowCount][];
            
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                testingSet.data[i] = new double[numOfTerminals];
                for (int j = 0; j < numOfTerminals; j++)
                {
                    if (j < testingSet.NumVariable)//Nezavisne varijable
                        testingSet.data[i][j] = testingData[j][i];
                    else if (j >= terminalSet.NumVariable && j < numOfTerminals - 1)//Konstante
                        testingSet.data[i][j] = terminalSet.data[0][j];
                    else
                        testingSet.data[i][j] = testingData[j - terminalSet.NumConstant][i];//Izlazna varijabla iz eperimenta
                }
            }
            testingSet.Izracunaj();

            y = 0;
            modelTestIten.Clear();
            for (int i = 0; i < testingSet.RowCount; i++)
            {
                // evalue the function
                y = functionSet.Evaluate(lst, testingSet, i);

                // check for correct numeric value
                if (double.IsNaN(y) || double.IsInfinity(y))
                    y = 0;
                int k = testingSet.NumVariable;
                int j=0;
                //Kod time series potrebno je podesiti ulazne varijable u odnosu na neto izracunate vrijednosti
                for (j = i; j < testingSet.RowCount && k>=0; j++)
                {
                    
                    if(k==testingSet.NumVariable)
                        testingSet.data[j][testingSet.NumVariable +testingSet.NumConstant] = y;
                    else
                        testingSet.data[j][k] = y;
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
                for (int i = 1; i < trainingData.Length; i++)
                {
                    str = "X" + i.ToString();
                    dataGridViewRezultat.Columns.Add(str, str);
                }
                dataGridViewRezultat.Columns.Add("colY", "Y");
                dataGridViewRezultat.Columns.Add("colYa", "Ya");
                dataGridViewRezultat.Columns.Add("colD", "R");

                for (int i = 0; i < trainingData[0].Length; i++)
                {
                    int r = dataGridViewRezultat.Rows.Add();
                    dataGridViewRezultat.Rows[r].Cells[0].Value = i + 1;
                    for (int j = 0; j < trainingData.Length; j++)
                    {
                        dataGridViewRezultat.Rows[r].Cells[j+1].Value = trainingData[j][i];

                    }
                    dataGridViewRezultat.Rows[r].Cells[trainingData.Length + 1].Value = gpModel[i,1];

                    dataGridViewRezultat.Rows[r].Cells[trainingData.Length + 2].Value = trainingData[trainingData.Length - 1][i] - gpModel[i,1];
                }

                PrikaziIOptimizirajGPModel();
                PrikaziModelTesta();
            }
            catch
            {
                MessageBox.Show("Greška u formiranju modela experimentalnih podataka");
            }
        }
        private void PrikaziModelTesta()
        {
            //Ako testni podaci nisu ucitani onda nista
            if (testingData == null)
                return;
            try
            {
                //Dodavanje dodatnih kolona na testne podatke
                if (dataGridViewTestData.Columns.Count != terminalSet.NumVariable + 4)
                {
                    dataGridViewTestData.Columns.Add("colYa", "Ya");
                    dataGridViewTestData.Columns.Add("colD", "R");
                }

                for (int i = 0; i < testingData[0].Length; i++)
                {

                    dataGridViewTestData.Rows[i].Cells[testingData.Length + 1].Value = gpTestModel[i, 1];

                    dataGridViewTestData.Rows[i].Cells[testingData.Length + 2].Value = testingData[testingData.Length - 1][i] - gpTestModel[i, 1];
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
            FunctionTree.ToListExpression(lst, bestHromosome.Root);
            eoptMatematickiModel.Text = functionSet.DecodeWithOptimisationExpression(lst, terminalSet);
            enooptMatematickiModel.Text = functionSet.DecodeExpression(lst, terminalSet);
        }
        //Visual presentation of the tree
        private void button1_Click(object sender, EventArgs e)
        {
            if (bestHromosome == null)
                return;
            TreeExpression dlg = new TreeExpression();
            dlg.TreeDrawer.DrawTreeExpression(bestHromosome.Root, functionSet);
            dlg.ShowDialog();
            dlg.Dispose();
        }
        //Load testing data
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
                    //read data in to buffer
                    string buffer = reader.ReadToEnd();
                    //Remove delimating chars
                    char[] sep = { '\r', '\n' };
                    //define the row
                    string[] vrste = buffer.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //Define the columns
                    string[] kolone = vrste[0].Split(';');
                    //Define inner data
                    testingData = new double[kolone.Length][];


                    for (int j = 0; j < kolone.Length; j++)
                    {
                        testingData[j] = new double[vrste.Length];

                        for (int k = 0; k < vrste.Length; k++)
                        {
                            kolone = vrste[k].Split(';');
                            testingData[j][k] = double.Parse(kolone[j]);
                        }
                    }

                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Error reding file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            gpTestModel = new double[testingData[0].Length, 2];

            dataGridViewTestData.Columns.Clear();
            dataGridViewTestData.Rows.Clear();

            dataGridViewTestData.Columns.Add("colRB", "RB");
            string str;

            for (int i = 0; i < testingData.Length; i++)
            {
                if (testingData.Length == i + 1)
                    str = "Y";
                else
                    str = "X" + i.ToString();

                dataGridViewTestData.Columns.Add(str, str);
            }


            for (int i = 0; i < testingData[0].Length; i++)
            {
                int r = dataGridViewTestData.Rows.Add();
                dataGridViewTestData.Rows[r].Cells[0].Value = i + 1;

                for (int j = 0; j < testingData.Length; j++)
                    dataGridViewTestData.Rows[r].Cells[j + 1].Value = testingData[j][i];

            }


            double[] x = new double[testingData[0].Length];

            int m = testingData.Length;
            for (int k = 0; k < testingData[0].Length; k++)
                x[k] = k + 1;

            zedGraphTestData.GraphPane.AddCurve("Testing data", x, testingData[testingData.Length - 1], Color.Red, ZedGraph.SymbolType.Triangle);
            modelTestIten = zedGraphTestData.GraphPane.AddCurve("Model Set", null, null, Color.Blue, ZedGraph.SymbolType.Diamond);
            zedGraphTestData.GraphPane.XAxis.Scale.Max = testingData[0].Length + 1;
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

            DialogResult retVal = MessageBox.Show("Changing function set, current population will be discarded.\n Do you want to continue?", "GPdotNET", MessageBoxButtons.YesNo);

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
                if (terminalSet.data != null)
                    terminalSet.data = null;
                StreamReader reader = null;

                try
                {
                    // open selected file
                    reader = System.IO.File.OpenText(openFileDialog1.FileName);
                    //read data in to buffer
                    string buffer = reader.ReadToEnd();
                    //Remove delimating chars
                    char[] sep = { '\r', '\n' };
                    //define the row
                    string[] vrste = buffer.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    //Define the columns
                    string[] kolone = vrste[0].Split(';');
                    //Define inner data
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
                    //gpModel = new double[trainingData[0].Length, 2];
                }
                catch (Exception)
                {
                    MessageBox.Show("Error reding file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                dataGridViewTimeSeries.Columns.Add("colRB", "RB");
                dataGridViewTimeSeries.Columns.Add("colSerie", "Value");


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
                MessageBox.Show("Please load series first!");
                return;
            }
            int numVariable, numberTestData;
            //Konfiguracije varijabli
            if (!int.TryParse(textBox4.Text, out numVariable))
            {
                MessageBox.Show("Please correct number of variables!");
                return;
            }
            //Konfiguracije varijabli
            if (!int.TryParse(textBox5.Text, out numberTestData))
            {
                MessageBox.Show("Please correct number testing samples!");
                return;
            }
            //Provjera konzistentnosti serije
            if (timeSerie.Length - 2 * numVariable - numberTestData < 0)
            {
                MessageBox.Show("Please correct number of variable and testing data!");
                return;
            }
            int numberTreningData=timeSerie.Length-numVariable-numberTestData;

            //Training data se sastoji od varijabli i izlazne funkcije
            trainingData = new double[numVariable+1][];
            for (int i = 0; i < numVariable+1; i++)
            {
                trainingData[i] = new double[numberTreningData];
                for (int j = i; j < numberTreningData+i; j++)
                    trainingData[i][j-i]=timeSerie[j];

            }

           //Na osnovu experimenta definisemo kakav ce modle biti
            gpModel = new double[trainingData[0].Length, 2];
            // update list and chart
            UpdateDataGridView();
            UpdateChart(trainingData);


            //POdaci za testiranje

            testingData = new double[numVariable+1][];
            for (int i = 0; i < numVariable + 1; i++)
            {
                testingData[i] = new double[numberTestData];
                int k = 0;
                for (int j = numberTreningData + i; j < numberTreningData + numberTestData+i; j++)
                {
                    testingData[i][k] = timeSerie[j];
                    k++;
                }

            }
            NapuniGridTestnimPodacima();
            timeSeriesPrediction = true;

        }

        
        
    }
}
