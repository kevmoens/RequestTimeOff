using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System.Windows.Controls;

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for Holidays.xaml
    /// </summary>
    public partial class Holidays : UserControl, IPage
    {
        public Holidays(HolidaysViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Holidays()
        {
            InitializeComponent();
        }
    }
}
