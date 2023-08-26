using Core.Entities;
using Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(
        StoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Product> GetProductByIdAsync(
        int id) =>
        await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id) ?? throw new InvalidOperationException();

    public async Task<IReadOnlyList<Product>> GetProductsAsync() =>
        await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();

    public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync() => await _context.ProductBrands.ToListAsync();

    public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync() => await _context.ProductTypes.ToListAsync();
}
