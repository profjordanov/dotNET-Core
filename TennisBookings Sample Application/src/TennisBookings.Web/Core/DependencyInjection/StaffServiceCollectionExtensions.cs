using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class StaffServiceCollectionExtensions
    {
        public static IServiceCollection AddStaffServices(this IServiceCollection services)
        {
            services.AddSingleton<IStaffRolesOptionsService, StaffRolesOptionsService>();
            
            return services;
        }
    }
}
