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
namespace GPNETLib
{
    
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
        TournamentSelection=3
    }
    [Serializable]
    public enum RawFitness
    {
        MR,//Višesturka regresija
        R2,//Koeficijent determinacije
        RMS,//Kvadratna greska
        SE,//Rezidual
        RR,//Relativni rezidual
        RES//Kvadratna greska

    }
    [Serializable]
    public class GPParameters
    {
        //Initialization metods
        public EInitializationMethod einitializationMethod;
        public int maxInitLevel;

        //Selection methods
        public ESelectionMethod eselectionMethod;

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
            eselectionMethod = ESelectionMethod.EliteSelection;
            maxInitLevel =7;
            maxCossoverLevel = 10;
            maxMutationLevel = 10;
            probCrossover = 1.0F;
            probMutation = 1.0F;
            probPermutation = 1.0F;
            probReproduction = 0.20F;
        }
    }
}
