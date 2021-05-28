using System;
using System.Collections.Generic;
using final.server.Models;
using final.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly ProfilesService _profilesService;
    public ProfilesController(AccountService accountService, ProfilesService profilesService)
    {
      _accountService = accountService;
      _profilesService = profilesService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfileById(string id)
    {
      try
      {
        Profile profile = _accountService.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(int id)
    {
      try
      {
        List<Keep> keeps = _profilesService.GetKeepsByProfileId(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public ActionResult<List<Keep>> GetVaultsByProfileId(int id)
    {
      try
      {
        List<Vault> vaults = _profilesService.GetVaultsByProfileId(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}