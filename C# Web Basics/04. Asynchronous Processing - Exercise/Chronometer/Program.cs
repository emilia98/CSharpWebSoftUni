using System;
using System.Linq;

namespace Chronometer
{
    class Program
    {
        static void Main()
        {
            Chronometer chronometer = new Chronometer();

            while (true)
            {
                string command = Console.ReadLine();

                switch(command)
                {
                    case "start": chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "laps":
                        Console.WriteLine(chronometer.Laps.Count > 0
                            ?
                            String.Join('\n', chronometer.Laps.Select((l, i) => $"{i + 1}. {l}"))
                            :
                            "(no laps)");
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    case "exit":
                        return;
                }
            }
        }
    }
}
