using System.Collections.Generic;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain
{
    public class CourtBookingResult
    {
        private CourtBookingResult(CourtBooking booking, bool passedRules, IEnumerable<string> errors)
        {
            CourtBooking = booking;
            BookingSuccessful = passedRules;
            Errors = errors;
        }

        public CourtBooking CourtBooking { get; }

        public bool BookingSuccessful { get; }

        public IEnumerable<string> Errors { get; }

        public static CourtBookingResult Success(CourtBooking courtBooking) => new CourtBookingResult(courtBooking, true, null);

        public static CourtBookingResult Failure(IEnumerable<string> errors) => new CourtBookingResult(null, false, errors);
    }
}
