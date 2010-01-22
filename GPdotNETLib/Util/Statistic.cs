using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPdotNETLib
{
    /// <summary>
    /// Set of statistics functions
    /// </summary>
    /// 
    /// <remarks>The class represents collection of functions used
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
        /// Calculate standard deviation of discreate values is Rooth Mean Square error
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
        /// Calculate standard deviation of discreate values is Rooth Mean Square error
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
        /// to a normal distribution. That is, TrainingData sets with high kurtosis tend to
        /// have a distinct peak near the AverageValue, decline rather rapidly, and have 
        /// heavy tails. Data sets with low kurtosis tend to have a flat top 
        /// near the AverageValue rather than a sharp peak. A uniform distribution 
        /// would be the extreme case. 
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

        /// <summary>
        /// Calculate median value
        /// </summary>
       

        /// <summary>
        /// Get range around median containing specified percentage of values
        /// </summary>


        /// <summary>
        /// Calculate an entropy
        /// </summary>
        
    }
}
