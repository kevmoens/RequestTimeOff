using System.Collections.Generic;

namespace RequestTimeOff.Models
{
    public class Session
    {
        public User User { get; set; }
        public List<User> DepartmentColleagues { get; set; }
    }
}
