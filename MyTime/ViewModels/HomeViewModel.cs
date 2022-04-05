using LiveCharts;
using LiveCharts.Wpf;
using MyTime.Models;
using MyTime.Models.Database;
using MyTime.Repositories;
using System;
using System.Linq;

namespace MyTime.ViewModels
{
    public class HomeViewModel : Observerable
    {
        private readonly IDatabaseRepository _workTimeRepository = new DatabaseRepository();

        public HomeViewModel()
        {
            var workTimesPerDay = _workTimeRepository.GetWorkTimesPerDay();

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
