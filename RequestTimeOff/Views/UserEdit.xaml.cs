using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : UserControl, IPage
    {
        public UserEdit(UserEditViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public UserEdit()
        {
            InitializeComponent();
        }
    }
}
