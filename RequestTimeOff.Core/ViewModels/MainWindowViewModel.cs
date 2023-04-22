using RequestTimeOff.MVVM;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class MainWindowViewModel 
    {
        private readonly INavigationService _navigationService;
        public MainWindowViewModel(INavigationService navigationService)
        {
            LoadedCommand = new DelegateCommand(OnLoaded);
            _navigationService = navigationService;

        }
        public ICommand LoadedCommand { get; set; }

        private void OnLoaded()
        {
            _navigationService.NavigateTo("Login");
        }
    }
}
