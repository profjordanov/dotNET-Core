using System.Threading.Tasks;
using TennisBookings.Web.Data;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Domain.Rules
{
    public class MemberCourtBookingsMaxHoursPerDayRule : IScopedCourtBookingRule
    {
        private readonly ICourtBookingService _courtBookingService;

        public MemberCourtBookingsMaxHoursPerDayRule(ICourtBookingService courtBookingService)
        {
            _courtBookingService = courtBookingService;
        }

        public async Task<bool> CompliesWithRuleAsync(CourtBooking booking)
        {
            var hoursBooked = await _courtBookingService.GetBookedHoursForMemberAsync(booking.Member, booking.StartDateTime.Date);

            var hoursRequested = (booking.EndDateTime - booking.StartDateTime).Hours;

            return hoursBooked + hoursRequested <= 5;
        }

        public string ErrorMessage => "Members may only book a total of 5 hours of court time per day.";
    }
}
