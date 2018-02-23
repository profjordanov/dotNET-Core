using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using MyCodeCamp.Filters;
using MyCodeCamp.Models;

namespace MyCodeCamp.Controllers
{
  [Route("api/[controller]")]
  [ValidateModel]
  public class CampsController : BaseController
  {
    private ILogger<CampsController> _logger;
    private ICampRepository _repo;
    private IMapper _mapper;

    public CampsController(ICampRepository repo, 
      ILogger<CampsController> logger,
      IMapper mapper)
    {
      _repo = repo;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet("")]
    public IActionResult Get()
    {
      var camps = _repo.GetAllCamps();

      return Ok(_mapper.Map<IEnumerable<CampModel>>(camps));
    }

    [HttpGet("{moniker}", Name = "CampGet")]
    public IActionResult Get(string moniker, bool includeSpeakers = false)
    {
      try
      {
        Camp camp = null;

        if (includeSpeakers) camp = _repo.GetCampByMonikerWithSpeakers(moniker);
        else camp = _repo.GetCampByMoniker(moniker);

        if (camp == null) return NotFound($"Camp {moniker} was not found");

        return Ok(_mapper.Map<CampModel>(camp));
      }
      catch
      {
      }

      return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]CampModel model)
    {
      try
      {
        _logger.LogInformation("Creating a new Code Camp");

        var camp = _mapper.Map<Camp>(model);

        _repo.Add(camp);
        if (await _repo.SaveAllAsync())
        {
          var newUri = Url.Link("CampGet", new { moniker = camp.Moniker });
          return Created(newUri, _mapper.Map<CampModel>(camp));
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

    [HttpPut("{moniker}")]
    public async Task<IActionResult> Put(string moniker, [FromBody] CampModel model)
    {
      try
      {
        var oldCamp = _repo.GetCampByMoniker(moniker);
        if (oldCamp == null) return NotFound($"Could not find a camp with an moniker of {moniker}");

        _mapper.Map(model, oldCamp);

        if (await _repo.SaveAllAsync())
        {
          return Ok(_mapper.Map<CampModel>(oldCamp));
        }
      }
      catch (Exception)
      {

      }

      return BadRequest("Couldn't update Camp");
    }

    [HttpDelete("{moniker}")]
    public async Task<IActionResult> Delete(string moniker)
    {
      try
      {
        var oldCamp = _repo.GetCampByMoniker(moniker);
        if (oldCamp == null) return NotFound($"Could not find Camp with moniker of {moniker}");

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






