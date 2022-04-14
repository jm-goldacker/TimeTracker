using Autofac;
using MyTime.Models.StopWatches;
using MyTime.Repositories;
using MyTime.ViewModels;
using MyTime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime
{
    internal class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DatabaseRepository>().As<IDatabaseRepository>();

            builder.RegisterType<HomeViewModel>().AsSelf();
            builder.RegisterType<WorkTimeViewModel>().AsSelf();
            builder.RegisterType<TasksViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<HomeView>().AsSelf();

            builder.RegisterType<PauseStopWatch>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<TaskStopWatch>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<WorkStopWatch>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            return builder.Build();
        }
    }
}
