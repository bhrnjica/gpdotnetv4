using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPdotNET.Core.Statistics;
namespace GPdotNET.Util
{
    /// <summary>
    /// ROC class implemented based on Accord Statistics Library http://accord-framework.net implementation
    /// </summary>
    public class ROC
    {
        private double m_area;     // the exact area computed using the trapezoidal rule
        private double m_error;    // the AUC ROC standard error for sample


        private double[] m_expected; // The ground truth, confirmed data
        private double[] m_prediction;  // The test predictions for the data

        private double[] m_positiveResults; // the subjects which should have been computed as positive
        private double[] m_negativeResults; // the subjects which should have been computed as negative

        private double[] m_positiveAccuracy; // DeLong's pseudoaccuracy for positive subjects
        private double[] m_negativeAccuracy; // DeLong's pseudoaccuracy for negative subjects

        // The real number of positives and negatives in the actual (true) data
        private int m_positiveCount;
        private int m_negativeCount;

        // The values which represent positive and negative values in our
        //  actual data (such as presence or absence of some disease)
        private double m_dtrueValue;
        private double m_dfalseValue;

        // The minimum and maximum values in the prediction data (such
        // as categorical rankings collected from test subjects)
        private double m_min;
        private double m_max;


        // The cm_points to hold our curve point information
        //Consusion matrix points for each value of trashold
        private ROCPoints cm_points;

        public ROCPoints ROCCollection { get { return cm_points; } }
        public double Area { get { return m_area; }}
        public double Error { get { return m_error; } }

        public double PositiveLabel { get { return m_dtrueValue; } }
        public double NegativeLabel { get { return m_dfalseValue; } }

        /// <summary>
        /// main constructor
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="prediction"></param>
        public ROC(double[] expected, double[] prediction)
        {
            // Initial argument checking
            if (expected.Length != prediction.Length)
                throw new ArgumentException("The size of the expected and prediction arrays must match.");
            this.m_expected = new double[expected.Length];
            this.m_prediction = new double[prediction.Length];
            //
            Array.Copy(expected, this.m_expected, prediction.Length);
            Array.Copy(prediction, this.m_prediction, prediction.Length);

            initialize();
        }


        /// <summary>
        /// calculate ROC curve for given number of points
        /// </summary>
        /// <param name="ptRoc"></param>
        public void Calculate(double ptRoc)
        {
            var step = (m_max - m_min) / ptRoc;
            var points = new List<ConfusionMatrix>();
            double cutoff;

            // Create the curve, computing a point for each cutoff value
            for (cutoff = m_min; cutoff <= m_max; cutoff += step)
                points.Add(ComputePoint(cutoff));


            // Sort the curve by descending specificity
            points.Sort(new Comparison<ConfusionMatrix>(order));
            var last = points[points.Count - 1];
            if (last.FalsePositiveRate != 0.0 || last.Sensitivity != 0.0)
                points.Add(ComputePoint(Double.PositiveInfinity));
            

            // Create the point cm_points
            this.cm_points = new ROCPoints(points.ToArray());

            // Calculate area and error associated with this curve
            this.m_area = calculateAreaUnderCurve();
            this.m_error = calculateStandardError();
        }

        /// <summary>
        /// computes points for given trashold
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public ConfusionMatrix ComputePoint(double threshold)
        {
            int truePositives = 0;
            int trueNegatives = 0;

            for (int i = 0; i < this.m_expected.Length; i++)
            {
                bool actual = (this.m_expected[i] == m_dtrueValue);
                bool predicted = (this.m_prediction[i] >= threshold);


                // If the prediction equals the true measured value
                if (predicted == actual)
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (predicted == true)
                        truePositives++; // Positive hit
                    else trueNegatives++;// Negative hit
                }
            }

            // The other values can be computed from available variables
            int falsePositives = m_negativeCount - trueNegatives;
            int falseNegatives = m_positiveCount - truePositives;

            var cm = new ConfusionMatrix(
                truePositives: truePositives, falseNegatives: falseNegatives,
                falsePositives: falsePositives, trueNegatives: trueNegatives);

            cm.SetCutOffValue(threshold);

            return cm;
        }

        /// <summary>
        /// Pepares data to show
        /// </summary>
        /// <param name="zedModel"></param>
        /// <param name="includeRandom"></param>
        public void GetData(out double[] x, out double[] y)
        {
            x = cm_points.GetOneMinusSpecificity();
            y = cm_points.GetSensitivity();

            return;   
        }


