using System.ComponentModel.DataAnnotations;

namespace RequestTimeOff.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Dept { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int SickHrs { get; set; }
        public int VacHrs { get; set; }
    }
}
