using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Services
{
    public class CourtMaintenanceService : ICourtMaintenanceService
    {
        private readonly TennisBookingDbContext _dbContext;

        public CourtMaintenanceService(TennisBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CourtMaintenanceSchedule>> GetUpcomingMaintenance()
        {
            return await _dbContext.CourtMaintenance
                .AsNoTracking()
                .Include(x => x.Court)
                .Where(x => x.EndDate > DateTime.Now).ToListAsync();
        }
    }
}
