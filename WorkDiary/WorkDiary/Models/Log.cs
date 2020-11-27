using System;

namespace WorkDiary.Models
{
    public class Log
    {
        private int id;
        private int? userId;
        private DateTime date;
        private string _event;

        public int Id { get => id; set => id = value; }
        public int? UserId { get => userId; set => userId = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Event { get => _event; set => _event = value; }
    }
}