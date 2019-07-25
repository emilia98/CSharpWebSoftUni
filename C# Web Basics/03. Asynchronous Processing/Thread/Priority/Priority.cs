using System;
using System.Diagnostics;
using System.Threading;

namespace Priority
{
    class Program
    {
        static void Main()
        {
            Thread thread = new Thread(MyThreadMain);
            thread.Priority = int.Parse(Console.ReadLine()) == 1 ? ThreadPriority.Highest : ThreadPriority.Lowest;
            thread.Start();

            for (int i = 0; i < 5; i++)
            {
                Thread thread2 = new Thread(() => { while (true) { } });
                thread2.Start();
                thread2.Priority = ThreadPriority.Lowest;
            }
        }

        static void MyThreadMain()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(NumberOfPrimeNumebrs(2,1000000));
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
