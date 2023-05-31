using System.Collections.Generic;

namespace RequestTimeOff.MVVM
{
    public interface INavigationAware
    {
        void OnNavigatedTo(Dictionary<string, object> parameters);
        void OnNavigated();
    }
}
