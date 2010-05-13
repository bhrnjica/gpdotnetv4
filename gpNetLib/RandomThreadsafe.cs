// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// gpNETLib 
using System;
using NPack;
namespace GPNETLib
{
    ///Based on implementation of Random ThreadSafe http://blogs.msdn.com/pfxteam/archive/2009/02/19/9434171.aspx
    /// <summary>
    /// Thread Safe Generator slucajnih brojeva. Obzirom da klasa 
    /// Random nije ThreadSafe potrebno je zakljucati generiranje 
    /// slucajnog broja prije samog generiranja
    /// </summary>
    [Serializable]
    public class ThreadSafeRandom
    {
        private static MersenneTwister _global = new MersenneTwister();
        [ThreadStatic]
        private static MersenneTwister _local; 

        public ThreadSafeRandom()
        {
        }

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
        }

    }
}
