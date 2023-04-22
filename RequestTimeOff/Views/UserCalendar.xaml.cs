using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for UserCalendar.xaml
    /// </summary>
    public partial class UserCalendar : UserControl
    {
        public UserCalendar(UserCalendarViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public UserCalendar()
        {
            InitializeComponent();
        }
    }
}
