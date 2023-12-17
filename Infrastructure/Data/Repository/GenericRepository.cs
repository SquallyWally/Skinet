using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class GenericRepository<T> : IGenericRepository<T>
    where T : BaseEntity
{
    private readonly StoreContext _context;

    public GenericRepository(
        StoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<T> GetByIdAsync(
        int id) =>
        await _context.Set<T>()
            .FindAsync(id) ?? throw new InvalidOperationException();

    public async Task<IReadOnlyList<T>> ListAllAsync() =>
        await _context.Set<T>()
            .ToListAsync();

    public async Task<T?> GetEntityWithSpec(
        ISpecification<T> spec) =>
        await ApplySpecification(spec)
            .FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> ListAsync(
        ISpecification<T> spec) =>
        ( await ApplySpecification(spec)
            .ToListAsync() )!;

    public async Task<int> CountAsync(
        ISpecification<T> spec) =>
        await ApplySpecification(spec)
            .CountAsync();

    public void Add(
        T entity)
    {
        _context.Set<T>()
            .Add(entity);
    }

    public void Update(
        T entity)
    {
        _context.Set<T>()
            .Attach(entity);

        _context.Entry(entity)
            .State = EntityState.Modified;
    }

    public void Delete(
        T entity)
    {
        _context.Set<T>()
            .Remove(entity);
    }

    #region Internal

    private IQueryable<T?> ApplySpecification(
        ISpecification<T> spec) =>
        SpecificationEvaluator<T>.GetQuery(
            _context.Set<T>()
                .AsQueryable(),
            spec);

    #endregion
}
