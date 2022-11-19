using MyTime.Models;
using MyTime.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace MyTime.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        //private object _currentView;

        //public object CurrentView
        //{
        //    get { return _currentView; }
        //    private set
        //    {
        //        _currentView = value;
        //        OnPropertyChanged();
        //    }
        //}

        public HomeViewModel HomeViewModel { get; }

        public TasksViewModel TasksViewModel { get; }

        public WorkTimeViewModel WorkTimeViewModel { get; }

        public DelegateCommand<string> NavigateToViewInContentRegionCommand { get; }

        public DelegateCommand<string> CloseCommand { get; } = new((s) => MessageBox.Show(s));

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion("ContentRegion", "HomeView");

            NavigateToViewInContentRegionCommand = new DelegateCommand<string>(NavigateToViewInContentRegion);
        }

        private void NavigateToViewInContentRegion(string viewName)
        {
            if (!string.IsNullOrEmpty(viewName))
            {
                regionManager.RequestNavigate("ContentRegion", viewName, Debug);
            }
        }

        public void Debug(NavigationResult r)
        {
            var t = r;
        }

        //public MainViewModel(IDatabaseRepository repo, HomeViewModel homeViewModel, TasksViewModel tasksViewModel, WorkTimeViewModel workTimeViewModel)
        //{
        //    HomeViewModel = homeViewModel;
        //    TasksViewModel = tasksViewModel;
        //    WorkTimeViewModel = workTimeViewModel;

        //    CurrentView = homeViewModel;

        //    HomeViewCommand = new RelayCommand(o => CurrentView = HomeViewModel);
        //    TasksViewCommand = new RelayCommand(o => CurrentView = TasksViewModel);
        //    WorkTimeViewCommand = new RelayCommand(o => CurrentView = WorkTimeViewModel);
        //    CloseCommand = new RelayCommand(CloseWindow);
        //}

        //private void CloseWindow(object? window)
        //{
        //    if (window != null && window is Window)
        //    {
        //        ((Window)window).Close();
        //    }
        //}
    }
}
