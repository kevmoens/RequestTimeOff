using Microsoft.Extensions.DependencyInjection;
using RequestTimeOff.MVVM;
using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public HomeAdminViewModel(INavigationService navigationService, IServiceProvider serviceProvider)
        {
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            UsersCommand = new DelegateCommand(OnUsers);
        }

        public ICommand UsersCommand { get; set; }
        private ContentControl  _DisplayContent;

        public ContentControl DisplayContent
        {
            get { return _DisplayContent; }
            set { _DisplayContent = value; OnPropertyChanged(); }
        }

        public void OnUsers()
        {
            DisplayContent = _serviceProvider.GetService<Users>();
        }

    }
}
