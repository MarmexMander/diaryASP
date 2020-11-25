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

            //if (!context.Objects.Any())
            //{
                //context.Objects.AddRange(
                //    new Object
                //    {
                //        Name = "Something 1",
                //        Company = "Company 1",
                //        Price = 600
                //    },
                //    new Object
                //    {
                //        Name = "Something 2",
                //        Company = "Company 2",
                //        Price = 550
                //    },
                //    new Object
                //    {
                //        Name = "Something 3",
                //        Company = "Company 3",
                //        Price = 500
                //    }
                //);
                context.SaveChanges();
            //}
        }
    }
}
