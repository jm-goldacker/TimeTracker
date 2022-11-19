using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.Models.StopWatches
{
    public class StopWatchesWrapper : IStopWatchesWrapper
    {
        private IStopWatch workTimeStopWatch;
        public IStopWatch WorkTimeStopWatch => workTimeStopWatch ??= new StopWatch();

        private IStopWatch taskStopWatch;
        public IStopWatch TaskStopWatch => taskStopWatch ??= new StopWatch();

        private IStopWatch pauseStopWatch;
        public IStopWatch PauseStopWatch => pauseStopWatch ??= new StopWatch();
    }
}
