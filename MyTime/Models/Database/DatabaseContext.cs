using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTime.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<WorkTime> WorkTimes { get; set; }
        public DbSet<PauseTime> PauseTimes { get; set; }
        public DbSet<TaskTime> TaskTimes { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=C:\\Users\\golda\\source\\repos\\MyTime\\MyTime\\timeTracker.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
