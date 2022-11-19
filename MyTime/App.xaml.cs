using Autofac;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using MyTime.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace MyTime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();
            return window;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<WorkTimeView>();
            containerRegistry.RegisterForNavigation<TasksView>();
            containerRegistry.Register<IDatabaseRepository, DatabaseRepository>();
            containerRegistry.RegisterSingleton<ITaskStopWatch, TaskStopWatch>();
            containerRegistry.RegisterSingleton<IWorkStopWatch, WorkStopWatch>();
            containerRegistry.RegisterSingleton<IPauseStopWatch, PauseStopWatch>();
        }
    }
}
