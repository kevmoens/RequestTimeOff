using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //private readonly IServiceProvider _serviceProvider;
        //private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        //public SettingsViewModel(IServiceProvider serviceProvider, IRequestTimeOffRepository requestTimeOffRepository)
        //{
        //    _serviceProvider = serviceProvider;
        //    _requestTimeOffRepository = requestTimeOffRepository;

        //}

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

    }
}
