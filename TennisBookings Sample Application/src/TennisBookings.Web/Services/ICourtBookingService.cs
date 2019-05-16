using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Services
{
    public interface ICourtBookingService
    {
        Task CreateCourtBooking(CourtBooking courtBooking);

        Task<bool> CancelBooking(int bookingId);

        Task<CourtBooking> LoadBooking(int bookingId);

        Task<IEnumerable<CourtBooking>> BookingsUntilDateAsync(DateTime date);

        Task<IEnumerable<CourtBooking>> BookingsForDayAsync(DateTime date);

        Task<IEnumerable<CourtBooking>> CourtBookingsForDayAsync(DateTime date, int courtId);

        Task<IEnumerable<CourtBooking>> MemberBookingsForDayAsync(DateTime date, Member member);

        Task<int> GetBookedHoursForMemberAsync(int memberId, DateTime date);

        Task<IEnumerable<CourtBooking>> GetFutureBookingsForMemberAsync(Member member);

        Task<int> GetBookedHoursForMemberAsync(Member member, DateTime date);
    }
}
