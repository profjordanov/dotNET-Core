using System.Collections.Generic;

namespace TennisBookings.Web.Data
{
    public class Court
    {
        public int Id { get; set; }

        public CourtType Type { get; set; }

        public string Name { get; set; }

        public ICollection<CourtBooking> Bookings { get; set; }
    }
}
