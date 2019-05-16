using System;
using System.Collections.Generic;

namespace TennisBookings.Web.Domain
{
    public class HourlyUnavailability
    {
        public HourlyUnavailability(int hour, HashSet<int> unavailableCourtIds)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentException("The hour must be between 0 and 23", nameof(hour));

            Hour = hour;
            UnavailableCourtIds = unavailableCourtIds ?? throw new ArgumentNullException(nameof(unavailableCourtIds));
        }

        public int Hour { get; }

        public HashSet<int> UnavailableCourtIds { get; }
    }
}
