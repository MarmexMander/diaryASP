﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDiary.Models
{
    public class User
    {
        int id;
        string fullName;
        string email;
        string passHash;
        DateTime birthdTime;
        int accessLevel;

        public string Email { get => email; set => email = value; }
        public int AccessLevel { get => accessLevel; set => accessLevel = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public int Id { get => id; set => id = value; }
        public string PassHash { get => passHash; set => passHash = value; }
        public DateTime BirthdTime { get => birthdTime; set => birthdTime = value; }
    }
}