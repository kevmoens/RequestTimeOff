using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl, IPage
    {
        public HomePage(HomePageViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
