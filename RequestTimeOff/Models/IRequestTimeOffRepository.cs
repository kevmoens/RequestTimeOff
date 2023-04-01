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
    }
}
