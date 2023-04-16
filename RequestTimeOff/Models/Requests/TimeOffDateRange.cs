using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.Requests
{
    /// <summary>
    /// This class is used to calculate the dates for a time off request when having a reoccurance.
    /// It skips Holdays and weekends
    /// </summary>
    public class TimeOffDateRange : ITimeOffDateRange
    {
        public DateTimeOffset SelectedDate { get; set; }
        public int Reoccurance { get; set; }

        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private List<Holiday> _holidays;

        public TimeOffDateRange(IRequestTimeOffRepository requestTimeOffRepository)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
        }

        public List<DateTimeOffset> GetDates()
        {
            _holidays = GetHolidays();
            DateTimeOffset currDate = SelectedDate;
            List<DateTimeOffset> result = new()
            {
                currDate
            };
            for (int i = 1; i < Reoccurance; i++)
            {
                currDate = GetNextDate(currDate);
                result.Add(currDate);
            }
            return result;
        }

        private DateTimeOffset GetNextDate(DateTimeOffset currDate)
        {
            currDate = currDate.AddDays(1);
            bool TryAgain = true;
            while (TryAgain)
            {
                TryAgain = false;
                if (currDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    currDate = currDate.AddDays(2);
                }
                //else if (currDate.DayOfWeek == DayOfWeek.Sunday)
                //{
                //    currDate = currDate.AddDays(1);
                //}
                if (_holidays.Where(r => r.Date.Date == currDate.Date).Any())
                {
                    currDate = currDate.AddDays(1);
                    TryAgain = true;
                }
            }

            return currDate;
        }

        private List<Holiday> GetHolidays()
        {
            return _requestTimeOffRepository
                .HolidayQuery(HolidayFilterBySelectedDate(SelectedDate)
                    );
        }

        private const int OneMonth = 30;
        private static Func<Holiday, bool> _holidayFilter;
        private static DateTimeOffset _holidayFilterSelectedDate;
        public static Func<Holiday, bool> HolidayFilterBySelectedDate(DateTimeOffset selectedDate) 
        { 
            if (_holidayFilter != null && _holidayFilterSelectedDate == selectedDate)
            {
                return _holidayFilter;
            }
            _holidayFilterSelectedDate = selectedDate;
            _holidayFilter =  h => selectedDate <= h.Date
                && h.Date <= selectedDate.AddDays(OneMonth);
            return _holidayFilter;
        }
    }

}
