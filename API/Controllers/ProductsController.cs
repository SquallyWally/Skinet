using API.Dtos;

using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

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
    public async Task<IEnumerable<ProductsToReturnDto>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();

        var products = await _productsRepository.ListAsync(spec);

        return products.Select(
            product => new ProductsToReturnDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    PictureUrl = product.PictureUrl,
                    ProductBrand = product.ProductBrand.Name,
                    ProductType = product.ProductType.Name,
                });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductsToReturnDto>> GetProduct(
        int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var product = await _productsRepository.GetEntityWithSpec(spec);

        return new ProductsToReturnDto
            {
                Id = product!.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name,
            };
    }

    [HttpGet]
    [Route("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductsBrands()
    {
        return Ok(await _productBrandsRepository.ListAllAsync());
    }

    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductsTypes()
    {
        return Ok(await _productTypesRepository.ListAllAsync());
    }
}
