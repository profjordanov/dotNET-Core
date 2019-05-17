using System;

namespace TennisBookings.Web.Services
{
    public interface IHomePageGreetingService
    {
        [Obsolete("Prefer the GetRandomGreeting method defined in IGreetingService")]
        string GetRandomHomePageGreeting();
    }    
}
