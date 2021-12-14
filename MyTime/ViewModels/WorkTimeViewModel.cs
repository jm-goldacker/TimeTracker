using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyTime.Models;
using MyTime.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyTime.ViewModels
{
    class WorkTimeViewModel : BaseViewModel
    {
        private readonly DatabaseContext _context;

        public StopWatch WorkTimeStopWatch { get; set; }
        public StopWatch PauseTimeStopWatch {get; set; }

        public RelayCommand StartWorkTimeStopWatch {get; set; }

        public RelayCommand StopWorkTimeStopWatch { get; set;}

        public RelayCommand PauseWorkTimeStopWatch { get; set; }

        private string _workTime = "00:00";

        public string WorkTime 
        { 
            get => _workTime;
            set
            {
                _workTime = value;
                OnPropertyChanged();
            }
        }

        private string _pauseTime = "00:00";

        public string PauseTime
        {
            get => _pauseTime;
            set
            {
                _pauseTime = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<WorkTime> _workTimes;

        public ObservableCollection<WorkTime> WorkTimes 
        { 
            get
            {
                return _workTimes;
            }
            set
            {
                _workTimes = value;
                OnPropertyChanged();
            }
        }

        public WorkTimeViewModel()
        {
            _context = new DatabaseContext();
            _workTimes = new ObservableCollection<WorkTime>(_context.WorkTimes.ToList());

            WorkTimeStopWatch = new StopWatch();
            WorkTimeStopWatch.Tick += new EventHandler(delegate (object sender, EventArgs e)
            {
                WorkTime = WorkTimeStopWatch.ToString();
            });

            PauseTimeStopWatch = new StopWatch();
            PauseTimeStopWatch.Tick += new EventHandler(delegate (object sender, EventArgs e)
            {
                PauseTime = PauseTimeStopWatch.ToString();
            });

            StartWorkTimeStopWatch = new RelayCommand(o =>
            {
                WorkTimeStopWatch.Start();

                if (PauseTimeStopWatch.IsRunning)
                    PauseTimeStopWatch.Pause();
            });

            StopWorkTimeStopWatch = new RelayCommand(o =>
            {
                WorkTimeStopWatch.Stop();
                PauseTimeStopWatch.Stop();
                SaveCurrentWorkTime();
            });

            PauseWorkTimeStopWatch = new RelayCommand(o =>
            {
                WorkTimeStopWatch.Pause();
                PauseTimeStopWatch.Start();
            });
        }

        private void SaveCurrentWorkTime()
        {
            var workTime = new WorkTime()
            {
                StartTime = WorkTimeStopWatch.StartTime,
                EndTime = WorkTimeStopWatch.EndTime
            };

            WorkTimes.Add(workTime);
            _context.WorkTimes.Add(workTime);
            _context.SaveChanges();
        }
    }
}
