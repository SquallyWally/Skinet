﻿using Core.Entities;
using Core.Interfaces;

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
}
