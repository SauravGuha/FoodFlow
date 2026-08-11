
using System.Linq.Expressions;
using FoodFlow.Domain.Models;

namespace FoodFlow.Application.Common.Repositories;

public interface IBaseRepository<T> where T : BaseModel
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default,
    params string[] includeProps);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetAllAsync<TKey>(Expression<Func<T, bool>>? condition, Expression<Func<T, TKey>>? orderBy, CancellationToken cancellationToken = default);

    Task<int> GetQueryCount<TKey>(Expression<Func<T, bool>>? condition, Expression<Func<T, TKey>>? orderBy, CancellationToken cancellationToken = default);
}