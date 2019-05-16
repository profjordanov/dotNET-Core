using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    public class MaxPeakTimeBookingLengthRule : ISingletonCourtBookingRule
    {
        private readonly IClubConfiguration _clubConfiguration;
        private readonly BookingConfiguration _bookingConfiguration;

        public MaxPeakTimeBookingLengthRule(IClubConfiguration clubConfiguration, IOptions<BookingConfiguration> options)
        {
            _clubConfiguration = clubConfiguration;
            _bookingConfiguration = options.Value;
        }

        public Task<bool> CompliesWithRuleAsync(CourtBooking booking)
        {
            if (booking.EndDateTime.Hour < _clubConfiguration.PeakStartHour)
                return Task.FromResult(true);

            var peakHours = 0;
            for (var hour = booking.StartDateTime.Hour; hour < booking.EndDateTime.Hour; hour++)
            {
                if (hour >= _clubConfiguration.PeakStartHour && hour <= _clubConfiguration.PeakEndHour)
                {
                    peakHours++;
                }
            }

            return Task.FromResult(peakHours <= _bookingConfiguration.MaxPeakBookingLengthInHours);
        }

        public string ErrorMessage => "The court booking is too long";
    }
}
