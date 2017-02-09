using GPdotNET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace GPdotNET.Engine.PSO
{
    public delegate double CostFunction(double[] inputs);
    public class ParticleSwarm
    {
         
        PSOParameters m_Parameters;
        CostFunction m_CostFunction;
        Particle[] m_Particles;

        double m_MinVel;  // velocities
        double m_MaxVel;
        double m_MinLoc; // for each weight
        double m_MaxLoc;


        //helper for handling with locations and velocities
        public double[] m_Location; // equivalent to x-Values and/or solution
        public double m_Fitness;
        public double[] m_Velocities;

        public double[] m_BestLocations; // best position found so far by this Particle
        public double m_BestFitness;

        public double[] m_BestGlobalLocation; // best position found so far by this Particle
        public double m_BestGlobalFitness;



        public ParticleSwarm(PSOParameters param, CostFunction fun)
        {
            if (fun == null)
                throw new Exception("Cost Function cannot be null."); 
            m_Parameters = param;
            m_CostFunction = fun;

            //initialize starting position and velocitiy
            m_MinLoc = m_Parameters.m_Min;// -5.0; // for each weight
            m_MaxLoc = m_Parameters.m_Max;
            m_MinVel = -0.1 * m_Parameters.m_Max;  // velocities
            m_MaxVel = 0.1 * m_Parameters.m_Max; 
        }

        public float InitSwarm()
        {

            m_Particles = new Particle[m_Parameters.m_ParticlesNumber];

            m_BestGlobalLocation = new double[m_Parameters.m_Dimension]; // best solution found by any particle in the swarm. 
            //implicit initialization to all 0.0
            m_BestGlobalFitness = double.MaxValue; 

            double newFitness = 0;
            // initialize each Particle in the swarm with random locations and velocities
            for (int i = 0; i < m_Particles.Length; ++i) 
            {
                //generate random position for each dimenzions
                double[] randomPosition = new double[m_Parameters.m_Dimension];
                for (int j = 0; j < randomPosition.Length; ++j)
                    randomPosition[j] = Globals.radn.NextDouble(m_MinLoc, m_MaxLoc);
                
                //calculate cost function
                newFitness = m_CostFunction(randomPosition); 


                double[] randomVelocity = new double[m_Parameters.m_Dimension];

                for (int j = 0; j < randomVelocity.Length; ++j)
                    randomVelocity[j] = Globals.radn.NextDouble(m_MinVel,m_MaxVel);

                //initialize particle oafte position and velicity initialization
                m_Particles[i] = new Particle(randomPosition, newFitness, randomVelocity, randomPosition, newFitness);

                // exchange the best fitness with the current one
                if (m_Particles[i].m_Fitness < m_BestGlobalFitness)
                {
                    m_BestGlobalFitness = m_Particles[i].m_Fitness;
                    m_Particles[i].m_Locations.CopyTo(m_BestGlobalLocation, 0);
                }
            }

            return (float)newFitness;
        }

        public float RunSwarm()
        {
            //new values for the current running iteration  Index of (k+1)
            double[] newVelocity = new double[m_Parameters.m_Dimension];
            double[] newPosition = new double[m_Parameters.m_Dimension];
            double newFitness=0;

            //for each particle in the Swarm
            for (int i = 0; i < m_Particles.Length; ++i) // each Particle
            {
                var currParticle = m_Particles[i];

                // each x value of the velocity
                for (int j = 0; j < currParticle.m_Velocities.Length; ++j) 
                {
                   var  r1 = Globals.radn.NextDouble();
                   var  r2 = Globals.radn.NextDouble();

                   // calculation of the new velocity
                   // Vk+1= iw*Vk + c1*r1*(best-Xk) + c2*r2*(gBest-Xk)
                   //
                    newVelocity[j] =

                      (m_Parameters.m_IWeight * currParticle.m_Velocities[j]) +                                             //iw*Vk

                      (m_Parameters.m_GWeight * r1 * (currParticle.m_BestLocation[j] - currParticle.m_Locations[j])) +    //c1*r1*(best-Xk)

                      (m_Parameters.m_LWeight * r2 * (m_BestGlobalLocation[j] - currParticle.m_Locations[j]));          //c2*r2*(gBest-Xk)
                            
                    //constrains for the velocity
                    if (newVelocity[j] < m_MinVel)
                        newVelocity[j] = m_MinVel;
                    else if (newVelocity[j] > m_MaxVel)
                        newVelocity[j] = m_MaxVel;    
                }

                newVelocity.CopyTo(currParticle.m_Velocities, 0);

                for (int j = 0; j < currParticle.m_Locations.Length; ++j)
                {
                    newPosition[j] = currParticle.m_Locations[j] + newVelocity[j];  // compute new position

                    //constains for the location
                    if (newPosition[j] < m_MinLoc)
                        newPosition[j] = m_MinLoc;
                    else if (newPosition[j] > m_MaxLoc)
                        newPosition[j] = m_MaxLoc;
                }

                newPosition.CopyTo(currParticle.m_Locations, 0);

                //
                //calculate cost function
                newFitness = m_CostFunction(newPosition); // smaller values better
                currParticle.m_Fitness = newFitness;

                if (newFitness < currParticle.m_BestFitness) // new particle best?
                {
                    newPosition.CopyTo(currParticle.m_BestLocation, 0);
                    currParticle.m_BestFitness = newFitness;
                }

                if (newFitness < m_BestGlobalFitness) // new global best?
                {
                    newPosition.CopyTo(m_BestGlobalLocation, 0);
                    m_BestGlobalFitness = newFitness;
                }

            }

            return (float)newFitness;
        }

        internal bool ParticleFromSTring(string[] values)
        {
            int counter = 0;
            //check if the index has right value
            if (values[counter++] != "particles")
                throw new Exception("The File is corrupt!");

            ///
            m_Particles = new Particle[m_Parameters.m_ParticlesNumber];
            var persistedPosition = new double[m_Parameters.m_Dimension];
            var persistedVelocities = new double[m_Parameters.m_Dimension];
            var persistedBestPosition = new double[m_Parameters.m_Dimension];

            //
            for (int i = 0; i < m_Parameters.m_ParticlesNumber; i++)
            {

                //best  fitness
               var  bestFitness = double.Parse(values[counter++], CultureInfo.InvariantCulture);// par.m_BestFitness.ToString(CultureInfo.InvariantCulture) + ";";
               var  newFitness = double.Parse(values[counter++], CultureInfo.InvariantCulture);// par.m_Fitness.ToString(CultureInfo.InvariantCulture) + ";";

                for (int j = 0; j < m_Parameters.m_Dimension; j++)
                    persistedPosition[j] = double.Parse(values[counter++], CultureInfo.InvariantCulture);
                for (int j = 0; j < m_Parameters.m_Dimension; j++)
                    persistedVelocities[j] = double.Parse(values[counter++], CultureInfo.InvariantCulture);
                for (int j = 0; j < m_Parameters.m_Dimension; j++)
                    persistedBestPosition[j] = double.Parse(values[counter++], CultureInfo.InvariantCulture);

                //initialize particle by passing position and velicity values
                m_Particles[i] = new Particle(persistedPosition, newFitness, persistedVelocities, persistedBestPosition, bestFitness);
            }
            //
            return true;
        }

        internal string SaveAlgoritm()
        {

            string str = "particles;";

            for (int i=0; i < m_Particles.Length; i++)
            {
                var par = m_Particles[0];
                //best  fitness
                str += par.m_BestFitness.ToString(CultureInfo.InvariantCulture) + ";";
                str += par.m_Fitness.ToString(CultureInfo.InvariantCulture) + ";";

                foreach (var l in par.m_Locations)
                    str += l.ToString(CultureInfo.InvariantCulture) + ";";

                foreach (var v in par.m_Velocities)
                    str += v.ToString(CultureInfo.InvariantCulture) + ";";

                foreach (var bl in par.m_BestLocation)
                    str += bl.ToString(CultureInfo.InvariantCulture) + ";";

            }

            return str;
        }

    } 
}
