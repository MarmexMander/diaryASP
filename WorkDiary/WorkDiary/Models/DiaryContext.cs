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
        public DbSet<Object> Product { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options)
            : base(options)
        {

            Database.EnsureCreated();
        }
    }
}
