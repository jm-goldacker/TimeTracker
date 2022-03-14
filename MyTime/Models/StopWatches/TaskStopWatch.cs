using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.Models.StopWatches
{
    public sealed class TaskStopWatch : StopWatch
    {
        private TaskStopWatch() : base() { }

        private static readonly Lazy<TaskStopWatch> _instance = new Lazy<TaskStopWatch>(() =>new TaskStopWatch());

        public static TaskStopWatch Instance => _instance.Value;
    }
}
