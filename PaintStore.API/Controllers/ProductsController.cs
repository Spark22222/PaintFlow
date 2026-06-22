using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PaintProduct>> GetAllPaintProducts(
        int page = 1,
        int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }

        List<PaintProduct> products = FakeData.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(products);
    }

    [HttpGet("price-range")]
    public ActionResult<List<PaintProduct>> GetProductsByPriceRange(
        decimal min,
        decimal max)
    {
        if (min > max)
        {
            return BadRequest("Min price cannot be greater than max price.");
        }

        List<PaintProduct> products = FakeData.Products
            .Where(product =>
                product.Price >= min &&
                product.Price <= max)
            .ToList();

        return Ok(products);
    }

    [HttpGet("{paintId:int}")]
    public ActionResult<PaintProduct> GetPaintProductByPaintId(int paintId)
    {
        PaintProduct? product = FakeData.Products
            .FirstOrDefault(product => product.Id == paintId);

        if (product == null)
        {
            return NotFound($"Paint product with id {paintId} was not found.");
        }

        return Ok(product);
    }

    [HttpGet("user/{userId:int}")]
    public ActionResult<List<PaintProduct>> GetPaintProductsByUserId(int userId)
    {
        bool userExists = FakeData.Users
            .Any(user => user.UserId == userId);

        if (!userExists)
        {
            return NotFound($"User with id {userId} was not found.");
        }

        List<PaintProduct> products = FakeData.Orders
            .Where(order => order.UserId == userId)
            .SelectMany(order => order.Products)
            .DistinctBy(product => product.Id)
            .ToList();

        return Ok(products);
    }
}