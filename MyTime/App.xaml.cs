using Autofac;
using MyTime.ViewModels;
using System.Windows;

namespace MyTime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var binder = new Bootstrapper();
            var container = binder.Bootstrap();
            DISource.Resolver = (type) =>
            {
                return container.Resolve(type);
            };

            using (var scope = container.BeginLifetimeScope())
            {
                
                var mw = scope.Resolve<MainWindow>();
                mw.Show();
            }
        }

    }
}
