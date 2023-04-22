using System;

namespace RequestTimeOff.Models.Date
{
    public interface ISystemDateTime
    {
        DateTimeOffset Now();
    }
}
