using System;

namespace WorkDiary.Models
{
    public class Log
    {
        public Log(int? userId, string @event)
        {
            this.UserId = userId;
            Date = DateTime.Now;
            this.Event = @event;
        }

        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime Date { get; set; }

        public string Event { get; set; }
    }
}