using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisBookings.Web.Services.Notifications
{
    public class CompositeNotificationService : INotificationService
    {
        private readonly IEnumerable<INotificationService> _notificationServices;

        public CompositeNotificationService(IEnumerable<INotificationService> notificationServices)
        {
            _notificationServices = notificationServices;
        }

        public async Task SendAsync(string message, string userId)
        {
            foreach (var notificationService in _notificationServices)
            {
                await notificationService.SendAsync(message, userId);
            }
        }
    }
}