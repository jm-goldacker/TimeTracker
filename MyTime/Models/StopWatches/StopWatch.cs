using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace MyTime.Models
{
    public abstract class StopWatch : DispatcherTimer, INotifyPropertyChanged
    {

        Stopwatch sw = new Stopwatch();
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
                return sw.Elapsed;
            }
        }

        public StopWatch() : base()
        {
            Interval = new TimeSpan(0, 0, 1);
            Tick += new EventHandler(delegate (object? sender, EventArgs e)
            {
                OnPropertyChanged("Duration");
            });
        }

        public new void Start(bool resume = false)
        {
            base.Start();
            sw.Start();

            if (!resume)
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
