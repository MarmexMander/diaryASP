using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
