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

    //Function representation in GP, also know as primitive computer program.
     public class GPFunction
    {
       
        //If selected = true,it is included of the current gprun
        public bool Selected { get; set; }

        //Number of aritry
        public int Aritry { get; set; }

        //Function name
        public string Name { get; set; }

        //Description of the function
        public string Description { get; set; }

        //DEfinition of the function
        public string Definition { get; set; }

        //Excel definition of the function. GP model has to be provided in excel expression
        public string ExcelDefinition { get; set; }

        //R Lang definition of the function. GP model has to be provided in R expression
        public string RDefinition { get; set; }

        //Methematica definition of the function. GP model has to be provided in excel expression
        public string MathematicaDefinition { get; set; }

        //Read only means it is default function in current GPdotNet version
        public bool IsReadOnly { get; set; }

        //The function behaves as a distribution 
        // mean, deviation, rsquare etc
        public bool IsDistribution { get; set; }

        //Relative probability to be chosen
        public int Weight { get; set; }

        //Function ID
        public int ID { get; set; }

        //Parameters separate with semicolon
        public string Parameters { get; set; }
        

        /// <summary>
        /// Override ToString member
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

    }
}
