using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Filters;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
  public class AuthController : Controller
  {
    private CampContext _context;
    private ILogger<AuthController> _logger;
    private SignInManager<CampUser> _signInMgr;

    public AuthController(CampContext context, SignInManager<CampUser> signInMgr, ILogger<AuthController> logger)
    {
      _context = context;
      _signInMgr = signInMgr;
      _logger = logger;
    }

    [HttpPost("api/auth/login")]
    [ValidateModel]
    public async Task<IActionResult> Login([FromBody] CredentialModel model)
    {
      try
      {
        var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false);
        if (result.Succeeded)
        {
          return Ok();
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Exception thrown while logging in: {ex}");  
      }

      return BadRequest("Failed to login");
    }
  }
}
