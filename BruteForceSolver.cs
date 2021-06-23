namespace SchedulingProblemGrobel
{
    public class BruteForceSolver
    {
        private readonly TaskToSchedule[] K;
        private readonly int[] P1;
        private readonly int[] P2;
        private readonly int M1SetupTime;
        private readonly int M2SetupTime;

        public BruteForceSolver(TaskToSchedule[] k, int m1SetupTime, int m2SetupTime)
        {
            K = k;
            M1SetupTime = m1SetupTime;
            M2SetupTime = m2SetupTime;
            P1 = new int[K.Length];
            P2 = new int[K.Length];
        }

        public int Solve()
        {
            CountDuration(0);

            var p1Sum = Helpers.SumElements(P1)*2;
            var p2Sum = Helpers.SumElements(P2)*2;

            return p1Sum < p2Sum ? p1Sum : p2Sum; 
        }

        private void CountDuration(int i)
        {
            if(i == 0)
            {
                P1[i] = M1SetupTime + K[i].DurationM1;
                //P2[i] = M2SetupTime + K[i].DurationM2 + P1[i];
                P2[i] = M2SetupTime + K[i].DurationM2;
            }
            else 
            {
                var m1Duration = M1SetupTime + K[i].DurationM1;

                if(m1Duration <= P2[i-1])
                {
                    P1[i] = P2[i-1];
                }
                else
                {
                    P1[i] = m1Duration;
                }

                P2[i] = P1[i] + M1SetupTime + K[i].DurationM2;                
            }  

            i++;

            if(i < K.Length)
            {
                CountDuration(i);
            }       
        }

        
    }
}