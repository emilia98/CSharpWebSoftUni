using System;
using System.Diagnostics;

namespace PrimeNumbers_Sync
{
    class PrimeNumbers_Sync
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();

            int count = NumberOfPrimeNumebrs(2, 10000000);
            Console.WriteLine(count);
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("Done");

            while (true)
            {
                string line = Console.ReadLine();
                Console.WriteLine(line);

                if(line == "exit")
                {
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
                    if(i % div == 0)
                    {
                        isPrime = true;
                        break;
                    }
                }

                if(isPrime)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
