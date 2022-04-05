using Microsoft.EntityFrameworkCore;
using MyTime.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyTime.Repositories
{
    public class WorkTimeRepository : IWorkTimeRepository
    {
        private DatabaseContext _context = new();

        public IDictionary<DateTime, double> GetWorkTimesPerDay()
        {
            return _context.WorkTimes
                .ToList()
                .GroupBy(wt => wt.Start.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalHours = g.Sum(x => x.Duration.TotalHours)
                })
                .ToDictionary(d => d.Date, d => d.TotalHours);
        }

        public IReadOnlyCollection<WorkTime> GetWorkTimes()
        {
            return _context.WorkTimes
                .Include(wt => wt.PauseTimes)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<PauseTime> GetPauses()
        {
            return _context.PauseTimes
                .ToList()
                .AsReadOnly();
        }

        public void SaveWorkTime(WorkTime workTime)
        {
            _context.WorkTimes.Add(workTime);
            _context.SaveChanges();
        }

        public IReadOnlyCollection<TaskTime> GetTaskTimes()
        {
            return _context.TaskTimes
                .ToList()
                .AsReadOnly();
        }

        public void SaveTaskTime(TaskTime task)
        {
            _context.TaskTimes.Add(task);
            _context.SaveChanges();
        }
    }
}
