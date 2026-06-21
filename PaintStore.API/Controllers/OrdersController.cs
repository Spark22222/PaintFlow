using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Order>> GetAllOrders(int page = 1,int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        List<Order> orders = FakeData.Orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Ok(orders);
    }

    [HttpGet("price-range")]
     public ActionResult<List<Order>> GetOrdersByPriceRange(decimal min,decimal max)
    {
        if (min > max)
        {
            return BadRequest("Min price cannot be greater than max price.");
        }

        List<Order> orders = FakeData.Orders.Where(order => order.TotalPrice >= min && order.TotalPrice <= max).ToList();

        return Ok(orders);
    }

    [HttpGet("paint/{paintId}")]
    public ActionResult<List<Order>> GetOrdersByPaintId(int paintId)
    {
        List<Order> orders = FakeData.Orders.Where(order => order.Products.Any(product => product.Id == paintId)).ToList();

        return Ok(orders);
    }

        [HttpGet("user/{userId}")]
    public ActionResult<List<Order>> GetOrdersByUserId(int userId)
    {
        List<Order> orders = FakeData.Orders
            .Where(order => order.UserId == userId)
            .ToList();

        return Ok(orders);
    }

    [HttpGet("last-month")]
    public ActionResult<List<Order>> GetLastMonthOrders()
    {
        DateTime today = DateTime.Today;
        DateTime firstDayOfThisMonth = new DateTime(today.Year, today.Month, 1);
        DateTime firstDayOfLastMonth = firstDayOfThisMonth.AddMonths(-1);
        DateTime lastDayOfLastMonth = firstDayOfThisMonth.AddDays(-1);

        List<Order> orders = FakeData.Orders.Where(order => order.CreatedAt.Date >= firstDayOfLastMonth && order.CreatedAt.Date <= lastDayOfLastMonth).ToList();

        return Ok(orders);
    }

    [HttpGet("date")]
    public ActionResult<List<Order>> GetOrdersByDate(DateTime date)
    {
        List<Order> orders = FakeData.Orders.Where(order => order.CreatedAt.Date == date).ToList();

        return Ok(orders);
    }

}