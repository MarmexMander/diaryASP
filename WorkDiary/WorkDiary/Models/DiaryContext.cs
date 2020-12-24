using Microsoft.EntityFrameworkCore;

namespace WorkDiary.Models
{
    public class DiaryContext : DbContext
    {
        //DB arrays
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Event> Events { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            SampleData.Initialize(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasMany(e => e.Users).WithMany(u => u.Events);
            modelBuilder.Entity<Event>().HasMany(e => e.ShowedUpUsers).WithMany(u=>u.VisitedEvents);
            modelBuilder.Entity<User>().HasOne(u => u.Position).WithMany();
        }
    }
}