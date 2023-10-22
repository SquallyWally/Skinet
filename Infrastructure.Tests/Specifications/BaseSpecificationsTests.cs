using System.Linq.Expressions;
using System.Reflection;

using Core.Specifications;

namespace Infrastructure.Tests.Specifications;

[TestFixture]
public class BaseSpecificationTests
{
    [Test]
    public void Constructor_WithCriteria_SetsCriteria()
    {
        // Arrange
        Expression<Func<MyEntity, bool>> expectedCriteria = x => x.Id == _Id;

        // Act
        var specification = new BaseSpecification<MyEntity>(expectedCriteria);

        // Assert
        Assert.That(
            specification.Criteria,
            Is.EqualTo(expectedCriteria));
    }

    [Test]
    public void AddInclude_AddsToIncludesList()
    {
        // Arrange
        var specification = new BaseSpecification<MyEntity>();
        Expression<Func<MyEntity, object>> includeExpression = x => x.RelatedEntity;

        // Act
        var methodInfo = typeof(BaseSpecification<MyEntity>).GetMethod(
            _addIncludeName,
            BindingFlags.NonPublic | BindingFlags.Instance);

        methodInfo?.Invoke(
            specification,
            new object[]
                {
                    includeExpression,
                });

        // Assert
        Assert.That(
            specification.Includes,
            Does.Contain(includeExpression));
    }

    [Test]
    public void AddOrderBy_SetsOrderBy()
    {
        // Arrange
        var specification = new BaseSpecification<MyEntity>();
        Expression<Func<MyEntity, object>> orderByExpression = x => x.Id;

        // Act
        var methodInfo = typeof(BaseSpecification<MyEntity>).GetMethod(
            _addOrderByName,
            BindingFlags.NonPublic | BindingFlags.Instance);

        methodInfo?.Invoke(
            specification,
            new object[]
                {
                    orderByExpression,
                });

        // Assert
        Assert.That(
            specification.OrderBy,
            Is.EqualTo(orderByExpression));
    }

    [Test]
    public void AddOrderByDescending_SetsOrderByDescending()
    {
        // Arrange
        var specification = new BaseSpecification<MyEntity>();
        Expression<Func<MyEntity, object>> orderByDescendingExpression = x => x.Id;

        // Act
        var methodInfo = typeof(BaseSpecification<MyEntity>).GetMethod(
            _addOrderByDescendingName,
            BindingFlags.NonPublic | BindingFlags.Instance);

        methodInfo?.Invoke(
            specification,
            new object[]
                {
                    orderByDescendingExpression,
                });

        // Assert
        Assert.That(
            specification.OrderByDescending,
            Is.EqualTo(orderByDescendingExpression));
    }

    [Test]
    public void ApplyPaging_SetsPagingProperties()
    {
        // Arrange
        var specification = new TestableBaseSpecification<MyEntity>();

        // Act
        specification.InvokeApplyPaging(
            _skip,
            _take);

        Assert.Multiple(
            () =>
                {
                    // Assert
                    Assert.That(
                        specification.Skip,
                        Is.EqualTo(_skip));

                    Assert.That(
                        specification.Take,
                        Is.EqualTo(_take));

                    Assert.That(
                        specification.IsPagingEnabled,
                        Is.True);
                });
    }

    #region Helpers

    internal const string _addIncludeName = "AddInclude";

    internal const string _addOrderByName = "AddOrderBy";

    internal const string _addOrderByDescendingName = "AddOrderByDescending";

    internal const int _skip = 10;

    internal const int _take = 10;

    internal const int _Id = 1;

    #endregion
}

public class TestableBaseSpecification<T> : BaseSpecification<T>
{
    public void InvokeApplyPaging(
        int skip,
        int take)
    {
        ApplyPaging(
            skip,
            take);
    }
}

// Define a sample entity class for testing
public class MyEntity
{
    public int Id { get; set; }

    public RelatedEntity RelatedEntity { get; set; }
}

public record RelatedEntity;
