using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RequestTimeOff.Models.HomePages
{
    public class RTEColorBrushes : Dictionary<string, SolidColorBrush>
    {
        public RTEColorBrushes()
        {
            Add(RTEColorNames.Default.ToString(), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BAE35B"))); 
            Add(RTEColorNames.Declined.ToString(), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2F24"))); 
            Add(RTEColorNames.Requested.ToString(), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9090FF"))); 
            Add(RTEColorNames.Approved.ToString(), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#62FF62"))); 
            Add(RTEColorNames.SickRequested.ToString(), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ede8a1"))); 
            Add(RTEColorNames.SickApproved.ToString(), new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5ec69")));
        }
    }
}
