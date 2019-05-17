using System.Threading.Tasks;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public interface IWeatherForecaster
    {
        Task<CurrentWeatherResult> GetCurrentWeatherAsync();
    }
}