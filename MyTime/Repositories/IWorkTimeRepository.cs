using MyTime.Models.Database;
using System;
using System.Collections.Generic;

namespace MyTime.Repositories
{
    public interface IWorkTimeRepository
    {
        IDictionary<DateTime, double> GetWorkTimesPerDay();

        IReadOnlyCollection<WorkTime> GetWorkTimes();
        IReadOnlyCollection<PauseTime> GetPauses();
        void SaveWorkTime(WorkTime workTime);
    }
}