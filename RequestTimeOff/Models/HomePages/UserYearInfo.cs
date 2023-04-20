using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace RequestTimeOff.Models.HomePages
{
    /// <summary>
    /// When the user changes the year, this class will update the user's schedule and remaining hours.
    /// This is used on the HomePage for the user to display year information on Guages.
    /// </summary>
    public class UserYearInfo : INotifyPropertyChanged, IUserYearInfo
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly Session _session;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly ILogger<UserYearInfo> _logger;
        public UserYearInfo(Session session, IRequestTimeOffRepository requestTimeOffRepository, ILogger<UserYearInfo> logger)
        {
            _session = session;
            _requestTimeOffRepository = requestTimeOffRepository;
            _logger = logger;
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            private set { _year = value; OnPropertyChanged(); }
        }

        private int _vacRemain;
        public int VacRemain
        {
            get { return _vacRemain; }
            private set { _vacRemain = value; OnPropertyChanged(); }
        }

        private int _sickRemain;
        public int SickRemain
        {
            get { return _sickRemain; }
            private set { _sickRemain = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TimeOff> _schedule;
        public ObservableCollection<TimeOff> Schedule
        {
            get { return _schedule; }
            private set { _schedule = value; OnPropertyChanged(); }
        }

        public async Task ChangeYear(int year)
        {
            Year = year; ;
            var firstOfYear = GetFirstOfYear(year);
            var lastOfYear = GetLastOfYear(year);
            var timeOffFilter = TimeOffFilterByRange(firstOfYear, lastOfYear);
            try
            {
                await Task.Run(() =>
                {
                    List<TimeOff> requests = _requestTimeOffRepository
                        .TimeOffQuery(timeOffFilter)
                        .ToList();
                    Schedule = new ObservableCollection<TimeOff>(requests);

                    var sickReqs = requests.Where(t => t.Type == TimeOffType.Sick).ToList();
                    var vacReqs = Schedule.Where(t => t.Type == TimeOffType.Vacation).ToList();

                    SickRemain = _session.User.SickHrs - sickReqs.Sum(t => t.Range.Hours());
                    VacRemain = _session.User.VacHrs - vacReqs.Sum(t => t.Range.Hours());
                });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "ChangeYear");
            }
        }

        private static DateTimeOffset GetFirstOfYear(int year)
        {
            return new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }

        private static DateTimeOffset GetLastOfYear(int year)
        {
            return new DateTimeOffset(year, 12, 31, 0, 0, 0, TimeSpan.Zero);
        }

        private static Func<TimeOff, bool> _timeOffFilter;
        private static DateTimeOffset _timeOffFilterStartDate;
        private static DateTimeOffset _timeOffFilterEndDate;
        [ExcludeFromCodeCoverage]
        internal static Func<TimeOff, bool> TimeOffFilterByRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            if (_timeOffFilter != null && _timeOffFilterStartDate == startDate && _timeOffFilterEndDate == endDate)
            {
                return _timeOffFilter;
            }
            _timeOffFilterStartDate = startDate;
            _timeOffFilterEndDate = endDate;
            _timeOffFilter = h => startDate <= h.Date
                && h.Date <= endDate;
            return _timeOffFilter;
        }

    }
}
