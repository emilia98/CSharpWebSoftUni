using System;
using System.IO;
using System.Threading.Tasks;

namespace Wait
{
    class Program
    {
        static void Main()
        {
            var task2 = Task.Run(() => File.ReadAllTextAsync("../../../file.txt"))
                  .ContinueWith(x => string.Format("Welcome, {0}", x.Result.ToString()))
                  .ContinueWith(y => Console.WriteLine(y.Result.ToString()));

            task2.Wait();

            Console.WriteLine($"{task2.IsCompleted} {task2.IsFaulted}");

            var task1 = Task.Run(() => File.ReadAllTextAsync("../file.txt"))
            .ContinueWith(x => string.Format("Welcome, {0}", x.Result.ToString()))
            .ContinueWith(y => Console.WriteLine(y.Result.ToString()));

            task1.Wait();

            Console.ReadLine();

            /*
            Console.WriteLine(task1.Exception);

            Console.WriteLine("");

            Console.WriteLine($"{task1.IsCompleted} {task1.IsFaulted}");
            */

        }
    }
}
