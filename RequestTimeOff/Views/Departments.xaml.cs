using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class Departments : UserControl, IPage
    {
        public Departments(DepartmentsViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Departments()
        {
            InitializeComponent();
        }
    }
}
