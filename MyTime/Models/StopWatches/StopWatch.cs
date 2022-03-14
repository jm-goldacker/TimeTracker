using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace MyTime.Models
{
    public abstract class StopWatch : DispatcherTimer
    {

        Stopwatch sw = new Stopwatch();

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public TimeSpan Duration { get => EndTime - StartTime; }

        public StopWatch() : base()
        {
            Interval = new TimeSpan(0, 0, 1);
            Tick += new EventHandler(delegate (object? sender, EventArgs e)
            {
                EndTime = DateTime.Now;
            });
        }

        public new void Start()
        {
            base.Start();
            sw.Start();

            StartTime = DateTime.Now;
        }

        public new void Stop()
        {
            base.Stop();
            sw.Reset();

            EndTime = DateTime.Now;
        }

        public void Pause()
        {
            sw.Stop();
        }

        public bool IsRunning => sw.IsRunning;
    }
}
