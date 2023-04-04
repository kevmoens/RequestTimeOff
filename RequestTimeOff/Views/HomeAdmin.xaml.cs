using MaterialDesignThemes.Wpf;
using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
