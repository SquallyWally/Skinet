using Core.Entities;
using Core.Interfaces;

using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.UnitOfWork
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        private IUnitOfWork _unitOfWork;
        private StoreContext _context;

        [SetUp]
        public void Setup()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new StoreContext(options);
            _unitOfWork = new Data.UnitOfWork(_context);
        }

        [Test]
        public void Repository_ReturnsSameInstanceForSameType()
        {
            // Arrange
            var repository1 = _unitOfWork.Repository<Product>();
            var repository2 = _unitOfWork.Repository<Product>();

            // Assert
            Assert.That(repository1, Is.SameAs(repository2));
        }

        [Test]
        public void Repository_ReturnsDifferentInstancesForDifferentTypes()
        {
            // Arrange
            var repository1 = _unitOfWork.Repository<Product>();
            var repository2 = _unitOfWork.Repository<ProductBrand>();

            // Assert
            Assert.That(repository1, Is.Not.SameAs(repository2));
        }

        /*
        [Test]
        public async Task Complete_SavesChangesToContext()
        {
            // Arrange
            var product = new Product { Name = "TestProduct", Price = 10.0m };
            var repository = _unitOfWork.Repository<Product>();
 

            // Act
            var result = await _unitOfWork.Complete();

            // Assert
            Assert.That(result, Is.EqualTo(1)); // One entity added, so one change expected

            // Ensure the entity is actually in the context after SaveChanges
            var productFromContext = await _context.Products.FirstOrDefaultAsync(p => p.Name == "TestProduct");
            Assert.That(productFromContext, Is.Not.Null);
            Assert.That(productFromContext?.Price, Is.EqualTo(10.0m));
        }
        */

        [TearDown]
        public void TearDown()
        {
            // Clean up in-memory database after each test
            _context.Database.EnsureDeleted();
        }
    }
}
