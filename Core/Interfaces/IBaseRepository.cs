using System.Linq.Expressions;

namespace Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAll();
    Task<T?> GetById(int id);
    Task<T> Add(T entity);
    Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
    Task<T> Update(T entity);
    Task Delete(int id);
    Task DeleteRange(IEnumerable<T> entities);
    Task<T?> Find(Expression<Func<T, bool>> predicate, string[]? includes);

    Task<IEnumerable<T>?> FindAll(
        Expression<Func<T, bool>> predicate,
        int skip,
        int take,
        string[]? includes,
        Expression<Func<T, object>> orderBy,
        string? orderByDirection);

    Task<int> CountAsync(Expression<Func<T, bool>>? criteria);
}