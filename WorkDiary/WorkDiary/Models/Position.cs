using System;
using System.Collections.Generic;

namespace WorkDiary.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public double Wage { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}