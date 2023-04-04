using RequestTimeOff.Extensions;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Sessions;
using RequestTimeOff.MVVM;
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
    public class LoginViewModel : INotifyPropertyChanged
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
        private readonly ISessionLoad _sessionLoad;
        public LoginViewModel(INavigationService navigationService, IRequestTimeOffRepository requestTimeOffRepository, Session session, ISessionLoad sessionLoad)
        {
            _navigationService = navigationService;
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            _sessionLoad = sessionLoad;
            LoginCommand = new DelegateCommand<PasswordBox>(OnLogin);
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }
        public void OnLogin(PasswordBox passwordBox)
        {
            var user = _requestTimeOffRepository.UserQuery(u => (u.Username ?? "").ToUpper() == (Username ?? "").ToUpper()).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Invalid Username");
                return;
            }
            if (ValidatePasswordFailed(user, passwordBox.Password))
            {
                MessageBox.Show("Invalid Password");
                return;
            }
            _session.User = user;
            _sessionLoad.Initialize().Await(new Action<Exception>((ex) => { MessageBox.Show(ex.Message); }));
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
