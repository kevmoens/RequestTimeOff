using RequestTimeOff.Extensions;
using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.Models.Sessions;
using RequestTimeOff.MVVM;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly INavigationService _navigationService;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;
        private readonly ISessionLoad _sessionLoad;
        private readonly IMessageBox _messageBox;
        [ExcludeFromCodeCoverage]
        public LoginViewModel(INavigationService navigationService, IRequestTimeOffRepository requestTimeOffRepository, Session session, ISessionLoad sessionLoad, IMessageBox messageBox)
        {
            _navigationService = navigationService;
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            _sessionLoad = sessionLoad;
            _messageBox = messageBox;
            LoginCommand = new DelegateCommand(OnLogin);
        }

        // Stryker disable all : Properties used for binding in the view 1
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }


        public ICommand LoginCommand { get; set; }
        // Stryker restore all

        public void OnLogin()
        {
            // Stryker disable once all
            var user = _requestTimeOffRepository.UserQuery(u => (u.Username ?? "").ToUpper() == (Username ?? "").ToUpper()).FirstOrDefault();
            if (user == null)
            {
                _messageBox.Show("Invalid Username");
                return;
            }
            if (ValidatePasswordFailed(user,  Password))
            {
                _messageBox.Show("Invalid Password");
                return;
            }
            _session.User = user;
            // Stryker disable once all
            _sessionLoad.Initialize().Await(new Action<Exception>((ex) => { _messageBox.Show(ex.Message); }));
            if (user.IsAdmin)
            {
                _navigationService.NavigateTo("HomeAdmin");
                return;
            }
            _navigationService.NavigateTo("Home");

        }
        bool ValidatePasswordFailed(User user, string password)
        {
            if (user.Password == password)
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.Password) && string.IsNullOrEmpty(password))
            {
                return false;
            }
            return true;
        }
    }
}
