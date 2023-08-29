using Core.Entities;
using Core.Interfaces;

using NSubstitute;

namespace Infrastructure.Tests;

[TestFixture]
public class GenericRepositoryTests
{
#pragma warning disable CS8618
    private IGenericRepository<Product> _productRepository;

    private IGenericRepository<ProductType> _productTypesRepository;

    private IGenericRepository<ProductBrand> _productBrandsRepository;

#pragma warning restore CS8618

    [SetUp]
    public void Setup()
    {
        _productRepository = Substitute.For<IGenericRepository<Product>>();
        _productTypesRepository = Substitute.For<IGenericRepository<ProductType>>();
        _productBrandsRepository = Substitute.For<IGenericRepository<ProductBrand>>();

        _productRepository
            .GetByIdAsync(_productId)
            .Returns(_product);

        _productRepository
            .ListAllAsync()
            .Returns(_products);

        _productTypesRepository
            .ListAllAsync()
            .Returns(_productTypes);

        _productBrandsRepository
            .ListAllAsync()
            .Returns(_productBrands);
    }

    [Test]
    public async Task GetProductByIdAsync_ProductExists_ReturnsProduct()
    {
        var result = await _productRepository.GetByIdAsync(_productId);

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result.Id,
            Is.EqualTo(_productId));

        await _productRepository.Received()
            .GetByIdAsync(_productId);
    }

    [Test]
    public async Task GetProductsAsync_ReturnsListOfProducts()
    {
        var result = await _productRepository.ListAllAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_products.Count));

        await _productRepository.Received()
            .ListAllAsync();
    }

    [Test]
    public async Task GetProductsBrandsAsync_ReturnsListOfProductBrands()
    {
        var result = await _productBrandsRepository.ListAllAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_productBrands.Count));

        await _productBrandsRepository.Received()
            .ListAllAsync();
    }

    [Test]
    public async Task GetProductsTypesAsync_ReturnsListOfProductTypes()
    {
        var result = await _productTypesRepository.ListAllAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_productTypes.Count));

        await _productTypesRepository.Received()
            .ListAllAsync();
    }

    #region Helpers

    private const int _productId = 1;

    private readonly Product _product = new()
        {
            Id = _productId,
        };

    private readonly List<Product> _products = new()
        {
            new(),
            new(),
        };

    private readonly List<ProductBrand> _productBrands = new()
        {
            new(),
            new(),
        };

    private readonly List<ProductType> _productTypes = new()
        {
            new(),
            new(),
        };

    #endregion
}
