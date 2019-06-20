using System;
using System.Diagnostics;
using System.Threading;

namespace PrimeNumbers_Thread
{
    class PrimeNumbers_Thread
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Thread => kogato prikluchat vsichki metodi, togava prikluchva cqlata programa
            Thread thread = new Thread(() =>
            {
                Console.WriteLine(NumberOfPrimeNumebrs(2, 10000000));
                Console.WriteLine(sw.Elapsed);
            });
            thread.Start();

            Console.WriteLine("Done");

            while (true)
            {
                string line = Console.ReadLine();
                Console.WriteLine(line);

                if (line == "exit")
                {
                    // The program does not stop, because it's waiting for all the threads to end
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
