using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public interface IUnavailabilityProvider
    {
        Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date);

        Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId);
    }

}
