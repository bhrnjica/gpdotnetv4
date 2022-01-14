using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNET.Engine.PSO
{
    public class Particle
    {
        
        public double m_Fitness;
        public double m_BestFitness; //global gBest

        public double[] m_Velocities;
        public double[] m_Locations; // 
        public double[] m_BestLocation; // local pBest
       

        public Particle(double[] position, double fitness, double[] velocity, double[] bestPosition, double bestFitness)
        {
            this.m_Locations = new double[position.Length];
            position.CopyTo(this.m_Locations, 0);


            this.m_Fitness = fitness;
            this.m_Velocities = new double[velocity.Length];
            velocity.CopyTo(this.m_Velocities, 0);


            this.m_BestLocation = new double[bestPosition.Length];
            bestPosition.CopyTo(this.m_BestLocation, 0);


            this.m_BestFitness = bestFitness;
        }
    } 
}
