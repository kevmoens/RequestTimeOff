using System;
using System.ComponentModel.DataAnnotations;

namespace RequestTimeOff.Models
{
    public class Holiday
    {
        public string Name { get; set; }
        [Key]
        public DateTimeOffset Date { get; set; }
    }
}
