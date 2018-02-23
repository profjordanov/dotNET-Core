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
  public class CampUrlResolver : IValueResolver<Camp, CampModel, string>
  {
    private IHttpContextAccessor _httpContextAccessor;

    public CampUrlResolver(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }


    public string Resolve(Camp source, CampModel destination, string destMember, ResolutionContext context)
    {
      var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.URLHELPER];
      return url.Link("CampGet", new { moniker = source.Moniker });

    }
  }
}
