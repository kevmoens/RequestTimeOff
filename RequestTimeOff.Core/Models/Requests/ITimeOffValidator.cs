using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestTimeOff.Core.Models.Requests
{
    public interface ITimeOffValidator
    { 
        List<TimeOff> ExistingRequests { get; set; }
    }
}
