using Microsoft.AspNetCore.Mvc;

namespace be_learn_dotnet.Controllers
{
  [ApiController]
  [Route("/")] // route root (GET /)
  public class AppController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      var port = HttpContext.Connection.LocalPort;
      return Ok($"Project is run at port {port}!");
    }
  }
}
