using System;
using System.Threading.Tasks;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public interface IBookingService
    {
        Task<HourlyAvailabilityDictionary> GetBookingAvailabilityForDateAsync(DateTime date);

        Task<int> GetMaxBookingSlotForCourtAsync(DateTime startTime, DateTime endTime, int courtId);
    }
}