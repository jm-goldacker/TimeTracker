using MyTime.Models;
using System.Windows;

namespace MyTime.ViewModels
{
    class MainViewModel : Observerable
    {
        private object _currentView = new HomeViewModel();

        public object CurrentView
        {
            get { return _currentView; }
            private set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public HomeViewModel HomeViewModel { get; } = new();

        public TasksViewModel TasksViewModel { get; } = new();

        public WorkTimeViewModel WorkTimeViewModel { get; } = new();

        public RelayCommand HomeViewCommand { get; }

        public RelayCommand TasksViewCommand { get; }

        public RelayCommand WorkTimeViewCommand { get; }

        public RelayCommand CloseCommand { get; }

        public MainViewModel()
        {            
            HomeViewCommand = new RelayCommand(o => CurrentView = HomeViewModel);
            TasksViewCommand = new RelayCommand(o => CurrentView = TasksViewModel);
            WorkTimeViewCommand = new RelayCommand(o => CurrentView = WorkTimeViewModel );
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
