using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Domain;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class MembershipServiceCollectionExtensions
    {
        public static IServiceCollection AddMembershipServices(this IServiceCollection services)
        {
            services.TryAddTransient<IMembershipAdvertBuilder, MembershipAdvertBuilder>();
            services.TryAddScoped<IMembershipAdvert>(sp =>
            {
                var builder = sp.GetService<IMembershipAdvertBuilder>();

                builder.WithDiscount(10m);

                return builder.Build();
            }); // implementation factory for complex service construction

            return services;
        }
    }
}
