using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPNETLib
{
    /// <summary>
    /// This class provides training and testing data for GP.
    /// </summary>
    [Serializable]
    public class GPTerminalSet
    {   //Structure of Terminal set: first independent variables x, than random constant R and last index position is Output Y
        //x1,x2, ... , xn, R1,R2, ... ,Rn, Y - one row in dataarray
        public double[][] TrainingData { get; set; }
        public double[][] TestingData { get; set; }

        public int NumVariables { get; set; }
        public int NumConstants { get; set; }
        public double AverageValue { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public bool IsTimeSeries { get; set; }
        public int RowCount { get; set; }

        public GPTerminalSet()
        {
            TrainingData = null;
            TestingData = null;
            IsTimeSeries = false;
        }

        //Calculate statistic data for TerminalSet.
        public void CalculateStat()
        {
            if (TrainingData == null)
                throw new Exception("Terminal set is empty!");
            if (NumVariables == 0)
                throw new Exception("The number of variables is 0!");

            int yindex = TrainingData[0].Length - NumConstants + NumConstants - 1;
            RowCount = (short)TrainingData.Length;

            var stat = from p1 in Enumerable.Range(0, RowCount)
                       from p2 in TrainingData
                       select p2[yindex];

            double maxValue = stat.Max();
            double minValue = stat.Min();
            double averageValue = stat.Average();
        }
    }
}
