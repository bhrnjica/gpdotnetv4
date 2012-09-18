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
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using GPdotNET.Engine;
using GPdotNET.Core;
using System.Threading;
using GPdotNET.Tool.Common;
using GPdotNET.Tool.Common.GUI;

namespace GPdotNET.App
{
    public partial class MainWindow
    {
       
        /// <summary>
        /// Closing the current model. Save data ...
        /// </summary>
        /// <returns></returns>
        private bool CloseCurrentModel()
        {
            if (tabControl1 == null || tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
                return true;

            var retVal = System.Windows.Forms.DialogResult.No;
            if(_isFileDirty)
                retVal = MessageBox.Show("Save before close?", "GPdotNET", MessageBoxButtons.YesNoCancel);

            if (retVal == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (_setPanel != null)
                        rbtnSaveModel_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else if (retVal == System.Windows.Forms.DialogResult.Cancel)
                return false;

            if (tabControl1 != null && tabControl1.TabPages != null)
            {
                tabControl1.TabPages.Clear();
                tabControl1.Visible = false;
                panel2.Visible = true;
            }

            if (_dataPanel != null)
            {
                _dataPanel.Dispose();
                _dataPanel = null;
            }
            if (_funPanel != null)
            {
                _funPanel.Dispose();
                _funPanel = null;
            }
            if (_setPanel != null)
            {
                _setPanel.Dispose();
                _setPanel = null;
            }
            if (_funDefinit != null)
            {
                _funDefinit.Dispose();
                _funDefinit = null;
            }
            if (_runPanel != null)
            {
                _runPanel.Dispose();
                _runPanel = null;
            }
            if (_optimizePanel != null)
            {
                _optimizePanel.Dispose();
                _optimizePanel = null;
            }
            if (_resultPanel != null)
            {
                _resultPanel.Dispose();
                _resultPanel = null;
            }
            if (_predictionPanel != null)
            {
                _predictionPanel.Dispose();
                _predictionPanel = null;
            }

            return true;
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_runningEngine > 0)
            {
                MessageBox.Show("Stop program running, before you exit!", "GPdotNET");
                e.Cancel = true;
                return;
            }

            if (_setPanel == null)
                return;

            var retVal = System.Windows.Forms.DialogResult.No;
            if(_isFileDirty)
                retVal=MessageBox.Show("Save before exit?", "GPdotNET", MessageBoxButtons.YesNoCancel);

            if (retVal == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if(_setPanel!=null)
                        rbtnSaveModel_Click(null, null);

                    e.Cancel = false;
                    base.OnClosing(e);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    e.Cancel = true;
                    return;
                }
            }
            else if (retVal == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = false;
                base.OnClosing(e);
            }
            else
                e.Cancel = true;
        }
        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Load(object sender, EventArgs e)
        {
            Text = "GPdotNET v2.0 - BETA 2";
            LoadToolbars();
            InitStartPanel();

        }

        /// <summary>
        /// 
        /// </summary>
        private void InitStartPanel()
        {
            pbLink1.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.hyperlink_32.png");
            pbLink2.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.hyperlink_32.png");
            pbLink3.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.hyperlink_32.png");

            pbNew.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.newgp48.png");
            pbOpen.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.opengp48.png");

            pbLogoHor.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.gpdotnetlogo_hor.png");

           // pbSep1.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.sep_32.png");
           // pbSep1.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.sep_32.png");
        }

