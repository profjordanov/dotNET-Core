using System;

namespace TennisBookings.Web.Data
{
    public class CourtBooking
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int MemberId { get; set; }

        public Member Member { get; set; }

        public Court Court { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
