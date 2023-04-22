using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Page, IPage
    {
        public HomeAdmin(HomeAdminViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public HomeAdmin()
        {
            InitializeComponent();

        }
    }
}
