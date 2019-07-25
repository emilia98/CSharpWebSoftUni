using System;
using System.Diagnostics;
using System.Threading;

namespace ThrowException
{
    class Program
    {
        static void Main()
        {
            try
            {
                Thread thread = new Thread(MyThreadMain);
                thread.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine("Done.");
        }


        static void MyThreadMain()
        {
            Stopwatch sw = Stopwatch.StartNew();
            throw new Exception("Here...");
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
