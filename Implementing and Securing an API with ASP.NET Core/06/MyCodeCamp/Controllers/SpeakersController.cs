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
  [Route("api/camps/{moniker}/speakers")]
  [ValidateModel]
  public class SpeakersController : BaseController
  {
    private ILogger<SpeakersController> _logger;
    private IMapper _mapper;
    private ICampRepository _repository;

    public SpeakersController(ICampRepository repository,
      ILogger<SpeakersController> logger,
      IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(string moniker, bool includeTalks = false)
    {
      var speakers = includeTalks ? _repository.GetSpeakersByMonikerWithTalks(moniker) : _repository.GetSpeakersByMoniker(moniker);

      return Ok(_mapper.Map<IEnumerable<SpeakerModel>>(speakers));
    }

    [HttpGet("{id}", Name = "SpeakerGet")]
    public IActionResult Get(string moniker, int id, bool includeTalks = false)
    {
      var speaker = includeTalks ? _repository.GetSpeakerWithTalks(id) : _repository.GetSpeaker(id);
      if (speaker == null) return NotFound();
      if (speaker.Camp.Moniker != moniker) return BadRequest("Speaker not in specified Camp");

      return Ok(_mapper.Map<SpeakerModel>(speaker));
    }

    [HttpPost]
    public async Task<IActionResult> Post(string moniker, [FromBody]SpeakerModel model)
    {
      try
      {
        var camp = _repository.GetCampByMoniker(moniker);
        if (camp == null) return BadRequest("Could not find camp");

        var speaker = _mapper.Map<Speaker>(model);
        speaker.Camp = camp;

        _repository.Add(speaker);

        if (await _repository.SaveAllAsync())
        {
          var url = Url.Link("SpeakerGet", new { moniker = camp.Moniker, id = speaker.Id });
          return Created(url, _mapper.Map<SpeakerModel>(speaker));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Exception thrown while adding speaker: {ex}");
      }

      return BadRequest("Could not add new speaker");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string moniker,
      int id,
      [FromBody] SpeakerModel model)
    {
      try
      {
        var speaker = _repository.GetSpeaker(id);
        if (speaker == null) return NotFound();
        if (speaker.Camp.Moniker != moniker) return BadRequest("Speaker and Camp do not match");

        _mapper.Map(model, speaker);

        if (await _repository.SaveAllAsync())
        {
          return Ok(_mapper.Map<SpeakerModel>(speaker));
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Exception thrown while updating speaker: {ex}");
      }

      return BadRequest("Could not update speaker");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string moniker, int id)
    {
      try
      {
        var speaker = _repository.GetSpeaker(id);
        if (speaker == null) return NotFound();
        if (speaker.Camp.Moniker != moniker) return BadRequest("Speaker and Camp do not match");

        _repository.Delete(speaker);

        if (await _repository.SaveAllAsync())
        {
          return Ok();
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Exception thrown while deleting speaker: {ex}");
      }

      return BadRequest("Could not delete speaker");
    }
  }
}
