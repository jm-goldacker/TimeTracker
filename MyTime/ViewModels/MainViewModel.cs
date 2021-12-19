using MyTime.Helpers;
using MyTime.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyTime.ViewModels
{
    class MainViewModel : Observerable
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public HomeViewModel HomeViewModel { get; set; }

        public TasksViewModel TasksViewModel { get; set; }

        public WorkTimeViewModel WorkTimeViewModel { get; set; }

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand TasksViewCommand { get; set; }

        public RelayCommand WorkTimeViewCommand { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            TasksViewModel = new TasksViewModel();
            WorkTimeViewModel = new WorkTimeViewModel();
            
            CurrentView = HomeViewModel;

            HomeViewCommand = new RelayCommand(o => CurrentView = HomeViewModel);
            TasksViewCommand = new RelayCommand(o => CurrentView = TasksViewModel);
            WorkTimeViewCommand = new RelayCommand(o => CurrentView = WorkTimeViewModel );
            CloseCommand = new RelayCommand(CloseWindow);

        }

        private void CloseWindow(object window)
        {
            if (window != null && window is Window)
            {
                ((Window)window).Close();
            }
        }
    }
}
