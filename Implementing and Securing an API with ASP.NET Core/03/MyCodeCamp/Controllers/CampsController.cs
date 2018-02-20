using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;

namespace MyCodeCamp.Controllers
{
  [Route("api/[controller]")]
  public class CampsController : Controller
  {
    private ILogger<CampsController> _logger;
    private ICampRepository _repo;

    public CampsController(ICampRepository repo, ILogger<CampsController> logger)
    {
      _repo = repo;
      _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Get()
    {
      var camps = _repo.GetAllCamps();

      return Ok(camps);
    }

    [HttpGet("{id}", Name = "CampGet")]
    public IActionResult Get(int id, bool includeSpeakers = false)
    {
      try
      {
        Camp camp = null;

        if (includeSpeakers) camp = _repo.GetCampWithSpeakers(id);
        else camp = _repo.GetCamp(id);

        if (camp == null) return NotFound($"Camp {id} was not found");

        return Ok(camp);
      }
      catch
      {
      }

      return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Camp model)
    {
      try
      {
        _logger.LogInformation("Creating a new Code Camp");

        _repo.Add(model);
        if (await _repo.SaveAllAsync())
        {
          var newUri = Url.Link("CampGet", new { id = model.Id });
          return Created(newUri, model);
        }
        else
        {
          _logger.LogWarning("Could not save Camp to the database");
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Threw exception while saving Camp: {ex}");
      }

      return BadRequest();

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Camp model)
    {
      try
      {
        var oldCamp = _repo.GetCamp(id);
        if (oldCamp == null) return NotFound($"Could not find a camp with an ID of {id}");

        // Map model to the oldCamp
        oldCamp.Name = model.Name ?? oldCamp.Name;
        oldCamp.Description = model.Description ?? oldCamp.Description;
        oldCamp.Location = model.Location ?? oldCamp.Location;
        oldCamp.Length = model.Length > 0 ? model.Length : oldCamp.Length;
        oldCamp.EventDate = model.EventDate != DateTime.MinValue ? model.EventDate : oldCamp.EventDate; 

        if (await _repo.SaveAllAsync())
        {
          return Ok(oldCamp);
        }
      }
      catch (Exception)
      {

      }

      return BadRequest("Couldn't update Camp");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var oldCamp = _repo.GetCamp(id);
        if (oldCamp == null) return NotFound($"Could not find Camp with ID of {id}");

        _repo.Delete(oldCamp);
        if (await _repo.SaveAllAsync())
        {
          return Ok();
        }
      }
      catch (Exception)
      {
      }

      return BadRequest("Could not delete Camp");
    }

  }
}






