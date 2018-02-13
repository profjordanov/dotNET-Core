﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
  [Route("/api/orders/{orderid}/items")]
  [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
  public class OrderItemsController : Controller
  {
    private readonly IDutchRepository _repository;
    private readonly ILogger<OrderItemsController> _logger;
    private readonly IMapper _mapper;

    public OrderItemsController(IDutchRepository repository, 
      ILogger<OrderItemsController> logger,
      IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(int orderId)
    {
      var order = _repository.GetOrderById(User.Identity.Name, orderId);
      if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
      return NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int orderId, int id)
    {
      var order = _repository.GetOrderById(User.Identity.Name, orderId);
      if (order != null)
      {
        var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
        if (item != null)
        {
          return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
        }
      }
      return NotFound();

    }


  }
}
