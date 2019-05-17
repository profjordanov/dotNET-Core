using System;

namespace TennisBookings.Web.Areas.Admin.ViewModels
{
    public class CourtBookingViewModel
    {
        public int BookingId { get; set; }

        public string MemberName { get; set; }

        public string CourtName { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
