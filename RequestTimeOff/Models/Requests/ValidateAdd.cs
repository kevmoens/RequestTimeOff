using RequestTimeOff.Models.Date;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RequestTimeOff.Models.Requests
{
    public class ValidateAdd : IValidateAdd
    {
        public DateTimeOffset SelectedDate { get; set; }
        public ObservableCollection<TimeOff> ExistingRequests { get; set; }

        public List<DateTimeOffset> NewDates { get; set; }

        private ISystemDateTime _systemDateTime;
        private IRequestTimeOffRepository _requestTimeOffRepository;
        private Session _session;
        public ValidateAdd(ISystemDateTime systemDateTime, IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _systemDateTime = systemDateTime;
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
        }
        public string ValidateInput()
        {
            if (SelectedDate == null)
            {
                return "Date is required";
            }
            if (SelectedDate < _systemDateTime.Now().Date)
            {
                return $"Date must be on or after {_systemDateTime.Now().Date.ToShortDateString()}";
            }
            if (_requestTimeOffRepository.HolidayQuery(h => h.Date == _systemDateTime.Now()).Any())
            {
                return "You can't request off a holiday.";
            }
            if (SelectedDate.DayOfWeek == DayOfWeek.Saturday || SelectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return "Request cannot be on a weekend.";
            }
            return string.Empty;
        }

        public string ValidateDates()
        {
            foreach (var date in NewDates)
            {
                if (ExistingRequests.Any(r => r.Date == date))
                {
                    return $"Unable to add a duplicate date {date.Date.ToShortDateString()}";
                }
                if (_requestTimeOffRepository.TimeOffQuery(t => t.Username == _session.User.Username && t.Date == date).Any())
                {
                    return $"Unable to add a duplicate date {date.Date.ToShortDateString()}";
                }
            }
            return string.Empty;
        }
    }
}
