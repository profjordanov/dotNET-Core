using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TennisBookings.Web.Services.Notifications
{
    public class SmsNotificationService : INotificationService
    {
        private readonly ILogger<SmsNotificationService> _logger;

        public SmsNotificationService(ILogger<SmsNotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string message, string userId)
        {
            // imagine sending logic via an external service

            _logger.LogInformation($"Sending SMS notification to user '{userId}'.");

            return Task.CompletedTask;
        }
    }
}