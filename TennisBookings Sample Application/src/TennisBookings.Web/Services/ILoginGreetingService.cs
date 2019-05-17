namespace TennisBookings.Web.Services
{
    public interface IGreetingService
    {
        string GetRandomGreeting();

        string GetRandomLoginGreeting(string name);
    }
}
