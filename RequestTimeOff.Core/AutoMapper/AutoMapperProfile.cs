using AutoMapper;
using RequestTimeOff.Core.Models.Requests;
using RequestTimeOff.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RequestTimeOff.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        [ExcludeFromCodeCoverage]
        public AutoMapperProfile()
        {
            CreateMap<TimeOff, PendingTimeOff>();
        }
    }
}
