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
using System.Windows.Forms;
using GPdotNET.Core;
using GPdotNET.Engine;
using GPdotNET.Util;

namespace GPdotNET.Tool.Common
{
   /// <summary>
   /// Class implements several controls for showing best result GP found during GP programm runing
   /// </summary>
    public partial class ResultPanel : UserControl
    {
        #region Ctor and Fields
        private float prevFitness = -1;
        private GPChromosome _gpModel;
        private double[] _consts;
        public ResultPanel()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var filePath = GPModelGlobals.GetFileFromSaveDialog("PNG File Format", "*.png");
            if(!string.IsNullOrEmpty(filePath))
                treeCtrlDrawer1.SaveAsImage(filePath);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            
        }
        
        /// <summary>
        /// Reports about current evolution. See other reposrt methods
        /// </summary>
        /// <param name="currentEvolution"></param>
        /// <param name="averageFitness"></param>
        /// <param name="ch"></param>
        /// <param name="repType"></param>
        public void ReportProgress(int currentEvolution, float averageFitness, IChromosome ch, int repType)
        {
            if (ch == null)
                return;

            if (prevFitness < ch.Fitness)
            {
               
                prevFitness = ch.Fitness;
                if (ch is GPChromosome)
                {
                    _gpModel = (GPdotNET.Engine.GPChromosome)ch;

                    if(Globals.functions!=null)
                        enooptMatematickiModel.Text = Globals.functions.DecodeExpression(_gpModel.expressionTree);

                    treeCtrlDrawer1.DrawTreeExpression(_gpModel.expressionTree, Globals.GetGPNodeStringRep);
                }
            }
        }

        /// <summary>
        /// Returns GP tree expression
        /// </summary>
        /// <returns></returns>
        public GPNode GetGPModel()
        {
            if (_gpModel == null)
                return null;
            return _gpModel.expressionTree;
        }

        /// <summary>
        /// Resets previous solution
        /// </summary>
        public void ResetSolution()
        {
            prevFitness = -1;
            _gpModel = null;
        }

        /// <summary>
        /// Check if there is a prev solution
        /// </summary>
        /// <returns></returns>
        public bool HasPrevSoluton()
        {
            return prevFitness >= 0;
        }

        /// <summary>
        /// Sets the constants and load them in to gridView
        /// </summary>
        /// <param name="cons"></param>
        public void SetConstants(double[] cons)
        {
            _consts = cons;

            if (_consts == null || _consts.Length == 0)
            {
                listView1.Clear();
                return;
            }
            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numRow = _consts.Length;

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = "Name";
            colHeader.Width = 45;
            listView1.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            colHeader.Text = "Value";
            colHeader.Width = 60;
            listView1.Columns.Add(colHeader);
    
            for (int j = 0; j < numRow; j++)
            {
                ListViewItem LVI = listView1.Items.Add("R"+(j + 1).ToString());
                LVI.SubItems.Add(_consts[j].ToString());
            }
        }



        #endregion

        private void btnROC_Click(object sender, System.EventArgs e)
        {

        }
    }
}
