using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class UserCalendarViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;
        public UserCalendarViewModel(IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            LoadedCommand = new DelegateCommand(OnLoaded);
            PreviousCommand = new DelegateCommand(OnPrevious);
            NextCommand = new DelegateCommand(OnNext);

        }
        public ICommand LoadedCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }

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


        private int? _date10;
        private int? _date11;
        private int? _date12;
        private int? _date13;
        private int? _date14;
        private int? _date15;
        private int? _date16;

        private int? _date20;
        private int? _date21;
        private int? _date22;
        private int? _date23;
        private int? _date24;
        private int? _date25;
        private int? _date26;

        private int? _date30;
        private int? _date31;
        private int? _date32;
        private int? _date33;
        private int? _date34;
        private int? _date35;
        private int? _date36;

        private int? _date40;
        private int? _date41;
        private int? _date42;
        private int? _date43;
        private int? _date44;
        private int? _date45;
        private int? _date46;

        private int? _date50;
        private int? _date51;
        private int? _date52;
        private int? _date53;
        private int? _date54;
        private int? _date55;
        private int? _date56;

        private int? _date60;
        private int? _date61;

        public int? Date10 { get { return _date10; } set { _date10 = value; OnPropertyChanged(); } }
        public int? Date11 { get { return _date11; } set { _date11 = value; OnPropertyChanged(); } }
        public int? Date12 { get { return _date12; } set { _date12 = value; OnPropertyChanged(); } }
        public int? Date13 { get { return _date13; } set { _date13 = value; OnPropertyChanged(); } }
        public int? Date14 { get { return _date14; } set { _date14 = value; OnPropertyChanged(); } }
        public int? Date15 { get { return _date15; } set { _date15 = value; OnPropertyChanged(); } }
        public int? Date16 { get { return _date16; } set { _date16 = value; OnPropertyChanged(); } }

        public int? Date20 { get { return _date20; } set { _date20 = value; OnPropertyChanged(); } }
        public int? Date21 { get { return _date21; } set { _date21 = value; OnPropertyChanged(); } }
        public int? Date22 { get { return _date22; } set { _date22 = value; OnPropertyChanged(); } }
        public int? Date23 { get { return _date23; } set { _date23 = value; OnPropertyChanged(); } }
        public int? Date24 { get { return _date24; } set { _date24 = value; OnPropertyChanged(); } }
        public int? Date25 { get { return _date25; } set { _date25 = value; OnPropertyChanged(); } }
        public int? Date26 { get { return _date26; } set { _date26 = value; OnPropertyChanged(); } }

        public int? Date30 { get { return _date30; } set { _date30 = value; OnPropertyChanged(); } }
        public int? Date31 { get { return _date31; } set { _date31 = value; OnPropertyChanged(); } }
        public int? Date32 { get { return _date32; } set { _date32 = value; OnPropertyChanged(); } }
        public int? Date33 { get { return _date33; } set { _date33 = value; OnPropertyChanged(); } }
        public int? Date34 { get { return _date34; } set { _date34 = value; OnPropertyChanged(); } }
        public int? Date35 { get { return _date35; } set { _date35 = value; OnPropertyChanged(); } }
        public int? Date36 { get { return _date36; } set { _date36 = value; OnPropertyChanged(); } }

        public int? Date40 { get { return _date40; } set { _date40 = value; OnPropertyChanged(); } }
        public int? Date41 { get { return _date41; } set { _date41 = value; OnPropertyChanged(); } }
        public int? Date42 { get { return _date42; } set { _date42 = value; OnPropertyChanged(); } }
        public int? Date43 { get { return _date43; } set { _date43 = value; OnPropertyChanged(); } }
        public int? Date44 { get { return _date44; } set { _date44 = value; OnPropertyChanged(); } }
        public int? Date45 { get { return _date45; } set { _date45 = value; OnPropertyChanged(); } }
        public int? Date46 { get { return _date46; } set { _date46 = value; OnPropertyChanged(); } }

        public int? Date50 { get { return _date50; } set { _date50 = value; OnPropertyChanged(); } }
        public int? Date51 { get { return _date51; } set { _date51 = value; OnPropertyChanged(); } }
        public int? Date52 { get { return _date52; } set { _date52 = value; OnPropertyChanged(); } }
        public int? Date53 { get { return _date53; } set { _date53 = value; OnPropertyChanged(); } }
        public int? Date54 { get { return _date54; } set { _date54 = value; OnPropertyChanged(); } }
        public int? Date55 { get { return _date55; } set { _date55 = value; OnPropertyChanged(); } }
        public int? Date56 { get { return _date56; } set { _date56 = value; OnPropertyChanged(); } }

        public int? Date60 { get { return _date60; } set { _date60 = value; OnPropertyChanged(); } }
        public int? Date61 { get { return _date61; } set { _date61 = value; OnPropertyChanged(); } }


        private ObservableCollection<TimeOff> _timeOffs10;
        private ObservableCollection<TimeOff> _timeOffs11;
        private ObservableCollection<TimeOff> _timeOffs12;
        private ObservableCollection<TimeOff> _timeOffs13;
        private ObservableCollection<TimeOff> _timeOffs14;
        private ObservableCollection<TimeOff> _timeOffs15;
        private ObservableCollection<TimeOff> _timeOffs16;

        private ObservableCollection<TimeOff> _timeOffs20;
        private ObservableCollection<TimeOff> _timeOffs21;
        private ObservableCollection<TimeOff> _timeOffs22;
        private ObservableCollection<TimeOff> _timeOffs23;
        private ObservableCollection<TimeOff> _timeOffs24;
        private ObservableCollection<TimeOff> _timeOffs25;
        private ObservableCollection<TimeOff> _timeOffs26;

        private ObservableCollection<TimeOff> _timeOffs30;
        private ObservableCollection<TimeOff> _timeOffs31;
        private ObservableCollection<TimeOff> _timeOffs32;
        private ObservableCollection<TimeOff> _timeOffs33;
        private ObservableCollection<TimeOff> _timeOffs34;
        private ObservableCollection<TimeOff> _timeOffs35;
        private ObservableCollection<TimeOff> _timeOffs36;

        private ObservableCollection<TimeOff> _timeOffs40;
        private ObservableCollection<TimeOff> _timeOffs41;
        private ObservableCollection<TimeOff> _timeOffs42;
        private ObservableCollection<TimeOff> _timeOffs43;
        private ObservableCollection<TimeOff> _timeOffs44;
        private ObservableCollection<TimeOff> _timeOffs45;
        private ObservableCollection<TimeOff> _timeOffs46;

        private ObservableCollection<TimeOff> _timeOffs50;
        private ObservableCollection<TimeOff> _timeOffs51;
        private ObservableCollection<TimeOff> _timeOffs52;
        private ObservableCollection<TimeOff> _timeOffs53;
        private ObservableCollection<TimeOff> _timeOffs54;
        private ObservableCollection<TimeOff> _timeOffs55;
        private ObservableCollection<TimeOff> _timeOffs56;

        private ObservableCollection<TimeOff> _timeOffs60;
        private ObservableCollection<TimeOff> _timeOffs61;

        public ObservableCollection<TimeOff> TimeOffs10 { get { return _timeOffs10; } set { _timeOffs10 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs11 { get { return _timeOffs11; } set { _timeOffs11 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs12 { get { return _timeOffs12; } set { _timeOffs12 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs13 { get { return _timeOffs13; } set { _timeOffs13 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs14 { get { return _timeOffs14; } set { _timeOffs14 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs15 { get { return _timeOffs15; } set { _timeOffs15 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs16 { get { return _timeOffs16; } set { _timeOffs16 = value; OnPropertyChanged(); } }

        public ObservableCollection<TimeOff> TimeOffs20 { get { return _timeOffs20; } set { _timeOffs20 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs21 { get { return _timeOffs21; } set { _timeOffs21 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs22 { get { return _timeOffs22; } set { _timeOffs22 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs23 { get { return _timeOffs23; } set { _timeOffs23 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs24 { get { return _timeOffs24; } set { _timeOffs24 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs25 { get { return _timeOffs25; } set { _timeOffs25 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs26 { get { return _timeOffs26; } set { _timeOffs26 = value; OnPropertyChanged(); } }

        public ObservableCollection<TimeOff> TimeOffs30 { get { return _timeOffs30; } set { _timeOffs30 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs31 { get { return _timeOffs31; } set { _timeOffs31 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs32 { get { return _timeOffs32; } set { _timeOffs32 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs33 { get { return _timeOffs33; } set { _timeOffs33 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs34 { get { return _timeOffs34; } set { _timeOffs34 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs35 { get { return _timeOffs35; } set { _timeOffs35 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs36 { get { return _timeOffs36; } set { _timeOffs36 = value; OnPropertyChanged(); } }

        public ObservableCollection<TimeOff> TimeOffs40 { get { return _timeOffs40; } set { _timeOffs40 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs41 { get { return _timeOffs41; } set { _timeOffs41 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs42 { get { return _timeOffs42; } set { _timeOffs42 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs43 { get { return _timeOffs43; } set { _timeOffs43 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs44 { get { return _timeOffs44; } set { _timeOffs44 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs45 { get { return _timeOffs45; } set { _timeOffs45 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs46 { get { return _timeOffs46; } set { _timeOffs46 = value; OnPropertyChanged(); } }

        public ObservableCollection<TimeOff> TimeOffs50 { get { return _timeOffs50; } set { _timeOffs50 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs51 { get { return _timeOffs51; } set { _timeOffs51 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs52 { get { return _timeOffs52; } set { _timeOffs52 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs53 { get { return _timeOffs53; } set { _timeOffs53 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs54 { get { return _timeOffs54; } set { _timeOffs54 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs55 { get { return _timeOffs55; } set { _timeOffs55 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs56 { get { return _timeOffs56; } set { _timeOffs56 = value; OnPropertyChanged(); } }

        public ObservableCollection<TimeOff> TimeOffs60 { get { return _timeOffs60; } set { _timeOffs60 = value; OnPropertyChanged(); } }
        public ObservableCollection<TimeOff> TimeOffs61 { get { return _timeOffs61; } set { _timeOffs61 = value; OnPropertyChanged(); } }
        private void SetDate(int pos, int? day)
        {
            switch (pos)
            {
                case 0: Date10 = day; break;
                case 1: Date11 = day; break;
                case 2: Date12 = day; break;
                case 3: Date13 = day; break;
                case 4: Date14 = day; break;
                case 5: Date15 = day; break;
                case 6: Date16 = day; break;
                case 7: Date20 = day; break;
                case 8: Date21 = day; break;
                case 9: Date22 = day; break;
                case 10: Date23 = day; break;
                case 11: Date24 = day; break;
                case 12: Date25 = day; break;
                case 13: Date26 = day; break;
                case 14: Date30 = day; break;
                case 15: Date31 = day; break;
                case 16: Date32 = day; break;
                case 17: Date33 = day; break;
                case 18: Date34 = day; break;
                case 19: Date35 = day; break;
                case 20: Date36 = day; break;
                case 21: Date40 = day; break;
                case 22: Date41 = day; break;
                case 23: Date42 = day; break;
                case 24: Date43 = day; break;
                case 25: Date44 = day; break;
                case 26: Date45 = day; break;
                case 27: Date46 = day; break;
                case 28: Date50 = day; break;
                case 29: Date51 = day; break;
                case 30: Date52 = day; break;
                case 31: Date53 = day; break;
                case 32: Date54 = day; break;
                case 33: Date55 = day; break;
                case 34: Date56 = day; break;
                case 35: Date60 = day; break;
                case 36: Date61 = day; break;
            }
        }

        private void SetTimeOffs(int pos, List<TimeOff> offs)
        {
            switch (pos)
            {
                case 0: TimeOffs10 = new ObservableCollection<TimeOff>(offs); break;
                case 1: TimeOffs11 = new ObservableCollection<TimeOff>(offs); break;
                case 2: TimeOffs12 = new ObservableCollection<TimeOff>(offs); break;
                case 3: TimeOffs13 = new ObservableCollection<TimeOff>(offs); break;
                case 4: TimeOffs14 = new ObservableCollection<TimeOff>(offs); break;
                case 5: TimeOffs15 = new ObservableCollection<TimeOff>(offs); break;
                case 6: TimeOffs16 = new ObservableCollection<TimeOff>(offs); break;
                case 7: TimeOffs20 = new ObservableCollection<TimeOff>(offs); break;
                case 8: TimeOffs21 = new ObservableCollection<TimeOff>(offs); break;
                case 9: TimeOffs22 = new ObservableCollection<TimeOff>(offs); break;
                case 10: TimeOffs23 = new ObservableCollection<TimeOff>(offs); break;
                case 11: TimeOffs24 = new ObservableCollection<TimeOff>(offs); break;
                case 12: TimeOffs25 = new ObservableCollection<TimeOff>(offs); break;
                case 13: TimeOffs26 = new ObservableCollection<TimeOff>(offs); break;
                case 14: TimeOffs30 = new ObservableCollection<TimeOff>(offs); break;
                case 15: TimeOffs31 = new ObservableCollection<TimeOff>(offs); break;
                case 16: TimeOffs32 = new ObservableCollection<TimeOff>(offs); break;
                case 17: TimeOffs33 = new ObservableCollection<TimeOff>(offs); break;
                case 18: TimeOffs34 = new ObservableCollection<TimeOff>(offs); break;
                case 19: TimeOffs35 = new ObservableCollection<TimeOff>(offs); break;
                case 20: TimeOffs36 = new ObservableCollection<TimeOff>(offs); break;
                case 21: TimeOffs40 = new ObservableCollection<TimeOff>(offs); break;
                case 22: TimeOffs41 = new ObservableCollection<TimeOff>(offs); break;
                case 23: TimeOffs42 = new ObservableCollection<TimeOff>(offs); break;
                case 24: TimeOffs43 = new ObservableCollection<TimeOff>(offs); break;
                case 25: TimeOffs44 = new ObservableCollection<TimeOff>(offs); break;
                case 26: TimeOffs45 = new ObservableCollection<TimeOff>(offs); break;
                case 27: TimeOffs46 = new ObservableCollection<TimeOff>(offs); break;
                case 28: TimeOffs50 = new ObservableCollection<TimeOff>(offs); break;
                case 29: TimeOffs51 = new ObservableCollection<TimeOff>(offs); break;
                case 30: TimeOffs52 = new ObservableCollection<TimeOff>(offs); break;
                case 31: TimeOffs53 = new ObservableCollection<TimeOff>(offs); break;
                case 32: TimeOffs54 = new ObservableCollection<TimeOff>(offs); break;
                case 33: TimeOffs55 = new ObservableCollection<TimeOff>(offs); break;
                case 34: TimeOffs56 = new ObservableCollection<TimeOff>(offs); break;
                case 35: TimeOffs60 = new ObservableCollection<TimeOff>(offs); break;
                case 36: TimeOffs61 = new ObservableCollection<TimeOff>(offs); break;
            }
        }

        private void OnLoaded()
        {
            LoadMonth();
        }
        void OnPrevious()
        {

            DateTime currDate = new DateTime(Year, Month, 1);
            currDate = currDate.AddMonths(-1);
            Year = currDate.Year;
            Month = currDate.Month;
            LoadMonth();
        }
        void OnNext()
        {
            DateTime currDate = new DateTime(Year, Month, 1);
            currDate = currDate.AddMonths(1);
            Year = currDate.Year;
            Month = currDate.Month;
            LoadMonth();
        }
        private void LoadMonth()
        {
            DateTime currDate = new DateTime(Year, Month, 1);
            Holidays = new ObservableCollection<Holiday>(_requestTimeOffRepository.HolidayQuery(h => h.Date.Year == Year && h.Date.Month == Month));
            MonthName = currDate.ToString("MMMM");
            bool weekDayStarted = false;
            for (int i = 0; i <= 36; i++)
            {
                if (weekDayStarted == false)
                {
                    if (currDate.DayOfWeek == GetDayOfWeek(i))
                    {
                        weekDayStarted = true;
                    }
                    else
                    {
                        SetDate(i, null);
                        SetTimeOffs(i, Array.Empty<TimeOff>().ToList());
                        continue;
                    }
                }

                if (currDate.Month != Month)
                {
                    SetDate(i, null);
                    SetTimeOffs(i, Array.Empty<TimeOff>().ToList());
                    continue;
                }

                SetDate(i, currDate.Day);
                SetTimeOffs(i, _requestTimeOffRepository.TimeOffQuery(t => t.Date == currDate && t.Username == _session.User.Username));

                currDate = currDate.AddDays(1);
            }
        }
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
    }
}
