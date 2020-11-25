using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDiary.Models
{
    public class SampleData
    {
        public static void Initialize(DiaryContext context)
        {
            //Add data

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        FullName = "Root Admin",
                        PassHash = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
