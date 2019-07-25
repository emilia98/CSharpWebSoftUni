using System;
using System.IO;
using System.Threading.Tasks;

namespace Void
{
    class Void
    {
        static void Main()
        {
            Run();
            Console.WriteLine("Done.");
        }

        static async Task Run()
        {
            try
            {
                var lines = await File.ReadAllLinesAsync("temp.txt");
                await File.WriteAllLinesAsync("temp2.txt", lines);
                Console.WriteLine(lines[0]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("*** exception *** ");
            }
        }
    }
}
