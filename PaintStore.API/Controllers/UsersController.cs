using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly PaintStoreDbContext _context;

    public UsersController(PaintStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers(
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        List<User> users = await _context.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        User? user = await _context.Users
            .FirstOrDefaultAsync(user => user.UserId == id);

        if (user == null)
        {
            return NotFound($"User with id {id} was not found.");
        }

        return Ok(user);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<User>>> GetUserByName(string name)
    {
        List<User> users = await _context.Users
            .Where(user =>
                EF.Functions.Like(user.Name, $"%{name}%"))
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("email")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        User? user = await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            return NotFound($"User with email {email} was not found.");
        }

        return Ok(user);
    }
}