using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public class Holiday
    {
        public string Name { get; set; }
        [Key]
        public DateTimeOffset Date { get; set; }
    }
}
