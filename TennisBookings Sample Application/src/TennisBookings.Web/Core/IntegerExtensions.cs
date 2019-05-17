namespace TennisBookings.Web.Core
{
    public static class IntegerExtensions
    {
        public static string To12HourClockString(this int hour)
        {
            var suffix = hour < 12 ? "am" : "pm";

            var finalHour = hour;

            if (hour == 0)
            {
                finalHour = 12;
            }
            else if (hour > 12)
            {
                finalHour = hour - 12;
            }

            return $"{finalHour} {suffix}";
        }
    }
}