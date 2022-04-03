using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyTime.ViewModels
{
    class WorkTimeViewModel : Observerable
    {
        private readonly DatabaseContext _context = new();

        public RelayCommand StartWorkTime {get; set; }

        public RelayCommand StopWorkTime { get; set;}

        public RelayCommand PauseWorkTime { get; set; }

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

        public AccumulatedTimes AccumulatedWorkTimes { get; private set; } = new();

        private static readonly AccumulatedTimes accumulatedTimes = new();

        public AccumulatedTimes AccumulatedPauseTimes { get; private set; } = accumulatedTimes;
        public WorkStopWatch WorkTimeStopWatch { get; } = WorkStopWatch.Instance;
        public PauseStopWatch PauseTimeStopWatch { get; } = PauseStopWatch.Instance;

        private readonly List<PauseTime> _currentPauseTimes = new();

        public WorkTimeViewModel()
        {
            WorkTimes = new ObservableCollection<WorkTime>(_context.WorkTimes.ToList());
            PauseTimes = new ObservableCollection<PauseTime>(_context.PauseTimes.ToList());
            AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes);
            AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);

            // stop pause, create new pause, start work time
            StartWorkTime = new RelayCommand(o => 
            {
                if (PauseTimeStopWatch.IsRunning)
                {
                    PauseTimeStopWatch.Stop();
                    SavePause();
                    WorkTimeStopWatch.Start(true);
                }

                else
                    WorkTimeStopWatch.Start();
            }, o => !WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning);

            // stop pause, stop worktime
            StopWorkTime = new RelayCommand(o =>
            {
                WorkTimeStopWatch.Stop();
                SaveWorkTime();

                if (PauseTimeStopWatch.Duration.TotalSeconds > 0)
                {
                    PauseTimeStopWatch.Stop();
                    SavePause();
                }
            }, o => WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning);

            PauseWorkTime = new RelayCommand(o =>
            {
                //WorkTimeStopWatch.Pause();
                PauseTimeStopWatch.Start();
            }, o => WorkTimeStopWatch.IsRunning && !PauseTimeStopWatch.IsRunning);
        }

        private void SaveWorkTime()
        {
            var workTime = new WorkTime
            {
                Start = WorkTimeStopWatch.StartTime,
                End = WorkTimeStopWatch.EndTime,
                PauseTimes = _currentPauseTimes
            };

            WorkTimes.Add(workTime);
            AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes);
            _context.WorkTimes.Add(workTime);
            _context.SaveChanges();
        }

        private void SavePause()
        {
            var pauseTime = new PauseTime
            {
                Start = PauseTimeStopWatch.StartTime,
                End = PauseTimeStopWatch.EndTime
            };

            PauseTimes.Add(pauseTime);
            AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);

            _currentPauseTimes.Add(pauseTime);
        }
    }
}
