using System;

namespace SchedulingProblemGrobel
{
    public static class Helpers
    {
        public static int SumElements(int[] array)
        {
            int sum = 0;

            foreach(var element in array)
            {
                sum += element;
            }

            return sum;
        }

        public static TaskToSchedule[] GenerateTasks(int n, int maxLength)
        {
            var tasks = new TaskToSchedule[n];
            var random = new Random();
            
            for(int i=0; i<tasks.Length; i++)
            {

                var durationM1 = random.Next(1, maxLength);
                var durationM2 = random.Next(1, maxLength);

                tasks[i] = new TaskToSchedule(durationM1, durationM2);
            }

            return tasks;
        }
    }
}