using RequestTimeOff.Models.Date;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        public void ValidateInput()
        {
            if (SelectedDate < _systemDateTime.Now())
            {
                throw new ArgumentException($"Date must be on or after {_systemDateTime.Now().Date.ToShortDateString()}");
            }
            if (_requestTimeOffRepository.HolidayQuery(h => h.Date == _systemDateTime.Now()).Any())
            {
                throw new ArgumentException("You can't request off a holiday.");
            }

            if (SelectedDate.DayOfWeek == DayOfWeek.Saturday || SelectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException( "Request cannot be on a weekend.");
            }
        }

        public void ValidateDates()
        {
            foreach (var date in NewDates)
            {
                if (ExistingRequests.Any(r => r.Date == date))
                {
                    throw new ArgumentException($"Unable to add a duplicate date {date.Date.ToShortDateString()}");
                }
                if (_requestTimeOffRepository.TimeOffQuery(t => t.Username == _session.User.Username && t.Date == date).Any())
                {
                    throw new ArgumentException($"Unable to add a duplicate date {date.Date.ToShortDateString()}");
                }
            }
        }
    }
}
