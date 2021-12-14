using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MyTime.Models
{
    public class StopWatch : DispatcherTimer
    {

        Stopwatch sw = new Stopwatch();

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public string ElapsedTime { get; private set; }

        public StopWatch() : base()
        {
            Interval = new TimeSpan(0, 0, 1);
        }

        public void Start()
        {
            base.Start();
            sw.Start();

            StartTime = DateTime.Now;
        }

        public void Stop()
        {
            sw.Reset();

            EndTime = DateTime.Now;
        }

        public void Pause()
        {
            sw.Stop();
        }

        public override string ToString()
        {
            TimeSpan timeSpan = sw.Elapsed;
            return string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }

        public bool IsRunning => sw.IsRunning;
    }
}
