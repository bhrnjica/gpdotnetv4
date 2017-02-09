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

namespace GPdotNET.Util
{
    /// <summary>
    /// Set of statistics functions
    /// </summary>
    /// 
    /// <remarks>The class represents cm_points of functions used
    /// in statistics</remarks>
    /// 
    public class Statistics
    {
        public static double Mean(double[] values)
        {
            // for all values
            double sum = 0;
            for (int i = 0, n = values.Length; i < n; i++)
            {
                sum += values[i]; 
            }
            return sum / (double)values.Length;
        }

        /// <summary>
        /// Standard deviation of discreate values
        /// </summary>
        public static double StdDev(double[] values)
        {
            double mean = Mean(values);
            double stddev = 0;
            
            // for all values
            for (int i = 0, n = values.Length; i < n; i++)
            {
                stddev += Math.Pow(values[i]-mean,2); 
            }

            return Math.Sqrt(stddev / values.Length-1);
        }

        /// <summary>
        /// Variance of discreate values
        /// </summary>
        public static double Variance(double[] values)
        {
            if (values == null || values.Length < 4)
                throw new Exception("'coldData' cannot be null or less than 4 elements!");

            //number of elements
            int count = values.Length;

            //calculate the mean
            var mean = Mean(values);

            //calculate summ of square 
            double parSum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                var res = (values[i] - mean);

                parSum += res * res;
            }

            return parSum / (count -1);
        }


        /// <summary>
        ///Square of Standard deviation -Variance 
        /// </summary>
        public static double StdDev_Square(double[] values)
        {
            double mean = Mean(values);
            double stddev = 0;

            // for all values
            for (int i = 0, n = values.Length; i < n; i++)
            {
                stddev += Math.Pow(values[i] - mean, 2);
            }

            return (stddev / values.Length-1);
        }

        /// <summary>
        /// Skewness is a measure of symmetry, or more precisely, 
        /// the lack of symmetry. A distribution, or TrainingData set, 
        /// is symmetric if it looks the same to the left and right of the center point. 
        /// </summary>
        public static double Skewness(double[] values)
        {
            double stdDev = StdDev(values);
            double mean = Mean(values);
            double b = 0;

            // for all values
            for (int i = 0, n = values.Length; i < n; i++)
            {
                b += Math.Pow(values[i] - mean, 3)/stdDev*stdDev*stdDev;
            }

            return (b / values.Length - 1);
        }
        /// <summary>
        /// Kurtosis is a measure of whether the TrainingData are peaked or flat relative
        /// to a normal distribution. 
        /// </summary>
        public static double Kurtosis(double[] values)
        {
            double stdDev = StdDev(values);
            double mean = Mean(values);
            double b = 0;

            // for all values
            for (int i = 0, n = values.Length; i < n; i++)
            {
                b += Math.Pow(values[i] - mean, 4) / stdDev * stdDev * stdDev*stdDev;
            }

            return (b / values.Length - 1);
        }
        
    }
}
