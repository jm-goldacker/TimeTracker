using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyTime.ViewModels
{
    public class TasksViewModel : Observerable
    {
        public ITaskStopWatch TasksStopWatch { get; set; }

        private IWorkStopWatch _workStopWatch;

        public RelayCommand StartTasksStopWatch { get; set; }

        public RelayCommand StopTasksStopWatch { get; set; }

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
                _taskTimes = value;
                OnPropertyChanged();
            }
        }

        private readonly IDatabaseRepository _repository;

        public TasksViewModel(IDatabaseRepository repository, ITaskStopWatch taskStopWatch, IWorkStopWatch workStopWatch)
        {
            _repository = repository;
            TasksStopWatch = taskStopWatch;
            _workStopWatch = workStopWatch;

            TaskTimes = new ObservableCollection<TaskTime>(_repository.GetTaskTimes());

            StartTasksStopWatch = new RelayCommand(OnStartTaskExecute, OnStartTaskCanExecute);
            StopTasksStopWatch = new RelayCommand(OnStopTaskExecute, OnStopTaskCanExecute);
        }

        private void OnStartTaskExecute(object? obj)
        {
            TasksStopWatch.Start();
        }

        private bool OnStartTaskCanExecute(object? arg)
        {
            return _workStopWatch.IsRunning && !TasksStopWatch.IsRunning;
        }

        private void OnStopTaskExecute(object? obj)
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

        private bool OnStopTaskCanExecute(object? arg)
        {
            return TasksStopWatch.IsRunning;
        }
    }
}
