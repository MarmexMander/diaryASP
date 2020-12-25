using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkDiary.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int AccessLevel { get; set; }

        [Required]
        public string FullName { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string PassHash { get; set; }

        [Required]
        public DateTime BirthdTime { get; set; }

        public string Status { get; set; }

        public List<Event> Events { get; set; }

        public List<Event> VisitedEvents { get; set; }
        
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}