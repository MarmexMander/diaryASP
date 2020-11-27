namespace WorkDiary.Models
{
    public class Position
    {
        private int id;
        private int userId;
        private double wage;
        private string name;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public double Wage { get => wage; set => wage = value; }
        public string Name { get => name; set => name = value; }
    }
}