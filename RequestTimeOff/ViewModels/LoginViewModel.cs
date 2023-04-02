using RequestTimeOff.Models;
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
        public LoginViewModel(INavigationService navigationService, IRequestTimeOffRepository requestTimeOffRepository)
        {
            _navigationService = navigationService;
            _requestTimeOffRepository = requestTimeOffRepository;
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
            if (ValidatePasswordFailed())
            {
                MessageBox.Show("Invalid Password");
                return;
            }
            if (user.IsAdmin)
            {
                _navigationService.NavigateTo("HomeAdmin");
                return;
            }
            _navigationService.NavigateTo("Home");

            bool ValidatePasswordFailed()
            {
                if (user.Password == passwordBox.Password)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(user.Password) && string.IsNullOrEmpty(passwordBox.Password))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
