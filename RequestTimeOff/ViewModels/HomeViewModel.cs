using Microsoft.Extensions.DependencyInjection;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using RequestTimeOff.MVVM.Events;
using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly PubSubToken _viewNavigationToken;
        public HomeViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IRequestTimeOffRepository requestTimeOffRepository)
        {
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            _requestTimeOffRepository = requestTimeOffRepository;
            LoadedCommand = new DelegateCommand(OnLoaded);
            CalendarCommand = new DelegateCommand(OnCalendar);
            HolidaysCommand = new DelegateCommand(OnHolidays);
            SignoutCommand = new DelegateCommand(OnSignout);
            _viewNavigationToken = ViewNavigationPubSub.Instance.Subscribe(OnViewNavigation);
        }


        public ICommand LoadedCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand HolidaysCommand { get; set; }
        public ICommand SignoutCommand { get; set; }
        private int _PendingRequests;

        public int PendingRequests
        {
            get { return _PendingRequests; }
            set { _PendingRequests = value; OnPropertyChanged(); }
        }

        private bool _HamburgerOpen;
        public bool HamburgerOpen
        {
            get { return _HamburgerOpen; }
            set { _HamburgerOpen = value; OnPropertyChanged(); }
        }

        private FrameworkElement _DisplayContent;

        public FrameworkElement DisplayContent
        {
            get { return _DisplayContent; }
            set { _DisplayContent = value; OnPropertyChanged(); }
        }

        public void OnLoaded()
        {
            PendingRequests = _requestTimeOffRepository.TimeOffQuery(t => t.Approved == false && t.Declined == false).Count;
        }

        private void OnCalendar()
        {
            DisplayContent = _serviceProvider.GetService<Views.Calendar>();
            HamburgerOpen = false;
        }
        private void OnHolidays()
        {
            DisplayContent = _serviceProvider.GetService<Holidays>();
            HamburgerOpen = false;
        }

        public void OnSignout()
        {
            _navigationService.GoBack();
        }

        public void OnViewNavigation(ViewNavigation view)
        {
            var viewModel = view.Content.DataContext as INavigationAware;
            DisplayContent = view.Content;
            if (viewModel != null)
            {
                viewModel.OnNavigatedTo(view.Parameters);
            }
        }
    }
}
