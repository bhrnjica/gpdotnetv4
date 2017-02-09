using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPdotNET.Core.Statistics
{
    /// <summary>
    /// Implement extension methods for statistics clculation between two data sets X and Y eg. sum of square error, pearson coeff,... 
    /// 
    /// </summary>
    public static class AdvancedStatistics
    {
        /// <summary>
        /// Calculates sum of squares residuals of the two datasets
        /// </summary>
        /// <param name="data1">first data set</param>
        /// <param name="data2">second dataset</param>
        /// <returns></returns>
        public static double SSROf(this double[] data1, double[] data2)
        {
            if (data1 == null || data1.Length < 2)
                throw new Exception("'xData' cannot be null or empty!");

            if (data2 == null || data2.Length < 2)
                throw new Exception("'yData' cannot be null or empty!");

            if (data1.Length != data2.Length)
                throw new Exception("Both datasets must be of the same size!");

            //calculate summ of the square residuals
            double ssr = 0;
            for(int i=0; i < data1.Length; i++)
            {
                var r = (data1[i] - data2[i]);
                ssr += r * r;
            }
               

            return ssr;
        }

        /// <summary>
        /// Calculates Pearson corellation coefficient of two data sets
        /// </summary>
        /// <param name="data1"> first data set</param>
        /// <param name="data2">second data set </param>
        /// <returns></returns>
        public static double CorrCoeffOf(this double[] data1, double[] data2)
        {
            if (data1 == null || data1.Length < 2)
                throw new Exception("'xData' cannot be null or empty!");

            if (data2 == null || data2.Length < 2)
                throw new Exception("'yData' cannot be null or empty!");

            if (data1.Length != data2.Length)
                throw new Exception("Both datasets must be of the same size!");

            //calculate average for each dataset
            double aav = data1.MeanOf();
            double bav = data2.MeanOf();

            double corr = 0;
            double ab = 0, aa = 0, bb = 0;
            for (int i = 0; i < data1.Length; i++)
            {
                var a = data1[i] - aav;
                var b = data2[i] - bav;

                ab += a * b;
                aa += a * a;
                bb += b * b;
            }

            corr = ab / Math.Sqrt(aa * bb);

            return corr;
        }
    }
}
