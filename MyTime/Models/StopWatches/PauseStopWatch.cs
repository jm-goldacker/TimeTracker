using System;

namespace MyTime.Models.StopWatches
{
    public sealed class PauseStopWatch : StopWatch
    {
        private PauseStopWatch() : base() { }

        private static Lazy<PauseStopWatch> _instance = new Lazy<PauseStopWatch>(() => new PauseStopWatch());

        public static PauseStopWatch Instance => _instance.Value;
    }
}
