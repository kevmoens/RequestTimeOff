using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.Date
{
    public interface ISystemDateTime
    {
        DateTimeOffset Now();
    }
}
