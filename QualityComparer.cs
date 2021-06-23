using System;
using System.Collections.Generic;

namespace SchedulingProblemGrobel
{
    public class QualityComparer
    {
        public static double StandardDeviation(IList<int> exactSolutions, IList<int> approximateSolutions)
        {
            var variance = Variance(exactSolutions, approximateSolutions);
            
            var stdDeviation = Math.Sqrt(variance);

            return stdDeviation;
        }

        public static double Variance(IList<int> exactSolutions, IList<int> approximateSolutions)
        {
            if (exactSolutions.Count != approximateSolutions.Count) 
                throw new IndexOutOfRangeException();

            int n = exactSolutions.Count;
            double denominator = 0.0;

            for(int i=0; i<n; i++)
            {
                var substractResult = approximateSolutions[i] - exactSolutions[i];
                var squared = Math.Pow(substractResult, 2);

                denominator += squared; 
            }

            var variance = denominator/n;

            return variance;
        }
    }
}