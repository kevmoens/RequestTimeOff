using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl, IPage
    {
        public Calendar(CalendarViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Calendar()
        {
            InitializeComponent();
        }
    }
}
