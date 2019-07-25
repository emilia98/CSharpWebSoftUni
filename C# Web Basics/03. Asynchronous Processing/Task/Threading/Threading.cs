using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    class Threading
    {
        static void Main()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
                Task.Run(() =>
                {
                    var x = i;
                    for (int j = 0; j < 100000; j++)
                    {
                        Console.WriteLine($"{j} => {x} {Thread.CurrentThread.ManagedThreadId}");
                    }
                });
            }

            Console.ReadLine();
        }
    }
}
