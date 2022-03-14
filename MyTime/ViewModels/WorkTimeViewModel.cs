using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyTime.Helpers;
using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
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
    class WorkTimeViewModel : Observerable
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public RelayCommand StartWorkTimeStopWatch {get; set; }

        public RelayCommand StopWorkTimeStopWatch { get; set;}

        public RelayCommand PauseWorkTimeStopWatch { get; set; }

        private TimeSpan _currentWorkTimeDuration = new TimeSpan();

        public TimeSpan CurrentWorkTimeDuration
        {
            get 
            { 
                return _currentWorkTimeDuration; 
            }
            set
            {
                _currentWorkTimeDuration = value;
                OnPropertyChanged();
            }
        }

        private PauseTime _currentPause = new PauseTime();

        public PauseTime CurrentPause
        {
            get => _currentPause;
            set
            {
                _currentPause = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<WorkTime> _workTimes;
        private ObservableCollection<PauseTime> _pauseTimes;

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
        public ObservableCollection<PauseTime> PauseTimes
        {
            get
            {
                return _pauseTimes;
            }
            set
            {
                _pauseTimes = value;
                OnPropertyChanged();
            }
        }

        public AccumulatedTimes AccumulatedWorkTimes { get; private set; } = new AccumulatedTimes();

        public AccumulatedTimes AccumulatedPauseTimes { get; private set; } = new AccumulatedTimes();

        public WorkTimeViewModel()
        {
            WorkTimes = new ObservableCollection<WorkTime>(_context.WorkTimes.ToList());
            PauseTimes = new ObservableCollection<PauseTime>(_context.PauseTimes.ToList());
            AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes); 
            AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);

            var WorkTimeStopWatch = WorkStopWatch.Instance;
            WorkTimeStopWatch.Tick += new EventHandler(delegate (object? sender, EventArgs e)
            {
                CurrentWorkTimeDuration = WorkTimeStopWatch.Duration;
            });

            var PauseTimeStopWatch = PauseStopWatch.Instance;
            PauseTimeStopWatch.Tick += new EventHandler(delegate (object? sender, EventArgs e)
            {
                CurrentPause.End = DateTime.Now;
            });

            // stop pause, create new pause, start work time
            StartWorkTimeStopWatch = new RelayCommand(o =>
            {
                if (PauseTimeStopWatch.IsRunning)
                {
                    PauseTimeStopWatch.Stop();
                    AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);
                }
                else if (!WorkTimeStopWatch.IsRunning)
                {
                    //CurrentWorkTime = new WorkTime();
                    //WorkTimes.Add(CurrentWorkTime);
                }

                WorkTimeStopWatch.Start();
            });

            // stop pause, stop worktime
            StopWorkTimeStopWatch = new RelayCommand(o =>
            {
                if (WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning)
                {
                    WorkTimeStopWatch.Stop();
                    PauseTimeStopWatch.Stop();

                    //_context.WorkTimes.Add((WorkTime)WorkStopWatch.Instance.CurrentTime);
                    _context.SaveChanges();

                    AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes);
                    AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);
                }
            });

            PauseWorkTimeStopWatch = new RelayCommand(o =>
            {
                if (WorkTimeStopWatch.IsRunning) 
                { 
                    WorkTimeStopWatch.Pause();

                    CurrentPause = new PauseTime();
                    PauseTimes.Add(CurrentPause);
                    //CurrentWorkTime.PauseTimes.Add(CurrentPause);

                    PauseTimeStopWatch.Start();
                }
            });
        }
    }
}
