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
  public class SpeakerUrlResolver : IValueResolver<Speaker, SpeakerModel, string>
  {
    private IHttpContextAccessor _httpContextAccessor;

    public SpeakerUrlResolver(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string Resolve(Speaker source, SpeakerModel destination, string destMember, ResolutionContext context)
    {
      var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.URLHELPER];
      return url.Link("SpeakerGet", new { moniker = source.Camp.Moniker, id = source.Id });

    }
  }
}
