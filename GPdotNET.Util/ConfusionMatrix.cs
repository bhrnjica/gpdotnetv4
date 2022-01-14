// Accord Statistics Library
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2017
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace GPdotNET.Util
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;


    /// <summary>
    ///   Binary decision confusion matrix.
    /// </summary>
    /// 
    /// <example>
    ///   <code>
    ///   // The correct and expected output values (as confirmed by a Gold
    ///   //  standard rule, actual experiment or true verification)
    ///   int[] expected = { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 };
    ///   
    ///   // The values as predicted by the decision system or
    ///   //  the test whose performance is being measured.
    ///   int[] predicted = { 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 };
    ///   
    ///   
    ///   // In this test, 1 means positive, 0 means negative
    ///   int positiveValue = 1;
    ///   int negativeValue = 0;
    ///   
    ///   // Create a new confusion matrix using the given parameters
    ///   ConfusionMatrix matrix = new ConfusionMatrix(predicted, expected,
    ///       positiveValue, negativeValue);
    ///
    ///   // At this point,
    ///   //   True Positives should be equal to 1;
    ///   //   True Negatives should be equal to 6;
    ///   //   False Negatives should be equal to 1;
    ///   //   False Positives should be equal to 2.
    ///   </code>
    /// </example>
    /// 
    [Serializable]
    public class ConfusionMatrix
    {

        //  2x2 confusion matrix
        //
        //         | a(TP)    b(FN) |
        //   A =   |                |
        //         | c(FP)    d(TN) |
        //

        private int truePositives;  // a
        private int falseNegatives; // b
        private int falsePositives; // c
        private int trueNegatives;  // d


        private double? chiSquare;



        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        public ConfusionMatrix(
            int truePositives, int falseNegatives,
            int falsePositives, int trueNegatives)
        {
            this.truePositives = truePositives;
            this.trueNegatives = trueNegatives;
            this.falsePositives = falsePositives;
            this.falseNegatives = falseNegatives;
        }

        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        public ConfusionMatrix(int[,] matrix)
        {
            this.truePositives = matrix[0, 0];
            this.falseNegatives = matrix[0, 1];

            this.falsePositives = matrix[1, 0];
            this.trueNegatives = matrix[1, 1];
        }

        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        /// <param name="predicted">The values predicted by the model.</param>
        /// <param name="expected">The actual, truth values from the data.</param>
        /// 
        public ConfusionMatrix(bool[] predicted, bool[] expected)
        {
            // Initial argument checking
            if (predicted == null)
                throw new ArgumentNullException("predicted");
            if (expected == null)
                throw new ArgumentNullException("expected");
            if (predicted.Length != expected.Length)
                throw new Exception("The size of the predicted and expected arrays must match.");


            // For each of the predicted values,
            for (int i = 0; i < predicted.Length; i++)
            {
                bool prediction = predicted[i];
                bool expectation = expected[i];


                // If the prediction equals the true measured value
                if (expectation == prediction)
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (prediction == true)
                    {
                        truePositives++; // Positive hit
                    }
                    else
                    {
                        trueNegatives++; // Negative hit
                    }
                }
                else
                {
                    // We have a miss. Now we have to see
                    //  if it was a positive or negative miss
                    if (prediction == true)
                    {
                        falsePositives++; // Positive hit
                    }
                    else
                    {
                        falseNegatives++; // Negative hit
                    }
                }
            }
        }

        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        /// <param name="predicted">The values predicted by the model.</param>
        /// <param name="expected">The actual, truth values from the data.</param>
        /// <param name="positiveValue">The integer label which identifies a value as positive.</param>
        /// 
        public ConfusionMatrix(int[] predicted, int[] expected, int positiveValue = 1)
        {
            // Initial argument checking
            if (predicted == null) throw new ArgumentNullException("predicted");
            if (expected == null) throw new ArgumentNullException("expected");
            if (predicted.Length != expected.Length)
                throw new Exception("The size of the predicted and expected arrays must match.");


            for (int i = 0; i < predicted.Length; i++)
            {
                bool prediction = predicted[i] == positiveValue;
                bool expectation = expected[i] == positiveValue;


                // If the prediction equals the true measured value
                if (expectation == prediction)
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (prediction == true)
                    {
                        truePositives++; // Positive hit
                    }
                    else
                    {
                        trueNegatives++; // Negative hit
                    }
                }
                else
                {
                    // We have a miss. Now we have to see
                    //  if it was a positive or negative miss
                    if (prediction == true)
                    {
                        falsePositives++; // Positive hit
                    }
                    else
                    {
                        falseNegatives++; // Negative hit
                    }
                }
            }

        }


        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        /// <param name="predicted">The values predicted by the model.</param>
        /// <param name="expected">The actual, truth values from the data.</param>
        /// <param name="positiveValue">The integer label which identifies a value as positive.</param>
        /// <param name="negativeValue">The integer label which identifies a value as negative.</param>
        /// 
        public ConfusionMatrix(int[] predicted, int[] expected, int positiveValue, int negativeValue)
        {
            // Initial argument checking
            if (predicted == null) throw new ArgumentNullException("predicted");
            if (expected == null) throw new ArgumentNullException("expected");
            if (predicted.Length != expected.Length)
                throw new Exception("The size of the predicted and expected arrays must match.");


            for (int i = 0; i < predicted.Length; i++)
            {

                // If the prediction equals the true measured value
                if (predicted[i] == expected[i])
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (predicted[i] == positiveValue)
                    {
                        truePositives++; // Positive hit
                    }
                    else if (predicted[i] == negativeValue)
                    {
                        trueNegatives++; // Negative hit
                    }
                }
                else
                {
                    // We have a miss. Now we have to see
                    //  if it was a positive or negative miss
                    if (predicted[i] == positiveValue)
                    {
                        falsePositives++; // Positive hit
                    }
                    else if (predicted[i] == negativeValue)
                    {
                        falseNegatives++; // Negative hit
                    }
                }
            }
        }

        public void SetCutOffValue(double value)
        {
            cutoff = value;
        }
        // Discrimination threshold (cutoff value)
        private double cutoff;

        /// <summary>
        ///   Gets the cutoff value (discrimination threshold) for this point.
        /// </summary>
        /// 
        public double Cutoff
        {
            get { return cutoff; }
        }

        /// <summary>
        ///   Gets the confusion matrix in count matrix form.
        /// </summary>
        /// 
        /// <remarks>
        ///   The table is listed as true positives, false negatives
        ///   on its first row, false positives and true negatives in
        ///   its second row.
        /// </remarks>
        /// 
        public int[,] Matrix
        {
            get
            {
                return new int[,]
                {
                    { truePositives, falseNegatives },
                    { falsePositives, trueNegatives },
                };
            }
        }

        /// <summary>
        ///   Gets the marginal sums for table rows.
        /// </summary>
        /// 
        /// <value>
        ///   Returns a vector with the sum of true positives and 
        ///   false negatives on its first position, and the sum
        ///   of false positives and true negatives on the second.
        /// </value>
        /// 
        public int[] RowTotals
        {
            get
            {
                return new int[]
                {
                    truePositives + falseNegatives, // ActualPositives
                    falsePositives + trueNegatives, // ActualNegatives
                };
            }
        }

        /// <summary>
        ///   Gets the marginal sums for table columns.
        /// </summary>
        /// 
        /// <value>
        ///   Returns a vector with the sum of true positives and
        ///   false positives on its first position, and the sum
        ///   of false negatives and true negatives on the second.
        /// </value>
        /// 
        public int[] ColumnTotals
        {
            get
            {
                return new int[]
                {
                    truePositives + falsePositives, // PredictedPositives
                    falseNegatives + trueNegatives, // PredictedNegatives
                };
            }
        }


        /// <summary>
        ///   Gets the number of observations for this matrix
        /// </summary>
        /// 
        public int Samples
        {
            get
            {
                return trueNegatives + truePositives +
                    falseNegatives + falsePositives;
            }
        }

        /// <summary>
        ///   Gets the number of actual positives.
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of positives cases can be computed by
        ///   taking the sum of true positives and false negatives.
        /// </remarks>
        /// 
        public int ActualPositives
        {
            get { return truePositives + falseNegatives; }
        }

        /// <summary>
        ///   Gets the number of actual negatives
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of negatives cases can be computed by
        ///   taking the sum of true negatives and false positives.
        /// </remarks>
        /// 
        public int ActualNegatives
        {
            get { return trueNegatives + falsePositives; }
        }

        /// <summary>
        ///   Gets the number of predicted positives.
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of cases predicted as positive by the
        ///   test. This value can be computed by adding the
        ///   true positives and false positives.
        /// </remarks>
        /// 
        public int PredictedPositives
        {
            get { return truePositives + falsePositives; }
        }

        /// <summary>
        ///   Gets the number of predicted negatives.
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of cases predicted as negative by the
        ///   test. This value can be computed by adding the
        ///   true negatives and false negatives.
        /// </remarks>
        /// 
        public int PredictedNegatives
        {
            get { return trueNegatives + falseNegatives; }
        }

        /// <summary>
        ///   Cases correctly identified by the system as positives.
        /// </summary>
        /// 

        public int TruePositives
        {
            get { return truePositives; }
        }

        /// <summary>
        ///   Cases correctly identified by the system as negatives.
        /// </summary>
        /// 
        public int TrueNegatives
        {
            get { return trueNegatives; }
        }

        /// <summary>
        ///   Cases incorrectly identified by the system as positives.
        /// </summary>
        /// 
        public int FalsePositives
        {
            get { return falsePositives; }
        }

        /// <summary>
        ///   Cases incorrectly identified by the system as negatives.
        /// </summary>
        /// 
        public int FalseNegatives
        {
            get { return falseNegatives; }
        }

        /// <summary>
        ///   Sensitivity, also known as True Positive Rate
        /// </summary>
        /// 
        /// <remarks>
        ///   The Sensitivity is calculated as <c>TPR = TP / (TP + FN)</c>.
        /// </remarks>
        /// 
        public double Sensitivity
        {
            get
            {
                return (truePositives == 0) ?
                    0 : (double)truePositives / (truePositives + falseNegatives);
            }
        }

        /// <summary>
        ///   Specificity, also known as True Negative Rate
        /// </summary>
        /// 
        /// <remarks>
        ///   The Specificity is calculated as <c>TNR = TN / (FP + TN)</c>.
        ///   It can also be calculated as: <c>TNR = (1-False Positive Rate)</c>.
        /// </remarks>
        /// 
        public double Specificity
        {
            get
            {
                return (trueNegatives == 0) ?
                    0 : (double)trueNegatives / (trueNegatives + falsePositives);
            }
        }

        /// <summary>
        ///  Efficiency, the arithmetic mean of sensitivity and specificity
        /// </summary>
        /// 
        public double Efficiency
        {
            get { return (Sensitivity + Specificity) / 2.0; }
        }

        /// <summary>
        ///   Accuracy, or raw performance of the system
        /// </summary>
        /// 
        /// <remarks>
        ///   The Accuracy is calculated as 
        ///   <c>ACC = (TP + TN) / (P + N).</c>
        /// </remarks>
        /// 
        public double Accuracy
        {
            get { return 1.0 * (truePositives + trueNegatives) / Samples; }
        }

        /// <summary>
        ///  Prevalence of outcome occurrence.
        /// </summary>
        /// 
        public double Prevalence
        {
            get { return ActualPositives / (double)Samples; }
        }

        /// <summary>
        ///   Positive Predictive Value, also known as Positive Precision
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The Positive Predictive Value tells us how likely is 
        ///   that a patient has a disease, given that the test for
        ///   this disease is positive.</para>
        /// <para>
        ///   The Positive Predictive Rate is calculated as
        ///   <c>PPV = TP / (TP + FP)</c>.</para>
        /// </remarks>
        /// 
        public double PositivePredictiveValue
        {
            get
            {
                double f = truePositives + FalsePositives;

                if (f != 0)
                    return truePositives / f;

                return 1.0;
            }
        }

        /// <summary>
        ///   Negative Predictive Value, also known as Negative Precision
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The Negative Predictive Value tells us how likely it is
        ///   that the disease is NOT present for a patient, given that
        ///   the patient's test for the disease is negative.</para>
        /// <para>
        ///   The Negative Predictive Value is calculated as 
        ///   <c>NPV = TN / (TN + FN)</c>.</para> 
        /// </remarks>
        /// 

        public double NegativePredictiveValue
        {
            get
            {
                double f = (trueNegatives + falseNegatives);

                if (f != 0)
                    return trueNegatives / f;

                return 1.0;
            }
        }


        /// <summary>
        ///   False Positive Rate, also known as false alarm rate.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The rate of false alarms in a test.</para>
        /// <para>
        ///   The False Positive Rate can be calculated as
        ///   <c>FPR = FP / (FP + TN)</c> or <c>FPR = (1-specificity)</c>.
        /// </para>
        /// </remarks>
        /// 

        public double FalsePositiveRate
        {
            get
            {
                return (double)falsePositives / (falsePositives + trueNegatives);
            }
        }

        /// <summary>
        ///   False Discovery Rate, or the expected false positive rate.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The False Discovery Rate is actually the expected false positive rate.</para>
        /// <para>
        ///   For example, if 1000 observations were experimentally predicted to
        ///   be different, and a maximum FDR for these observations was 0.10, then
        ///   100 of these observations would be expected to be false positives.</para>
        /// <para>
        ///   The False Discovery Rate is calculated as
        ///   <c>FDR = FP / (FP + TP)</c>.</para>
        /// </remarks>
        /// 

        public double FalseDiscoveryRate
        {
            get
            {
                double d = falsePositives + truePositives;

                if (d != 0.0)
                    return falsePositives / d;

                return 1.0;
            }
        }

        /// <summary>
        ///   Matthews Correlation Coefficient, also known as Phi coefficient 
        /// </summary>
        /// 
        /// <remarks>
        ///   A coefficient of +1 represents a perfect prediction, 0 an
        ///   average random prediction and −1 an inverse prediction.
        /// </remarks>
        /// 

        public double MatthewsCorrelationCoefficient
        {
            get
            {
                double tp = truePositives;
                double tn = trueNegatives;
                double fp = falsePositives;
                double fn = falseNegatives;

                double den = System.Math.Sqrt((tp + fp) * (tp + fn) * (tn + fp) * (tn + fn));

                if (den == 0.0)
                    return 0;

                double num = (tp * tn) - (fp * fn);

                return num / den;
            }
        }


        /// <summary>
        ///   Odds-ratio.
        /// </summary>
        /// 
        /// <remarks>
        ///   References: http://www.iph.ufrgs.br/corpodocente/marques/cd/rd/presabs.htm
        /// </remarks>
        /// 

        public double OddsRatio
        {
            get
            {
                return (double)(truePositives * trueNegatives) / (falsePositives * falseNegatives);
            }
        }

        /// <summary>
        ///   Kappa coefficient.
        /// </summary>
        ///
        /// <remarks>
        ///   References: http://www.iph.ufrgs.br/corpodocente/marques/cd/rd/presabs.htm
        /// </remarks>
        ///

        public double Kappa
        {
            get
            {
                double a = truePositives;
                double b = falsePositives;
                double c = falseNegatives;
                double d = trueNegatives;
                double N = Samples;

                return (double)((a + d) - (((a + c) * (a + b) + (b + d) * (c + d)) / N))
                    / (N - (((a + c) * (a + b) + (b + d) * (c + d)) / N));
            }
        }

        

        /// <summary>
        ///   Diagnostic power.
        /// </summary>
        /// 

        public double OverallDiagnosticPower
        {
            get { return (double)(falsePositives + trueNegatives) / Samples; }
        }

        /// <summary>
        ///   Normalized Mutual Information.
        /// </summary>
        /// 

        public double NormalizedMutualInformation
        {
            get
            {
                double a = truePositives;
                double b = falsePositives;
                double c = falseNegatives;
                double d = trueNegatives;
                double N = Samples;

                double num = a * Math.Log(a) + b * Math.Log(b) + c * Math.Log(c) + d * Math.Log(d)
                           - (a + b) * Math.Log(a + b) - (c + d) * Math.Log(c + d);

                double den = N * Math.Log(N) - ((a + c) * Math.Log(a + c) + (b + d) * Math.Log(b + d));

                return 1 + num / den;
            }
        }

        /// <summary>
        ///   Precision, same as the <see cref="PositivePredictiveValue"/>.
        /// </summary>
        /// 
        public double Precision
        {
            get { return PositivePredictiveValue; }
        }

        /// <summary>
        ///   Recall, same as the <see cref="Sensitivity"/>.
        /// </summary>
        /// 
        public double Recall
        {
            get { return Sensitivity; }
        }

        /// <summary>
        ///   F-Score, computed as the harmonic mean of
        ///   <see cref="Precision"/> and <see cref="Recall"/>.
        /// </summary>
        /// 

        public double FScore
        {
            get { return 2.0 * (Precision * Recall) / (Precision + Recall); }
        }

        /// <summary>
        ///   Expected values, or values that could
        ///   have been generated just by chance.
        /// </summary>
        /// 

        public double[,] ExpectedValues
        {
            get
            {
                var row = RowTotals;
                var col = ColumnTotals;

                var expected = new double[2, 2];

                for (int i = 0; i < row.Length; i++)
                    for (int j = 0; j < col.Length; j++)
                        expected[i, j] = (col[j] * row[i]) / (double)Samples;

                return expected;
            }
        }

        /// <summary>
        ///   Gets the Chi-Square statistic for the contingency table.
        /// </summary>
        /// 

        public double ChiSquare
        {
            get
            {
                if (chiSquare == null)
                {
                    var row = RowTotals;
                    var col = ColumnTotals;

                    double x = 0;
                    for (int i = 0; i < row.Length; i++)
                    {
                        for (int j = 0; j < col.Length; j++)
                        {
                            double e = (row[i] * col[j]) / (double)Samples;
                            double o = Matrix[i, j];

                            x += ((o - e) * (o - e)) / e;
                        }
                    }

                    chiSquare = x;
                }

                return chiSquare.Value;
            }
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> representing this confusion matrix.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> representing this confusion matrix.
        /// </returns>
        /// 
        public override string ToString()
        {
            return String.Format(System.Globalization.CultureInfo.CurrentCulture,
                "TP:{0} FP:{2}, FN:{3} TN:{1}",
                truePositives, trueNegatives, falsePositives, falseNegatives);
        }

        /// <summary>
        ///   Converts this matrix into a <see cref="GeneralConfusionMatrix"/>.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="GeneralConfusionMatrix"/> with the same contents as this matrix.
        /// </returns>
        /// 
        //public GeneralConfusionMatrix ToGeneralMatrix()
        //{
        //    // Create a new matrix assuming negative instances 
        //    // are class 0, and positive instances are class 1.

        //    int[,] matrix =
        //    {
        //        //   class 0          class 1
        //        { trueNegatives,  falsePositives }, // class 0
        //        { falseNegatives, truePositives  }, // class 1
        //    };

        //    return new GeneralConfusionMatrix(matrix);
        //}

        /// <summary>
        ///   Combines several confusion matrices into one single matrix.
        /// </summary>
        /// 
        /// <param name="matrices">The matrices to combine.</param>
        /// 
        public static ConfusionMatrix Combine(params ConfusionMatrix[] matrices)
        {
            if (matrices == null) throw new ArgumentNullException("matrices");
            if (matrices.Length == 0) throw new ArgumentException("At least one confusion matrix is required.");

            int[,] total = new int[2, 2];

            foreach (var matrix in matrices)
            {
                for (int j = 0; j < 2; j++)
                    for (int k = 0; k < 2; k++)
                        total[j, k] += matrix.Matrix[j, k];
            }

            return new ConfusionMatrix(total);
        }
    }

    public class ROCPoints : ReadOnlyCollection<ConfusionMatrix>
    {
        internal ROCPoints(ConfusionMatrix[] points)
            : base(points) { }

        /// <summary>
        ///   Gets the (1-specificity, sensitivity) values as (x,y) coordinates.
        /// </summary>
        /// 
        /// <returns>
        ///   An jagged double array where each element is a double[] vector
        ///   with two positions; the first is the value for 1-specificity (x)
        ///   and the second the value for sensitivity (y).
        /// </returns>
        /// 
        public double[][] GetXYValues()
        {
            double[] x = new double[this.Count];
            double[] y = new double[this.Count];

            for (int i = 0; i < Count; i++)
            {
                x[i] = 1.0 - this[i].Specificity;
                y[i] = this[i].Sensitivity;
            }

            double[][] points = new double[this.Count][];
            for (int i = 0; i < points.Length; i++)
                points[i] = new[] { x[i], y[i] };

            return points;
        }

        /// <summary>
        ///   Gets an array containing (1-specificity) 
        ///   values for each point in the curve.
        /// </summary>
        /// 
        public double[] GetOneMinusSpecificity()
        {
            double[] x = new double[this.Count];
            for (int i = 0; i < x.Length; i++)
                x[i] = 1.0 - this[i].Specificity;
            return x;
        }

        /// <summary>
        ///   Gets an array containing (sensitivity) 
        ///   values for each point in the curve.
        /// </summary>
        /// 
        public double[] GetSensitivity()
        {
            double[] y = new double[this.Count];
            for (int i = 0; i < y.Length; i++)
                y[i] = this[i].Sensitivity;
            return y;
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public override string ToString()
        {
            return Count.ToString();
        }

    }
}
