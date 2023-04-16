using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RequestTimeOff.Converters
{
    public class UserCalendarCellBorderColorConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = (int?)value[2];

            if (day != null && (value[1] is ObservableCollection<Holiday> holidays))
            {
                if (holidays.Any(h => h.Date.Day == day))
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
            }

            if (value[0] is not ObservableCollection<TimeOff> timeOffs) return new SolidColorBrush(Colors.Transparent);
            if (timeOffs.Count == 0) return new SolidColorBrush(Colors.Transparent);
            if (timeOffs.Count == 1)
            {
                if (timeOffs[0].Type == TimeOffType.Sick)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else if (timeOffs[0].Type == TimeOffType.Vacation)
                {
                    return new SolidColorBrush(Colors.LightGreen);
                }
            }
            else
            {
                return new SolidColorBrush(Colors.Purple);
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }
    }
}
