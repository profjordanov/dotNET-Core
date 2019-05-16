using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    public interface IBookingRuleProcessor
    {
        Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(CourtBooking courtBooking);
    }
}