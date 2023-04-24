using System;
using System.Collections.Generic;

namespace RequestTimeOff.MVVM
{
    public interface INavigationService
    {
        void NavigateTo(string pageKey);
        void NavigateTo(string pageKey, Dictionary<string, object> parameters);
        void ViewNavigateTo(string pageKey);
        void ViewNavigateTo(string pageKey, Dictionary<string, object> parameters);
        void GoBack();
    }
}
