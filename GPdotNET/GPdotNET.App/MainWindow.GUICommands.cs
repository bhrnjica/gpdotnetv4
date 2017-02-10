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
using System.IO;
using System.Threading.Tasks;
using GPdotNET.Util;
using GPdotNET.Core.Experiment;
using ClosedXML.Excel;
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
            //unscribe to events
            if(_mainGPFactory!=null)
                this._mainGPFactory.ReportEvolution -= new EvolutionHandler(gpFactory_ReportEvolution);
            if(_secondFactory!=null)
                this._secondFactory.ReportEvolution -= new EvolutionHandler(gpFactory_ReportEvolution);
            if (_mainANNFactory != null)
                this._mainANNFactory.ReportIteration -= new EvolutionHandler(annFactory_ReportIteration);
           

            if (tabControl1 != null && tabControl1.TabPages != null)
            {
                tabControl1.TabPages.Clear();
                tabControl1.Visible = false;
                panel2.Visible = true;
            }


            if (_dataPanel != null)
            {
                _dataPanel.DataLoaded -= _dataPanel_DataLoaded;
                _dataPanel.DataPredictionLoaded -= _dataPanel_DataPredictionLoaded;
                _dataPanel.Dispose();
                _dataPanel = null;
            }

            if (_experimentPanel != null)
            {
                _experimentPanel.DataLoaded -= _experimentPanel_DataLoaded;
                _experimentPanel.DataPredictionLoaded -= _experimentalPanel_DataPredictionLoaded;
                _experimentPanel.Dispose();
                _experimentPanel = null;
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
            if (_setANNPanel != null)
            {
                _setANNPanel.Dispose();
                _setANNPanel = null;
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
            if (_runANNPanel != null)
            {
                _runANNPanel.Dispose();
                _runANNPanel = null;
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
            if (_funDefinit != null)
            {
                _funDefinit.btnFinishAnalFun.Click -= btnFinishAnalFun_Click;
                _funDefinit.Dispose();
                _funDefinit = null;
            }
            if (_tspPanel != null)
            {
                _tspPanel.Dispose();
                _tspPanel = null;
            }

            _baseRunPanel = null; 

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
                    if (_setPanel != null)
                    {
                        var mi = new ToolStripButton();
                        mi.Text = "Save";
                        ToolStripItemClickedEventArgs ee = new ToolStripItemClickedEventArgs(mi);
                       
                        rbtnSaveModel_ItemClicked(null, ee);
                    }

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
            Text = _appName;
            LoadToolbars();
            //loading file through cmd line (double click from WinExplorer)
            if (CmdLineParam != null)
            {
                
                if (CmdLineParam.Length > 0)
                {
                    if(Open(CmdLineParam[0]))
                     return;
                }
            }
           
            InitStartPanel();

        }

        /// <summary>
        /// 
        /// </summary>
        private void InitStartPanel()
        {
            pbLink1.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.hyperlink_32.png");
            pbLink2.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.hyperlink_32.png");
            pbLink3.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.hyperlink_32.png");

            pbNew.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.newgp48.png");
            pbOpen.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.opengp48.png");

            pbLogoHor.Image = Extensions.LoadImageFromName("GPdotNET.App.Resources.gpLogo_350x134pix.png");

           // pbSep1.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.sep_32.png");
           // pbSep1.Image = Utility.LoadImageFromName("GPdotNET.App.Resources.sep_32.png");
        }

        /// <summary>
        /// 
        /// </summary>
        void LoadToolbars()
        {
            rbtnNewModel.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.newgp48.png");
            // rbtnNewModel.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.newgp48g.png");
            rbtnNewModel.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnNewModel.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnOpenModel.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.opengp48.png");
            //rbtnOpenModel.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnOpenModel.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnOpenModel.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnSaveModel.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.savegp48.png");
            //rbtnSaveModel.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnSaveModel.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnSaveModel.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnCloseModel.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.closegp48.png");
            //rbtnCloseModel.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnCloseModel.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnCloseModel.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            rbtnExportModel.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.exportmodel48.png");
            //rbtnExportModel.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnExportModel.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnExportModel.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");


         //   rbtnExportTest.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.exporttest48.png");
            //rbtnExportTest.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
        //    rbtnExportTest.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
        //    rbtnExportTest.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnInfo.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.about48.png");
            //rbtnInfo.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnInfo.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnInfo.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnExit.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.exit48.png");
            //rbtnExit.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnExit.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnExit.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnRun.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.runmodel48.png");
            //rbtnRun.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnRun.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnRun.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnStop.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.stopmodel48.png");
            //rbtnStop.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnStop.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnStop.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");



            rbtnOptimize.img = Extensions.LoadImageFromName("GPdotNET.App.Resources.optimizemodel48.png");
            //rbtnOptimize.img_back = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.backgrd.png");
            rbtnOptimize.img_click = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.click1.png");
            rbtnOptimize.img_on = Extensions.LoadImageFromName("GPdotNET.App.Resources.CustomButtonPic.on1.png");

            _contextMenuStrip1.Items.Add("Save");
            _contextMenuStrip1.Items.Add("Save as");


            //events
            this.rbtnNewModel.Click += new System.EventHandler(this.rbtnNewModel_Click);
            this.rbtnOpenModel.Click += new System.EventHandler(this.rbtnOpenModel_Click);
            this.rbtnSaveModel.Click += new System.EventHandler(this.rbtnSaveModel_Click);
            this.rbtnCloseModel.Click += new System.EventHandler(this.rbtnCloseModel_Click);
            this.rbtnExportModel.Click += new System.EventHandler(this.rbtnExportModel_Click);
        //    this.rbtnExportTest.Click += new System.EventHandler(this.rbtnExportTest_Click);
            this._contextMenuStrip1.ItemClicked += rbtnSaveModel_ItemClicked;
           

        }

        #region ToolBar Commands

        /// <summary>
        /// GP NEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnNewModel_Click(object sender, EventArgs e)
        {
            if (!CloseCurrentModel())
                return;

           // TreeView treeView1 = new TreeView();
            NewGPModel newGPModelDlg = new NewGPModel();
            if (newGPModelDlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            LoadModelWizard(newGPModelDlg.ModelType);
            string newModelName = "";
            if (_GPModel == GPModelType.TSP)
                newModelName = "TSP Problem Solver";
            else if (_GPModel == GPModelType.ANNMODEL)
                newModelName = "[New Neural Network Prediction]";
            else
                newModelName = "[New GP Prediction]";
            this.Text = string.Format("{0} - {1}", _appName, newModelName);
            txtStatusMessage.Text = "Ready!";
        }
       
        /// <summary>
        /// GP OPen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnOpenModel_Click(object sender, EventArgs e)
        {
            //if (_GPModel == GPModelType.ANNMODEL)
            //{
            //    MessageBox.Show("Opening ANN model is not implemented.");
            //    return;
            //}

            OpenFromFile();
            if (string.IsNullOrEmpty(_filePath))
                this.Text = _appName;
            else
            {
               var fName= Path.GetFileName(_filePath);
               this.Text =string.Format("{0} - {1}",_appName , fName);
            }
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// GP Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnSaveModel_Click(object sender, EventArgs e)
        {
            Point screenPoint = rbtnSaveModel.PointToScreen(new Point(rbtnSaveModel.Left, rbtnSaveModel.Bottom));
            if (screenPoint.Y + _contextMenuStrip1.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                _contextMenuStrip1.Show(rbtnSaveModel, new Point(0, -_contextMenuStrip1.Size.Height));
            }
            else
            {
                _contextMenuStrip1.Show(rbtnSaveModel, new Point(0, rbtnCloseModel.Height));
            }    
          
        }

        void rbtnSaveModel_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if(_GPModel == GPModelType.ANNMODEL)
            //{
            //    MessageBox.Show("Persisting ANN model is not implemented.");
            //    return;
            //}
            if (e.ClickedItem.Text == "Save")
            {
                if (_filePath == "")
                    _filePath = GPModelGlobals.GetFileFromSaveDialog();

                if (SaveToFile(_filePath))
                {
                    var fName = Path.GetFileName(_filePath);
                    this.Text = string.Format("{0} - {1}", _appName, fName);
                    txtStatusMessage.Text = "File is successfuly saved!";
                }
            }
            else if (e.ClickedItem.Text == "Save as")
            {
                _filePath = GPModelGlobals.GetFileFromSaveDialog();
                if (SaveToFile(_filePath))
                {
                    var fName = Path.GetFileName(_filePath);
                    this.Text = string.Format("{0} - {1}", _appName, fName);
                    txtStatusMessage.Text = "File is successfuly saved!";
                }
            }
        }

        /// <summary>
        /// GP Close current model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnCloseModel_Click(object sender, EventArgs e)
        {
            if (CloseCurrentModel())
            {
                Text = _appName;
                txtStatusMessage.Text = "Ready!";
            }
            
        }

        /// <summary>
        /// Export model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnExportModel_Click(object sender, EventArgs e)
        {
            bool forceToCSV = false;
            int selectedOption = -1;
            txtStatusMessage.Text = "Ready!";

            if ((Globals.gpterminals==null || Globals.gpterminals.TrainingData==null) &&
                _GPModel != GPModelType.ANNMODEL)
                return;

            ExportDialog dlg = new ExportDialog();
            dlg.isAnnModelExport = _GPModel == GPModelType.ANNMODEL; 
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            selectedOption = dlg.SelectedOption;
            var selectedItem = dlg.SelectedItem;

            //
            if (dlg.SelectedOption == -1)
                forceToCSV = true;
            else if (dlg.SelectedOption == 1 || selectedItem.IndexOf("Excel", StringComparison.OrdinalIgnoreCase) >= 0)
                forceToCSV = false;
            else
                forceToCSV = true;
            
            
            
            //Sample code for running agains MONO or .NET
            Type t = Type.GetType("Mono.Runtime");
            //export to R
            if (selectedItem.IndexOf("R Language", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var ch = _mainGPFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = GPModelGlobals.GetFileFromSaveDialog("Text File Format", "*.txt");
                if (string.IsNullOrEmpty(strPath))
                    return;

                if (_experimentPanel != null)//gor GPotNET v4,..
                {
                    Utility.ExportToR(_experimentPanel.Experiment,
                                      Globals.gpterminals.NumVariables,
                                      Globals.gpterminals.NumConstants,
                                      ch.expressionTree, strPath);
                }
                else { MessageBox.Show("Export is not suported!", "GPdotNET"); }
               
            }

            //export to Mathematica
            else if (selectedItem.IndexOf("Mathematica", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var ch = _mainGPFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = GPModelGlobals.GetFileFromSaveDialog("Text File Format", "*.txt");
                if (string.IsNullOrEmpty(strPath))
                    return;
                
                if (_experimentPanel != null)//gor GPotNET v4,..
                {
                    Utility.ExportToMathematica(_experimentPanel.Experiment,
                                      Globals.gpterminals.NumVariables,
                                      Globals.gpterminals.NumConstants,
                                      ch.expressionTree, strPath);
                }
                else//for GPdotNET v1, v2, v3
                {
                    Utility.ExportToMathematica(Globals.gpterminals.TrainingData,
                                            Globals.gpterminals.NumVariables,
                                            Globals.gpterminals.NumConstants,
                                            ch.expressionTree, strPath);
                }
            }

            else if (t != null || forceToCSV)//You are running with the Mono VM
            {
                var ch = _mainGPFactory.BestChromosome() as GPChromosome;
                if (ch == null)
                    return;
                string strPath = GPModelGlobals.GetFileFromSaveDialog("CSV File Format", "*.csv");
                if (string.IsNullOrEmpty(strPath))
                    return;
                Utility.ExportToCSV(Globals.gpterminals.TrainingData,
                                     Globals.gpterminals.NumVariables,
                                     Globals.gpterminals.NumConstants,
                                     ch.expressionTree, strPath);
            }
            else//"You are running on .net"
            {
                GPChromosome ch = null;
                if (_mainGPFactory !=null)
                {
                    ch = _mainGPFactory.BestChromosome() as GPChromosome;
                    if (ch == null)
                        return;
                }
               
                string strPath = GPModelGlobals.GetFileFromSaveDialog("Excel File Format", "*.xlsx");
                if (string.IsNullOrEmpty(strPath))
                    return;
                if(_experimentPanel != null)//gor GPotNET v4,..
                {
                    if(Globals.gpterminals!=null)//GP Models
                    Utility.ExportToExcel(_experimentPanel.Experiment,
                                      Globals.gpterminals.NumVariables,
                                      Globals.gpterminals.NumConstants,
                                      ch.expressionTree, strPath);
                    else//ANN models
                    {
                        var strFormula = _mainANNFactory.GenerateFormula();
                        var y1 = _mainANNFactory.CalculateModelForExport(false);
                        var y2 = _mainANNFactory.CalculateModelForExport(true);

                        Utility.ExportToExcel(_experimentPanel.Experiment, y1, y2, strPath);
                    }
                       
                }
                else//for GPdotNET v1, v2, v3
                {
                    Utility.ExportToExcel(Globals.gpterminals.NumVariables,
                                          Globals.gpterminals.NumConstants,
                                          ch.expressionTree, strPath);
                }
            }
        }
#endregion 

        #region Predefined  Samples
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void portalLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/bhrnjica/gpdotnet");
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
            string strPath = Application.StartupPath + "\\Resources_Files\\GPdotNET_User_Guide.pdf";
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
           // this.Text = string.Format("{0} - {1}", _appName, "[NewModel]");
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
        #region Regression and Aproximation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simple_fun_mod_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\simple_function_gp.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void surfaceRoughnessPrediction_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\surface_roughness_gp.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
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
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
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
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
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
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        #endregion

        #region TSP, TP AP
        /// <summary>
        /// Assignment problem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\as_demo.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        /// <summary>
        /// Transportation problems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\ts_demo.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\tsp_131_cities.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        /// <summary>
        /// TSP Problem sample
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\tsp_20_cities.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void microsoft_StockModeling2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\montly_msft_2003_2012.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }

        #region Classification problems GP , ANN

        private void linkLabel17_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\gp_titanic_survival.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }

        private void linkLabel20_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\ann_prediction_maintanance.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        private void linkLabel16_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\ann_muliclass_iris_flover.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        private void linkLabel21_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\ann_regression_water_parameters.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        private void linkLabel19_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string strPath = Application.StartupPath + "\\Resources_Files\\gp_muliclass_glass_identification.gpa"; //gp_iris_flover.gpa";

            Open(strPath);
            var fName = Path.GetFileName(_filePath);
            this.Text = string.Format("{0} - {1}", _appName, fName);
            txtStatusMessage.Text = "Ready!";
        }
        #endregion
        #endregion

        #region Prepare Algoritms
        /// <summary>
        /// brunFactory=true means we prepare algorithm during the algorithm run. Otherwize from file
        /// </summary>
        private bool PrepareGP(bool brunFactory = true)
        {
            try
            {
                //set file as dirty 
                _isFileDirty = true;

                //Define GP parameters
                var gpp = _setPanel.GetParameters();
                gpp.algorithmType = Algorithm.GP;
                _resultPanel.SetConstants(_setPanel.Constants);

                //Define GP Terminals
                var trainigData = _dataPanel != null ? _dataPanel.Training : _experimentPanel.GetTrainingData();
                var testingData = _dataPanel != null ? _dataPanel.Testing : _experimentPanel.GetTestingData();
                
                //
                GPTerminalSet gpTSet = GPModelGlobals.GenerateTerminalSet(trainigData, //training data
                                                                          _setPanel.Constants, // random constant
                                                                          testingData); // data for testing and prediction
                //calculate class count
                
                Globals.classCount= _experimentPanel==null? 0: _experimentPanel.GetClassCount();

                //Deine GP function set
                GPFunctionSet gpFSet = new GPFunctionSet();
                gpFSet.SetFunction(_funPanel.GPFunctions);

                //Set GP Terminals
                var ter = gpTSet.GetTerminals();
                gpFSet.SetTerimals(ter);

                if (_optimizePanel != null)
                    _optimizePanel.FillTerminalBounds(ter.Where(x => x.Value.Name.Contains("X")).Select(x => x.Value.Name).ToList());

                //Set experimental var if it is GPMOdel type
                if(_GPModel== GPModelType.GPMODEL)
                    _mainGPFactory.m_Experiment = _experimentPanel.Experiment;

                if (brunFactory)
                {
                    //set indicator to 1 means GP is running
                    _runningEngine = 1;

                    //Call prepare algorithm
                    _mainGPFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
                }
                else
                {
                    _mainGPFactory.SetTerminalSet(gpTSet);
                    _mainGPFactory.SetFunctionSet(gpFSet);
                    _mainGPFactory.SetGPParameter(gpp);
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

           
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

            if (_mainGPFactory != null)
            {
                gpp = _mainGPFactory.GetParameters();
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
            if (_GPModel == GPModelType.SRO)
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
            Dictionary<int,GPTerminal> tt = null;

            if (_GPModel == GPModelType.AO)
            {
                tt = _funDefinit.GetTerminals();

                //Define TerminalSet
                gpTSet = new GPTerminalSet();
                gpTSet.RowCount = 1;
                gpTSet.NumVariables = tt.Count(x => x.Value.IsConstant == false);
                gpTSet.NumConstants = tt.Count(x => x.Value.IsConstant == true);

                //define FunctionSet by loading functions and set max and min values for terminals
                gpFSet = new GPFunctionSet();
                gpFSet.SetFunction(GPModelGlobals.LoadFunctionsfromXMLFile(), false);
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
                gpTSet = _mainGPFactory.GetTerminalSet();
                gpFSet = _mainGPFactory.GetFunctionSet(); 
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
                _secondFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
            }
            else//wne we need just init factory but not to b e run
            {
                _secondFactory.SetTerminalSet(gpTSet);
                _secondFactory.SetFunctionSet(gpFSet);
                _secondFactory.SetGPParameter(gpp);
            }
            return true;
            
        }

        /// <summary>
        /// 
        /// </summary>
        private bool PrepareTSP(bool brunFactory = true)
        {
            try
            {
                if (_dataPanel.Training == null)
                    return false;

                //set file as dirty 
                _isFileDirty = true;

                //Define GP parameters
                var gpp = _setPanel.GetParameters();
                gpp.algorithmType = Algorithm.GA;
                gpp.chromosomeKind = GAChromosome.TSP;

                //Define GP Terminals
                GPTerminalSet gpTSet = GPModelGlobals.GenerateTSPTerminalSet(_dataPanel.Training); 
                //Deine GP function set
                GPFunctionSet gpFSet = new GPFunctionSet();

                var fitnes = new TSPFitness();
                gpp.GPFitness = fitnes;
                
                //Set GP Terminals
                var ter = gpTSet.GetTerminals();
                gpFSet.SetTerimals(ter);

               
                if (brunFactory)
                {
                    //set indicator to 1 means GP is running
                    _runningEngine = 1;

                    //Call prepare algorithm
                    _mainGPFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
                }
                else
                {
                    _mainGPFactory.SetTerminalSet(gpTSet);
                    _mainGPFactory.SetFunctionSet(gpFSet);
                    _mainGPFactory.SetGPParameter(gpp);
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private bool PrepareALOC(bool brunFactory = true)
        {
            try
            {
                if (_dataPanel.Training == null)
                    return false;

                //set file as dirty 
                _isFileDirty = true;

                //Define GP parameters
                var gpp = _setPanel.GetParameters();
                gpp.algorithmType = Algorithm.GA;
                gpp.chromosomeKind = GAChromosome.ALOC;

                //Define GP Terminals
                GPTerminalSet gpTSet = GPModelGlobals.GenerateALOCTerminalSet(_dataPanel.Training);
                //Deine GP function set
                GPFunctionSet gpFSet = new GPFunctionSet();

                var fitnes = new ALOCFitness();
                gpp.GPFitness = fitnes;
                //Set what kind of optimization needs to be performed
                if (_alocPanel != null)
                    fitnes.IsMinimize = _alocPanel.IsMinimum();
                //Set GP Terminals
                var ter = gpTSet.GetTerminals();
                gpFSet.SetTerimals(ter);


                if (brunFactory)
                {
                    //set indicator to 1 means GP is running
                    _runningEngine = 1;

                    //Call prepare algorithm
                    _mainGPFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
                }
                else
                {
                    _mainGPFactory.SetTerminalSet(gpTSet);
                    _mainGPFactory.SetFunctionSet(gpFSet);
                    _mainGPFactory.SetGPParameter(gpp);
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        private bool PrepareTrans(bool brunFactory = true)
        {
            try
            {
                if (_dataPanel.Training == null)
                    return false;

                //set file as dirty 
                _isFileDirty = true;

                //Define GP parameters
                var gpp = _setPanel.GetParameters();
                gpp.algorithmType = Algorithm.GA;
                gpp.chromosomeKind = GAChromosome.TP;

                //Define GP Terminals
                GPTerminalSet gpTSet = GPModelGlobals.GenerateTRANSPORTTerminalSet(_dataPanel.Training);
                //Deine GP function set
                GPFunctionSet gpFSet = new GPFunctionSet();

                var fitnes = new TRANSFitness();
                gpp.GPFitness = fitnes;
                //Set what kind of optimization needs to be performed
                if (_alocPanel != null)
                    fitnes.IsMinimize = _alocPanel.IsMinimum();
                //Set GP Terminals
                var ter = gpTSet.GetTerminals();
                gpFSet.SetTerimals(ter);


                if (brunFactory)
                {
                    //set indicator to 1 means GP is running
                    _runningEngine = 1;

                    //Call prepare algorithm
                    _mainGPFactory.PrepareAlgorithm(gpTSet, gpFSet, gpp);
                }
                else
                {
                    _mainGPFactory.SetTerminalSet(gpTSet);
                    _mainGPFactory.SetFunctionSet(gpFSet);
                    _mainGPFactory.SetGPParameter(gpp);
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private bool PrepareANN(bool brunFactory = true)
        {
            try
            {
                //set file as dirty 
                _isFileDirty = true;

                //Define GP parameters
                var annp = _setANNPanel.GetParameters();
                

                //Define Experiment
                Experiment exp = _experimentPanel.Experiment;

                if (brunFactory)
                {
                    //set indicator to 1 means ANN is running
                    _runningEngine = 1;

                    //Call prepare algorithm
                    _mainANNFactory.PrepareAlgorithm(exp, annp);
                }
                else
                {
                    //_mainANNFactory.SetExperiment(gpTSet);
                    _mainANNFactory.SetANNParameters(annp);
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }
#endregion 

        #region Start Stop Reset Close Trainers
        /// <summary>
        /// 
        /// </summary>
        private void runProgram()
        {
            switch (_GPModel)   
            {
                case GPModelType.SR:
                case GPModelType.SRO:
                case GPModelType.TS:
                    if (PrepareGP())
                        _runGP(_runPanel, GPRunType.GPMODSolver);
                    break;
                case GPModelType.AO:
                    MessageBox.Show("Press Optimize for Analytic optimization!");
                    break;
                case GPModelType.TSP:
                    if(PrepareTSP())
                        _runGP(_tspPanel, GPRunType.GATSPSolver);
                    break;
                case GPModelType.AP:
                    if (PrepareALOC())
                        _runGP(_alocPanel, GPRunType.GAAPSolver);
                    break;
                case GPModelType.TP:
                    if (PrepareTrans())
                        _runGP(_alocPanel, GPRunType.GATPSolver);
                    break;
                case GPModelType.ANNMODEL:
                    if (PrepareANN())
                        _runANN(_runANNPanel, GPRunType.ANNMODSolver);
                    break;
                case GPModelType.GPMODEL:
                    if (PrepareGP())
                    {
                       
                        _runGP(_runPanel, GPRunType.GPMODSolver);
                    }
                        
                    break;
                default:
                    break;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void optimizeProgram()
        {
            if (_GPModel == Tool.Common.GPModelType.AO || _GPModel == Tool.Common.GPModelType.SRO)
            {
               
               if(PrepareGA())
                _runGP(_optimizePanel, GPRunType.GAOPTSolver);
            }

        }

        /// <summary>
        /// Running Genetic Programming and Genetic Algoritm solvers
        /// </summary>
        private void _runGP(BaseRunPanel pn, GPRunType gpTYpe = GPRunType.GPMODSolver)
        {
            try
            {
                Task tsk;
                float tv = pn.TerminationValue;
                int tt = pn.TerminationType;

                UpdateGUI(1);
                int currIter = pn.GetCurrentIteration();

                if (_mainGPFactory != null)
                    _mainGPFactory.SetCurrentIteration(currIter);

                if (gpTYpe == GPRunType.GPMODSolver)
                    tsk = new Task(() => _mainGPFactory.StartEvolution(tv, tt));
                else if (gpTYpe == GPRunType.GAOPTSolver)
                    tsk = new Task(() => _secondFactory.StartEvolution(tv, tt));
                else if (gpTYpe == GPRunType.GATSPSolver)
                    tsk = new Task(() => _mainGPFactory.StartEvolution(tv, tt));
                else if (gpTYpe == GPRunType.GAAPSolver)
                    tsk = new Task(() => _mainGPFactory.StartEvolution(tv, tt));
                else
                    tsk = new Task(() => _mainGPFactory.StartEvolution(tv, tt));
                //
                tsk.Start();

                //when task is end
                tsk.ContinueWith((t) =>
                    {
                        t.Dispose();
                    });

            }
            catch (Exception ex)
            {
                _runningEngine = 0;
                MessageBox.Show(ex.Message);
            } 

        }

        /// <summary>
        /// Running ANN SOlvers
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="gpTYpe"></param>
        private void _runANN(ANNRunPanel pn, GPRunType gpTYpe = GPRunType.GPMODSolver)
        {
            try
            {
                Task tsk;
                float tv = pn.TerminationValue;
                int tt = pn.TerminationType;
                //
                UpdateGUI(1);

                int currIter = pn.GetCurrentIteration();

                if (_mainANNFactory != null)
                    _mainANNFactory.SetCurrentIteration(currIter);

                if (gpTYpe == GPRunType.ANNMODSolver)
                    tsk = new Task(() => _mainANNFactory.StartIteration(tv, tt));
                else
                    tsk = new Task(() => _mainANNFactory.StartIteration(tv, tt));
                //
                tsk.Start();

                //when task is end
                tsk.ContinueWith((t) =>
                {
                    t.Dispose();
                });

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
        void gpFactory_ReportEvolution(object sender, ProgressIndicatorEventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        ReportEvolution(e);
                        //set indicator to stped
                        if (e.ReportType == ProgramState.Finished)
                            _runningEngine = 0;
                    }
                    ));
            }
            else
            {
               ReportEvolution(e);
               //set indicator to stped
               if (e.ReportType == ProgramState.Finished)
                   _runningEngine = 0;
            }
            
        }

        /// <summary>
        /// Main event for reporting about GP 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> event argument</param>
        void annFactory_ReportIteration(object sender, ProgressIndicatorEventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.Invoke(
                    new Action(() =>
                    {
                        ReportEvolution(e);
                        //set indicator to stped
                        if (e.ReportType == ProgramState.Finished)
                            _runningEngine = 0;
                    }
                    ));
            }
            else
            {
                ReportEvolution(e);
                //set indicator to stped
                if (e.ReportType == ProgramState.Finished)
                    _runningEngine = 0;
            }

        }

        private void ReportEvolution(ProgressIndicatorEventArgs e)
        {
            UpdateGUI((int)e.ReportType);

            if (_runPanel != null && _runningEngine == 1)
            {
                _runPanel.ReportProgress(e.CurrentIteration, e.AverageFitness, e.BestChromosome, (int)e.ReportType, e.LearnOutput);
            }

            if (_runANNPanel != null && _runningEngine == 1)
            {
                _runANNPanel.ReportProgress(e.CurrentIteration, e.LearningError, e.LearnOutput,(int)e.ReportType);
            }

            if (_tspPanel != null && _runningEngine == 1)
            {
                _tspPanel.ReportProgress(e.CurrentIteration, e.AverageFitness, e.BestChromosome, (int)e.ReportType);
            }
            if (_alocPanel != null && _runningEngine == 1)
            {
                _alocPanel.ReportProgress(e.CurrentIteration, e.AverageFitness, e.BestChromosome, (int)e.ReportType);
            }
            if (e.ReportType == ProgramState.Finished && _optimizePanel != null && _runningEngine == 1)
            {
                string str = Globals.gpterminals.ToStringMaxMinValues();
                _optimizePanel.SetMaximumAndMinimumValues(str);
            }

            if (_optimizePanel != null && _runningEngine==2)
                _optimizePanel.ReportProgress(e.CurrentIteration, e.AverageFitness, e.BestChromosome , (int)e.ReportType);

            if (_resultPanel != null && _runningEngine == 1)
                _resultPanel.ReportProgress(e.CurrentIteration, e.AverageFitness, e.BestChromosome , (int)e.ReportType);

            if (_predictionPanel != null && _runningEngine == 1 && _runANNPanel==null)
                _predictionPanel.ReportProgress(e.CurrentIteration, e.AverageFitness, e.BestChromosome as GPChromosome, (int)e.ReportType, e.PredicOutput);

            if (_predictionPanel != null && _runningEngine == 1 && _runANNPanel != null)
                _predictionPanel.ReportANNProgress(e.CurrentIteration, e.LearningError, e.PredicOutput, (int)e.ReportType);
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
                //gpModelPanel.Enabled = false;
                //gpExportPanel.Enabled = false;
                rbtnCloseModel.Enabled = false;
                gpCommonPanel.Enabled = false;
                rbtnOpenModel.Enabled = false;
                rbtnNewModel.Enabled = false;
                rbtnRun.Enabled = false;
                rbtnStop.Enabled = true;
                rbtnOptimize.Enabled = false;
                _bgpRuning = 2;
                
                //panels
                //panels
                if (_dataPanel != null)
                    _dataPanel.EnableCtrls(false);
                if (_experimentPanel != null)
                    _experimentPanel.EnableCtrls(false);
                if (_setPanel != null)
                    _setPanel.EnableCtrls(false);
                if (_setANNPanel != null)
                    _setANNPanel.EnableCtrls(false);
                if (_funPanel != null)
                    _funPanel.EnableCtrls(false);
                if (_funDefinit != null)
                    _funDefinit.EnableCtrls(false);
                if (_runPanel != null)
                    _runPanel.EnableCtrls(false);
                if (_runANNPanel != null)
                    _runANNPanel.EnableCtrls(false);
                if (_resultPanel != null)
                    _resultPanel.EnableCtrls(false);
                if (_optimizePanel != null)
                    _optimizePanel.EnableCtrls(false);
                if (_tspPanel != null)
                    _tspPanel.EnableCtrls(false);
                if (_alocPanel != null)
                    _alocPanel.EnableCtrls(false);

                txtStatusMessage.Text = "Program is running!";


            }
            else if (runType==3 && _bgpRuning != runType)
            {
                

                //gpModelPanel.Enabled = true;
                //gpExportPanel.Enabled = true;
                rbtnCloseModel.Enabled = true;
                gpCommonPanel.Enabled = true;
                rbtnOpenModel.Enabled = true;
                rbtnNewModel.Enabled = true;
                rbtnRun.Enabled = true;
                rbtnStop.Enabled = false;
                rbtnOptimize.Enabled = true;
                _bgpRuning = 0;

                //panels
                if (_dataPanel != null)
                    _dataPanel.EnableCtrls(true);
                if (_experimentPanel != null)
                    _experimentPanel.EnableCtrls(true);
                if (_setPanel != null)
                    _setPanel.EnableCtrls(true);
                if (_setANNPanel != null)
                    _setANNPanel.EnableCtrls(true);
                if (_funPanel != null)
                    _funPanel.EnableCtrls(true);
                if (_funDefinit != null)
                    _funDefinit.EnableCtrls(true);
                if (_runPanel != null)
                    _runPanel.EnableCtrls(true);
                if (_runANNPanel != null)
                    _runANNPanel.EnableCtrls(true);
                if (_resultPanel != null)
                    _resultPanel.EnableCtrls(true);
                if (_optimizePanel != null)
                    _optimizePanel.EnableCtrls(true);
                if (_tspPanel != null)
                    _tspPanel.EnableCtrls(true);
                if (_alocPanel != null)
                    _alocPanel.EnableCtrls(true);
                txtStatusMessage.Text = "Ready!";
            }
           // Console.WriteLine("runType={0} bRunnig={1}", runType, _bgpRuning);
        }

       /// <summary>
       /// 
       /// </summary>
       private void stopProgram()
        {
            _bgpRuning = 0;

            if (!GPFactory.StopIteration)
                GPFactory.StopIteration = true;
            else 
           {
               _runningEngine = 0;
                //in case if run button is disabled, but engine is not running.
               if (rbtnStop.Enabled==true)
                UpdateGUI(3);
            }

            if (!BPFactory.StopIteration)
                BPFactory.StopIteration = true;
            else
            {
                _runningEngine = 0;
                //in case if run button is disabled, but engine is not running.
                if (rbtnStop.Enabled == true)
                    UpdateGUI(3);
            }

        }

        #endregion

    }
}
