using MyTime.Helpers;
using MyTime.Models;
using MyTime.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.ViewModels
{
    class TasksViewModel : Observerable
    {
        private readonly DatabaseContext _context;

        public StopWatch TasksStopWatch { get; set; }
       
        public RelayCommand StartTasksStopWatch { get; set; }

        public RelayCommand StopTasksStopWatch { get; set; }

        public RelayCommand PauseTasksStopWatch { get; set; }


        private TaskTime _currentTaskTime = new TaskTime();

        public TaskTime CurrentTaskTime
        {
            get => _currentTaskTime;
            set
            {
                _currentTaskTime = value;
                OnPropertyChanged();
            }
        }

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

        public TasksViewModel()
        {
            _context = new DatabaseContext();
            TaskTimes = new ObservableCollection<TaskTime>(_context.TaskTimes.ToList());

            TasksStopWatch = new StopWatch();
            TasksStopWatch.Tick += new EventHandler(delegate (object sender, EventArgs e)
            {
                CurrentTaskTime.End = DateTime.Now;
            });

            // stop pause, create new pause, start work time
            StartTasksStopWatch = new RelayCommand(o =>
            {
                if (!TasksStopWatch.IsRunning)
                {
                    CurrentTaskTime = new TaskTime();
                    CurrentTaskTime.Name = CurrentTaskName;
                    TaskTimes.Add(CurrentTaskTime);
                }

                TasksStopWatch.Start();
            });

            // stop pause, stop worktime
            StopTasksStopWatch = new RelayCommand(o =>
            {
                if (TasksStopWatch.IsRunning)
                {
                    TasksStopWatch.Stop();

                    CurrentTaskTime.Name = CurrentTaskName;

                    _context.TaskTimes.Add(CurrentTaskTime);
                    _context.SaveChanges();
                }
            });
        }
    }
}
