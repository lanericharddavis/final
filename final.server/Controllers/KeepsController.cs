using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using final.server.Models;
using final.server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace final.server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
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

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep keep = _keepsService.GetById(id);
        return keep;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keep.CreatorId = userInfo.Id;
        Keep newKeep = _keepsService.Create(keep);
        newKeep.Creator = userInfo;
        return Ok(newKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep update)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        update.Id = id;
        Keep updated = _keepsService.Update(update, userInfo);
        updated.Creator = userInfo;
        return Ok(updated);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Keep>> Remove(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        _keepsService.Remove(id, userInfo.Id);
        return Ok("Keep Successfully Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}