using System.ComponentModel.DataAnnotations;

namespace RequestTimeOff.Models
{
    public class Department
    {
        [Key]
        public string Dept { get; set; }
        public string Description { get; set; }
    }
}
