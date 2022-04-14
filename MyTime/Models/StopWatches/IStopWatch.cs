using System;

namespace MyTime.Models
{
    public interface IStopWatch
    {
        TimeSpan Duration { get; }
        DateTime EndTime { get; }
        DateTime StartTime { get; }
        bool IsRunning { get; }

        void Start(bool resume = false);
        void Stop();
    }
}