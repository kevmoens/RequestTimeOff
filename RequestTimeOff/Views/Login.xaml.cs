using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public INavigationAware ViewModel { get; set; }
    }
}
