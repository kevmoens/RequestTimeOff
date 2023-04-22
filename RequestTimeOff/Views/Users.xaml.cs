using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl, IPage
    {
        public Users(UsersViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Users()
        {
            InitializeComponent();
        }
    }
}
