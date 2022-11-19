using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyTime.ViewModels
{
    public class WorkTimeViewModel : BindableBase
    {
        public DelegateCommand StartWork { get; set; }

        public DelegateCommand StopWork { get; set; }

        public DelegateCommand PauseWork { get; set; }

        private ObservableCollection<WorkTime> _workTimes;

        public ObservableCollection<WorkTime> WorkTimes
        {
            get
            {
                return _workTimes;
            }
            set
            {
                SetProperty(ref _workTimes, value);
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
                SetProperty(ref _pauseTimes, value);
            }
        }

        public AccumulatedTimes AccumulatedWorkTimes { get; private set; } = new();

        public AccumulatedTimes AccumulatedPauseTimes { get; private set; } = new();

        public IWorkStopWatch WorkTimeStopWatch { get; }

        public IPauseStopWatch PauseTimeStopWatch { get; }

        private readonly List<PauseTime> _currentPauseTimes = new();

        private readonly IDatabaseRepository _repository;

        public WorkTimeViewModel(IDatabaseRepository repository, IWorkStopWatch workStopWatch, IPauseStopWatch pauseStopWatch)
        {
            _repository = repository;

            WorkTimeStopWatch = workStopWatch;
            PauseTimeStopWatch = pauseStopWatch;

            LoadTimes();

            StartWork = new DelegateCommand(OnStartWorkExecute, OnStartWorkCanExecute);
            StopWork = new DelegateCommand(OnStopWorkExecute, OnStopWorkCanExecute);
            PauseWork = new DelegateCommand(OnPauseWorkExecute, OnPauseWorkCanExecute);
        }

        private void LoadTimes()
        {
            WorkTimes = new(_repository.GetWorkTimes());
            PauseTimes = new(_repository.GetPauses());
            AccumulatedWorkTimes.UpdateAccumulatedWorkTimes(WorkTimes);
            AccumulatedPauseTimes.UpdateAccumulatedWorkTimes(PauseTimes);
        }

        private void OnStartWorkExecute()
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

        private bool OnStartWorkCanExecute()
        {
            return !WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning;
        }

        private void OnStopWorkExecute()
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

        private bool OnStopWorkCanExecute()
        {
            return WorkTimeStopWatch.IsRunning || PauseTimeStopWatch.IsRunning;
        }

        private void OnPauseWorkExecute()
        {
            PauseTimeStopWatch.Start();
        }

        private bool OnPauseWorkCanExecute()
        {
            return WorkTimeStopWatch.IsRunning && !PauseTimeStopWatch.IsRunning;
        }
    }
}
