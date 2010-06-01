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
using System.Windows.Forms;
using GPdotNET.Properties;
using GPNETLib;
using Excel = Microsoft.Office.Interop.Excel;
namespace GPdotNET
{
    public partial class GPdotNetApp : Form
    {
        GP_TS_Panel childForm = null;
        public GPdotNetApp()
        {
            InitializeComponent();
            
        }

        void childForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (childForm.IsGPRunning == true)
            {
                MessageBox.Show(Properties.Resources.SR_StopFirst, Properties.Resources.SR_ApplicationName);
                e.Cancel = true;
                return;
            }
            DialogResult retVal = MessageBox.Show(Resources.SR_AskForSave, Resources.SR_ApplicationName, MessageBoxButtons.YesNoCancel);
            if (DialogResult.Cancel == retVal)
                e.Cancel = true;
            else if (DialogResult.Yes == retVal)
            {
                SpasiDokument();
            }
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            if (childForm != null)
            {
                MessageBox.Show(Resources.SR_SDI_Warning, Resources.SR_ApplicationName);
                return;
            }
            ChooseModelDlg dlg = new ChooseModelDlg();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;
            childForm = NovaForma();
            childForm.filePath = "";
            childForm.TimeSeriesPrediction = dlg.TimeSeries;
            if (dlg.TimeSeries)
                childForm.Text = Resources.SR_New_TSModel;
            else
                childForm.Text = Resources.SR_New_SRModel;
            childForm.Show();
        }

        void childForm_Disposed(object sender, EventArgs e)
        {
            //childForm.Dispose();
            childForm = null;
        }

