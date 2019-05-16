using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ExternalServicesConfig>(config.GetSection("ExternalServices"));
            services.Configure<ClubConfiguration>(config.GetSection("ClubSettings"));
            services.Configure<BookingConfiguration>(config.GetSection("CourtBookings"));
            services.Configure<FeaturesConfiguration>(config.GetSection("Features"));
            services.Configure<MembershipConfiguration>(config.GetSection("Membership"));

            services.TryAddSingleton<IBookingConfiguration>(sp =>
                sp.GetRequiredService<IOptions<BookingConfiguration>>().Value); // forwarding via implementation factory

            services.TryAddSingleton<IClubConfiguration>(sp =>
                sp.GetRequiredService<IOptions<ClubConfiguration>>().Value); // forwarding via implementation factory

            return services;
        }
    }
}
