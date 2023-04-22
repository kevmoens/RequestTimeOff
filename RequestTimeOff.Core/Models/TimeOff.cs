using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestTimeOff.Models
{
    public class TimeOff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTimeOffset Date { get; set; }
        public TimeOffType Type { get; set; }
        public TimeOffRange Range { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public bool Declined { get; set; }
        public string Reason { get; set; }
        public string Manager { get; set; }
    }
}
