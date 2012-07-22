﻿//////////////////////////////////////////////////////////////////////////////////////////
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

namespace GPdotNET.Tool.Common
{
    /// <summary>
    /// Class implements parameters for GPa and GP
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        #region CTor and Fields
        public SettingsPanel()
        {
            InitializeComponent();
            LoadInitMethodsinCombo();
            LoadFitnessFunsInCombo();
            LoadSelectionMethodsInCOmbo();

            //Set initial value
            cmbInitMethods.SelectedIndex = 2;
            cmbSelectionMethods.SelectedIndex = 0;
            cmbFitnessFuncs.SelectedIndex = 0;

            txtProbMutation.Text = (5.0 / 100.0).ToString();
            txtProbPermutation.Text = (5.0 / 100.0).ToString();
            txtProbReproduction.Text = (20.0 / 100.0).ToString();
            txtProbCrossover.Text = (90.0 / 100.0).ToString();
            txtProbEncaptulation.Text = (90.0 / 100.0).ToString();
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            txtElitism.Text = 1.ToString();
            
        }
        #endregion

        #region private Methods
        /// <summary>
        ///  //we need here to provide loading all class which are derived from IFitness interface
        /// on that way we hav complete customisation of th fitness functions
        /// </summary>
        private void LoadFitnessFunsInCombo()
        {
            cmbFitnessFuncs.Items.Add("MSE	-Mean square error ");
            cmbFitnessFuncs.Items.Add("RMSE	-Root mean square error ");
            cmbFitnessFuncs.Items.Add("MAE	-Mean apsolute error ");
            cmbFitnessFuncs.Items.Add("RSE	-Root square error ");
            cmbFitnessFuncs.Items.Add("RRSE	-Relative root square error ");
            cmbFitnessFuncs.Items.Add("RAE	-Root apsolute error ");
            cmbFitnessFuncs.Items.Add("rMSE	-relative MSE ");
            cmbFitnessFuncs.Items.Add("rRMSE	-relative RMSE ");
            cmbFitnessFuncs.Items.Add("rMAE	-relative MAE ");
            cmbFitnessFuncs.Items.Add("rRSE	-relative RSE ");
            cmbFitnessFuncs.Items.Add("rRRSE	-relative RRSE ");
            cmbFitnessFuncs.Items.Add("rRAE	-relative RAE ");
            cmbFitnessFuncs.Items.Add("AE	-Apsolute error ");
            cmbFitnessFuncs.Items.Add("RE	-Relative  error ");
            cmbFitnessFuncs.Items.Add("CC	-Corelation coefficient ");
            
        }

        /// <summary>
        /// Enumerate ENUM of selection methods and insert as ComboBox items
        /// so the user can easyly select.
        /// this is also handy when you extend selectionmethods and populate them automaticaly
        /// </summary>
        private void LoadInitMethodsinCombo()
        {
            //fill combo with initialization methdos
            foreach (var initM in Enum.GetValues(typeof(GPInitializationMethod)))
            {
                cmbInitMethods.Items.Add(initM.ToString());
            }
        }

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
        /// TO DO
        /// Proces of selecting fitness function form combobox
        /// </summary>
        /// <param name="selIndex"></param>
        /// <returns></returns>
        private IFitnessFunction SelectFitnessFun(int selIndex)
        {
            return new RMSEFitness(); 
        }


     
        /// <summary>
        /// When the secection method is changed, dfault parms must be set properly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectionMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSelectionMethods.SelectedItem==null)
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
#endregion

        #region Public Methods
        /// <summary>
        /// Set GUI based on the GPParameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool LoadinitialParameters(GPParameters parameters)
        {

            txtPopSize.Text = parameters.popSize.ToString();
            cmbSelectionMethods.SelectedIndex = (int)parameters.eselectionMethod;
            //cmbFitnessFuncs.SelectedIndex = (int)parameters.efitnessFunction;

            txtElitism.Text = parameters.elitism.ToString();
            cmbInitMethods.SelectedIndex = (int)parameters.einitializationMethod;
            txtSelParam1.Text = parameters.SelParam1.ToString();
            txtSelParam2.Text = parameters.SelParam1.ToString();

            txtInitTreeDepth.Text = parameters.maxInitLevel.ToString();
            txtOperationTreeDepth.Text = parameters.maxOperationLevel.ToString();


            txtProbCrossover.Text = parameters.probCrossover.ToString();
            txtProbMutation.Text = parameters.probMutation.ToString();
            txtProbReproduction.Text = parameters.probReproduction.ToString();
            //txtProbPermutation.Text = parameters.probPermutation.ToString();
            //txtProbEncaptulation.Text = "ewe";

            txtRandomConsTo.Text = parameters.rConstTo.ToString();
            txtRandomConsFrom.Text = parameters.rConstFrom.ToString();
            txtRandomConstNum.Text = parameters.rConstNum.ToString();

            radioIsParallel.Checked = parameters.bParalelGP;

            return true;
        }

