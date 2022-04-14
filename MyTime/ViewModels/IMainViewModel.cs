namespace MyTime.ViewModels
{
    public interface IMainViewModel
    {
        RelayCommand CloseCommand { get; }
        object CurrentView { get; }
        RelayCommand HomeViewCommand { get; }
        IHomeViewModel HomeViewModel { get; }
        RelayCommand TasksViewCommand { get; }
        ITasksViewModel TasksViewModel { get; }
        RelayCommand WorkTimeViewCommand { get; }
        IWorkTimeViewModel WorkTimeViewModel { get; }
    }
}