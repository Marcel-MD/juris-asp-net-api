using System.Linq.Expressions;
using Juris.Models.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Juris.Data.IRepositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IList<T>> GetAll(
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    );

    Task<T> Get(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    );

    Task<T> GetById(long id);
    Task Insert(T entity);
    Task InsertRange(IEnumerable<T> entities);
    Task Delete(int id);
    void DeleteRange(IEnumerable<T> entities);
    void Update(T entity);
}