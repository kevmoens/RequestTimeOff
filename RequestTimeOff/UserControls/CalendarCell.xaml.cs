using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace RequestTimeOff.UserControls
{
    /// <summary>
    /// Interaction logic for CalendarCell.xaml
    /// </summary>
    public partial class CalendarCell : UserControl
    {
        public CalendarCell()
        {
            InitializeComponent();
        }



        public int? DisplayDate
        {
            get { return (int?)GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayDateProperty =
            DependencyProperty.Register("DisplayDate", typeof(int?), typeof(CalendarCell), new PropertyMetadata());



        public ObservableCollection<TimeOff> TimeOffs
        {
            get { return (ObservableCollection<TimeOff>)GetValue(TimeOffsProperty); }
            set { SetValue(TimeOffsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeOffs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeOffsProperty =
            DependencyProperty.Register("TimeOffs", typeof(ObservableCollection<TimeOff>), typeof(CalendarCell), new PropertyMetadata());




    }
}
