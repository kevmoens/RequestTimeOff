﻿using RequestTimeOff.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RequestTimeOff.UserControls
{
    /// <summary>
    /// Interaction logic for UserCalendarCell.xaml
    /// </summary>
    public partial class UserCalendarCell : UserControl
    {
        public UserCalendarCell()
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
            DependencyProperty.Register("DisplayDate", typeof(int?), typeof(UserCalendarCell), new PropertyMetadata());


        public ObservableCollection<TimeOff> TimeOffs
        {
            get { return (ObservableCollection<TimeOff>)GetValue(TimeOffsProperty); }
            set { SetValue(TimeOffsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeOffs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeOffsProperty =
            DependencyProperty.Register("TimeOffs", typeof(ObservableCollection<TimeOff>), typeof(UserCalendarCell), new PropertyMetadata());



        public ObservableCollection<Holiday> Holidays
        {
            get { return (ObservableCollection<Holiday>)GetValue(HolidaysProperty); }
            set { SetValue(HolidaysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeOffs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HolidaysProperty =
            DependencyProperty.Register("Holidays", typeof(ObservableCollection<Holiday>), typeof(UserCalendarCell), new PropertyMetadata());

    }
}
