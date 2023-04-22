using System.Collections.Generic;

namespace RequestTimeOff.MVVM.Events
{
    public class ViewNavigationPubSub : PubSub<ViewNavigation>
    {
        public static ViewNavigationPubSub Instance { get; } = new ViewNavigationPubSub();
    }
    public class ViewNavigation 
    {
        public IPage Content { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
