using System.Linq.Expressions;

using Core.Entities;
using Core.Specifications;

namespace Infrastructure.Tests.Specifications;

[TestFixture]
public class ProductWithFiltersForCountSpecificationsTests
{
    [Test]
    public void Constructor_WithProductParams_SearchCriteria_SetsCorrectly()
    {
        // Arrange
        var productParams = new ProductSpecParams
            {
                Search = _name,
            };

        // Act
        var specification = new ProductWithFiltersForCountSpecifications(productParams);
        var expectedCriteria = GetExpectedCriteria(productParams);
        var compiledSpecCriteria = specification.Criteria.Compile();

        // Assert
        var product = new Product
            {
                Name = _name,
            };

        var actualResult = compiledSpecCriteria(product);
        var expectedResult = expectedCriteria.Compile()(product);

        Assert.That(
            actualResult,
            Is.EqualTo(expectedResult));
    }

    #region Helpers

    internal const string _name = "Product1";

    private static Expression<Func<Product, bool>> GetExpectedCriteria(
        ProductSpecParams productParams)
    {
        return x =>
            ( string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower()
                .Contains(productParams.Search) ) &&
            ( !productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId ) &&
            ( !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId );
    }

    #endregion
}
