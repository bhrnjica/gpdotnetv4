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

namespace GPdotNET.Tool.Common
{
   /// <summary>
   /// Class implements several controls for showing best result GP found during GP programm runing
   /// </summary>
    public partial class ResultPanel : UserControl
    {
        #region CTor and Fields
        private float prevFitness = -1;
        private GPChromosome _gpModel;
        public ResultPanel()
        {
            InitializeComponent();
          
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
            if (prevFitness < ch.Fitness)
            {
               
                prevFitness = ch.Fitness;
                if (ch is GPChromosome)
                {
                    _gpModel = (GPdotNET.Engine.GPChromosome)ch;
                    enooptMatematickiModel.Text = _gpModel.expressionTree.ToString();
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
        #endregion

        
    }
}
