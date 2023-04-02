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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class HomeAdminViewModel : INotifyPropertyChanged
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
        private readonly Session _session;
        private readonly PubSubToken _viewNavigationToken;
        public HomeAdminViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            LoadedCommand = new DelegateCommand(OnLoaded);
            PendingRequestsCommand = new DelegateCommand(OnPendingRequests);
            CalendarCommand = new DelegateCommand(OnCalendar);
            UsersCommand = new DelegateCommand(OnUsers);
            DepartmentsCommand = new DelegateCommand(OnDepartments);
            HolidaysCommand = new DelegateCommand(OnHolidays);
            SignoutCommand = new DelegateCommand(OnSignout);
            _viewNavigationToken = ViewNavigationPubSub.Instance.Subscribe(OnViewNavigation);
        }


        public ICommand LoadedCommand { get; set; }
        public ICommand PendingRequestsCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand UsersCommand { get; set; }
        public ICommand DepartmentsCommand { get; set; }
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

        private FrameworkElement  _DisplayContent;

        public FrameworkElement DisplayContent
        {
            get { return _DisplayContent; }
            set { _DisplayContent = value; OnPropertyChanged(); }
        }

        public void OnLoaded()
        {
           PendingRequests =  _requestTimeOffRepository.TimeOffQuery(t => t.Approved == false && t.Declined == false).Count;

            ViewNavigation viewNav = new ViewNavigation();
            var view = _serviceProvider.GetService<HomePageAdmin>();
            viewNav.Content = view;
            ViewNavigationPubSub.Instance.Publish(viewNav);
        }

        private void OnPendingRequests()
        {
            DisplayContent = _serviceProvider.GetService<PendingRequests>();
            HamburgerOpen = false;
        }
        private void OnCalendar()
        {
            DisplayContent = _serviceProvider.GetService<Views.Calendar>();
            HamburgerOpen = false;
        }
        public void OnUsers()
        {
            DisplayContent = _serviceProvider.GetService<Users>();
            HamburgerOpen = false;
        }
        private void OnDepartments()
        {
            DisplayContent = _serviceProvider.GetService<Departments>();
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
            if (_session.User.IsAdmin == false)
            {
                return;
            }
            var viewModel = view.Content.DataContext as INavigationAware;
            DisplayContent = view.Content;
            if (viewModel != null)
            {
                viewModel.OnNavigatedTo(view.Parameters);
            }
            HamburgerOpen = false;
        }
    }
    
}
