using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RequestTimeOff.MVVM.Events
{
    public class ViewNavigationPubSub : PubSub<ViewNavigation>
    {
        public static ViewNavigationPubSub Instance { get; } = new ViewNavigationPubSub();
    }
    public class ViewNavigation 
    {
        public FrameworkElement Content { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
