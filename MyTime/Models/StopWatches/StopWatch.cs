using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace MyTime.Models
{
    public class StopWatch : BindableBase, IStopWatch
    {
        private DispatcherTimer _timer = new();
        private Stopwatch _stopWatch = new();
        private DateTime _endTime;

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { 
            get 
            { 
                return _endTime; 
            } 
            private set
            {
                SetProperty(ref _endTime, value, "Duration");
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return _stopWatch.Elapsed;
            }
        }

        public StopWatch() : base()
        {
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(delegate (object? sender, EventArgs e)
            {
                
                RaisePropertyChanged(nameof(Duration));
            });
        }

        public void Start(bool resume = false)
        {
            _timer.Start();
            _stopWatch.Start();

            if (!resume)
                StartTime = DateTime.Now;

            RaisePropertyChanged(nameof(IsRunning));
        }

        public void Stop()
        {
            _timer.Stop();
            _stopWatch.Reset();

            EndTime = DateTime.Now;

            RaisePropertyChanged(nameof(IsRunning));
        }

        public void Pause()
        {
            _stopWatch.Stop();

            RaisePropertyChanged(nameof(IsRunning));
        }

        public bool IsRunning => _stopWatch.IsRunning;
    }
}
