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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vksService;

    public VaultKeepsController(VaultKeepsService vksService)
    {
      _vksService = vksService;
    }


    [HttpGet("{id}")]
    public ActionResult<VaultKeepViewModel> GetById(int id)
    {
      try
      {
        return Ok(_vksService.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeep)
    {
      try
      {
        // REVIEW enforce the current user context
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeep.CreatorId = userInfo.Id;
        var newVaultKeep = _vksService.Create(vaultKeep);
        return Ok(newVaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Remove(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        _vksService.Remove(id, userInfo.Id);
        return Ok("VaultKeep Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}