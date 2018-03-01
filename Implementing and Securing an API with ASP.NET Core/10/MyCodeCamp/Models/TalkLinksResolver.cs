using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCodeCamp.Controllers;
using MyCodeCamp.Data.Entities;

namespace MyCodeCamp.Models
{
  public class TalkLinksResolver : IValueResolver<Talk, TalkModel, ICollection<LinkModel>>
  {
    private IHttpContextAccessor _httpContextAccessor;

    public TalkLinksResolver(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public ICollection<LinkModel> Resolve(Talk source, TalkModel destination, ICollection<LinkModel> destMember, ResolutionContext context)
    {
      var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.URLHELPER];

      return new List<LinkModel>()
      {
        new LinkModel()
        {
          Rel = "Self",
          Href = url.Link("GetTalk", new { moniker = source.Speaker.Camp.Moniker, speakerId = source.Speaker.Id, id = source.Id })
        },
        new LinkModel()
        {
          Rel = "Update",
          Href = url.Link("UpdateTalk", new { moniker = source.Speaker.Camp.Moniker, speakerId = source.Speaker.Id, id = source.Id }),
          Verb = "PUT"
        },
        new LinkModel()
        {
          Rel = "Speaker",
          Href = url.Link("SpeakerGet", new { moniker = source.Speaker.Camp.Moniker, id = source.Speaker.Id })
        },
      };
    }
  }
}
