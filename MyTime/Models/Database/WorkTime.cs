using System.Collections.Generic;

namespace MyTime.Models.Database
{
    public class WorkTime : BaseTime
    {
        public virtual ICollection<PauseTime> PauseTimes { get; set; } = new List<PauseTime>();
    }
}
