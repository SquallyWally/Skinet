using System.Linq.Expressions;

using Core.Entities;
using Core.Specifications;

namespace Infrastructure.Tests.Specifications;

[TestFixture]
public class ProductsWithTypesAndBrandsSpecificationTests
{
    [Test]
    public void Constructor_WithProductParams_SearchCriteria_SetsCorrectly()
    {
        // Arrange
        var productParams = new ProductSpecParams
        {
            Search = _name,
            BrandId = 1,
            TypeId = 2,
            PageSize = 10,
            PageNumber = 1,
            Sort = _sort,
        };

        // Act
        var specification = new ProductsWithTypesAndBrandsSpecification(productParams);

        // Assert
        var expectedCriteria = GetExpectedCriteria(productParams);
        var compiledSpecCriteria = specification.Criteria.Compile();

        var product = new Product
        {
            Name = _name,
            ProductBrandId = 1,
            ProductTypeId = 2,
        };

        var actualResult = compiledSpecCriteria(product);
        var expectedResult = expectedCriteria.Compile()(product);

        Assert.That(
            actualResult,
            Is.EqualTo(expectedResult));
    }

    #region Helpers

    internal const string _name = "Product1";

    internal const string _sort = "priceAsc";

    private Expression<Func<Product, bool>> GetExpectedCriteria(
        ProductSpecParams productParams)
    {
        return x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower()
                .Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId);
    }

    #endregion
}
