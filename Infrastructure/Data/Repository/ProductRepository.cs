using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Repository;

public class ProductRepository : IProductRepository
{
    public async Task<Product> GetProductByIdAsync(
        int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }
}