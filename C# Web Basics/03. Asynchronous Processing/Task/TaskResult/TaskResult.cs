using System;
using System.Threading.Tasks;

namespace TaskResult
{
    class TaskResult
    {
        static void Main()
        {
            Task task = new Task(MyThreadMain);

            task.Start();

            try
            {
                task.Wait();
            }
            catch(Exception e)
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("*** exception ***");
                }
            }

            Console.WriteLine("Done.");

        }

        static void MyThreadMain()
        {
            throw new Exception("Here...");
        }
    }
}
