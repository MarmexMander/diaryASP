using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDiary.Models
{
    public class Event
    {
        private int id;
        private string nameEvent;
        private string typeEvent;
        private float durationEvent;
        private DateTime dateTimeEvent;

        public int Id { get => id; set => id = value; }
        public string NameEvent { get => nameEvent; set => nameEvent = value; }
        public string TypeEvent { get => typeEvent; set => typeEvent = value; }
        public float DurationEvent { get => durationEvent; set => durationEvent = value; }
        public DateTime DateTimeEvent { get => dateTimeEvent; set => dateTimeEvent = value; }
        [NotMapped]
        public IEnumerable<User> PropertyUsers;

    }
}
