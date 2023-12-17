using System.Collections;

using Core.Entities;
using Core.Interfaces;

using Infrastructure.Data.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _context;

    private Hashtable _repositories;

    public UnitOfWork(
        StoreContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseEntity
    {
        if ( _repositories == null )
        {
            _repositories = new Hashtable();
        }

        var type = typeof(TEntity).Name;

        if ( !_repositories.ContainsKey(type) )
        {
            var repositoryType = typeof(GenericRepository<>);

            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)),
                _context);

            _repositories.Add(
                type,
                repositoryInstance);
        }

        return (IGenericRepository<TEntity>) _repositories[type];
    }

    public async Task<int> Complete() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
