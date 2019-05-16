using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TennisBookings.Web.Services.Notifications
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendAsync(string message, string userId)
        {
            // imagine sending logic via an external service

            _logger.LogInformation($"Sending email notification to user '{userId}'.");
            
            return Task.CompletedTask;
        }
    }
}
