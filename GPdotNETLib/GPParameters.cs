using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gpNetLib;

namespace GPdotNETLib
{
    using System;
    using gpNetLib.Selections;
    [Serializable]
    public enum EInitializationMethod
    {
        FullInitialization = 0,
        GrowInitialization=1,
        HalfHalfInitialization=2
    }
    [Serializable]
    public enum ESelectionMethod
    {
        EliteSelection=0,
        Rankselection=1,
        RouletteWheelSelection=2,
        TournamentSelection=3,
        StochasticSelection=4,
        FUSSSelection=5,
        SkrgicSelection=6
    }
    [Serializable]
    public enum EFitnessFunction
    {
        MSE=0,//Višesturka regresija
        RMSE,//Koeficijent determinacije
        MAE,//Kvadratna greska
        RSE,//Rezidual
        RRSE,//Relativni rezidual
        RAE,//Kvadratna greska
        AE,//Apsolute error
        AEH,//Apsolute error / hits
        RE, //Relative  error
        REH, //Relative error/ hits
        rMSE, 
        rRMSE,
        rMAE,
        rRSE,
        rRRSE,
        rRAE,
        PR,//Pearson R square
        CC//Corelation coefficient

    }
    [Serializable]
    public class GPParameters
    {
        //Initialization metods
        public EInitializationMethod einitializationMethod;
        public int maxInitLevel;
        
        //Selection methods
        private ESelectionMethod esm;
        public ESelectionMethod eselectionMethod
        {
            get
            {
                return esm;
            }
            set
            {
                if (esm == value)
                    return;
                esm = value;
                GPSelectionMethod = SelectionMethodFromEnum(value);
            }
        }
        //Fitness Function
        private EFitnessFunction eff;
        public EFitnessFunction efitnessFunction
        {
            get
            {
                return eff;
            }
            set 
            {
                if (eff == value)
                    return;
                eff = value;
                GPFitness = FitnessFromEnum(value);
            }
        }

        public IFitnessFunction GPFitness;
        public ISelection GPSelectionMethod;

        //Primary oparation
        public float probCrossover;
        public int maxCossoverLevel;

        //Secondary Operation
        public float probMutation;
        public int maxMutationLevel;
        public float probPermutation;
        //    public float                    probEncaptilation;
        //    public bool                     bEditing;
        //    public bool                     bDecimation;
        public float probReproduction;

        public GPParameters()
        {
            einitializationMethod = EInitializationMethod.FullInitialization;
            eselectionMethod = ESelectionMethod.TournamentSelection;
            efitnessFunction = EFitnessFunction.MSE;
            maxInitLevel =5;
            maxCossoverLevel = 5;
            maxMutationLevel = 5;
            probCrossover = 1.0F;
            probMutation = 1.0F;
            probPermutation = 1.0F;
            probReproduction = 0.20F;
            GPFitness = FitnessFromEnum(efitnessFunction);
            GPSelectionMethod = SelectionMethodFromEnum(eselectionMethod);
        }
        public IFitnessFunction FitnessFromEnum(EFitnessFunction eFitnessFunction)
        {
            IFitnessFunction gpFitness;
            //Akonije definisan fitness uzmi standardnu MSE- Kvadratnu grešku
            switch (eFitnessFunction)
            {

                case EFitnessFunction.MSE:
                    gpFitness = new MSE_Fitness();
                    break;
                case EFitnessFunction.RMSE:
                    gpFitness = new RMSEFitness();
                    break;
                case EFitnessFunction.MAE:
                    gpFitness = new MAEFitness();
                    break;
                case EFitnessFunction.RSE:
                    gpFitness = new RSEFitness();
                    break;
                case EFitnessFunction.RRSE:
                    gpFitness = new RRSEFitness();
                    break;
                case EFitnessFunction.RAE:
                    gpFitness = new RAEFitness();
                    break;
                case EFitnessFunction.AE:
                    gpFitness = new AESRFitness();
                    break;
                case EFitnessFunction.AEH:
                    gpFitness = new AEHitsFitness();
                    break;
                case EFitnessFunction.RE:
                    gpFitness = new RESRFitness();
                    break;
                case EFitnessFunction.REH:
                    gpFitness = new REHitsFitness();
                    break;
                case EFitnessFunction.rMSE:
                    gpFitness = new r_MSEFitness();
                    break;
                case EFitnessFunction.rRMSE:
                    gpFitness = new r_MSEFitness();
                    break;
                case EFitnessFunction.rMAE:
                    gpFitness = new r_MAEFitness();
                    break;
                case EFitnessFunction.rRSE:
                    gpFitness = new r_RSEFitness();
                    break;
                case EFitnessFunction.rRRSE:
                    gpFitness = new r_RRSEFitness();
                    break;
                case EFitnessFunction.rRAE:
                    gpFitness = new r_RAEFitness();
                    break;
                case EFitnessFunction.PR:
                    gpFitness = new PearsonsCFitness();
                    break;
                case EFitnessFunction.CC:
                    gpFitness = new CCFitness();
                    break;
                default:
                    gpFitness = new MSE_Fitness();
                    break;
            }
            return gpFitness;
        }
        public ISelection SelectionMethodFromEnum(ESelectionMethod eSelectionMethod)
        {
            ISelection gpSelectionMethod;
            
            switch (eSelectionMethod)
            {

                case ESelectionMethod.EliteSelection:
                    gpSelectionMethod = new EliteSelection();
                    break;
                case ESelectionMethod.Rankselection:
                    gpSelectionMethod = new RankSelection();
                    break;
                case ESelectionMethod.RouletteWheelSelection:
                    gpSelectionMethod = new RouletteWheelSelection();
                    break;
                case ESelectionMethod.TournamentSelection:
                    gpSelectionMethod = new TournmentSelection();
                    break;
                case ESelectionMethod.StochasticSelection:
                    gpSelectionMethod = new StohasticUniversalSelection();
                    break;
                case ESelectionMethod.FUSSSelection:
                    gpSelectionMethod = new FUSSSelection();
                    break;
                case ESelectionMethod.SkrgicSelection:
                    gpSelectionMethod = new SkrgicSelection();
                    break;
                default:
                    gpSelectionMethod = new EliteSelection();
                    break;
            }
            return gpSelectionMethod;
        }
    }
}
