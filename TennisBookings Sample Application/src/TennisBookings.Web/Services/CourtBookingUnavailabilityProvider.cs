using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisBookings.Web.Data;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class CourtBookingUnavailabilityProvider : IUnavailabilityProvider
    {
        private readonly ICourtBookingService _courtBookingService;

        public CourtBookingUnavailabilityProvider(ICourtBookingService courtBookingService)
        {
            _courtBookingService = courtBookingService;
        }

        public async Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date)
        {
            var bookings = await _courtBookingService.BookingsForDayAsync(date);

            return BookingsToUnavailability(bookings);
        }

        public async Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId)
        {
            var bookings = await _courtBookingService.CourtBookingsForDayAsync(date, courtId);

            var availability = BookingsToUnavailability(bookings);

            return availability.Select(x => x.Hour);
        }

        private IEnumerable<HourlyUnavailability> BookingsToUnavailability(IEnumerable<CourtBooking> courtBookings)
        {
            var unavailability = new List<HourlyUnavailability>();

            for (var i = 0; i < 24; i++)
            {
                unavailability.Add(new HourlyUnavailability(i, new HashSet<int>()));
            }

            foreach (var booking in courtBookings)
            {
                for (var i = booking.StartDateTime.Hour; i < booking.EndDateTime.Hour; i++)
                {
                    unavailability.First(x => x.Hour == i).UnavailableCourtIds.Add(booking.CourtId);
                }
            }

            return unavailability.Where(x => x.UnavailableCourtIds.Any());
        }
    }
}