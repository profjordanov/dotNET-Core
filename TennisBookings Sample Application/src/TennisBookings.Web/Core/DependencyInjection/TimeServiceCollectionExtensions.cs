using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class TimeServiceCollectionExtensions
    {
        public static IServiceCollection AddTimeServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IUtcTimeService, TimeService>();

            return services;
        }
    }
}
