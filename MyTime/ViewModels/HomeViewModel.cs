﻿using LiveCharts;
using LiveCharts.Wpf;
using MyTime.Repositories;
using Prism.Mvvm;
using System;
using System.Linq;

namespace MyTime.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IDatabaseRepository _repository;

        public HomeViewModel(IDatabaseRepository repository)
        {
            _repository = repository;
            var workTimesPerDay = _repository.GetWorkTimesPerDay();

            SeriesCollection = new SeriesCollection()
            {
                new LineSeries()
                {
                    Title = "work time",
                    Values = new ChartValues<double> (workTimesPerDay.Values)
                }
            };

            Labels = workTimesPerDay.Keys.Select(k => k.ToString()).ToArray();
            YFormatter = value => value.ToString("F1");
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}
