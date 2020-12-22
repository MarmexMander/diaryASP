using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDiary.Models
{
    public class UserViewModel
    {
        public User Model { get; set; }
        public List<Position> Positions { get; set; }
    }
}
