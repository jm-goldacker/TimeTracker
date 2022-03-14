using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.Models.StopWatches
{
    public sealed class PauseStopWatch : StopWatch
    {
        private PauseStopWatch() : base() { }

        private static Lazy<PauseStopWatch> _instance = new Lazy<PauseStopWatch>(() => new PauseStopWatch());

        public static PauseStopWatch Instance => _instance.Value;
    }
}