        /// <summary>
        /// 
        /// </summary>
        void LoadToolbars()
        {
            rbtnNewModel.img = Utility.LoadImageFromName("GPdotNET.App.Resources.newgp48.png");
           // rbtnNewModel.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.newgp48g.png");
            rbtnNewModel.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnNewModel.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnOpenModel.img = Utility.LoadImageFromName("GPdotNET.App.Resources.opengp48.png");
          //rbtnOpenModel.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnOpenModel.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnOpenModel.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnSaveModel.img = Utility.LoadImageFromName("GPdotNET.App.Resources.savegp48.png");
          //rbtnSaveModel.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnSaveModel.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnSaveModel.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnSaveAsModel.img = Utility.LoadImageFromName("GPdotNET.App.Resources.saveasgp48.png");
          //rbtnSaveAsModel.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnSaveAsModel.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnSaveAsModel.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnCloseModel.img = Utility.LoadImageFromName("GPdotNET.App.Resources.closegp48.png");
            //rbtnCloseModel.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnCloseModel.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnCloseModel.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnExportModel.img = Utility.LoadImageFromName("GPdotNET.App.Resources.exportmodel48.png");
            //rbtnExportModel.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnExportModel.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnExportModel.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");


            rbtnExportTest.img = Utility.LoadImageFromName("GPdotNET.App.Resources.exporttest48.png");
            //rbtnExportTest.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnExportTest.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnExportTest.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnInfo.img = Utility.LoadImageFromName("GPdotNET.App.Resources.about48.png");
            //rbtnInfo.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnInfo.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnInfo.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnExit.img = Utility.LoadImageFromName("GPdotNET.App.Resources.exit48.png");
            //rbtnExit.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnExit.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnExit.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnRun.img = Utility.LoadImageFromName("GPdotNET.App.Resources.runmodel48.png");
            //rbtnRun.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnRun.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnRun.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnStop.img = Utility.LoadImageFromName("GPdotNET.App.Resources.stopmodel48.png");
            //rbtnStop.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnStop.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnStop.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnOptimize.img = Utility.LoadImageFromName("GPdotNET.App.Resources.optimizemodel48.png");
            //rbtnOptimize.img_back = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnOptimize.img_click = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnOptimize.img_on = Utility.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");


            //events
            this.rbtnNewModel.Click += new System.EventHandler(this.rbtnNewModel_Click);
            this.rbtnOpenModel.Click += new System.EventHandler(this.rbtnOpenModel_Click);
            this.rbtnSaveModel.Click += new System.EventHandler(this.rbtnSaveModel_Click);
            this.rbtnSaveAsModel.Click += new System.EventHandler(this.rbtnSaveAsModel_Click);
            this.rbtnCloseModel.Click += new System.EventHandler(this.rbtnCloseModel_Click);
            this.rbtnExportModel.Click += new System.EventHandler(this.rbtnExportModel_Click);
            this.rbtnExportTest.Click += new System.EventHandler(this.rbtnExportTest_Click);
           

        }

