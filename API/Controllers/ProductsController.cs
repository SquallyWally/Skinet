using Core.Entities;
using Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts() => Ok(await _productRepository.GetProductsAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(
        int id) =>
        await _productRepository.GetProductByIdAsync(id);
}
