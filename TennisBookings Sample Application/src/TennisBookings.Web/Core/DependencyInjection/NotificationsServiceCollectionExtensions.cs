using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Services.Notifications;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class NotificationsServiceCollectionExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.TryAddSingleton<EmailNotificationService>();
            services.TryAddSingleton<SmsNotificationService>();

            services.AddSingleton<INotificationService>(sp => 
                new CompositeNotificationService(
                    new INotificationService[]
                    {
                        sp.GetRequiredService<EmailNotificationService>(),
                        sp.GetRequiredService<SmsNotificationService>()
                    })); // composite pattern

            return services;
        }
    }
}
