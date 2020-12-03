using System;
using System.Collections.Generic;
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
        private Position position;
        
        public string Email { get => email; set => email = value; }
        public int AccessLevel { get => accessLevel; set => accessLevel = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public int Id { get => id; set => id = value; }
        public string PassHash { get => passHash; set => passHash = value; }
        public DateTime BirthdTime { get => birthdTime; set => birthdTime = value; }
        public string Status { get => status; set => status = value; }
        public List<Event> Events { get => events; set => events = value; }
        public Position Position { get => position; set => position = value; }

        //
        public int getSalary()
        {
            //FINISH IT!!!!...........................................
            return 0;
        }
    }
}