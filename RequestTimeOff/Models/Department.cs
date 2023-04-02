using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public class Department
    {
        [Key]
        public string Dept { get; set; }
        public string Description { get; set; }
    }
}
