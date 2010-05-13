// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// GPdotNET 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPDOTNET
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
           DialogResult retVal= MessageBox.Show("Save changes?", "GPdotNET", MessageBoxButtons.YesNoCancel);
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
                MessageBox.Show("Please, close existing document first!");
                return;
            }
            childForm = NovaForma();
            childForm.filePath = "";
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
                MessageBox.Show("Please, close existing document first!");
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
       

        
    }
}
