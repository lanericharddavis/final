using final.server.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.server.Controllers
{
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;

    public VaultsController(VaultsService vaultsService)
    {
      _vaultsService = vaultsService;
    }


    // [HttpGet("{id}")]
    // [HttpPost]
    // [HttpPut]
    // [HttpDelete]
  }
}