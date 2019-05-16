using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TennisBookings.Web.Data;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Pages
{
    public class BookingsModel : PageModel
    {
        private readonly UserManager<TennisBookingsUser> _userManager;
        private readonly ICourtBookingService _courtBookingService;
        private readonly IGreetingService _loginGreetingService;

        public BookingsModel(UserManager<TennisBookingsUser> userManager, ICourtBookingService courtBookingService, IGreetingService loginGreetingService)
        {
            _userManager = userManager;
            _courtBookingService = courtBookingService;
            _loginGreetingService = loginGreetingService;
        }

        public IEnumerable<IGrouping<DateTime, CourtBooking>> CourtBookings { get; set; }

        public string Greeting { get; private set; }

        [TempData]
        public bool BookingSuccess { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.Users
                .Include(u => u.Member)
                .FirstOrDefaultAsync(u => u.NormalizedEmail == User.Identity.Name);

            if (user == null)
                return new ChallengeResult();

            Greeting = _loginGreetingService.GetRandomLoginGreeting($"{user.Member.Forename} {user.Member.Surname}");

            var bookings = await _courtBookingService.GetFutureBookingsForMemberAsync(user.Member);

            CourtBookings = bookings.GroupBy(x => x.StartDateTime.Date);

            return Page();
        }
    }
}