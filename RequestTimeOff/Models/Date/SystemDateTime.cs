using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.Date
{
    public class SystemDateTime : ISystemDateTime
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}
