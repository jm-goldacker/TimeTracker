using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyTime.ViewModels
{
    public class TasksViewModel : BindableBase
    {
        public ITaskStopWatch TasksStopWatch { get; set; }

        private IWorkStopWatch _workStopWatch;

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

        public TasksViewModel(IDatabaseRepository repository, ITaskStopWatch taskStopWatch, IWorkStopWatch workStopWatch)
        {
            _repository = repository;
            TasksStopWatch = taskStopWatch;
            _workStopWatch = workStopWatch;

            TaskTimes = new ObservableCollection<TaskTime>(_repository.GetTaskTimes());

            StartTasksStopWatch = new DelegateCommand(OnStartTaskExecute, OnStartTaskCanExecute);
            StopTasksStopWatch = new DelegateCommand(OnStopTaskExecute, OnStopTaskCanExecute);
        }

        private void OnStartTaskExecute()
        {
            TasksStopWatch.Start();
        }

        private bool OnStartTaskCanExecute()
        {
            return _workStopWatch.IsRunning && !TasksStopWatch.IsRunning;
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
