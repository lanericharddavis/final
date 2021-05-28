using System;
using System.Collections.Generic;
using final.server.Models;
using final.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.server.Controllers
{
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        List<Keep> keeps = _keepsService.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // [HttpGet("{id}")]
    // [HttpPost]
    // [HttpPut]
    // [HttpDelete]
  }
}