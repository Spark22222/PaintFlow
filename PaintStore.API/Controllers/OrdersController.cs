using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly PaintStoreDbContext _context;

    public OrdersController(PaintStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllOrders(
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("price-range")]
    public async Task<ActionResult<List<Order>>> GetOrdersByPriceRange(
        decimal min,
        decimal max)
    {
        if (min > max)
        {
            return BadRequest("Min price cannot be greater than max price.");
        }

        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Where(order =>
                order.TotalPrice >= min &&
                order.TotalPrice <= max)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("paint/{paintId:int}")]
    public async Task<ActionResult<List<Order>>> GetOrdersByPaintId(int paintId)
    {
        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Where(order =>
                order.OrderItems.Any(item =>
                    item.PaintProductId == paintId))
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<Order>>> GetOrdersByUserId(int userId)
    {
        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Where(order => order.UserId == userId)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("last-month")]
    public async Task<ActionResult<List<Order>>> GetLastMonthOrders()
    {
        DateTime today = DateTime.Today;
        DateTime firstDayOfThisMonth =
            new DateTime(today.Year, today.Month, 1);

        DateTime firstDayOfLastMonth =
            firstDayOfThisMonth.AddMonths(-1);

        DateTime firstDayOfThisMonthStart =
            firstDayOfThisMonth;

        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Where(order =>
                order.CreatedAt >= firstDayOfLastMonth &&
                order.CreatedAt < firstDayOfThisMonthStart)
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("date")]
    public async Task<ActionResult<List<Order>>> GetOrdersByDate(DateTime date)
    {
        DateTime start = date.Date;
        DateTime end = start.AddDays(1);

        List<Order> orders = await _context.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.PaintProduct)
            .Where(order =>
                order.CreatedAt >= start &&
                order.CreatedAt < end)
            .ToListAsync();

        return Ok(orders);
    }
}