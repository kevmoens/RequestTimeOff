using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page, IPage
    {
        public Home(HomeViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Home()
        {
            InitializeComponent();
        }
    }
}
