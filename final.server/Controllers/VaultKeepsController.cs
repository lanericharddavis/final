using final.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.server.Controllers
{
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vksService;

    public VaultKeepsController(VaultKeepsService vksService)
    {
      _vksService = vksService;
    }

    // [HttpGet] OR [HttpGet("{id}")] ???
    // [HttpPost]
    // [HttpDelete]
  }
}