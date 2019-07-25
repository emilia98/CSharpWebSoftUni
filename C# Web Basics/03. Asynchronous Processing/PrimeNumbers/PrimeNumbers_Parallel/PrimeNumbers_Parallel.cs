using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumbers_Parallel
{
    class PrimeNumbers_Parallel
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(NumberOfPrimeNumebrs(2, 10000000));
            Console.WriteLine(sw.Elapsed);

            Console.WriteLine("Done");

            /*
            For 
            9335420
            00:00:12.1500400
            Done

            Parallel
            9335420
            00:00:03.5043491
            Done
             */
        }

        static int NumberOfPrimeNumebrs(int from, int to)
        {
            object lockObj = new object();
            int count = 0;


            Parallel.For(from, to + 1, (i) =>
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
                    Interlocked.Increment(ref count);
                    /*
                    lock (lockObj)
                    {
                        count++;
                    }
                    */
                }
            });

            /*
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
        }*/

            return count;
        }
    }
}
