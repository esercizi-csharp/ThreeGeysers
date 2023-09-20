using System;

namespace ThreeGeysers
{
    internal class Program
    {

        // Some considerations that influence the precision
        // The geyser starts withing some hours (2, 4 or 6 hours equals to 7200, 14400 or 21600 seconds)
        //    of the first observation day (day 0)
        // We split the time in seconds (86400 seconds a day)
        // 100000 visitors in 1 hundred days (86400 x 100 seconds in total) that arrives at a random time
        // All calculations are done in seconds since day zero

        const int theWholeTime = 86400 * 100;

        const int geyser1Interval = 7200;
        const int geyser2Interval = 14400;
        const int geyser3Interval = 21600;

        private const int totalVisitors = 100000;

        static Random random = new Random();




        static void Main(string[] args)
        {
            long geyser1OverallNextEruptionCount = 0;
            long geyser2OverallNextEruptionCount = 0;
            long geyser3OverallNextEruptionCount = 0;

            for (int simulation = 0; simulation < 1000; simulation++)
            {

                int geyser1NextEruptionCount;
                int geyser2NextEruptionCount;
                int geyser3NextEruptionCount;

                GetGeysersNextEruptionCount(out geyser1NextEruptionCount, out geyser2NextEruptionCount, out geyser3NextEruptionCount);

                geyser1OverallNextEruptionCount += geyser1NextEruptionCount;
                geyser2OverallNextEruptionCount += geyser2NextEruptionCount;
                geyser3OverallNextEruptionCount += geyser3NextEruptionCount;
            }

            var totalAttemps = geyser1OverallNextEruptionCount + geyser2OverallNextEruptionCount + geyser3OverallNextEruptionCount;
            Console.WriteLine($"Total attemps {totalAttemps}");
            Console.WriteLine($"Geyser A erupted {geyser1OverallNextEruptionCount} ({(double)geyser1OverallNextEruptionCount / totalAttemps * 100:#.00}%)");
            Console.WriteLine($"Geyser B erupted {geyser2OverallNextEruptionCount} ({(double)geyser2OverallNextEruptionCount / totalAttemps * 100:#.00}%)");
            Console.WriteLine($"Geyser C erupted {geyser3OverallNextEruptionCount} ({(double)geyser3OverallNextEruptionCount / totalAttemps * 100:#.00}%)");


            Console.ReadLine();
        }

        private static void GetGeysersNextEruptionCount(out int geyser1NextEruptionCount, out int geyser2NextEruptionCount, out int geyser3NextEruptionCount)
        {
            geyser1NextEruptionCount = 0;
            geyser2NextEruptionCount = 0;
            geyser3NextEruptionCount = 0;

            int geyser1StartTime = random.Next(geyser1Interval);
            int geyser2StartTime = random.Next(geyser2Interval);
            int geyser3StartTime = random.Next(geyser3Interval);

            for (int visitor = 0; visitor < totalVisitors; visitor++)
            {

                int visitorArrivalTime = random.Next(theWholeTime);

                int geyser1NextEruptionTime = NextEruptionTime(visitorArrivalTime, geyser1StartTime, geyser1Interval);
                int geyser2NextEruptionTime = NextEruptionTime(visitorArrivalTime, geyser2StartTime, geyser2Interval);
                int geyser3NextEruptionTime = NextEruptionTime(visitorArrivalTime, geyser3StartTime, geyser3Interval);

                if (geyser1NextEruptionTime < geyser2NextEruptionTime)
                {
                    if (geyser1NextEruptionTime < geyser3NextEruptionTime)
                        geyser1NextEruptionCount++;
                    else
                        geyser3NextEruptionCount++;
                }
                else if (geyser2NextEruptionTime < geyser3NextEruptionTime)
                {
                    geyser2NextEruptionCount++;
                }
                else
                {
                    geyser3NextEruptionCount++;
                }
            }
        }

        private static int NextEruptionTime(int visitorArrivalTime, int startTime, int interval)
        {
            int geyser1NumberOfEruptionsBeforeVisitorArrival = visitorArrivalTime / interval;
            int nextEruptionTime = geyser1NumberOfEruptionsBeforeVisitorArrival * interval + startTime;
            while (nextEruptionTime < visitorArrivalTime)
                nextEruptionTime += interval;
            return nextEruptionTime;
        }
    }
}
