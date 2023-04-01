using Microsoft.Extensions.DependencyInjection;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using RequestTimeOff.MVVM.Events;
using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IServiceProvider _serviceProvider;
        public UsersViewModel(IRequestTimeOffRepository requestTimeOffRepository, IServiceProvider serviceProvider)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _serviceProvider = serviceProvider;
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
        public void OnLoaded()
        {
            Users = new ObservableCollection<User>(_requestTimeOffRepository.UserQuery(u => true));
        }

        private void OnEdit(User user)
        {
            ViewNavigation viewNav = new ViewNavigation();
            var view = _serviceProvider.GetService<UserEdit>();
            viewNav.Content = view;
            viewNav.Parameters = new Dictionary<string, object> { { "User", user } };
            ViewNavigationPubSub.Instance.Publish(viewNav);
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
            ViewNavigation viewNav = new ViewNavigation();
            var view = _serviceProvider.GetService<UserEdit>();
            viewNav.Content = view;
            ViewNavigationPubSub.Instance.Publish(viewNav);
        }
    }

}
