using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RequestTimeOff.Models.Requests
{
    public interface IValidateAdd
    {
        ObservableCollection<TimeOff> ExistingRequests { get; set; }
        List<DateTimeOffset> NewDates { get; set; }
        DateTimeOffset SelectedDate { get; set; }

        string ValidateDates();
        string ValidateInput();
    }
}