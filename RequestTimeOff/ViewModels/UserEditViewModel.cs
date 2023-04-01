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
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class UserEditViewModel : INotifyPropertyChanged, INavigationAware
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly IServiceProvider _serviceProvider;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        public UserEditViewModel(IServiceProvider serviceProvider, IRequestTimeOffRepository requestTimeOffRepository)
        {
            _serviceProvider = serviceProvider;
            _requestTimeOffRepository = requestTimeOffRepository;
            SaveCommand = new DelegateCommand(OnSave);
            BackCommand = new DelegateCommand(OnBack);
        }
        public ICommand SaveCommand { get; set; }
        public ICommand BackCommand { get; set; }
        private User _User;

        public User User
        {
            get { return _User; }
            set { _User = value; OnPropertyChanged(); }
        }
        private bool _isNew;
        public bool IsNew { get { return _isNew; } set { _isNew = value; OnPropertyChanged(); } }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        private string _passwordVerify;

        public string PasswordVerify
        {
            get { return _passwordVerify; }
            set { _passwordVerify = value; OnPropertyChanged(); }
        }

        public void OnNavigatedTo(Dictionary<string, object> parameters)
        {
            if (parameters?.TryGetValue("User", out object user) == true)
            {
                User = user as User;
                Password = User.Password;
                PasswordVerify = User.Password;
                IsNew = false;
                return;
            }
            IsNew = true;
            Password = string.Empty;
            PasswordVerify = string.Empty;
            User = new User();
        }
        private void OnSave()
        {
            //TODO Validation 

            if (_isNew)
            {
                User.Password = Password;
                _requestTimeOffRepository.AddUser(User);
                OnBack();
                return;
            }
            _requestTimeOffRepository.UpdateUser(User);
            OnBack();
        }
        private void OnBack()
        {
            ViewNavigation viewNav = new ViewNavigation();
            var view = _serviceProvider.GetService<Users>();
            viewNav.Content = view;
            ViewNavigationPubSub.Instance.Publish(viewNav);
        }
    }

}
