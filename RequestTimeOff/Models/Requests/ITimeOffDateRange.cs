using System;
using System.Collections.Generic;

namespace RequestTimeOff.Models.Requests
{
    public interface ITimeOffDateRange
    {
        int Reoccurance { get; set; }
        DateTimeOffset SelectedDate { get; set; }

        List<DateTimeOffset> GetDates();
    }
}