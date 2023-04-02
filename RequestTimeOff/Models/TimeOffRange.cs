using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.Models
{
    public enum TimeOffRange
    {
        None = 0,
        AM = 1,
        PM = 2,
        FullDay = 3
    }

    public static class TimeOffRangeExtensions
    {
        public static int Hours(this TimeOffRange range)
        {
            switch (range)
            {
                case TimeOffRange.AM:

                    return 4;
                case TimeOffRange.None:
                    return 0;
                case TimeOffRange.PM:
                    return 4;
                case TimeOffRange.FullDay:
                    return 8;
            }
            return 0;
        }
    }

}