        /// <summary>
        /// Return current value of GP params
        /// </summary>
        /// <returns></returns>
        public GPParameters GetParameters()
        {
            GPParameters parameters = new GPParameters();


            parameters.einitializationMethod = (GPInitializationMethod)cmbInitMethods.SelectedIndex;
            parameters.eselectionMethod = (GPSelectionMethod)cmbSelectionMethods.SelectedIndex;

            //TO DO solve fitnes setting
            parameters.GPFitness = SelectFitnessFun(cmbFitnessFuncs.SelectedIndex);
            // parameters.InitiFitness();


            if (!int.TryParse(txtPopSize.Text, out parameters.popSize))
            {
                MessageBox.Show("Invalid value for Population size!");
                return null;
            }

            if (!int.TryParse(txtElitism.Text, out parameters.elitism))
            {
                MessageBox.Show("Invalid value for Elitism!");
                return null;
            }

            if (!float.TryParse(txtSelParam1.Text, out parameters.SelParam1))
            {
                MessageBox.Show("Invalid value for Parameter 1!");
                return null;
            }

            if (!float.TryParse(txtSelParam2.Text, out parameters.SelParam2))
            {
                MessageBox.Show("Invalid value for Parameter 2!");
                return null;
            }



            if (!int.TryParse(txtInitTreeDepth.Text, out parameters.maxInitLevel))
            {
                MessageBox.Show("Invalid value for Initial tree depth!");
                return null;
            }

            if (!int.TryParse(txtOperationTreeDepth.Text, out parameters.maxOperationLevel))
            {
                MessageBox.Show("Invalid value for Operation tree depth!");
                return null;
            }

            parameters.bParalelGP = radioIsParallel.Checked;


            if (!float.TryParse(txtRandomConsFrom.Text, out parameters.rConstFrom))
            {
                MessageBox.Show("Invalid value for interval to!");
                return null;
            }

            if (!float.TryParse(txtRandomConsTo.Text, out parameters.rConstTo))
            {
                MessageBox.Show("Invalid value for interval from!");
                return null;
            }

            if (!int.TryParse(txtRandomConstNum.Text, out parameters.rConstNum))
            {
                MessageBox.Show("Invalid value for Number of Constants!");
                return null;
            }

            if (parameters.rConstNum < 0)
            {
                MessageBox.Show("Invalid value for Number of Constants!");
                return null;
            }


            if (!float.TryParse(txtProbCrossover.Text, out parameters.probCrossover))
            {
                MessageBox.Show("Invalid value for Crossover probability!");
                return null;
            }

            if (!float.TryParse(txtProbMutation.Text, out parameters.probMutation))
            {
                MessageBox.Show("Invalid value for  Mutation probability!");
                return null;
            }

            if (!float.TryParse(txtProbReproduction.Text, out parameters.probReproduction))
            {
                MessageBox.Show("Invalid value for  Reproduction probability!");
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
            groupBox6.Enabled = p;
            groupBox8.Enabled = p;

        }

        /// <summary>
        /// Setting number of random constats
        /// </summary>
        /// <param name="p"></param>
        public void SetNumOfConstance(int p)
        {
            txtRandomConstNum.Text = p.ToString();
        }

        /// <summary>
        /// Desrilization of parametars
        /// </summary>
        /// <param name="p"></param>
        public void SetParameters(string p)
        {
            var pstr = p.Split(';');
            GPParameters parameters = new GPParameters();
            try
            {

                //PopSize
                txtPopSize.Text=pstr[0];
                //Fitness
                int temp=0;
                if (!int.TryParse(pstr[1], out temp))
                    temp = 0;
                cmbFitnessFuncs.SelectedIndex = temp;

                ///Init method
                temp = 0;
                if (!int.TryParse(pstr[2], out temp))
                    temp = 0;
                cmbInitMethods.SelectedIndex = temp;

                //init depth
                txtInitTreeDepth.Text = pstr[3];

                //operation depth
                txtOperationTreeDepth.Text = pstr[4];


                //Selection Elitism
                txtElitism.Text = pstr[5];

                //Selection method
                temp = 0;
                if (!int.TryParse(pstr[6], out temp))
                    temp = 0;
                cmbSelectionMethods.SelectedIndex = temp;

                //Selection param1
                txtSelParam1.Text = pstr[7];

                //Selection param2
                txtSelParam2.Text = pstr[8];

                //Random constant from
                txtRandomConsFrom.Text = pstr[9];

                //Random constant to
                txtRandomConsTo.Text = pstr[10];

                //Random constant count
                txtRandomConstNum.Text = pstr[11];

                //Crossover method
                float val = 0;
                if (!float.TryParse(pstr[12],System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                    val = 0;
                txtProbCrossover.Text = val.ToString();

                //mutation method
                val = 0;
                if (!float.TryParse(pstr[13], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                    val = 0;
                txtProbMutation.Text = val.ToString();

                //reproduction method
                val = 0;
                if (!float.TryParse(pstr[14], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                    val = 0;
                txtProbReproduction.Text = val.ToString();

                //Permutation method
                val = 0;
                if (!float.TryParse(pstr[15], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                    val = 0;
                txtProbPermutation.Text = val.ToString();

                //Encaptualtion method
                val = 0;
                if (!float.TryParse(pstr[16], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                    val = 0;
                txtProbEncaptulation.Text = val.ToString();


                //Editing
                temp = 0;
                if (!int.TryParse(pstr[17], out temp))
                    temp = 0;
                checkBox2.Checked = temp==0?false:true;
                //decimation
                temp = 0;
                if (!int.TryParse(pstr[18], out temp))
                    temp = 0;
                checkBox2.Checked = temp == 0 ? false : true;

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
               
                //PopSize
                int intValue = 0;
                if (!int.TryParse(txtPopSize.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for Population size!");
                    return null;
                }
                retVal += intValue.ToString() + ";";
                //Fitness
                if(cmbFitnessFuncs.SelectedIndex==-1)
                {
                    MessageBox.Show("Invalid value for Fitness function!");
                    return null;
                }
                retVal += cmbFitnessFuncs.SelectedIndex.ToString() + ";"; 

                ///Init method
                if (cmbInitMethods.SelectedIndex == -1)
                {
                    MessageBox.Show("Invalid value for Initalize method!");
                    return null;
                }
                retVal += cmbInitMethods.SelectedIndex.ToString() + ";"; 

                //init depth
                if (!int.TryParse(txtInitTreeDepth.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for init  dept tree!");
                    return null;
                }
                retVal += intValue.ToString() + ";";

                //operation depth
                if (!int.TryParse(txtOperationTreeDepth.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for operation dept tree!");
                    return null;
                }
                retVal += intValue.ToString() + ";";


                //Selection Elitism
                if (!int.TryParse(txtElitism.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for operation dept tree!");
                    return null;
                }
                retVal += intValue.ToString() + ";";


                //Selection method
                if (cmbSelectionMethods.SelectedIndex == -1)
                {
                    MessageBox.Show("Invalid value for Initalize method!");
                    return null;
                }
                retVal += cmbSelectionMethods.SelectedIndex.ToString() + ";"; 

                //Selection param1
                float floatValue;
                if (!float.TryParse(txtSelParam1.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for Sel param 1!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Selection param2
                if (!float.TryParse(txtSelParam2.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for Sel param 2!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Random constant from
                if (!float.TryParse(txtRandomConsFrom.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for Random constant From!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Random constant to
                if (!float.TryParse(txtRandomConsTo.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for Random constant From!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";

                //Random constant count
                if (!int.TryParse(txtRandomConstNum.Text, out intValue))
                {
                    MessageBox.Show("Invalid value for operation dept tree!");
                    return null;
                }
                retVal += intValue.ToString() + ";";

                //Crossover method
                if (!float.TryParse(txtProbCrossover.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for probability of Crossove.!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
               

                //mutation method
                if (!float.TryParse(txtProbMutation.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for probability of Mutation.!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
                

                //reproduction method
                if (!float.TryParse(txtProbReproduction.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for probability of Reproduction.!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
                

                //Permutation method
                if (!float.TryParse(txtProbPermutation.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for probability of Permutation.!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
               

                //Encaptualtion method
                if (!float.TryParse(txtProbEncaptulation.Text, out floatValue))
                {
                    MessageBox.Show("Invalid value for probability of Encaptulation.!");
                    return null;
                }
                retVal += floatValue.ToString(CultureInfo.InvariantCulture) + ";";
              
                //Editing
                retVal += checkBox2.Checked==true? "1" : "0" + ";";
                
                //decimation
                retVal += checkBox3.Checked == true ? "1" : "0" + ";";

                return retVal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
