using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for PendingRequests.xaml
    /// </summary>
    public partial class PendingRequests : UserControl, IPage
    {
        public PendingRequests(PendingRequestsViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public PendingRequests()
        {
            InitializeComponent();
        }
    }
}
