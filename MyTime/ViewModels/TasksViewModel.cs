using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace MyTime.ViewModels
{
    public class TasksViewModel : BindableBase
    {
        public IStopWatch TasksStopWatch { get; set; }

        public IStopWatch WorkTimeStopWatch { get; set; }

        public DelegateCommand StartTasksStopWatch { get; set; }

        public DelegateCommand StopTasksStopWatch { get; set; }

        public string CurrentTaskName { get; set; } = string.Empty;

        private ObservableCollection<TaskTime> _taskTimes;

        public ObservableCollection<TaskTime> TaskTimes
        {
            get
            {
                return _taskTimes;
            }
            set
            {
                SetProperty(ref _taskTimes, value);
            }
        }

        private readonly IDatabaseRepository _repository;

        public TasksViewModel(IDatabaseRepository repository, IStopWatchesWrapper stopWatchesWrapper)
        {
            _repository = repository;
            TasksStopWatch = stopWatchesWrapper.TaskStopWatch;
            WorkTimeStopWatch = stopWatchesWrapper.WorkTimeStopWatch;

            TaskTimes = new ObservableCollection<TaskTime>(_repository.GetTaskTimes());

            StartTasksStopWatch = new DelegateCommand(OnStartTaskExecute, OnStartTaskCanExecute)
                .ObservesProperty(() => TasksStopWatch.IsRunning)
                .ObservesProperty(() => WorkTimeStopWatch.IsRunning);
            StopTasksStopWatch = new DelegateCommand(OnStopTaskExecute, OnStopTaskCanExecute)
                .ObservesProperty(() => TasksStopWatch.IsRunning)
                .ObservesProperty(() => WorkTimeStopWatch.IsRunning);
        }

        private void OnStartTaskExecute()
        {
            TasksStopWatch.Start();
        }

        private bool OnStartTaskCanExecute()
        {
            return WorkTimeStopWatch.IsRunning && !TasksStopWatch.IsRunning;
        }

        private void OnStopTaskExecute()
        {
            if (TasksStopWatch.IsRunning)
            {
                TasksStopWatch.Stop();

                var task = new TaskTime()
                {
                    Start = TasksStopWatch.StartTime,
                    End = TasksStopWatch.EndTime,
                    Name = CurrentTaskName
                };

                _repository.SaveTaskTime(task);
                TaskTimes.Add(task);
            }
        }

        private bool OnStopTaskCanExecute()
        {
            return TasksStopWatch.IsRunning;
        }
    }
}
