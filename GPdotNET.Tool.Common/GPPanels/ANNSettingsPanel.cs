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
using System.Globalization;
using System.Windows.Forms;
using GPdotNET.Core;
using GPdotNET.Engine;
using GPdotNET.Util;
using GPdotNET.Core.Interfaces;
using GPdotNET.Engine.ANN;

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Class implements parameters for GPa and GP
    /// </summary>
    public partial class ANNSettingsPanel : UserControl
    {
        #region CTor and Fields

       
        public ANNSettingsPanel()
        {
            InitializeComponent();
           
            LoadActivationFunsInCombo();

            LoadSelectionMethodsInCOmbo();

            var p= new ANNParameters();

            SetParameters(p);
        }

        
        #endregion


        #region Properties
        
        #endregion

        #region private Methods

        /// <summary>
        /// Enumerate ENUM of selection methods and insert as ComboBox items
        /// so the user can easyly select.
        /// this is also handy when you extend selectionmethods and populate them automaticaly
        /// </summary>
        private void LoadSelectionMethodsInCOmbo()
        {
            //fill combo with initialization methdos
            foreach (var initM in Enum.GetValues(typeof(GPSelectionMethod)))
                cmbSelectionMethods.Items.Add(initM.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void SetLearnigAlgorithm(int v)
        {
            cmbLearningAlgorithm.SelectedIndex = v;
        }

        /// <summary>
        /// When the secection method is changed, dfault parms must be set properly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectionMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectionMethods.SelectedItem == null)
                return;


            switch (cmbSelectionMethods.SelectedIndex)
            {
                //Fitness proportionate selection
                case 0:
                    lbSelParam1.Visible = false;
                    txtSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    txtSelParam2.Visible = false;
                    label3.Visible = false;
                    break;
                //Rank Selection
                case 1:
                    lbSelParam1.Visible = false;
                    txtSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    txtSelParam2.Visible = false;
                    label3.Visible = false;
                    break;
                //Tournment Selection
                case 2:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Tour Size:";
                    txtSelParam1.Text = 2.ToString();
                    txtSelParam1.Visible = true;
                    lbSelParam2.Visible = false;
                    txtSelParam2.Visible = false;
                    label3.Text = "(0-10)";
                    label3.Visible = true;
                    break;
                //Stochastic unversal selection
                case 3:
                    lbSelParam1.Visible = false;
                    txtSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    txtSelParam2.Visible = false;
                    label3.Visible = false;
                    break;
                //FUSS selection
                case 4:
                    lbSelParam1.Visible = false;
                    txtSelParam1.Visible = false;
                    lbSelParam2.Visible = false;
                    txtSelParam2.Visible = false;
                    label3.Visible = false;
                    break;
                //Skrgic Selection
                case 5:
                    lbSelParam1.Visible = true;
                    lbSelParam1.Text = "Nonlinear Coef:";
                    txtSelParam1.Text = (1.0 / 5.0).ToString();
                    txtSelParam1.Visible = true;
                    lbSelParam2.Visible = false;
                    txtSelParam2.Visible = false;
                    label3.Text = "(0-10)";
                    label3.Visible = true;
                    break;
                //Default Selection
                default:
                    break;
            }
        }
        /// <summary>
        ///  //we need here to provide loading all class which are derived from IFitness interface
        /// on that way we hav complete customisation of th fitness functions
        /// </summary>
        private void LoadActivationFunsInCombo()
        {
            cmbActivationFuncs.Items.Add("Linear Function ");
            cmbActivationFuncs.Items.Add("Binary Function ");
            cmbActivationFuncs.Items.Add("Sigmoid Function ");
            cmbActivationFuncs.Items.Add("BipolarSigmoid Function ");
            cmbActivationFuncs.Items.Add("Tan-Sigmoid Function ");
                        
        }
       
        
        /// <summary>
        /// Enumerate ENUM of selection methods and insert as ComboBox items
        /// so the user can easyly select.
        /// this is also handy when you extend selectionmethods and populate them automaticaly
        /// </summary>
       

        /// <summary>
        /// 
        /// Proces of selecting activation function form combobox
        /// </summary>
        /// <param name="selIndex"></param>
        /// <returns></returns>
        private IANNActivation SelectActivationFun(int selIndex)
        {
            return new Sigmoid(0);
            //switch (selIndex)
            //{
            //    case 0:
            //        return new RMSEFitness(); 
            //    case 1:
            //        return new MSEFitness();
            //    case 2:
            //        return new MAEFitness();
            //    case 3:
            //        return new RSEFitness();
            //    case 4:
            //        return new RRSEFitness();
            //    case 5:
            //        return new RAEFitness();
            //    default:
            //        return new RMSEFitness();
            //}
            
        }
        private void cmbActivationFuncs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbActivationFuncs.SelectedIndex == 2 || cmbActivationFuncs.SelectedIndex == 3)
            {
                label16.Visible = true;
                txtActFunParam1.Visible = true;
                label10.Visible = true;
            }

            else
            {
                label16.Visible = false;
                txtActFunParam1.Visible = false;
                label10.Visible = false;
            }

        }

        private void cmbLearningAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLearningAlgorithm.SelectedIndex == 0)
            {
                groupBoxGA.Visible = false;
                groupBoxPSO.Visible = false;

            }
            else if (cmbLearningAlgorithm.SelectedIndex == 1)
            {
                groupBoxGA.Visible = false;
                groupBoxPSO.Visible = true;

            }
            else
            {
                groupBoxGA.Visible = true;
                groupBoxPSO.Visible = false;

            }
        }



        #endregion

        #region Public Methods
        /// <summary>
        /// Set GUI based on the GPParameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool SetParameters(ANNParameters parameters)
        {
            //Activation function
            if (parameters.m_ActFunction is Linear)
                cmbActivationFuncs.SelectedIndex = 0;
            else if(parameters.m_ActFunction is Binary)
                cmbActivationFuncs.SelectedIndex = 1;
            else if(parameters.m_ActFunction is Sigmoid)
                cmbActivationFuncs.SelectedIndex = 2;
            else if (parameters.m_ActFunction is BipolarSign)
                cmbActivationFuncs.SelectedIndex = 3;
            else if (parameters.m_ActFunction is TanH)
                cmbActivationFuncs.SelectedIndex = 4;


            //larning algoritm
            if (parameters.m_LearningAlgo == LearningAlgoritm.BP)
                cmbLearningAlgorithm.SelectedIndex = 0;
            else if (parameters.m_LearningAlgo == LearningAlgoritm.PSO)
                cmbLearningAlgorithm.SelectedIndex = 1;
            else
                cmbLearningAlgorithm.SelectedIndex = 2;

            
            txtActFunParam1.Text = parameters.m_ActFuncParam1.ToString();


            txtMomentum.Text = parameters.m_Momentum.ToString();
            textLearningRate.Text = parameters.m_LearningRate.ToString();
            textNumHiddenLayers.Text = parameters.m_NumHiddenLayers.ToString();
            textNeuronsOfEachLAyer.Text = parameters.m_NeuronsInHiddenLayer.ToString();

            textParticles.Text = parameters.m_PSOParameters.m_ParticlesNumber.ToString();


            textIWeight.Text = parameters.m_PSOParameters.m_IWeight.ToString();

            textCWeight.Text = parameters.m_PSOParameters.m_GWeight.ToString();

            textSWeight.Text = parameters.m_PSOParameters.m_LWeight.ToString();

            return true;
        }

        /// <summary>
        /// Return current value of GP params
        /// </summary>
        /// <returns></returns>
        public ANNParameters GetParameters()
        {
            ANNParameters parameters = new ANNParameters();


            if (!double.TryParse(txtMomentum.Text, out parameters.m_Momentum))
            {
                MessageBox.Show("Invalid value for Momentum!");
                return null;
            }

           
            if (!double.TryParse(textLearningRate.Text, out parameters.m_LearningRate))
            {
                MessageBox.Show("Invalid value for Learning Rate!");
                return null;
            }


            if (!int.TryParse(textNumHiddenLayers.Text, out parameters.m_NumHiddenLayers))
            {
                MessageBox.Show("Invalid value for number of Layers!");
                return null;
            }

            if (!int.TryParse(textNeuronsOfEachLAyer.Text, out parameters.m_NeuronsInHiddenLayer))
            {
                MessageBox.Show("Invalid value for number of Neurons in hidden Layer!");
                return null;
            }

            //parameters for activation function
            if (!double.TryParse(txtActFunParam1.Text, out parameters.m_ActFuncParam1))
            {
                MessageBox.Show("Invalid value for Parameter 1!");
                return null;
            }

            
            //activation function
            if (cmbActivationFuncs.SelectedIndex == 0)
                parameters.m_ActFunction = new Linear();
            else if (cmbActivationFuncs.SelectedIndex == 1)
                parameters.m_ActFunction = new Binary();
            else if (cmbActivationFuncs.SelectedIndex == 2)
                parameters.m_ActFunction = new Sigmoid(parameters.m_ActFuncParam1);
            else if (cmbActivationFuncs.SelectedIndex == 3)
                parameters.m_ActFunction = new BipolarSign(parameters.m_ActFuncParam1);
            else
                parameters.m_ActFunction = new TanH();


            //learning algo
            if (cmbLearningAlgorithm.SelectedIndex == 0)
                parameters.m_LearningAlgo = LearningAlgoritm.BP;
            else if (cmbLearningAlgorithm.SelectedIndex == 1)
                parameters.m_LearningAlgo = LearningAlgoritm.PSO;
            else
                parameters.m_LearningAlgo = LearningAlgoritm.BP;


            if (!int.TryParse(textParticles.Text, out parameters.m_PSOParameters.m_ParticlesNumber))
            {
                MessageBox.Show("Invalid value for Number of Particles!");
                return null;
            }

             if (!double.TryParse(textIWeight.Text, out parameters.m_PSOParameters.m_IWeight))
            {
                MessageBox.Show("Invalid value for Inertia Weight!");
                return null;
            }

             if (!double.TryParse(textCWeight.Text, out parameters.m_PSOParameters.m_LWeight))
            {
                MessageBox.Show("Invalid value for Cognitive Weight!");
                return null;
            }

             if (!double.TryParse(textParticles.Text, out parameters.m_PSOParameters.m_GWeight))
            {
                MessageBox.Show("Invalid value for Social Weight!");
                return null;
            }

            return parameters;
        }

        /// <summary>
        /// Enables or disables controls during of running program
        /// </summary>
        /// <param name="p"></param>
        public void EnableCtrls(bool p)
        {
            groupBox1.Enabled = p;
            groupBox2.Enabled = p;
            groupBoxGA.Enabled = p;
            groupBoxPSO.Enabled = p;

        }


        /// <summary>
        /// Desrilization of parametars
        /// </summary>
        /// <param name="p"></param>
        public void SetParametersFromString(string p)
        {
            var pstr = p.Split(';');

            try
            {
                //MOmentum
                txtMomentum.Text = pstr[0];
                textLearningRate.Text = pstr[1];

                 ///act fun
                int temp = 0;
                if (!int.TryParse(pstr[2], out temp))
                    temp = 0;
                cmbActivationFuncs.SelectedIndex = temp;

                //layer number
                textNumHiddenLayers.Text= pstr[3];

                //neuron number
                textNeuronsOfEachLAyer.Text = pstr[4];

                //particles
                textParticles.Text = pstr[5];

                //inertia weight
                textIWeight.Text = pstr[6];

                //cognitive weight
                textCWeight.Text = pstr[7];

                //socal weight
                textSWeight.Text = pstr[8];
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Serilization of parameters
        /// </summary>
        /// <returns></returns>
        public string ParametersToString()
        {
            string retVal = "";
            try
            {

                //Momentum
                float intValue = 0;
                if (!float.TryParse(txtMomentum.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Momentum!");
                    return null;
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture) + ";";

                //learning rate
                if (!float.TryParse(textLearningRate.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Learning rate!");
                    return null;
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture) + ";";

                //act func
                if (cmbActivationFuncs.SelectedIndex == -1)
                {
                    MessageBox.Show("Invalid value for Activation function!");
                    return null;
                }
                retVal += cmbActivationFuncs.SelectedIndex.ToString() + ";";

                //llayer number
                if (!float.TryParse(textNumHiddenLayers.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Lalyers!");
                    return null;
                }
                retVal += intValue.ToString() + ";";

                //neuron number
                if (!float.TryParse(textNeuronsOfEachLAyer.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Neurons for each layer!");
                    return null;
                }
                retVal += intValue.ToString() + ";";

                //PSO paramneters
                //Particle number
                if (!float.TryParse(textParticles.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Particle number!");
                    return null;
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Inertia weight
                if (!float.TryParse(textIWeight.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Inertia Weight!");
                    return null;
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Cognitive weight
                if (!float.TryParse(textCWeight.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Cognitive Weight!");
                    return null;
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Social weight
                if (!float.TryParse(textSWeight.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Social Weight!");
                    return null;
                }
                retVal += intValue.ToString(CultureInfo.InvariantCulture);

                return retVal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void LockLearningAlgoritm()
        {
            cmbLearningAlgorithm.Enabled = false;
        }


        #endregion

    }
}
