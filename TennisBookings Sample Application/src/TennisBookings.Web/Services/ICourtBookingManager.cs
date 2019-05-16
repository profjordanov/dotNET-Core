using System;
using System.Threading.Tasks;
using TennisBookings.Web.Data;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public interface ICourtBookingManager
    {
        Task<CourtBookingResult> MakeBookingAsync(DateTime startDateTime, DateTime endDateTime, int courtId, Member member);
    }
}
