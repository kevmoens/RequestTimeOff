using MaterialDesignThemes.Wpf;
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
    public class WindowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return PackIconKind.WindowMaximize;
            }
            if ((WindowState)value == WindowState.Normal)
            {
                return PackIconKind.WindowMaximize;
            }
            return PackIconKind.WindowRestore;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return WindowState.Normal;
        }
    }
}
