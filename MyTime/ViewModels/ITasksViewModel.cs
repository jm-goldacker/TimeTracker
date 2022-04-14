using MyTime.Models;
using MyTime.Models.Database;
using System.Collections.ObjectModel;

namespace MyTime.ViewModels
{
    public interface ITasksViewModel
    {
        string CurrentTaskName { get; set; }
        RelayCommand StartTasksStopWatch { get; set; }
        RelayCommand StopTasksStopWatch { get; set; }
        StopWatch TasksStopWatch { get; set; }
        ObservableCollection<TaskTime> TaskTimes { get; set; }
    }
}