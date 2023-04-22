using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page, IPage
    {
        public Login(LoginViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Login()
        {
            InitializeComponent();            
        }
    }
}
