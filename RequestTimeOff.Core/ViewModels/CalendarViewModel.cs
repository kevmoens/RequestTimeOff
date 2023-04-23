using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
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
        [ExcludeFromCodeCoverage]
        public CalendarViewModel(IRequestTimeOffRepository requestTimeOffRepository)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            LoadedCommand = new DelegateCommand(OnLoaded);
            PreviousCommand = new DelegateCommand(OnPrevious);
            NextCommand = new DelegateCommand(OnNext);

        }


        public ICommand LoadedCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

        private ObservableCollection<int?> _dates = new ObservableCollection<int?>
        {
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null },
            { null }
        };

        private ObservableCollection<ObservableCollection<TimeOff>> _timeOffs = new ObservableCollection<ObservableCollection<TimeOff>>
        { 
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() },
            {new ObservableCollection<TimeOff>() }
        };

        // Stryker disable all : Properties used for binding in the view 1
        private int _year = DateTime.Now.Year;

        public int Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(); }
        }

        private int _month = DateTime.Now.Month;

        public int Month
        {
            get { return _month; }
            set { _month = value; OnPropertyChanged(); }
        }

        private string _monthName;

        public string MonthName
        {
            get { return _monthName; }
            set { _monthName = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Holiday> _holidays;

        public ObservableCollection<Holiday> Holidays
        {
            get { return _holidays; }
            set { _holidays = value; OnPropertyChanged(); }
        }

        public ObservableCollection<int?> Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ObservableCollection<TimeOff>> TimeOffs
        {
            get { return _timeOffs; }
            set { _timeOffs = value; OnPropertyChanged(); }
        }
        // Stryker restore all

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
        private void OnLoaded()
        {
            LoadMonth();
        }

        [ExcludeFromCodeCoverage]
        void OnPrevious()
        {

            DateTime currDate = new DateTime(Year, Month, 1);
            currDate = currDate.AddMonths(-1);
            Year = currDate.Year;
            Month = currDate.Month;
            LoadMonth();
        }

        [ExcludeFromCodeCoverage]
        void OnNext()
        {
            DateTime currDate = new DateTime(Year, Month, 1);
            currDate = currDate.AddMonths(1);
            Year = currDate.Year;
            Month = currDate.Month;
            LoadMonth();
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

        internal void LoadMonth()
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
                    SetTimeOffs(i, new List<TimeOff>());
                    // Stryker restore all
                    continue;
                }

                if (currDate.Month != Month)
                {
                    // Stryker disable all : Properties used for binding in the view 1
                    SetDate(i, null);
                    SetTimeOffs(i, new List<TimeOff>());
                    // Stryker restore all
                    continue;
                }

                // Stryker disable all : Properties used for binding in the view 1
                SetDate(i, currDate.Day);
                SetTimeOffs(i, _requestTimeOffRepository.TimeOffQuery(t => t.Date == currDate));
                // Stryker restore all

                currDate = currDate.AddDays(1);
            }
        }

    }
}
