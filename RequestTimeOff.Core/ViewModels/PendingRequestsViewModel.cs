using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class PendingRequestsViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        public PendingRequestsViewModel(IRequestTimeOffRepository requestTimeOffRepository)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            LoadedCommand = new DelegateCommand(OnLoaded);
        }

        public ICommand LoadedCommand { get; set; }

        private ObservableCollection<TimeOff> _requests;

        public ObservableCollection<TimeOff> Requests
        {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged(); }
        }

        private void OnLoaded()
        {
            var requests = _requestTimeOffRepository
                .TimeOffQuery(t => t.Approved == false && t.Declined == false)
                .OrderBy(t => t.Username)
                .OrderBy(t => t.Date)
                .ToList();
            Requests = new ObservableCollection<TimeOff>(requests);
            
        }

    }
}
