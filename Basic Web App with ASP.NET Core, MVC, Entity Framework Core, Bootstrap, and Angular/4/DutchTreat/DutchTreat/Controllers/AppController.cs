using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
  public class AppController : Controller
  {
    private readonly IMailService _mailService;

    public AppController(IMailService mailService)
    {
      _mailService = mailService;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("contact")]
    public IActionResult Contact()
    {
      return View();
    }

    [HttpPost("contact")]
    public IActionResult Contact(ContactViewModel model)
    {
      if (ModelState.IsValid)
      {
        // Send the email
        _mailService.SendMessage("shawn@wildermuth.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
        ViewBag.UserMessage = "Mail Sent";
        ModelState.Clear();
      }

      return View();
    }

    public IActionResult About()
    {
      ViewBag.Title = "About Us";

      return View();
    }

  }
}
