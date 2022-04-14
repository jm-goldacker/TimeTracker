using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace MyTime.Models
{
    public abstract class StopWatch : INotifyPropertyChanged
    {
        private DispatcherTimer _timer = new();
        private Stopwatch _stopWatch = new();
        private DateTime _endTime;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { 
            get 
            { 
                return _endTime; 
            } 
            private set
            {
                _endTime = value;
                OnPropertyChanged("Duration");
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
                OnPropertyChanged("Duration");
            });
        }

        public void Start(bool resume = false)
        {
            _timer.Start();
            _stopWatch.Start();

            if (!resume)
                StartTime = DateTime.Now;
        }

        public void Stop()
        {
            _timer.Stop();
            _stopWatch.Reset();

            EndTime = DateTime.Now;
        }

        public void Pause()
        {
            _stopWatch.Stop();
        }

        public bool IsRunning => _stopWatch.IsRunning;
    }
}
