// Copyright 2006-2009 Bahrudin Hrnjica (bhrnjica@hotmail.com)
// gpNETLib 
using System;

namespace GPNETLib
{
    /// <summary>
    /// This class provide training data for envolve GP algoritam
    /// </summary>
    [Serializable]
    public class GPTerminalSet
    {
        //x1,x2, ... , xn, R1,R2, ... ,Rn, Y
        //Datamembers
        public double[][] data = null;

        public double[][] testingData;
        //
        public int NumVariable { get; set;}
        public int NumConstant { get; set;}

        public double srVrijednost;
        public double maxValue;
        public double minValue;

        public int RowCount { get; set; }

        public GPTerminalSet()
        {}
        public void Izracunaj()
        {
            if (data == null)
                return;
            if (NumVariable == 0)
                return;

            RowCount = data.Length;
            srVrijednost = 0;
            maxValue = double.MinValue;
            minValue = double.MaxValue;
            int numTernimal=data[0].Length-NumConstant;
            for (int i = 0, n = numTernimal+NumConstant; i < RowCount; i++)
            {
                if (maxValue < data[i][n - 1])
                    maxValue = data[i][n - 1];
                if (minValue > data[i][n - 1])
                    minValue = data[i][n - 1];

                srVrijednost += data[i][n - 1];

            }
            srVrijednost = srVrijednost / RowCount;
        }
    }
}
