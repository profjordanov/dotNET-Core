using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Domain.Rules
{
    public class BookingRuleProcessor : IBookingRuleProcessor
    {
        private readonly IEnumerable<ICourtBookingRule> _rules;

        public BookingRuleProcessor(IEnumerable<ICourtBookingRule> rules)
        {
            _rules = rules;
        }

        public async Task<(bool, IEnumerable<string>)> PassesAllRulesAsync(CourtBooking courtBooking)
        {
            var passedRules = true;

            var errors = new List<string>();

            foreach (var rule in _rules)
            {
                if (!await rule.CompliesWithRuleAsync(courtBooking))
                {
                    errors.Add(rule.ErrorMessage);
                    passedRules = false;
                }
            }

            return (passedRules, errors);
        }
    }
}
