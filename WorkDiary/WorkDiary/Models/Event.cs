using System;
using System.Collections.Generic;

namespace WorkDiary.Models
{
    public class Event
    {
        private int id;
        private string nameEvent;
        private string typeEvent;
        private float durationEvent;
        private DateTime dateTimeEvent;
        private List<User> users;
        private List<User> showedUpUsers;
        
        public int Id { get => id; set => id = value; }
        public string NameEvent { get => nameEvent; set => nameEvent = value; }
        public string TypeEvent { get => typeEvent; set => typeEvent = value; }
        public float DurationEvent { get => durationEvent; set => durationEvent = value; }
        public DateTime DateTimeEvent { get => dateTimeEvent; set => dateTimeEvent = value; }
        public List<User> Users { get => users; set => users = value; }
        public List<User> ShowedUpUsers { get => showedUpUsers; set => showedUpUsers = value; }
    }
}
