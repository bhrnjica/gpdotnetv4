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
namespace GPdotNET.Core
{
    public enum Algorithm
    {
        GP=0,
        GA,
    }

    public enum GAChromosome
    {
        Binary=0,
        Continue=1,
        TSP=2,
        ALOC=3,
        TP=4,
    }
    public enum GPInitializationMethod
    {
        FullInitialization = 0,
        GrowInitialization=1,
        HalfHalfInitialization=2
    }
    public enum GPSelectionMethod
    {
       // EliteSelection=0,
        FitnessProportionateSelection = 0,
        Rankselection=1,
        TournamentSelection=2,
        StochasticUniversalSelection=3,
        FUSSelection=4,
        SkrgicSelection=5
    }
    
    public class GPParameters
    {
        //Type of algortihm
        public Algorithm algorithmType;
        //Kind of GA chromosome
        public GAChromosome chromosomeKind;

        //Kind of optimization
        public int OptType = 0;
        //POpulation size
        public int popSize;

        //Initialization metods
        public GPInitializationMethod einitializationMethod;
        public int maxInitLevel;

       
        //Selection methods and parameters
        public GPSelectionMethod eselectionMethod;
        public float SelParam1;
        public float SelParam2;
        public int elitism;

        //Reproduction prcentige
        public float probReproduction;

        //Probability of oparations
        public float probCrossover;
        public float probMutation;

        //enable disable protected operations
        public bool isProtectedOperationEnabled;

        //crossover improvements params
        public int broodSize;

        //Maximum level during operation
        public int maxOperationLevel;

        //Fitness method used durig GP
        public IFitnessFunction GPFitness;

        public float rConstFrom, rConstTo;
        public int rConstNum;

        public bool bParalelGP;
        //    public float probPermutation;
        //    public float                    probEncaptilation;
        //    public bool                     bEditing;
        //    public bool                     bDecimation;
        ///   public float probReproduction;

      

        public GPParameters()
        {
            //initial value of GA 
            algorithmType = Algorithm.GP;
            chromosomeKind = GAChromosome.Continue;

            einitializationMethod = GPInitializationMethod.FullInitialization;
            eselectionMethod = GPSelectionMethod.Rankselection;
            GPFitness = null;//new RMSEFitness();
            SelParam1 = 3;
            SelParam2 = 0;
            elitism = 1;
            maxInitLevel =5;
            probCrossover = 0.99F;
            probMutation = 0.10F;
            probReproduction = 0.20F;
            popSize = 1000;
            broodSize = 1;
            isProtectedOperationEnabled = true;
            rConstFrom = -1;
            rConstTo = 1;
            rConstNum = 5;

            bParalelGP = false;
        }

        public void SetFitnessFunction(IFitnessFunction fitness)
        {
            GPFitness=fitness;
        }
    }
}
