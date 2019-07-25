using System;
using System.Collections.Generic;
using System.Threading;

namespace RaceCondition
{
    class RaceCondition
    {
        static void Main()
        {
            Method_1();

            Method_2();
        }

        static void Method_1 ()
        {
            int a = 0;

            for (int i = 0; i < 6; i++)
            {
                Thread thread = new Thread(() =>
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        a++;
                    }
                });

                thread.Start();
            }

            Console.WriteLine(a); // The value of A here is before all the threads end their execution
            Console.WriteLine("Done.");
        }

        static void Method_2()
        {
            int a = 0;
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 6; i++)
            {
                Thread thread = new Thread(() =>
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        a++;
                    }
                });

                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine(a); // The value of A here is before all the threads end their execution
            Console.WriteLine("Done.");
        }
    }
}
