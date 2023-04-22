using RequestTimeOff.Models;
using RequestTimeOff.Models.HomePages;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RequestTimeOff.Converters
{
    public class RTEColorConverter : IMultiValueConverter
    {
        private RTEColorBrushes _colors = new();
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Length == 3 && values[0] != null)
            {
                TimeOffType type = (TimeOffType)values[0];
                bool approved = (bool)values[1];
                bool declined = (bool)values[2];
                if (type == TimeOffType.Vacation)
                {
                    if (approved)
                    {
                        return _colors[RTEColorNames.Approved.ToString()];
                    }
                    if (declined == false)
                    {
                        return _colors[RTEColorNames.Requested.ToString()];
                    }

                    return _colors[RTEColorNames.Declined.ToString()];
                }

                //Sick
                if (approved)
                {
                    return _colors[RTEColorNames.SickApproved.ToString()];
                }
                if (declined == false)
                {
                    return _colors[RTEColorNames.SickRequested.ToString()];
                }

                return _colors[RTEColorNames.Declined.ToString()];

            }
            return _colors[RTEColorNames.Default.ToString()];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return Array.Empty<object>();
        }
    }
}
