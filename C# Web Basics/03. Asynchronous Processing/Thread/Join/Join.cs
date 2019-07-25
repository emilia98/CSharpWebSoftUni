using System;
using System.Diagnostics;
using System.Threading;

namespace Join
{
    class Program
    {
        static void Main()
        {
            Thread thread = new Thread(MyThreadMain);
            // schedules the thread for execution
            thread.Start();

            // Wait until the thread ends it's execution -> waits for the thread to finish its work (blocks the calling thread)
            thread.Join();

            Console.WriteLine("Done.");
        }

        static void MyThreadMain()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(NumberOfPrimeNumebrs(2, 1000000));
            Console.WriteLine(sw.Elapsed);
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
