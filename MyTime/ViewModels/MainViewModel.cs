using MyTime.Models;
using MyTime.Repositories;
using System.Windows;

namespace MyTime.ViewModels
{
    public class MainViewModel : Observerable
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel HomeViewModel { get; }

        public TasksViewModel TasksViewModel { get; }

        public WorkTimeViewModel WorkTimeViewModel { get; }

        public RelayCommand HomeViewCommand { get; }

        public RelayCommand TasksViewCommand { get; }

        public RelayCommand WorkTimeViewCommand { get; }

        public RelayCommand CloseCommand { get; }

        public MainViewModel(IDatabaseRepository repo, HomeViewModel homeViewModel, TasksViewModel tasksViewModel, WorkTimeViewModel workTimeViewModel)
        {
            HomeViewModel = homeViewModel;
            TasksViewModel = tasksViewModel;
            WorkTimeViewModel = workTimeViewModel;

            CurrentView = homeViewModel;

            HomeViewCommand = new RelayCommand(o => CurrentView = HomeViewModel);
            TasksViewCommand = new RelayCommand(o => CurrentView = TasksViewModel);
            WorkTimeViewCommand = new RelayCommand(o => CurrentView = WorkTimeViewModel);
            CloseCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow(object? window)
        {
            if (window != null && window is Window)
            {
                ((Window)window).Close();
            }
        }
    }
}
