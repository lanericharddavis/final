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
  }
}