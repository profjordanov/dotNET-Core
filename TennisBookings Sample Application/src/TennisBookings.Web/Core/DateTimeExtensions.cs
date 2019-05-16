using System;

namespace TennisBookings.Web.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime GetEndOfWeek(this DateTime dateTime)
        {
            var now = DateTime.UtcNow;
            var offset = DayOfWeek.Monday - now.DayOfWeek;

            if (offset == 1) return DateTime.UtcNow.Date.AddDays(1).AddMilliseconds(-1);

            var weekStart = now.AddDays(offset);
            var endOfWeek = weekStart.AddDays(6);

            return endOfWeek.Date.AddDays(1).AddMilliseconds(-1);
        }
    }
}
