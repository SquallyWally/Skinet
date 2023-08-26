using Core.Entities;
using Core.Interfaces;

using Moq;

namespace Infrastructure.Tests.Products;

[TestFixture]
public class ProductRepositoryTests
{
#pragma warning disable CS8618
    private Mock<IProductRepository> _productRepository;
#pragma warning restore CS8618

    [SetUp]
    public void Setup()
    {
        _productRepository = new Mock<IProductRepository>(MockBehavior.Strict);

        _productRepository
            .Setup(r => r.GetProductByIdAsync(_productId))
            .ReturnsAsync(_product);

        _productRepository
            .Setup(r => r.GetProductsAsync())
            .ReturnsAsync(_products);

        _productRepository
            .Setup(r => r.GetProductsBrandsAsync())
            .ReturnsAsync(_productBrands);

        _productRepository
            .Setup(r => r.GetProductsTypesAsync())
            .ReturnsAsync(_productTypes);
    }

    [Test]
    public async Task GetProductByIdAsync_ProductExists_ReturnsProduct()
    {
        var result = await _productRepository.Object.GetProductByIdAsync(_productId);

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result.Id,
            Is.EqualTo(_productId));

        _productRepository.Verify();
    }

    [Test]
    public async Task GetProductsAsync_ReturnsListOfProducts()
    {
        var result = await _productRepository.Object.GetProductsAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_products.Count));

        _productRepository.Verify();
    }

    [Test]
    public async Task GetProductsBrandsAsync_ReturnsListOfProductBrands()
    {
        var result = await _productRepository.Object.GetProductsBrandsAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_productBrands.Count));

        _productRepository.Verify();
    }

    [Test]
    public async Task GetProductsTypesAsync_ReturnsListOfProductTypes()
    {
        var result = await _productRepository.Object.GetProductsTypesAsync();

        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result,
            Has.Count.EqualTo(_productTypes.Count));

        _productRepository.Verify();
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
