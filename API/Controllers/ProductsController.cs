using API.Dtos;

using AutoMapper;

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

    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product>      productsRepository,
        IGenericRepository<ProductBrand> productBrandsRepository,
        IGenericRepository<ProductType>  productTypesRepository,
        IMapper                          mapper)
    {
        _productsRepository = productsRepository;
        _productBrandsRepository = productBrandsRepository;
        _productTypesRepository = productTypesRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductsToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();

        var products = await _productsRepository.ListAsync(spec);

        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductsToReturnDto>>(products));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductsToReturnDto>> GetProduct(
        int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var product = await _productsRepository.GetEntityWithSpec(spec);

        return _mapper.Map<Product, ProductsToReturnDto>(product);
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
