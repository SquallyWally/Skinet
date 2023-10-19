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

    #region Helpers

    internal const string _addIncludeName = "AddInclude";

    internal const int _Id = 1;

    #endregion
}

// Define a sample entity class for testing
public class MyEntity
{
    public int Id { get; set; }

    public RelatedEntity RelatedEntity { get; set; }
}

public record RelatedEntity;