        /// <summary>
        /// Initialize ROC curve by calculating variables
        /// </summary>
        private void initialize()
        {

            // Determine which numbers correspond to each binary category
            m_dtrueValue = m_dfalseValue = m_expected[0];
            for (int i = 1; i < m_expected.Length; i++)
            {
                if (m_dtrueValue < m_expected[i]) m_dtrueValue = m_expected[i];
                if (m_dfalseValue > m_expected[i]) m_dfalseValue = m_expected[i];
            }

            // Count the real number of positive and negative cases
            for (int i = 0; i < m_expected.Length; i++)
            {
                if (m_expected[i] == m_dtrueValue)
                    this.m_positiveCount++;
            }

            m_min = m_prediction.Min();
            m_max = m_prediction.Max();

            // Negative cases is just the number of cases minus the number of positives
            this.m_negativeCount = this.m_expected.Length - this.m_positiveCount;

            // Get ratings for true positives
            int[] positiveIndices = getIndicesOfValue(m_expected, m_dtrueValue);
            double[] X = getValuesFromIndices(m_prediction, positiveIndices);

            int[] negativeIndices = getIndicesOfValue(m_expected, m_dfalseValue);
            double[] Y = getValuesFromIndices(m_prediction, negativeIndices);

            m_positiveResults = X;
            m_negativeResults = Y;

            // cal culates accuracy
            m_positiveAccuracy = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                m_positiveAccuracy[i] = calculateProbab(X[i], Y);
            }
            //cal neg acurracy
            m_negativeAccuracy = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                m_negativeAccuracy[i] = 1.0 - calculateProbab(Y[i], X);
            }

        }

        /// <summary>
        /// calculate probability for x on y set
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static double calculateProbab(double x, double[] y)
        {
            double sum = 0;
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] < x)
                    sum += 1;
                if (y[i] == x)
                    sum += 0.5;
            }

            return sum / y.Length;
        }

        /// <summary>
        /// returns values for a given array indices
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        private double[] getValuesFromIndices(double[] elements, int[] indices)
        {
            List<double> values = new List<double>();
            for (int i = 0; i < indices.Length; i++)
            {
                values.Add(elements[indices[i]]);

            }

            return values.ToArray();
        }

        /// <summary>
        /// returns indices of the array for specified condition
        /// </summary>
        /// <param name="expected"></param>
        /// <returns></returns>
        private int[] getIndicesOfValue(double[] elements, double value)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == value)
                    indices.Add(i);

            }

            return indices.ToArray();
        }

        /// <summary>
        /// order confusion matrices
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int order(ConfusionMatrix a, ConfusionMatrix b)
        {
            // First order by descending specificity
            int c = a.Specificity.CompareTo(b.Specificity);

            if (c == 0) // then order by ascending sensitivity
                return -a.Sensitivity.CompareTo(b.Sensitivity);
            else return c;
        }


        /// <summary>
        ///   Calculates the area under the ROC curve using the trapezium method.
        /// </summary>
        /// <remarks>
        ///   The area under a ROC curve can never be less than 0.50. If the area is first calculated as
        ///   less than 0.50, the definition of abnormal will be reversed from a higher test value to a
        ///   lower test value.
        /// </remarks>
        private double calculateAreaUnderCurve()
        {
            double sum = 0.0;
            double tpz = 0.0;

            for (int i = 0; i < cm_points.Count - 1; i++)
            {
                // Obs: False Positive Rate = (1-specificity)
                tpz = cm_points[i].Sensitivity + cm_points[i + 1].Sensitivity;
                tpz = tpz * (cm_points[i].FalsePositiveRate - cm_points[i + 1].FalsePositiveRate) / 2.0;
                sum += tpz;
            }

            if (sum < 0.5)
                return 1.0 - sum;
            else return sum;
        }


        /// <summary>
        /// calculate Standardized Error
        /// </summary>
        /// <returns></returns>
        private double calculateStandardError()
        {
            double[] Vx = m_positiveAccuracy;
            double[] Vy = m_negativeAccuracy;

            double varVx = Vx.VarianceOfS();
            double varVy = Vy.VarianceOfS();

            return Math.Sqrt(varVx / Vx.Length + varVy / Vy.Length);
        }

        
    }
}
