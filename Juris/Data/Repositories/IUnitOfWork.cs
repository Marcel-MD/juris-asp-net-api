using Juris.Models.Entities;

namespace Juris.Data.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<AppointmentRequest> AppointmentRequestRepository { get; }
    IGenericRepository<Profile> ProfileRepository { get; }
    IGenericRepository<Review> ReviewRepository { get; }
    IGenericRepository<Education> EducationRepository { get; }
    IGenericRepository<Experience> ExperienceRepository { get; }

    Task Save();
}