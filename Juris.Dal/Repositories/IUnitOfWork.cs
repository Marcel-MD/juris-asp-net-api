using Juris.Domain.Entities;

namespace Juris.Dal.Repositories;

/// <summary>
///     The unit of work class serves one purpose: to make sure that when you use multiple repositories, they share a
///     single database context. That way, when a unit of work is complete you can call the SaveChanges method on that
///     instance of the context and be assured that all related changes will be coordinated. Each repository property
///     checks whether the repository already exists. If not, it instantiates the repository, passing in the context
///     instance. As a result, all repositories share the same context instance.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<AppointmentRequest> AppointmentRequestRepository { get; }
    IGenericRepository<Profile> ProfileRepository { get; }
    IGenericRepository<Review> ReviewRepository { get; }
    IGenericRepository<Education> EducationRepository { get; }
    IGenericRepository<Experience> ExperienceRepository { get; }
    IGenericRepository<ProfileCategory> ProfileCategoryRepository { get; }
    IGenericRepository<City> CityRepository { get; }

    /// <summary>
    ///     The Save method calls SaveChanges on the database context.
    /// </summary>
    Task Save();
}