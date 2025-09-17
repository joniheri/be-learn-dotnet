using Microsoft.AspNetCore.Mvc;
using be_learn_dotnet.Models;
using be_learn_dotnet.Services;
using be_learn_dotnet.Helpers;

namespace be_learn_dotnet.Controllers
{
  [ApiController]
  [Route("users")]
  public class UserController(UserService service) : ControllerBase
  {
    private readonly UserService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] int? page, [FromQuery] int? size)
    {
      var users = _service.GetAll();
      return Ok(new { status = "success", message = "get data success", data = users });
    }

    // GET /users?page=1&size=10
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] int? page, [FromQuery] int? size)
    {
      int currentPage = page ?? 1;
      int pageSize = size ?? 10;

      var result = _service.GetPaged(currentPage, pageSize);

      var response = ApiResponse.SuccessWithPagination(
        message: "get data success",
        data: result.Data,
        page: currentPage,
        size: pageSize,
        from: result.From,
        to: result.To,
        currentPage: currentPage,
        totalPage: result.TotalPage,
        totalData: result.TotalData
      );

      return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _service.GetById(id);
      if (user == null) return NotFound(new { status = "error", message = "user not found" });
      return Ok(new { status = "success", message = "get data success", data = user });
    }

    [HttpPost]
    public IActionResult Create([FromBody] UserModel newUser)
    {
      var created = _service.Create(newUser);
      if (created == null) return BadRequest(new { status = "error", message = "failed to create user" });
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, new { status = "success", message = "create success", data = created });
    }

    [HttpPatch("{id}")]
    public IActionResult Update(int id, [FromBody] UserModel updatedUser)
    {
      var user = _service.Update(id, updatedUser);
      if (user == null) return NotFound(new { status = "error", message = "user not found" });
      return Ok(new { status = "success", message = "update success", data = user });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var deleted = _service.Delete(id);
      if (!deleted) return NotFound(new { status = "error", message = "user not found" });
      return Ok(new { status = "success", message = "delete success" });
    }
  }
}
