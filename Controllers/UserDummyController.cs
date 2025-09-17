using Microsoft.AspNetCore.Mvc;
using be_learn_dotnet.Models;
using be_learn_dotnet.Services;
using be_learn_dotnet.Helpers;

namespace be_learn_dotnet.Controllers
{
  [ApiController]
  [Route("users-dummy")]
  public class UserDummyController : ControllerBase
  {
    private readonly UserDummyService _userService;

    // constructor pakai Dependency Injection
    public UserDummyController(UserDummyService userService)
    {
      _userService = userService;
    }

    // GET /users-dummy
    [HttpGet]
    public IActionResult GetAll([FromQuery] int? page, [FromQuery] int? size)
    {
      int currentPage = page ?? 1;
      int pageSize = size ?? 10;

      var data = _userService.GetAll(currentPage, pageSize, out int totalData, out int totalPage, out int from, out int to);

      var response = ApiResponse.SuccessWithPagination(
        message: "get data success",
        data: data,
        page: currentPage,
        size: pageSize,
        from: from,
        to: to,
        currentPage: currentPage,
        totalPage: totalPage,
        totalData: totalData
      );

      return Ok(response);
    }

    // GET /users-dummy/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _userService.GetById(id);
      if (user == null) return NotFound(ApiResponse.Error($"User with ID: {id} not found"));

      return Ok(ApiResponse.Success("get user by id success", user));
    }

    // POST /users-dummy
    [HttpPost]
    public IActionResult Create([FromBody] UserDummyModel newUser)
    {
      var createdUser = _userService.Create(newUser);
      if (createdUser == null) return BadRequest(ApiResponse.Error("failed to create user"));

      return CreatedAtAction(
        nameof(GetById),
        new { id = createdUser.Id },
        ApiResponse.Success("create user success", createdUser)
      );
    }

    // PATCH /users-dummy/{id}
    [HttpPatch("{id}")]
    public IActionResult Update(int id, [FromBody] UserDummyModel updatedUser)
    {
      var user = _userService.Update(id, updatedUser);
      if (user == null) return NotFound(ApiResponse.Error($"User with ID: {id} not found"));

      return Ok(ApiResponse.Success("update user success", user));
    }

    // DELETE /users-dummy/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var deleted = _userService.Delete(id);
      if (!deleted) return NotFound(new { message = "User not found" });

      return Ok(ApiResponse.Success("delete user success"));
    }
  }
}
