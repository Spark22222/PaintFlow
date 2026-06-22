using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<User>> GetUsers(
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        List<User> users = FakeData.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetUserById(int id)
    {
        User? user = FakeData.Users
            .FirstOrDefault(user => user.UserId == id);

        if (user == null)
        {
            return NotFound($"User with id {id} was not found.");
        }

        return Ok(user);
    }

    [HttpGet("name/{name}")]
    public ActionResult<List<User>> GetUserByName(string name)
    {
        List<User> users = FakeData.Users
            .Where(user =>
                user.Name.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            .ToList();

        return Ok(users);
    }

    [HttpGet("email")]
    public ActionResult<User> GetUserByEmail(string email)
    {
        User? user = FakeData.Users
            .FirstOrDefault(user =>
                user.Email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (user == null)
        {
            return NotFound($"User with email {email} was not found.");
        }

        return Ok(user);
    }
}