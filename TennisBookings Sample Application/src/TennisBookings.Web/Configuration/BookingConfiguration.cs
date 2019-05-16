
namespace TennisBookings.Web.Configuration
{
    public class BookingConfiguration : IBookingConfiguration
    {
        public int MaxRegularBookingLengthInHours { get; set; }

        public int MaxPeakBookingLengthInHours { get; set; }
    }
}