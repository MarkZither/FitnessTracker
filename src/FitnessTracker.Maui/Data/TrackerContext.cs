using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Maui.Data
{
    public class TrackerContext : DbContext
    {
        public TrackerContext()
        { }
        public TrackerContext(DbContextOptions<TrackerContext> options) : base(options)
        {
        }

        public DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>().ToTable("Route");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=TrackerOnConfiguring.db");
        }
    }
}
