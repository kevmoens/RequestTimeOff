using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class UsersViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly INavigationService _navigationService;
        public UsersViewModel(IRequestTimeOffRepository requestTimeOffRepository, INavigationService navigationService)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _navigationService = navigationService;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ChangedCommand = new DelegateCommand<User>(OnChanged);
            AddCommand = new DelegateCommand(OnAdd);
            DeleteCommand = new DelegateCommand<User>(OnDelete);
            EditCommand = new DelegateCommand<User>(OnEdit);
        }


        public ICommand LoadedCommand { get; set; }
        public ICommand ChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users { get { return _users; } set { _users = value; OnPropertyChanged(); } }
        private ObservableCollection<Department> _departments;
        public ObservableCollection<Department> Departments { get { return _departments; } set { _departments = value; OnPropertyChanged(); } }
        public void OnLoaded()
        {
            Users = new ObservableCollection<User>(_requestTimeOffRepository.UserQuery(u => true).OrderBy(u => u.Username));
            Departments = new ObservableCollection<Department>(_requestTimeOffRepository.DepartmentQuery(d => true).OrderBy(d => d.Dept));
                
        }

        private void OnEdit(User user)
        {
            _navigationService.ViewNavigateTo("UserEdit", new Dictionary<string, object> { { "User", user } });
        }

        private void OnDelete(User user)
        {
            _requestTimeOffRepository.RemoveUser(user);
            Users.Remove(user);
        }

        private void OnChanged(User user)
        {
            _requestTimeOffRepository.UpdateUser(user);
        }

        private void OnAdd()
        {
            _navigationService.ViewNavigateTo("UserEdit");
        }
    }

}
