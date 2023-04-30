using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IRequestTimeOffRepository _requestTimeOffRepository;

        [ExcludeFromCodeCoverage]
        public PendingRequestsViewModel(IRequestTimeOffRepository requestTimeOffRepository)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ApproveCommand = new DelegateCommand<TimeOff>(OnApprove);
            DeclineCommand = new DelegateCommand<TimeOff>(OnDecline);
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }

        private ObservableCollection<TimeOff> _requests;

        [ExcludeFromCodeCoverage]
        public ObservableCollection<TimeOff> Requests
        {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged(); }
        }

        [ExcludeFromCodeCoverage]
        private void OnLoaded()
        {
            var requests = _requestTimeOffRepository
                .TimeOffQuery(t => t.Approved == false && t.Declined == false)
                .OrderBy(t => t.Username)
                .OrderBy(t => t.Date)
                .ToList();
            Requests = new ObservableCollection<TimeOff>(requests);
            
        }
        public void OnApprove(TimeOff timeOff)
        {
            timeOff.Approved = true;
            _requestTimeOffRepository.UpdateTimeOff(timeOff);
            // Stryker disable once all
            OnLoaded();
        }
        public void OnDecline(TimeOff timeOff)
        {
            timeOff.Declined = true;
            _requestTimeOffRepository.UpdateTimeOff(timeOff);
            // Stryker disable once all
            OnLoaded();
        }
    }
}
