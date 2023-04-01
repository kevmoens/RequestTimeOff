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
        List<Holiday> HolidayQuery(Expression<Func<Holiday, bool>> expression);
        List<TimeOff> TimeOffQuery(Expression<Func<TimeOff, bool>> expression);
        List<User> UserQuery(Expression<Func<User, bool>> expression);

        bool AddHoliday(Holiday holiday);
        bool RemoveHoliday(Holiday holiday);
        bool UpdateHoliday(Holiday holiday);
        bool UpdateTimeOff(TimeOff timeOff);
        bool AddTimeOff(TimeOff timeOff);
        bool RemoveTimeOff(TimeOff timeOff);
        bool AddUser(User user);
        bool RemoveUser(User user);
        bool UpdateUser(User user);
    }
}
