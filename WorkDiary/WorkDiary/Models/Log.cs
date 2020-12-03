using System;

namespace WorkDiary.Models
{
    public class Log
    {
        public Log(User userId, string message)
        {
            this.User = userId;
            Date = DateTime.Now;
            this.Message = message;
        }

        public int Id { get; set; }

        public User User { get; set; }

        public DateTime Date { get; set; }
        
        public string Message { get; set; }
    }
}