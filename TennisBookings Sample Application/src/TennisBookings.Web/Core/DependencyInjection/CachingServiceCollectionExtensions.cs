using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Core.Caching;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class CachingServiceCollectionExtensions
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.TryAddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>)); // open generic registration

            services.TryAddSingleton<IDistributedCacheFactory, DistributedCacheFactory>();

            return services;
        }
    }
}
