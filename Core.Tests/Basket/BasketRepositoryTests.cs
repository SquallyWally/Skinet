using System.Text.Json;

using Core.Entities;
using Core.Interfaces;

using Infrastructure.Data.Repository;

using NSubstitute;

using StackExchange.Redis;

namespace Core.Tests.Basket;

[TestFixture]
public class BasketRepositoryTests
{
#pragma warning disable CS8618
    private IBasketRepository _basketRepository;

    private IConnectionMultiplexer _redis;
#pragma warning restore CS8618
    [SetUp]
    public void Setup()
    {
        _redis = Substitute.For<IConnectionMultiplexer>();
        _basketRepository = new BasketRepository(_redis);
    }

    [Test]
    public async Task GetBasketAsync_BasketExists_ReturnsCustomerBasket()
    {
        // Arrange

        _redis.GetDatabase()
            .StringGetAsync(_basketId)
            .Returns(JsonSerializer.Serialize(_customerBasket));

        // Act
        var result = await _basketRepository.GetBasketAsync(_basketId);

        // Assert
        Assert.That(
            result,
            Is.Not.Null);

        Assert.That(
            result?.Id,
            Is.EqualTo(_basketId));

        await _redis.GetDatabase()
            .Received()
            .StringGetAsync(_basketId);
    }

    [Test]
    public async Task GetBasketAsync_BasketDoesNotExist_ReturnsNull()
    {
        // Arrange
        _redis.GetDatabase()
            .StringGetAsync(_nonExistentBasketId)
            .Returns(Task.FromResult(RedisValue.Null));

        // Act
        var result = await _basketRepository.GetBasketAsync(_nonExistentBasketId);

        // Assert
        Assert.That(
            result,
            Is.Null);

        await _redis.GetDatabase()
            .Received()
            .StringGetAsync(_nonExistentBasketId);
    }

    [Test]
    public async Task DeleteBasketAsync_DeletesBasket_ReturnsTrue()
    {
        // Arrange
        _redis.GetDatabase()
            .KeyDeleteAsync(_basketId)
            .Returns(true);

        // Act
        var result = await _basketRepository.DeleteBasketAsync(_basketId);

        // Assert
        Assert.That(
            result,
            Is.True);

        await _redis.GetDatabase()
            .Received()
            .KeyDeleteAsync(_basketId);
    }

    [Test]
    public async Task DeleteBasketAsync_FailsToDelete_ReturnsFalse()
    {
        // Arrange
        _redis.GetDatabase()
            .KeyDeleteAsync(_basketId)
            .Returns(false);

        // Act
        var result = await _basketRepository.DeleteBasketAsync(_basketId);

        // Assert
        Assert.That(
            result,
            Is.False);

        await _redis.GetDatabase()
            .Received()
            .KeyDeleteAsync(_basketId);
    }

    #region Helpers

    internal const string _basketId = "testBasketId";

    internal const string _nonExistentBasketId = "nonExistentBasketId";

    internal readonly CustomerBasket _customerBasket = new()
        {
            Id = _basketId,
            Items = new List<BasketItem>(),
        };

    #endregion
}
