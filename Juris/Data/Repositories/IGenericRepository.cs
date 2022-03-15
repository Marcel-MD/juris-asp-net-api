using System.Linq.Expressions;
using Juris.Models.Entities;

namespace Juris.Data.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IList<T>> GetAll(
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includes
    );

    Task<T> Get(Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes);

    Task<T> GetById(long id);
    Task Insert(T entity);
    Task InsertRange(IEnumerable<T> entities);
    Task Delete(long id);
    void DeleteRange(IEnumerable<T> entities);
    void Update(T entity);
}