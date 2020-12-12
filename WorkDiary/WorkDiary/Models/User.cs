using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;

namespace WorkDiary.Models
{
    public class User
    {
        private int id;
        private string fullName;
        private string email;
        private string passHash;
        private DateTime birthdTime;
        private int accessLevel;
        private string status;
        private List<Event> events;
        private List<Event> visitedEvents;

        private Position position;
        
        [Required]
        public string Email { get => email; set => email = value; }
        [Required]
        public int AccessLevel { get => accessLevel; set => accessLevel = value; }
        [Required]
        public string FullName { get => fullName; set => fullName = value; }
        public int Id { get => id; set => id = value; }
        [Required]
        public string PassHash { get => passHash; set => passHash = value; }
        [Required]
        public DateTime BirthdTime { get => birthdTime; set => birthdTime = value; }
        public string Status { get => status; set => status = value; }
        public List<Event> Events { get => events; set => events = value; }
        public List<Event> VisitedEvents { get => visitedEvents; set => visitedEvents = value; }
        public Position Position { get => position; set => position = value; }

        //
        public int getSalary()
        {
            //FINISH IT!!!!...........................................
            return 0;
        }
    }
}