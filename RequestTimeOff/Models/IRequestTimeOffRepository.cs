using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public interface IRequestTimeOffRepository
    {
        List<Holiday> HolidayQuery(Func<Holiday, bool> expression);
        List<TimeOff> TimeOffQuery(Func<TimeOff, bool> expression);
        List<User> UserQuery(Func<User, bool> expression);
        List<Department> DepartmentQuery(Func<Department, bool> expression);

        bool AddHoliday(Holiday holiday);
        bool RemoveHoliday(Holiday holiday);
        bool UpdateHoliday(Holiday holiday);
        bool UpdateTimeOff(TimeOff timeOff);
        bool AddTimeOff(TimeOff timeOff);
        bool RemoveTimeOff(TimeOff timeOff);
        bool AddUser(User user);
        bool RemoveUser(User user);
        bool UpdateUser(User user);
        bool AddDepartment(Department department);
        bool RemoveDepartment(Department department);
        bool UpdateDepartment(Department department);
    }
}
