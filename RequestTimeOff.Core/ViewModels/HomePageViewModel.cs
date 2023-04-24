using Microsoft.Extensions.Logging;
using Neleus.DependencyInjection.Extensions;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.HomePages;
using RequestTimeOff.MVVM;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly INavigationService _navigationService;
        private readonly Session _session;
        private readonly ISystemDateTime _systemDateTime;
        private IUserYearInfo _userYearInfo;
        private readonly ILogger<HomePageViewModel> _logger;
        private IServiceByNameFactory<IPage> _pageFactory;
        public HomePageViewModel(INavigationService navigationService,
                                 Session session,
                                 ISystemDateTime systemDateTime,
                                 IUserYearInfo userYearInfo,
                                 ILogger<HomePageViewModel> logger,
                                 IServiceByNameFactory<IPage> pageFactory
            )
        {
            _navigationService = navigationService;
            _session = session;
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
        public Session Session { get { return _session; } }

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

        public IUserYearInfo UserYearInfo
        {
            get { return _userYearInfo; }
            set { _userYearInfo = value; OnPropertyChanged(); }
        }

        private void OnLoaded()
        {
            CurrYear = _systemDateTime.Now().Year;
            PrevYear = CurrYear - 1;
            NextYear = CurrYear + 1;
            UserCalendar = _pageFactory.GetByName("UserCalendar");
            OnChangeYear(CurrYear);
        }
        private async void OnChangeYear(int year)
        {
            try
            {
                SelectedYear = year;
                await UserYearInfo.ChangeYear(year);
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
    }
}
