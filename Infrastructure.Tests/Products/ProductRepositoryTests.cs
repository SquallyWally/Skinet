using Core.Entities;
using Core.Interfaces;

using NSubstitute;

namespace Infrastructure.Tests.Products;

[TestFixture]
public class ProductRepositoryTests
{
#pragma warning disable CS8618
    private IProductRepository _productRepository;
#pragma warning restore CS8618

    [SetUp]
    public void Setup()
    {
        _productRepository = Substitute.For<IProductRepository>();

        _productRepository
            .GetProductByIdAsync(_productId)
            .Returns(_product);

        _productRepository
            .GetProductsAsync()
            .Returns(_products);

        _productRepository
            .GetProductsBrandsAsync()
            .Returns(_productBrands);

        _productRepository
            .GetProductsTypesAsync()
            .Returns(_productTypes);
    }

    [Test]
    public async Task GetProductByIdAsync_ProductExists_ReturnsProduct()
    {
        var result = await _productRepository.GetProductByIdAsync(_productId);

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result.Id,
            Is.EqualTo(_productId));

        await _productRepository.Received()
            .GetProductByIdAsync(_productId);
    }

    [Test]
    public async Task GetProductsAsync_ReturnsListOfProducts()
    {
        var result = await _productRepository.GetProductsAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_products.Count));

        await _productRepository.Received()
            .GetProductsAsync();
    }

    [Test]
    public async Task GetProductsBrandsAsync_ReturnsListOfProductBrands()
    {
        var result = await _productRepository.GetProductsBrandsAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_productBrands.Count));

        await _productRepository.Received()
            .GetProductsBrandsAsync();
    }

    [Test]
    public async Task GetProductsTypesAsync_ReturnsListOfProductTypes()
    {
        var result = await _productRepository.GetProductsTypesAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_productTypes.Count));

        await _productRepository.Received()
            .GetProductsTypesAsync();
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
