using System;
using System.Threading;

namespace ExceptionHandling
{
    class ExceptionHandling
    {
        static void Main()
        {
            /* Exceptions cannot be handled outside thread
            try
            {
                new Thread(Wrong).Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            */

            // Exceptions should be handled inside the executed methods
            new Thread(Correct).Start();
        }

        static void Wrong()
        {
            throw new Exception("!!!!!!!");
        }

        static void Correct()
        {
            try
            {
                throw new Exception("*** exception ***");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception handled!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
