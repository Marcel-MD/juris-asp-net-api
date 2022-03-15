using System.Linq.Expressions;
using Juris.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Juris.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DatabaseContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> GetById(long id)
    {
        return await _dbContext.FindAsync<T>(id);
    }

    public async Task Insert(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task InsertRange(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task Delete(long id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null) _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
            query = includes.Aggregate(query,
                (current, include) => current.Include(include));

        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (expression != null) query = query.Where(expression);

        if (includes != null)
            query = includes.Aggregate(query,
                (current, include) => current.Include(include));

        if (orderBy != null) query = orderBy(query);

        return await query.AsNoTracking().ToListAsync();
    }
}