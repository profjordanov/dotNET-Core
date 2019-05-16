using System;
using System.Threading.Tasks;
using TennisBookings.Web.Data;
using TennisBookings.Web.Domain;
using TennisBookings.Web.Domain.Rules;
using TennisBookings.Web.Services.Notifications;

namespace TennisBookings.Web.Services
{
    public class CourtBookingManager : ICourtBookingManager
    {
        private readonly ICourtBookingService _bookingService;
        private readonly IBookingRuleProcessor _bookingRuleProcessor;
        private readonly INotificationService _notificationService;

        public CourtBookingManager(
            ICourtBookingService bookingService, 
            IBookingRuleProcessor bookingRuleProcessor, 
            INotificationService notificationService)
        {
            _bookingService = bookingService;
            _bookingRuleProcessor = bookingRuleProcessor;
            _notificationService = notificationService;
        }

        public async Task<CourtBookingResult> MakeBookingAsync(DateTime startDateTime, DateTime endDateTime, int courtId, Member member)
        {
            var courtBooking = new CourtBooking
            {
                CourtId = courtId,
                Member = member,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };

            var (passedRules, errors) = await _bookingRuleProcessor.PassesAllRulesAsync(courtBooking);

            if (!passedRules)
                return CourtBookingResult.Failure(errors);

            await _bookingService.CreateCourtBooking(courtBooking);

            await _notificationService.SendAsync("Thank you. Your booking is confirmed", member.User.Id);

            return CourtBookingResult.Success(courtBooking);
        }
    }
}
