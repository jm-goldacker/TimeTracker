using Microsoft.EntityFrameworkCore;
using MyTime.Models.Database;

namespace MyTime.Repositories
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
                "Data Source=timeTracker.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
