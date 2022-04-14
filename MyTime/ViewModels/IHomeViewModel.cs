using LiveCharts;
using System;

namespace MyTime.ViewModels
{
    public interface IHomeViewModel
    {
        string[] Labels { get; set; }
        SeriesCollection SeriesCollection { get; set; }
        Func<double, string> YFormatter { get; set; }
    }
}