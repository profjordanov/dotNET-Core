using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public interface IMembershipAdvertBuilder
    {
        MembershipAdvert Build();
        MembershipAdvertBuilder WithDiscount(decimal discount);
    }
}