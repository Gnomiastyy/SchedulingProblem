using System;
using System.Linq;

namespace SchedulingProblemGrobel
{
    public enum Processor
    {
        P1,
        P2
    }
    public class HeuristicSolver
    {
        private readonly TaskToSchedule[] K;
        private readonly int[] P1;
        private readonly int[] P2;
        private int bestSolution;
        private readonly int M1SetupTime;
        private readonly int M2SetupTime;

        public HeuristicSolver(TaskToSchedule[] k, int m1SetupTime, int m2SetupTime)
        {
            K = k;
            M1SetupTime = m1SetupTime;
            M2SetupTime = m2SetupTime;
            P1 = new int[K.Length];
            P2 = new int[K.Length];
        }

        public int Solve()
        {
            var randomSol = FindRandomSolution();
            bestSolution = randomSol;

            ReconstructBy(Processor.P1);

            var p1Sum = Helpers.SumElements(P1);
            var p2Sum = Helpers.SumElements(P2);
            
            if (randomSol != p1Sum && randomSol != p2Sum)
            {
                bestSolution = p1Sum > p2Sum && p1Sum != 0 ? p1Sum : p2Sum;
            }

            ReconstructBy(Processor.P2);

            p1Sum = Helpers.SumElements(P1);
            p2Sum = Helpers.SumElements(P2);

            if (bestSolution != p1Sum && bestSolution != p2Sum)
            {
                bestSolution = p1Sum > p2Sum && p1Sum != 0 ? p1Sum : p2Sum;
            }

            return bestSolution;
        }


        public int FindRandomSolution()
        {
            var p1Sum = 0;
            var p2Sum = 0;

            foreach(var k in K)
            {
                p1Sum += k.DurationM1 + M1SetupTime + p2Sum - k.DurationM2;
                p2Sum += k.DurationM2 + M2SetupTime + k.DurationM1;
            }

            return p2Sum < p1Sum 
                ? p2Sum - (int)Math.Sqrt(p2Sum) 
                : p1Sum - (int)Math.Sqrt(p1Sum);
        }

        public void ReconstructBy(Processor p)
        {
            TaskToSchedule[] sorted;

            if (p == Processor.P1)
            {
                sorted = K.OrderBy(x => x.DurationM1).ToArray();
            }
            else 
            {
                sorted = K.OrderBy(x => x.DurationM2).ToArray();
            }
            

            for(int i=0; i<sorted.Length; i++)
            {
                var allTimeM1 = sorted[i].DurationM1 + M1SetupTime;
                int allTimeM2 = 0;

                if(i == 0)
                {
                    allTimeM2 = sorted[i].DurationM2 + M2SetupTime 
                        + sorted[i].DurationM1 + M1SetupTime;
                }
                else
                {
                    allTimeM2 = sorted[i].DurationM2 + M2SetupTime + P1[i-1];
                }
                

                if(i > 0)
                {
                    if (allTimeM1 > allTimeM2)
                    {
                        P1[i] = allTimeM1;
                        P2[i] = allTimeM1 + P1[i-1];
                    }
                    else
                    {
                        P1[i] = allTimeM2;
                        P2[i] = P1[i-1] + allTimeM2;
                    }
                }
                else
                {
                    P1[i] = allTimeM1;
                    P2[i] = allTimeM2 + P1[i];
                }              
            }
        }
    }
}