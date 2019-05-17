using System;

namespace TennisBookings.Web.Data
{
    public class CourtMaintenanceSchedule
    {
        public int Id { get; set; }

        public string WorkTitle { get; set; }
        
        public bool CourtIsClosed { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CourtId { get; set; }

        public Court Court { get; set; }
    }
}
