using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using System.Collections.ObjectModel;

namespace MyTime.ViewModels
{
    public interface IWorkTimeViewModel
    {
        AccumulatedTimes AccumulatedPauseTimes { get; }
        AccumulatedTimes AccumulatedWorkTimes { get; }
        ObservableCollection<PauseTime> PauseTimes { get; set; }
        PauseStopWatch PauseTimeStopWatch { get; }
        RelayCommand PauseWork { get; set; }
        RelayCommand StartWork { get; set; }
        RelayCommand StopWork { get; set; }
        ObservableCollection<WorkTime> WorkTimes { get; set; }
        WorkStopWatch WorkTimeStopWatch { get; }
    }
}