using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisBookings.Web.Areas.Admin.ViewModels
{
    public class BookingListerViewModel
    {
        public bool CancelSuccessful { get; set; }

        public IEnumerable<IGrouping<DateTime, CourtBookingViewModel>> CourtBookings { get; set; }

        public DateTime EndOfWeek { get; set; }
    }
}
