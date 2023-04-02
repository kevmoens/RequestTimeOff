using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public class TimeOff
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public TimeOffRange Range { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public bool Declined { get; set; }
        public string Reason { get; set; }
        public string Manager { get; set; }
    }
}
