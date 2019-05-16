using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisBookings.Web.Areas.Admin.ViewModels
{
    public class MaintenanceListerViewModel
    {
        public IEnumerable<IGrouping<DateTime, CourtMaintenanceViewModel>> ScheduledMaintenanceWork { get; set; }
    }
}
