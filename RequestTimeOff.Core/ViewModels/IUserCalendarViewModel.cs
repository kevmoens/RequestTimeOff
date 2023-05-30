using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Core.ViewModels
{
    public interface IUserCalendarViewModel
    {
        string Username { get; set; }
        Task LoadMonth();
    }
}
