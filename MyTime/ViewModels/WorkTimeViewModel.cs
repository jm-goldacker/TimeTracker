using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyTime.Helpers;
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
    class WorkTimeViewModel : Observerable
    {
        private readonly DatabaseContext _context;

        public StopWatch WorkTimeStopWatch { get; set; }
        public StopWatch PauseTimeStopWatch {get; set; }

        public RelayCommand StartWorkTimeStopWatch {get; set; }

        public RelayCommand StopWorkTimeStopWatch { get; set;}

        public RelayCommand PauseWorkTimeStopWatch { get; set; }


        private WorkTime _currentWorkTime = new WorkTime();

        public WorkTime CurrentWorkTime
        {
            get => _currentWorkTime;
            set
            {
                _currentWorkTime = value;
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
            _context = new DatabaseContext();
            WorkTimes = new ObservableCollection<WorkTime>(_context.WorkTimes.ToList());
            PauseTimes = new ObservableCollection<PauseTime>(_context.PauseTimes.ToList());
            AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes); 
            AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);

            WorkTimeStopWatch = new StopWatch();
            WorkTimeStopWatch.Tick += new EventHandler(delegate (object sender, EventArgs e)
            {
                CurrentWorkTime.End = DateTime.Now;
            });

            PauseTimeStopWatch = new StopWatch();
            PauseTimeStopWatch.Tick += new EventHandler(delegate (object sender, EventArgs e)
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
                    CurrentWorkTime = new WorkTime();
                    WorkTimes.Add(CurrentWorkTime);
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

                    _context.WorkTimes.Add(CurrentWorkTime);
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
                    CurrentWorkTime.PauseTimes.Add(CurrentPause);

                    PauseTimeStopWatch.Start();
                }
            });
        }
    }
}
