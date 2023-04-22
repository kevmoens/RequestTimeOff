using System;
using System.Globalization;
using System.Windows.Data;

namespace RequestTimeOff.Converters
{
    public class HomePageCircularGaugeScaleSweepAngleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var Hrs = (int)values[0];
            var Remain = (int)values[1];

            var result = ((double)Hrs - (double)Remain) / (double)Hrs * 360;
            
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }
    }
}
