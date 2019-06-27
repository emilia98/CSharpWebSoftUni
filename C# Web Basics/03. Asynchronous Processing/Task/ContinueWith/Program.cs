using System;
using System.IO;
using System.Threading.Tasks;

namespace ContinueWith
{
    class Program
    {
        static void Main()
        {
            Task.Run(() => File.ReadAllTextAsync("../../../file.txt"))
                .ContinueWith(x => string.Format("Welcome, {0}", x.Result.ToString()))
                .ContinueWith(y => Console.WriteLine(y.Result.ToString()))
                .Wait();
        }
    }
}
