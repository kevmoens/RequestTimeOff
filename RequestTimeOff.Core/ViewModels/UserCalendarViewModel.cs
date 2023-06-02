using RequestTimeOff.Core.ViewModels;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class UserCalendarViewModel : INotifyPropertyChanged, IUserCalendarViewModel
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;

        [ExcludeFromCodeCoverage]
        public UserCalendarViewModel(IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            Username = _session.User.Username;
            LoadedCommand = new DelegateCommand(OnLoaded);
            PreviousCommand = new DelegateCommand(OnPrevious);
            NextCommand = new DelegateCommand(OnNext);

        }


        public ICommand LoadedCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

        // S t r y k e r disable once all
        private ObservableCollection<int?> _dates = new ObservableCollection<int?>(from int? i in Enumerable.Range(0, 37) select (int?)null);

        // S t r y k e r disable once all
        private ObservableCollection<ObservableCollection<TimeOff>> _timeOffs = new ObservableCollection<ObservableCollection<TimeOff>>(from int? i in Enumerable.Range(0, 37) select new ObservableCollection<TimeOff>());

        private int _year = DateTime.Now.Year;

        [ExcludeFromCodeCoverage]
        public int Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(); }
        }

        private int _month = DateTime.Now.Month;

        [ExcludeFromCodeCoverage]
        public int Month
        {
            get { return _month; }
            set { _month = value; OnPropertyChanged(); }
        }

        private string _monthName;

        [ExcludeFromCodeCoverage]
        public string MonthName
        {
            get { return _monthName; }
            set { _monthName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Holiday> _holidays;

        [ExcludeFromCodeCoverage]
        public ObservableCollection<Holiday> Holidays
        {
            get { return _holidays; }
            set { _holidays = value; OnPropertyChanged(); }
        }

        [ExcludeFromCodeCoverage]
        public ObservableCollection<int?> Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged(); }
        }

        [ExcludeFromCodeCoverage]
        public ObservableCollection<ObservableCollection<TimeOff>> TimeOffs
        {
            get { return _timeOffs; }
            set { _timeOffs = value; OnPropertyChanged(); }
        }

        private string _username;
        [ExcludeFromCodeCoverage]
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        [ExcludeFromCodeCoverage]
        private void SetDate(int pos, int? day)
        {
            Dates[pos] = day; OnPropertyChanged($"Dates[{pos}]");
        }

        [ExcludeFromCodeCoverage]
        private void SetTimeOffs(int pos, List<TimeOff> offs)
        {
            TimeOffs[pos] = new ObservableCollection<TimeOff>(offs); OnPropertyChanged($"TimeOffs[{pos}]");
        }

        [ExcludeFromCodeCoverage]
        private async void OnLoaded()
        {
            await LoadMonth();
        }

        [ExcludeFromCodeCoverage]
        async void OnPrevious()
        {

            DateTime currDate = new DateTime(Year, Month, 1);
            currDate = currDate.AddMonths(-1);
            Year = currDate.Year;
            Month = currDate.Month;
            await LoadMonth();
        }

        [ExcludeFromCodeCoverage]
        async void OnNext()
        {
            DateTime currDate = new DateTime(Year, Month, 1);
            currDate = currDate.AddMonths(1);
            Year = currDate.Year;
            Month = currDate.Month;
            await LoadMonth();
        }
        [ExcludeFromCodeCoverage]
        private DayOfWeek GetDayOfWeek(int pos)
        {
            switch (pos % 7)
            {
                case 0: return DayOfWeek.Sunday;
                case 1: return DayOfWeek.Monday;
                case 2: return DayOfWeek.Tuesday;
                case 3: return DayOfWeek.Wednesday;
                case 4: return DayOfWeek.Thursday;
                case 5: return DayOfWeek.Friday;
                case 6: return DayOfWeek.Saturday;
            }
            return DayOfWeek.Sunday;
        }

        public async Task LoadMonth()
        {
            await Task.Run(() =>
            {
                DateTime currDate = new DateTime(Year, Month, 1);
                MonthName = currDate.ToString("MMMM");
                Holidays = new ObservableCollection<Holiday>(_requestTimeOffRepository.HolidayQuery(h => h.Date.Year == Year && h.Date.Month == Month));
                bool weekDayStarted = false;
                for (int i = 0; i <= 36; i++)
                {
                    if (currDate.DayOfWeek == GetDayOfWeek(i))
                    {
                        weekDayStarted = true;
                    }

                    if (weekDayStarted == false)
                    {
                        // Stryker disable all : Properties used for binding in the view 1
                        SetDate(i, null);
                        SetTimeOffs(i, Array.Empty<TimeOff>().ToList());
                        // Stryker restore all
                        continue;
                    }

                    if (currDate.Month != Month)
                    {
                        // Stryker disable all : Properties used for binding in the view 1
                        SetDate(i, null);
                        SetTimeOffs(i, Array.Empty<TimeOff>().ToList());
                        // Stryker restore all
                        continue;
                    }

                    // Stryker disable all : Properties used for binding in the view 1
                    SetDate(i, currDate.Day);
                    SetTimeOffs(i, _requestTimeOffRepository.TimeOffQuery(t => t.Date == currDate && t.Username == Username && t.Declined == false));
                    // Stryker restore all

                    currDate = currDate.AddDays(1);
                }
            });
        }

    }
}
