using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RequestTimeOff.ViewModels
{
    public class HomePageAdminViewModel : INotifyPropertyChanged
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
        //public HomePageAdminViewModel(IServiceProvider serviceProvider, IRequestTimeOffRepository requestTimeOffRepository)
        //{
        //    _serviceProvider = serviceProvider;
        //    _requestTimeOffRepository = requestTimeOffRepository;

        //}
        private int _currYear;
        public int CurrYear
        {
            get { return _currYear; }
            set { _currYear = value; OnPropertyChanged(); }
        }
    }
}
