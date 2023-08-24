using Core.Entities;
using Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<Product> GetProductByIdAsync(
        int id) => await _context.Products.FindAsync(id) ?? throw new InvalidOperationException();
    public async Task<IReadOnlyList<Product>> GetProductsAsync() => await _context.Products.ToListAsync();
}