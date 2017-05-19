//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Tree based genetic programming tool                                         //
// Copyright 2006-2013 Bahrudin Hrnjica                                                 //
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPdotNET.Tool.Common;
using GPdotNET.Engine;
using GPdotNET.Core;
using GPdotNET.Util;
using System.Reflection;

namespace GPdotNET.App
{
    public partial class MainWindow : Form
    {
        #region Private Member
        //base class for all running panel
        BaseRunPanel            _baseRunPanel = null;

        InfoPanel               _infoPanel;
        DataPanel               _dataPanel;
        ExperimentPanel         _experimentPanel;
        FunctionPanel           _funPanel;
        SettingsPanel           _setPanel;
        ANNSettingsPanel        _setANNPanel;
        AnaliticFunctionDef     _funDefinit;
        RunPanel                _runPanel;
        ANNRunPanel             _runANNPanel;
        OptimizePanel           _optimizePanel;
        ResultPanel             _resultPanel;
        PreditionPanel          _predictionPanel;
        TSPRunPanel             _tspPanel;
        ALOCRunPanel            _alocPanel;

        GPModelType             _GPModel;

        GPFactory               _mainGPFactory;
        GPFactory               _secondFactory;

        AIFactory               _mainANNFactory;
       
        int                     _runningEngine = 0;

        int                     _bgpRuning = 0;

        string                  _filePath = "";
        bool                    _isFileDirty=false;
        ContextMenuStrip        _contextMenuStrip1;

        string                  _appName = "GPdotNET v4.0";
        //Open document through cmd line
        public string[] CmdLineParam
        {
            get;
            set;
        }
        #endregion

