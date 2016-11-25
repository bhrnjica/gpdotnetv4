//--------------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved. 
// 
//  File: ThreadSafeRandom.cs
//
//--------------------------------------------------------------------------

using System;
#if WINDOWS_APP
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
#else
using System.Security.Cryptography;
#endif

using System.Threading;


namespace GPdotNET.Core
{
    /// <summary>
    /// Represents a thread-safe, pseudo-random number generator.
    /// </summary>
    public sealed class ThreadSafeRandom : Random, IDisposable
    {
        public void Dispose()
        {
#if WINDOWS_APP

#else

            _global.Dispose();

#endif

            _local.Dispose();
            GC.SuppressFinalize(this);
        }


#if WINDOWS_APP
        
#else
        /// <summary>Seed provider.</summary>
        private static readonly RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
#endif



        /// <summary>The underlyin provider of randomness, one instance per thread, initialized with _global.</summary>
        private ThreadLocal<Random> _local = new ThreadLocal<Random>(() =>
        {
            byte[] buffer = new byte[4];

#if WINDOWS_APP
                IBuffer randomBuffer = CryptographicBuffer.GenerateRandom(250);
                CryptographicBuffer.CopyToByteArray(randomBuffer, out buffer);
#else

            _global.GetBytes(buffer); // RNGCryptoServiceProvider is thread-safe for use in this manner

#endif

            return new Random(BitConverter.ToInt32(buffer, 0));
        });


        /// <summary>Returns a nonnegative random number.</summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than MaxValue.</returns>
        public override int Next()
        {
            return _local.Value.Next();
        }

        /// <summary>Returns a nonnegative random number less than the specified maximum.</summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero. 
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than maxValue; 
        /// that is, the range of return values ordinarily includes zero but not maxValue. However, 
        /// if maxValue equals zero, maxValue is returned.
        /// </returns>
        public override int Next(int maxValue)
        {
            return _local.Value.Next(maxValue);
        }

        /// <summary>Returns a random number within a specified range.</summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue; 
        /// that is, the range of return values includes minValue but not maxValue. 
        /// If minValue equals maxValue, minValue is returned.
        /// </returns>
        public override int Next(int minValue, int maxValue)
        {
            return _local.Value.Next(minValue, maxValue);
        }
        /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public override void NextBytes(byte[] buffer)
        {
            _local.Value.NextBytes(buffer);
        }
        /// <summary>Returns a random number between 0.0 and 1.0.</summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
        public override double NextDouble()
        {
            return _local.Value.NextDouble();
        }
        public double NextDouble(double minValue, double maxValue)
        {
            return minValue + _local.Value.NextDouble() * (maxValue - minValue);
        }
    }
}
/*
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
*/