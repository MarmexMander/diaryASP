using System.Linq;

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
                        PassHash = "65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5",//qwerty
                        AccessLevel = 3,
                        Email = "qwerty@qwe.asd"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}