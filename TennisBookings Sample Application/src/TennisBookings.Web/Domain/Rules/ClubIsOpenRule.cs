using System.Threading.Tasks;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    public class ClubIsOpenRule : ISingletonCourtBookingRule
    {
        private readonly IClubConfiguration _clubConfiguration;

        public ClubIsOpenRule(IClubConfiguration clubConfiguration)
        {
            _clubConfiguration = clubConfiguration;
        }

        public Task<bool> CompliesWithRuleAsync(CourtBooking booking)
        {
            var startHourPasses = booking.StartDateTime.Hour >= _clubConfiguration.OpenHour;
            var endHourPasses = booking.EndDateTime.Hour <= _clubConfiguration.CloseHour;

            return Task.FromResult(startHourPasses && endHourPasses);
        }

        public string ErrorMessage => "Can't make a booking when the club is closed";
    }
}
