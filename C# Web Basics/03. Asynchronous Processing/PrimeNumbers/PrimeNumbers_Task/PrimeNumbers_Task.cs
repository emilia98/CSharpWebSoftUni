using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrimeNumbers_Task
{
    class PrimeNumbers_Task
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();

            int count2 = -1;

            // Task => glavnata programa ne izchakva taskovete da prikluchat
            Task.Run(() =>
            {
                int count = NumberOfPrimeNumebrs(2, 10000000);
                Console.WriteLine(count);
                Console.WriteLine(sw.Elapsed);
            });

            Task.Run(() =>
            {
                count2 = 1000;
                Console.WriteLine(count2);
            });

            Console.WriteLine(count2);
            Console.WriteLine("Done");

            while (true)
            {
                string line = Console.ReadLine();
                Console.WriteLine(line);

                if (line == "exit")
                {
                    // The program stops, does not wait for all the tasks to end
                    return;
                }
            }
        }

        static int NumberOfPrimeNumebrs(int from, int to)
        {
            int count = 0;

            for (int i = from; i <= to; i++)
            {
                bool isPrime = false;
                for (int div = 2; div <= Math.Sqrt(i); div++)
                {
                    if (i % div == 0)
                    {
                        isPrime = true;
                        break;
                    }
                }

                if (isPrime)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
