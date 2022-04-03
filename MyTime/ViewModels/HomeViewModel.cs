using LiveCharts;
using LiveCharts.Wpf;
using MyTime.Models;
using MyTime.Models.Database;
using System;
using System.Linq;

namespace MyTime.ViewModels
{
    public class HomeViewModel : Observerable
    {
        private DatabaseContext _context = new DatabaseContext();
        public HomeViewModel()
        {
            var workTimes = _context.WorkTimes.ToList();
            var workTimesPerDay = workTimes
                .GroupBy(wt => wt.Start.Date)
                .Select(g => new { Date = g.Key, TotalHours = g.Sum(x => x.Duration.TotalHours) })
                .ToDictionary(d => d.Date, d => d.TotalHours);

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
