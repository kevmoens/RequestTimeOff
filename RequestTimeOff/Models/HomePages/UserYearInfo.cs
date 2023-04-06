using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.HomePages
{
    public class UserYearInfo : INotifyPropertyChanged, IUserYearInfo
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
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
            set { _year = value; OnPropertyChanged(); }
        }

        private int _vacRemain;
        public int VacRemain
        {
            get { return _vacRemain; }
            set { _vacRemain = value; OnPropertyChanged(); }
        }

        private int _sickRemain;
        public int SickRemain
        {
            get { return _sickRemain; }
            set { _sickRemain = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TimeOff> _schedule;
        public ObservableCollection<TimeOff> Schedule
        {
            get { return _schedule; }
            set { _schedule = value; OnPropertyChanged(); }
        }

        public async Task ChangeYear(int year)
        {
            var firstOfYear = GetFirstOfYear(year);
            var lastOfYear = GetLastOfYear(year);
            try
            {
                await Task.Run(() =>
                {
                    List<TimeOff> requests = _requestTimeOffRepository
                        .TimeOffQuery(new Func<TimeOff, bool>((t) => { return firstOfYear <= t.Date && t.Date <= lastOfYear; }))
                        .OrderBy(t => t.Date)
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

        private DateTimeOffset GetFirstOfYear(int year)
        {
            return new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }

        private DateTimeOffset GetLastOfYear(int year)
        {
            return new DateTimeOffset(year, 12, 31, 0, 0, 0, TimeSpan.Zero);
        }

    }
}
