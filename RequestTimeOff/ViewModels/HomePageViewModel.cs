﻿using Microsoft.Extensions.Logging;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.HomePages;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private readonly IServiceProvider _serviceProvider;
        private readonly Session _session;
        private ISystemDateTime _systemDateTime;
        private IUserYearInfo _userYearInfo;
        private ILogger<HomePageViewModel> _logger;
        public HomePageViewModel(IServiceProvider serviceProvider,
                                 Session session,
                                 ISystemDateTime systemDateTime,
                                 IUserYearInfo userYearInfo,
                                 ILogger<HomePageViewModel> logger)
        {
            _serviceProvider = serviceProvider;
            _session = session;
            _systemDateTime = systemDateTime;
            _userYearInfo = userYearInfo;
            _logger = logger;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ChangeYearCommand = new DelegateCommand<int>(OnChangeYear);
            NewRequestOffCommand = new DelegateCommand(OnNewRequest);
        }
        public ICommand LoadedCommand { get; set; }
        public ICommand ChangeYearCommand { get; set; }
        public ICommand NewRequestOffCommand { get; set; }
        public Session Session { get { return _session; } }

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
            OnChangeYear(CurrYear);
        }
        private async void OnChangeYear(int year)
        {
            try
            {
                await UserYearInfo.ChangeYear(year);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnChangeYear");
            }
        }
        private void OnNewRequest()
        {

        }
    }
}
