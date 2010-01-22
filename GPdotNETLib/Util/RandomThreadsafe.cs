using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPack;
namespace GPdotNETLib
{
    ///Nova implementacija Random ThreadSafe http://blogs.msdn.com/pfxteam/archive/2009/02/19/9434171.aspx
    /// <summary>
    /// Thread Safe Generator slucajnih brojeva. Obzirom da klasa 
    /// Random nije ThreadSafe potrebno je zakljucati generiranje 
    /// slucajnog broja prije samog generiranja
    /// </summary>
    [Serializable]
    public class ThreadSafeRandom
    {
       // private static Random random;
    //    private static NPack.MersenneTwister random;

        private static MersenneTwister _global = new MersenneTwister();
        [ThreadStatic]
        private static MersenneTwister _local; 

        public ThreadSafeRandom()
        {
            //random = new Random((int)DateTime.Now.Ticks);
            //random = new NPack.MersenneTwister();
        }
       /* public ThreadSafeRandom(int tick)
        {
            //random = new Random(tick);
            random = new NPack.MersenneTwister(tick);
        }*/
        public int Next()
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            return inst.Next(); 
/*
            lock (random)
            {
                return random.Next();
            }*/
        }
        public int Next(int maxValue)
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            return inst.Next(maxValue); 
          /*  lock (random)
            {
                return random.Next(MaxValue);
            }*/
        }
        public int Next(int minValue, int maxValue)
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            return inst.Next(minValue, maxValue);
           /* lock (random)
            {
                return random.Next(MinValue, MaxValue);
            }*/
        }
        public void NextBytes(byte[] buffer)
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            inst.NextBytes(buffer);
           /* lock (random)
            {
                random.NextBytes(buffer);
            }*/
        }
        public double NextDouble()
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            return inst.NextDouble();
           /* lock (random)
            {
                return random.NextDouble();
            }*/
        }
        public double NextDouble(double minVal, double maxVal)
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            return (minVal+inst.NextDouble()*(maxVal-minVal));
            /* lock (random)
             {
                 return random.NextDouble();
             }*/
        }
        public double NextDouble(double minVal, double maxVal, bool includeMax)
        {
            MersenneTwister inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new MersenneTwister(seed);
            }
            return (minVal + inst.NextDouble(includeMax) * (maxVal - minVal));
            /* lock (random)
             {
                 return random.NextDouble();
             }*/
        }

    }
}
