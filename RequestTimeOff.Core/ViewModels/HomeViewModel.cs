using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using RequestTimeOff.MVVM.Events;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;
        public HomeViewModel(INavigationService navigationService, IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _navigationService = navigationService;
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            LoadedCommand = new DelegateCommand(OnLoaded);
            HomeCommand = new DelegateCommand(OnHome);
            CalendarCommand = new DelegateCommand(OnCalendar);
            HolidaysCommand = new DelegateCommand(OnHolidays);
            SettingsCommand = new DelegateCommand(OnSettings);
            SignoutCommand = new DelegateCommand(OnSignout);
            ViewNavigationPubSub.Instance.Subscribe(OnViewNavigation);
        }


        public ICommand LoadedCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand HolidaysCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
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

        private object _DisplayContent;

        public object DisplayContent
        {
            get { return _DisplayContent; }
            set { _DisplayContent = value; OnPropertyChanged(); }
        }

        public void OnLoaded()
        {
            PendingRequests = _requestTimeOffRepository.TimeOffQuery(t => t.Approved == false && t.Declined == false).Count;

            _navigationService.ViewNavigateTo("HomePage");
        }
        private void OnHome()
        {
            _navigationService.ViewNavigateTo("HomePage");
        }
        private void OnCalendar()
        {
            _navigationService.ViewNavigateTo("Calendar");
            HamburgerOpen = false;
        }
        private void OnHolidays()
        {
            _navigationService.ViewNavigateTo("Holidays");
            HamburgerOpen = false;
        }
        private void OnSettings()
        {
            _navigationService.ViewNavigateTo("Settings");
            HamburgerOpen = false;
        }
        public void OnSignout()
        {
            _navigationService.GoBack();
        }

        public void OnViewNavigation(ViewNavigation view)
        {
            if (_session.User.IsAdmin)
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
    }
}
