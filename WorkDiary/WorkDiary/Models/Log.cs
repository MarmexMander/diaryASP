using System;

namespace WorkDiary.Models
{
    public class Log
    {
        public Log(string message, User user)
        {
            this.User = user;
            Date = DateTime.Now;
            this.Message = message;
        }
        public Log(string message)
        {
            this.User = null;
            Date = DateTime.Now;
            this.Message = message;
        }

        public int Id { get; set; }

        public User User { get; set; }

        public DateTime Date { get; set; }
        
        public string Message { get; set; }
    }
}