using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly PaintStoreDbContext _context;

    public ProductsController(PaintStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<PaintProduct>>> GetAllPaintProducts(
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        List<PaintProduct> products = await _context.PaintProducts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("price-range")]
    public async Task<ActionResult<List<PaintProduct>>> GetProductsByPriceRange(
        decimal min,
        decimal max)
    {
        if (min > max)
        {
            return BadRequest("Min price cannot be greater than max price.");
        }

        List<PaintProduct> products = await _context.PaintProducts
            .Where(product =>
                product.Price >= min &&
                product.Price <= max)
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{paintId:int}")]
    public async Task<ActionResult<PaintProduct>> GetPaintProductByPaintId(
        int paintId)
    {
        PaintProduct? product = await _context.PaintProducts
            .FirstOrDefaultAsync(product => product.Id == paintId);

        if (product == null)
        {
            return NotFound($"Paint product with id {paintId} was not found.");
        }

        return Ok(product);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<PaintProduct>>> GetPaintProductsByUserId(
        int userId)
    {
        bool userExists = await _context.Users
            .AnyAsync(user => user.UserId == userId);

        if (!userExists)
        {
            return NotFound($"User with id {userId} was not found.");
        }

        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Where(order => order.UserId == userId)
            .ToListAsync();

        List<PaintProduct> products = orders
            .SelectMany(order => order.OrderItems)
            .Where(item => item.PaintProduct != null)
            .Select(item => item.PaintProduct!)
            .DistinctBy(product => product.Id)
            .ToList();

        return Ok(products);
    }
}