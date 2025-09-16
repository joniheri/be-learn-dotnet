using Microsoft.AspNetCore.Mvc;
using MyBackendApi.Data;
using MyBackendApi.Models;

namespace MyBackendApi.Controllers
{
  [ApiController]
  [Route("users-dummy")]
  public class UsersController : ControllerBase
  {
    // GET /users
    [HttpGet]
    public IActionResult GetAll([FromQuery] int? page, [FromQuery] int? size)
    {
      // default value
      int currentPage = page ?? 1;
      int pageSize = size ?? 10;

      // total data
      int totalData = UserData.Users.Count;
      int totalPage = (int)Math.Ceiling(totalData / (double)pageSize);

      // ambil data sesuai page
      var data = UserData.Users
          .Skip((currentPage - 1) * pageSize)
          .Take(pageSize)
          .ToList();

      // hitung from - to
      int from = ((currentPage - 1) * pageSize) + 1;
      int to = Math.Min(from + pageSize - 1, totalData);

      var response = new
      {
        status = "success",
        message = "get data success",
        data,
        page = currentPage,
        size = pageSize,
        from,
        to,
        currentPage,
        totalPage
      };

      return Ok(response);
    }

    // GET /users/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = UserData.Users.FirstOrDefault(u => u.Id == id);
      if (user == null) return NotFound(new { message = "User not found" });
      return Ok(user);
    }

    // POST /users
    [HttpPost]
    public IActionResult Create([FromBody] UserDummyModel newUser)
    {
      if (UserData.Users.Any(u => u.Id == newUser.Id))
      {
        return BadRequest(new { message = "User with this ID already exists" });
      }
      UserData.Users.Add(newUser);
      return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
    }

    // PATCH /users/{id}
    [HttpPatch("{id}")]
    public IActionResult Update(int id, [FromBody] UserDummyModel updatedUser)
    {
      var user = UserData.Users.FirstOrDefault(u => u.Id == id);
      if (user == null) return NotFound(new { message = "User not found" });

      if (!string.IsNullOrEmpty(updatedUser.Name))
        user.Name = updatedUser.Name;

      if (!string.IsNullOrEmpty(updatedUser.Email))
        user.Email = updatedUser.Email;

      return Ok(user);
    }

    // DELETE /users/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var user = UserData.Users.FirstOrDefault(u => u.Id == id);
      if (user == null) return NotFound(new { message = "User not found" });

      UserData.Users.Remove(user);
      return NoContent();
    }
  }
}
