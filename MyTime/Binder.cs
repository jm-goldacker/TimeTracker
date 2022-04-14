using Autofac;
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
    internal class Binder
    {
        public IContainer Bind()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DatabaseRepository>().As<IDatabaseRepository>();

            builder.RegisterType<HomeViewModel>().AsSelf();
            builder.RegisterType<WorkTimeViewModel>().AsSelf();
            builder.RegisterType<TasksViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<HomeView>().AsSelf();

            return builder.Build();
        }
    }
}
