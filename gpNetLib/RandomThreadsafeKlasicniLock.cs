using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GP_NET
{
    /// <summary>
    /// Thread Safe Generator slucajnih brojeva. Obzirom da klasa 
    /// Random nije ThreadSafe potrebno je zakljucati generiranje 
    /// slucajnog broja prije samog generiranja
    /// </summary>
    [Serializable]
    public class ThreadSafeRandom
    {
       // private static Random random;
        private static NPack.MersenneTwister random;

        public ThreadSafeRandom()
        {
            //random = new Random((int)DateTime.Now.Ticks);
            random = new NPack.MersenneTwister();
        }
        public ThreadSafeRandom(int tick)
        {
            //random = new Random(tick);
            random = new NPack.MersenneTwister(tick);
        }
        public int Next()
        {
            lock (random)
            {
                return random.Next();
            }
        }
        public int Next(int maxValue)
        {
            lock (random)
            {
                return random.Next(maxValue);
            }
        }
        public int Next(int minValue, int maxValue)
        {
            lock (random)
            {
                return random.Next(minValue, maxValue);
            }
        }
        public void NextBytes(byte[] buffer)
        {
            lock (random)
            {
                random.NextBytes(buffer);
            }
        }
        public double NextDouble()
        {
            lock (random)
            {
                return random.NextDouble();
            }
        }

    }
}
