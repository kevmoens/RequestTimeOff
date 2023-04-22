using System;

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
