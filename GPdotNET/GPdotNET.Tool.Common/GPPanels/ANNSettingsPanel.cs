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
           

            var p= new ANNParameters();

            SetParameters(p);
        }

        
        #endregion


        #region Properties
        
        #endregion

        #region private Methods
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


     
        
#endregion

        #region Public Methods
        /// <summary>
        /// Set GUI based on the GPParameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool SetParameters(ANNParameters parameters)
        {

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
            groupBox3.Enabled = p;
            groupBox4.Enabled = p;

        }


        /// <summary>
        /// Desrilization of parametars
        /// </summary>
        /// <param name="p"></param>
        public void SetParameters(string p)
        {
            //var pstr = p.Split(';');
            
            //try
            //{

            //    //PopSize
            //    txtIterations.Text=pstr[0];
            //    //Fitness
            //    int temp=0;
            //    if (!int.TryParse(pstr[1], out temp))
            //        temp = 0;
            //    cmbActivationFuncs.SelectedIndex = temp;

            //    ///Init method
            //    temp = 0;
            //    if (!int.TryParse(pstr[2], out temp))
            //        temp = 0;
            //    cmbInitMethods.SelectedIndex = temp;

            //    //init depth
            //    txtInitTreeDepth.Text = pstr[3];

            //    //operation depth
            //    txtOperationTreeDepth.Text = pstr[4];


            //    //Selection Elitism
            //    txtElitism.Text = pstr[5];

            //    //Selection method
            //    temp = 0;
            //    if (!int.TryParse(pstr[6], out temp))
            //        temp = 0;
            //    cmbSelectionMethods.SelectedIndex = temp;

            //    //Selection param1
            //    txtSelParam1.Text = pstr[7];

            //    //Selection param2
            //    txtSelParam2.Text = pstr[8];

            //    //Random constant from
            //    txtRandomConsFrom.Text = pstr[9];

            //    //Random constant to
            //    txtRandomConsTo.Text = pstr[10];

            //    //Random constant count
            //    txtRandomConstNum.Text = pstr[11];

            //    //Crossover method
            //    float val = 0;
            //    if (!float.TryParse(pstr[12],System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            //        val = 0;
            //    txtMomentum.Text = val.ToString();

            //    //mutation method
            //    val = 0;
            //    if (!float.TryParse(pstr[13], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            //        val = 0;
            //    txtActFunParam1.Text = val.ToString();

            //    //reproduction method
            //    val = 0;
            //    if (!float.TryParse(pstr[14], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            //        val = 0;
            //    txtActFunParam2.Text = val.ToString();

            //    //Permutation method
            //    val = 0;
            //    if (!float.TryParse(pstr[15], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            //        val = 0;
            //    txtProbPermutation.Text = val.ToString();

            //    //Encaptualtion method
            //    val = 0;
            //    if (!float.TryParse(pstr[16], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            //        val = 0;
            //    txtProbEncaptulation.Text = val.ToString();

            //    //Editing
            //    temp = 0;
            //    if (!int.TryParse(pstr[17], out temp))
            //        temp = 0;
            //    checkBox2.Checked = temp==0?false:true;
            //    //decimation
            //    temp = 0;
            //    if (!int.TryParse(pstr[18], out temp))
            //        temp = 0;
            //    checkBox2.Checked = temp == 0 ? false : true;

            //    if (pstr.Length <= 20)
            //        return;

            //    int numCount=int.Parse(pstr[11]);
            //    _constants= new double[numCount];
            //    for (int i = 0; i < numCount; i++)
            //    {
            //        val = 0;
            //        if (!float.TryParse(pstr[19+i], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            //            val = 0;
            //        _constants[i] = val;
            //    }

            //    //todo
            //    //default value
            //    txtBroodSize.Text = "1";
            //        return;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}
        }

        /// <summary>
        /// Serilization of parameters
        /// </summary>
        /// <returns></returns>
        public string ParametersToString()
        {
            return "";
            //string retVal = "";
            //try
            //{
               
            //    //PopSize
            //    int intValue = 0;
            //    if (!int.TryParse(txtIterations.Text, out intValue))
            //    {
            //        MessageBox.Show("Invalid value for Population size!");
            //        return null;
            //    }
            //    retVal += intValue.ToString() + ";";
            //    //Fitness
            //    if(cmbActivationFuncs.SelectedIndex==-1)
            //    {
            //        MessageBox.Show("Invalid value for Fitness function!");
            //        return null;
            //    }
            //    retVal += cmbActivationFuncs.SelectedIndex.ToString() + ";"; 

            //    ///Init method
            //    if (cmbInitMethods.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Invalid value for Initalize method!");
            //        return null;
            //    }
            //    retVal += cmbInitMethods.SelectedIndex.ToString() + ";"; 

            //    //init depth
            //    if (!int.TryParse(txtInitTreeDepth.Text, out intValue))
            //    {
            //        MessageBox.Show("Invalid value for init  dept tree!");
            //        return null;
            //    }
            //    retVal += intValue.ToString() + ";";

            //    //operation depth
            //    if (!int.TryParse(txtOperationTreeDepth.Text, out intValue))
            //    {
            //        MessageBox.Show("Invalid value for operation dept tree!");
            //        return null;
            //    }
            //    retVal += intValue.ToString() + ";";


            //    //Selection Elitism
            //    if (!int.TryParse(txtElitism.Text, out intValue))
            //    {
            //        MessageBox.Show("Invalid value for operation dept tree!");
            //        return null;
            //    }
            //    retVal += intValue.ToString() + ";";


            //    //Selection method
            //    if (cmbSelectionMethods.SelectedIndex == -1)
            //    {
            //        MessageBox.Show("Invalid value for Initalize method!");
            //        return null;
            //    }
            //    retVal += cmbSelectionMethods.SelectedIndex.ToString() + ";"; 

            //    //Selection param1
            //    float floatValue;
            //    if (!float.TryParse(txtSelParam1.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for Sel param 1!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

            //    //Selection param2
            //    if (!float.TryParse(txtSelParam2.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for Sel param 2!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

            //    //Random constant from
            //    if (!float.TryParse(txtRandomConsFrom.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for Random constant From!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

            //    //Random constant to
            //    if (!float.TryParse(txtRandomConsTo.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for Random constant From!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

            //    //Random constant count
            //    if (!int.TryParse(txtRandomConstNum.Text, out intValue))
            //    {
            //        MessageBox.Show("Invalid value for operation dept tree!");
            //        return null;
            //    }
            //    retVal += intValue.ToString() + ";";

            //    //Crossover method
            //    if (!float.TryParse(txtMomentum.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for probability of Crossove.!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
               

            //    //mutation method
            //    if (!float.TryParse(txtActFunParam1.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for probability of Mutation.!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
                

            //    //reproduction method
            //    if (!float.TryParse(txtActFunParam2.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for probability of Reproduction.!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
                

            //    //Permutation method
            //    if (!float.TryParse(txtProbPermutation.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for probability of Permutation.!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
               

            //    //Encaptualtion method
            //    if (!float.TryParse(txtProbEncaptulation.Text, out floatValue))
            //    {
            //        MessageBox.Show("Invalid value for probability of Encaptulation.!");
            //        return null;
            //    }
            //    retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
              
            //    //Editing
            //    retVal += checkBox2.Checked==true? "1" : "0" + ";";
                
            //    //decimation
            //    retVal += checkBox3.Checked == true ? "1" : "0" + ";";

            //    //Storing constants in to string
            //    for (int i = 0; i < GetParameters().rConstNum; i++)
            //    {
            //        retVal += Constants[i].ToString(CultureInfo.InvariantCulture);

            //        if (i != GetParameters().rConstNum + 1)
            //            retVal += ";";
            //    }

            //    //init depth
            //    if (!int.TryParse(txtBroodSize.Text, out intValue))
            //    {
            //        MessageBox.Show("Invalid Brood SIze value!");
            //        return null;
            //    }
            //    retVal += intValue.ToString() + ";";
            //    return retVal;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return null;
            //}
        }
        #endregion    

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
    }
}
