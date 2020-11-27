using Microsoft.EntityFrameworkCore;

namespace WorkDiary.Models
{
    public class DiaryContext : DbContext
    {
        //DB arrays
        public DbSet<User> Users { get; set; }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            SampleData.Initialize(this);
        }
    }
}