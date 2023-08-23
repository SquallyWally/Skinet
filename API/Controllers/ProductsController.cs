using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public string GetProducts() => "this will be a list of products";
    
    [HttpGet("{id:int}")]
    public string GetProduct(int id) => "this will be a product";
}
