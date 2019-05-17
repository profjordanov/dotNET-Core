using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Services
{
    public interface ICourtMaintenanceService
    {
        Task<IEnumerable<CourtMaintenanceSchedule>> GetUpcomingMaintenance();
    }
}