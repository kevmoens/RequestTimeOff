using Microsoft.Extensions.Logging;
using Neleus.DependencyInjection.Extensions;
using RequestTimeOff.Core.ViewModels;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.HomePages;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged, INavigationAware
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly INavigationService _navigationService;
        private Session _session;
        private readonly ISystemDateTime _systemDateTime;
        private IUserYearInfo _userYearInfo;
        private readonly ILogger<HomePageViewModel> _logger;
        private readonly IServiceByNameFactory<IPage> _pageFactory;
        public HomePageViewModel(INavigationService navigationService,
                                 Session session,
                                 ISystemDateTime systemDateTime,
                                 IUserYearInfo userYearInfo,
                                 ILogger<HomePageViewModel> logger,
                                 IServiceByNameFactory<IPage> pageFactory
            )
        {
            _navigationService = navigationService;
            Session = session;
            SessionUsername = Session.User.Username;
            _systemDateTime = systemDateTime;
            _userYearInfo = userYearInfo;
            _logger = logger;
            _pageFactory = pageFactory;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ChangeYearCommand = new DelegateCommand<int>(OnChangeYear);
            NewRequestOffCommand = new DelegateCommand(OnNewRequest);
        }
        public ICommand LoadedCommand { get; set; }
        public ICommand ChangeYearCommand { get; set; }
        public ICommand NewRequestOffCommand { get; set; }
        public Session Session { get { return _session; } private set { _session = value; OnPropertyChanged(); } }

        private IPage _userCalendar;
        public IPage UserCalendar {
            get { return _userCalendar; }
            set { _userCalendar = value; OnPropertyChanged(); }
        }

        private int _prevYear;

        public int PrevYear
        {
            get { return _prevYear; }
            set { _prevYear = value; OnPropertyChanged(); }
        }
        private int _currYear;
        public int CurrYear
        {
            get { return _currYear; }
            set { _currYear = value; OnPropertyChanged(); }
        }
        private int _nextYear;
        public int NextYear
        {
            get { return _nextYear; }
            set { _nextYear = value; OnPropertyChanged(); }
        }
        private int _selectedYear;

        public int SelectedYear
        {
            get { return _selectedYear; }
            set { _selectedYear = value; OnPropertyChanged(); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _sessionUsername;

        public string SessionUsername
        {
            get { return _sessionUsername; }
            set { _sessionUsername = value; OnPropertyChanged(); }
        }

        public IUserYearInfo UserYearInfo
        {
            get { return _userYearInfo; }
            set { _userYearInfo = value; OnPropertyChanged(); }
        }

        private async void OnLoaded()
        {
            CurrYear = _systemDateTime.Now().Year;
            PrevYear = CurrYear - 1;
            NextYear = CurrYear + 1;
            UserCalendar = _pageFactory.GetByName("UserCalendar");
            ((IUserCalendarViewModel)UserCalendar.DataContext).Username = Username;
            await ((IUserCalendarViewModel)UserCalendar.DataContext).LoadMonth();
            OnChangeYear(CurrYear);
        }
        private async void OnChangeYear(int year)
        {
            try
            {
                SelectedYear = year;
                UserYearInfo.Username = Username;
                await UserYearInfo.ChangeYear(year);
                ((IUserCalendarViewModel)UserCalendar.DataContext).Username = Username;
                await ((IUserCalendarViewModel)UserCalendar.DataContext).LoadMonth();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnChangeYear");
            }
        }
        private void OnNewRequest()
        {
            _navigationService.ViewNavigateTo("NewRequest");
        }

        public async void OnNavigatedTo(Dictionary<string, object> parameters)
        {
            SessionUsername = Session.User.Username;
            if (parameters?.ContainsKey("User") ?? false)
            {
                Username = ((User)parameters["User"]).Username;
            } else
            {
                Username = Session.User.Username;
            }

            if (UserCalendar?.DataContext != null)
            {
                ((IUserCalendarViewModel)UserCalendar.DataContext).Username = Username;
                await ((IUserCalendarViewModel)UserCalendar.DataContext).LoadMonth();
            }
        }
        public void OnNavigated()
        {

        }
    }
}
