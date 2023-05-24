using RequestTimeOff.Core.MVVM.Events;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using RequestTimeOff.MVVM.Events;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;
        public HomeAdminViewModel(INavigationService navigationService, IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _navigationService = navigationService;
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            LoadedCommand = new DelegateCommand(OnLoaded);
            PendingRequestsCommand = new DelegateCommand(OnPendingRequests);
            CalendarCommand = new DelegateCommand(OnCalendar);
            UsersCommand = new DelegateCommand(OnUsers);
            DepartmentsCommand = new DelegateCommand(OnDepartments);
            HolidaysCommand = new DelegateCommand(OnHolidays);
            SignoutCommand = new DelegateCommand(OnSignout);
            ViewNavigationPubSub.Instance.Subscribe(OnViewNavigation);
            PendingRequestUpdatePubSub.Instance.Subscribe(OnPendingRequestUpdate);
        }


        public ICommand LoadedCommand { get; set; }
        public ICommand PendingRequestsCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand UsersCommand { get; set; }
        public ICommand DepartmentsCommand { get; set; }
        public ICommand HolidaysCommand { get; set; }
        public ICommand SignoutCommand { get; set; }
        
        public Session Session { get { return _session; } }

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

        private object  _DisplayContent;

        public object DisplayContent
        {
            get { return _DisplayContent; }
            set { _DisplayContent = value; OnPropertyChanged(); }
        }

        public void OnLoaded()
        {
           PendingRequests =  _requestTimeOffRepository.TimeOffQuery(t => t.Approved == false && t.Declined == false).Count;

            _navigationService.ViewNavigateTo("Calendar");
        }

        private void OnPendingRequests()
        {
            _navigationService.ViewNavigateTo("PendingRequests");
            HamburgerOpen = false;
        }
        private void OnCalendar()
        {
            _navigationService.ViewNavigateTo("Calendar");
            HamburgerOpen = false;
        }
        public void OnUsers()
        {
            _navigationService.ViewNavigateTo("Users");
            HamburgerOpen = false;
        }
        private void OnDepartments()
        {
            _navigationService.ViewNavigateTo("Departments");
            HamburgerOpen = false;
        }
        private void OnHolidays()
        {
            _navigationService.ViewNavigateTo("Holidays");
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
            DisplayContent = view.Content;
            if (view.Content.DataContext is INavigationAware viewModel)
            {
                viewModel.OnNavigatedTo(view.Parameters);
            }
            HamburgerOpen = false;
        }
        public void OnPendingRequestUpdate(PendingRequestUpdate update)
        {
            PendingRequests = _requestTimeOffRepository.TimeOffQuery(t => t.Approved == false && t.Declined == false).Count;
        }
    }
    
}
