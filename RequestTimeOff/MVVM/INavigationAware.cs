using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.MVVM
{
    public interface INavigationAware
    {
        void OnNavigatedTo(Dictionary<string, object> parameters);
    }
}
