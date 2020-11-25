using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDiary.Models
{
    public class Log
    {
        int id;
        int? userId;
        DateTime date;
        string _event;

        public int Id { get => id; set => id = value; }
        public int? UserId { get => userId; set => userId = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Event { get => _event; set => _event = value; }
    }
}
