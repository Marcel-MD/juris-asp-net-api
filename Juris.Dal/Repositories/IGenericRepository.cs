using System.Linq.Expressions;
using Juris.Domain.Entities;

namespace Juris.Dal.Repositories;

/// <summary>
///     Generic repository with basic CRUD operations.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Gets all entities.
    /// </summary>
    /// <param name="expression">Custom filtering function.</param>
    /// <param name="orderBy">Custom ordering function.</param>
    /// <param name="pageNumber">Page number, requires <b>pageSize</b> to be specified.</param>
    /// <param name="pageSize">Number of entities in a page.</param>
    /// <param name="includes">Functions that specify what entities to include.</param>
    /// <returns>List of entities.</returns>
    Task<IList<T>> GetAll(
        Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? pageNumber = null, int? pageSize = null,
        params Expression<Func<T, object>>[] includes
    );

    /// <summary>
    ///     Get one entity.
    /// </summary>
    /// <param name="expression">Custom filtering function.</param>
    /// <param name="includes">Functions that specify what entities to include.</param>
    /// <returns>Entity or null if doesn't exist.</returns>
    Task<T> Get(Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes);

    /// <summary>
    ///     Get entity by id.
    /// </summary>
    /// <param name="id">Id of the entity.</param>
    /// <returns>Entity or null if doesn't exist.</returns>
    Task<T> GetById(long id);

    /// <summary>
    ///     Insert entity.
    /// </summary>
    Task Insert(T entity);

    /// <summary>
    ///     Insert list of entities.
    /// </summary>
    Task InsertRange(IEnumerable<T> entities);

    /// <summary>
    ///     Delete entity by id.
    /// </summary>
    /// <param name="id">Id of the entity.</param>
    Task Delete(long id);

    /// <summary>
    ///     Delete list of entities.
    /// </summary>
    void DeleteRange(IEnumerable<T> entities);

    /// <summary>
    ///     Update entity.
    /// </summary>
    void Update(T entity);
}