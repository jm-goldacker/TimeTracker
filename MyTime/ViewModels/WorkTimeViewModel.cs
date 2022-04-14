using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyTime.ViewModels
{
    public class WorkTimeViewModel : Observerable, IWorkTimeViewModel
    {
        public RelayCommand StartWork { get; set; }

        public RelayCommand StopWork { get; set; }

        public RelayCommand PauseWork { get; set; }

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

        private ObservableCollection<PauseTime> _pauseTimes;

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

        public AccumulatedTimes AccumulatedPauseTimes { get; private set; } = new();

        public WorkStopWatch WorkTimeStopWatch { get; } = WorkStopWatch.Instance;

        public PauseStopWatch PauseTimeStopWatch { get; } = PauseStopWatch.Instance;

        private readonly List<PauseTime> _currentPauseTimes = new();

        private readonly IDatabaseRepository _repository;

        public WorkTimeViewModel(IDatabaseRepository repository)
        {
            _repository = repository;
            LoadTimes();

            StartWork = new RelayCommand(OnStartWorkExecute, OnStartWorkCanExecute);
            StopWork = new RelayCommand(OnStopWorkExecute, OnStopWorkCanExecute);
            PauseWork = new RelayCommand(OnPauseWorkExecute, OnPauseWorkCanExecute);
        }

        private void LoadTimes()
        {
            WorkTimes = new(_repository.GetWorkTimes());
            PauseTimes = new(_repository.GetPauses());
            AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes);
            AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);
        }

        private void OnStartWorkExecute(object? o)
        {
            if (PauseTimeStopWatch.IsRunning)
            {
                PauseTimeStopWatch.Stop();
                SavePause();
                WorkTimeStopWatch.Start(true);
            }

            else
                WorkTimeStopWatch.Start();
        }

        private bool OnStartWorkCanExecute(object? o)
        {
            return !WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning;
        }

        private void OnStopWorkExecute(object? o)
        {
            WorkTimeStopWatch.Stop();
            SaveWorkTime();

            if (PauseTimeStopWatch.Duration.TotalSeconds > 0)
            {
                PauseTimeStopWatch.Stop();
                SavePause();
            }
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
            _repository.SaveWorkTime(workTime);
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

        private bool OnStopWorkCanExecute(object? o)
        {
            return WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning;
        }

        private void OnPauseWorkExecute(object? o)
        {
            PauseTimeStopWatch.Start();
        }

        private bool OnPauseWorkCanExecute(object? o)
        {
            return WorkTimeStopWatch.IsRunning && !PauseTimeStopWatch.IsRunning;
        }
    }
}
