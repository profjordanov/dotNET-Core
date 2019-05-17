using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class UpcomingHoursUnavailabilityProvider : IUnavailabilityProvider
    {
        private readonly ICourtService _courtService;

        public UpcomingHoursUnavailabilityProvider(ICourtService courtService)
        {
            _courtService = courtService;
        }

        public async Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date)
        {
            var courts = await _courtService.GetCourtIds();

            if (date.Date != DateTime.UtcNow.Date)
                return Array.Empty<HourlyUnavailability>();

            var firstHourAvailable = GetFirstHourAvailable();

            var unavailableHours = new List<HourlyUnavailability>();

            for (var i = 0; i < firstHourAvailable; i++)
            {
                unavailableHours.Add(new HourlyUnavailability(i, courts));
            }

            return unavailableHours;
        }

        public Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId)
        {
            if (date.Date != DateTime.UtcNow.Date)
                return Task.FromResult(Enumerable.Empty<int>());

            var firstHourAvailable = GetFirstHourAvailable();

            var unavailableHours = new List<int>();

            for (var i = 0; i < firstHourAvailable; i++)
            {
                unavailableHours.Add(i);
            }

            return Task.FromResult(unavailableHours.AsEnumerable());
        }

        private int GetFirstHourAvailable()
        {
            var firstHourAvailable = DateTime.UtcNow.Hour + 1;

            return DateTime.UtcNow.Minute < 30 ? firstHourAvailable : firstHourAvailable + 1;
        }
    }
}
