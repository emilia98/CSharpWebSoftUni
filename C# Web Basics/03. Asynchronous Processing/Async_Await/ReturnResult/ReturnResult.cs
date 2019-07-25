using System;
using System.IO;
using System.Threading.Tasks;

namespace ReturnResult
{
    class ReturnResult
    {
        static async Task Main()
        {
            string result = await Run();
            Console.WriteLine(result);
            Console.WriteLine("Done.");
        }

        static async Task<string> Run()
        {
            try
            {
                var lines = await File.ReadAllLinesAsync("in.txt");
                await File.WriteAllLinesAsync("out.txt", lines);
                return lines[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("*** exception *** ");
            }

            return null;
        }
    }
}
