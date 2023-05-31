using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class UserEditViewModel : INotifyPropertyChanged, INavigationAware
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
        private readonly IMessageBox _messageBox;   
        [ExcludeFromCodeCoverage]
        public UserEditViewModel(INavigationService navigationService, IRequestTimeOffRepository requestTimeOffRepository, IMessageBox messageBox)
        {
            _navigationService = navigationService;
            _requestTimeOffRepository = requestTimeOffRepository;
            _messageBox = messageBox;   
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

        // Stryker disable all : Properties used for binding in the view 1
        private ObservableCollection<Department> _departments;
        public ObservableCollection<Department> Departments { get { return _departments; } set { _departments = value; OnPropertyChanged(); } }
        // Stryker restore all
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

        [ExcludeFromCodeCoverage]
        public void OnNavigatedTo(Dictionary<string, object> parameters)
        {
            Departments = new ObservableCollection<Department>(_requestTimeOffRepository.DepartmentQuery(d => true).OrderBy(d => d.Dept));
            if (parameters?.TryGetValue("User", out object objuser) == true)
            {
                User user = (User)objuser;
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
        public void OnNavigated()
        {

        }
        internal void OnSave()
        {
            if (Password != PasswordVerify)
            {
                _messageBox.Show("Passwords do not match");
                return;
            }

            if (IsNew)
            {
                User.Password = Password;
                _requestTimeOffRepository.AddUser(User);
                OnBack();
                return;
            }
            _requestTimeOffRepository.UpdateUser(User);
            OnBack();
        }

        [ExcludeFromCodeCoverage]
        private void OnBack()
        {
            _navigationService.ViewNavigateTo("Users");
        }
    }

}
