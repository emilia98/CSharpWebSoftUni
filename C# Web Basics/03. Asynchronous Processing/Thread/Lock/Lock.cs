using System;
using System.Threading;

namespace Lock
{
    class Lock
    {
        static void Main()
        {
            // used for an identifies for the lock
            object lockObj = new object();
            int a = 0;

            for (int i = 0; i < 6; i++)
            {
                Thread thread = new Thread(() =>
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        lock(lockObj)
                        {
                            a++;
                        }

                        /* That's how lock() {} works
                        Monitor.Enter(lockObj);
                        a++;
                        Monitor.Exit();
                        */
                    }
                });

                thread.Start();
                thread.Join();
            }

            Console.WriteLine(a);
            Console.WriteLine("Done.");
        }
    }
}
