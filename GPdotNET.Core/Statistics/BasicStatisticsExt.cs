using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPdotNET.Core.Statistics
{
    /// <summary>
    /// Implement extension methods for statistics clculation on one data set eg. mean, median, variance,... 
    /// Modul calculate mean value of array of numbers. 
    /// The mean is the average of the numbers.
    /// </summary>
    public static class BasicStatistics
    {
        /// <summary>
        /// Calculate mean value of array of numbers. 
        /// </summary>
        /// <param name="colData"> array of values </param>
        /// <returns>calculated mean</returns>
        public static double MeanOf(this double[] colData)
        {
            if (colData == null || colData.Length < 2)
                throw new Exception("'coldData' cannot be null or empty!");

            //calculate summ of the values
            double sum = 0;
            for(int i=0; i < colData.Length; i++)
                sum += colData[i];

            //calculate mean
            double retVal = sum / colData.Length;

            return retVal;
        }

        /// <summary>
        /// /// Modul calculate median value of array of numbers. 
        /// If there is an odd number of data values 
        /// then the median will be the value in the middle. 
        /// If there is an even number of data values the median 
        /// is the mean of the two data values in the middle. 
        /// For the data set 1, 1, 2, 5, 6, 6, 9 the median is 5. 
        /// For the data set 1, 1, 2, 6, 6, 9 the median is 4.
        /// </summary>
        /// <param name="colData"></param>
        /// <returns></returns>
        public static double MedianOf(this double[] colData)
        {
            if (colData == null || colData.Length < 2)
                throw new Exception("'coldData' cannot be null or empty!");

            //initial mean value
            double median = 0;
            int medianIndex = colData.Length / 2;

            //make a deep copy of the data
            var temp = new double[colData.Length];
            Array.Copy(colData, temp, colData.Length);
            //sort the values

            Array.Sort(temp);

            //in case we have odd number of elements in data set
            // median is just in the middle of the dataset
            if(temp.Length % 2 == 1)
            {
                // 
                median = temp[medianIndex];
            }
            else//otherwize calculate average between two element in the middle
            {
                var val1 = temp[medianIndex - 1];
                var val2 = temp[medianIndex];

                //calculate the median
                median = (val1 + val2) / 2; 
            }

            return median;
        }

        /// <summary>
        /// Calculate variance for the sample data .
        /// </summary>
        /// <param name="colData"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static double VarianceOfS(this double[] colData)
        {
            if (colData == null || colData.Length < 4)
                throw new Exception("'coldData' cannot be null or less than 4 elements!");
            
            //number of elements
            int count = colData.Length;

            //calculate the mean
            var mean = colData.MeanOf();

            //calculate summ of square 
            double parSum = 0;
            for (int i = 0; i < colData.Length; i++)
            {
                var res = (colData[i] - mean);

                parSum += res*res;
            }
                
            return parSum/(count-1);
        }
        /// <summary>
        /// Calculate StandardDeviation
        /// </summary>
        /// <param name="colData"></param>
        /// <returns></returns>
        public static double Stdev(this double[] colData)
        {
            if (colData == null || colData.Length < 4)
                throw new Exception("'coldData' cannot be null or less than 4 elements!");

            //number of elements
            int count = colData.Length;

            //calculate the mean
            var vars = colData.VarianceOfS();

            //calculate summ of square 
            return Math.Sqrt(vars);
        }

        /// <summary>
        /// Calculate variance for the whole population.
        /// </summary>
        /// <param name="colData"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static double VarianceOfP(this double[] colData)
        {
            if (colData == null || colData.Length < 4)
                throw new Exception("'coldData' cannot be null or less than 4 elements!");

            //number of elements
            int count = colData.Length;

            //calculate the mean
            var mean = colData.MeanOf();

            //calculate summ of square 
            double parSum = 0;
            for (int i = 0; i < colData.Length; i++)
            {
                var res = (colData[i] - mean);

                parSum += res * res;
            }

            return parSum / count;
        }

        /// <summary>
        /// Calculates the minimum and maximum value for each column in dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns>tuple wher the first value is MIN, and second value is MAX</returns>
        public static Tuple<double[], double[]> calculateMinMax(this double[][] dataset)
        {
            //
            if (dataset == null || dataset.Length == 0)
                throw new Exception("data cannot be null or empty!");

            var minMax = new Tuple<double[], double[]>( new double[dataset[0].Length], new double[dataset[0].Length]);


            for (int i = 0; i < dataset.Length; i++)
            {
                for (int j = 0; j < dataset[0].Length; j++)
                {
                    if (dataset[i][j] > minMax.Item2[j])
                        minMax.Item2[j] = dataset[i][j];

                    if (dataset[i][j] < minMax.Item1[j])
                        minMax.Item1[j] = dataset[i][j];
                }
            }

            return minMax;
        }

        /// <summary>
        /// Calculate Mean and Standard deviation
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns>return tuple of mean and StDev</returns>
        public static Tuple<double[], double[]> calculateMeanStDev(this double[][] dataset)
        {
            //
            if (dataset == null || dataset.Length <= 1)
                throw new Exception("data cannot be null or empty!");

            var meanStdev = new Tuple<double[], double[]>(new double[dataset[0].Length], new double[dataset[0].Length]);
            double[] means = new double[dataset[0].Length];
            double[] stdevs = new double[dataset[0].Length];

            //first calculate means
            for (int i = 0; i < dataset.Length; i++)
            {
                for (int j = 0; j < dataset[0].Length; j++)
                { 
                    means[j] += dataset[i][j];
                }
            }
            //devide by number of rows
            for (int i = 0; i < means.Length; i++)
            {
                means[i] = means[i]/ dataset.Length;
            }

            //calculate standard deviation
            for (int i = 0; i < dataset.Length; i++)
            {
                for (int j = 0; j < dataset[0].Length; j++)
                {
                    var v = dataset[i][j] - means[j];
                    stdevs[j] += v*v;
                }
            }

            //calculate stdev
            for (int i = 0; i < means.Length; i++)
            {
                stdevs[i] = Math.Sqrt(stdevs[i]/( dataset.Length - 1));
            }

            return new Tuple<double[], double[]>(means, stdevs);
        }
    }
}
