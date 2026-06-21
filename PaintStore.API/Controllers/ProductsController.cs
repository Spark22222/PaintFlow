using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Data;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PaintProduct>> GetAllProducts()
    {
        return Ok(FakeData.Products);
    }
}