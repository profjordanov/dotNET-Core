using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisBookings.Web.Areas.Admin.ViewModels;
using TennisBookings.Web.Core;
using TennisBookings.Web.Pages;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CourtsController : Controller
    {
        private readonly ICourtBookingService _courtBookingService;
        private readonly ICourtMaintenanceService _courtMaintenanceService;

        public CourtsController(ICourtBookingService courtBookingService, ICourtMaintenanceService courtMaintenanceService)
        {
            _courtBookingService = courtBookingService;
            _courtMaintenanceService = courtMaintenanceService;
        }
        
        [HttpGet]
        [Route("Bookings/Upcoming")]
        public async Task<ActionResult> WeeklyBookings()
        {
            var bookings = await _courtBookingService.BookingsUntilDateAsync(DateTime.UtcNow.GetEndOfWeek());

            var bookingsViewModel = bookings.Select(x => new CourtBookingViewModel
            {
                BookingId = x.Id,
                CourtName = x.Court.Name,
                StartDateTime = x.StartDateTime,
                EndDateTime = x.EndDateTime,
                MemberName = $"{x.Member.Forename} {x.Member.Surname}"
            }).GroupBy(x => x.StartDateTime.Date);

            var viewModel = new BookingListerViewModel {CourtBookings = bookingsViewModel, EndOfWeek = DateTime.UtcNow.GetEndOfWeek() };

            if (TempData.TryGetValue("BookingCancelled", out var successObject) is bool success)
            {
                viewModel.CancelSuccessful = success;
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("Booking/{bookingId:int}/Cancel")]
        public async Task<ActionResult> CancelBooking(int bookingId)
        {
            var courtBooking = await _courtBookingService.LoadBooking(bookingId);

            if (courtBooking == null)
                return NotFound();

            var viewModel = new CancelBookingConfirmationViewModel
            {
                BookingId = bookingId,
                CourtName = courtBooking.Court.Name,
                MemberName = $"{courtBooking.Member.Forename} {courtBooking.Member.Surname}",
                Date = courtBooking.StartDateTime.Date,
                StartTime = courtBooking.StartDateTime.Hour.To12HourClockString(),
                EndTime = courtBooking.EndDateTime.Hour.To12HourClockString()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("Booking/{bookingId:int}/Cancel")]
        public async Task<ActionResult> CancelBookingConfirmation(int bookingId, string submitButton)
        {
            if (submitButton == "Cancel")
            {
                return RedirectToAction("WeeklyBookings");
            }

            await _courtBookingService.CancelBooking(bookingId);

            TempData["BookingCancelled"] = true;

            return RedirectToAction("WeeklyBookings");
        }

        [Route("Maintenance/Upcoming")]
        public async Task<ActionResult> UpcomingMaintenance()
        {
            var maintenanceSchedules = await _courtMaintenanceService.GetUpcomingMaintenance();

            var maintenanceViewModels = maintenanceSchedules.Select(x => new CourtMaintenanceViewModel
            {
                CourtName = x.Court.Name,
                StartDateTime = x.StartDate,
                EndDateTime = x.EndDate,
                Title = x.WorkTitle,
                CourtIsClosed = x.CourtIsClosed
            }).GroupBy(x => x.StartDateTime.Date);

            var viewModel = new MaintenanceListerViewModel { ScheduledMaintenanceWork = maintenanceViewModels };

            return View(viewModel);
        }
    }
}
