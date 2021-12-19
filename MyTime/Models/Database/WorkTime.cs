using MyTime.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTime.Models.Database
{
    public class WorkTime : BaseTime
    {
        public virtual ICollection<PauseTime> PauseTimes { get; set; } = new List<PauseTime>();
    }
}
