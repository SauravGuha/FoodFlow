
using FoodFlow.Application.Common.Repositories;
using FoodFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Persistence.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    protected readonly FoodFlowContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(FoodFlowContext context)
    {
        _context = context;
        this._dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await this._dbSet.AddAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        this._dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await this._dbSet.FindAsync(id, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        this._dbSet.Update(entity);
        return Task.CompletedTask;
    }

}