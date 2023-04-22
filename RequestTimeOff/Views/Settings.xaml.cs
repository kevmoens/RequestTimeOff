using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl, IPage
    {
        public Settings(SettingsViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Settings()
        {
            InitializeComponent();
        }
    }
}
