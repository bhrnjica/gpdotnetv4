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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GPdotNET.Tool.Common;
using GPdotNET.Engine;
using GPdotNET.Core;

namespace GPdotNET.App
{
    public partial class MainWindow : Form
    {
        //base class for all running panel
        BaseRunPanel            _baseRunPanel = null;

        InfoPanel               _infoPanel;
        DataPanel               _dataPanel;
        FunctionPanel           _funPanel;
        SettingsPanel           _setPanel;
        AnaliticFunctionDef     _funDefinit;
        RunPanel                _runPanel;
        OptimizePanel           _optimizePanel;
        ResultPanel             _resultPanel;
        PreditionPanel          _predictionPanel;
        TSPRunPanel             _tspPanel;
        ALOCRunPanel            _alocPanel;

        GPModelType             _GPModel;

        GPFactory               _mainFactory;
        GPFactory               _secondFactory;
        //GPFactory               _tspFactory;
       // GPFactory               _alocFactory;
        int                     _runningEngine = 0;

        int                     _bgpRuning = 0;

        string                  _filePath = "";
        bool                    _isFileDirty=false;
        ContextMenuStrip        _contextMenuStrip1;

        string                  _appName = "GPdotNET v3.0 BETA 1";
        //Open document through cmd line
        public string[] CmdLineParam
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
            _contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            Load += new EventHandler(MainWindow_Load);
            this.Icon = Utility.LoadIconFromName("GPdotNET.App.Resources.gpdotnet_ico48.ico");
            this.logoPictureBox.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.gp256V.png");
            
            
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

        /// <summary>
        /// Resets programs
        /// </summary>
        private void ResetProgram()
        {
            //unscribe to events
            if (_mainFactory != null)
                this._mainFactory.ReportEvolution -= new EvolutionHandler(gpFactory_ReportEvolution);
            
            if (_secondFactory != null)
                this._secondFactory.ReportEvolution -= new EvolutionHandler(gpFactory_ReportEvolution);

            //if (_tspFactory != null)
            //    this._tspFactory.ReportEvolution -= new Engine.EvolutionHandler(gpFactory_ReportEvolution);

            //if (_alocFactory != null)
            //    this._alocFactory.ReportEvolution -= new Engine.EvolutionHandler(gpFactory_ReportEvolution);
            
            _mainFactory = null;
            _secondFactory = null;
            //_tspFactory = null;
            //_alocFactory = null;
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
                case GPModelType.SymbolicRegression:
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

                        this._mainFactory = new GPFactory();
                        this._mainFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        //set base run panel on _runPAnel
                        _baseRunPanel = _runPanel;

                    }
                    break;
                case GPModelType.SymbolicRegressionWithOptimization:
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

                        this._mainFactory = new GPFactory();
                        this._mainFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        this._secondFactory = new GPFactory();
                        this._secondFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);


                        //set base run panel on _runPAnel
                        _baseRunPanel = _runPanel;
                    }
                    break;
                case GPModelType.TimeSeries:
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

                        this._mainFactory = new GPFactory();
                        this._mainFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        //set base run panel on _runPAnel
                        _baseRunPanel = _runPanel;

                    }
                    break;
                case GPModelType.AnaliticFunctionOptimization:
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

                        this._mainFactory = new GPFactory();
                        this._mainFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        //set base run panel on _TSPPanel
                        _baseRunPanel = _tspPanel;
                    }
                    break;
                case GPModelType.ALOC:
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

                        this._mainFactory = new GPFactory();
                        this._mainFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        //set base run panel on _TSPPanel
                        _baseRunPanel = _alocPanel;
                    }
                    break;
                case GPModelType.Transport:
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

                        this._mainFactory = new GPFactory();
                        this._mainFactory.ReportEvolution += new EvolutionHandler(gpFactory_ReportEvolution);

                        _infoPanel = new InfoPanel();
                        loadGPPanelInMainWindow(this, _infoPanel, "Info");

                        //set base run panel on _TSPPanel
                        _baseRunPanel = _alocPanel;
                    }
                    break;
                default:
                    break;
            }

            //Events from datapanel about loading dat
            if (_dataPanel != null)
               _dataPanel.DataLoaded += _dataPanel_DataLoaded; 


            if (_funDefinit != null)
               _funDefinit.btnFinishAnalFun.Click += btnFinishAnalFun_Click;
                    
            if (_dataPanel != null)
               _dataPanel.DataPredictionLoaded += _dataPanel_DataPredictionLoaded;

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
            if (_mainFactory != null)
                _mainFactory.ResetSolution();
            if (_secondFactory != null)
                _secondFactory.ResetSolution();
            //if (_tspFactory != null)
            //    _tspFactory.ResetSolution();
            //if (_alocFactory != null)
            //    _alocFactory.ResetSolution();

            if (_runPanel != null)
                _runPanel.ResetSolution();
            if (_resultPanel != null)
                _resultPanel.ResetSolution();
            if (_predictionPanel != null)
                _predictionPanel.ResetSolution();
            if (_mainFactory != null)
                _mainFactory.ResetSolution();
            if (_mainFactory != null)
                _mainFactory.ResetSolution();
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
		/// Load page in to GP Main Window. GP Model consis of several specific panels
		/// </summary>
		/// <param name="mainWnd"></param>
		/// <param name="panel"></param>
		/// <param name="TabeItemCaption"></param>
        void loadGPPanelInMainWindow(Form mainWnd, UserControl panel, string TabeItemCaption)
        {
            try
            {
                //helpers
                
                int treeCtrlWidth = logoPictureBox.Width;
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
                tbfunctionsPage.Text = TabeItemCaption;
                tbfunctionsPage.UseVisualStyleBackColor = true;

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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
        /// running GP algoritam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void run_GP(object sender, EventArgs e)
        {
            if (_runningEngine > 0)
            {
                MessageBox.Show("Two Engine cannot run at the same time.");
                return;
            }
            if (_resultPanel != null || _tspPanel != null || _alocPanel != null)
            {
                bool res = false;
                if (_resultPanel != null)
                    res= _resultPanel.HasPrevSoluton();
                else if (_tspPanel != null)
                    res = _tspPanel.HasPrevSoluton();
                else
                    res = _alocPanel.HasPrevSoluton();

                if (res)
                {
                    if (DialogResult.Yes == MessageBox.Show("Would you like to reset previous solution?", Properties.Resources.SR_ApplicationName, MessageBoxButtons.YesNo))
                    {
                        if (_runPanel != null)
                            _runPanel.ResetSolution();

                        if (_tspPanel != null)
                            _tspPanel.ResetSolution();

                        if (_alocPanel != null)
                            _alocPanel.ResetSolution();

                        if (_resultPanel != null)
                            _resultPanel.ResetSolution();

                        if (_predictionPanel != null)
                            _predictionPanel.ResetSolution();
                        
                        if (_mainFactory != null)
                            _mainFactory.ResetSolution();
                        if (_secondFactory != null)
                            _secondFactory.ResetSolution();
                        //if (_tspFactory != null)
                        //    _tspFactory.ResetSolution();
                        //if (_alocFactory != null)
                        //    _alocFactory.ResetSolution();
                        
                    }
                }
            }
            runProgram();
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
