using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RequestTimeOff.ViewModels
{
    public class HomePageAdminViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Stryker disable all : Properties used for binding in the view 1

        private int _currYear;
        public int CurrYear
        {
            get { return _currYear; }
            set { _currYear = value; OnPropertyChanged(); }
        }
        // Stryker restore all
    }
}
