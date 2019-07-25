using System;
using System.IO;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class ExceptionHandling
    {
        static async Task Main()
        {
            try
            {
                string result = await Run();
                Console.WriteLine(result);
                Console.WriteLine("Done.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static async Task<string> Run()
        {
            throw new Exception("!!!!!!!");
            var lines = await File.ReadAllLinesAsync("in.txt");
            await File.WriteAllLinesAsync("out.txt", lines);
            return lines[0];
        }
    }
}