        private void OpenFile(object sender, EventArgs e)
        {

            if (childForm != null)
            {
                MessageBox.Show(Resources.SR_SDI_Warning, Resources.SR_ApplicationName);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "GPdotNET Files (*.gpx)|*.gpx|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                OtvoriDokument(FileName);
            }

            
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(childForm!=null)
                SpasiDokument();
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (childForm == null)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.gpx)|*.gpx|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                childForm.filePath = FileName;
                SpasiDokument();
            }
        }
        private string SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.gpx)|*.gpx|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return "";
        }
        private void OtvoriDokument(string FileName)
        {
            if (childForm == null)
            {
                childForm = NovaForma();
            }
            childForm.OpenFromFile(FileName);
            childForm.Text = System.IO.Path.GetFileName(FileName);
            childForm.MdiParent = this;
            childForm.Show();
        }

        private GP_TS_Panel NovaForma()
        {
            GP_TS_Panel frm = new GP_TS_Panel();
            frm.FormClosing += new FormClosingEventHandler(childForm_FormClosing);
            frm.Disposed += new EventHandler(childForm_Disposed);
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            return frm;
        }
        private void SpasiDokument()
        {
            if (childForm == null)
                return;
            if (childForm.filePath == "")
            {
                childForm.filePath = SaveToFile();
                if (childForm.filePath == "")
                    return;
            }
           
            childForm.SaveToFile();
            
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutGPdotNET dlg = new AboutGPdotNET();
            dlg.ShowDialog();
            dlg.Dispose();
        }
        //Export GP model with Training TrainingData
        private void gPModelWithTrainingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GP_TS_Panel GP_panel;
            if (this.MdiChildren.Length > 0)
            {
                GP_panel = this.MdiChildren[0] as GP_TS_Panel;

                if (GP_panel != null)
                {

                    //Write to excell file
                    Excel.Application oXL;
                    Excel._Workbook oWB;
                    Excel._Worksheet oSheet;
                    //    Excel.Range oRng;

                    string formula = "";
                    int inputVarCount = 0;
                    int constCount = 0;
                    if (GP_panel.GPConstants != null)
                        constCount = GP_panel.GPConstants.Length;
                    if (GP_panel.GPTrainingData == null)
                    {
                        MessageBox.Show(Resources.SR_DataEmpty, Resources.SR_ApplicationName);
                        return;
                    }
                    else
                        inputVarCount = GP_panel.GPTrainingData.Length;

                    try
                    {
                        //Start Excel and get Application object.
                        oXL = new Excel.Application();
                        oXL.Visible = true;

                        //Get a new workbook.
                        oWB = (Excel._Workbook)(oXL.Workbooks.Add());
                        oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                        //TITLE
                        oSheet.Cells[1, 1] = Resources.SR_TrainingData;
                        oSheet.get_Range("A1", "D1").Font.Bold = true;
                        oSheet.get_Range("A1", "D1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        //COLUMNS NAMES
                        oSheet.Cells[2, 1] = Resources.SR_SampleNumber;
                        for (int i = 0; i < inputVarCount - 1; i++)
                        {
                            oSheet.Cells[2, i + 2] = "X" + (i + 1).ToString();
                        }

                        for (int i = 0; i < constCount; i++)
                        {
                            oSheet.Cells[2, inputVarCount + i + 1] = "R" + (i + 1).ToString();
                        }
                        oSheet.Cells[2, inputVarCount + constCount + 1] = "Y";
                        oSheet.Cells[2, inputVarCount + constCount + 2] = "Ygp";

                        //Add Training TrainingData.
                        for (int i = 0; i < inputVarCount - 1; i++)
                        {
                            for (int j = 0; j < GP_panel.GPTrainingData[i].Length; j++)
                            {
                                if (i == 0)
                                    oSheet.Cells[j + 3, i + 1] = j + 1;
                                oSheet.Cells[j + 3, i + 2] = GP_panel.GPTrainingData[i][j];
                            }
                        }

                        //Constants
                        for (int i = 0; i < constCount; i++)
                        {
                            for (int j = 0; j < GP_panel.GPTrainingData[0].Length; j++)
                            {
                                oSheet.Cells[j + 3, inputVarCount + i + 1] = GP_panel.GPConstants[i];
                            }

                        }
                        //Output variable 
                        for (int j = 0; j < GP_panel.GPTrainingData[0].Length; j++)
                        {
                            oSheet.Cells[j + 3, inputVarCount + constCount + 1] = GP_panel.GPTrainingData[inputVarCount - 1][j];
                        }
                        if (GP_panel.functionSet == null)
                        {
                            MessageBox.Show(Resources.SR_FunctionSetNull, Resources.SR_ApplicationName);
                            return;
                        }
                        if (GP_panel.terminalSet == null)
                        {
                            MessageBox.Show(Resources.SR_TerminalSetNull, Resources.SR_ApplicationName);
                            return;
                        }
                        //GP Model formula
                        List<int> lst = new List<int>();
                        FunctionTree.ToListExpression(lst, GP_panel.GPBestHromosome.Root);
                        formula = "=" + GP_panel.functionSet.DecodeExpressionInExcellForm(lst, GP_panel.terminalSet);
                        AlphaCharEnum alphaEnum = new AlphaCharEnum();
                        // char alphaStart = Char.Parse("B"); 
                        for (int i = 0; i < inputVarCount - 1; i++)
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
                            string cell = alphaEnum.AlphabetFromIndex(inputVarCount + 1 + i) + "3";
                            formula = formula.Replace(var, cell);
                            // alphaStart++;
                        }
                        oSheet.Cells[3, inputVarCount + constCount + 2].Formula = formula;
                        //Copy formula from cell
                        oSheet.Cells[3, inputVarCount + constCount + 2].Copy();
                        //And paste to all sample TrainingData
                        for (int j = 1; j < GP_panel.GPTrainingData[0].Length; j++)
                        {
                            oSheet.Paste(oSheet.Cells[j + 3, inputVarCount + constCount + 2]);
                        }
                        //Make sure Excel is visible and give the user control
                        //of Microsoft Excel's lifetime.
                        oXL.Visible = true;
                        oXL.UserControl = true;
                    }
                    catch
                    {
                        ExcelFormulaDlg dlg = new ExcelFormulaDlg();
                        dlg.textBox1.Text = formula;
                        dlg.ShowDialog();
                        dlg.Dispose();
                    }
                }
            }


        }
        //Export GP model with Testing TrainingData
        private void gPModelWithTestingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GP_TS_Panel GP_panel;
            if (this.MdiChildren.Length > 0)
            {
                GP_panel = this.MdiChildren[0] as GP_TS_Panel;

                if (GP_panel != null)
                {

                    //Write to excell file
                    Excel.Application oXL;
                    Excel._Workbook oWB;
                    Excel._Worksheet oSheet;
                    //    Excel.Range oRng;

                    string formula = "";
                    int inputVarCount = 0;
                    int constCount = 0;
                    if (GP_panel.GPConstants != null)
                        constCount = GP_panel.GPConstants.Length;
                    if (GP_panel.GPTestingData == null)
                    {
                        MessageBox.Show(Resources.SR_DataEmpty, Resources.SR_ApplicationName);
                        return;
                    }
                    else
                        inputVarCount = GP_panel.GPTestingData.Length;

                    try
                    {
                        //Start Excel and get Application object.
                        oXL = new Excel.Application();
                        oXL.Visible = true;

                        //Get a new workbook.
                        oWB = (Excel._Workbook)(oXL.Workbooks.Add());
                        oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                        //TITLE
                        oSheet.Cells[1, 1] = Resources.SR_TrainingData;
                        oSheet.get_Range("A1", "D1").Font.Bold = true;
                        oSheet.get_Range("A1", "D1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        //COLUMNS NAMES
                        oSheet.Cells[2, 1] = Resources.SR_SampleNumber;
                        for (int i = 0; i < inputVarCount - 1; i++)
                        {
                            oSheet.Cells[2, i + 2] = "X" + (i + 1).ToString();
                        }

                        for (int i = 0; i < constCount; i++)
                        {
                            oSheet.Cells[2, inputVarCount + i + 1] = "R" + (i + 1).ToString();
                        }
                        oSheet.Cells[2, inputVarCount + constCount + 1] = "Y";
                        oSheet.Cells[2, inputVarCount + constCount + 2] = "Ygp";

                        //Add Training TrainingData.
                        for (int i = 0; i < inputVarCount - 1; i++)
                        {
                            for (int j = 0; j < GP_panel.GPTestingData[i].Length; j++)
                            {
                                if (i == 0)
                                    oSheet.Cells[j + 3, i + 1] = j + 1;
                                oSheet.Cells[j + 3, i + 2] = GP_panel.GPTestingData[i][j];
                            }
                        }

                        //Constants
                        for (int i = 0; i < constCount; i++)
                        {
                            for (int j = 0; j < GP_panel.GPTestingData[0].Length; j++)
                            {
                                oSheet.Cells[j + 3, inputVarCount + i + 1] = GP_panel.GPConstants[i];
                            }

                        }
                        //Output variable 
                        for (int j = 0; j < GP_panel.GPTestingData[0].Length; j++)
                        {
                            oSheet.Cells[j + 3, inputVarCount + constCount + 1] = GP_panel.GPTestingData[inputVarCount - 1][j];
                        }
                        if (GP_panel.functionSet == null)
                        {
                            MessageBox.Show(Resources.SR_FunctionSetNull, Resources.SR_ApplicationName);
                            return;
                        }
                        if (GP_panel.terminalSet == null)
                        {
                            MessageBox.Show(Resources.SR_TerminalSetNull, Resources.SR_ApplicationName);
                            return;
                        }
                        //GP Model formula
                        List<int> lst = new List<int>();
                        FunctionTree.ToListExpression(lst, GP_panel.GPBestHromosome.Root);
                        formula = "=" + GP_panel.functionSet.DecodeExpressionInExcellForm(lst, GP_panel.terminalSet);
                        AlphaCharEnum alphaEnum = new AlphaCharEnum();
                        // char alphaStart = Char.Parse("B"); 
                        for (int i = 0; i < inputVarCount - 1; i++)
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
                            string cell = alphaEnum.AlphabetFromIndex(inputVarCount + 1 + i) + "3";
                            formula = formula.Replace(var, cell);
                            // alphaStart++;
                        }
                        oSheet.Cells[3, inputVarCount + constCount + 2].Formula = formula;
                        //Copy formula from cell
                        oSheet.Cells[3, inputVarCount + constCount + 2].Copy();
                        //And paste to all sample TrainingData
                        for (int j = 1; j < GP_panel.GPTestingData[0].Length; j++)
                        {
                            oSheet.Paste(oSheet.Cells[j + 3, inputVarCount + constCount + 2]);
                        }
                        //Make sure Excel is visible and give the user control
                        //of Microsoft Excel's lifetime.
                        oXL.Visible = true;
                        oXL.UserControl = true;
                    }
                    catch
                    {
                        ExcelFormulaDlg dlg = new ExcelFormulaDlg();
                        dlg.textBox1.Text = formula;
                        dlg.ShowDialog();
                        dlg.Dispose();
                    }
                }
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Lang == false)
                return;
            Properties.Settings.Default.Lang = false;
            Properties.Settings.Default.Save();

            MessageBox.Show(Resources.SR_ChangedLangWarning, Resources.SR_ApplicationName);
        }

        private void bosnianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Lang == true)
                return;
            Properties.Settings.Default.Lang = true;
            Properties.Settings.Default.Save();
            MessageBox.Show(Resources.SR_ChangedLangWarning);
        }

        private void lanhuageToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Lang == false)
            {
                bosnianToolStripMenuItem.Checked = false;
                englishToolStripMenuItem.Checked = true;
            }
            else
            {
                bosnianToolStripMenuItem.Checked = true;
                englishToolStripMenuItem.Checked = false;
            }
        }

       
    }
}
