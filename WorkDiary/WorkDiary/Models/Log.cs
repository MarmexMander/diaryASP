using System;

namespace WorkDiary.Models
{
    public class Log
    {
        private int id;
        private int? userId;
        private DateTime date;
        private string @event;

        public int Id { get => id; set => id = value; }
        public int? UserId { get => userId; set => userId = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Event { get => @event; set => @event = value; }

        public Log(int? userId, string @event)
        {
            this.userId = userId;
            date = DateTime.Now;
            this.@event = @event;
        }
    }
}