        /// <summary>
        /// GP NEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNewModel_Click(object sender, EventArgs e)
        {
            if (!CloseCurrentModel())
                return;

            TreeView treeView1 = new TreeView();
            NewGPModel newGPModelDlg = new NewGPModel();
            if (newGPModelDlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            LoadModelWizard(newGPModelDlg.ModelType);

            txtStatusMessage.Text = "Ready!";
        }
       
        /// <summary>
        /// GP OPen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnOpenModel_Click(object sender, EventArgs e)
        {
           
            OpenFromFile();

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// GP Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnSaveModel_Click(object sender, EventArgs e)
        {
           if (_filePath == "")
              _filePath= CommonMethods.GetFileFromSaveDialog();

           if (SaveToFile(_filePath))
               txtStatusMessage.Text = "File is successfuly saved!";
        }

        /// <summary>
        /// GP Close current model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnCloseModel_Click(object sender, EventArgs e)
        {
            CloseCurrentModel();

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// GP Save As
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnSaveAsModel_Click(object sender, EventArgs e)
        {
            _filePath = CommonMethods.GetFileFromSaveDialog();
            if (SaveToFile(_filePath))
                txtStatusMessage.Text = "File is successfuly saved!";
        }

        /// <summary>
        /// Export model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnExportModel_Click(object sender, EventArgs e)
        {
            bool forceToCSV = false;
            txtStatusMessage.Text = "Ready!";
            if (_gpFactory == null || Globals.gpterminals.TrainingData==null)
                return;

            ExportDialog dlg = new ExportDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            if (dlg.SelectedOption == -1)
                forceToCSV = true;
            else if (dlg.SelectedOption == 1)
                forceToCSV = false;
            else
                forceToCSV = true;
            
            
            
            //Sample code for running agains MONO or .NET
            Type t = Type.GetType("Mono.Runtime");




            if (t != null || forceToCSV)//You are running with the Mono VM
            {
                var ch = _gpFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = CommonMethods.GetFileFromSaveDialog("CSV File Format", "*.csv");
                if (string.IsNullOrEmpty(strPath))
                    return;
                Utility.ExportToCSV(Globals.gpterminals.TrainingData,
                                     Globals.gpterminals.NumVariables,
                                     Globals.gpterminals.NumConstants,
                                     ch.expressionTree, strPath);
            }
            else//"You are running on .net"
            {
                var ch = _gpFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = CommonMethods.GetFileFromSaveDialog("Excel File Format", "*.xlsx");
                if (string.IsNullOrEmpty(strPath))
                    return;
                Utility.ExportToExcel(Globals.gpterminals.TrainingData,
                                      Globals.gpterminals.NumVariables,
                                      Globals.gpterminals.NumConstants,
                                      ch.expressionTree, strPath);
            }
        }

        /// <summary>
        /// Export test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnExportTest_Click(object sender, EventArgs e)
        {
           
            bool forceToCSV = false;
            txtStatusMessage.Text = "Ready!";
            if (Globals.gpterminals.TestingData == null)
            {
                MessageBox.Show("There is no Testing Data!");
                return;
            }

            ExportDialog dlg = new ExportDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            if (dlg.SelectedOption == -1)
                forceToCSV = true;
            else if (dlg.SelectedOption == 1)
                forceToCSV = false;
            else
                forceToCSV = true;

            //Sample code for running agains MONO or .NET
            Type t = Type.GetType("Mono.Runtime");


            if (t != null || forceToCSV)//You are running with the Mono VM
            {
                var ch = _gpFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = CommonMethods.GetFileFromSaveDialog("CSV File Format", "*.csv");
                if (string.IsNullOrEmpty(strPath))
                    return;
                Utility.ExportToCSV(Globals.gpterminals.TestingData,
                                     Globals.gpterminals.NumVariables,
                                     Globals.gpterminals.NumConstants,
                                     ch.expressionTree, strPath);
            }
            else//"You are running on .net"
            {


                var ch = _gpFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = CommonMethods.GetFileFromSaveDialog("Excel File Format", "*.xlsx");
                if (string.IsNullOrEmpty(strPath))
                    return;
                Utility.ExportToExcel(Globals.gpterminals.TestingData,
                                      Globals.gpterminals.NumVariables,
                                      Globals.gpterminals.NumConstants,
                                      ch.expressionTree, strPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void portalLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gpdotnet.codeplex.com");
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://bhrnjica.wordpress.com/gpdotnet");
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\GPdotNET_v2_User_Guide.pdf";
            System.Diagnostics.Process.Start(strPath);
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rbtnNewModel_Click(null, null);
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rbtnOpenModel_Click(null, null);
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simple_fun_mod_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\simple_case.gpa";

            Open(strPath);

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void surfaceRoughnessPrediction_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\surface_roughness_prediction.gpa";

            Open(strPath);

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weldHardness_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\hardness_modeling.gpa";

            Open(strPath);

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void global_Optimization(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\simple_optimization.gpa";

            Open(strPath);

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cnc_params_Optimization(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\life_tool_opt.gpa";

            Open(strPath);

            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void microsoft_StockModeling(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\montly_msft_2003_2012.gpa";

            Open(strPath);

            txtStatusMessage.Text = "Ready!";
        }


        /// <summary>
        /// 
        /// </summary>
        private bool PrepareGP(bool brunFactory = true)
        {
            //set file as dirty 
            _isFileDirty = true;

            //Define GP parameters
            var gpp= _setPanel.GetParameters();
            gpp.algorithmType = Algorithm.GP;
            _resultPanel.SetConstants(_setPanel.Constants);

            //Define GP Terminals
            GPTerminalSet gpTSet =  GPModelGlobals.GenerateTerminalSet(_dataPanel.Training, //training data
                                                                        _setPanel.Constants, // random constant
                                                                        _dataPanel.Testing); // data for testing and prediction
            //Deine GP function set
            GPFunctionSet gpFSet = new GPFunctionSet();
            var fn = _funPanel.GPFunctions.Where(f => f.Selected == true).ToList();
            gpFSet.SetFunction(fn);
            
            //Set GP Terminals
            var ter = gpTSet.GetTerminals();
            gpFSet.SetTerimals(ter);

            if (_optimizePanel != null)
                _optimizePanel.FillTerminalBounds(ter.Where(x=>x.Name.Contains("X")).Select(x=>x.Name).ToList());


            if (brunFactory)
            {
                //set indicator to 1 means GP is running
                _runningEngine = 1;

                //Call prepare algorithm
                _gpFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
            }
            else
            {
                _gpFactory.SetTerminalSet(gpTSet);
                _gpFactory.SetFunctionSet(gpFSet);
                _gpFactory.SetGPParameter(gpp);
            }
            return true;

           
        }

        /// <summary>
        /// 
        /// </summary>
        private bool PrepareGA(bool brunFactory=true)
        {
            //set file as dirty 
            _isFileDirty = true;

            //GA parameters
            GPParameters gpp;

            if (_gpFactory != null)
            {
                gpp = _gpFactory.GetParameters();
                if (gpp == null)
                {
                    MessageBox.Show("Before optimization, GPModel must be created!");
                    return false;
                }
            }
            else
            {
                gpp = _setPanel.GetParameters();
                if (gpp == null)
                {
                    MessageBox.Show("Error in generating GP parameters!");
                    return false;
                }
            }

            var fitnes= new AnalyticFunctionFitness();
            gpp.GPFitness = fitnes;
            gpp.algorithmType = Algorithm.GA;
            gpp.chromosomeKind = GAChromosome.Continue;


            //Define function to be optimize. We have two choices 1. optimize GP Model 2. Optimize Analytic function
            if (_GPModel == GPModelType.SymbolicRegressionWithOptimization)
            {
                var res=_resultPanel.GetGPModel();
                if (res == null)
                {
                    MessageBox.Show("Before optimization, GPModel must be created!");
                    return false;
                }
                fitnes.FunToOptimize =res;
            }
            else
                fitnes.FunToOptimize = _funDefinit.TreeNodeToGPNode();

            //Set what kind of optimization needs to be performed
            if(_optimizePanel!=null)
                fitnes.IsMinimize = _optimizePanel.IsMinimum();

            //Declare terminals
            GPTerminalSet gpTSet=null;
            GPFunctionSet gpFSet=null;
            List<GPTerminal> tt = null;

            if (_GPModel == GPModelType.AnaliticFunctionOptimization)
            {
                tt = _funDefinit.GetTerminals();

                //Define TerminalSet
                gpTSet = new GPTerminalSet();
                gpTSet.RowCount = 1;
                gpTSet.NumVariables = tt.Count(x => x.IsConstant == false);
                gpTSet.NumConstants = tt.Count(x => x.IsConstant == true);

                //define FunctionSet by loading functions and set max and min values for terminals
                gpFSet = new GPFunctionSet();
                gpFSet.SetFunction(CommonMethods.LoadFunctionsfromXMLFile());
                gpFSet.SetTerimals(tt);

                //Define terminals 
                //define Training data in TerminalSet
                gpTSet.SingleTrainingData = new double[gpTSet.NumVariables + gpTSet.NumConstants + 1];

                int countVars = gpTSet.NumVariables + gpTSet.NumConstants;
                for (int i = gpTSet.NumVariables; i < countVars; i++)
                    gpTSet.SingleTrainingData[i] = tt[i].fValue;
            }
            else
            {
                //Declare terminals
                gpTSet = _gpFactory.GetTerminalSet();
                gpFSet = _gpFactory.GetFunctionSet(); 
                tt =gpFSet.GetTerminals();

                //Define terminals 
                //define Training data in TerminalSet
                gpTSet.SingleTrainingData = new double[gpTSet.NumVariables + gpTSet.NumConstants + 1];

                int countVars = gpTSet.NumVariables + gpTSet.NumConstants;
                for (int i = gpTSet.NumVariables; i < countVars; i++)
                    gpTSet.SingleTrainingData[i] = gpTSet.TrainingData[0][i];
            }

            var bounds = _optimizePanel.GetTerminalBounds();
            if (bounds == null)
            {
                MessageBox.Show("Min and Max values for input variables are not defined!!");
                return false;
            }
            for (int i = 0; i < tt.Count; i++)
            {
                var ter=tt[i];
                for (int j = 0; j < bounds.Count; j++)
                {
                    if (bounds[j].Name == ter.Name)
                    {
                        if (bounds[j].maxValue <= bounds[j].minValue)
                        {
                            MessageBox.Show("Min and Max values for input variables are not defined!!");
                            return false;
                        }
                        ter.maxValue = bounds[j].maxValue;
                        ter.minValue = bounds[j].minValue;
                    }
                }
            }

            
            if (brunFactory)
            {
                //set indicator to 1 means GP is running
                _runningEngine = 2;

                //Call prepare algorithm
                _gaFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
            }
            else//wne we need just init factory but not to b e run
            {
                _gaFactory.SetTerminalSet(gpTSet);
                _gaFactory.SetFunctionSet(gpFSet);
                _gaFactory.SetGPParameter(gpp);
            }
            return true;
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void runProgram()
        {
            if (_GPModel == Tool.Common.GPModelType.AnaliticFunctionOptimization)
            {
                MessageBox.Show("Press Optimize for Analytic optimization!");
                return;
            }

            if(PrepareGP())
                _run(_runPanel);
        }

        /// <summary>
        /// 
        /// </summary>
        private void optimizeProgram()
        {
            if (_GPModel == Tool.Common.GPModelType.AnaliticFunctionOptimization || _GPModel == Tool.Common.GPModelType.SymbolicRegressionWithOptimization)
            {
               
               if(PrepareGA())
                _run(_optimizePanel, false);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void _run(BaseRunPanel pn, bool isGPFactory=true)
        {
            try
            {
                float tv = pn.TerminationValue;
                int tt = pn.TerminationType;

                // Create the thread object, passing in the StartEvolution method
                // via a ThreadStart delegate.
                Thread oThread ;

                if(isGPFactory)
                    oThread = new Thread(() => _gpFactory.StartEvolution(tv, tt));
                else
                    oThread = new Thread(() => _gaFactory.StartEvolution(tv, tt));

                //before we start update GUI
                UpdateGUI(1);

                // Start the thread
                oThread.Start();

                // Spin for a while waiting for the started thread to become
                // alive:
                while (!oThread.IsAlive) ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 

        }

        /// <summary>
        /// Main event for reporting about GP 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> event argument</param>
        void gpFactory_ReportEvolution(object sender, Engine.ReportCurrentEvolutionEventArgs e)
        {
           

            //set indicator to stped
            if (e.ReportType == ReportGPType.Finished)
                _runningEngine = 0;

            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(new Action(() => ReportEvolution(e)));
            }
            else
            {
               ReportEvolution(e);
            }
            
        }

        private void ReportEvolution(ReportCurrentEvolutionEventArgs e)
        {
            UpdateGUI((int)e.ReportType);

            if (_runPanel != null && _runningEngine==1)
                _runPanel.ReportProgress(e.CurrentEvolution, e.AverageFitness, e.BestChromosome , (int)e.ReportType);

            if (_optimizePanel != null && _runningEngine==2)
                _optimizePanel.ReportProgress(e.CurrentEvolution, e.AverageFitness, e.BestChromosome , (int)e.ReportType);

            if (_resultPanel != null && _runningEngine == 1)
                _resultPanel.ReportProgress(e.CurrentEvolution, e.AverageFitness, e.BestChromosome , (int)e.ReportType);

            if (_predictionPanel != null && _runningEngine == 1)
                _predictionPanel.ReportProgress(e.CurrentEvolution, e.AverageFitness, e.BestChromosome as GPChromosome, (int)e.ReportType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRunning"></param>
        private void UpdateGUI(int runType)
        {
            //update ribbon buttons
            if (runType < 3 && _bgpRuning != runType)
            {
                gpModelPanel.Enabled = false;
                gpExportPanel.Enabled = false;
                gpCommonPanel.Enabled = false;
                rbtnRun.Enabled = false;
                rbtnStop.Enabled = true;
                rbtnOptimize.Enabled = false;
                _bgpRuning = 2;
                
                //panels
                //panels
                if (_dataPanel != null)
                    _dataPanel.EnableCtrls(false);
                if (_setPanel != null)
                    _setPanel.EnableCtrls(false);
                if (_funPanel != null)
                    _funPanel.EnableCtrls(false);
                if (_funDefinit != null)
                    _funDefinit.EnableCtrls(false);
                if (_runPanel != null)
                    _runPanel.EnableCtrls(false);
                if (_resultPanel != null)
                    _resultPanel.EnableCtrls(false);
                if (_optimizePanel != null)
                    _optimizePanel.EnableCtrls(false);


            }
            else if (runType==3 && _bgpRuning != runType)
            {
                gpModelPanel.Enabled = true;
                gpExportPanel.Enabled = true;
                gpCommonPanel.Enabled = true;
                rbtnRun.Enabled = true;
                rbtnStop.Enabled = false;
                rbtnOptimize.Enabled = true;
                _bgpRuning = 0;

                //panels
                if (_dataPanel != null)
                    _dataPanel.EnableCtrls(true);
                if (_setPanel != null)
                    _setPanel.EnableCtrls(true);
                if (_funPanel != null)
                    _funPanel.EnableCtrls(true);
                if (_funDefinit != null)
                    _funDefinit.EnableCtrls(true);
                if (_runPanel != null)
                    _runPanel.EnableCtrls(true);
                if (_resultPanel != null)
                    _resultPanel.EnableCtrls(true);
                if (_optimizePanel != null)
                    _optimizePanel.EnableCtrls(true);
            }

        }

       /// <summary>
       /// 
       /// </summary>
       private void stopProgram()
       {
           _bgpRuning = 0;
           GPFactory.StopEvolution = true;
       }     

	}
}
