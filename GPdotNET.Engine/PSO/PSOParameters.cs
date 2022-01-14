//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET Artifical Intelligence Tool                                                 //
// Copyright 2006-2014 Bahrudin Hrnjica                                                 //
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNET.Core.Interfaces;
using GPdotNET.Core.Experiment;
namespace GPdotNET.Engine.PSO
{
    /// <summary>
    /// This class represent parameters which user can modify in order to get the best result from the algoritm.
    /// </summary>
    public class PSOParameters
    {
        //weights
        public double m_IWeight;// inertia weight
        public double m_LWeight;// cognitive/local weight
        public double m_GWeight;// social/global weight
        
        public int m_Dimension;


        public int m_ParticlesNumber;//number of particles

        //lower constrains
        public double m_Min;
        public double m_Max;

        

        public PSOParameters()
        {
            //
            m_IWeight = 0.77564;
            m_LWeight = 1.57564;
            m_GWeight = 1.37564;
            m_ParticlesNumber = 10;
            m_Min = -5.0;
            m_Max = 5.0;
   
        }
    }
}
