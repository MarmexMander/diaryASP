using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkDiary.Models
{
    public class Position
    {
        int id;
        int userId;
        double wage;
        string name;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public double Wage { get => wage; set => wage = value; }
        public string Name { get => name; set => name = value; }
    }
}
