using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T>
    where T : BaseEntity
{
    Task<T> GetByIdAsync(
        int id);

    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T?> GetEntityWithSpec(
        ISpecification<T> spec);

    Task<IReadOnlyList<T>> ListAsync(
        ISpecification<T> spec);

    Task<int> CountAsync(
        ISpecification<T> spec);

    // are not async because we actually use it to track the change in memory. The Unit of Work will handle the saving changes
    // are not async because we actually use it to track the change in memory. The Unit of Work will handle the saving changes
    void Add(
        T entity);

    void Update(
        T entity);

    void Delete(
        T entity);
}
