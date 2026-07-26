
using System.Linq.Expressions;
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

    public async Task<IEnumerable<T>> GetAllAsync<TKey>(Expression<Func<T, bool>>? condition,
    Expression<Func<T, TKey>>? orderBy, CancellationToken cancellationToken = default)
    {
        var query = this._dbSet as IQueryable<T>;

        if (condition != null)
            query = query.Where(condition);

        if (orderBy != null)
            query = query.OrderBy(orderBy)
            .ThenBy(t => t.CreatedAt);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default,
    params string[] includeProps)
    {
        var query = this._dbSet.Where(e => e.Id == id);
        foreach (var prop in includeProps)
        {
            query = query.Include(prop);
        }
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        this._dbSet.Update(entity);
        return Task.CompletedTask;
    }

}