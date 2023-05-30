using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RequestTimeOff.Converters
{
    public class IsCurrentUserVisibilityConverter : DependencyObject, IMultiValueConverter
    {


        public Visibility TrueVisibility
        {
            get { return (Visibility)GetValue(TrueVisibilityProperty); }
            set { SetValue(TrueVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrueVisibilityProperty =
            DependencyProperty.Register("TrueVisibility", typeof(Visibility), typeof(IsCurrentUserVisibilityConverter), new PropertyMetadata());

        public Visibility FalseVisibility
        {
            get { return (Visibility)GetValue(FalseVisibilityProperty); }
            set { SetValue(FalseVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FalseVisibilityProperty =
            DependencyProperty.Register("FalseVisibility", typeof(Visibility), typeof(IsCurrentUserVisibilityConverter), new PropertyMetadata());


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string CurrentUser = (string)values[0];
            string SessionUser = (string)values[1];
            if (CurrentUser == SessionUser) 
            {
                return TrueVisibility;
            }
            return FalseVisibility;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
