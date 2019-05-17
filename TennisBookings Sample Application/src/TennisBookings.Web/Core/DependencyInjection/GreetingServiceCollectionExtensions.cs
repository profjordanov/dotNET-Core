using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class GreetingServiceCollectionExtensions
    {
        public static IServiceCollection AddGreetings(this IServiceCollection services)
        {
            services.TryAddSingleton<GreetingService>();

            services.TryAddSingleton<IHomePageGreetingService>(sp =>
                sp.GetRequiredService<GreetingService>());

            services.TryAddSingleton<IGreetingService>(sp =>
                sp.GetRequiredService<GreetingService>());

            return services;
        }
    }
}
