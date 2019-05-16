using Microsoft.Extensions.Options;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public class MembershipAdvertBuilder : IMembershipAdvertBuilder
    {
        public MembershipAdvertBuilder(IOptions<MembershipConfiguration> memberShipOptions)
        {
            _fullPrice = memberShipOptions.Value.MonthlyMembershipFullPrice;
        }

        private readonly decimal _fullPrice;

        private decimal _discount;

        public MembershipAdvertBuilder WithDiscount(decimal discount)
        {
            if (_discount < _fullPrice / 2)
            {
                _discount = _fullPrice / 2;
            }

            _discount = discount;

            return this;
        }

        public MembershipAdvert Build()
        {
            var discountedPrice = _fullPrice - _discount;
            return new MembershipAdvert(discountedPrice, _discount);
        }
    }
}