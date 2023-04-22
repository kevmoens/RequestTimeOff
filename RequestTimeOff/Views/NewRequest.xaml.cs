using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for NewRequest.xaml
    /// </summary>
    public partial class NewRequest : UserControl, IPage
    {
        public NewRequest(NewRequestViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public NewRequest()
        {
            InitializeComponent();
        }
    }
}
