using System.Threading.Tasks;

namespace TennisBookings.Web.Services.Notifications
{
    public interface INotificationService
    {
        Task SendAsync(string message, string userId);
    }
}
