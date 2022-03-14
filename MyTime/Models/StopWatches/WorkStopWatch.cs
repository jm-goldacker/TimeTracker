using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.Models.StopWatches
{
    public sealed class WorkStopWatch : StopWatch
    {
        private WorkStopWatch() : base() { }

        private static readonly Lazy<WorkStopWatch> _instance = new Lazy<WorkStopWatch>(() => new WorkStopWatch());

        public static WorkStopWatch Instance => _instance.Value;
    }
}
