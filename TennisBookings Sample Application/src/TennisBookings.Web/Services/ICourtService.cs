using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Services
{
    public interface ICourtService
    {
        Task<IEnumerable<Court>> GetOutdoorCourts();
        Task<HashSet<int>> GetCourtIds();
    }
}