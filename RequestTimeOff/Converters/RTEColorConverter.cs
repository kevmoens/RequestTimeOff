using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RequestTimeOff.Converters
{
    public class RTEColorConverter : IMultiValueConverter
    {
        public static SolidColorBrush DefaultColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BAE35B"));
        public static SolidColorBrush OtherBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2F24"));
        public static SolidColorBrush RequestedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9090FF"));
        public static SolidColorBrush ApprovedBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62FF62"));
        public static SolidColorBrush CompletedBrush = ApprovedBrush;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Length == 3 && values[0] != null)
            {
                //TimeOffType type = (TimeOffType)values[0];
                bool approved = (bool)values[1];
                bool declined = (bool)values[2];

                if (approved)
                {
                    return ApprovedBrush;
                }
                if (declined == false)
                {
                    return RequestedBrush;
                }

                return OtherBrush;

            }
            return OtherBrush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
