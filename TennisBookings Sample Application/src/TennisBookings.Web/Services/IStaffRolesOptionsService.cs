using System.Collections.Generic;

namespace TennisBookings.Web.Services
{
    public interface IStaffRolesOptionsService
    {
        List<string> Roles { get; }
    }
}
