using Core.Entities;
using Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepository;

    private readonly IGenericRepository<ProductBrand> _productBrandsRepository;

    private readonly IGenericRepository<ProductType> _productTypesRepository;

    public ProductsController(
        IGenericRepository<Product>      productsRepository,
        IGenericRepository<ProductBrand> productBrandsRepository,
        IGenericRepository<ProductType>  productTypesRepository)
    {
        _productsRepository = productsRepository;
        _productBrandsRepository = productBrandsRepository;
        _productTypesRepository = productTypesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts() => Ok(await _productsRepository.ListAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(
        int id) =>
        await _productsRepository.GetByIdAsync(id);

    [HttpGet]
    [Route("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductsBrands() => Ok(await _productBrandsRepository.ListAllAsync());

    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductsTypes() => Ok(await _productTypesRepository.ListAllAsync());
}
