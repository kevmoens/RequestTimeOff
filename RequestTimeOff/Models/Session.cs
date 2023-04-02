using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public class Session
    {
        public User User { get; set; }
        public List<User> DepartmentColleagues { get; set; }
    }
}