        #region Ctor and drag&drop methods
        public MainWindow()
        {
            InitializeComponent();
            _contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            Load += new EventHandler(MainWindow_Load);
            this.Icon = Extensions.LoadIconFromName("GPdotNET.App.Resources.gpdotnet_ico48.ico");
            // this.logoPictureBox.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.gp256V.png");

           

        }

       
        //Drag and drop functionality
        private void GPdotNetApp_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        //Drag and drop functionality
        private void GPdotNetApp_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileNames != null && fileNames.Length != 0)
            {
                Open(fileNames[0]);
            }
        }
        #endregion
        /// <summary>
        /// Resets programs
        /// </summary>
        private void ResetProgram()
        {
            //unscribe to events
            if (_mainGPFactory != null)
                this._mainGPFactory.ReportEvolution -= new EvolutionHandler(gpFactory_ReportEvolution);
            
            if (_secondFactory != null)
                this._secondFactory.ReportEvolution -= new EvolutionHandler(gpFactory_ReportEvolution);

           
            _mainGPFactory = null;
            _secondFactory = null;
           
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="model"></param>
		void LoadModelWizard(GPModelType model )
        {

            _GPModel = model;

            switch (_GPModel)
            {
                case GPModelType.SR:
                    {
                       

                        _dataPanel = new DataPanel();
                        loadGPPanelInMainWindow(this, _dataPanel, "Load Data");
                        _dataPanel.SetProblemType(_GPModel);

                        _funPanel = new FunctionPanel();
                        loadGPPanelInMainWindow(this, _funPanel, "Functions");

                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");

                        _runPanel = new RunPanel();
                        loadGPPanelInMainWindow(this, _runPanel, "Run");

                        _resultPanel = new ResultPanel();
                        loadGPPanelInMainWindow(this, _resultPanel, "Result");

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        this._mainGPFactory = new GPFactory();
                        this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        //set base run panel on _runPAnel
                        _baseRunPanel = _runPanel;

                    }
                    break;
                case GPModelType.SRO:
                    {
                       
                        _dataPanel = new DataPanel();
                        loadGPPanelInMainWindow(this, _dataPanel, "Load Data");
                        _dataPanel.SetProblemType(_GPModel);

                        _funPanel = new FunctionPanel();
                        loadGPPanelInMainWindow(this, _funPanel, "Functions");

                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");

                        _runPanel = new RunPanel();
                        loadGPPanelInMainWindow(this, _runPanel, "Run");


                        _optimizePanel = new OptimizePanel();
                        loadGPPanelInMainWindow(this, _optimizePanel, "Optimize Model");

                        _resultPanel = new ResultPanel();
                        loadGPPanelInMainWindow(this, _resultPanel, "Result");

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        this._mainGPFactory = new GPFactory();
                        this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        this._secondFactory = new GPFactory();
                        this._secondFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);


                        //set base run panel on _runPAnel
                        _baseRunPanel = _runPanel;
                    }
                    break;
                case GPModelType.TS:
                    {
                        
                        _dataPanel = new DataPanel();
                        loadGPPanelInMainWindow(this, _dataPanel, "Load Data");
                        _dataPanel.SetProblemType(_GPModel);

                        _funPanel = new FunctionPanel();
                        loadGPPanelInMainWindow(this, _funPanel, "Functions");

                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");
                        _setPanel.SetNumOfConstance(0);

                        _runPanel = new RunPanel();
                        loadGPPanelInMainWindow(this, _runPanel, "Run");

                        _resultPanel = new ResultPanel();
                        loadGPPanelInMainWindow(this, _resultPanel, "Result");

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        this._mainGPFactory = new GPFactory();
                        this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        //set base run panel on _runPAnel
                        _baseRunPanel = _runPanel;

                    }
                    break;
                case GPModelType.AO:
                    {
                     
                        _funDefinit = new AnaliticFunctionDef();
                        loadGPPanelInMainWindow(this, _funDefinit, "Analytic  function");

                        _funPanel = new FunctionPanel();
                        loadGPPanelInMainWindow(this, _funPanel, "Functions");
                        
                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");
                        _setPanel.ShowGAParams();

                        _optimizePanel = new OptimizePanel();
                        loadGPPanelInMainWindow(this, _optimizePanel, "Optimize Model");


                        //initi pages after creation
                        //
                        _funDefinit.LoadFuns(_funPanel.GPFunctions);
                        tabControl1.TabPages.RemoveAt(1);

                        this._secondFactory = new GPFactory();
                        this._secondFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        //set base run panel on _optimizePanel
                        _baseRunPanel = _optimizePanel;
                    }
                    break;
                case GPModelType.TSP:
                    {
                        _dataPanel = new DataPanel();
                        loadGPPanelInMainWindow(this, _dataPanel, "Load Data");
                        _dataPanel.SetProblemType(_GPModel);
                        
                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");
                        _setPanel.ShowGAParams();
                        var p = _setPanel.GetParameters();
                        p.popSize = 2500;
                        p.eselectionMethod = GPSelectionMethod.SkrgicSelection;
                        p.SelParam1 = 2.5f;
                        _setPanel.SetParameters(p);
                        
                        _tspPanel = new TSPRunPanel();
                        loadGPPanelInMainWindow(this, _tspPanel, "Simulation");

                        this._mainGPFactory = new GPFactory();
                        this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        //set base run panel on _TSPPanel
                        _baseRunPanel = _tspPanel;
                    }
                    break;
                case GPModelType.AP:
                    {
                        _dataPanel = new DataPanel();
                        loadGPPanelInMainWindow(this, _dataPanel, "Load Data");
                        _dataPanel.SetProblemType(_GPModel);

                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");
                        _setPanel.ShowGAParams();
                        var p = _setPanel.GetParameters();
                        p.popSize = 2500;
                        p.eselectionMethod = GPSelectionMethod.SkrgicSelection;
                        p.SelParam1 = 2.5f;
                        _setPanel.SetParameters(p);

                        _alocPanel = new ALOCRunPanel();
                        loadGPPanelInMainWindow(this, _alocPanel, "Simulation");

                        this._mainGPFactory = new GPFactory();
                        this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        //set base run panel on _TSPPanel
                        _baseRunPanel = _alocPanel;
                    }
                    break;
                case GPModelType.TP:
                    {
                        _dataPanel = new DataPanel();
                        loadGPPanelInMainWindow(this, _dataPanel, "Load Data");
                        _dataPanel.SetProblemType(_GPModel);

                        _setPanel = new SettingsPanel();
                        loadGPPanelInMainWindow(this, _setPanel, "Settings");
                        _setPanel.ShowGAParams();
                        var p = _setPanel.GetParameters();
                        p.popSize = 2500;
                        p.eselectionMethod = GPSelectionMethod.SkrgicSelection;
                        p.SelParam1 = 2.5f;
                        _setPanel.SetParameters(p);

                        _alocPanel = new ALOCRunPanel();
                        loadGPPanelInMainWindow(this, _alocPanel, "Simulation");

                        this._mainGPFactory = new GPFactory();
                        this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        //set base run panel on _TSPPanel
                        _baseRunPanel = _alocPanel;
                    }
                    break;
                case GPModelType.ANNMODEL:
                case GPModelType.GPMODEL:
                    {
                        _experimentPanel = new ExperimentPanel();
                         loadGPPanelInMainWindow(this, _experimentPanel, "Load Experiment");
                        _experimentPanel.SetProblemType(_GPModel);

                        //factory creating moved on place when the experimental data is created and prepared
                        //this._mainANNFactory = new ANNFactory();
                        //this._mainANNFactory.ReportIteration += new EvolutionHandler(annFactory_ReportIteration);

                    }
                    break;
                default:
                    break;
            }

            //Events from datapanel about loading dat
            if (_dataPanel != null)
               _dataPanel.DataLoaded += _dataPanel_DataLoaded;

            if (_dataPanel != null)
                _dataPanel.DataPredictionLoaded += _dataPanel_DataPredictionLoaded;

            //Events from experiment panel about loading dat
            if (_experimentPanel != null)
                _experimentPanel.DataLoaded += _experimentPanel_DataLoaded;

            if (_experimentPanel != null)
                _experimentPanel.DataPredictionLoaded += _experimentalPanel_DataPredictionLoaded;


            if (_funDefinit != null)
               _funDefinit.btnFinishAnalFun.Click += btnFinishAnalFun_Click;
                    
            

            if(_setPanel !=null)
                _setPanel.ResetSolution += _setPanel_ResetSolution;
               
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _setPanel_ResetSolution(object sender, EventArgs e)
        {
            if (_mainGPFactory != null)
                _mainGPFactory.ResetSolution();
            if (_secondFactory != null)
                _secondFactory.ResetSolution();
           
            if (_runPanel != null)
                _runPanel.ResetSolution();

            if (_runANNPanel != null)
                _runANNPanel.ResetSolution();

            if (_resultPanel != null)
                _resultPanel.ResetSolution();
            if (_predictionPanel != null)
                _predictionPanel.ResetSolution();
            if (_mainGPFactory != null)
                _mainGPFactory.ResetSolution();
            if (_mainGPFactory != null)
                _mainGPFactory.ResetSolution();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _dataPanel_DataPredictionLoaded(object sender, EventArgs e)
        {
            if (_predictionPanel == null)
            {
                _predictionPanel = new PreditionPanel();
                loadGPPanelInMainWindow(this, _predictionPanel, "Prediction");

            }
            if (_runPanel != null)
            {
                _predictionPanel.FillPredictionData(_dataPanel.Testing);

            }

            if (_runANNPanel != null)
            {
                _predictionPanel.FillPredictionData(_dataPanel.Testing);

            }

            _isFileDirty = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _experimentalPanel_DataPredictionLoaded(object sender, EventArgs e)
        {
            if (_predictionPanel == null)
            {
                var s = sender as ExperimentPanel;
                if(s!=null && s.numericUpDown1.Value >= 1)
                {
                    _predictionPanel = new PreditionPanel();
                    loadGPPanelInMainWindow(this, _predictionPanel, "Prediction");
                }
            }
            else
            {
                if (!_experimentPanel.Experiment.IsTestDataExist())
                {
                    removeGPPanel("Prediction");
                    _predictionPanel.Dispose();
                    _predictionPanel = null;
                }
                else
                    _predictionPanel.ResetSolution();
            }
            
            if (_runPanel != null)
            {
                if(_dataPanel!=null)
                    _predictionPanel.FillPredictionData(_dataPanel.Testing);
                else
                {
                    if(_experimentPanel.Experiment.IsTestDataExist())
                     _predictionPanel.FillPredictionData(_experimentPanel.Experiment);
                }
                   

            }

            if (_runANNPanel != null)
            {
                _predictionPanel.FillPredictionData(_experimentPanel.Experiment);

            }

            if (_infoPanel != null)
            {
                removeGPPanel("Info");

                _infoPanel = new InfoPanel();
                loadGPPanelInMainWindow(this, _infoPanel, "Info");

            }

            _isFileDirty = true;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnFinishAnalFun_Click(object sender, EventArgs e)
        {
            if (_optimizePanel != null)
            {
                _optimizePanel.FillTerminalBounds(_funDefinit.GetTerminalNames());

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _dataPanel_DataLoaded(object sender, EventArgs e)
        {
            if (_runPanel != null)
            {
                _runPanel.UpdateChartDataPoint(_dataPanel.GetOutputValues(), false);
                _isFileDirty = true;
            }
            
            if (_tspPanel != null)
            {
                _tspPanel.DrawCityMap(_dataPanel.GetCityMap());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _experimentPanel_DataLoaded(object sender, EventArgs e)
        {
            //Depending on experimental type of output data (numerical or categorical or boolean)
            // prepare the panels for parameters and type of the factory.
           if(_GPModel == GPModelType.ANNMODEL)
            {
                //create facotry based on column data type
                if (_experimentPanel.IsCategoricalOutput)
                    this._mainANNFactory = new PSOFactory();
                else
                    this._mainANNFactory = new BPFactory();

                //
                if (_setANNPanel == null)
                {
                    _setANNPanel = new ANNSettingsPanel();
                    loadGPPanelInMainWindow(this, _setANNPanel, "Settings");

                    //lock some parameters based on type of output variable
                    if(_experimentPanel.Experiment.GetOutputColumnType()== Core.Experiment.ColumnDataType.Binary ||
                       _experimentPanel.Experiment.GetOutputColumnType() == Core.Experiment.ColumnDataType.Categorical
                        )
                    _setANNPanel.SetLearnigAlgorithm(1);
                    else
                    {
                        _setANNPanel.SetLearnigAlgorithm(0);
                    }

                    _setANNPanel.LockLearningAlgoritm();
                }

                if (_runANNPanel == null)
                {
                    _runANNPanel = new ANNRunPanel();
                    loadGPPanelInMainWindow(this, _runANNPanel, "Modeling");

                }

                if (_infoPanel == null)
                {
                    _infoPanel = new InfoPanel();
                    loadGPPanelInMainWindow(this, _infoPanel, "Info");

                }

                if (_runANNPanel != null)
                {
                    this._mainANNFactory.ReportIteration += new EvolutionHandler(annFactory_ReportIteration);
                    _runANNPanel.UpdateChartDataPoint(_experimentPanel.GetOutputValues(), false);
                    _isFileDirty = true;
                }
            }
           else//Preparing GP for modelling nd prediction
            {
                //create facotry based on column data type
                if (_experimentPanel.IsBinarylOutput || _experimentPanel.IsCategoricalOutput)
                    this._mainGPFactory = new GPFactoryClass();
                else if (_experimentPanel.GetOutputColumnType() == Core.Experiment.ColumnDataType.Numeric)
                    this._mainGPFactory = new GPFactory();
                else
                {
                    throw new Exception("Unknown output value type!");
                }
                    

               
                if (_funPanel == null)
                {
                    _funPanel = new FunctionPanel();
                    loadGPPanelInMainWindow(this, _funPanel, "Functions");
                }

                if (_setPanel == null)
                {
                    _setPanel = new SettingsPanel();
                    loadGPPanelInMainWindow(this, _setPanel, "Settings");
                }
                //set problem type 
                _setPanel.SetParamForClassification(_experimentPanel.GetOutputColumnType());

                if (_runPanel == null)
                {
                    _runPanel = new RunPanel();
                    loadGPPanelInMainWindow(this, _runPanel, "Run");
                }


                if (_resultPanel == null)
                {
                    _resultPanel = new ResultPanel();
                    loadGPPanelInMainWindow(this, _resultPanel, "Result");
                }


                if (_infoPanel == null)
                {
                    _infoPanel = new InfoPanel();
                    loadGPPanelInMainWindow(this, _infoPanel, "Info");
                }


                //set base run panel on _runPAnel
                _baseRunPanel = _runPanel;

                if (_runPanel != null)
                {
                    
                    this._mainGPFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);
                    _runPanel.ResetSolution();
                    _runPanel.UpdateChartDataPoint(_experimentPanel.GetOutputValues(), false);
                    _isFileDirty = true;
                }



                if (_funDefinit != null)
                    _funDefinit.btnFinishAnalFun.Click += btnFinishAnalFun_Click;



                if (_setPanel != null)
                    _setPanel.ResetSolution += _setPanel_ResetSolution;

                
            }


        }
        
		/// <summary>
		/// Load page in to GP Main Window. GP Model consis of several specific panels
		/// </summary>
		/// <param name="mainWnd"></param>
		/// <param name="panel"></param>
		/// <param name="TabItemCaption"></param>
        void loadGPPanelInMainWindow(Form mainWnd, UserControl panel, string TabItemCaption)
        {
            try
            {
                //helpers

                int treeCtrlWidth = 0;// logoPictureBox.Width;
                int gapX = 2;
                int gapY = 5;
                int ribbonHeight = ribbonPanel.Height;

                if (tabControl1 == null)
                {
                    //creating TabControl
                    this.tabControl1 = new System.Windows.Forms.TabControl();
                    this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                    this.tabControl1.Name = "tabControl1";
                    this.tabControl1.SelectedIndex = 0;
                    this.tabControl1.Location = new System.Drawing.Point(treeCtrlWidth+gapX, ribbonHeight+gapY);
                    this.tabControl1.Size = new System.Drawing.Size(this.Width - treeCtrlWidth - 10 * gapX, this.Height - ribbonHeight - statusStrip1.Height - 9 * gapY);
                    this.tabControl1.TabIndex = 2;
                    this.Controls.Add(this.tabControl1);
                }
                else
                {
                    tabControl1.Visible = true;
                    panel2.Visible = false;
                }

                panel2.Visible = false;
                
                //ataching the panel in tabPage and page to tabctrl
                int tabItemHeight = 25;
                TabPage tbfunctionsPage = new TabPage();
                this.tabControl1.Controls.Add(tbfunctionsPage);
                tbfunctionsPage.Controls.Add(panel);

                panel.Anchor = ((System.Windows.Forms.AnchorStyles)
                                ((System.Windows.Forms.AnchorStyles.Top |
                                  System.Windows.Forms.AnchorStyles.Bottom |
                                  System.Windows.Forms.AnchorStyles.Left |
                                  System.Windows.Forms.AnchorStyles.Right)));

                panel.Size = new Size(tabControl1.Width - 2*gapX, tabControl1.Height - tabItemHeight);
                panel.Location = new System.Drawing.Point(-3, 0);

                //define tabpage
                tbfunctionsPage.Name = "tabPage" + tabControl1.TabPages.Count.ToString();
                tbfunctionsPage.Padding = new System.Windows.Forms.Padding(2);
                tbfunctionsPage.TabIndex = tabControl1.TabPages.Count;
                tbfunctionsPage.Text = TabItemCaption;
                tbfunctionsPage.UseVisualStyleBackColor = true;

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        private void removeGPPanel(string p)
        {
            if (tabControl1 != null)
            {
                int index=-1;
                for(int i=0;i<tabControl1.Controls.Count; i++)
                {
                    if (tabControl1.Controls[i].Text == p)
                        index = i;
                }
                if(index>=0)
                    tabControl1.Controls.RemoveAt(index);
            }
                
        }
        /// <summary>
        /// About 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnInfo_Click(object sender, EventArgs e)
        {
            AboutGPdotNET dlg = new AboutGPdotNET();
            dlg.ShowDialog();
        }

        /// <summary>
        /// Close command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// running solver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_btnClicked(object sender, EventArgs e)
        {
            if (_runningEngine > 0)
            {
                MessageBox.Show("Two Engine cannot run at the same time.");
                return;
            }
            if ( _optimizePanel!=null || _resultPanel != null || _tspPanel != null || _alocPanel != null || _runANNPanel != null)
            {
                bool res = false;
                if (_resultPanel != null)
                    res= _resultPanel.HasPrevSoluton();
                else if (_runANNPanel != null)
                    res = _runANNPanel.HasPrevSoluton();
                else if (_tspPanel != null)
                    res = _tspPanel.HasPrevSoluton();
                else if (_optimizePanel != null)
                    res = _optimizePanel.HasPrevSoluton();
                else
                    res = _alocPanel.HasPrevSoluton();

                if (res)
                {
                    if (DialogResult.Yes == MessageBox.Show("Would you like to reset previous solution?", Properties.Resources.SR_ApplicationName, MessageBoxButtons.YesNo))
                    {
                        if (_runPanel != null)
                            _runPanel.ResetSolution();

                        if (_runANNPanel != null)
                        {
                            _runANNPanel.ResetSolution();
                            if (_mainANNFactory != null)
                                _mainANNFactory.ResetFactory();
                        }


                        if (_tspPanel != null)
                            _tspPanel.ResetSolution();

                        if (_alocPanel != null)
                            _alocPanel.ResetSolution();

                        if (_resultPanel != null)
                            _resultPanel.ResetSolution();

                        if (_predictionPanel != null)
                            _predictionPanel.ResetSolution();
                        
                        if (_mainGPFactory != null)
                            _mainGPFactory.ResetSolution();
                        if (_secondFactory != null)
                            _secondFactory.ResetSolution();
                       
                        
                    }
                    //else if (_runANNPanel != null)
                    //{
                    //    _runANNPanel.ContinueSearching();
                    //}
                }

                runProgram();
            }
           
            
        }
       
        /// <summary>
        /// stop alo 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopGP(object sender, EventArgs e)
        {
            stopProgram();
        }
       
        /// <summary>
        /// optimize GP model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optimizeGP(object sender, EventArgs e)
        {
            if (_runningEngine > 0)
            {
                MessageBox.Show("Two Engine cannot run at the same time.");
                return;
            }
            if (_optimizePanel != null)
            {
                if (_optimizePanel.HasPrevSoluton())
                {
                    if (DialogResult.Yes == MessageBox.Show("Would you like to reset previous solution?",Properties.Resources.SR_ApplicationName,MessageBoxButtons.YesNo))
                    {
                        if (_optimizePanel != null)
                            _optimizePanel.ResetSolution();
                        if (_secondFactory != null)
                            _secondFactory.ResetSolution();
                    }
                }
            }
            optimizeProgram();
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void GPdotNETApp_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) ||
                   e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        
    }
}
