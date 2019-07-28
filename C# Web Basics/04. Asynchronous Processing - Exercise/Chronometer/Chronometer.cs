using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private int milliseconds = 0;
        /*
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;
        */
        private bool hasStopped = true;
        private Stopwatch sw = new Stopwatch();

        // public string GetTime => String.Format("{0:d2}:{1:d2}:{2:d2}:{3:d4}", hours, minutes, seconds, milliseconds);
        public string GetTime => String.Format("{0:d2}:{1:d2}:{2:d4}", milliseconds / 60000, milliseconds / 1000, milliseconds % 1000);

        public List<string> Laps { get; set; }

        public Chronometer()
        {
            this.Laps = new List<string>();

            // this.Reset();
        }

        public string Lap()
        {
            var lap = this.GetTime;
            this.Laps.Add(lap);
            return lap;
        }

        public void Reset()
        {
            milliseconds = -1;
            if (hasStopped)
            {
                milliseconds = 0;
            }
           
            this.Laps = new List<string>();
            this.Stop();
        }

        public void Start()
        {
            hasStopped = false;

            Task.Run(() =>
            {
                sw.Start();
                while (!hasStopped)
                {
                    Thread.Sleep(1);
                    milliseconds++;
                    // Format();
                }
            });
        }

        /*
        private void Format()
        {
            if(milliseconds >= 1000)
            {
                seconds++;
                milliseconds -= 1000;
            }

            if(seconds == 60)
            {
                minutes++;
                seconds -= 60;
            }

            if(minutes == 60)
            {
                hours++;
                minutes -= 60;
            }
        }
        */
        

        public void Stop()
        {
            hasStopped = true;
        }
    }
}
