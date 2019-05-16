using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Auditing;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class AuditingServiceCollectionExtensions
    {
        public static IServiceCollection AddAuditing(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(IAuditor<>), typeof(Auditor<>)); // open generic registration

            return services;
        }
    }
}
