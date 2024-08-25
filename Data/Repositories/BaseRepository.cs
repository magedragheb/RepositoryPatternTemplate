using System.Linq.Expressions;
using Core.Constants;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Data;
public class BaseRepository<T>(RepoContext context) : IBaseRepository<T> where T : class
{
    public async Task<IEnumerable<T>?> GetAll() =>
        await context.Set<T>().ToListAsync();

    public async Task<T?> GetById(int id) =>
        await context.Set<T>().FindAsync(id);

    public async Task<T> Add(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
        return entities;
    }


    public Task<int> CountAsync(Expression<Func<T, bool>>? criteria)
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }



    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> Find(
        Expression<Func<T, bool>> criteria,
        string[]? includes)
    {
        IQueryable<T> query = context.Set<T>().AsNoTracking();
        if (includes != null)
            foreach (var item in includes)
                query = query.Include(item);
        return await query.SingleOrDefaultAsync(criteria);
    }

    public async Task<IEnumerable<T>?> FindAll(
        Expression<Func<T, bool>> criteria,
        int skip,
        int take,
        string[]? includes,
        Expression<Func<T, object>>? orderBy,
        string? orderByDirection)
    {
        IQueryable<T> query = context.Set<T>().AsNoTracking();
        if (includes != null)
            foreach (var item in includes)
                query = query.Include(item);
        if (orderBy != null)
        {
            if (orderByDirection == OrderBy.Ascending)
                query = query.OrderBy(orderBy);
            query = query.OrderByDescending(orderBy);
        }
        return await query.Where(criteria)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    Task IBaseRepository<T>.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Task IBaseRepository<T>.DeleteRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }
}