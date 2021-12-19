using MyTime.Helpers;
using MyTime.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTime.Models
{
    public class AccumulatedTimes : Observerable
    {
        private TimeSpan _dailyTime;
        private TimeSpan _weeklyTime;
        private TimeSpan _monthlyTime;

        public TimeSpan DailyTime { 
            get 
            {
                return _dailyTime;
            } 
            set
            {
                _dailyTime = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan WeeklyTime 
        { 
            get
            {
                return _weeklyTime;
            }
            private set
            {
                _weeklyTime = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan MonthlyTime 
        { 
            get
            {
                return _monthlyTime;
            }
            private set
            {
                _monthlyTime = value;
                OnPropertyChanged();
            }
        }

        public void UpdateAccumulatedWorkTimes(IEnumerable<BaseTime> workTimes)
        {
            DailyTime = new TimeSpan(workTimes
                            .Where(wt => wt.Start.DayOfYear == DateTime.Now.DayOfYear)
                            .ToList()
                            .Sum(wt => wt.Duration.Ticks));

            var currentDayOfWeek = DateTime.Now.DayOfWeek == DayOfWeek.Sunday 
                ? 7 
                : (double)DateTime.Now.DayOfWeek;
            var firstDayOfWeek = DateTime.Now.Subtract(TimeSpan.FromDays(currentDayOfWeek - 1));

            WeeklyTime = new TimeSpan(workTimes
                .Where(wt => wt.Start >= firstDayOfWeek)
                .ToList()
                .Sum(wt => wt.Duration.Ticks));
            MonthlyTime = new TimeSpan(workTimes
                .Where(wt => wt.Start.Month == DateTime.Now.Month)
                .ToList()
                .Sum(wt => wt.Duration.Ticks));
        }
    }
}
